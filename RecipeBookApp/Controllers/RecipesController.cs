using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.Core.DTOs;
using RecipeBook.Core.Entities;
using RecipeBook.Core.Interfaces;
using System.Text.RegularExpressions;

namespace RecipeRecipe.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeUOW _unitOfWork;

        private readonly IValidator<RecipeDTO> _recipeValidator;

        public RecipesController(IRecipeUOW unitOfWork, IValidator<RecipeDTO> recipeValidator)
        {
            _unitOfWork = unitOfWork;
            _recipeValidator = recipeValidator;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(CancellationToken token)
        {
            try
            {
                var recipes = await _unitOfWork.Recipes.GetAllAsync(r => new RecipeListItemDTO
                {
                    Id = r.Id,
                    Name = r.Name,
                    CreationDate = r.CreationDate
                }, token);

                return Ok(recipes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching recipes: {ex.Message}");
            }
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string id, CancellationToken token)
        {
            try
            {
                var recipe = await _unitOfWork.Recipes.GetByIdAsync(
                            r => r.Id == id,
                            r => new RecipeDTO
                            {
                                Id = r.Id,
                                Name = r.Name,
                                Instructions = r.Instructions,
                                Ingredients = r.Ingredients.Select(ri => new RecipeIngredientDTO
                                {
                                    Name = ri.Name,
                                    Quantity = ri.Quantity,
                                    MeasuringUnit = ri.MeasuringUnit
                                }).ToList()
                            }, token);

                if (recipe == null)
                {
                    return NotFound($"Recipe with ID {id} not found");
                }
                return Ok(recipe);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching recipe: {ex.Message}");
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] RecipeDTO recipe, CancellationToken token)
        {
            try
            {
                var validationResult = await _recipeValidator.ValidateAsync(recipe, token);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }
                var createdRecipe = await _unitOfWork.Recipes.AddAsync(MapDtoToEntity(recipe));
                await _unitOfWork.Complete(token);
                return Ok(createdRecipe.Id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating recipe: {ex.Message}");
            }
        }

        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(RecipeDTO recipe, CancellationToken token)
        {
            try
            {
                var validationResult = await _recipeValidator.ValidateAsync(recipe, token);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }
                var existingRecipe = await _unitOfWork.Recipes.GetByIdWithIncludesAsync(r => r.Id == recipe.Id, token, r => r.Ingredients);

                if (existingRecipe == null)
                {
                    return NotFound($"Recipe with ID {recipe.Id} not found");
                }

                existingRecipe.Name = recipe.Name;
                existingRecipe.Instructions = recipe.Instructions;

                _unitOfWork.Ingredients.DeleteRange(existingRecipe.Ingredients);

                existingRecipe.Ingredients = recipe.Ingredients.Select(i => new RecipeIngredient
                {
                    Name = i.Name,
                    Quantity = i.Quantity,
                    MeasuringUnit = i.MeasuringUnit,
                }).ToList();


                var updatedRecipe = _unitOfWork.Recipes.Update(existingRecipe);
                await _unitOfWork.Complete(token);
                return Ok(updatedRecipe.Id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating recipe: {ex.Message}");
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(string id, CancellationToken token)
        {
            try
            {
                var existingRecipe = await _unitOfWork.Recipes.GetByIdWithIncludesAsync(r => r.Id == id, token, r => r.Ingredients);

                if (existingRecipe == null)
                {
                    return NotFound($"Recipe with ID {id} not found");
                }
                _unitOfWork.Recipes.Delete(existingRecipe);
                await _unitOfWork.Complete(token);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting recipe: {ex.Message}");
            }
        }
        private Recipe MapDtoToEntity(RecipeDTO recipeDto)
        {
            var recipe = new Recipe
            {
                Id = ShortGUID(),
                Name = recipeDto.Name,
                Instructions = recipeDto.Instructions,
                CreationDate = DateTime.Now,
                CreatedById = "UserId",
                Ingredients = recipeDto.Ingredients.Select(i => new RecipeIngredient
                {
                    Id = ShortGUID(),
                    Name = i.Name,
                    Quantity = i.Quantity,
                    MeasuringUnit = i.MeasuringUnit,
                }).ToList()
            };

            return recipe;
        }
        public static string ShortGUID()
        {
            return Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
        }

    }
}
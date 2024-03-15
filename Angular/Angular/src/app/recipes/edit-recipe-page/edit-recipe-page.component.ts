import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Recipe } from 'src/app/_models/recipe/Recipe';
import { MeasuringUnit, RecipeIngredient } from 'src/app/_models/recipe/RecipeIngredient';
import { RecipeService } from 'src/app/services/recipe.service';
import { forIn } from 'lodash';

@Component({
  selector: 'app-edit-recipe-page',
  templateUrl: './edit-recipe-page.component.html',
  styleUrls: ['./edit-recipe-page.component.css'],
})
export class EditRecipeComponent implements OnInit {
  editRecipeForm: FormGroup;
  measuringUnits: { value: number; name: string }[] = [];
  ingredientsList: { name: string, quantity: number, measuringUnit: MeasuringUnit }[] = [];

  recipe!: Recipe;

  constructor(
    private fb: FormBuilder,
    public recipeService: RecipeService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.editRecipeForm = this.fb.group({});
  }

  ngOnInit(): void {
    this.editRecipeForm = this.fb.group({
      name: [
        '',
        [Validators.required, Validators.pattern('[A-Za-zء-ي_ , ، ]{3,}')],
      ],
      instructions: ['', [Validators.pattern('[A-Za-zء-ي _ , ، ]{3,}')]],
      ingredient: [
        '',
        [Validators.required, Validators.pattern('[A-Za-zء-ي_ , ، ]{3,}')],
      ],
      quantity: [
        '',
        [Validators.required, Validators.min(0)],
      ],
      measuringUnit: ['', Validators.required]
    });

    forIn(MeasuringUnit, (value, key) => {
      if (!isNaN(Number(value))) {
        this.measuringUnits.push({ value, name: key });
      }
    });

    this.route.params.subscribe((params) => {
      const Id = params['recipeid'];
      this.recipeService.getById(Id).subscribe((item) => {
        this.recipe = item;
        console.log(this.recipe);
        this.ingredientsList = item.ingredients;
      });
    });
  }

  onBack(): void {
    this.editRecipeForm.reset();
    this.router.navigate(['home']);
  }

  addIngredient() {
    const ingredientControl = this.editRecipeForm.get('ingredient');
    const quantityControl = this.editRecipeForm.get('quantity');
    const measuringUnitControl = this.editRecipeForm.get('measuringUnit');

    if (ingredientControl?.invalid || quantityControl?.invalid || measuringUnitControl?.invalid) {
      return;
    }

    const ingredientName = ingredientControl?.value;
    const quantity = quantityControl?.value;
    const measuringUnit = measuringUnitControl?.value;

    this.ingredientsList.push({
      name: ingredientName,
      quantity: quantity,
      measuringUnit: measuringUnit as MeasuringUnit
    });

    ingredientControl?.reset();
    quantityControl?.reset();
    measuringUnitControl?.reset();
  }

  addRecipe(e: Event) {
    e.preventDefault();

    const name = this.name?.value ?? '';
    const instructions = this.instructions?.value ?? '';
    const ingredients = this.ingredientsList.map(ingredient => {
      return new RecipeIngredient("", ingredient.name, ingredient.quantity, Number(ingredient.measuringUnit));
    });

    this.recipe = new Recipe("", name, instructions, ingredients);

    this.recipeService.edit(this.recipe).subscribe((id) => {
      console.log(id);
      this.editRecipeForm.reset();
      this.router.navigate([`details/${id}`]);
    });
  }

  get name() {
    return this.editRecipeForm.get('name');
  }

  get ingredient() {
    return this.editRecipeForm.get('ingredient');
  }

  get instructions() {
    return this.editRecipeForm.get('instructions');
  }

  get quantity() {
    return this.editRecipeForm.get('quantity');
  }

  get measuringUnit() {
    return this.editRecipeForm.get('measuringUnit')?.value;
  }

  getMeasuringUnitName(unit: MeasuringUnit): string {
    return MeasuringUnit[unit];
  }
}

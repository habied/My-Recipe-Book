import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Recipe } from 'src/app/_models/recipe/Recipe';
import { MeasuringUnit, RecipeIngredient } from 'src/app/_models/recipe/RecipeIngredient';
import { RecipeService } from 'src/app/services/recipe.service';
import { forIn } from 'lodash';

@Component({
  selector: 'app-create-recipe-page',
  templateUrl: './create-recipe-page.component.html',
  styleUrls: ['./create-recipe-page.component.css'],
})
export class CreateRecipeComponent implements OnInit {
  createRecipeForm: FormGroup;
  measuringUnits: { value: number; name: string }[] = [];
  ingredientsList: { name: string, quantity: number, measuringUnit: MeasuringUnit }[] = [];

  recipe!: Recipe;

  constructor(
    private fb: FormBuilder,
    public recipeService: RecipeService,
    private router: Router
  ) {
    forIn(MeasuringUnit, (value, key) => {
      if (!isNaN(Number(value))) {
        this.measuringUnits.push({ value, name: key });
      }
    });

    this.createRecipeForm = this.fb.group({
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
  }

  ngOnInit(): void {
  }

  onBack(): void {
    this.createRecipeForm.reset();
    this.router.navigate(['home']);
  }

  addIngredient() {
    const ingredientControl = this.createRecipeForm.get('ingredient');
    const quantityControl = this.createRecipeForm.get('quantity');
    const measuringUnitControl = this.createRecipeForm.get('measuringUnit');

    if (ingredientControl?.invalid || quantityControl?.invalid || measuringUnitControl?.invalid) {
      return;
    }

    const ingredientName = ingredientControl?.value;
    const quantity = quantityControl?.value;
    const measuringUnit = this.measuringUnit;

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

    this.recipeService.create(this.recipe).subscribe((id:any) => {
      this.createRecipeForm.reset();
      this.router.navigate([`details/${id}`]);
    });
  }



  get name() {
    return this.createRecipeForm.get('name');
  }

  get ingredient() {
    return this.createRecipeForm.get('ingredient');
  }

  get instructions() {
    return this.createRecipeForm.get('instructions');
  }

  get quantity() {
    return this.createRecipeForm.get('quantity');
  }

  get measuringUnit() {
    return this.createRecipeForm.get('measuringUnit')?.value;
  }

  getMeasuringUnitName(unit: MeasuringUnit): string {
    return MeasuringUnit[unit];
  }


}

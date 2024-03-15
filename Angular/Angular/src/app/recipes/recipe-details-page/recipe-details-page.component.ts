import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { Recipe } from 'src/app/_models/recipe/Recipe';
import { MeasuringUnit, RecipeIngredient } from 'src/app/_models/recipe/RecipeIngredient';
import { RecipeService } from 'src/app/services/recipe.service';

@Component({
  selector: 'app-recipe-details-page',
  templateUrl: './recipe-details-page.component.html',
  styleUrls: ['./recipe-details-page.component.css'],
})
export class RecipeDetailsComponent implements OnInit {
  constructor(
    private recipeService: RecipeService,
    private route: ActivatedRoute,
  ) {}

  ngOnInit() {
    this.route.params.subscribe((params) => {
      const Id = params['recipeid'];
      this.recipeService.getById(Id).subscribe((item) => {
        this.recipe = item;
        this.ingredients = item.ingredients;
      });
    });
  }

  recipe: Recipe | null = null;
  ingredients: RecipeIngredient[] | null = [] ;

  getMeasuringUnitLabel(unit: MeasuringUnit): string {
    switch (unit) {
      case MeasuringUnit.Count:
        return 'Unit';
      case MeasuringUnit.Cup:
        return 'Kilogram';
      case MeasuringUnit.Gram:
        return 'Gram';
      case MeasuringUnit.Ounce:
        return 'Liter';
      case MeasuringUnit.Milliliter:
        return 'Milliliter';
      case MeasuringUnit.Tablespoon:
        return 'Tablespoon';
      case MeasuringUnit.Teaspoon:
        return 'Teaspoon';
      default:
        return '';
    }
  }
}

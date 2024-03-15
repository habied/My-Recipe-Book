import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';

import { CreateRecipeComponent } from './create-recipe-page/create-recipe-page.component';
import { EditRecipeComponent } from './edit-recipe-page/edit-recipe-page.component';
import { RecipeDetailsComponent } from './recipe-details-page/recipe-details-page.component';
import { GetAllRecipesComponent } from './get-all-recipes-page/get-all-recipes-page.component';
import { RecipeRoutingModule } from './recipe-routing-module';

@NgModule({
  declarations: [
    CreateRecipeComponent,
    EditRecipeComponent,
    RecipeDetailsComponent,
    GetAllRecipesComponent
  ],
  imports: [CommonModule, RecipeRoutingModule, ReactiveFormsModule, FormsModule],
  exports: [CreateRecipeComponent, EditRecipeComponent, RecipeDetailsComponent, GetAllRecipesComponent],
})
export class RecipeModule {}

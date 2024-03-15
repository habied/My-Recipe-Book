import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';

import { CreateRecipeComponent } from './create-recipe-page/create-recipe-page.component';
import { EditRecipeComponent } from './edit-recipe-page/edit-recipe-page.component';
import { GetAllRecipesComponent } from './get-all-recipes-page/get-all-recipes-page.component';
import { RecipeDetailsComponent } from './recipe-details-page/recipe-details-page.component';

const routes: Routes = [
  { path: '', component: GetAllRecipesComponent },
  { path: 'details/:recipeid', component: RecipeDetailsComponent },
  {
    path: 'edit/:recipeid',
    component: EditRecipeComponent,
  },
  {
    path: 'create',
    component: CreateRecipeComponent,
  },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class RecipeRoutingModule {}

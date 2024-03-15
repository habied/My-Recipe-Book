import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RecipeModule } from './recipes/recipe.module';
import { GetAllRecipesComponent } from './recipes/get-all-recipes-page/get-all-recipes-page.component';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: GetAllRecipesComponent },
  {
    path: 'recipe',
    loadChildren: () => import('./recipes/recipe.module').then((m) => m.RecipeModule),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}

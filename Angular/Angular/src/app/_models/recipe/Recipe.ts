import { RecipeIngredient } from "./RecipeIngredient";

export class Recipe {
  constructor(
    public id: string,
    public name: string,
    public instructions: string,
    public ingredients: RecipeIngredient[],
  ) {}
}


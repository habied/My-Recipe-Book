export class RecipeIngredient {
  id: string;
  name: string;
  quantity: number;
  measuringUnit: number;

  constructor(id: string, name: string, quantity: number, measuringUnit: number) {
    this.id = id;
    this.name = name;
    this.quantity = quantity;
    this.measuringUnit = measuringUnit;
  }
}

export enum MeasuringUnit {
  Teaspoon = 1,
  Tablespoon = 2,
  Cup = 3,
  Ounce = 4,
  Gram = 5,
  Milliliter = 6,
  Count = 7
}

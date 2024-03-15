import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Recipe } from 'src/app/_models/recipe/Recipe';
import { RecipeService } from 'src/app/services/recipe.service';

@Component({
  selector: 'app-get-all-recipes-page',
  templateUrl: './get-all-recipes-page.component.html',
  styleUrls: ['./get-all-recipes-page.component.css'],
})
export class GetAllRecipesComponent implements OnInit {
  constructor(
    private router: Router,
    public recipeService: RecipeService,
    private route: ActivatedRoute,
  ) {
  }

  ngOnInit(): void {
          this.recipeService.getAll().subscribe((recipes) => {
          this.recipes = recipes;
        });
  }


  recipes: Recipe[] = [];
  categories = [
    { text: 'breakfast', icon: 'fa-coffee', description: 'Popular' },
    { text: 'dinner', icon: 'fa-hamburger', description: 'Special' },
    { text: 'lunch', icon: 'fa-utensils', description: 'Lovely' },
  ];

  deletedItem: Recipe | null = null;

  openModal(event: Event, item: Recipe) {
    event.stopPropagation();
    this.deletedItem = item;
    $('#myModal').modal('show');
  }

  openCreate(event: Event) {
    console.log("create");
    event.stopPropagation();
    this.router.navigate(['create']);
  }

  openEdit(event: Event, item: Recipe) {
    console.log("edit");
    event.stopPropagation();
    this.router.navigate(['edit',item.id]);
  }
  openDetails(event: Event, item: Recipe) {
    console.log("details");
    event.stopPropagation();
    this.router.navigate([`details/${item.id}`]);
  }

  deleteItem() {
    if (this.deletedItem) {
      this.recipeService.delete(this.deletedItem.id).subscribe(() => {
        $('#myModal').modal('hide');
        this.recipes = this.recipes.filter(
          (item) => item.id !== this.deletedItem?.id
        );
      });
    }
  }
}

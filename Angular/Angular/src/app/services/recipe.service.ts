import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Recipe } from '../_models/recipe/Recipe';
import { BehaviorSubject, Observable } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class RecipeService {
  constructor(public http: HttpClient) {}
  baseUrl: string = environment.apiUrl + '/Recipes';

  getAll() {
    return this.http.get<Recipe[]>(`${this.baseUrl}/GetAll`);
  }

  getById(id: string) {
    return this.http.get<Recipe>(`${this.baseUrl}/GetById?id=${id}`);
  }

  create(item: Recipe) {
    return this.http.post(`${this.baseUrl}/Create`, item);
  }

  edit(item: Recipe) {
    return this.http.put(`${this.baseUrl}/Edit`, item);
  }

  delete(id: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}/Delete?id=${id}`);
  }
}

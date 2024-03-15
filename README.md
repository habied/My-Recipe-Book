# My Recipe Book

My Recipe Book is a web application that allows users to manage their recipes. Users can create, view, edit, and delete recipes along with their ingredients and instructions.

## Table of Contents

- [Technologies Used](#technologies-used)
- [Project Structure](#project-structure)
- [API Endpoints](#api-endpoints)

## Technologies Used

- **ASP.NET Core**: The backend of the application is built using ASP.NET Core, a cross-platform, high-performance framework for building modern, cloud-based, internet-connected applications.
- **Entity Framework Core**: Entity Framework Core is used as the ORM (Object-Relational Mapper) to interact with the database.
- **Angular**: The frontend of the application is developed using Angular, a TypeScript-based open-source web application framework.
- **Angular Material**: Angular Material is used for the UI components and design elements.
- **Microsoft SQL Server**: The application uses Microsoft SQL Server as its database to store recipes and ingredients.
- **Lodash**: Lodash library is used for utility functions.
- **Bootstrap**: Bootstrap is used for responsive styling.

## Project Structure

The project follows a standard architecture pattern for a .NET Core Web API and an Angular frontend. Here is a brief overview of the project structure:

- **RecipeBook.Core**: Contains the core entities and DTOs (Data Transfer Objects) used across the application.
- **RecipeBook.EF**: Contains the Entity Framework related configurations, database context, and repositories.
- **RecipeRecipe.Web**: The ASP.NET Core Web API project.
- **Angular**: The Angular frontend application.
  - **src/app**: Contains Angular components, services, and modules.
    - **_models**: Contains Angular models that mirror the entities in the backend.
    - **_services**: Contains Angular services to interact with the backend API.
    - **recipes**: Contains components related to managing recipes.
    - **shared**: Contains shared components and utilities.

## API Endpoints

### Recipes Controller

- **GET /api/recipes/GetAll**: Retrieves all recipes.
- **GET /api/recipes/GetById**: Retrieves a recipe by ID.
- **POST /api/recipes/Create**: Creates a new recipe.
- **POST /api/recipes/Edit**: Edits an existing recipe.
- **DELETE /api/recipes/Delete**: Deletes a recipe.

### Ingredients Controller

- **GET /api/ingredients/GetAll**: Retrieves all ingredients.
- **GET /api/ingredients/GetById**: Retrieves an ingredient by ID.
- **POST /api/ingredients/Create**: Creates a new ingredient.
- **POST /api/ingredients/Edit**: Edits an existing ingredient.
- **DELETE /api/ingredients/Delete**: Deletes an ingredient.

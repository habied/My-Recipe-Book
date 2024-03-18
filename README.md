# My Recipe Book

My Recipe Book is a web application that allows users to manage their recipes. Users can create, view, edit, and delete recipes along with their ingredients and instructions.

## Table of Contents

- [Technologies Used](#technologies-used)
- [Project Structure](#project-structure)
- [API Endpoints](#api-endpoints)

---

## Technologies Used

### Backend
- **ASP.NET Core**: A cross-platform, high-performance framework for building modern, cloud-based, internet-connected applications.
- **Entity Framework Core**: ORM used to interact with the database.
- **Microsoft SQL Server**: Database for storing recipes and ingredients.

### Frontend
- **Angular**: A TypeScript-based open-source web application framework.
- **Angular Material**: UI components and design elements.
- **Bootstrap**: Used for responsive styling.

### Authentication
- **Google Authentication (SSO)**: Integrated for secure user authentication.

---

## Project Structure
The project follows an n-tier architecture with the repository pattern and unit of work:

- **Repositories**: Contains data access logic using the repository pattern.
- **Services**: Business logic layer where business rules and operations are implemented.
- **Controllers**: Handles incoming HTTP requests, interacts with services, and returns HTTP responses.

### Layers
- **RecipeBook.Core**: Contains core entities and DTOs used across the application.
- **RecipeBook.EF**: Entity Framework configurations, database context, and repositories.
- **RecipeRecipe.Web**: Main ASP.NET Core Web API project.

---

## API Endpoints

### Recipes Controller
- `GET /api/recipes/GetAll`: Retrieves all recipes.
- `GET /api/recipes/GetById`: Retrieves a recipe by ID.
- `POST /api/recipes/Create`: Creates a new recipe.
- `POST /api/recipes/Edit`: Edits an existing recipe.
- `DELETE /api/recipes/Delete`: Deletes a recipe.


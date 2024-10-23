# Cat API

A .NET 8 web application that fetches cat images from the Cat API and stores them in a SQL Server database. This project demonstrates how to integrate external APIs and handle database operations using Entity Framework Core.

## Features

- Fetches cat images from an external API.
- Stores fetched cat images in a SQL Server database.
- Supports Windows Authentication.
- Provides a simple RESTful API to access cat images.

## Technologies Used

- **.NET 8**
- **Entity Framework Core**
- **SQL Server**
- **HttpClient for API requests**

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server
- Visual Studio 2022

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/CatAPI.git
2. Open the solution in Visual Studio 2022.
3. Update the appsettings.json file with your SQL Server connection string.
4. Build the solution.

### Running the application

- Run the application using IIS Express.
- Access the API at https://localhost:44372/api/cats.
- Access the Swagger UI at https://localhost:44372/swagger/index.html

## API Endpoints

### 1. Fetch Cats

- **URL**: `/api/cats/fetch`
- **Method**: `POST`
- **Description**: This endpoint fetches cat images from an external API and saves them to the database. It also generates and saves associated tags for each cat based on their temperament.

### 2. Get Cat by ID

- **URL**: `/api/cats/{id}`
- **Method**: `GET`
- **Description**: This endpoint retrieves the details of a specific cat by its ID.
- **URL Parameters**:
  - `id` (integer): The ID of the cat to retrieve.

### 3. Get Cats

- **URL**: `/api/cats`
- **Method**: `GET`
- **Description**: This endpoint retrieves a list of cats from the database. It supports pagination and optional filtering by tags.
- **Query Parameters**:
  - `page` (integer): The page number for pagination (default is 1).
  - `pageSize` (integer): The number of cats to return per page (default is 10).
  - `tag` (string): A tag to filter the cats by.


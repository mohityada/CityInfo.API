# CityInfo.API

CityInfo.API is a RESTful web API that provides information about cities and their points of interest. It serves as a backend service for applications that require city data, such as tourism apps or location-based services.

---

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Installation](#installation)
- [Usage](#usage)
- [API Endpoints](#api-endpoints)
- [Contributing](#contributing)
- [License](#license)

---

## Introduction

CityInfo.API is designed to provide structured city data, including points of interest like landmarks, restaurants, and attractions. It allows developers to query city-related information efficiently and integrate it into their applications.

---

## Features

- RESTful API for city and point-of-interest data.
- Supports CRUD operations for city and point-of-interest entities.
- Built using .NET Core for robust performance.
- Structured JSON responses for easy integration.
- Secure endpoints with authentication and authorization (optional).

---

## Technologies Used

- **Language**: C#
- **Framework**: .NET Core
- **Database**: SQL Server (or any other compatible database)
- **Tools**: Entity Framework Core, Swagger for API documentation

---

## Installation

Follow these steps to set up the project locally:

1. Clone the repository:
   ```bash
   git clone https://github.com/mohityada/CityInfo.API
   ```

2. Navigate to the project directory:
   ```bash
   cd CityInfo.API
   ```

3. Restore dependencies:
   ```bash
   dotnet restore
   ```

4. Update the database connection string in `appsettings.json`:
   ```json
   "ConnectionStrings": {
       "CityInfoDB": "YourDatabaseConnectionString"
   }
   ```

5. Apply database migrations:
   ```bash
   dotnet ef database update
   ```

6. Run the application:
   ```bash
   dotnet run
   ```

---

## Usage

### Testing the API

1. Open your browser or a tool like Postman.
2. Use the base URL:
   ```
   http://localhost:5000/api
   ```
3. Explore the endpoints listed in the [API Endpoints](#api-endpoints) section.

### Swagger UI
The API comes with Swagger documentation. Navigate to:
```
http://localhost:5000/swagger
```
---

## API Endpoints

### Cities
- **GET /api/cities**: Retrieve a list of all cities.
- **GET /api/cities/{id}**: Retrieve details of a specific city.
- **POST /api/cities**: Add a new city.
- **PUT /api/cities/{id}**: Update an existing city.
- **DELETE /api/cities/{id}**: Delete a city.

### Points of Interest
- **GET /api/cities/{cityId}/pointsofinterest**: Retrieve points of interest for a specific city.
- **GET /api/cities/{cityId}/pointsofinterest/{id}**: Retrieve details of a specific point of interest.
- **POST /api/cities/{cityId}/pointsofinterest**: Add a new point of interest.
- **PUT /api/cities/{cityId}/pointsofinterest/{id}**: Update an existing point of interest.
- **DELETE /api/cities/{cityId}/pointsofinterest/{id}**: Delete a point of interest.

---

## Contributing

Contributions are welcome! Follow these steps to contribute:

1. Fork the repository.
2. Create a feature branch:
   ```bash
   git checkout -b feature/your-feature-name
   ```
3. Commit your changes:
   ```bash
   git commit -m "Description of your feature"
   ```
4. Push to your forked repository:
   ```bash
   git push origin feature/your-feature-name
   ```
5. Open a pull request.

---

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.

---

## Contact

For any questions or suggestions, feel free to open an issue or contact [Mohit Yadav](https://github.com/mohityada).

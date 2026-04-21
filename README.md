
# ContactList

*A web application built with .NET 10 to manage a contact database with categories and subcategories, supporting full CRUD operations and Docker orchestration.*
---

## 📖 Libraries & Dependencies

The following technologies and libraries were used in this project:

* **.NET 10** - Core framework.
* **Microsoft.EntityFrameworkCore** - ORM for database operations.
* **MySql.EntityFrameworkCore** - MySQL provider for EF Core.
* **Microsoft.VisualStudio.Azure.Containers.Tools** - Containerization support.
* **Microsoft.AspNetCore.Authentication.JwtBearer** - JWT Token support

---

## 💻 Classes and Methods Overview

### Controllers

#### Class: `LoginController`
| Method | Description |
| :--- | :--- |
| `Login()` | Returns generated bearer JWT token. If token generation fails, returns `400 BadRequest`. |

#### Class: `CategoriesController`
| Method | Description |
| :--- | :--- |
| `GetAll()` | Returns all categories from the database |
| `GetById(Guid id)` | Returns category with the specified ID |
| `GetSubCategoriesByCategoryId(Guid id)` | Returns all subcategories for the specified category ID |

#### Class: `SubCategoriesController`
| Method | Description |
| :--- | :--- |
| `GetAll()` | Returns all subcategories from the database |
| `GetById(Guid id)` | Returns subcategory with the specified ID |

#### Class: `ContactsController`
| Method | Description |
| :--- | :--- |
| `GetAll()` | Returns all contacts from the database |
| `GetById(Guid id)` | Returns contact with the specified ID. If not found returns `404 NotFound` |
| `CreateContact(CreateContactDTO createContactDto)` | Creates a contact from provided DTO. Returns `400 BadRequest` on validation failure |
| `UpdateContact(Guid id, UpdateContactDTO updateContactDto)` | Updates contact with given ID using DTO. Returns `404 NotFound` if not found, `400 BadRequest` on validation failure |
| `DeleteContact(Guid id)` | Deletes contact with given ID. Returns `404 NotFound` if not found |
---

### Models

#### Class: `Contact`
| Property | Description |
| :--- | :--- |
| `Id` | Contact identifier (Guid) |
| `Name` | First name |
| `Surname` | Last name |
| `Email` | Email address |
| `Password` | Encrypted/plain password field used by DTO conversion/validation |
| `CategoryId?` | FK to Category (nullable) |
| `SubCategoryId?` | FK to SubCategory (nullable) |
| `CustomSubCategory?` | Custom subcategory text |
| `Phone` | Phone number |
| `BirthDate` | Date of birth (DateOnly) |
| `Category?` | Navigation property to Category |
| `SubCategory?` | Navigation property to SubCategory |
| `ToDTO()` | Converts entity to `ContactDTO` |

#### Class: `Category`
| Property | Description |
| :--- | :--- |
| `Id` | Category identifier (Guid) |
| `Name` | Category name |
| `SubCategories` | Collection of `SubCategory` |
| `Contacts` | Collection of `Contact` |
| `ToDTO()` | Converts entity to `CategoryDTO` |

#### Class: `SubCategory`
| Property | Description |
| :--- | :--- |
| `Id` | Subcategory identifier (Guid) |
| `Name` | Subcategory name |
| `CategoryId` | FK to parent Category |
| `Category` | Navigation to parent Category |
| `Contacts` | Collection of `Contact` |
| `ToDTO()` | Converts entity to `SubCategoryDTO` |

### Data / Repositories

#### `ContactsRepository` (implements `IContactsRepository`)
| Method | Description |
| :--- | :--- |
| `Create(Contact contact)` | Adds and saves contact to DB |
| `Update(Contact contact)` | Validates uniqueness and updates existing contact |
| `Delete(Guid id)` | Removes contact from DB if exists |
| `GetAll()` | Returns all contacts from DB |
| `GetById(Guid id)` | Returns contact or throws KeyNotFoundException |
| `Exists(...)` | Existence checks |

#### `CategoriesRepository` (implements `ICategoriesRepository`)
| Method | Description |
| :--- | :--- |
| `GetAll()` | Returns all categories |
| `GetById(Guid id)` | Returns category or throws KeyNotFoundException |
| `Exists(Guid id)` | Existence check |

#### `SubCategoriesRepository` (implements `ISubCategoriesRepository`)
| Method | Description |
| :--- | :--- |
| `GetAll()` | Returns all subcategories |
| `GetById(Guid id)` | Returns subcategory or throws KeyNotFoundException |
| `GetByCategoryId(Guid categoryId)` | Returns subcategories filtered by category |
| `Exists(Guid id)` | Existence check |

### Services

#### `ContactsService` (implements `IContactsService`)
| Method | Description |
| :--- | :--- |
| `Create(CreateContactDTO)` | Validates DTO (email/password), converts to entity and calls repository |
| `Update(Guid, UpdateContactDTO)` | Validates and updates contact via repository |
| `Delete(Guid)` | Deletes contact via repository |
| `GetAll()` | Returns contacts as DTOs |
| `GetById(Guid)` | Returns single contact DTO or null |

#### `CategoriesService` (implements `ICategoriesService`)
| Method | Description |
| :--- | :--- |
| `GetAllCategories()` | Returns all categories |
| `GetCategoryById(Guid)` | Returns category or null |
| `GetSubCategoriesByCategoryId(Guid)` | Returns subcategories for category |

#### `SubCategoriesService` (implements `ISubCategoriesService`)
| Method | Description |
| :--- | :--- |
| `GetAllSubCategories()` | Returns all subcategories |
| `GetSubCategoryById(Guid)` | Returns subcategory or null |

### Security

#### `PasswordValidator`
| Method | Description |
| :--- | :--- |
| `static bool MeetTheCriteria(string password)` | Checks password strength: minimum 8 chars, lowercase, uppercase, digit and special character |

#### `Jwt`
| Method | Description |
| :--- | :--- |
| `JwtToken GenerateJwtToken(string username)` | Returns generated JWT token |

#### `JwtToken`
| Field | Description |
| :--- | :--- |
| `Token` | JWT Token |

---

## 🚀 Compilation and Execution

Follow these steps to build and run the application:

1.  **Clone the repository:**
    ```bash
    git clone https://github.com/X0j3m/ContactList.git
    cd ContactList
    ```

2.  **Build Docker image**
    ```bash
    docker compose build --no-cache
    ```

3.  **Run the application:**
    ```bash
    docker compose up -d
    ```

Note: if port `8080` or `8081` is occupied change it in `docker-compose.yml` file

---

## 🌐 API Endpoints (host: http://localhost:8080)

Below are the available API endpoints. Replace {id} with a GUID where required.

### Login API
| HTTP | Endpoint | Description | Request Body | Possible Responses | Requires authentication token |
| :--- | :--- | :--- | :--- | :--- | :--- |
| GET | http://localhost:8080/api/Login | Get authentication bearer token | n/a | 200 OK (Authentication token with expiration time) / 400 BadRequest (Error generating JWT token) | No |

### Categories API
| HTTP | Endpoint | Description | Request Body | Possible Responses | Requires bearer token |
| :--- | :--- | :--- | :--- | :--- | :--- |
| GET | http://localhost:8080/api/Categories | Get all categories | n/a | 200 OK (list of CategoryDTO) | No |
| GET | http://localhost:8080/api/Categories/{id} | Get category by id | n/a | 200 OK (CategoryDTO) / 404 NotFound | No |
| GET | http://localhost:8080/api/Categories/{id}/subcategories | Get subcategories for category | n/a | 200 OK (list of SubCategoryDTO) / 404 NotFound | No |

### SubCategories API
| HTTP | Endpoint | Description | Request Body | Possible Responses | Requires bearer token |
| :--- | :--- | :--- | :--- | :--- | :--- |
| GET | http://localhost:8080/api/SubCategories | Get all subcategories | n/a | 200 OK (list of SubCategoryDTO) | No |
| GET | http://localhost:8080/api/SubCategories/{id} | Get subcategory by id | n/a | 200 OK (SubCategoryDTO) / 404 NotFound | No |

### Contacts API
| HTTP | Endpoint | Description | Request Body | Possible Responses | Requires bearer token |
| :--- | :--- | :--- | :--- | :--- | :--- |
| GET | http://localhost:8080/api/Contacts | Get all contacts | n/a | 200 OK (list of ContactDTO) | No |
| GET | http://localhost:8080/api/Contacts/{id} | Get contact by id | n/a | 200 OK (ContactDTO) / 404 NotFound | No |
| POST | http://localhost:8080/api/Contacts | Create a new contact | JSON (Example below) | 201 Created (body: created id) / 400 BadRequest / 401 Unauthorized| Yes |
| PUT | http://localhost:8080/api/Contacts/{id} | Update contact by id | JSON (Example below) | 204 NoContent / 400 BadRequest / 401 Unauthorized / 404 NotFound | Yes |
| DELETE | http://localhost:8080/api/Contacts/{id} | Delete contact by id | n/a | 204 NoContent / 401 Unauthorized / 404 NotFound | Yes |

---

# 📜 Requests body exapmles
Contacts POST request body example
``` bash
{ 
	"Name": "John",
	"Surname": "Doe",
	"Email": "john@example.com",
	"Password": "P@ssw0rd!123",
	"CategoryId": "<GUID>|null",
	"SubcategoryId": "<GUID>|null",
	"CustomSubCategory": "string|null",
	"Phone": "123456789",
	"BirthDate": "2000-01-01"
}
```

Contacts PUT request body example
``` bash
{ 
    "Id": "<GUID>"
	"Name": "John",
	"Surname": "Doe",
	"Email": "john@example.com",
	"Password": "P@ssw0rd!123",
	"CategoryId": "<GUID>|null",
	"SubcategoryId": "<GUID>|null",
	"CustomSubCategory": "string|null",
	"Phone": "123456789",
	"BirthDate": "2000-01-01"
}
```

## 📝 Author
* Michał Ptasznik
* Contact: michal.ptasznik@protonmail.com

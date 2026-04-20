# ContactList

*A brief description of the project (one or two sentences explaining the purpose of the application).* 

---

## 🛠 Libraries & Dependencies

The following technologies and libraries were used in this project:

* **.NET 10**
* **Microsoft.EntityFrameworkCore**
* **MySql.EntityFrameworkCore**
* **Microsoft.VisualStudio.Azure.Containers.Tools.**

---

## 🏗 Classes and Methods Overview

### Controllers

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

### DTOs

#### `ContactDTO`
| Field | Description |
| :--- | :--- |
| `Id` | Contact identifier |
| `Name` | First name |
| `Surname` | Last name |
| `Email` | Email address |
| `Password` | Password (validated by PasswordValidator) |
| `CategoryId?` | Category Id as string |
| `SubcategoryId?` | Subcategory Id as string |
| `CustomSubCategory?` | Custom subcategory text |
| `Phone` | Phone number |
| `BirthDate` | Birth date as ISO string (yyyy-MM-dd) |
| `ToEntity(ContactDTO)` | Static method: validates email and password, converts DTO to `Contact` (returns null on validation error) |

#### `CreateContactDTO`, `UpdateContactDTO`
| Field | Description |
| :--- | :--- |
| `Name`, `Surname`, `Email`, `Password`, `CategoryId?`, `SubcategoryId?`, `CustomSubCategory?`, `Phone`, `BirthDate` | Input fields used for create/update operations |

#### `CategoryDTO`
| Field | Description |
| :--- | :--- |
| `Id` | Category identifier |
| `Name` | Category name |

#### `SubCategoryDTO`
| Field | Description |
| :--- | :--- |
| `Id` | Subcategory identifier |
| `Name` | Subcategory name |
| `CategoryId` | Parent category Id |

### Interfaces

#### `IContactsRepository`
| Method | Description |
| :--- | :--- |
| `bool Exists(Guid id)` | Checks if contact with id exists |
| `bool Exists(string email)` | Checks if contact with email exists |
| `bool Exists(Guid id, string email)` | Checks if contact with id and email exists |
| `ICollection<Contact> GetAll()` | Returns all contacts |
| `Contact GetById(Guid id)` | Returns contact or throws if not found |
| `void Create(Contact contact)` | Adds contact to store |
| `void Update(Contact contact)` | Updates existing contact |
| `void Delete(Guid id)` | Deletes contact by id |

#### `ICategoriesRepository`
| Method | Description |
| :--- | :--- |
| `bool Exists(Guid id)` | Checks if category exists |
| `ICollection<Category> GetAll()` | Returns all categories |
| `Category GetById(Guid id)` | Returns category or throws if not found |

#### `ISubCategoriesRepository`
| Method | Description |
| :--- | :--- |
| `bool Exists(Guid id)` | Checks if subcategory exists |
| `ICollection<SubCategory> GetAll()` | Returns all subcategories |
| `SubCategory GetById(Guid id)` | Returns subcategory or throws if not found |
| `ICollection<SubCategory> GetByCategoryId(Guid categoryId)` | Returns subcategories for a category |

#### `IContactsService`
| Method | Description |
| :--- | :--- |
| `Guid Create(CreateContactDTO contactDto)` | Business logic for creating a contact, returns created Id or Guid.Empty on failure |
| `bool Delete(Guid id)` | Business logic for deletion, returns success |
| `ICollection<ContactDTO> GetAll()` | Returns all contacts as DTOs |
| `ContactDTO? GetById(Guid id)` | Returns a contact DTO or null if not found |
| `Guid Update(Guid id, UpdateContactDTO updateContactDto)` | Updates contact, returns id or Guid.Empty on failure |

#### `ICategoriesService`
| Method | Description |
| :--- | :--- |
| `ICollection<Category> GetAllCategories()` | Returns all categories |
| `Category? GetCategoryById(Guid id)` | Returns category or null if not found |
| `ICollection<SubCategory> GetSubCategoriesByCategoryId(Guid categoryId)` | Returns subcategories for category |

#### `ISubCategoriesService`
| Method | Description |
| :--- | :--- |
| `ICollection<SubCategory> GetAllSubCategories()` | Returns all subcategories |
| `SubCategory? GetSubCategoryById(Guid id)` | Returns subcategory or null if not found |

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

### DbContext

#### `ContactsDbContext` (EF Core)
| Item | Description |
| :--- | :--- |
| `DbSet<Contact> Contacts` | Contacts table |
| `DbSet<Category> Categories` | Categories table |
| `DbSet<SubCategory> SubCategories` | SubCategories table |
| `OnModelCreating(ModelBuilder)` | Configures fields, DateOnly converter, relationships, indices and seeds initial data |

### Migrations

The initial migration `InitialCreate` defines database schema for Contacts, Categories and SubCategories and seeds default categories and subcategories.

### Program / Configuration

`Program.cs` registers DbContext (MySQL), repository and service implementations in the DI container and maps controllers.

---

## API Endpoints (host: http://localhost:8080)

Below are the available API endpoints. Replace {id} with a GUID where required.

### Categories API
| HTTP | Endpoint | Description | Request Body | Possible Responses |
| :--- | :--- | :--- | :--- | :--- |
| GET | http://localhost:8080/api/Categories | Get all categories | n/a | 200 OK (list of CategoryDTO) |
| GET | http://localhost:8080/api/Categories/{id} | Get category by id | n/a | 200 OK (CategoryDTO) / 404 NotFound |
| GET | http://localhost:8080/api/Categories/{id}/subcategories | Get subcategories for category | n/a | 200 OK (list of SubCategoryDTO) / 404 NotFound |

### SubCategories API
| HTTP | Endpoint | Description | Request Body | Possible Responses |
| :--- | :--- | :--- | :--- | :--- |
| GET | http://localhost:8080/api/SubCategories | Get all subcategories | n/a | 200 OK (list of SubCategoryDTO) |
| GET | http://localhost:8080/api/SubCategories/{id} | Get subcategory by id | n/a | 200 OK (SubCategoryDTO) / 404 NotFound |

### Contacts API
| HTTP | Endpoint | Description | Request Body | Possible Responses |
| :--- | :--- | :--- | :--- | :--- |
| GET | http://localhost:8080/api/Contacts | Get all contacts | n/a | 200 OK (list of ContactDTO) |
| GET | http://localhost:8080/api/Contacts/{id} | Get contact by id | n/a | 200 OK (ContactDTO) / 404 NotFound |
| POST | http://localhost:8080/api/Contacts | Create a new contact | JSON: { "Name": "John", "Surname": "Doe", "Email": "john@example.com", "Password": "P@ssw0rd!", "CategoryId": "<guid>|null", "SubcategoryId": "<guid>|null", "CustomSubCategory": "string|null", "Phone": "123456789", "BirthDate": "YYYY-MM-DD" } | 201 Created (body: created id) / 400 BadRequest |
| PUT | http://localhost:8080/api/Contacts/{id} | Update contact by id | JSON same as POST (without Id) | 204 NoContent / 400 BadRequest / 404 NotFound |
| DELETE | http://localhost:8080/api/Contacts/{id} | Delete contact by id | n/a | 204 NoContent / 404 NotFound |

Examples:
- Create (curl):
  curl -X POST "http://localhost:8080/api/Contacts" -H "Content-Type: application/json" -d '{"Name":"John","Surname":"Doe","Email":"john@example.com","Password":"P@ssw0rd1","Phone":"123456789","BirthDate":"1990-01-01"}'

- Get all contacts (curl):
  curl "http://localhost:8080/api/Contacts"

---

If you want, I can also add sample request/response payloads for each endpoint.
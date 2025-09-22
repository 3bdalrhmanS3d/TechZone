# TechZone API Documentation

A comprehensive e-commerce API for technology products, specifically designed for laptop sales and management.

## Base URL
```
https://techzoneeg.runasp.net/
```

## Authentication

The API uses JWT Bearer token authentication. Include the token in the Authorization header:

```
Authorization: Bearer <your_jwt_token>
```

### Admin Credentials (for testing)
- **Email**: admin@techzone.com
- **Password**: Admin@123

## Response Format

All API responses follow a consistent structure:

### Success Response Template
```json
{
  "isSuccess": true,
  "message": "Operation completed successfully",
  "messageAr": "تمت العملية بنجاح",
  "data": {}, // Response data object or array
  "errors": null,
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### Error Response Template
```json
{
  "isSuccess": false,
  "message": "Error message in English",
  "messageAr": "رسالة الخطأ بالعربية",
  "data": null,
  "errors": ["Detailed error message 1", "Detailed error message 2"],
  "statusCode": 400,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

## Authentication Endpoints

### 1. Register User
**POST** `/api/Auth/register`

Creates a new user account.

**Request Body:**
```json
{
  "userName": "john_doe",
  "email": "john.doe@example.com",
  "fullName": "John Doe",
  "password": "SecurePassword123",
  "confirmPassword": "SecurePassword123"
}
```

**Example Response (Success - 200):**
```json
{
  "isSuccess": true,
  "message": "Registration successful. Please check your email for verification.",
  "messageAr": "تم التسجيل بنجاح. يرجى التحقق من بريدك الإلكتروني للتأكيد.",
  "data": true,
  "errors": null,
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

**Error Response (409 - Conflict):**
```json
{
  "isSuccess": false,
  "message": "User already exists",
  "messageAr": "المستخدم موجود بالفعل",
  "data": false,
  "errors": ["Email is already registered"],
  "statusCode": 409,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### 2. Login
**POST** `/api/Auth/login`

Authenticates user and returns JWT token.

**Request Body:**
```json
{
  "email": "john.doe@example.com",
  "password": "SecurePassword123"
}
```

**Example Response (Success - 200):**
```json
{
  "isSuccess": true,
  "message": "Login successful",
  "messageAr": "تم تسجيل الدخول بنجاح",
  "data": {
    "message": "Login successful",
    "isAuthenticated": true,
    "username": "john_doe",
    "email": "john.doe@example.com",
    "roles": ["User"],
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "emailConfirmed": true,
    "refreshTokenExpiration": "2024-02-15T10:30:00Z"
  },
  "errors": null,
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### 3. Confirm Email
**POST** `/api/Auth/confirm-email`

Confirms user email with verification code.

**Request Body:**
```json
{
  "email": "john.doe@example.com",
  "code": "123456"
}
```

### 4. Refresh Token
**POST** `/api/Auth/refresh-token`

Refreshes the JWT token using refresh token.

**Request Body:**
```json
{
  "refreshToken": "your_refresh_token_here"
}
```

### 5. Logout
**POST** `/api/Auth/logout`

Logs out the current user.

**Example Response:**
```json
{
  "isSuccess": true,
  "message": "Logout successful",
  "messageAr": "تم تسجيل الخروج بنجاح",
  "data": true,
  "errors": null,
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

## Laptop Management Endpoints

### 1. Get All Laptops (Paginated)
**GET** `/api/Laptop`

Retrieves paginated list of laptops with filtering and sorting options.

**Query Parameters:**
- `Page` (integer, min: 1, max: 2147483647): Page number
- `PageSize` (integer, min: 1, max: 100): Number of items per page
- `Search` (string, max: 100): Search term
- `SortBy` (enum): 0 = Name, 1 = Price, 2 = Rating
- `SortDirection` (enum): 0 = Ascending, 1 = Descending

**Example Request:**
```
GET /api/Laptop?Page=1&PageSize=10&Search=Dell&SortBy=1&SortDirection=0
```

**Example Response (Success - 200):**
```json
{
  "isSuccess": true,
  "message": "Laptops retrieved successfully",
  "messageAr": "تم استرداد أجهزة الكمبيوتر المحمولة بنجاح",
  "data": {
    "items": [
      {
        "id": 1,
        "name": "Dell XPS 13",
        "price": 1299.99,
        "category": "Ultrabook",
        "images": [
          "https://example.com/images/dell-xps-13-1.jpg",
          "https://example.com/images/dell-xps-13-2.jpg"
        ],
        "rate": 4.5,
        "reviewsCount": 128,
        "isDiscounted": true,
        "discountedPrice": 1199.99,
        "shortDescription": "Premium ultrabook with stunning display"
      }
    ],
    "page": 1,
    "pageSize": 10,
    "totalCount": 45,
    "totalPages": 5,
    "hasPrevious": false,
    "hasNext": true,
    "startIndex": 1,
    "endIndex": 10
  },
  "errors": null,
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### 2. Get Laptop by ID
**GET** `/api/Laptop/{id}`

Retrieves detailed information about a specific laptop.

**Path Parameters:**
- `id` (integer): Laptop ID

**Example Request:**
```
GET /api/Laptop/1
```

**Example Response (Success - 200):**
```json
{
  "isSuccess": true,
  "message": "Laptop found successfully",
  "messageAr": "تم العثور على الكمبيوتر المحمول بنجاح",
  "data": {
    "id": 1,
    "modelName": "Dell XPS 13",
    "processor": "Intel Core i7-1165G7",
    "gpu": "Intel Iris Xe Graphics",
    "screenSize": "13.3 inch",
    "hasCamera": true,
    "hasKeyboard": true,
    "hasTouchScreen": false,
    "ports": "2x USB-C, 1x microSD, 1x 3.5mm audio",
    "description": "The Dell XPS 13 is a premium ultrabook...",
    "notes": "Latest generation processor",
    "warranty": "2 years international warranty",
    "brandId": 1,
    "categoryId": 2,
    "brand": {
      "id": 1,
      "name": "Dell",
      "country": "USA",
      "logoUrl": "https://example.com/logos/dell.png"
    },
    "category": {
      "id": 2,
      "name": "Ultrabook",
      "description": "Thin and lightweight laptops"
    }
  },
  "errors": null,
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### 3. Create New Laptop (Admin Only)
**POST** `/api/Laptop`

Creates a new laptop entry.

**Request Body:**
```json
{
  "modelName": "MacBook Pro 14",
  "processor": "Apple M1 Pro",
  "gpu": "Apple M1 Pro GPU",
  "screenSize": "14.2 inch",
  "hasCamera": true,
  "hasKeyboard": true,
  "hasTouchScreen": false,
  "ports": "3x USB-C, 1x HDMI, 1x SD card, 1x 3.5mm audio"
}
```

**Example Response (Created - 201):**
```json
{
  "isSuccess": true,
  "message": "Laptop created successfully",
  "messageAr": "تم إنشاء الكمبيوتر المحمول بنجاح",
  "data": {
    "modelName": "MacBook Pro 14",
    "processor": "Apple M1 Pro",
    "gpu": "Apple M1 Pro GPU",
    "screenSize": "14.2 inch",
    "hasCamera": true,
    "hasKeyboard": true,
    "hasTouchScreen": false,
    "ports": "3x USB-C, 1x HDMI, 1x SD card, 1x 3.5mm audio"
  },
  "errors": null,
  "statusCode": 201,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### 4. Update Laptop (Admin Only)
**PUT** `/api/Laptop/{id}`

Updates an existing laptop.

**Path Parameters:**
- `id` (integer): Laptop ID

**Request Body:**
```json
{
  "modelName": "MacBook Pro 14 (Updated)",
  "processor": "Apple M2 Pro",
  "gpu": "Apple M2 Pro GPU",
  "screenSize": "14.2 inch",
  "hasCamera": true,
  "hasKeyboard": true,
  "hasTouchScreen": false,
  "ports": "3x USB-C, 1x HDMI, 1x SD card, 1x 3.5mm audio",
  "id": 1
}
```

### 5. Delete Laptop (Admin Only)
**DELETE** `/api/Laptop/{id}`

Deletes a laptop entry.

**Path Parameters:**
- `id` (integer): Laptop ID

**Example Response (Success - 200):**
```json
{
  "isSuccess": true,
  "message": "Laptop deleted successfully",
  "messageAr": "تم حذف الكمبيوتر المحمول بنجاح",
  "data": true,
  "errors": null,
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### 6. Search Laptops
**GET** `/api/Laptop/search`

Searches for laptops by term.

**Query Parameters:**
- `searchTerm` (string): Search term

**Example Request:**
```
GET /api/Laptop/search?searchTerm=gaming
```

### 7. Filter Laptops
**GET** `/api/Laptop/filter`

Filters laptops by specifications and price.

**Query Parameters:**
- `processor` (string): Processor filter
- `gpu` (string): GPU filter
- `minPrice` (integer): Minimum price
- `maxPrice` (integer): Maximum price

**Example Request:**
```
GET /api/Laptop/filter?processor=Intel&minPrice=500&maxPrice=2000
```

### 8. Get Recommended Laptops
**GET** `/api/Laptop/recommended`

Retrieves recommended laptops (same parameters as regular laptop listing).

## Category Endpoints

### Get Categories with Laptop Counts
**GET** `/api/Category/with-laptop-counts`

Retrieves all categories with the count of laptops in each category.

**Example Response (Success - 200):**
```json
[
  {
    "id": 1,
    "name": "Gaming",
    "laptopsCount": 25
  },
  {
    "id": 2,
    "name": "Ultrabook",
    "laptopsCount": 18
  },
  {
    "id": 3,
    "name": "Workstation",
    "laptopsCount": 12
  }
]
```

## Error Handling

The API uses standard HTTP status codes:

- **200**: Success
- **201**: Created
- **400**: Bad Request
- **401**: Unauthorized
- **403**: Forbidden
- **404**: Not Found
- **409**: Conflict
- **500**: Internal Server Error

### Common Error Scenarios

**Validation Error (400):**
```json
{
  "isSuccess": false,
  "message": "Validation failed",
  "messageAr": "فشل في التحقق من صحة البيانات",
  "data": null,
  "errors": [
    "Email is required",
    "Password must be at least 6 characters"
  ],
  "statusCode": 400,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

**Unauthorized (401):**
```json
{
  "isSuccess": false,
  "message": "Unauthorized access",
  "messageAr": "وصول غير مصرح به",
  "data": null,
  "errors": ["Invalid credentials"],
  "statusCode": 401,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

**Not Found (404):**
```json
{
  "isSuccess": false,
  "message": "Resource not found",
  "messageAr": "المورد غير موجود",
  "data": null,
  "errors": ["Laptop with ID 999 not found"],
  "statusCode": 404,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

## Data Models

### Laptop Response DTO
```json
{
  "id": "integer",
  "name": "string",
  "price": "number",
  "category": "string",
  "images": ["string array"],
  "rate": "number",
  "reviewsCount": "integer",
  "isDiscounted": "boolean",
  "discountedPrice": "number (nullable)",
  "shortDescription": "string"
}
```

### Pagination Response
```json
{
  "items": ["array of items"],
  "page": "integer",
  "pageSize": "integer",
  "totalCount": "integer",
  "totalPages": "integer",
  "hasPrevious": "boolean",
  "hasNext": "boolean",
  "startIndex": "integer",
  "endIndex": "integer"
}
```

## Integration Notes

### For Frontend Teams:
1. Always check the `isSuccess` field before processing data
2. Use the `data` field for successful responses
3. Display error messages from the `message` field
4. Handle pagination using the pagination metadata
5. Implement proper error handling for all HTTP status codes

### For Flutter Teams:
1. Create model classes matching the response DTOs
2. Implement proper JSON serialization/deserialization
3. Use the `messageAr` field for Arabic localization
4. Handle network errors and timeouts appropriately
5. Implement token refresh logic for expired tokens

### Best Practices:
1. Always include the Authorization header for protected endpoints
2. Validate user input before sending requests
3. Implement proper loading states during API calls
4. Cache frequently accessed data when appropriate
5. Handle offline scenarios gracefully

## Rate Limiting

The API may implement rate limiting. If you exceed the rate limit, you'll receive a 429 status code.

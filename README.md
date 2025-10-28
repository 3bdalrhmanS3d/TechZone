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

# TechZone - Complete API Documentation

## **Base Information**

- **Base URL**: `/api`
- **Authentication**: API Key (Header: `Authorization: Bearer {token}`)
- **Content Type**: `application/json`
- **Response Format**: Standardized with pagination

## **Rate Limits**

- Anonymous users: 100 requests/hour
- Authenticated users: 1000 requests/hour
- Admin users: 5000 requests/hour

**Response Headers:**

- `X-RateLimit-Limit`: 1000
- `X-RateLimit-Remaining`: 999
- `X-RateLimit-Reset`: 1642435200

---

## **Table of Contents**

1. [Brands](https://claude.ai/chat/ef961f72-b402-4b47-af7c-30e97518724c#1-brands-endpoints)
2. [Categories](https://claude.ai/chat/ef961f72-b402-4b47-af7c-30e97518724c#2-categories-endpoints)
3. [Laptops](https://claude.ai/chat/ef961f72-b402-4b47-af7c-30e97518724c#3-laptops-endpoints)
4. [Laptop Variants](https://claude.ai/chat/ef961f72-b402-4b47-af7c-30e97518724c#4-laptop-variants-endpoints)
5. [Accessories](https://claude.ai/chat/ef961f72-b402-4b47-af7c-30e97518724c#5-accessories-endpoints)
6. [Orders](https://claude.ai/chat/ef961f72-b402-4b47-af7c-30e97518724c#6-orders-endpoints)
7. [Cart](https://claude.ai/chat/ef961f72-b402-4b47-af7c-30e97518724c#7-cart-endpoints)
8. [Ratings & Reviews](https://claude.ai/chat/ef961f72-b402-4b47-af7c-30e97518724c#8-ratings--reviews-endpoints)
9. [Discounts](https://claude.ai/chat/ef961f72-b402-4b47-af7c-30e97518724c#9-discounts-endpoints)
10. [Payments](https://claude.ai/chat/ef961f72-b402-4b47-af7c-30e97518724c#10-payments-endpoints)
11. [Shipments](https://claude.ai/chat/ef961f72-b402-4b47-af7c-30e97518724c#11-shipments-endpoints)
12. [Repair Services](https://claude.ai/chat/ef961f72-b402-4b47-af7c-30e97518724c#12-repair-services-endpoints)
13. [Stock Alerts](https://claude.ai/chat/ef961f72-b402-4b47-af7c-30e97518724c#13-stock-alerts-endpoints)
14. [Product Views](https://claude.ai/chat/ef961f72-b402-4b47-af7c-30e97518724c#14-product-views-endpoints)
15. [Images](https://claude.ai/chat/ef961f72-b402-4b47-af7c-30e97518724c#15-images-endpoints)
16. [Analytics](https://claude.ai/chat/ef961f72-b402-4b47-af7c-30e97518724c#16-analytics-endpoints)
17. [Error Codes](https://claude.ai/chat/ef961f72-b402-4b47-af7c-30e97518724c#error-codes)

---

## **1. BRANDS Endpoints**

### **1.1 Get All Brands**

```http
GET /api/brands
```

**Query Parameters:**

- `page` (int, default: 1)
- `pageSize` (int, default: 10, max: 100)
- `search` (string, optional)
- `isActive` (boolean, optional)
- `country` (string, optional)

**Response:**

```json
{
  "isSuccess": true,
  "message": "Brands fetched successfully",
  "messageAr": "تم جلب الماركات بنجاح",
  "data": {
    "items": [
      {
        "id": 1,
        "name": "Lenovo",
        "country": "China",
        "logoUrl": "/images/brands/lenovo.png",
        "description": "Innovative computing solutions and technology",
        "isActive": true,
        "createdAt": "2024-01-01T00:00:00Z",
        "updatedAt": "2024-01-15T10:30:00Z"
      },
      {
        "id": 2,
        "name": "HP",
        "country": "USA",
        "logoUrl": "/images/brands/hp.png",
        "description": "HP laptops and computing devices",
        "isActive": true,
        "createdAt": "2024-01-01T00:00:00Z",
        "updatedAt": "2024-01-15T10:30:00Z"
      }
    ],
    "page": 1,
    "pageSize": 10,
    "totalCount": 7,
    "totalPages": 1,
    "hasPrevious": false,
    "hasNext": false,
    "startIndex": 1,
    "endIndex": 7
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **1.2 Get Brand Details**

```http
GET /api/brands/{id}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Brand details fetched successfully",
  "messageAr": "تم جلب تفاصيل الماركة بنجاح",
  "data": {
    "id": 1,
    "name": "Lenovo",
    "country": "China",
    "logoUrl": "/images/brands/lenovo.png",
    "description": "Innovative computing solutions and technology",
    "isActive": true,
    "createdAt": "2024-01-01T00:00:00Z",
    "updatedAt": "2024-01-15T10:30:00Z",
    "statistics": {
      "totalLaptops": 25,
      "activeLaptops": 22,
      "totalSales": 15000000.00,
      "averageRating": 4.3
    }
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **1.3 Create Brand**

```http
POST /api/brands
```

**Headers:**

- `Authorization: Bearer {token}`
- `Content-Type: application/json`

**Body:**

```json
{
  "name": "Samsung",
  "country": "South Korea",
  "logoUrl": "/images/brands/samsung.png",
  "description": "Samsung laptops and electronics",
  "isActive": true
}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Brand created successfully",
  "messageAr": "تم إنشاء الماركة بنجاح",
  "data": {
    "id": 8,
    "name": "Samsung",
    "country": "South Korea",
    "logoUrl": "/images/brands/samsung.png",
    "description": "Samsung laptops and electronics",
    "isActive": true,
    "createdAt": "2024-01-15T10:30:00Z",
    "updatedAt": "2024-01-15T10:30:00Z"
  },
  "errors": [],
  "statusCode": 201,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **1.4 Update Brand**

```http
PUT /api/brands/{id}
```

**Body:**

```json
{
  "name": "Samsung Electronics",
  "country": "South Korea",
  "logoUrl": "/images/brands/samsung-new.png",
  "description": "Updated description",
  "isActive": true
}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Brand updated successfully",
  "messageAr": "تم تحديث الماركة بنجاح",
  "data": {
    "id": 8,
    "name": "Samsung Electronics",
    "country": "South Korea",
    "logoUrl": "/images/brands/samsung-new.png",
    "description": "Updated description",
    "isActive": true,
    "createdAt": "2024-01-15T10:30:00Z",
    "updatedAt": "2024-01-15T10:35:00Z"
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:35:00Z"
}
```

### **1.5 Delete Brand**

```http
DELETE /api/brands/{id}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Brand deleted successfully",
  "messageAr": "تم حذف الماركة بنجاح",
  "data": {
    "id": 8,
    "deletedAt": "2024-01-15T10:40:00Z"
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:40:00Z"
}
```

---

## **2. CATEGORIES Endpoints**

### **2.1 Get All Categories**

```http
GET /api/categories
```

**Query Parameters:**

- `page` (int, default: 1)
- `pageSize` (int, default: 20)
- `includeInactive` (boolean, default: false)
- `parentCategoryId` (int, optional)
- `search` (string, optional)

**Response:**

```json
{
  "isSuccess": true,
  "message": "Categories fetched successfully",
  "messageAr": "تم جلب الفئات بنجاح",
  "data": {
    "items": [
      {
        "id": 1,
        "name": "Ultrabooks",
        "description": "Lightweight and powerful laptops",
        "imageUrl": "/images/categories/ultrabooks.jpg",
        "parentCategoryId": null,
        "displayOrder": 1,
        "isActive": true,
        "childCategories": [],
        "productCount": 45
      },
      {
        "id": 2,
        "name": "Gaming Laptops",
        "description": "High-performance gaming machines",
        "imageUrl": "/images/categories/gaming.jpg",
        "parentCategoryId": null,
        "displayOrder": 2,
        "isActive": true,
        "childCategories": [],
        "productCount": 32
      }
    ],
    "page": 1,
    "pageSize": 20,
    "totalCount": 5,
    "totalPages": 1,
    "hasPrevious": false,
    "hasNext": false,
    "startIndex": 1,
    "endIndex": 5
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **2.2 Get Category Details**

```http
GET /api/categories/{id}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Category details fetched successfully",
  "messageAr": "تم جلب تفاصيل الفئة بنجاح",
  "data": {
    "id": 1,
    "name": "Ultrabooks",
    "description": "Lightweight and powerful laptops",
    "imageUrl": "/images/categories/ultrabooks.jpg",
    "parentCategoryId": null,
    "displayOrder": 1,
    "isActive": true,
    "createdAt": "2024-01-01T00:00:00Z",
    "updatedAt": "2024-01-15T10:30:00Z",
    "parentCategory": null,
    "childCategories": [
      {
        "id": 10,
        "name": "Business Ultrabooks",
        "productCount": 15
      }
    ],
    "statistics": {
      "totalProducts": 45,
      "averagePrice": 1299.99,
      "totalSales": 5000000.00
    }
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **2.3 Create Category**

```http
POST /api/categories
```

**Body:**

```json
{
  "name": "2-in-1 Laptops",
  "description": "Convertible laptops and tablets",
  "imageUrl": "/images/categories/2in1.jpg",
  "parentCategoryId": null,
  "displayOrder": 3,
  "isActive": true
}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Category created successfully",
  "messageAr": "تم إنشاء الفئة بنجاح",
  "data": {
    "id": 6,
    "name": "2-in-1 Laptops",
    "description": "Convertible laptops and tablets",
    "imageUrl": "/images/categories/2in1.jpg",
    "parentCategoryId": null,
    "displayOrder": 3,
    "isActive": true,
    "createdAt": "2024-01-15T10:30:00Z",
    "updatedAt": "2024-01-15T10:30:00Z"
  },
  "errors": [],
  "statusCode": 201,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **2.4 Update Category**

```http
PUT /api/categories/{id}
```

**Body:**

```json
{
  "name": "2-in-1 Convertible Laptops",
  "description": "Versatile convertible laptops and tablets",
  "imageUrl": "/images/categories/2in1-updated.jpg",
  "parentCategoryId": null,
  "displayOrder": 3,
  "isActive": true
}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Category updated successfully",
  "messageAr": "تم تحديث الفئة بنجاح",
  "data": {
    "id": 6,
    "name": "2-in-1 Convertible Laptops",
    "description": "Versatile convertible laptops and tablets",
    "imageUrl": "/images/categories/2in1-updated.jpg",
    "parentCategoryId": null,
    "displayOrder": 3,
    "isActive": true,
    "createdAt": "2024-01-15T10:30:00Z",
    "updatedAt": "2024-01-15T10:35:00Z"
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:35:00Z"
}
```

### **2.5 Delete Category**

```http
DELETE /api/categories/{id}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Category deleted successfully",
  "messageAr": "تم حذف الفئة بنجاح",
  "data": {
    "id": 6,
    "deletedAt": "2024-01-15T10:40:00Z"
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:40:00Z"
}
```

---

## **3. LAPTOPS Endpoints**

### **3.1 Get All Laptops**

```http
GET /api/laptops
```

**Query Parameters:**

- `page` (int, default: 1)
- `pageSize` (int, default: 20)
- `search` (string, optional)
- `brandId` (int, optional)
- `categoryId` (int, optional)
- `isActive` (boolean, optional)
- `releaseYear` (int, optional)
- `hasCamera` (boolean, optional)
- `hasTouchScreen` (boolean, optional)

**Response:**

```json
{
  "isSuccess": true,
  "message": "Laptops fetched successfully",
  "messageAr": "تم جلب أجهزة اللابتوب بنجاح",
  "data": {
    "items": [
      {
        "id": 1,
        "modelName": "Dell XPS 13",
        "brand": {
          "id": 2,
          "name": "Dell",
          "logoUrl": "/images/brands/dell.png"
        },
        "category": {
          "id": 1,
          "name": "Ultrabooks"
        },
        "processor": "Intel Core i7-1165G7",
        "gpu": "Intel Iris Xe Graphics",
        "screenSize": "13.4 inch",
        "hasCamera": true,
        "hasKeyboard": true,
        "hasTouchScreen": false,
        "releaseYear": 2023,
        "isActive": true,
        "variantCount": 3,
        "priceRange": {
          "min": 999.99,
          "max": 1499.99
        },
        "averageRating": 4.5,
        "mainImage": "/images/laptops/dell-xps13-main.jpg"
      }
    ],
    "page": 1,
    "pageSize": 20,
    "totalCount": 50,
    "totalPages": 3,
    "hasPrevious": false,
    "hasNext": true,
    "startIndex": 1,
    "endIndex": 20
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **3.2 Get Laptop Details**

```http
GET /api/laptops/{id}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Laptop details fetched successfully",
  "messageAr": "تم جلب تفاصيل اللابتوب بنجاح",
  "data": {
    "id": 1,
    "modelName": "Dell XPS 13",
    "brand": {
      "id": 2,
      "name": "Dell",
      "country": "USA",
      "logoUrl": "/images/brands/dell.png"
    },
    "category": {
      "id": 1,
      "name": "Ultrabooks",
      "description": "Lightweight and powerful laptops"
    },
    "processor": "Intel Core i7-1165G7",
    "gpu": "Intel Iris Xe Graphics",
    "screenSize": "13.4 inch",
    "hasCamera": true,
    "hasKeyboard": true,
    "hasTouchScreen": false,
    "description": "Premium ultrabook with stunning display and performance",
    "releaseYear": 2023,
    "storeLocation": "Cairo Mall",
    "storeContact": "+20123456789",
    "isActive": true,
    "createdAt": "2024-01-01T00:00:00Z",
    "updatedAt": "2024-01-15T10:30:00Z",
    "ports": [
      {
        "id": 1,
        "type": "USB-C",
        "quantity": 2
      },
      {
        "id": 2,
        "type": "USB-A",
        "quantity": 1
      },
      {
        "id": 3,
        "type": "HDMI",
        "quantity": 1
      }
    ],
    "warranty": {
      "id": 1,
      "durationMonths": 24,
      "type": "International Warranty",
      "coverage": "Hardware components and technical support",
      "provider": "Dell"
    },
    "images": [
      {
        "id": 1,
        "url": "/images/laptops/dell-xps13-1.jpg",
        "isMain": true,
        "displayOrder": 1
      },
      {
        "id": 2,
        "url": "/images/laptops/dell-xps13-2.jpg",
        "isMain": false,
        "displayOrder": 2
      }
    ],
    "variants": [
      {
        "id": 1,
        "sku": "DLXPS13-8-256-SSD",
        "ram": 8,
        "storage": 256,
        "storageType": "SSD",
        "currentPrice": 999.99,
        "stockStatus": "InStock"
      },
      {
        "id": 2,
        "sku": "DLXPS13-16-512-SSD",
        "ram": 16,
        "storage": 512,
        "storageType": "SSD",
        "currentPrice": 1299.99,
        "stockStatus": "InStock"
      }
    ],
    "statistics": {
      "averageRating": 4.5,
      "totalReviews": 124,
      "totalSales": 450,
      "viewCount": 5420
    }
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **3.3 Create Laptop**

```http
POST /api/laptops
```

**Body:**

```json
{
  "modelName": "Lenovo ThinkPad X1",
  "brandId": 1,
  "categoryId": 1,
  "processor": "Intel Core i7-1260P",
  "gpu": "Intel Iris Xe Graphics",
  "screenSize": "14 inch",
  "hasCamera": true,
  "hasKeyboard": true,
  "hasTouchScreen": false,
  "description": "Business laptop with excellent keyboard",
  "storeLocation": "City Center Mall",
  "storeContact": "+20123456789",
  "releaseYear": 2024,
  "isActive": true
}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Laptop created successfully",
  "messageAr": "تم إنشاء اللابتوب بنجاح",
  "data": {
    "id": 51,
    "modelName": "Lenovo ThinkPad X1",
    "brand": {
      "id": 1,
      "name": "Lenovo"
    },
    "category": {
      "id": 1,
      "name": "Ultrabooks"
    },
    "processor": "Intel Core i7-1260P",
    "gpu": "Intel Iris Xe Graphics",
    "screenSize": "14 inch",
    "hasCamera": true,
    "hasKeyboard": true,
    "hasTouchScreen": false,
    "description": "Business laptop with excellent keyboard",
    "releaseYear": 2024,
    "storeLocation": "City Center Mall",
    "storeContact": "+20123456789",
    "isActive": true,
    "createdAt": "2024-01-15T10:30:00Z",
    "updatedAt": "2024-01-15T10:30:00Z"
  },
  "errors": [],
  "statusCode": 201,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **3.4 Update Laptop**

```http
PUT /api/laptops/{id}
```

**Body:**

```json
{
  "modelName": "Lenovo ThinkPad X1 Carbon",
  "brandId": 1,
  "categoryId": 1,
  "processor": "Intel Core i7-1260P",
  "gpu": "Intel Iris Xe Graphics",
  "screenSize": "14 inch",
  "hasCamera": true,
  "hasKeyboard": true,
  "hasTouchScreen": true,
  "description": "Updated business laptop with touchscreen",
  "storeLocation": "City Center Mall",
  "storeContact": "+20123456789",
  "releaseYear": 2024,
  "isActive": true
}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Laptop updated successfully",
  "messageAr": "تم تحديث اللابتوب بنجاح",
  "data": {
    "id": 51,
    "modelName": "Lenovo ThinkPad X1 Carbon",
    "brand": {
      "id": 1,
      "name": "Lenovo"
    },
    "category": {
      "id": 1,
      "name": "Ultrabooks"
    },
    "processor": "Intel Core i7-1260P",
    "gpu": "Intel Iris Xe Graphics",
    "screenSize": "14 inch",
    "hasCamera": true,
    "hasKeyboard": true,
    "hasTouchScreen": true,
    "description": "Updated business laptop with touchscreen",
    "releaseYear": 2024,
    "storeLocation": "City Center Mall",
    "storeContact": "+20123456789",
    "isActive": true,
    "createdAt": "2024-01-15T10:30:00Z",
    "updatedAt": "2024-01-15T10:35:00Z"
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:35:00Z"
}
```

### **3.5 Delete Laptop**

```http
DELETE /api/laptops/{id}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Laptop deleted successfully",
  "messageAr": "تم حذف اللابتوب بنجاح",
  "data": {
    "id": 51,
    "deletedAt": "2024-01-15T10:40:00Z"
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:40:00Z"
}
```

---

## **4. LAPTOP VARIANTS Endpoints**

### **4.1 Get Recommended Laptop Variants**

```http
GET /api/laptop-variants/recommended
```

**Query Parameters:**

- `page` (int, default: 1)
- `pageSize` (int, default: 20)
- `search` (string, optional)
- `sortBy` (string, optional) - "price", "name", "rating", "createdAt"
- `sortDirection` (string, optional) - "asc", "desc"
- `categoryId` (int, optional)
- `brandId` (int, optional)

**Response:**

```json
{
  "isSuccess": true,
  "message": "Recommended laptop variants fetched successfully",
  "messageAr": "تم جلب أنواع اللابتوب الموصى بها بنجاح",
  "data": {
    "items": [
      {
        "id": 1,
        "sku": "DLXPS13-16-512-SSD",
        "ram": 16,
        "storage": 512,
        "storageType": "SSD",
        "currentPrice": 1299.99,
        "originalPrice": 1499.99,
        "discountPercentage": 13,
        "discountAmount": 200.00,
        "stockQuantity": 15,
        "reservedQuantity": 2,
        "availableQuantity": 13,
        "stockStatus": "InStock",
        "reorderLevel": 5,
        "laptop": {
          "id": 1,
          "modelName": "Dell XPS 13",
          "processor": "Intel Core i7-1165G7",
          "gpu": "Intel Iris Xe Graphics",
          "screenSize": "13.4 inch",
          "hasCamera": true,
          "hasTouchScreen": false,
          "storeLocation": "Cairo Mall"
        },
        "brand": {
          "id": 2,
          "name": "Dell",
          "logoUrl": "/images/brands/dell.png"
        },
        "category": {
          "id": 1,
          "name": "Ultrabooks"
        },
        "images": [
          "/images/laptops/dell-xps13-1.jpg",
          "/images/laptops/dell-xps13-2.jpg"
        ],
        "averageRating": 4.5,
        "reviewCount": 124,
        "isActive": true
      }
    ],
    "page": 1,
    "pageSize": 20,
    "totalCount": 150,
    "totalPages": 8,
    "hasPrevious": false,
    "hasNext": true,
    "startIndex": 1,
    "endIndex": 20
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **4.2 Advanced Laptop Variants Filter**

```http
GET /api/laptop-variants/filter
```

**Query Parameters:**

- `brandIds` (int[], optional) - 1,2,3
- `categoryIds` (int[], optional) - 1,2,3
- `minPrice` (decimal, optional)
- `maxPrice` (decimal, optional)
- `processor` (string, optional)
- `ram` (int[], optional) - 8,16,32
- `storage` (int[], optional) - 256,512,1024
- `storageType` (string, optional) - SSD,HDD
- `screenSize` (string[], optional) - 13,15,17
- `hasCamera` (boolean, optional)
- `hasTouchScreen` (boolean, optional)
- `inStock` (boolean, optional)
- `hasDiscount` (boolean, optional)
- `minRating` (decimal, optional)
- `releaseYear` (int, optional)
- `page`, `pageSize`, `sortBy`, `sortDirection`

**Response:** Same structure as 4.1

### **4.3 Get Laptop Variant Details**

```http
GET /api/laptop-variants/{id}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Laptop variant details fetched successfully",
  "messageAr": "تم جلب تفاصيل نوع اللابتوب بنجاح",
  "data": {
    "id": 1,
    "sku": "DLXPS13-16-512-SSD",
    "ram": 16,
    "storage": 512,
    "storageType": "SSD",
    "currentPrice": 1299.99,
    "originalPrice": 1499.99,
    "discountPercentage": 13,
    "discountAmount": 200.00,
    "stockQuantity": 15,
    "reservedQuantity": 2,
    "availableQuantity": 13,
    "stockStatus": "InStock",
    "reorderLevel": 5,
    "isActive": true,
    "createdAt": "2024-01-01T00:00:00Z",
    "updatedAt": "2024-01-15T10:30:00Z",
    "laptop": {
      "id": 1,
      "modelName": "Dell XPS 13",
      "processor": "Intel Core i7-1165G7",
      "gpu": "Intel Iris Xe Graphics",
      "screenSize": "13.4 inch",
      "hasCamera": true,
      "hasKeyboard": true,
      "hasTouchScreen": false,
      "description": "Premium ultrabook with stunning display and performance",
      "releaseYear": 2023,
      "storeLocation": "Cairo Mall",
      "storeContact": "+20123456789"
    },
    "brand": {
      "id": 2,
      "name": "Dell",
      "country": "USA",
      "logoUrl": "/images/brands/dell.png"
    },
    "category": {
      "id": 1,
      "name": "Ultrabooks",
      "description": "Lightweight and powerful laptops"
    },
    "ports": [
      {
        "id": 1,
        "type": "USB-C",
        "quantity": 2
      },
      {
        "id": 2,
        "type": "USB-A",
        "quantity": 1
      }
    ],
    "warranty": {
      "id": 1,
      "durationMonths": 24,
      "type": "International Warranty",
      "coverage": "Hardware components and technical support",
      "provider": "Dell"
    },
    "images": [
      {
        "id": 1,
        "url": "/images
```
### **4.4 Get Variants by Laptop ID**

```http
GET /api/laptops/{laptopId}/variants
```

**Query Parameters:**

- `page` (int, default: 1)
- `pageSize` (int, default: 10)
- `inStockOnly` (boolean, default: false)

**Response:**

```json
{
  "isSuccess": true,
  "message": "Laptop variants fetched successfully",
  "messageAr": "تم جلب أنواع اللابتوب بنجاح",
  "data": {
    "laptop": {
      "id": 1,
      "modelName": "Dell XPS 13",
      "processor": "Intel Core i7-1165G7",
      "gpu": "Intel Iris Xe Graphics",
      "screenSize": "13.4 inch",
      "hasCamera": true,
      "hasTouchScreen": false
    },
    "variants": {
      "items": [
        {
          "id": 1,
          "sku": "DLXPS13-8-256-SSD",
          "ram": 8,
          "storage": 256,
          "storageType": "SSD",
          "currentPrice": 999.99,
          "originalPrice": 1199.99,
          "discountPercentage": 17,
          "stockQuantity": 15,
          "reservedQuantity": 2,
          "availableQuantity": 13,
          "stockStatus": "InStock",
          "isActive": true
        },
        {
          "id": 2,
          "sku": "DLXPS13-16-512-SSD",
          "ram": 16,
          "storage": 512,
          "storageType": "SSD",
          "currentPrice": 1299.99,
          "originalPrice": 1499.99,
          "discountPercentage": 13,
          "stockQuantity": 10,
          "reservedQuantity": 1,
          "availableQuantity": 9,
          "stockStatus": "InStock",
          "isActive": true
        }
      ],
      "page": 1,
      "pageSize": 10,
      "totalCount": 2,
      "totalPages": 1,
      "hasPrevious": false,
      "hasNext": false,
      "startIndex": 1,
      "endIndex": 2
    }
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **4.5 Create Laptop Variant**

```http
POST /api/laptop-variants
```

**Body:**

```json
{
  "laptopId": 51,
  "sku": "LEN-TPX1-16-512-SSD",
  "ram": 16,
  "storage": 512,
  "storageType": "SSD",
  "currentPrice": 1499.99,
  "stockQuantity": 20,
  "reorderLevel": 5,
  "isActive": true
}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Laptop variant created successfully",
  "messageAr": "تم إنشاء نوع اللابتوب بنجاح",
  "data": {
    "id": 151,
    "sku": "LEN-TPX1-16-512-SSD",
    "laptopId": 51,
    "ram": 16,
    "storage": 512,
    "storageType": "SSD",
    "currentPrice": 1499.99,
    "stockQuantity": 20,
    "reservedQuantity": 0,
    "availableQuantity": 20,
    "reorderLevel": 5,
    "isActive": true,
    "createdAt": "2024-01-15T10:30:00Z",
    "updatedAt": "2024-01-15T10:30:00Z"
  },
  "errors": [],
  "statusCode": 201,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **4.6 Update Laptop Variant**

```http
PUT /api/laptop-variants/{id}
```

**Body:**

```json
{
  "sku": "LEN-TPX1-16-512-SSD-V2",
  "ram": 16,
  "storage": 512,
  "storageType": "NVMe SSD",
  "currentPrice": 1399.99,
  "stockQuantity": 25,
  "reorderLevel": 5,
  "isActive": true
}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Laptop variant updated successfully",
  "messageAr": "تم تحديث نوع اللابتوب بنجاح",
  "data": {
    "id": 151,
    "sku": "LEN-TPX1-16-512-SSD-V2",
    "laptopId": 51,
    "ram": 16,
    "storage": 512,
    "storageType": "NVMe SSD",
    "currentPrice": 1399.99,
    "oldPrice": 1499.99,
    "stockQuantity": 25,
    "reservedQuantity": 0,
    "availableQuantity": 25,
    "reorderLevel": 5,
    "isActive": true,
    "createdAt": "2024-01-15T10:30:00Z",
    "updatedAt": "2024-01-15T10:35:00Z"
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:35:00Z"
}
```

### **4.7 Delete Laptop Variant**

```http
DELETE /api/laptop-variants/{id}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Laptop variant deleted successfully",
  "messageAr": "تم حذف نوع اللابتوب بنجاح",
  "data": {
    "id": 151,
    "deletedAt": "2024-01-15T10:40:00Z"
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:40:00Z"
}
```

### **4.8 Update Stock Quantity**

```http
PATCH /api/laptop-variants/{id}/stock
```

**Body:**

```json
{
  "quantity": 25,
  "operation": "add",
  "reason": "Restocked from supplier"
}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Stock updated successfully",
  "messageAr": "تم تحديث المخزون بنجاح",
  "data": {
    "id": 151,
    "previousStock": 20,
    "newStock": 45,
    "reservedQuantity": 0,
    "availableQuantity": 45,
    "stockStatus": "InStock",
    "updatedAt": "2024-01-15T10:35:00Z"
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:35:00Z"
}
```

### **4.9 Get Laptop Variant Price History**

```http
GET /api/laptop-variants/{id}/price-history
```

**Query Parameters:**

- `days` (int, optional) - Last X days
- `page` (int, default: 1)
- `pageSize` (int, default: 10)

**Response:**

```json
{
  "isSuccess": true,
  "message": "Price history fetched successfully",
  "messageAr": "تم جلب سجل الأسعار بنجاح",
  "data": {
    "variant": {
      "id": 1,
      "sku": "DLXPS13-16-512-SSD",
      "currentPrice": 1299.99
    },
    "priceHistory": {
      "items": [
        {
          "id": 1,
          "oldPrice": 1499.99,
          "newPrice": 1299.99,
          "changeReason": "Seasonal Discount",
          "changePercentage": -13.34,
          "effectiveFrom": "2024-01-01T00:00:00Z",
          "effectiveTo": null,
          "changedBy": {
            "userId": "admin123",
            "userName": "Admin User"
          },
          "isCurrentPrice": true
        },
        {
          "id": 2,
          "oldPrice": 1399.99,
          "newPrice": 1499.99,
          "changeReason": "Price Adjustment",
          "changePercentage": 7.14,
          "effectiveFrom": "2023-12-01T00:00:00Z",
          "effectiveTo": "2023-12-31T23:59:59Z",
          "changedBy": {
            "userId": "admin123",
            "userName": "Admin User"
          },
          "isCurrentPrice": false
        }
      ],
      "page": 1,
      "pageSize": 10,
      "totalCount": 2,
      "totalPages": 1,
      "hasPrevious": false,
      "hasNext": false,
      "startIndex": 1,
      "endIndex": 2
    },
    "statistics": {
      "lowestPrice": 1299.99,
      "highestPrice": 1499.99,
      "averagePrice": 1399.99,
      "totalChanges": 2
    }
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **4.10 Bulk Update Stock**

```http
POST /api/laptop-variants/bulk-stock-update
```

**Body:**

```json
{
  "updates": [
    {
      "variantId": 1,
      "quantity": 30,
      "operation": "set"
    },
    {
      "variantId": 2,
      "quantity": 10,
      "operation": "add"
    },
    {
      "variantId": 3,
      "quantity": 5,
      "operation": "subtract"
    }
  ],
  "reason": "Monthly inventory adjustment"
}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Bulk stock update completed successfully",
  "messageAr": "تم تحديث المخزون بالجملة بنجاح",
  "data": {
    "successCount": 3,
    "failedCount": 0,
    "results": [
      {
        "variantId": 1,
        "success": true,
        "previousStock": 15,
        "newStock": 30,
        "message": "Stock updated successfully"
      },
      {
        "variantId": 2,
        "success": true,
        "previousStock": 20,
        "newStock": 30,
        "message": "Stock updated successfully"
      },
      {
        "variantId": 3,
        "success": true,
        "previousStock": 10,
        "newStock": 5,
        "message": "Stock updated successfully"
      }
    ]
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

---

## **5. ACCESSORIES Endpoints**

### **5.1 Get Recommended Accessories**

```http
GET /api/accessories/recommended
```

**Query Parameters:**

- `page` (int, default: 1)
- `pageSize` (int, default: 20)
- `search` (string, optional)
- `accessoryTypeId` (int, optional)
- `compatibleWithLaptopId` (int, optional)
- `minPrice` (decimal, optional)
- `maxPrice` (decimal, optional)
- `inStock` (boolean, optional)
- `sortBy` (string, optional) - "price", "name", "rating"
- `sortDirection` (string, optional) - "asc", "desc"

**Response:**

```json
{
  "isSuccess": true,
  "message": "Recommended accessories fetched successfully",
  "messageAr": "تم جلب الملحقات الموصى بها بنجاح",
  "data": {
    "items": [
      {
        "id": 1,
        "sku": "ACC-DCK-001",
        "name": "Dell USB-C Dock",
        "accessoryType": {
          "id": 1,
          "name": "Docking Station"
        },
        "description": "Multi-port docking station with power delivery",
        "currentPrice": 199.99,
        "originalPrice": 249.99,
        "discountPercentage": 20,
        "discountAmount": 50.00,
        "stockQuantity": 25,
        "reservedQuantity": 3,
        "availableQuantity": 22,
        "stockStatus": "InStock",
        "images": ["/images/accessories/dell-dock-1.jpg"],
        "mainImage": "/images/accessories/dell-dock-1.jpg",
        "averageRating": 4.2,
        "reviewCount": 89,
        "isActive": true
      }
    ],
    "page": 1,
    "pageSize": 20,
    "totalCount": 85,
    "totalPages": 5,
    "hasPrevious": false,
    "hasNext": true,
    "startIndex": 1,
    "endIndex": 20
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **5.2 Get Accessory Details**

```http
GET /api/accessories/{id}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Accessory details fetched successfully",
  "messageAr": "تم جلب تفاصيل الملحق بنجاح",
  "data": {
    "id": 1,
    "sku": "ACC-DCK-001",
    "name": "Dell USB-C Dock",
    "accessoryType": {
      "id": 1,
      "name": "Docking Station",
      "description": "Docking stations and port replicators"
    },
    "description": "Multi-port docking station with 90W power delivery",
    "currentPrice": 199.99,
    "originalPrice": 249.99,
    "discountPercentage": 20,
    "discountAmount": 50.00,
    "stockQuantity": 25,
    "reservedQuantity": 3,
    "availableQuantity": 22,
    "stockStatus": "InStock",
    "reorderLevel": 5,
    "isActive": true,
    "createdAt": "2024-01-01T00:00:00Z",
    "updatedAt": "2024-01-15T10:30:00Z",
    "attributes": [
      {
        "id": 1,
        "key": "Color",
        "value": "Black"
      },
      {
        "id": 2,
        "key": "Material",
        "value": "Plastic"
      },
      {
        "id": 3,
        "key": "Ports",
        "value": "2x USB-C, 4x USB-A, HDMI, Ethernet"
      },
      {
        "id": 4,
        "key": "Power Delivery",
        "value": "90W"
      }
    ],
    "images": [
      {
        "id": 1,
        "url": "/images/accessories/dell-dock-1.jpg",
        "isMain": true,
        "displayOrder": 1
      },
      {
        "id": 2,
        "url": "/images/accessories/dell-dock-2.jpg",
        "isMain": false,
        "displayOrder": 2
      }
    ],
    "compatibleLaptops": [
      {
        "id": 1,
        "modelName": "Dell XPS 13",
        "brand": "Dell",
        "image": "/images/laptops/dell-xps13-main.jpg"
      },
      {
        "id": 5,
        "modelName": "Dell XPS 15",
        "brand": "Dell",
        "image": "/images/laptops/dell-xps15-main.jpg"
      }
    ],
    "activeDiscounts": [
      {
        "id": 8,
        "code": "ACCESSORIES20",
        "title": "Accessories Sale",
        "discountType": "Percentage",
        "value": 20.00
      }
    ],
    "ratings": {
      "average": 4.2,
      "count": 89,
      "distribution": {
        "5": 45,
        "4": 30,
        "3": 10,
        "2": 3,
        "1": 1
      }
    }
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **5.3 Get Accessory Types**

```http
GET /api/accessory-types
```

**Query Parameters:**

- `page` (int, default: 1)
- `pageSize` (int, default: 20)

**Response:**

```json
{
  "isSuccess": true,
  "message": "Accessory types fetched successfully",
  "messageAr": "تم جلب أنواع الملحقات بنجاح",
  "data": {
    "items": [
      {
        "id": 1,
        "name": "Docking Station",
        "description": "Docking stations and port replicators",
        "productCount": 15
      },
      {
        "id": 2,
        "name": "Laptop Bag",
        "description": "Bags and cases for laptops",
        "productCount": 32
      },
      {
        "id": 3,
        "name": "Mouse",
        "description": "Computer mice and pointing devices",
        "productCount": 48
      },
      {
        "id": 4,
        "name": "Keyboard",
        "description": "External keyboards",
        "productCount": 28
      }
    ],
    "page": 1,
    "pageSize": 20,
    "totalCount": 10,
    "totalPages": 1,
    "hasPrevious": false,
    "hasNext": false,
    "startIndex": 1,
    "endIndex": 10
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **5.4 Create Accessory**

```http
POST /api/accessories
```

**Body:**

```json
{
  "sku": "ACC-BAG-001",
  "name": "Lenovo Laptop Bag",
  "accessoryTypeId": 2,
  "description": "Water-resistant laptop bag with padding for 15.6 inch laptops",
  "currentPrice": 49.99,
  "stockQuantity": 50,
  "reorderLevel": 10,
  "isActive": true,
  "attributes": [
    {
      "key": "Color",
      "value": "Black"
    },
    {
      "key": "Material",
      "value": "Nylon"
    },
    {
      "key": "Size",
      "value": "15.6 inch"
    }
  ],
  "compatibleLaptopIds": [1, 2, 3]
}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Accessory created successfully",
  "messageAr": "تم إنشاء الملحق بنجاح",
  "data": {
    "id": 86,
    "sku": "ACC-BAG-001",
    "name": "Lenovo Laptop Bag",
    "accessoryType": {
      "id": 2,
      "name": "Laptop Bag"
    },
    "description": "Water-resistant laptop bag with padding for 15.6 inch laptops",
    "currentPrice": 49.99,
    "stockQuantity": 50,
    "reservedQuantity": 0,
    "availableQuantity": 50,
    "reorderLevel": 10,
    "isActive": true,
    "createdAt": "2024-01-15T10:30:00Z",
    "updatedAt": "2024-01-15T10:30:00Z"
  },
  "errors": [],
  "statusCode": 201,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **5.5 Update Accessory**

```http
PUT /api/accessories/{id}
```

**Body:**

```json
{
  "sku": "ACC-BAG-001-V2",
  "name": "Lenovo Premium Laptop Bag",
  "accessoryTypeId": 2,
  "description": "Premium water-resistant laptop bag with extra padding",
  "currentPrice": 59.99,
  "stockQuantity": 50,
  "reorderLevel": 10,
  "isActive": true,
  "attributes": [
    {
      "key": "Color",
      "value": "Black"
    },
    {
      "key": "Material",
      "value": "Premium Nylon"
    },
    {
      "key": "Size",
      "value": "15.6 inch"
    }
  ],
  "compatibleLaptopIds": [1, 2, 3, 4]
}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Accessory updated successfully",
  "messageAr": "تم تحديث الملحق بنجاح",
  "data": {
    "id": 86,
    "sku": "ACC-BAG-001-V2",
    "name": "Lenovo Premium Laptop Bag",
    "accessoryType": {
      "id": 2,
      "name": "Laptop Bag"
    },
    "description": "Premium water-resistant laptop bag with extra padding",
    "currentPrice": 59.99,
    "oldPrice": 49.99,
    "stockQuantity": 50,
    "reservedQuantity": 0,
    "availableQuantity": 50,
    "reorderLevel": 10,
    "isActive": true,
    "createdAt": "2024-01-15T10:30:00Z",
    "updatedAt": "2024-01-15T10:35:00Z"
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:35:00Z"
}
```

### **5.6 Delete Accessory**

```http
DELETE /api/accessories/{id}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Accessory deleted successfully",
  "messageAr": "تم حذف الملحق بنجاح",
  "data": {
    "id": 86,
    "deletedAt": "2024-01-15T10:40:00Z"
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:40:00Z"
}
```

### **5.7 Get Accessory Price History**

```http
GET /api/accessories/{id}/price-history
```

**Query Parameters:**

- `days` (int, optional)
- `page` (int, default: 1)
- `pageSize` (int, default: 10)

**Response:**

```json
{
  "isSuccess": true,
  "message": "Price history fetched successfully",
  "messageAr": "تم جلب سجل الأسعار بنجاح",
  "data": {
    "accessory": {
      "id": 1,
      "sku": "ACC-DCK-001",
      "name": "Dell USB-C Dock",
      "currentPrice": 199.99
    },
    "priceHistory": {
      "items": [
        {
          "id": 45,
          "oldPrice": 249.99,
          "newPrice": 199.99,
          "changeReason": "Promotional Discount",
          "changePercentage": -20.00,
          "effectiveFrom": "2024-01-10T00:00:00Z",
          "effectiveTo": null,
          "changedBy": {
            "userId": "admin123",
            "userName": "Admin User"
          },
          "isCurrentPrice": true
        }
      ],
      "page": 1,
      "pageSize": 10,
      "totalCount": 1,
      "totalPages": 1,
      "hasPrevious": false,
      "hasNext": false,
      "startIndex": 1,
      "endIndex": 1
    },
    "statistics": {
      "lowestPrice": 199.99,
      "highestPrice": 249.99,
      "averagePrice": 224.99,
      "totalChanges": 1
    }
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

---

## **6. ORDERS Endpoints**

### **6.1 Get User Orders**

```http
GET /api/orders
```

**Query Parameters:**

- `page` (int, default: 1)
- `pageSize` (int, default: 10)
- `status` (string, optional) - Pending, Reserved, Processing, Shipped, Delivered, Cancelled
- `orderType` (string, optional) - Reservation, Delivery
- `startDate` (datetime, optional)
- `endDate` (datetime, optional)
- `minAmount` (decimal, optional)
- `maxAmount` (decimal, optional)

**Response:**

```json
{
  "isSuccess": true,
  "message": "Orders fetched successfully",
  "messageAr": "تم جلب الطلبات بنجاح",
  "data": {
    "items": [
      {
        "id": 1001,
        "orderNumber": "TZ-2024-1001",
        "orderType": "Reservation",
        "status": "Reserved",
        "totalAmount": 1499.99,
        "reservationAmount": 299.99,
        "remainingAmount": 1200.00,
        "orderDate": "2024-01-15T10:30:00Z",
        "reservationExpiryDate": "2024-02-14T10:30:00Z",
        "daysUntilExpiry": 30,
        "itemCount": 1,
        "items": [
          {
            "productType": "LaptopVariant",
            "productName": "Dell XPS 13 - 16GB RAM, 512GB SSD",
            "quantity": 1,
            "unitPrice": 1499.99
          }
        ]
      },
      {
        "id": 1002,
        "orderNumber": "TZ-2024-1002",
        "orderType": "Delivery",
        "status": "Shipped",
        "totalAmount": 249.99,
        "orderDate": "2024-01-14T09:15:00Z",
        "deliveryAddress": "123 Main St, Cairo, Egypt",
        "estimatedDelivery": "2024-01-18T00:00:00Z",
        "itemCount": 2,
        "items": [
          {
            "productType": "Accessory",
            "productName": "Dell USB-C Dock",
            "quantity": 1,
            "unitPrice": 199.99
          },
          {
            "productType": "Accessory",
            "productName": "Laptop Bag",
            "quantity": 1,
            "unitPrice": 50.00
          }
        ]
      }
    ],
    "page": 1,
    "pageSize": 10,
    "totalCount": 2,
    "totalPages": 1,
    "hasPrevious": false,
    "hasNext": false,
    "startIndex": 1,
    "endIndex": 2
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **6.2 Get Order Details**

```http
GET /api/orders/{id}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Order details fetched successfully",
  "messageAr": "تم جلب تفاصيل الطلب بنجاح",
  "data": {
    "id": 1001,
    "orderNumber": "TZ-2024-1001",
    "orderType": "Reservation",
    "status": "Reserved",
    "user": {
      "id": "user123",
      "firstName": "Ahmed",
      "lastName": "Hassan",
      "email": "ahmed@example.com",
      "phoneNumber": "+201234567890"/laptops/dell-xps13-1.jpg",
        "isMain": true,
        "displayOrder": 1
      },
      {
        "id": 2,
        "url": "/images/laptops/dell-xps13-2.jpg",
        "isMain": false,
        "displayOrder": 2
      }
    ],
    "variants": [
      {
        "id": 1,
        "sku": "DLXPS13-8-256-SSD",
        "ram": 8,
        "storage": 256,
        "storageType": "SSD",
        "currentPrice": 999.99,
        "stockStatus": "InStock"
      },
      {
        "id": 2,
        "sku": "DLXPS13-16-512-SSD",
        "ram": 16,
        "storage": 512,
        "storageType": "SSD",
        "currentPrice": 1299.99,
        "stockStatus": "InStock"
      }
    ],
    "statistics": {
      "averageRating": 4.5,
      "totalReviews": 124,
      "totalSales": 450,
      "viewCount": 5420
    }
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **6.3 Create Order (Reservation/Delivery)**

```http
POST /api/orders
```

**Body:**

```json
{
  "orderType": "Reservation",
  "items": [
    {
      "productType": "LaptopVariant",
      "productId": 151,
      "quantity": 1
    },
    {
      "productType": "Accessory",
      "productId": 25,
      "quantity": 2
    }
  ],
  "discountCode": "WINTER2024",
  "deliveryAddress": "123 Main St, Cairo, Egypt",
  "deliveryInstructions": "Call before delivery"
}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Order created successfully. Please proceed to payment.",
  "messageAr": "تم إنشاء الطلب بنجاح. يرجى المتابعة للدفع.",
  "data": {
    "id": 1001,
    "orderNumber": "TZ-2024-1001",
    "orderType": "Reservation",
    "status": "Pending",
    "items": [
      {
        "productType": "LaptopVariant",
        "productId": 151,
        "productName": "Lenovo ThinkPad X1 - 16GB RAM, 512GB SSD",
        "quantity": 1,
        "unitPrice": 1499.99,
        "discountAmount": 0,
        "totalPrice": 1499.99
      },
      {
        "productType": "Accessory",
        "productId": 25,
        "productName": "Laptop Bag Premium",
        "quantity": 2,
        "unitPrice": 49.99,
        "discountAmount": 10.00,
        "totalPrice": 89.98
      }
    ],
    "subtotalAmount": 1589.97,
    "discountAmount": 10.00,
    "shippingCost": 0,
    "taxAmount": 0,
    "totalAmount": 1579.97,
    "reservationAmount": 315.99,
    "reservationExpiryDate": "2024-02-14T10:30:00Z",
    "orderDate": "2024-01-15T10:30:00Z",
    "paymentUrl": "https://accept.paymob.com/api/acceptance/iframes/123456?payment_token=xyz789"
  },
  "errors": [],
  "statusCode": 201,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **6.4 Update Order Status**

```http
PATCH /api/orders/{id}/status
```

**Headers:**

- `Authorization: Bearer {admin_token}`

**Body:**

```json
{
  "status": "Reserved",
  "notes": "Customer confirmed reservation and payment completed"
}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Order status updated successfully",
  "messageAr": "تم تحديث حالة الطلب بنجاح",
  "data": {
    "id": 1001,
    "orderNumber": "TZ-2024-1001",
    "previousStatus": "Pending",
    "currentStatus": "Reserved",
    "updatedAt": "2024-01-15T10:35:00Z",
    "updatedBy": "admin123"
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:35:00Z"
}
```

### **6.5 Cancel Order**

```http
POST /api/orders/{id}/cancel
```

**Body:**

```json
{
  "reason": "Customer changed mind",
  "refundAmount": 299.99
}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Order cancelled successfully",
  "messageAr": "تم إلغاء الطلب بنجاح",
  "data": {
    "id": 1001,
    "orderNumber": "TZ-2024-1001",
    "status": "Cancelled",
    "cancelReason": "Customer changed mind",
    "refundAmount": 299.99,
    "refundStatus": "Pending",
    "cancelledAt": "2024-01-15T10:40:00Z"
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:40:00Z"
}
```

### **6.6 Complete Reservation (Pick Up)**

```http
POST /api/orders/{id}/complete-reservation
```

**Headers:**

- `Authorization: Bearer {admin_token}`

**Body:**

```json
{
  "remainingPaymentAmount": 1200.00,
  "notes": "Customer picked up laptop from store"
}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Reservation completed successfully",
  "messageAr": "تم إتمام الحجز بنجاح",
  "data": {
    "id": 1001,
    "orderNumber": "TZ-2024-1001",
    "status": "Delivered",
    "isReservationCompleted": true,
    "completedAt": "2024-01-20T14:30:00Z",
    "totalPaid": 1499.99
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-20T14:30:00Z"
}
```

---

## **7. CART Endpoints**

### **7.1 Get Cart**

```http
GET /api/cart
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Cart fetched successfully",
  "messageAr": "تم جلب السلة بنجاح",
  "data": {
    "items": [
      {
        "id": 89,
        "productType": "LaptopVariant",
        "productId": 151,
        "productName": "Lenovo ThinkPad X1 - 16GB RAM, 512GB SSD",
        "sku": "LEN-TPX1-16-512-SSD",
        "quantity": 1,
        "unitPrice": 1499.99,
        "discountAmount": 0,
        "totalPrice": 1499.99,
        "stockAvailable": 20,
        "image": "/images/laptops/lenovo-thinkpad-x1.jpg",
        "addedAt": "2024-01-15T10:00:00Z"
      },
      {
        "id": 90,
        "productType": "Accessory",
        "productId": 25,
        "productName": "Laptop Bag Premium",
        "sku": "ACC-BAG-001",
        "quantity": 1,
        "unitPrice": 49.99,
        "discountAmount": 5.00,
        "totalPrice": 44.99,
        "stockAvailable": 50,
        "image": "/images/accessories/laptop-bag.jpg",
        "addedAt": "2024-01-15T10:15:00Z"
      }
    ],
    "totalItems": 2,
    "subtotal": 1549.98,
    "discount": 5.00,
    "tax": 0,
    "shipping": 0,
    "total": 1544.98,
    "appliedDiscountCode": null
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **7.2 Add to Cart**

```http
POST /api/cart/items
```

**Body:**

```json
{
  "productType": "LaptopVariant",
  "productId": 151,
  "quantity": 1
}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Item added to cart successfully",
  "messageAr": "تم إضافة العنصر إلى السلة بنجاح",
  "data": {
    "id": 89,
    "productType": "LaptopVariant",
    "productId": 151,
    "productName": "Lenovo ThinkPad X1 - 16GB RAM, 512GB SSD",
    "quantity": 1,
    "unitPrice": 1499.99,
    "totalPrice": 1499.99,
    "addedAt": "2024-01-15T10:30:00Z",
    "cartSummary": {
      "totalItems": 1,
      "total": 1499.99
    }
  },
  "errors": [],
  "statusCode": 201,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **7.3 Update Cart Item Quantity**

```http
PUT /api/cart/items/{itemId}
```

**Body:**

```json
{
  "quantity": 2
}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Cart item updated successfully",
  "messageAr": "تم تحديث عنصر السلة بنجاح",
  "data": {
    "id": 89,
    "productType": "LaptopVariant",
    "productId": 151,
    "quantity": 2,
    "unitPrice": 1499.99,
    "totalPrice": 2999.98,
    "updatedAt": "2024-01-15T10:35:00Z"
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:35:00Z"
}
```

### **7.4 Remove from Cart**

```http
DELETE /api/cart/items/{itemId}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Item removed from cart successfully",
  "messageAr": "تم إزالة العنصر من السلة بنجاح",
  "data": {
    "removedItemId": 89,
    "cartSummary": {
      "totalItems": 0,
      "total": 0
    }
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:40:00Z"
}
```

### **7.5 Clear Cart**

```http
DELETE /api/cart
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Cart cleared successfully",
  "messageAr": "تم مسح السلة بنجاح",
  "data": {
    "itemsRemoved": 2,
    "clearedAt": "2024-01-15T10:45:00Z"
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:45:00Z"
}
```

### **7.6 Apply Discount Code**

```http
POST /api/cart/apply-discount
```

**Body:**

```json
{
  "code": "WINTER2024"
}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Discount code applied successfully",
  "messageAr": "تم تطبيق رمز الخصم بنجاح",
  "data": {
    "code": "WINTER2024",
    "discountType": "Percentage",
    "value": 13.00,
    "discountAmount": 195.00,
    "cartSummary": {
      "subtotal": 1499.99,
      "discount": 195.00,
      "total": 1304.99
    }
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:50:00Z"
}
```

### **7.7 Remove Discount Code**

```http
DELETE /api/cart/discount
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Discount code removed successfully",
  "messageAr": "تم إزالة رمز الخصم بنجاح",
  "data": {
    "cartSummary": {
      "subtotal": 1499.99,
      "discount": 0,
      "total": 1499.99
    }
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:55:00Z"
}
```

---

## **8. RATINGS & REVIEWS Endpoints**

### **8.1 Get Product Ratings**

```http
GET /api/ratings
```

**Query Parameters:**

- `productType` (enum: LaptopVariant, Accessory, required)
- `productId` (int, required)
- `page` (int, default: 1)
- `pageSize` (int, default: 10)
- `stars` (int, optional) - Filter by rating (1-5)
- `verifiedOnly` (boolean, default: false)
- `sortBy` (string, optional) - "recent", "helpful", "rating"

**Response:**

```json
{
  "isSuccess": true,
  "message": "Ratings fetched successfully",
  "messageAr": "تم جلب التقييمات بنجاح",
  "data": {
    "product": {
      "type": "LaptopVariant",
      "id": 1,
      "name": "Dell XPS 13 - 16GB RAM, 512GB SSD"
    },
    "summary": {
      "averageRating": 4.5,
      "totalReviews": 124,
      "distribution": {
        "5": 74,
        "4": 30,
        "3": 15,
        "2": 3,
        "1": 2
      },
      "verifiedPurchases": 98
    },
    "items": [
      {
        "id": 1,
        "user": {
          "id": "user123",
          "firstName": "Ahmed",
          "lastName": "H.",
          "profileImage": "/images/users/default.jpg"
        },
        "stars": 5,
        "comment": "Excellent laptop! Fast and lightweight. Perfect for work and travel.",
        "isVerifiedPurchase": true,
        "createdAt": "2024-01-10T15:30:00Z",
        "updatedAt": "2024-01-10T15:30:00Z",
        "helpfulCount": 12,
        "reportCount": 0
      },
      {
        "id": 2,
        "user": {
          "id": "user456",
          "firstName": "Fatima",
          "lastName": "M.",
          "profileImage": "/images/users/default.jpg"
        },
        "stars": 4,
        "comment": "Great performance, but battery could be better.",
        "isVerifiedPurchase": true,
        "createdAt": "2024-01-08T09:15:00Z",
        "updatedAt": "2024-01-08T09:15:00Z",
        "helpfulCount": 8,
        "reportCount": 0
      }
    ],
    "page": 1,
    "pageSize": 10,
    "totalCount": 124,
    "totalPages": 13,
    "hasPrevious": false,
    "hasNext": true,
    "startIndex": 1,
    "endIndex": 10
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **8.2 Get User's Rating for Product**

```http
GET /api/ratings/my-rating
```

**Query Parameters:**

- `productType` (enum: LaptopVariant, Accessory, required)
- `productId` (int, required)

**Response:**

```json
{
  "isSuccess": true,
  "message": "User rating fetched successfully",
  "messageAr": "تم جلب تقييم المستخدم بنجاح",
  "data": {
    "id": 1,
    "productType": "LaptopVariant",
    "productId": 1,
    "stars": 5,
    "comment": "Excellent laptop! Fast and lightweight.",
    "isVerifiedPurchase": true,
    "createdAt": "2024-01-10T15:30:00Z",
    "updatedAt": "2024-01-10T15:30:00Z"
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **8.3 Create Rating**

```http
POST /api/ratings
```

**Body:**

```json
{
  "productType": "LaptopVariant",
  "productId": 1,
  "stars": 5,
  "comment": "Excellent laptop! Fast and lightweight. Perfect for work and travel."
}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Rating created successfully",
  "messageAr": "تم إنشاء التقييم بنجاح",
  "data": {
    "id": 1,
    "productType": "LaptopVariant",
    "productId": 1,
    "stars": 5,
    "comment": "Excellent laptop! Fast and lightweight. Perfect for work and travel.",
    "isVerifiedPurchase": true,
    "createdAt": "2024-01-15T10:30:00Z",
    "updatedAt": "2024-01-15T10:30:00Z"
  },
  "errors": [],
  "statusCode": 201,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **8.4 Update Rating**

```http
PUT /api/ratings/{id}
```

**Body:**

```json
{
  "stars": 4,
  "comment": "Updated: Great performance, but battery could be better."
}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Rating updated successfully",
  "messageAr": "تم تحديث التقييم بنجاح",
  "data": {
    "id": 1,
    "stars": 4,
    "comment": "Updated: Great performance, but battery could be better.",
    "updatedAt": "2024-01-15T10:35:00Z"
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:35:00Z"
}
```

### **8.5 Delete Rating**

```http
DELETE /api/ratings/{id}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Rating deleted successfully",
  "messageAr": "تم حذف التقييم بنجاح",
  "data": {
    "id": 1,
    "deletedAt": "2024-01-15T10:40:00Z"
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:40:00Z"
}
```

### **8.6 Mark Rating as Helpful**

```http
POST /api/ratings/{id}/helpful
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Rating marked as helpful",
  "messageAr": "تم تمييز التقييم كمفيد",
  "data": {
    "ratingId": 1,
    "helpfulCount": 13
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:45:00Z"
}
```

### **8.7 Report Rating**

```http
POST /api/ratings/{id}/report
```

**Body:**

```json
{
  "reason": "Inappropriate content"
}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Rating reported successfully",
  "messageAr": "تم الإبلاغ عن التقييم بنجاح",
  "data": {
    "ratingId": 1,
    "reportedAt": "2024-01-15T10:50:00Z"
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:50:00Z"
}
```

---

## **9. DISCOUNTS Endpoints**

### **9.1 Get All Discounts**

```http
GET /api/discounts
```

**Query Parameters:**

- `page` (int, default: 1)
- `pageSize` (int, default: 20)
- `isActive` (boolean, optional)
- `discountType` (enum: Percentage, FixedAmount, optional)
- `search` (string, optional)

**Response:**

```json
{
  "isSuccess": true,
  "message": "Discounts fetched successfully",
  "messageAr": "تم جلب الخصومات بنجاح",
  "data": {
    "items": [
      {
        "id": 5,
        "code": "WINTER2024",
        "title": "Winter Sale 2024",
        "description": "Get 13% off on all laptops",
        "discountType": "Percentage",
        "value": 13.00,
        "minimumPurchase": 1000.00,
        "maxDiscountAmount": 500.00,
        "usageLimit": 1000,
        "usageCount": 245,
        "remainingUses": 755,
        "startDate": "2024-01-01T00:00:00Z",
        "endDate": "2024-02-28T23:59:59Z",
        "isActive": true,
        "isGlobal": true,
        "applicableProducts": []
      },
      {
        "id": 8,
        "code": "ACCESSORIES20",
        "title": "Accessories Sale",
        "description": "20% off on selected accessories",
        "discountType": "Percentage",
        "value": 20.00,
        "minimumPurchase": 50.00,
        "maxDiscountAmount": 100.00,
        "usageLimit": 500,
        "usageCount": 89,
        "remainingUses": 411,
        "startDate": "2024-01-10T00:00:00Z",
        "endDate": "2024-01-31T23:59:59Z",
        "isActive": true,
        "isGlobal": false,
        "applicableProducts": [
          {
            "productType": "Accessory",
            "productId": 1,
            "productName": "Dell USB-C Dock"
          }
        ]
      }
    ],
    "page": 1,
    "pageSize": 20,
    "totalCount": 12,
    "totalPages": 1,
    "hasPrevious": false,
    "hasNext": false,
    "startIndex": 1,
    "endIndex": 12
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **9.2 Get Discount Details**

```http
GET /api/discounts/{id}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Discount details fetched successfully",
  "messageAr": "تم جلب تفاصيل الخصم بنجاح",
  "data": {
    "id": 5,
    "code": "WINTER2024",
    "title": "Winter Sale 2024",
    "description": "Get 13% off on all laptops during winter season",
    "discountType": "Percentage",
    "value": 13.00,
    "minimumPurchase": 1000.00,
    "maxDiscountAmount": 500.00,
    "usageLimit": 1000,
    "usageCount": 245,
    "remainingUses": 755,
    "startDate": "2024-01-01T00:00:00Z",
    "endDate": "2024-02-28T23:59:59Z",
    "isActive": true,
    "isGlobal": true,
    "createdAt": "2023-12-15T10:00:00Z",
    "updatedAt": "2024-01-15T10:30:00Z",
    "applicableProducts": [],
    "statistics": {
      "totalRevenue": 487500.00,
      "averageOrderValue": 1989.80,
      "totalOrders": 245
    }
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **9.3 Validate Discount Code**

```http
POST /api/discounts/validate
```

**Body:**

```json
{
  "code": "WINTER2024",
  "cartTotal": 1499.99,
  "items": [
    {
      "productType": "LaptopVariant",
      "productId": 1,
      "quantity": 1,
      "price": 1499.99
    }
  ]
}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Discount code is valid",
  "messageAr": "رمز الخصم صالح",
  "data": {
    "isValid": true,
    "discount": {
      "id": 5,
      "code": "WINTER2024",
      "discountType": "Percentage",
      "value": 13.00
    },
    "discountAmount": 195.00,
    "finalTotal": 1304.99,
    "eligibleItems": [
      {
        "productType": "LaptopVariant",
        "productId": 1,
        "discountApplied": 195.00
      }
    ]
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **9.4 Create Discount**

```http
POST /api/discounts
```

**Headers:**

- `Authorization: Bearer {admin_token}`

**Body:**

```json
{
  "code": "SPRING2024",
  "title": "Spring Sale 2024",
  "description": "Celebrate spring with 15% off",
  "discountType": "Percentage",
  "value": 15.00,
  "minimumPurchase": 500.00,
  "maxDiscountAmount": 300.00,
  "usageLimit": 500,
  "startDate": "2024-03-01T00:00:00Z",
  "endDate": "2024-03-31T23:59:59Z",
  "isActive": true,
  "applicableProducts": [
    {
      "productType": "LaptopVariant",
      "productId": 1
    },
    {
      "productType": "LaptopVariant",
      "productId": 2
    }
  ]
}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Discount created successfully",
  "messageAr": "تم إنشاء الخصم بنجاح",
  "data": {
    "id": 15,
    "code": "SPRING2024",
    "title": "Spring Sale 2024",
    "description": "Celebrate spring with 15% off",
    "discountType": "Percentage",
    "value": 15.00,
    "minimumPurchase": 500.00,
    "maxDiscountAmount": 300.00,
    "usageLimit": 500,
    "usageCount": 0,
    "startDate": "2024-03-01T00:00:00Z",
    "endDate": "2024-03-31T23:59:59Z",
    "isActive": true,
    "isGlobal": false,
    "createdAt": "2024-01-15T10:30:00Z",
    "updatedAt": "2024-01-15T10:30:00Z"
  },
  "errors": [],
  "statusCode": 201,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **9.5 Update Discount**

```http
PUT /api/discounts/{id}
```

**Headers:**

- `Authorization: Bearer {admin_token}`

**Body:**

```json
{
  "code": "SPRING2024",
  "title": "Spring Mega Sale 2024",
  "description": "Updated: Celebrate spring with 20% off",
  "discountType": "Percentage",
  "value": 20.00,
  "minimumPurchase": 500.00,
  "maxDiscountAmount": 400.00,
  "usageLimit": 1000,
  "startDate": "2024-03-01T00:00:00Z",
  "endDate": "2024-03-31T23:59:59Z",
  "isActive": true
}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Discount updated successfully",
  "messageAr": "تم تحديث الخصم بنجاح",
  "data": {
    "id": 15,
    "code": "SPRING2024",
    "title": "Spring Mega Sale 2024",
    "value": 20.00,
    "updatedAt": "2024-01-15T10:35:00Z"
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:35:00Z"
}
```

### **9.6 Delete Discount**

```http
DELETE /api/discounts/{id}
```

**Headers:**

- `Authorization: Bearer {admin_token}`

**Response:**

```json
{
  "isSuccess": true,
  "message": "Discount deleted successfully",
  "messageAr": "تم حذف الخصم بنجاح",
  "data": {
    "id": 15,
    "deletedAt": "2024-01-15T10:40:00Z"
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:40:00Z"
}
```

### **9.7 Get User Discount Usage History**

```http
GET /api/discounts/my-usage
```

**Query Parameters:**

- `page` (int, default: 1)
- `pageSize` (int, default: 10)

**Response:**

```json
{
  "isSuccess": true,
  "message": "Discount usage history fetched successfully",
  "messageAr": "تم جلب سجل استخدام الخصومات بنجاح",
  "data": {
    "items": [
      {
        "id": 1,
        "discount": {
          "id": 5,
          "code": "WINTER2024",
          "title": "Winter Sale 2024",
          "value": 13.00
        },
        "order": {
          "id": 1001,
          "orderNumber": "TZ-2024-1001",
          "totalAmount": 1304.99,
          "discountAmount": 195.00
        },
        "usedAt": "2024-01-15T10:30:00Z"
      }
    ],
    "page": 1,
    "pageSize": 10,
    "totalCount": 1,
    "totalPages": 1,
    "hasPrevious": false,
    "hasNext": false,
    "startIndex": 1,
    "endIndex": 1
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

---

## **10. PAYMENTS Endpoints**

### **10.1 Get Payment Details**

```http
GET /api/payments/{id}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Payment details fetched successfully",
  "messageAr": "تم جلب تفاصيل الدفع بنجاح",
  "data": {
    "id": 1,
    "order": {
      "id": 1001,
      "orderNumber": "TZ-2024-1001"
    },
    "paymobOrderId": "PMB123456789",
    "integrationId": 123456,
    "transactionId": "TXN987654321",
    "amountCents": 29999,
    "currency": "EGP",
    "paymentMethod": "Card",
    "paymentStatus": "COMPLETED",
    "errorCode": null,
    "errorMessage": null,
    "refundedAmountCents": 0,
    "capturedAmountCents": 29999,
    "is3DS": true,
    "isCaptured": true,
    "isRefunded": false,
    "billingData": {
      "firstName": "Ahmed",
      "lastName": "Hassan",
      "email": "ahmed@example.com",
      "phoneNumber": "+201234567890"
    },
    "paymobCreatedAt": "2024-01-15T10:30:00Z",
    "createdAt": "2024-01-15T10:30:00Z",
    "updatedAt": "2024-01-15T10:31:00Z"
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **10.2 Get Order Payments**

```http
GET /api/orders/{orderId}/payments
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Order payments fetched successfully",
  "messageAr": "تم جلب مدفوعات الطلب بنجاح",
  "data": {
    "order": {
      "id": 1001,
      "orderNumber": "TZ-2024-1001",
      "totalAmount": 1499.99
    },
    "payments": [
      {
        "id": 1,
        "amountCents": 29999,
        "paymentStatus": "COMPLETED",
        "paymentMethod": "Card",
        "createdAt": "2024-01-15T10:30:00Z"
      }
    ],
    "totalPaid": 299.99,
    "remainingAmount": 1200.00
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **10.3 Initiate Payment**

```http
POST /api/payments/initiate
```

**Body:**

```json
{
  "orderId": 1001,
  "paymentMethod": "Card",
  "returnUrl": "https://techzone.com/payment/callback"
}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Payment initiated successfully",
  "messageAr": "تم بدء عملية الدفع بنجاح",
  "data": {
    "paymentId": 1,
    "paymobOrderId": "PMB123456789",
    "paymentToken": "xyz789abc",
    "iframeUrl": "https://accept.paymob.com/api/acceptance/iframes/123456?payment_token=xyz789abc",
    "amountCents": 29999,
    "currency": "EGP",
    "expiresAt": "2024-01-15T11:30:00Z"
  },
  "errors": [],
  "statusCode": 201,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **10.4 Payment Callback (Paymob Webhook)**

```http
POST /api/payments/callback
```

**Body:** (Sent by Paymob)

```json
{
  "obj": {
    "id": 123456789,
    "order": {
      "id": "PMB123456789"
    },
    "amount_cents": 29999,
    "success": true,
    "is_3d_secure": true,
    "integration_id": 123456,
    "currency": "EGP",
    "pending": false,
    "is_auth": false,
    "is_capture": true,
    "is_refunded": false,
    "is_standalone_payment": true,
    "is_voided": false,
    "refunded_amount_cents": 0,
    "captured_amount": 29999,
    "merchant_order_id": "TZ-2024-1001",
    "created_at": "2024-01-15T10:30:00Z"
  }
}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Payment callback processed successfully",
  "messageAr": "تم معالجة استجابة الدفع بنجاح",
  "data": {
    "processed": true,
    "orderId": 1001,
    "orderStatus": "Reserved",
    "paymentStatus": "COMPLETED"
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:31:00Z"
}
```

### **10.5 Refund Payment**

```http
POST /api/payments/{id}/refund
```

**Headers:**

- `Authorization: Bearer {admin_token}`

**Body:**

```json
{
  "amountCents": 29999,
  "reason": "Customer requested cancellation"
}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Refund processed successfully",
  "messageAr": "تمت معالجة الاسترجاع بنجاح",
  "data": {
    "paymentId": 1,
    "refundedAmountCents": 29999,
    "refundStatus": "REFUNDED",
    "refundedAt": "2024-01-15T10:35:00Z"
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:35:00Z"
}
```

---

## **11. SHIPMENTS Endpoints**

### **11.1 Get Order Shipment**

```http
GET /api/orders/{orderId}/shipment
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Shipment details fetched successfully",
  "messageAr": "تم جلب تفاصيل الشحن بنجاح",
  "data": {
    "id": 1,
    "order": {
      "id": 1002,
      "orderNumber": "TZ-2024-1002",
      "orderType": "Delivery"
    },
    "carrier": "Aramex",
    "trackingNumber": "ARX123456789",
    "status": "Shipped",
    "shippedAt": "2024-01-16T09:00:00Z",
    "estimatedDelivery": "2024-01-18T23:59:59Z",
    "deliveredAt": null,
    "failedReason": null,
    "trackingHistory": [
      {
        "status": "Preparing",
        "location": "Cairo Warehouse",
        "timestamp": "2024-01-15T10:30:00Z",
        "description": "Package is being prepared"
      },
      {
        "status": "Shipped",
        "location": "Cairo Distribution Center",
        "timestamp": "2024-01-16T09:00:00Z",
        "description": "Package has been picked up by carrier"
      },
      {
        "status": "In Transit",
        "location": "Giza Hub",
        "timestamp": "2024-01-16T14:30:00Z",
        "description": "Package is in transit"
      }
    ],
    "createdAt": "2024-01-15T10:30:00Z",
    "updatedAt": "2024-01-16T14:30:00Z"
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-16T15:00:00Z"
}
```

### **11.2 Create Shipment**

```http
POST /api/shipments
```

**Headers:**

- `Authorization: Bearer {admin_token}`

**Body:**

```json
{
  "orderId": 1002,
  "carrier": "Aramex",
  "trackingNumber": "ARX123456789",
  "estimatedDelivery": "2024-01-18T23:59:59Z"
}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Shipment created successfully",
  "messageAr": "تم إنشاء الشحنة بنجاح",
  "data": {
    "id": 1,
    "orderId": 1002,
    "carrier": "Aramex",
    "trackingNumber": "ARX123456789",
    "status": "Preparing",
    "estimatedDelivery": "2024-01-18T23:59:59Z",
    "createdAt": "2024-01-15T10:30:00Z"
  },
  "errors": [],
  "statusCode": 201,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **11.3 Update Shipment Status**

```http
PATCH /api/shipments/{id}/status
```

**Headers:**

- `Authorization: Bearer {admin_token}`

**Body:**

```json
{
  "status": "Shipped",
  "location": "Cairo Distribution Center",
  "description": "Package has been picked up by carrier"
}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Shipment status updated successfully",
  "messageAr": "تم تحديث حالة الشحنة بنجاح",
  "data": {
    "id": 1,
    "previousStatus": "Preparing",
    "currentStatus": "Shipped",
    "shippedAt": "2024-01-16T09:00:00Z",
    "updatedAt": "2024-01-16T09:00:00Z"
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-16T09:00:00Z"
}
```

### **11.4 Track Shipment**

```http
GET /api/shipments/track/{trackingNumber}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Shipment tracking information fetched successfully",
  "messageAr": "تم جلب معلومات تتبع الشحنة بنجاح",
  "data": {
    "trackingNumber": "ARX123456789",
    "carrier": "Aramex",
    "currentStatus": "Shipped",
    "estimatedDelivery": "2024-01-18T23:59:59Z",
    "trackingHistory": [
      {
        "status": "Preparing",
        "location": "Cairo Warehouse",
        "timestamp": "2024-01-15T10:30:00Z",
        "description": "Package is being prepared"
      },
      {
        "status": "Shipped",
        "location": "Cairo Distribution Center",
        "timestamp": "2024-01-16T09:00:00Z",
        "description": "Package has been picked up by carrier"
      }
    ]
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-16T15:00:00Z"
}
```

### **11.5 Mark as Delivered**

```http
POST /api/shipments/{id}/deliver
```

**Headers:**

- `Authorization: Bearer {admin_token}`

**Body:**

```json
{
  "deliveredAt": "2024-01-18T14:30:00Z",
  "recipientName": "Ahmed Hassan",
  "notes": "Delivered successfully to customer"
}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Shipment marked as delivered successfully",
  "messageAr": "تم تمييز الشحنة كمسلمة بنجاح",
  "data": {
    "id": 1,
    "status": "Delivered",
    "deliveredAt": "2024-01-18T14:30:00Z",
    "recipientName": "Ahmed Hassan",
    "orderId": 1002,
    "orderStatus": "Delivered"
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-18T14:30:00Z"
}
```

### **11.6 Mark as Failed**

```http
POST /api/shipments/{id}/fail
```

**Headers:**

- `Authorization: Bearer {admin_token}`

**Body:**

```json
{
  "failedReason": "Customer not available at delivery address",
  "attemptedAt": "2024-01-18T10:00:00Z"
}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Shipment marked as failed",
  "messageAr": "تم تمييز الشحنة كفاشلة",
  "data": {
    "id": 1,
    "status": "Failed",
    "failedReason": "Customer not available at delivery address",
    "attemptedAt": "2024-01-18T10:00:00Z"
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-18T10:05:00Z"
}
```

---

## **12. REPAIR SERVICES Endpoints**

### **12.1 Get Repair Service Items**

```http
GET /api/repair-services/items
```

**Query Parameters:**

- `page` (int, default: 1)
- `pageSize` (int, default: 20)
- `repairType` (enum: Hardware, Software, Diagnostic, Upgrade, Other, optional)
- `isActive` (boolean, optional)

**Response:**

```json
{
  "isSuccess": true,
  "message": "Repair service items fetched successfully",
  "messageAr": "تم جلب خدمات الإصلاح بنجاح",
  "data": {
    "items": [
      {
        "id": 1,
        "name": "Screen Replacement",
        "repairType": "Hardware",
        "description": "Replace cracked or damaged laptop screen",
        "basePrice": 150.00,
        "estimatedDays": 3,
        "isActive": true
      },
      {
        "id": 2,
        "name": "Battery Replacement",
        "repairType": "Hardware",
        "description": "Replace worn out or defective battery",
        "basePrice": 80.00,
        "estimatedDays": 2,
        "isActive": true
      },
      {
        "id": 3,
        "name": "OS Reinstallation",
        "repairType": "Software",
        "description": "Clean installation of operating system",
        "basePrice": 50.00,
        "estimatedDays": 1,
        "isActive": true
      },
      {
        "id": 4,
        "name": "Virus Removal",
        "repairType": "Software",
        "description": "Remove malware and viruses",
        "basePrice": 40.00,
        "estimatedDays": 1,
        "isActive": true
      },
      {
        "id": 5,
        "name": "RAM Upgrade",
        "repairType": "Upgrade",
        "description": "Upgrade laptop RAM capacity",
        "basePrice": 100.00,
        "estimatedDays": 1,
        "isActive": true
      }
    ],
    "page": 1,
    "pageSize": 20,
    "totalCount": 15,
    "totalPages": 1,
    "hasPrevious": false,
    "hasNext": false,
    "startIndex": 1,
    "endIndex": 15
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **12.2 Get Repair Service Item Details**

```http
GET /api/repair-services/items/{id}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Repair service item details fetched successfully",
  "messageAr": "تم جلب تفاصيل خدمة الإصلاح بنجاح",
  "data": {
    "id": 1,
    "name": "Screen Replacement",
    "repairType": "Hardware",
    "description": "Replace cracked or damaged laptop screen with genuine parts",
    "basePrice": 150.00,
    "estimatedDays": 3,
    "isActive": true,
    "createdAt": "2024-01-01T00:00:00Z",
    "updatedAt": "2024-01-15T10:30:00Z",
    "statistics": {
      "totalRequests": 45,
      "completedRequests": 42,
      "averageCompletionDays": 2.5,
      "customerSatisfaction": 4.7
    }
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **12.3 Get User Repair Requests**

```http
GET /api/repair-services/my-requests
```

**Query Parameters:**

- `page` (int, default: 1)
- `pageSize` (int, default: 10)
- `status` (enum: Pending, Diagnosed, Approved, InProgress, Completed, Cancelled, PickupReady, optional)

**Response:**

```json
{
  "isSuccess": true,
  "message": "Repair requests fetched successfully",
  "messageAr": "تم جلب طلبات الإصلاح بنجاح",
  "data": {
    "items": [
      {
        "id": 1,
        "requestNumber": "REP-2024-0001",
        "serviceItem": {
          "id": 1,
          "name": "Screen Replacement",
          "repairType": "Hardware"
        },
        "deviceSerial": "LNVXPS13-001",
        "issueDescription": "Screen is cracked and has dead pixels",
        "status": "InProgress",
        "priority": "Normal",
        "quotedPrice": 180.00,
        "requestDate": "2024-01-10T10:00:00Z",
        "estimatedCompletion": "2024-01-13T17:00:00Z"
      },
      {
        "id": 2,
        "requestNumber": "REP-2024-0002",
        "serviceItem": {
          "id": 3,
          "name": "OS Reinstallation",
          "repairType": "Software"
        },
        "deviceSerial": "HPENV14-002",
        "issueDescription": "Windows won't boot, need clean installation",
        "status": "Completed",
        "priority": "Normal",
        "quotedPrice": 50.00,
        "requestDate": "2024-01-05T09:00:00Z",
        "completedDate": "2024-01-06T15:00:00Z"
      }
    ],
    "page": 1,
    "pageSize": 10,
    "totalCount": 2,
    "totalPages": 1,
    "hasPrevious": false,
    "hasNext": false,
    "startIndex": 1,
    "endIndex": 2
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **12.4 Get Repair Request Details**

```http
GET /api/repair-services/requests/{id}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Repair request details fetched successfully",
  "messageAr": "تم جلب تفاصيل طلب الإصلاح بنجاح",
  "data": {
    "id": 1,
    "requestNumber": "REP-2024-0001",
    "user": {
      "id": "user123",
      "firstName": "Ahmed",
      "lastName": "Hassan",
      "email": "ahmed@example.com",
      "phoneNumber": "+201234567890"
    },
    "serviceItem": {
      "id": 1,
      "name": "Screen Replacement",
      "repairType": "Hardware",
      "basePrice": 150.00,
      "estimatedDays": 3
    },
    "laptopVariant": {
      "id": 1,
      "name": "Dell XPS 13 - 16GB RAM, 512GB SSD",
      "sku": "DLXPS13-16-512-SSD"
    },
    "deviceSerial": "LNVXPS13-001",
    "issueDescription": "Screen is cracked and has dead pixels in the upper right corner. Happened after accidental drop.",
    "diagnosisNotes": "Screen confirmed damaged. LCD panel needs replacement. Digitizer intact.",
    "quotedPrice": 180.00,
    "status": "InProgress",
    "priority": "Normal",
    "requestDate": "2024-01-10T10:00:00Z",
    "diagnosedAt": "2024-01-10T15:00:00Z",
    "approvedAt": "2024-01-10T16:00:00Z",
    "estimatedCompletion": "2024-01-13T17:00:00Z",
    "completedDate": null,
    "statusHistory": [
      {
        "status": "Pending",
        "timestamp": "2024-01-10T10:00:00Z",
        "notes": "Repair request submitted"
      },
      {
        "status": "Diagnosed",
        "timestamp": "2024-01-10T15:00:00Z",
        "notes": "Screen damage confirmed, quote provided"
      },
      {
        "status": "Approved",
        "timestamp": "2024-01-10T16:00:00Z",
        "notes": "Customer approved the quote"
      },
      {
        "status": "InProgress",
        "timestamp": "2024-01-11T09:00:00Z",
        "notes": "Repair work started"
      }
    ],
    "createdAt": "2024-01-10T10:00:00Z",
    "updatedAt": "2024-01-11T09:00:00Z"
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **12.5 Create Repair Request**

```http
POST /api/repair-services/requests
```

**Body:**

```json
{
  "serviceItemId": 1,
  "laptopVariantId": 1,
  "deviceSerial": "LNVXPS13-001",
  "issueDescription": "Screen is cracked and has dead pixels in the upper right corner. Happened after accidental drop.",
  "priority": "Normal"
}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Repair request created successfully",
  "messageAr": "تم إنشاء طلب الإصلاح بنجاح",
  "data": {
    "id": 1,
    "requestNumber": "REP-2024-0001",
    "serviceItem": {
      "id": 1,
      "name": "Screen Replacement",
      "basePrice": 150.00,
      "estimatedDays": 3
    },
    "deviceSerial": "LNVXPS13-001",
    "issueDescription": "Screen is cracked and has dead pixels in the upper right corner. Happened after accidental drop.",
    "status": "Pending",
    "priority": "Normal",
    "requestDate": "2024-01-15T10:30:00Z"
  },
  "errors": [],
  "statusCode": 201,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **12.6 Update Repair Request Status**

```json
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:35:00Z"
}
```

### **12.7 Cancel Repair Request**

```http
DELETE /api/repair-services/requests/{id}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Repair request cancelled successfully",
  "messageAr": "تم إلغاء طلب الإصلاح بنجاح",
  "data": {
    "id": 1,
    "requestNumber": "REP-2024-0001",
    "status": "Cancelled",
    "cancelledAt": "2024-01-15T10:40:00Z"
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:40:00Z"
}
```

### **12.8 Approve Repair Quote**

```http
POST /api/repair-services/requests/{id}/approve
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Repair quote approved successfully",
  "messageAr": "تم الموافقة على عرض الإصلاح بنجاح",
  "data": {
    "id": 1,
    "requestNumber": "REP-2024-0001",
    "status": "Approved",
    "quotedPrice": 180.00,
    "approvedAt": "2024-01-15T10:45:00Z"
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:45:00Z"
}
```

---

## **13. STOCK ALERTS Endpoints**

### **13.1 Get User Stock Alerts**

```http
GET /api/stock-alerts
```

**Query Parameters:**

- `page` (int, default: 1)
- `pageSize` (int, default: 20)
- `isNotified` (boolean, optional)
- `productType` (enum: LaptopVariant, Accessory, optional)

**Response:**

```json
{
  "isSuccess": true,
  "message": "Stock alerts fetched successfully",
  "messageAr": "تم جلب تنبيهات المخزون بنجاح",
  "data": {
    "items": [
      {
        "id": 1,
        "product": {
          "type": "LaptopVariant",
          "id": 151,
          "name": "Lenovo ThinkPad X1 - 16GB RAM, 512GB SSD",
          "sku": "LEN-TPX1-16-512-SSD",
          "currentPrice": 1499.99,
          "stockQuantity": 0,
          "image": "/images/laptops/lenovo-thinkpad-x1.jpg"
        },
        "isNotified": false,
        "createdAt": "2024-01-10T10:00:00Z",
        "notifiedAt": null
      },
      {
        "id": 2,
        "product": {
          "type": "Accessory",
          "id": 25,
          "name": "Laptop Bag Premium",
          "sku": "ACC-BAG-001",
          "currentPrice": 49.99,
          "stockQuantity": 15,
          "image": "/images/accessories/laptop-bag.jpg"
        },
        "isNotified": true,
        "createdAt": "2024-01-05T09:00:00Z",
        "notifiedAt": "2024-01-12T14:30:00Z"
      }
    ],
    "page": 1,
    "pageSize": 20,
    "totalCount": 2,
    "totalPages": 1,
    "hasPrevious": false,
    "hasNext": false,
    "startIndex": 1,
    "endIndex": 2
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **13.2 Create Stock Alert**

```http
POST /api/stock-alerts
```

**Body:**

```json
{
  "productType": "LaptopVariant",
  "productId": 151
}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Stock alert created successfully. You will be notified when the product is back in stock.",
  "messageAr": "تم إنشاء تنبيه المخزون بنجاح. سيتم إخطارك عندما يعود المنتج إلى المخزون.",
  "data": {
    "id": 1,
    "productType": "LaptopVariant",
    "productId": 151,
    "productName": "Lenovo ThinkPad X1 - 16GB RAM, 512GB SSD",
    "isNotified": false,
    "createdAt": "2024-01-15T10:30:00Z"
  },
  "errors": [],
  "statusCode": 201,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **13.3 Delete Stock Alert**

```http
DELETE /api/stock-alerts/{id}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Stock alert deleted successfully",
  "messageAr": "تم حذف تنبيه المخزون بنجاح",
  "data": {
    "id": 1,
    "deletedAt": "2024-01-15T10:35:00Z"
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:35:00Z"
}
```

---

## **14. PRODUCT VIEWS Endpoints**

### **14.1 Track Product View**

```http
POST /api/product-views
```

**Body:**

```json
{
  "productType": "LaptopVariant",
  "productId": 1
}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Product view tracked successfully",
  "messageAr": "تم تتبع عرض المنتج بنجاح",
  "data": {
    "id": 1,
    "productType": "LaptopVariant",
    "productId": 1,
    "viewedAt": "2024-01-15T10:30:00Z"
  },
  "errors": [],
  "statusCode": 201,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **14.2 Get Product View Statistics**

```http
GET /api/product-views/statistics
```

**Query Parameters:**

- `productType` (enum: LaptopVariant, Accessory, required)
- `productId` (int, required)
- `startDate` (datetime, optional)
- `endDate` (datetime, optional)

**Response:**

```json
{
  "isSuccess": true,
  "message": "Product view statistics fetched successfully",
  "messageAr": "تم جلب إحصائيات عرض المنتج بنجاح",
  "data": {
    "product": {
      "type": "LaptopVariant",
      "id": 1,
      "name": "Dell XPS 13 - 16GB RAM, 512GB SSD"
    },
    "totalViews": 1547,
    "uniqueUsers": 892,
    "anonymousViews": 655,
    "viewsByDate": [
      {
        "date": "2024-01-15",
        "views": 87
      },
      {
        "date": "2024-01-14",
        "views": 92
      },
      {
        "date": "2024-01-13",
        "views": 76
      }
    ],
    "averageDailyViews": 82.5,
    "peakViewDate": "2024-01-10",
    "peakViewCount": 145
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **14.3 Get User's Recently Viewed Products**

```http
GET /api/product-views/my-history
```

**Query Parameters:**

- `page` (int, default: 1)
- `pageSize` (int, default: 20)

**Response:**

```json
{
  "isSuccess": true,
  "message": "Recently viewed products fetched successfully",
  "messageAr": "تم جلب المنتجات المشاهدة مؤخراً بنجاح",
  "data": {
    "items": [
      {
        "id": 1,
        "product": {
          "type": "LaptopVariant",
          "id": 1,
          "name": "Dell XPS 13 - 16GB RAM, 512GB SSD",
          "sku": "DLXPS13-16-512-SSD",
          "currentPrice": 1299.99,
          "stockQuantity": 15,
          "image": "/images/laptops/dell-xps-13.jpg",
          "averageRating": 4.5
        },
        "viewedAt": "2024-01-15T10:30:00Z"
      },
      {
        "id": 2,
        "product": {
          "type": "Accessory",
          "id": 25,
          "name": "Laptop Bag Premium",
          "sku": "ACC-BAG-001",
          "currentPrice": 49.99,
          "stockQuantity": 50,
          "image": "/images/accessories/laptop-bag.jpg",
          "averageRating": 4.2
        },
        "viewedAt": "2024-01-15T09:45:00Z"
      }
    ],
    "page": 1,
    "pageSize": 20,
    "totalCount": 12,
    "totalPages": 1,
    "hasPrevious": false,
    "hasNext": false,
    "startIndex": 1,
    "endIndex": 12
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

---

## **15. IMAGES Endpoints**

### **15.1 Upload Image**

```http
POST /api/images/upload
```

**Headers:**

- `Content-Type: multipart/form-data`
- `Authorization: Bearer {admin_token}`

**Form Data:**

- `file` (file, required)
- `entityType` (string, required) - "Laptop", "Accessory", "Category", "Brand"
- `entityId` (int, required)
- `isMain` (boolean, default: false)
- `displayOrder` (int, default: 0)

**Response:**

```json
{
  "isSuccess": true,
  "message": "Image uploaded successfully",
  "messageAr": "تم رفع الصورة بنجاح",
  "data": {
    "id": 1,
    "imageUrl": "/images/laptops/dell-xps-13-01.jpg",
    "entityType": "Laptop",
    "entityId": 1,
    "isMain": false,
    "displayOrder": 1,
    "uploadedAt": "2024-01-15T10:30:00Z"
  },
  "errors": [],
  "statusCode": 201,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **15.2 Delete Image**

```http
DELETE /api/images/{id}
```

**Headers:**

- `Authorization: Bearer {admin_token}`

**Response:**

```json
{
  "isSuccess": true,
  "message": "Image deleted successfully",
  "messageAr": "تم حذف الصورة بنجاح",
  "data": {
    "id": 1,
    "deletedAt": "2024-01-15T10:35:00Z"
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:35:00Z"
}
```

### **15.3 Set Main Image**

```http
PATCH /api/images/{id}/set-main
```

**Headers:**

- `Authorization: Bearer {admin_token}`

**Response:**

```json
{
  "isSuccess": true,
  "message": "Main image updated successfully",
  "messageAr": "تم تحديث الصورة الرئيسية بنجاح",
  "data": {
    "id": 1,
    "imageUrl": "/images/laptops/dell-xps-13-01.jpg",
    "isMain": true,
    "updatedAt": "2024-01-15T10:40:00Z"
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:40:00Z"
}
```

### **15.4 Reorder Images**

```http
PUT /api/images/reorder
```

**Headers:**

- `Authorization: Bearer {admin_token}`

**Body:**

```json
{
  "entityType": "Laptop",
  "entityId": 1,
  "imageOrders": [
    { "imageId": 1, "displayOrder": 0 },
    { "imageId": 2, "displayOrder": 1 },
    { "imageId": 3, "displayOrder": 2 }
  ]
}
```

**Response:**

```json
{
  "isSuccess": true,
  "message": "Images reordered successfully",
  "messageAr": "تم إعادة ترتيب الصور بنجاح",
  "data": {
    "entityType": "Laptop",
    "entityId": 1,
    "updatedImages": 3,
    "updatedAt": "2024-01-15T10:45:00Z"
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:45:00Z"
}
```

---

## **16. ANALYTICS Endpoints**

### **16.1 Get Dashboard Statistics**

```http
GET /api/analytics/dashboard
```

**Headers:**

- `Authorization: Bearer {admin_token}`

**Query Parameters:**

- `startDate` (datetime, optional)
- `endDate` (datetime, optional)

**Response:**

```json
{
  "isSuccess": true,
  "message": "Dashboard statistics fetched successfully",
  "messageAr": "تم جلب إحصائيات لوحة التحكم بنجاح",
  "data": {
    "overview": {
      "totalRevenue": 487500.00,
      "totalOrders": 245,
      "activeUsers": 1547,
      "pendingRepairs": 12,
      "lowStockItems": 8
    },
    "salesByDay": [
      {
        "date": "2024-01-15",
        "revenue": 15430.50,
        "orders": 8
      },
      {
        "date": "2024-01-14",
        "revenue": 22100.00,
        "orders": 12
      }
    ],
    "topProducts": [
      {
        "productType": "LaptopVariant",
        "productId": 1,
        "productName": "Dell XPS 13 - 16GB RAM, 512GB SSD",
        "totalSold": 45,
        "revenue": 58499.55
      },
      {
        "productType": "LaptopVariant",
        "productId": 151,
        "productName": "Lenovo ThinkPad X1 - 16GB RAM, 512GB SSD",
        "totalSold": 32,
        "revenue": 47999.68
      }
    ],
    "ordersByStatus": {
      "Pending": 5,
      "Reserved": 12,
      "Processing": 8,
      "Shipped": 15,
      "Delivered": 189,
      "Cancelled": 16
    },
    "categoryPerformance": [
      {
        "categoryId": 1,
        "categoryName": "Business Laptops",
        "totalSold": 87,
        "revenue": 125600.00
      },
      {
        "categoryId": 2,
        "categoryName": "Gaming Laptops",
        "totalSold": 65,
        "revenue": 156800.00
      }
    ]
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **16.2 Get Sales Report**

```http
GET /api/analytics/sales-report
```

**Headers:**

- `Authorization: Bearer {admin_token}`

**Query Parameters:**

- `startDate` (datetime, required)
- `endDate` (datetime, required)
- `groupBy` (enum: day, week, month, default: day)
- `categoryId` (int, optional)
- `brandId` (int, optional)

**Response:**

```json
{
  "isSuccess": true,
  "message": "Sales report fetched successfully",
  "messageAr": "تم جلب تقرير المبيعات بنجاح",
  "data": {
    "period": {
      "startDate": "2024-01-01T00:00:00Z",
      "endDate": "2024-01-31T23:59:59Z",
      "groupBy": "day"
    },
    "summary": {
      "totalRevenue": 487500.00,
      "totalOrders": 245,
      "averageOrderValue": 1989.80,
      "totalItemsSold": 312,
      "totalDiscountsGiven": 15240.00
    },
    "salesData": [
      {
        "period": "2024-01-01",
        "revenue": 12450.00,
        "orders": 6,
        "itemsSold": 8
      },
      {
        "period": "2024-01-02",
        "revenue": 18900.00,
        "orders": 9,
        "itemsSold": 11
      }
    ],
    "topSellingProducts": [
      {
        "productType": "LaptopVariant",
        "productId": 1,
        "productName": "Dell XPS 13 - 16GB RAM, 512GB SSD",
        "quantitySold": 45,
        "revenue": 58499.55
      }
    ],
    "paymentMethodBreakdown": {
      "Card": 215,
      "Cash": 30
    }
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **16.3 Get Inventory Report**

```http
GET /api/analytics/inventory-report
```

**Headers:**

- `Authorization: Bearer {admin_token}`

**Query Parameters:**

- `categoryId` (int, optional)
- `brandId` (int, optional)
- `stockStatus` (enum: InStock, LowStock, OutOfStock, optional)

**Response:**

```json
{
  "isSuccess": true,
  "message": "Inventory report fetched successfully",
  "messageAr": "تم جلب تقرير المخزون بنجاح",
  "data": {
    "summary": {
      "totalProducts": 156,
      "inStock": 132,
      "lowStock": 18,
      "outOfStock": 6,
      "totalValue": 1250000.00
    },
    "lowStockItems": [
      {
        "productType": "LaptopVariant",
        "productId": 15,
        "productName": "HP Envy 14 - 8GB RAM, 256GB SSD",
        "sku": "HPENV14-8-256-SSD",
        "currentStock": 3,
        "reorderLevel": 5,
        "reservedQuantity": 1,
        "availableStock": 2
      }
    ],
    "outOfStockItems": [
      {
        "productType": "LaptopVariant",
        "productId": 151,
        "productName": "Lenovo ThinkPad X1 - 16GB RAM, 512GB SSD",
        "sku": "LEN-TPX1-16-512-SSD",
        "currentStock": 0,
        "pendingStockAlerts": 12
      }
    ],
    "categoryBreakdown": [
      {
        "categoryId": 1,
        "categoryName": "Business Laptops",
        "totalItems": 45,
        "inStock": 38,
        "lowStock": 5,
        "outOfStock": 2
      }
    ]
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **16.4 Get Customer Analytics**

```http
GET /api/analytics/customers
```

**Headers:**

- `Authorization: Bearer {admin_token}`

**Query Parameters:**

- `startDate` (datetime, optional)
- `endDate` (datetime, optional)

**Response:**

```json
{
  "isSuccess": true,
  "message": "Customer analytics fetched successfully",
  "messageAr": "تم جلب تحليلات العملاء بنجاح",
  "data": {
    "summary": {
      "totalCustomers": 1547,
      "newCustomers": 87,
      "returningCustomers": 245,
      "averageOrderValue": 1989.80,
      "customerLifetimeValue": 3450.00
    },
    "topCustomers": [
      {
        "userId": "user123",
        "firstName": "Ahmed",
        "lastName": "Hassan",
        "email": "ahmed@example.com",
        "totalOrders": 8,
        "totalSpent": 15600.00,
        "averageOrderValue": 1950.00
      }
    ],
    "customerRetention": {
      "repeatCustomerRate": 15.8,
      "averageOrdersPerCustomer": 1.4,
      "churnRate": 8.2
    },
    "customersByRegion": [
      {
        "city": "Cairo",
        "customers": 654,
        "orders": 892,
        "revenue": 285400.00
      },
      {
        "city": "Giza",
        "customers": 412,
        "orders": 534,
        "revenue": 156700.00
      }
    ]
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

---

## **ERROR CODES**

### **Standard Error Response Format**

```json
{
  "isSuccess": false,
  "message": "Error message in English",
  "messageAr": "رسالة الخطأ بالعربية",
  "data": null,
  "errors": [
    {
      "field": "email",
      "message": "Email is required",
      "code": "REQUIRED_FIELD"
    }
  ],
  "statusCode": 400,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

### **HTTP Status Codes**

- `200 OK` - Request succeeded
- `201 Created` - Resource created successfully
- `400 Bad Request` - Invalid request data
- `401 Unauthorized` - Missing or invalid authentication
- `403 Forbidden` - Insufficient permissions
- `404 Not Found` - Resource not found
- `409 Conflict` - Resource conflict (e.g., duplicate)
- `422 Unprocessable Entity` - Validation errors
- `429 Too Many Requests` - Rate limit exceeded
- `500 Internal Server Error` - Server error

### **Common Error Codes**

- `REQUIRED_FIELD` - Required field missing
- `INVALID_FORMAT` - Invalid field format
- `NOT_FOUND` - Resource not found
- `DUPLICATE_ENTRY` - Duplicate record
- `INSUFFICIENT_STOCK` - Not enough stock
- `INVALID_DISCOUNT_CODE` - Invalid or expired discount code
- `PAYMENT_FAILED` - Payment processing failed
- `ORDER_EXPIRED` - Reservation expired
- `UNAUTHORIZED_ACCESS` - Access denied
- `RATE_LIMIT_EXCEEDED` - Too many requests

---

## **PAGINATION**

All list endpoints follow the same pagination structure:

**Query Parameters:**

- `page` (int, default: 1)
- `pageSize` (int, default: 20, max: 100)

**Response Structure:**

```json
{
  "items": [...],
  "page": 1,
  "pageSize": 20,
  "totalCount": 245,
  "totalPages": 13,
  "hasPrevious": false,
  "hasNext": true,
  "startIndex": 1,
  "endIndex": 20
}
```

---

## **FILTERING & SORTING**

Most list endpoints support:

**Common Query Parameters:**

- `search` (string) - Search term
- `sortBy` (string) - Field to sort by
- `sortOrder` (enum: asc, desc, default: desc)
- `isActive` (boolean) - Filter by active status

**Example:**

```http
GET /api/laptops/variants?search=dell&sortBy=price&sortOrder=asc&page=1&pageSize=20
```

---

## **WEBHOOKS**

### **Payment Callback (Paymob)**

```http
POST /api/payments/callback
```

Receives payment status updates from Paymob payment gateway.

### **Shipment Updates**

```http
POST /api/webhooks/shipment-update
```

Receives tracking updates from shipping carriers.

---

## **BEST PRACTICES**

1. **Authentication**: Always include `Authorization: Bearer {token}` header
2. **Rate Limiting**: Respect rate limit headers
3. **Error Handling**: Check `isSuccess` field in responses
4. **Pagination**: Use pagination for large datasets
5. **Caching**: Cache static data (brands, categories)
6. **Idempotency**: Use same request ID for retries
7. **Versioning**: API version included in base URL if needed

---

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

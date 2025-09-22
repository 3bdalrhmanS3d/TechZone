---
title: TechZone API
language_tabs:
  - shell: Shell
  - http: HTTP
  - javascript: JavaScript
  - ruby: Ruby
  - python: Python
  - php: PHP
  - java: Java
  - go: Go
toc_footers: []
includes: []
search: true
code_clipboard: true
highlight_theme: darkula
headingLevel: 2
generator: "@tarslib/widdershins v4.0.30"

---

# TechZone API

A comprehensive e-commerce API for technology products

Base URLs:

Email: <a href="mailto:support@techzone.com">TechZone Support</a> 

# Authentication

* API Key (Bearer)
    - Parameter Name: **Authorization**, in: header. JWT Authorization header using the Bearer scheme. 
                          Enter 'Bearer' [space] and then your token in the text input below.
                          Example: 'Bearer 12345abcdef'

# Auth

## POST /api/Auth/register

POST /api/Auth/register

> Body Parameters

```json
{
  "userName": "string",
  "email": "user@example.com",
  "fullName": "string",
  "password": "string",
  "confirmPassword": "string"
}
```

### Params

|Name|Location|Type|Required|Description|
|---|---|---|---|---|
|body|body|[RegisterDto](#schemaregisterdto)| no |none|

> Response Examples

> 200 Response

```json
{
  "isSuccess": true,
  "message": "string",
  "messageAr": "string",
  "data": true,
  "errors": [
    "string"
  ],
  "statusCode": 0,
  "timestamp": "2019-08-24T14:15:22Z"
}
```

### Responses

|HTTP Status Code |Meaning|Description|Data schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[BooleanServiceResponse](#schemabooleanserviceresponse)|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[BooleanServiceResponse](#schemabooleanserviceresponse)|
|409|[Conflict](https://tools.ietf.org/html/rfc7231#section-6.5.8)|Conflict|[BooleanServiceResponse](#schemabooleanserviceresponse)|
|500|[Internal Server Error](https://tools.ietf.org/html/rfc7231#section-6.6.1)|Internal Server Error|[BooleanServiceResponse](#schemabooleanserviceresponse)|

## POST /api/Auth/login

POST /api/Auth/login

> Body Parameters

```json
{
  "email": "string",
  "password": "string"
}
```

### Params

|Name|Location|Type|Required|Description|
|---|---|---|---|---|
|body|body|[TokenRequestDto](#schematokenrequestdto)| no |none|

> Response Examples

> 200 Response

```json
{
  "isSuccess": true,
  "message": "string",
  "messageAr": "string",
  "data": {
    "message": "string",
    "isAuthenticated": true,
    "username": "string",
    "email": "string",
    "roles": [
      "string"
    ],
    "token": "string",
    "emailConfirmed": true,
    "refreshTokenExpiration": "2019-08-24T14:15:22Z"
  },
  "errors": [
    "string"
  ],
  "statusCode": 0,
  "timestamp": "2019-08-24T14:15:22Z"
}
```

### Responses

|HTTP Status Code |Meaning|Description|Data schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[AuthDtoServiceResponse](#schemaauthdtoserviceresponse)|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[AuthDtoServiceResponse](#schemaauthdtoserviceresponse)|
|401|[Unauthorized](https://tools.ietf.org/html/rfc7235#section-3.1)|Unauthorized|[AuthDtoServiceResponse](#schemaauthdtoserviceresponse)|
|403|[Forbidden](https://tools.ietf.org/html/rfc7231#section-6.5.3)|Forbidden|[AuthDtoServiceResponse](#schemaauthdtoserviceresponse)|
|500|[Internal Server Error](https://tools.ietf.org/html/rfc7231#section-6.6.1)|Internal Server Error|[AuthDtoServiceResponse](#schemaauthdtoserviceresponse)|

## POST /api/Auth/confirm-email

POST /api/Auth/confirm-email

> Body Parameters

```json
{
  "email": "user@example.com",
  "code": "string"
}
```

### Params

|Name|Location|Type|Required|Description|
|---|---|---|---|---|
|body|body|[ConfirmEmailWithCodeDto](#schemaconfirmemailwithcodedto)| no |none|

> Response Examples

> 200 Response

```json
{
  "isSuccess": true,
  "message": "string",
  "messageAr": "string",
  "data": true,
  "errors": [
    "string"
  ],
  "statusCode": 0,
  "timestamp": "2019-08-24T14:15:22Z"
}
```

### Responses

|HTTP Status Code |Meaning|Description|Data schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[BooleanServiceResponse](#schemabooleanserviceresponse)|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[BooleanServiceResponse](#schemabooleanserviceresponse)|
|404|[Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4)|Not Found|[BooleanServiceResponse](#schemabooleanserviceresponse)|
|500|[Internal Server Error](https://tools.ietf.org/html/rfc7231#section-6.6.1)|Internal Server Error|[BooleanServiceResponse](#schemabooleanserviceresponse)|

## POST /api/Auth/resend-verification-code

POST /api/Auth/resend-verification-code

> Body Parameters

```json
{
  "email": "user@example.com",
  "verificationType": 0
}
```

### Params

|Name|Location|Type|Required|Description|
|---|---|---|---|---|
|body|body|[ResendVerificationCodeDto](#schemaresendverificationcodedto)| no |none|

> Response Examples

> 200 Response

```json
{
  "isSuccess": true,
  "message": "string",
  "messageAr": "string",
  "data": true,
  "errors": [
    "string"
  ],
  "statusCode": 0,
  "timestamp": "2019-08-24T14:15:22Z"
}
```

### Responses

|HTTP Status Code |Meaning|Description|Data schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[BooleanServiceResponse](#schemabooleanserviceresponse)|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[BooleanServiceResponse](#schemabooleanserviceresponse)|
|500|[Internal Server Error](https://tools.ietf.org/html/rfc7231#section-6.6.1)|Internal Server Error|[BooleanServiceResponse](#schemabooleanserviceresponse)|

## POST /api/Auth/forgot-password

POST /api/Auth/forgot-password

> Body Parameters

```json
{
  "email": "user@example.com"
}
```

### Params

|Name|Location|Type|Required|Description|
|---|---|---|---|---|
|body|body|[ForgotPasswordDto](#schemaforgotpassworddto)| no |none|

> Response Examples

> 200 Response

```json
{
  "isSuccess": true,
  "message": "string",
  "messageAr": "string",
  "data": true,
  "errors": [
    "string"
  ],
  "statusCode": 0,
  "timestamp": "2019-08-24T14:15:22Z"
}
```

### Responses

|HTTP Status Code |Meaning|Description|Data schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[BooleanServiceResponse](#schemabooleanserviceresponse)|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[BooleanServiceResponse](#schemabooleanserviceresponse)|
|500|[Internal Server Error](https://tools.ietf.org/html/rfc7231#section-6.6.1)|Internal Server Error|[BooleanServiceResponse](#schemabooleanserviceresponse)|

## POST /api/Auth/reset-password

POST /api/Auth/reset-password

> Body Parameters

```json
{
  "email": "user@example.com",
  "code": "string",
  "newPassword": "string",
  "confirmPassword": "string"
}
```

### Params

|Name|Location|Type|Required|Description|
|---|---|---|---|---|
|body|body|[ResetPasswordWithCodeDto](#schemaresetpasswordwithcodedto)| no |none|

> Response Examples

> 200 Response

```json
{
  "isSuccess": true,
  "message": "string",
  "messageAr": "string",
  "data": true,
  "errors": [
    "string"
  ],
  "statusCode": 0,
  "timestamp": "2019-08-24T14:15:22Z"
}
```

### Responses

|HTTP Status Code |Meaning|Description|Data schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[BooleanServiceResponse](#schemabooleanserviceresponse)|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[BooleanServiceResponse](#schemabooleanserviceresponse)|
|404|[Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4)|Not Found|[BooleanServiceResponse](#schemabooleanserviceresponse)|
|500|[Internal Server Error](https://tools.ietf.org/html/rfc7231#section-6.6.1)|Internal Server Error|[BooleanServiceResponse](#schemabooleanserviceresponse)|

## POST /api/Auth/change-password

POST /api/Auth/change-password

> Body Parameters

```json
{
  "currentPassword": "string",
  "newPassword": "string",
  "confirmPassword": "string"
}
```

### Params

|Name|Location|Type|Required|Description|
|---|---|---|---|---|
|body|body|[ChangePasswordDto](#schemachangepassworddto)| no |none|

> Response Examples

> 200 Response

```json
{
  "isSuccess": true,
  "message": "string",
  "messageAr": "string",
  "data": true,
  "errors": [
    "string"
  ],
  "statusCode": 0,
  "timestamp": "2019-08-24T14:15:22Z"
}
```

### Responses

|HTTP Status Code |Meaning|Description|Data schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[BooleanServiceResponse](#schemabooleanserviceresponse)|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[BooleanServiceResponse](#schemabooleanserviceresponse)|
|401|[Unauthorized](https://tools.ietf.org/html/rfc7235#section-3.1)|Unauthorized|[BooleanServiceResponse](#schemabooleanserviceresponse)|
|404|[Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4)|Not Found|[BooleanServiceResponse](#schemabooleanserviceresponse)|
|500|[Internal Server Error](https://tools.ietf.org/html/rfc7231#section-6.6.1)|Internal Server Error|[BooleanServiceResponse](#schemabooleanserviceresponse)|

## POST /api/Auth/refresh-token

POST /api/Auth/refresh-token

> Body Parameters

```json
{
  "refreshToken": "string"
}
```

### Params

|Name|Location|Type|Required|Description|
|---|---|---|---|---|
|body|body|[RefreshTokenDto](#schemarefreshtokendto)| no |none|

> Response Examples

> 200 Response

```json
{
  "isSuccess": true,
  "message": "string",
  "messageAr": "string",
  "data": {
    "message": "string",
    "isAuthenticated": true,
    "username": "string",
    "email": "string",
    "roles": [
      "string"
    ],
    "token": "string",
    "emailConfirmed": true,
    "refreshTokenExpiration": "2019-08-24T14:15:22Z"
  },
  "errors": [
    "string"
  ],
  "statusCode": 0,
  "timestamp": "2019-08-24T14:15:22Z"
}
```

### Responses

|HTTP Status Code |Meaning|Description|Data schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[AuthDtoServiceResponse](#schemaauthdtoserviceresponse)|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[AuthDtoServiceResponse](#schemaauthdtoserviceresponse)|
|401|[Unauthorized](https://tools.ietf.org/html/rfc7235#section-3.1)|Unauthorized|[AuthDtoServiceResponse](#schemaauthdtoserviceresponse)|
|500|[Internal Server Error](https://tools.ietf.org/html/rfc7231#section-6.6.1)|Internal Server Error|[AuthDtoServiceResponse](#schemaauthdtoserviceresponse)|

## POST /api/Auth/revoke-token

POST /api/Auth/revoke-token

> Body Parameters

```json
{
  "refreshToken": "string"
}
```

### Params

|Name|Location|Type|Required|Description|
|---|---|---|---|---|
|body|body|[RevokeTokenDto](#schemarevoketokendto)| no |none|

> Response Examples

> 200 Response

```json
{
  "isSuccess": true,
  "message": "string",
  "messageAr": "string",
  "data": true,
  "errors": [
    "string"
  ],
  "statusCode": 0,
  "timestamp": "2019-08-24T14:15:22Z"
}
```

### Responses

|HTTP Status Code |Meaning|Description|Data schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[BooleanServiceResponse](#schemabooleanserviceresponse)|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[BooleanServiceResponse](#schemabooleanserviceresponse)|
|401|[Unauthorized](https://tools.ietf.org/html/rfc7235#section-3.1)|Unauthorized|[BooleanServiceResponse](#schemabooleanserviceresponse)|
|404|[Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4)|Not Found|[BooleanServiceResponse](#schemabooleanserviceresponse)|
|500|[Internal Server Error](https://tools.ietf.org/html/rfc7231#section-6.6.1)|Internal Server Error|[BooleanServiceResponse](#schemabooleanserviceresponse)|

## POST /api/Auth/logout

POST /api/Auth/logout

> Response Examples

> 200 Response

```json
{
  "isSuccess": true,
  "message": "string",
  "messageAr": "string",
  "data": true,
  "errors": [
    "string"
  ],
  "statusCode": 0,
  "timestamp": "2019-08-24T14:15:22Z"
}
```

### Responses

|HTTP Status Code |Meaning|Description|Data schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[BooleanServiceResponse](#schemabooleanserviceresponse)|
|500|[Internal Server Error](https://tools.ietf.org/html/rfc7231#section-6.6.1)|Internal Server Error|[BooleanServiceResponse](#schemabooleanserviceresponse)|

## GET /api/Auth/status

GET /api/Auth/status

> Response Examples

> 200 Response

```json
{
  "isSuccess": true,
  "message": "string",
  "messageAr": "string",
  "data": null,
  "errors": [
    "string"
  ],
  "statusCode": 0,
  "timestamp": "2019-08-24T14:15:22Z"
}
```

### Responses

|HTTP Status Code |Meaning|Description|Data schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[ObjectServiceResponse](#schemaobjectserviceresponse)|
|500|[Internal Server Error](https://tools.ietf.org/html/rfc7231#section-6.6.1)|Internal Server Error|[ObjectServiceResponse](#schemaobjectserviceresponse)|

# Category

## GET /api/Category/with-laptop-counts

GET /api/Category/with-laptop-counts

> Response Examples

> 200 Response

```json
[
  {
    "id": 0,
    "name": "string",
    "laptopsCount": 0
  }
]
```

### Responses

|HTTP Status Code |Meaning|Description|Data schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|Inline|

### Responses Data Schema

HTTP Status Code **200**

|Name|Type|Required|Restrictions|Title|description|
|---|---|---|---|---|---|
|*anonymous*|[[CategoryWithCountDto](#schemacategorywithcountdto)]|false|none||none|
|» id|integer(int32)|false|none||none|
|» name|string¦null|false|none||none|
|» laptopsCount|integer(int32)|false|none||none|

# Laptop

## GET /api/Laptop

GET /api/Laptop

### Params

|Name|Location|Type|Required|Description|
|---|---|---|---|---|
|Page|query|integer(int32)| no |none|
|PageSize|query|integer(int32)| no |none|
|Search|query|string| no |none|
|SortBy|query|[LaptopSortBy](#schemalaptopsortby)| no |none|
|SortDirection|query|[SortDirection](#schemasortdirection)| no |none|

#### Enum

|Name|Value|
|---|---|
|SortBy|0|
|SortBy|1|
|SortBy|2|
|SortDirection|0|
|SortDirection|1|

> Response Examples

> 200 Response

```json
{
  "isSuccess": true,
  "message": "string",
  "messageAr": "string",
  "data": {
    "items": [
      {
        "id": 0,
        "name": "string",
        "price": 0.1,
        "category": "string",
        "images": [
          "string"
        ],
        "rate": 0.1,
        "reviewsCount": 0,
        "isDiscounted": true,
        "discountedPrice": 0.1,
        "shortDescription": "string"
      }
    ],
    "page": 0,
    "pageSize": 0,
    "totalCount": 0,
    "totalPages": 0,
    "hasPrevious": true,
    "hasNext": true,
    "startIndex": 0,
    "endIndex": 0
  },
  "errors": [
    "string"
  ],
  "statusCode": 0,
  "timestamp": "2019-08-24T14:15:22Z"
}
```

### Responses

|HTTP Status Code |Meaning|Description|Data schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[LaptopResponseDTOPagedResultServiceResponse](#schemalaptopresponsedtopagedresultserviceresponse)|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[LaptopResponseDTOPagedResultServiceResponse](#schemalaptopresponsedtopagedresultserviceresponse)|
|500|[Internal Server Error](https://tools.ietf.org/html/rfc7231#section-6.6.1)|Internal Server Error|[LaptopResponseDTOPagedResultServiceResponse](#schemalaptopresponsedtopagedresultserviceresponse)|

## POST /api/Laptop

POST /api/Laptop

> Body Parameters

```json
{
  "modelName": "string",
  "processor": "string",
  "gpu": "string",
  "screenSize": "string",
  "hasCamera": true,
  "hasKeyboard": true,
  "hasTouchScreen": true,
  "ports": "string"
}
```

### Params

|Name|Location|Type|Required|Description|
|---|---|---|---|---|
|body|body|[CreateLaptopDto](#schemacreatelaptopdto)| no |none|

> Response Examples

> 201 Response

```json
{
  "isSuccess": true,
  "message": "string",
  "messageAr": "string",
  "data": {
    "modelName": "string",
    "processor": "string",
    "gpu": "string",
    "screenSize": "string",
    "hasCamera": true,
    "hasKeyboard": true,
    "hasTouchScreen": true,
    "ports": "string"
  },
  "errors": [
    "string"
  ],
  "statusCode": 0,
  "timestamp": "2019-08-24T14:15:22Z"
}
```

### Responses

|HTTP Status Code |Meaning|Description|Data schema|
|---|---|---|---|
|201|[Created](https://tools.ietf.org/html/rfc7231#section-6.3.2)|Created|[CreateLaptopDtoServiceResponse](#schemacreatelaptopdtoserviceresponse)|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[ObjectServiceResponse](#schemaobjectserviceresponse)|
|409|[Conflict](https://tools.ietf.org/html/rfc7231#section-6.5.8)|Conflict|[ObjectServiceResponse](#schemaobjectserviceresponse)|
|500|[Internal Server Error](https://tools.ietf.org/html/rfc7231#section-6.6.1)|Internal Server Error|[ObjectServiceResponse](#schemaobjectserviceresponse)|

## GET /api/Laptop/recommended

GET /api/Laptop/recommended

### Params

|Name|Location|Type|Required|Description|
|---|---|---|---|---|
|Page|query|integer(int32)| no |none|
|PageSize|query|integer(int32)| no |none|
|Search|query|string| no |none|
|SortBy|query|[LaptopSortBy](#schemalaptopsortby)| no |none|
|SortDirection|query|[SortDirection](#schemasortdirection)| no |none|

#### Enum

|Name|Value|
|---|---|
|SortBy|0|
|SortBy|1|
|SortBy|2|
|SortDirection|0|
|SortDirection|1|

> Response Examples

> 200 Response

```json
{
  "isSuccess": true,
  "message": "string",
  "messageAr": "string",
  "data": {
    "items": [
      {
        "id": 0,
        "name": "string",
        "price": 0.1,
        "category": "string",
        "images": [
          "string"
        ],
        "rate": 0.1,
        "reviewsCount": 0,
        "isDiscounted": true,
        "discountedPrice": 0.1,
        "shortDescription": "string"
      }
    ],
    "page": 0,
    "pageSize": 0,
    "totalCount": 0,
    "totalPages": 0,
    "hasPrevious": true,
    "hasNext": true,
    "startIndex": 0,
    "endIndex": 0
  },
  "errors": [
    "string"
  ],
  "statusCode": 0,
  "timestamp": "2019-08-24T14:15:22Z"
}
```

### Responses

|HTTP Status Code |Meaning|Description|Data schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[LaptopResponseDTOPagedResultServiceResponse](#schemalaptopresponsedtopagedresultserviceresponse)|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[LaptopResponseDTOPagedResultServiceResponse](#schemalaptopresponsedtopagedresultserviceresponse)|
|500|[Internal Server Error](https://tools.ietf.org/html/rfc7231#section-6.6.1)|Internal Server Error|[LaptopResponseDTOPagedResultServiceResponse](#schemalaptopresponsedtopagedresultserviceresponse)|

## GET /api/Laptop/{id}

GET /api/Laptop/{id}

### Params

|Name|Location|Type|Required|Description|
|---|---|---|---|---|
|id|path|integer(int32)| yes |none|

> Response Examples

> 200 Response

```json
{
  "isSuccess": true,
  "message": "string",
  "messageAr": "string",
  "data": {
    "id": 0,
    "modelName": "string",
    "processor": "string",
    "gpu": "string",
    "screenSize": "string",
    "hasCamera": true,
    "hasKeyboard": true,
    "hasTouchScreen": true,
    "ports": "string",
    "description": "string",
    "notes": "string",
    "warranty": "string",
    "brandId": 0,
    "categoryId": 0,
    "brand": {
      "id": 0,
      "name": "string",
      "country": "string",
      "logoUrl": "string",
      "description": "string",
      "laptops": [
        {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      ]
    },
    "category": {
      "id": 0,
      "name": "string",
      "description": "string",
      "laptops": [
        {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      ]
    },
    "variants": [
      {
        "id": 0,
        "laptopId": 0,
        "discountId": 0,
        "ram": 2147483647,
        "storage": 2147483647,
        "price": 0.1,
        "stockQuantity": 2147483647,
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        },
        "discount": {
          "id": 0,
          "title": "string",
          "description": "string",
          "percentage": 100,
          "startDate": "2019-08-24T14:15:22Z",
          "endDate": "2019-08-24T14:15:22Z",
          "isActive": true,
          "laptopVariants": [
            null
          ]
        },
        "orderItems": [
          {
            "id": null,
            "orderId": null,
            "order": null,
            "laptopVariantId": null,
            "laptopVariant": null,
            "quantity": null,
            "unitPrice": null
          }
        ],
        "cartItems": [
          {
            "id": null,
            "applicationUserId": null,
            "laptopVariantId": null,
            "quantity": null,
            "addedAt": null,
            "user": null,
            "laptopVariant": null
          }
        ]
      }
    ],
    "images": [
      {
        "id": 0,
        "laptopId": 0,
        "imageUrl": "string",
        "isMain": true,
        "uploadedAt": "2019-08-24T14:15:22Z",
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      }
    ],
    "ratings": [
      {
        "id": 0,
        "userId": "string",
        "laptopId": 0,
        "stars": 1,
        "comment": "string",
        "createdAt": "2019-08-24T14:15:22Z",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      }
    ],
    "repairRequests": [
      {
        "requestId": 0,
        "applicationUserId": "string",
        "itemId": 0,
        "laptopId": 0,
        "notes": "string",
        "status": 0,
        "requestDate": "2019-08-24T14:15:22Z",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "repairServiceItem": {
          "itemId": 0,
          "name": "string",
          "repairType": "[",
          "price": 0.1,
          "estimatedTime": "string",
          "repairRequests": [
            null
          ]
        },
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      }
    ]
  },
  "errors": [
    "string"
  ],
  "statusCode": 0,
  "timestamp": "2019-08-24T14:15:22Z"
}
```

### Responses

|HTTP Status Code |Meaning|Description|Data schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[LaptopServiceResponse](#schemalaptopserviceresponse)|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[LaptopServiceResponse](#schemalaptopserviceresponse)|
|404|[Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4)|Not Found|[LaptopServiceResponse](#schemalaptopserviceresponse)|
|500|[Internal Server Error](https://tools.ietf.org/html/rfc7231#section-6.6.1)|Internal Server Error|[LaptopServiceResponse](#schemalaptopserviceresponse)|

## PUT /api/Laptop/{id}

PUT /api/Laptop/{id}

> Body Parameters

```json
{
  "modelName": "string",
  "processor": "string",
  "gpu": "string",
  "screenSize": "string",
  "hasCamera": true,
  "hasKeyboard": true,
  "hasTouchScreen": true,
  "ports": "string",
  "id": 0
}
```

### Params

|Name|Location|Type|Required|Description|
|---|---|---|---|---|
|id|path|integer(int32)| yes |none|
|body|body|[UpdateLaptopDto](#schemaupdatelaptopdto)| no |none|

> Response Examples

> 200 Response

```json
{
  "isSuccess": true,
  "message": "string",
  "messageAr": "string",
  "data": {
    "id": 0,
    "modelName": "string",
    "processor": "string",
    "gpu": "string",
    "screenSize": "string",
    "hasCamera": true,
    "hasKeyboard": true,
    "hasTouchScreen": true,
    "ports": "string",
    "description": "string",
    "notes": "string",
    "warranty": "string",
    "brandId": 0,
    "categoryId": 0,
    "brand": {
      "id": 0,
      "name": "string",
      "country": "string",
      "logoUrl": "string",
      "description": "string",
      "laptops": [
        {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      ]
    },
    "category": {
      "id": 0,
      "name": "string",
      "description": "string",
      "laptops": [
        {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      ]
    },
    "variants": [
      {
        "id": 0,
        "laptopId": 0,
        "discountId": 0,
        "ram": 2147483647,
        "storage": 2147483647,
        "price": 0.1,
        "stockQuantity": 2147483647,
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        },
        "discount": {
          "id": 0,
          "title": "string",
          "description": "string",
          "percentage": 100,
          "startDate": "2019-08-24T14:15:22Z",
          "endDate": "2019-08-24T14:15:22Z",
          "isActive": true,
          "laptopVariants": [
            null
          ]
        },
        "orderItems": [
          {
            "id": null,
            "orderId": null,
            "order": null,
            "laptopVariantId": null,
            "laptopVariant": null,
            "quantity": null,
            "unitPrice": null
          }
        ],
        "cartItems": [
          {
            "id": null,
            "applicationUserId": null,
            "laptopVariantId": null,
            "quantity": null,
            "addedAt": null,
            "user": null,
            "laptopVariant": null
          }
        ]
      }
    ],
    "images": [
      {
        "id": 0,
        "laptopId": 0,
        "imageUrl": "string",
        "isMain": true,
        "uploadedAt": "2019-08-24T14:15:22Z",
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      }
    ],
    "ratings": [
      {
        "id": 0,
        "userId": "string",
        "laptopId": 0,
        "stars": 1,
        "comment": "string",
        "createdAt": "2019-08-24T14:15:22Z",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      }
    ],
    "repairRequests": [
      {
        "requestId": 0,
        "applicationUserId": "string",
        "itemId": 0,
        "laptopId": 0,
        "notes": "string",
        "status": 0,
        "requestDate": "2019-08-24T14:15:22Z",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "repairServiceItem": {
          "itemId": 0,
          "name": "string",
          "repairType": "[",
          "price": 0.1,
          "estimatedTime": "string",
          "repairRequests": [
            null
          ]
        },
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      }
    ]
  },
  "errors": [
    "string"
  ],
  "statusCode": 0,
  "timestamp": "2019-08-24T14:15:22Z"
}
```

### Responses

|HTTP Status Code |Meaning|Description|Data schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[LaptopServiceResponse](#schemalaptopserviceresponse)|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[LaptopServiceResponse](#schemalaptopserviceresponse)|
|404|[Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4)|Not Found|[LaptopServiceResponse](#schemalaptopserviceresponse)|
|409|[Conflict](https://tools.ietf.org/html/rfc7231#section-6.5.8)|Conflict|[LaptopServiceResponse](#schemalaptopserviceresponse)|
|500|[Internal Server Error](https://tools.ietf.org/html/rfc7231#section-6.6.1)|Internal Server Error|[LaptopServiceResponse](#schemalaptopserviceresponse)|

## DELETE /api/Laptop/{id}

DELETE /api/Laptop/{id}

### Params

|Name|Location|Type|Required|Description|
|---|---|---|---|---|
|id|path|integer(int32)| yes |none|

> Response Examples

> 200 Response

```json
{
  "isSuccess": true,
  "message": "string",
  "messageAr": "string",
  "data": true,
  "errors": [
    "string"
  ],
  "statusCode": 0,
  "timestamp": "2019-08-24T14:15:22Z"
}
```

### Responses

|HTTP Status Code |Meaning|Description|Data schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[BooleanServiceResponse](#schemabooleanserviceresponse)|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[BooleanServiceResponse](#schemabooleanserviceresponse)|
|404|[Not Found](https://tools.ietf.org/html/rfc7231#section-6.5.4)|Not Found|[BooleanServiceResponse](#schemabooleanserviceresponse)|
|500|[Internal Server Error](https://tools.ietf.org/html/rfc7231#section-6.6.1)|Internal Server Error|[BooleanServiceResponse](#schemabooleanserviceresponse)|

## GET /api/Laptop/search

GET /api/Laptop/search

### Params

|Name|Location|Type|Required|Description|
|---|---|---|---|---|
|searchTerm|query|string| no |none|

> Response Examples

> 200 Response

```json
{
  "isSuccess": true,
  "message": "string",
  "messageAr": "string",
  "data": [
    {
      "id": 0,
      "modelName": "string",
      "processor": "string",
      "gpu": "string",
      "screenSize": "string",
      "hasCamera": true,
      "hasKeyboard": true,
      "hasTouchScreen": true,
      "ports": "string",
      "description": "string",
      "notes": "string",
      "warranty": "string",
      "brandId": 0,
      "categoryId": 0,
      "brand": {
        "id": 0,
        "name": "string",
        "country": "string",
        "logoUrl": "string",
        "description": "string",
        "laptops": [
          {
            "id": null,
            "modelName": null,
            "processor": null,
            "gpu": null,
            "screenSize": null,
            "hasCamera": null,
            "hasKeyboard": null,
            "hasTouchScreen": null,
            "ports": null,
            "description": null,
            "notes": null,
            "warranty": null,
            "brandId": null,
            "categoryId": null,
            "brand": null,
            "category": null,
            "variants": null,
            "images": null,
            "ratings": null,
            "repairRequests": null
          }
        ]
      },
      "category": {
        "id": 0,
        "name": "string",
        "description": "string",
        "laptops": [
          {
            "id": null,
            "modelName": null,
            "processor": null,
            "gpu": null,
            "screenSize": null,
            "hasCamera": null,
            "hasKeyboard": null,
            "hasTouchScreen": null,
            "ports": null,
            "description": null,
            "notes": null,
            "warranty": null,
            "brandId": null,
            "categoryId": null,
            "brand": null,
            "category": null,
            "variants": null,
            "images": null,
            "ratings": null,
            "repairRequests": null
          }
        ]
      },
      "variants": [
        {
          "id": 0,
          "laptopId": 0,
          "discountId": 0,
          "ram": 2147483647,
          "storage": 2147483647,
          "price": 0.1,
          "stockQuantity": 2147483647,
          "laptop": {
            "id": null,
            "modelName": null,
            "processor": null,
            "gpu": null,
            "screenSize": null,
            "hasCamera": null,
            "hasKeyboard": null,
            "hasTouchScreen": null,
            "ports": null,
            "description": null,
            "notes": null,
            "warranty": null,
            "brandId": null,
            "categoryId": null,
            "brand": null,
            "category": null,
            "variants": null,
            "images": null,
            "ratings": null,
            "repairRequests": null
          },
          "discount": {
            "id": null,
            "title": null,
            "description": null,
            "percentage": null,
            "startDate": null,
            "endDate": null,
            "isActive": null,
            "laptopVariants": null
          },
          "orderItems": [
            {}
          ],
          "cartItems": [
            {}
          ]
        }
      ],
      "images": [
        {
          "id": 0,
          "laptopId": 0,
          "imageUrl": "string",
          "isMain": true,
          "uploadedAt": "2019-08-24T14:15:22Z",
          "laptop": {
            "id": null,
            "modelName": null,
            "processor": null,
            "gpu": null,
            "screenSize": null,
            "hasCamera": null,
            "hasKeyboard": null,
            "hasTouchScreen": null,
            "ports": null,
            "description": null,
            "notes": null,
            "warranty": null,
            "brandId": null,
            "categoryId": null,
            "brand": null,
            "category": null,
            "variants": null,
            "images": null,
            "ratings": null,
            "repairRequests": null
          }
        }
      ],
      "ratings": [
        {
          "id": 0,
          "userId": "string",
          "laptopId": 0,
          "stars": 1,
          "comment": "string",
          "createdAt": "2019-08-24T14:15:22Z",
          "user": {
            "id": null,
            "userName": null,
            "normalizedUserName": null,
            "email": null,
            "normalizedEmail": null,
            "emailConfirmed": null,
            "passwordHash": null,
            "securityStamp": null,
            "concurrencyStamp": null,
            "phoneNumber": null,
            "phoneNumberConfirmed": null,
            "twoFactorEnabled": null,
            "lockoutEnd": null,
            "lockoutEnabled": null,
            "accessFailedCount": null,
            "fullName": null,
            "phone": null,
            "role": null,
            "isActive": null,
            "createdAt": null,
            "orders": null,
            "ratings": null,
            "cartItems": null,
            "repairRequests": null,
            "auditLogs": null,
            "refreshTokens": null,
            "verificationCodes": null,
            "emailQueue": null
          },
          "laptop": {
            "id": null,
            "modelName": null,
            "processor": null,
            "gpu": null,
            "screenSize": null,
            "hasCamera": null,
            "hasKeyboard": null,
            "hasTouchScreen": null,
            "ports": null,
            "description": null,
            "notes": null,
            "warranty": null,
            "brandId": null,
            "categoryId": null,
            "brand": null,
            "category": null,
            "variants": null,
            "images": null,
            "ratings": null,
            "repairRequests": null
          }
        }
      ],
      "repairRequests": [
        {
          "requestId": 0,
          "applicationUserId": "string",
          "itemId": 0,
          "laptopId": 0,
          "notes": "string",
          "status": 0,
          "requestDate": "2019-08-24T14:15:22Z",
          "user": {
            "id": null,
            "userName": null,
            "normalizedUserName": null,
            "email": null,
            "normalizedEmail": null,
            "emailConfirmed": null,
            "passwordHash": null,
            "securityStamp": null,
            "concurrencyStamp": null,
            "phoneNumber": null,
            "phoneNumberConfirmed": null,
            "twoFactorEnabled": null,
            "lockoutEnd": null,
            "lockoutEnabled": null,
            "accessFailedCount": null,
            "fullName": null,
            "phone": null,
            "role": null,
            "isActive": null,
            "createdAt": null,
            "orders": null,
            "ratings": null,
            "cartItems": null,
            "repairRequests": null,
            "auditLogs": null,
            "refreshTokens": null,
            "verificationCodes": null,
            "emailQueue": null
          },
          "repairServiceItem": {
            "itemId": null,
            "name": null,
            "repairType": null,
            "price": null,
            "estimatedTime": null,
            "repairRequests": null
          },
          "laptop": {
            "id": null,
            "modelName": null,
            "processor": null,
            "gpu": null,
            "screenSize": null,
            "hasCamera": null,
            "hasKeyboard": null,
            "hasTouchScreen": null,
            "ports": null,
            "description": null,
            "notes": null,
            "warranty": null,
            "brandId": null,
            "categoryId": null,
            "brand": null,
            "category": null,
            "variants": null,
            "images": null,
            "ratings": null,
            "repairRequests": null
          }
        }
      ]
    }
  ],
  "errors": [
    "string"
  ],
  "statusCode": 0,
  "timestamp": "2019-08-24T14:15:22Z"
}
```

### Responses

|HTTP Status Code |Meaning|Description|Data schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[LaptopIEnumerableServiceResponse](#schemalaptopienumerableserviceresponse)|
|400|[Bad Request](https://tools.ietf.org/html/rfc7231#section-6.5.1)|Bad Request|[LaptopIEnumerableServiceResponse](#schemalaptopienumerableserviceresponse)|
|500|[Internal Server Error](https://tools.ietf.org/html/rfc7231#section-6.6.1)|Internal Server Error|[LaptopIEnumerableServiceResponse](#schemalaptopienumerableserviceresponse)|

## GET /api/Laptop/filter

GET /api/Laptop/filter

### Params

|Name|Location|Type|Required|Description|
|---|---|---|---|---|
|processor|query|string| no |none|
|gpu|query|string| no |none|
|minPrice|query|integer(int32)| no |none|
|maxPrice|query|integer(int32)| no |none|

> Response Examples

> 200 Response

```json
{
  "isSuccess": true,
  "message": "string",
  "messageAr": "string",
  "data": [
    {
      "id": 0,
      "modelName": "string",
      "processor": "string",
      "gpu": "string",
      "screenSize": "string",
      "hasCamera": true,
      "hasKeyboard": true,
      "hasTouchScreen": true,
      "ports": "string",
      "description": "string",
      "notes": "string",
      "warranty": "string",
      "brandId": 0,
      "categoryId": 0,
      "brand": {
        "id": 0,
        "name": "string",
        "country": "string",
        "logoUrl": "string",
        "description": "string",
        "laptops": [
          {
            "id": null,
            "modelName": null,
            "processor": null,
            "gpu": null,
            "screenSize": null,
            "hasCamera": null,
            "hasKeyboard": null,
            "hasTouchScreen": null,
            "ports": null,
            "description": null,
            "notes": null,
            "warranty": null,
            "brandId": null,
            "categoryId": null,
            "brand": null,
            "category": null,
            "variants": null,
            "images": null,
            "ratings": null,
            "repairRequests": null
          }
        ]
      },
      "category": {
        "id": 0,
        "name": "string",
        "description": "string",
        "laptops": [
          {
            "id": null,
            "modelName": null,
            "processor": null,
            "gpu": null,
            "screenSize": null,
            "hasCamera": null,
            "hasKeyboard": null,
            "hasTouchScreen": null,
            "ports": null,
            "description": null,
            "notes": null,
            "warranty": null,
            "brandId": null,
            "categoryId": null,
            "brand": null,
            "category": null,
            "variants": null,
            "images": null,
            "ratings": null,
            "repairRequests": null
          }
        ]
      },
      "variants": [
        {
          "id": 0,
          "laptopId": 0,
          "discountId": 0,
          "ram": 2147483647,
          "storage": 2147483647,
          "price": 0.1,
          "stockQuantity": 2147483647,
          "laptop": {
            "id": null,
            "modelName": null,
            "processor": null,
            "gpu": null,
            "screenSize": null,
            "hasCamera": null,
            "hasKeyboard": null,
            "hasTouchScreen": null,
            "ports": null,
            "description": null,
            "notes": null,
            "warranty": null,
            "brandId": null,
            "categoryId": null,
            "brand": null,
            "category": null,
            "variants": null,
            "images": null,
            "ratings": null,
            "repairRequests": null
          },
          "discount": {
            "id": null,
            "title": null,
            "description": null,
            "percentage": null,
            "startDate": null,
            "endDate": null,
            "isActive": null,
            "laptopVariants": null
          },
          "orderItems": [
            {}
          ],
          "cartItems": [
            {}
          ]
        }
      ],
      "images": [
        {
          "id": 0,
          "laptopId": 0,
          "imageUrl": "string",
          "isMain": true,
          "uploadedAt": "2019-08-24T14:15:22Z",
          "laptop": {
            "id": null,
            "modelName": null,
            "processor": null,
            "gpu": null,
            "screenSize": null,
            "hasCamera": null,
            "hasKeyboard": null,
            "hasTouchScreen": null,
            "ports": null,
            "description": null,
            "notes": null,
            "warranty": null,
            "brandId": null,
            "categoryId": null,
            "brand": null,
            "category": null,
            "variants": null,
            "images": null,
            "ratings": null,
            "repairRequests": null
          }
        }
      ],
      "ratings": [
        {
          "id": 0,
          "userId": "string",
          "laptopId": 0,
          "stars": 1,
          "comment": "string",
          "createdAt": "2019-08-24T14:15:22Z",
          "user": {
            "id": null,
            "userName": null,
            "normalizedUserName": null,
            "email": null,
            "normalizedEmail": null,
            "emailConfirmed": null,
            "passwordHash": null,
            "securityStamp": null,
            "concurrencyStamp": null,
            "phoneNumber": null,
            "phoneNumberConfirmed": null,
            "twoFactorEnabled": null,
            "lockoutEnd": null,
            "lockoutEnabled": null,
            "accessFailedCount": null,
            "fullName": null,
            "phone": null,
            "role": null,
            "isActive": null,
            "createdAt": null,
            "orders": null,
            "ratings": null,
            "cartItems": null,
            "repairRequests": null,
            "auditLogs": null,
            "refreshTokens": null,
            "verificationCodes": null,
            "emailQueue": null
          },
          "laptop": {
            "id": null,
            "modelName": null,
            "processor": null,
            "gpu": null,
            "screenSize": null,
            "hasCamera": null,
            "hasKeyboard": null,
            "hasTouchScreen": null,
            "ports": null,
            "description": null,
            "notes": null,
            "warranty": null,
            "brandId": null,
            "categoryId": null,
            "brand": null,
            "category": null,
            "variants": null,
            "images": null,
            "ratings": null,
            "repairRequests": null
          }
        }
      ],
      "repairRequests": [
        {
          "requestId": 0,
          "applicationUserId": "string",
          "itemId": 0,
          "laptopId": 0,
          "notes": "string",
          "status": 0,
          "requestDate": "2019-08-24T14:15:22Z",
          "user": {
            "id": null,
            "userName": null,
            "normalizedUserName": null,
            "email": null,
            "normalizedEmail": null,
            "emailConfirmed": null,
            "passwordHash": null,
            "securityStamp": null,
            "concurrencyStamp": null,
            "phoneNumber": null,
            "phoneNumberConfirmed": null,
            "twoFactorEnabled": null,
            "lockoutEnd": null,
            "lockoutEnabled": null,
            "accessFailedCount": null,
            "fullName": null,
            "phone": null,
            "role": null,
            "isActive": null,
            "createdAt": null,
            "orders": null,
            "ratings": null,
            "cartItems": null,
            "repairRequests": null,
            "auditLogs": null,
            "refreshTokens": null,
            "verificationCodes": null,
            "emailQueue": null
          },
          "repairServiceItem": {
            "itemId": null,
            "name": null,
            "repairType": null,
            "price": null,
            "estimatedTime": null,
            "repairRequests": null
          },
          "laptop": {
            "id": null,
            "modelName": null,
            "processor": null,
            "gpu": null,
            "screenSize": null,
            "hasCamera": null,
            "hasKeyboard": null,
            "hasTouchScreen": null,
            "ports": null,
            "description": null,
            "notes": null,
            "warranty": null,
            "brandId": null,
            "categoryId": null,
            "brand": null,
            "category": null,
            "variants": null,
            "images": null,
            "ratings": null,
            "repairRequests": null
          }
        }
      ]
    }
  ],
  "errors": [
    "string"
  ],
  "statusCode": 0,
  "timestamp": "2019-08-24T14:15:22Z"
}
```

### Responses

|HTTP Status Code |Meaning|Description|Data schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|[LaptopIEnumerableServiceResponse](#schemalaptopienumerableserviceresponse)|
|500|[Internal Server Error](https://tools.ietf.org/html/rfc7231#section-6.6.1)|Internal Server Error|[LaptopIEnumerableServiceResponse](#schemalaptopienumerableserviceresponse)|

# TechZone.Api

## GET /

GET /

### Responses

|HTTP Status Code |Meaning|Description|Data schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|OK|None|

# Data Schema

<h2 id="tocS_ApplicationUser">ApplicationUser</h2>

<a id="schemaapplicationuser"></a>
<a id="schema_ApplicationUser"></a>
<a id="tocSapplicationuser"></a>
<a id="tocsapplicationuser"></a>

```json
{
  "id": "string",
  "userName": "string",
  "normalizedUserName": "string",
  "email": "string",
  "normalizedEmail": "string",
  "emailConfirmed": true,
  "passwordHash": "string",
  "securityStamp": "string",
  "concurrencyStamp": "string",
  "phoneNumber": "string",
  "phoneNumberConfirmed": true,
  "twoFactorEnabled": true,
  "lockoutEnd": "2019-08-24T14:15:22Z",
  "lockoutEnabled": true,
  "accessFailedCount": 0,
  "fullName": "string",
  "phone": "string",
  "role": "string",
  "isActive": true,
  "createdAt": "2019-08-24T14:15:22Z",
  "orders": [
    {
      "id": 0,
      "orderDate": "2019-08-24T14:15:22Z",
      "totalAmount": 0.1,
      "status": 0,
      "userId": "string",
      "applicationUser": {
        "id": "string",
        "userName": "string",
        "normalizedUserName": "string",
        "email": "string",
        "normalizedEmail": "string",
        "emailConfirmed": true,
        "passwordHash": "string",
        "securityStamp": "string",
        "concurrencyStamp": "string",
        "phoneNumber": "string",
        "phoneNumberConfirmed": true,
        "twoFactorEnabled": true,
        "lockoutEnd": "2019-08-24T14:15:22Z",
        "lockoutEnabled": true,
        "accessFailedCount": 0,
        "fullName": "string",
        "phone": "string",
        "role": "string",
        "isActive": true,
        "createdAt": "2019-08-24T14:15:22Z",
        "orders": [
          {
            "id": null,
            "orderDate": null,
            "totalAmount": null,
            "status": null,
            "userId": null,
            "applicationUser": null,
            "items": null,
            "payments": null,
            "shipping": null
          }
        ],
        "ratings": [
          {
            "id": null,
            "userId": null,
            "laptopId": null,
            "stars": null,
            "comment": null,
            "createdAt": null,
            "user": null,
            "laptop": null
          }
        ],
        "cartItems": [
          {
            "id": null,
            "applicationUserId": null,
            "laptopVariantId": null,
            "quantity": null,
            "addedAt": null,
            "user": null,
            "laptopVariant": null
          }
        ],
        "repairRequests": [
          {
            "requestId": null,
            "applicationUserId": null,
            "itemId": null,
            "laptopId": null,
            "notes": null,
            "status": null,
            "requestDate": null,
            "user": null,
            "repairServiceItem": null,
            "laptop": null
          }
        ],
        "auditLogs": [
          {
            "id": null,
            "applicationUserId": null,
            "action": null,
            "entity": null,
            "entityId": null,
            "timestamp": null,
            "details": null,
            "user": null
          }
        ],
        "refreshTokens": [
          {
            "token": null,
            "expiresOn": null,
            "isExpired": null,
            "createdOn": null,
            "revokedOn": null,
            "isActive": null
          }
        ],
        "verificationCodes": [
          {
            "id": null,
            "userId": null,
            "code": null,
            "type": null,
            "destination": null,
            "isUsed": null,
            "createdAt": null,
            "expiryDate": null,
            "attemptCount": null,
            "maxAttempts": null,
            "user": null
          }
        ],
        "emailQueue": [
          {
            "id": null,
            "toEmail": null,
            "toName": null,
            "subject": null,
            "body": null,
            "isHtml": null,
            "emailType": null,
            "status": null,
            "retryCount": null,
            "maxRetries": null,
            "createdAt": null,
            "scheduledAt": null,
            "sentAt": null,
            "nextRetryAt": null,
            "errorMessage": null,
            "priority": null,
            "userId": null,
            "templateData": null,
            "user": null
          }
        ]
      },
      "items": [
        {
          "id": 0,
          "orderId": 0,
          "order": {
            "id": null,
            "orderDate": null,
            "totalAmount": null,
            "status": null,
            "userId": null,
            "applicationUser": null,
            "items": null,
            "payments": null,
            "shipping": null
          },
          "laptopVariantId": 0,
          "laptopVariant": {
            "id": null,
            "laptopId": null,
            "discountId": null,
            "ram": null,
            "storage": null,
            "price": null,
            "stockQuantity": null,
            "laptop": null,
            "discount": null,
            "orderItems": null,
            "cartItems": null
          },
          "quantity": 0,
          "unitPrice": 0.1
        }
      ],
      "payments": [
        {
          "id": 0,
          "orderId": 0,
          "paymentMethod": 0,
          "paymentStatus": 0,
          "transactionId": "string",
          "paidAt": "2019-08-24T14:15:22Z",
          "order": {
            "id": null,
            "orderDate": null,
            "totalAmount": null,
            "status": null,
            "userId": null,
            "applicationUser": null,
            "items": null,
            "payments": null,
            "shipping": null
          }
        }
      ],
      "shipping": {
        "id": 0,
        "orderId": 0,
        "address": "string",
        "city": "string",
        "country": "string",
        "postalCode": "string",
        "trackingNumber": "string",
        "shippedAt": "2019-08-24T14:15:22Z",
        "deliveredAt": "2019-08-24T14:15:22Z",
        "order": {
          "id": 0,
          "orderDate": "2019-08-24T14:15:22Z",
          "totalAmount": 0.1,
          "status": "[",
          "userId": "string",
          "applicationUser": {},
          "items": [
            null
          ],
          "payments": [
            null
          ],
          "shipping": {}
        }
      }
    }
  ],
  "ratings": [
    {
      "id": 0,
      "userId": "string",
      "laptopId": 0,
      "stars": 1,
      "comment": "string",
      "createdAt": "2019-08-24T14:15:22Z",
      "user": {
        "id": "string",
        "userName": "string",
        "normalizedUserName": "string",
        "email": "string",
        "normalizedEmail": "string",
        "emailConfirmed": true,
        "passwordHash": "string",
        "securityStamp": "string",
        "concurrencyStamp": "string",
        "phoneNumber": "string",
        "phoneNumberConfirmed": true,
        "twoFactorEnabled": true,
        "lockoutEnd": "2019-08-24T14:15:22Z",
        "lockoutEnabled": true,
        "accessFailedCount": 0,
        "fullName": "string",
        "phone": "string",
        "role": "string",
        "isActive": true,
        "createdAt": "2019-08-24T14:15:22Z",
        "orders": [
          {
            "id": null,
            "orderDate": null,
            "totalAmount": null,
            "status": null,
            "userId": null,
            "applicationUser": null,
            "items": null,
            "payments": null,
            "shipping": null
          }
        ],
        "ratings": [
          {
            "id": null,
            "userId": null,
            "laptopId": null,
            "stars": null,
            "comment": null,
            "createdAt": null,
            "user": null,
            "laptop": null
          }
        ],
        "cartItems": [
          {
            "id": null,
            "applicationUserId": null,
            "laptopVariantId": null,
            "quantity": null,
            "addedAt": null,
            "user": null,
            "laptopVariant": null
          }
        ],
        "repairRequests": [
          {
            "requestId": null,
            "applicationUserId": null,
            "itemId": null,
            "laptopId": null,
            "notes": null,
            "status": null,
            "requestDate": null,
            "user": null,
            "repairServiceItem": null,
            "laptop": null
          }
        ],
        "auditLogs": [
          {
            "id": null,
            "applicationUserId": null,
            "action": null,
            "entity": null,
            "entityId": null,
            "timestamp": null,
            "details": null,
            "user": null
          }
        ],
        "refreshTokens": [
          {
            "token": null,
            "expiresOn": null,
            "isExpired": null,
            "createdOn": null,
            "revokedOn": null,
            "isActive": null
          }
        ],
        "verificationCodes": [
          {
            "id": null,
            "userId": null,
            "code": null,
            "type": null,
            "destination": null,
            "isUsed": null,
            "createdAt": null,
            "expiryDate": null,
            "attemptCount": null,
            "maxAttempts": null,
            "user": null
          }
        ],
        "emailQueue": [
          {
            "id": null,
            "toEmail": null,
            "toName": null,
            "subject": null,
            "body": null,
            "isHtml": null,
            "emailType": null,
            "status": null,
            "retryCount": null,
            "maxRetries": null,
            "createdAt": null,
            "scheduledAt": null,
            "sentAt": null,
            "nextRetryAt": null,
            "errorMessage": null,
            "priority": null,
            "userId": null,
            "templateData": null,
            "user": null
          }
        ]
      },
      "laptop": {
        "id": 0,
        "modelName": "string",
        "processor": "string",
        "gpu": "string",
        "screenSize": "string",
        "hasCamera": true,
        "hasKeyboard": true,
        "hasTouchScreen": true,
        "ports": "string",
        "description": "string",
        "notes": "string",
        "warranty": "string",
        "brandId": 0,
        "categoryId": 0,
        "brand": {
          "id": 0,
          "name": "string",
          "country": "string",
          "logoUrl": "string",
          "description": "string",
          "laptops": [
            null
          ]
        },
        "category": {
          "id": 0,
          "name": "string",
          "description": "string",
          "laptops": [
            null
          ]
        },
        "variants": [
          {
            "id": null,
            "laptopId": null,
            "discountId": null,
            "ram": null,
            "storage": null,
            "price": null,
            "stockQuantity": null,
            "laptop": null,
            "discount": null,
            "orderItems": null,
            "cartItems": null
          }
        ],
        "images": [
          {
            "id": null,
            "laptopId": null,
            "imageUrl": null,
            "isMain": null,
            "uploadedAt": null,
            "laptop": null
          }
        ],
        "ratings": [
          {
            "id": null,
            "userId": null,
            "laptopId": null,
            "stars": null,
            "comment": null,
            "createdAt": null,
            "user": null,
            "laptop": null
          }
        ],
        "repairRequests": [
          {
            "requestId": null,
            "applicationUserId": null,
            "itemId": null,
            "laptopId": null,
            "notes": null,
            "status": null,
            "requestDate": null,
            "user": null,
            "repairServiceItem": null,
            "laptop": null
          }
        ]
      }
    }
  ],
  "cartItems": [
    {
      "id": 0,
      "applicationUserId": "string",
      "laptopVariantId": 0,
      "quantity": 1,
      "addedAt": "2019-08-24T14:15:22Z",
      "user": {
        "id": "string",
        "userName": "string",
        "normalizedUserName": "string",
        "email": "string",
        "normalizedEmail": "string",
        "emailConfirmed": true,
        "passwordHash": "string",
        "securityStamp": "string",
        "concurrencyStamp": "string",
        "phoneNumber": "string",
        "phoneNumberConfirmed": true,
        "twoFactorEnabled": true,
        "lockoutEnd": "2019-08-24T14:15:22Z",
        "lockoutEnabled": true,
        "accessFailedCount": 0,
        "fullName": "string",
        "phone": "string",
        "role": "string",
        "isActive": true,
        "createdAt": "2019-08-24T14:15:22Z",
        "orders": [
          {
            "id": null,
            "orderDate": null,
            "totalAmount": null,
            "status": null,
            "userId": null,
            "applicationUser": null,
            "items": null,
            "payments": null,
            "shipping": null
          }
        ],
        "ratings": [
          {
            "id": null,
            "userId": null,
            "laptopId": null,
            "stars": null,
            "comment": null,
            "createdAt": null,
            "user": null,
            "laptop": null
          }
        ],
        "cartItems": [
          {
            "id": null,
            "applicationUserId": null,
            "laptopVariantId": null,
            "quantity": null,
            "addedAt": null,
            "user": null,
            "laptopVariant": null
          }
        ],
        "repairRequests": [
          {
            "requestId": null,
            "applicationUserId": null,
            "itemId": null,
            "laptopId": null,
            "notes": null,
            "status": null,
            "requestDate": null,
            "user": null,
            "repairServiceItem": null,
            "laptop": null
          }
        ],
        "auditLogs": [
          {
            "id": null,
            "applicationUserId": null,
            "action": null,
            "entity": null,
            "entityId": null,
            "timestamp": null,
            "details": null,
            "user": null
          }
        ],
        "refreshTokens": [
          {
            "token": null,
            "expiresOn": null,
            "isExpired": null,
            "createdOn": null,
            "revokedOn": null,
            "isActive": null
          }
        ],
        "verificationCodes": [
          {
            "id": null,
            "userId": null,
            "code": null,
            "type": null,
            "destination": null,
            "isUsed": null,
            "createdAt": null,
            "expiryDate": null,
            "attemptCount": null,
            "maxAttempts": null,
            "user": null
          }
        ],
        "emailQueue": [
          {
            "id": null,
            "toEmail": null,
            "toName": null,
            "subject": null,
            "body": null,
            "isHtml": null,
            "emailType": null,
            "status": null,
            "retryCount": null,
            "maxRetries": null,
            "createdAt": null,
            "scheduledAt": null,
            "sentAt": null,
            "nextRetryAt": null,
            "errorMessage": null,
            "priority": null,
            "userId": null,
            "templateData": null,
            "user": null
          }
        ]
      },
      "laptopVariant": {
        "id": 0,
        "laptopId": 0,
        "discountId": 0,
        "ram": 2147483647,
        "storage": 2147483647,
        "price": 0.1,
        "stockQuantity": 2147483647,
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        },
        "discount": {
          "id": 0,
          "title": "string",
          "description": "string",
          "percentage": 100,
          "startDate": "2019-08-24T14:15:22Z",
          "endDate": "2019-08-24T14:15:22Z",
          "isActive": true,
          "laptopVariants": [
            null
          ]
        },
        "orderItems": [
          {
            "id": null,
            "orderId": null,
            "order": null,
            "laptopVariantId": null,
            "laptopVariant": null,
            "quantity": null,
            "unitPrice": null
          }
        ],
        "cartItems": [
          {
            "id": null,
            "applicationUserId": null,
            "laptopVariantId": null,
            "quantity": null,
            "addedAt": null,
            "user": null,
            "laptopVariant": null
          }
        ]
      }
    }
  ],
  "repairRequests": [
    {
      "requestId": 0,
      "applicationUserId": "string",
      "itemId": 0,
      "laptopId": 0,
      "notes": "string",
      "status": 0,
      "requestDate": "2019-08-24T14:15:22Z",
      "user": {
        "id": "string",
        "userName": "string",
        "normalizedUserName": "string",
        "email": "string",
        "normalizedEmail": "string",
        "emailConfirmed": true,
        "passwordHash": "string",
        "securityStamp": "string",
        "concurrencyStamp": "string",
        "phoneNumber": "string",
        "phoneNumberConfirmed": true,
        "twoFactorEnabled": true,
        "lockoutEnd": "2019-08-24T14:15:22Z",
        "lockoutEnabled": true,
        "accessFailedCount": 0,
        "fullName": "string",
        "phone": "string",
        "role": "string",
        "isActive": true,
        "createdAt": "2019-08-24T14:15:22Z",
        "orders": [
          {
            "id": null,
            "orderDate": null,
            "totalAmount": null,
            "status": null,
            "userId": null,
            "applicationUser": null,
            "items": null,
            "payments": null,
            "shipping": null
          }
        ],
        "ratings": [
          {
            "id": null,
            "userId": null,
            "laptopId": null,
            "stars": null,
            "comment": null,
            "createdAt": null,
            "user": null,
            "laptop": null
          }
        ],
        "cartItems": [
          {
            "id": null,
            "applicationUserId": null,
            "laptopVariantId": null,
            "quantity": null,
            "addedAt": null,
            "user": null,
            "laptopVariant": null
          }
        ],
        "repairRequests": [
          {
            "requestId": null,
            "applicationUserId": null,
            "itemId": null,
            "laptopId": null,
            "notes": null,
            "status": null,
            "requestDate": null,
            "user": null,
            "repairServiceItem": null,
            "laptop": null
          }
        ],
        "auditLogs": [
          {
            "id": null,
            "applicationUserId": null,
            "action": null,
            "entity": null,
            "entityId": null,
            "timestamp": null,
            "details": null,
            "user": null
          }
        ],
        "refreshTokens": [
          {
            "token": null,
            "expiresOn": null,
            "isExpired": null,
            "createdOn": null,
            "revokedOn": null,
            "isActive": null
          }
        ],
        "verificationCodes": [
          {
            "id": null,
            "userId": null,
            "code": null,
            "type": null,
            "destination": null,
            "isUsed": null,
            "createdAt": null,
            "expiryDate": null,
            "attemptCount": null,
            "maxAttempts": null,
            "user": null
          }
        ],
        "emailQueue": [
          {
            "id": null,
            "toEmail": null,
            "toName": null,
            "subject": null,
            "body": null,
            "isHtml": null,
            "emailType": null,
            "status": null,
            "retryCount": null,
            "maxRetries": null,
            "createdAt": null,
            "scheduledAt": null,
            "sentAt": null,
            "nextRetryAt": null,
            "errorMessage": null,
            "priority": null,
            "userId": null,
            "templateData": null,
            "user": null
          }
        ]
      },
      "repairServiceItem": {
        "itemId": 0,
        "name": "string",
        "repairType": 0,
        "price": 0.1,
        "estimatedTime": "string",
        "repairRequests": [
          {
            "requestId": null,
            "applicationUserId": null,
            "itemId": null,
            "laptopId": null,
            "notes": null,
            "status": null,
            "requestDate": null,
            "user": null,
            "repairServiceItem": null,
            "laptop": null
          }
        ]
      },
      "laptop": {
        "id": 0,
        "modelName": "string",
        "processor": "string",
        "gpu": "string",
        "screenSize": "string",
        "hasCamera": true,
        "hasKeyboard": true,
        "hasTouchScreen": true,
        "ports": "string",
        "description": "string",
        "notes": "string",
        "warranty": "string",
        "brandId": 0,
        "categoryId": 0,
        "brand": {
          "id": 0,
          "name": "string",
          "country": "string",
          "logoUrl": "string",
          "description": "string",
          "laptops": [
            null
          ]
        },
        "category": {
          "id": 0,
          "name": "string",
          "description": "string",
          "laptops": [
            null
          ]
        },
        "variants": [
          {
            "id": null,
            "laptopId": null,
            "discountId": null,
            "ram": null,
            "storage": null,
            "price": null,
            "stockQuantity": null,
            "laptop": null,
            "discount": null,
            "orderItems": null,
            "cartItems": null
          }
        ],
        "images": [
          {
            "id": null,
            "laptopId": null,
            "imageUrl": null,
            "isMain": null,
            "uploadedAt": null,
            "laptop": null
          }
        ],
        "ratings": [
          {
            "id": null,
            "userId": null,
            "laptopId": null,
            "stars": null,
            "comment": null,
            "createdAt": null,
            "user": null,
            "laptop": null
          }
        ],
        "repairRequests": [
          {
            "requestId": null,
            "applicationUserId": null,
            "itemId": null,
            "laptopId": null,
            "notes": null,
            "status": null,
            "requestDate": null,
            "user": null,
            "repairServiceItem": null,
            "laptop": null
          }
        ]
      }
    }
  ],
  "auditLogs": [
    {
      "id": 0,
      "applicationUserId": "string",
      "action": "string",
      "entity": "string",
      "entityId": 0,
      "timestamp": "2019-08-24T14:15:22Z",
      "details": "string",
      "user": {
        "id": "string",
        "userName": "string",
        "normalizedUserName": "string",
        "email": "string",
        "normalizedEmail": "string",
        "emailConfirmed": true,
        "passwordHash": "string",
        "securityStamp": "string",
        "concurrencyStamp": "string",
        "phoneNumber": "string",
        "phoneNumberConfirmed": true,
        "twoFactorEnabled": true,
        "lockoutEnd": "2019-08-24T14:15:22Z",
        "lockoutEnabled": true,
        "accessFailedCount": 0,
        "fullName": "string",
        "phone": "string",
        "role": "string",
        "isActive": true,
        "createdAt": "2019-08-24T14:15:22Z",
        "orders": [
          {
            "id": null,
            "orderDate": null,
            "totalAmount": null,
            "status": null,
            "userId": null,
            "applicationUser": null,
            "items": null,
            "payments": null,
            "shipping": null
          }
        ],
        "ratings": [
          {
            "id": null,
            "userId": null,
            "laptopId": null,
            "stars": null,
            "comment": null,
            "createdAt": null,
            "user": null,
            "laptop": null
          }
        ],
        "cartItems": [
          {
            "id": null,
            "applicationUserId": null,
            "laptopVariantId": null,
            "quantity": null,
            "addedAt": null,
            "user": null,
            "laptopVariant": null
          }
        ],
        "repairRequests": [
          {
            "requestId": null,
            "applicationUserId": null,
            "itemId": null,
            "laptopId": null,
            "notes": null,
            "status": null,
            "requestDate": null,
            "user": null,
            "repairServiceItem": null,
            "laptop": null
          }
        ],
        "auditLogs": [
          {
            "id": null,
            "applicationUserId": null,
            "action": null,
            "entity": null,
            "entityId": null,
            "timestamp": null,
            "details": null,
            "user": null
          }
        ],
        "refreshTokens": [
          {
            "token": null,
            "expiresOn": null,
            "isExpired": null,
            "createdOn": null,
            "revokedOn": null,
            "isActive": null
          }
        ],
        "verificationCodes": [
          {
            "id": null,
            "userId": null,
            "code": null,
            "type": null,
            "destination": null,
            "isUsed": null,
            "createdAt": null,
            "expiryDate": null,
            "attemptCount": null,
            "maxAttempts": null,
            "user": null
          }
        ],
        "emailQueue": [
          {
            "id": null,
            "toEmail": null,
            "toName": null,
            "subject": null,
            "body": null,
            "isHtml": null,
            "emailType": null,
            "status": null,
            "retryCount": null,
            "maxRetries": null,
            "createdAt": null,
            "scheduledAt": null,
            "sentAt": null,
            "nextRetryAt": null,
            "errorMessage": null,
            "priority": null,
            "userId": null,
            "templateData": null,
            "user": null
          }
        ]
      }
    }
  ],
  "refreshTokens": [
    {
      "token": "string",
      "expiresOn": "2019-08-24T14:15:22Z",
      "isExpired": true,
      "createdOn": "2019-08-24T14:15:22Z",
      "revokedOn": "2019-08-24T14:15:22Z",
      "isActive": true
    }
  ],
  "verificationCodes": [
    {
      "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
      "userId": "string",
      "code": "string",
      "type": 0,
      "destination": 0,
      "isUsed": true,
      "createdAt": "2019-08-24T14:15:22Z",
      "expiryDate": "2019-08-24T14:15:22Z",
      "attemptCount": 10,
      "maxAttempts": 1,
      "user": {
        "id": "string",
        "userName": "string",
        "normalizedUserName": "string",
        "email": "string",
        "normalizedEmail": "string",
        "emailConfirmed": true,
        "passwordHash": "string",
        "securityStamp": "string",
        "concurrencyStamp": "string",
        "phoneNumber": "string",
        "phoneNumberConfirmed": true,
        "twoFactorEnabled": true,
        "lockoutEnd": "2019-08-24T14:15:22Z",
        "lockoutEnabled": true,
        "accessFailedCount": 0,
        "fullName": "string",
        "phone": "string",
        "role": "string",
        "isActive": true,
        "createdAt": "2019-08-24T14:15:22Z",
        "orders": [
          {
            "id": null,
            "orderDate": null,
            "totalAmount": null,
            "status": null,
            "userId": null,
            "applicationUser": null,
            "items": null,
            "payments": null,
            "shipping": null
          }
        ],
        "ratings": [
          {
            "id": null,
            "userId": null,
            "laptopId": null,
            "stars": null,
            "comment": null,
            "createdAt": null,
            "user": null,
            "laptop": null
          }
        ],
        "cartItems": [
          {
            "id": null,
            "applicationUserId": null,
            "laptopVariantId": null,
            "quantity": null,
            "addedAt": null,
            "user": null,
            "laptopVariant": null
          }
        ],
        "repairRequests": [
          {
            "requestId": null,
            "applicationUserId": null,
            "itemId": null,
            "laptopId": null,
            "notes": null,
            "status": null,
            "requestDate": null,
            "user": null,
            "repairServiceItem": null,
            "laptop": null
          }
        ],
        "auditLogs": [
          {
            "id": null,
            "applicationUserId": null,
            "action": null,
            "entity": null,
            "entityId": null,
            "timestamp": null,
            "details": null,
            "user": null
          }
        ],
        "refreshTokens": [
          {
            "token": null,
            "expiresOn": null,
            "isExpired": null,
            "createdOn": null,
            "revokedOn": null,
            "isActive": null
          }
        ],
        "verificationCodes": [
          {
            "id": null,
            "userId": null,
            "code": null,
            "type": null,
            "destination": null,
            "isUsed": null,
            "createdAt": null,
            "expiryDate": null,
            "attemptCount": null,
            "maxAttempts": null,
            "user": null
          }
        ],
        "emailQueue": [
          {
            "id": null,
            "toEmail": null,
            "toName": null,
            "subject": null,
            "body": null,
            "isHtml": null,
            "emailType": null,
            "status": null,
            "retryCount": null,
            "maxRetries": null,
            "createdAt": null,
            "scheduledAt": null,
            "sentAt": null,
            "nextRetryAt": null,
            "errorMessage": null,
            "priority": null,
            "userId": null,
            "templateData": null,
            "user": null
          }
        ]
      }
    }
  ],
  "emailQueue": [
    {
      "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
      "toEmail": "string",
      "toName": "string",
      "subject": "string",
      "body": "string",
      "isHtml": true,
      "emailType": 0,
      "status": 0,
      "retryCount": 0,
      "maxRetries": 0,
      "createdAt": "2019-08-24T14:15:22Z",
      "scheduledAt": "2019-08-24T14:15:22Z",
      "sentAt": "2019-08-24T14:15:22Z",
      "nextRetryAt": "2019-08-24T14:15:22Z",
      "errorMessage": "string",
      "priority": 0,
      "userId": "string",
      "templateData": "string",
      "user": {
        "id": "string",
        "userName": "string",
        "normalizedUserName": "string",
        "email": "string",
        "normalizedEmail": "string",
        "emailConfirmed": true,
        "passwordHash": "string",
        "securityStamp": "string",
        "concurrencyStamp": "string",
        "phoneNumber": "string",
        "phoneNumberConfirmed": true,
        "twoFactorEnabled": true,
        "lockoutEnd": "2019-08-24T14:15:22Z",
        "lockoutEnabled": true,
        "accessFailedCount": 0,
        "fullName": "string",
        "phone": "string",
        "role": "string",
        "isActive": true,
        "createdAt": "2019-08-24T14:15:22Z",
        "orders": [
          {
            "id": null,
            "orderDate": null,
            "totalAmount": null,
            "status": null,
            "userId": null,
            "applicationUser": null,
            "items": null,
            "payments": null,
            "shipping": null
          }
        ],
        "ratings": [
          {
            "id": null,
            "userId": null,
            "laptopId": null,
            "stars": null,
            "comment": null,
            "createdAt": null,
            "user": null,
            "laptop": null
          }
        ],
        "cartItems": [
          {
            "id": null,
            "applicationUserId": null,
            "laptopVariantId": null,
            "quantity": null,
            "addedAt": null,
            "user": null,
            "laptopVariant": null
          }
        ],
        "repairRequests": [
          {
            "requestId": null,
            "applicationUserId": null,
            "itemId": null,
            "laptopId": null,
            "notes": null,
            "status": null,
            "requestDate": null,
            "user": null,
            "repairServiceItem": null,
            "laptop": null
          }
        ],
        "auditLogs": [
          {
            "id": null,
            "applicationUserId": null,
            "action": null,
            "entity": null,
            "entityId": null,
            "timestamp": null,
            "details": null,
            "user": null
          }
        ],
        "refreshTokens": [
          {
            "token": null,
            "expiresOn": null,
            "isExpired": null,
            "createdOn": null,
            "revokedOn": null,
            "isActive": null
          }
        ],
        "verificationCodes": [
          {
            "id": null,
            "userId": null,
            "code": null,
            "type": null,
            "destination": null,
            "isUsed": null,
            "createdAt": null,
            "expiryDate": null,
            "attemptCount": null,
            "maxAttempts": null,
            "user": null
          }
        ],
        "emailQueue": [
          {
            "id": null,
            "toEmail": null,
            "toName": null,
            "subject": null,
            "body": null,
            "isHtml": null,
            "emailType": null,
            "status": null,
            "retryCount": null,
            "maxRetries": null,
            "createdAt": null,
            "scheduledAt": null,
            "sentAt": null,
            "nextRetryAt": null,
            "errorMessage": null,
            "priority": null,
            "userId": null,
            "templateData": null,
            "user": null
          }
        ]
      }
    }
  ]
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|id|string¦null|false|none||none|
|userName|string¦null|false|none||none|
|normalizedUserName|string¦null|false|none||none|
|email|string¦null|false|none||none|
|normalizedEmail|string¦null|false|none||none|
|emailConfirmed|boolean|false|none||none|
|passwordHash|string¦null|false|none||none|
|securityStamp|string¦null|false|none||none|
|concurrencyStamp|string¦null|false|none||none|
|phoneNumber|string¦null|false|none||none|
|phoneNumberConfirmed|boolean|false|none||none|
|twoFactorEnabled|boolean|false|none||none|
|lockoutEnd|string(date-time)¦null|false|none||none|
|lockoutEnabled|boolean|false|none||none|
|accessFailedCount|integer(int32)|false|none||none|
|fullName|string¦null|false|none||none|
|phone|string¦null|false|none||none|
|role|string|true|none||none|
|isActive|boolean|false|none||none|
|createdAt|string(date-time)|false|none||none|
|orders|[[Order](#schemaorder)]¦null|false|none||none|
|ratings|[[Rating](#schemarating)]¦null|false|none||none|
|cartItems|[[CartItem](#schemacartitem)]¦null|false|none||none|
|repairRequests|[[RepairRequest](#schemarepairrequest)]¦null|false|none||none|
|auditLogs|[[AuditLog](#schemaauditlog)]¦null|false|none||none|
|refreshTokens|[[RefreshToken](#schemarefreshtoken)]¦null|false|none||none|
|verificationCodes|[[VerificationCode](#schemaverificationcode)]¦null|false|none||none|
|emailQueue|[[EmailQueue](#schemaemailqueue)]¦null|false|none||none|

<h2 id="tocS_AuditLog">AuditLog</h2>

<a id="schemaauditlog"></a>
<a id="schema_AuditLog"></a>
<a id="tocSauditlog"></a>
<a id="tocsauditlog"></a>

```json
{
  "id": 0,
  "applicationUserId": "string",
  "action": "string",
  "entity": "string",
  "entityId": 0,
  "timestamp": "2019-08-24T14:15:22Z",
  "details": "string",
  "user": {
    "id": "string",
    "userName": "string",
    "normalizedUserName": "string",
    "email": "string",
    "normalizedEmail": "string",
    "emailConfirmed": true,
    "passwordHash": "string",
    "securityStamp": "string",
    "concurrencyStamp": "string",
    "phoneNumber": "string",
    "phoneNumberConfirmed": true,
    "twoFactorEnabled": true,
    "lockoutEnd": "2019-08-24T14:15:22Z",
    "lockoutEnabled": true,
    "accessFailedCount": 0,
    "fullName": "string",
    "phone": "string",
    "role": "string",
    "isActive": true,
    "createdAt": "2019-08-24T14:15:22Z",
    "orders": [
      {
        "id": 0,
        "orderDate": "2019-08-24T14:15:22Z",
        "totalAmount": 0.1,
        "status": 0,
        "userId": "string",
        "applicationUser": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "items": [
          {
            "id": null,
            "orderId": null,
            "order": null,
            "laptopVariantId": null,
            "laptopVariant": null,
            "quantity": null,
            "unitPrice": null
          }
        ],
        "payments": [
          {
            "id": null,
            "orderId": null,
            "paymentMethod": null,
            "paymentStatus": null,
            "transactionId": null,
            "paidAt": null,
            "order": null
          }
        ],
        "shipping": {
          "id": 0,
          "orderId": 0,
          "address": "string",
          "city": "string",
          "country": "string",
          "postalCode": "string",
          "trackingNumber": "string",
          "shippedAt": "2019-08-24T14:15:22Z",
          "deliveredAt": "2019-08-24T14:15:22Z",
          "order": {}
        }
      }
    ],
    "ratings": [
      {
        "id": 0,
        "userId": "string",
        "laptopId": 0,
        "stars": 1,
        "comment": "string",
        "createdAt": "2019-08-24T14:15:22Z",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      }
    ],
    "cartItems": [
      {
        "id": 0,
        "applicationUserId": "string",
        "laptopVariantId": 0,
        "quantity": 1,
        "addedAt": "2019-08-24T14:15:22Z",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "laptopVariant": {
          "id": 0,
          "laptopId": 0,
          "discountId": 0,
          "ram": 2147483647,
          "storage": 2147483647,
          "price": 0.1,
          "stockQuantity": 2147483647,
          "laptop": {},
          "discount": {},
          "orderItems": [
            null
          ],
          "cartItems": [
            null
          ]
        }
      }
    ],
    "repairRequests": [
      {
        "requestId": 0,
        "applicationUserId": "string",
        "itemId": 0,
        "laptopId": 0,
        "notes": "string",
        "status": 0,
        "requestDate": "2019-08-24T14:15:22Z",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "repairServiceItem": {
          "itemId": 0,
          "name": "string",
          "repairType": "[",
          "price": 0.1,
          "estimatedTime": "string",
          "repairRequests": [
            null
          ]
        },
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      }
    ],
    "auditLogs": [
      {
        "id": 0,
        "applicationUserId": "string",
        "action": "string",
        "entity": "string",
        "entityId": 0,
        "timestamp": "2019-08-24T14:15:22Z",
        "details": "string",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        }
      }
    ],
    "refreshTokens": [
      {
        "token": "string",
        "expiresOn": "2019-08-24T14:15:22Z",
        "isExpired": true,
        "createdOn": "2019-08-24T14:15:22Z",
        "revokedOn": "2019-08-24T14:15:22Z",
        "isActive": true
      }
    ],
    "verificationCodes": [
      {
        "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
        "userId": "string",
        "code": "string",
        "type": 0,
        "destination": 0,
        "isUsed": true,
        "createdAt": "2019-08-24T14:15:22Z",
        "expiryDate": "2019-08-24T14:15:22Z",
        "attemptCount": 10,
        "maxAttempts": 1,
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        }
      }
    ],
    "emailQueue": [
      {
        "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
        "toEmail": "string",
        "toName": "string",
        "subject": "string",
        "body": "string",
        "isHtml": true,
        "emailType": 0,
        "status": 0,
        "retryCount": 0,
        "maxRetries": 0,
        "createdAt": "2019-08-24T14:15:22Z",
        "scheduledAt": "2019-08-24T14:15:22Z",
        "sentAt": "2019-08-24T14:15:22Z",
        "nextRetryAt": "2019-08-24T14:15:22Z",
        "errorMessage": "string",
        "priority": 0,
        "userId": "string",
        "templateData": "string",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        }
      }
    ]
  }
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|id|integer(int32)|false|none||none|
|applicationUserId|string¦null|false|none||none|
|action|string|true|none||none|
|entity|string|true|none||none|
|entityId|integer(int32)|false|none||none|
|timestamp|string(date-time)|false|none||none|
|details|string¦null|false|none||none|
|user|[ApplicationUser](#schemaapplicationuser)|false|none||none|

<h2 id="tocS_AuthDto">AuthDto</h2>

<a id="schemaauthdto"></a>
<a id="schema_AuthDto"></a>
<a id="tocSauthdto"></a>
<a id="tocsauthdto"></a>

```json
{
  "message": "string",
  "isAuthenticated": true,
  "username": "string",
  "email": "string",
  "roles": [
    "string"
  ],
  "token": "string",
  "emailConfirmed": true,
  "refreshTokenExpiration": "2019-08-24T14:15:22Z"
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|message|string¦null|false|none||none|
|isAuthenticated|boolean|false|none||none|
|username|string¦null|false|none||none|
|email|string¦null|false|none||none|
|roles|[string]¦null|false|none||none|
|token|string¦null|false|none||none|
|emailConfirmed|boolean|false|none||none|
|refreshTokenExpiration|string(date-time)|false|none||none|

<h2 id="tocS_AuthDtoServiceResponse">AuthDtoServiceResponse</h2>

<a id="schemaauthdtoserviceresponse"></a>
<a id="schema_AuthDtoServiceResponse"></a>
<a id="tocSauthdtoserviceresponse"></a>
<a id="tocsauthdtoserviceresponse"></a>

```json
{
  "isSuccess": true,
  "message": "string",
  "messageAr": "string",
  "data": {
    "message": "string",
    "isAuthenticated": true,
    "username": "string",
    "email": "string",
    "roles": [
      "string"
    ],
    "token": "string",
    "emailConfirmed": true,
    "refreshTokenExpiration": "2019-08-24T14:15:22Z"
  },
  "errors": [
    "string"
  ],
  "statusCode": 0,
  "timestamp": "2019-08-24T14:15:22Z"
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|isSuccess|boolean|false|none||none|
|message|string¦null|false|none||none|
|messageAr|string¦null|false|none||none|
|data|[AuthDto](#schemaauthdto)|false|none||none|
|errors|[string]¦null|false|none||none|
|statusCode|integer(int32)|false|none||none|
|timestamp|string(date-time)|false|none||none|

<h2 id="tocS_BooleanServiceResponse">BooleanServiceResponse</h2>

<a id="schemabooleanserviceresponse"></a>
<a id="schema_BooleanServiceResponse"></a>
<a id="tocSbooleanserviceresponse"></a>
<a id="tocsbooleanserviceresponse"></a>

```json
{
  "isSuccess": true,
  "message": "string",
  "messageAr": "string",
  "data": true,
  "errors": [
    "string"
  ],
  "statusCode": 0,
  "timestamp": "2019-08-24T14:15:22Z"
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|isSuccess|boolean|false|none||none|
|message|string¦null|false|none||none|
|messageAr|string¦null|false|none||none|
|data|boolean|false|none||none|
|errors|[string]¦null|false|none||none|
|statusCode|integer(int32)|false|none||none|
|timestamp|string(date-time)|false|none||none|

<h2 id="tocS_Brand">Brand</h2>

<a id="schemabrand"></a>
<a id="schema_Brand"></a>
<a id="tocSbrand"></a>
<a id="tocsbrand"></a>

```json
{
  "id": 0,
  "name": "string",
  "country": "string",
  "logoUrl": "string",
  "description": "string",
  "laptops": [
    {
      "id": 0,
      "modelName": "string",
      "processor": "string",
      "gpu": "string",
      "screenSize": "string",
      "hasCamera": true,
      "hasKeyboard": true,
      "hasTouchScreen": true,
      "ports": "string",
      "description": "string",
      "notes": "string",
      "warranty": "string",
      "brandId": 0,
      "categoryId": 0,
      "brand": {
        "id": 0,
        "name": "string",
        "country": "string",
        "logoUrl": "string",
        "description": "string",
        "laptops": [
          {
            "id": null,
            "modelName": null,
            "processor": null,
            "gpu": null,
            "screenSize": null,
            "hasCamera": null,
            "hasKeyboard": null,
            "hasTouchScreen": null,
            "ports": null,
            "description": null,
            "notes": null,
            "warranty": null,
            "brandId": null,
            "categoryId": null,
            "brand": null,
            "category": null,
            "variants": null,
            "images": null,
            "ratings": null,
            "repairRequests": null
          }
        ]
      },
      "category": {
        "id": 0,
        "name": "string",
        "description": "string",
        "laptops": [
          {
            "id": null,
            "modelName": null,
            "processor": null,
            "gpu": null,
            "screenSize": null,
            "hasCamera": null,
            "hasKeyboard": null,
            "hasTouchScreen": null,
            "ports": null,
            "description": null,
            "notes": null,
            "warranty": null,
            "brandId": null,
            "categoryId": null,
            "brand": null,
            "category": null,
            "variants": null,
            "images": null,
            "ratings": null,
            "repairRequests": null
          }
        ]
      },
      "variants": [
        {
          "id": 0,
          "laptopId": 0,
          "discountId": 0,
          "ram": 2147483647,
          "storage": 2147483647,
          "price": 0.1,
          "stockQuantity": 2147483647,
          "laptop": {
            "id": null,
            "modelName": null,
            "processor": null,
            "gpu": null,
            "screenSize": null,
            "hasCamera": null,
            "hasKeyboard": null,
            "hasTouchScreen": null,
            "ports": null,
            "description": null,
            "notes": null,
            "warranty": null,
            "brandId": null,
            "categoryId": null,
            "brand": null,
            "category": null,
            "variants": null,
            "images": null,
            "ratings": null,
            "repairRequests": null
          },
          "discount": {
            "id": null,
            "title": null,
            "description": null,
            "percentage": null,
            "startDate": null,
            "endDate": null,
            "isActive": null,
            "laptopVariants": null
          },
          "orderItems": [
            {}
          ],
          "cartItems": [
            {}
          ]
        }
      ],
      "images": [
        {
          "id": 0,
          "laptopId": 0,
          "imageUrl": "string",
          "isMain": true,
          "uploadedAt": "2019-08-24T14:15:22Z",
          "laptop": {
            "id": null,
            "modelName": null,
            "processor": null,
            "gpu": null,
            "screenSize": null,
            "hasCamera": null,
            "hasKeyboard": null,
            "hasTouchScreen": null,
            "ports": null,
            "description": null,
            "notes": null,
            "warranty": null,
            "brandId": null,
            "categoryId": null,
            "brand": null,
            "category": null,
            "variants": null,
            "images": null,
            "ratings": null,
            "repairRequests": null
          }
        }
      ],
      "ratings": [
        {
          "id": 0,
          "userId": "string",
          "laptopId": 0,
          "stars": 1,
          "comment": "string",
          "createdAt": "2019-08-24T14:15:22Z",
          "user": {
            "id": null,
            "userName": null,
            "normalizedUserName": null,
            "email": null,
            "normalizedEmail": null,
            "emailConfirmed": null,
            "passwordHash": null,
            "securityStamp": null,
            "concurrencyStamp": null,
            "phoneNumber": null,
            "phoneNumberConfirmed": null,
            "twoFactorEnabled": null,
            "lockoutEnd": null,
            "lockoutEnabled": null,
            "accessFailedCount": null,
            "fullName": null,
            "phone": null,
            "role": null,
            "isActive": null,
            "createdAt": null,
            "orders": null,
            "ratings": null,
            "cartItems": null,
            "repairRequests": null,
            "auditLogs": null,
            "refreshTokens": null,
            "verificationCodes": null,
            "emailQueue": null
          },
          "laptop": {
            "id": null,
            "modelName": null,
            "processor": null,
            "gpu": null,
            "screenSize": null,
            "hasCamera": null,
            "hasKeyboard": null,
            "hasTouchScreen": null,
            "ports": null,
            "description": null,
            "notes": null,
            "warranty": null,
            "brandId": null,
            "categoryId": null,
            "brand": null,
            "category": null,
            "variants": null,
            "images": null,
            "ratings": null,
            "repairRequests": null
          }
        }
      ],
      "repairRequests": [
        {
          "requestId": 0,
          "applicationUserId": "string",
          "itemId": 0,
          "laptopId": 0,
          "notes": "string",
          "status": 0,
          "requestDate": "2019-08-24T14:15:22Z",
          "user": {
            "id": null,
            "userName": null,
            "normalizedUserName": null,
            "email": null,
            "normalizedEmail": null,
            "emailConfirmed": null,
            "passwordHash": null,
            "securityStamp": null,
            "concurrencyStamp": null,
            "phoneNumber": null,
            "phoneNumberConfirmed": null,
            "twoFactorEnabled": null,
            "lockoutEnd": null,
            "lockoutEnabled": null,
            "accessFailedCount": null,
            "fullName": null,
            "phone": null,
            "role": null,
            "isActive": null,
            "createdAt": null,
            "orders": null,
            "ratings": null,
            "cartItems": null,
            "repairRequests": null,
            "auditLogs": null,
            "refreshTokens": null,
            "verificationCodes": null,
            "emailQueue": null
          },
          "repairServiceItem": {
            "itemId": null,
            "name": null,
            "repairType": null,
            "price": null,
            "estimatedTime": null,
            "repairRequests": null
          },
          "laptop": {
            "id": null,
            "modelName": null,
            "processor": null,
            "gpu": null,
            "screenSize": null,
            "hasCamera": null,
            "hasKeyboard": null,
            "hasTouchScreen": null,
            "ports": null,
            "description": null,
            "notes": null,
            "warranty": null,
            "brandId": null,
            "categoryId": null,
            "brand": null,
            "category": null,
            "variants": null,
            "images": null,
            "ratings": null,
            "repairRequests": null
          }
        }
      ]
    }
  ]
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|id|integer(int32)|false|none||none|
|name|string|true|none||none|
|country|string¦null|false|none||none|
|logoUrl|string¦null|false|none||none|
|description|string¦null|false|none||none|
|laptops|[[Laptop](#schemalaptop)]¦null|false|none||none|

<h2 id="tocS_CartItem">CartItem</h2>

<a id="schemacartitem"></a>
<a id="schema_CartItem"></a>
<a id="tocScartitem"></a>
<a id="tocscartitem"></a>

```json
{
  "id": 0,
  "applicationUserId": "string",
  "laptopVariantId": 0,
  "quantity": 1,
  "addedAt": "2019-08-24T14:15:22Z",
  "user": {
    "id": "string",
    "userName": "string",
    "normalizedUserName": "string",
    "email": "string",
    "normalizedEmail": "string",
    "emailConfirmed": true,
    "passwordHash": "string",
    "securityStamp": "string",
    "concurrencyStamp": "string",
    "phoneNumber": "string",
    "phoneNumberConfirmed": true,
    "twoFactorEnabled": true,
    "lockoutEnd": "2019-08-24T14:15:22Z",
    "lockoutEnabled": true,
    "accessFailedCount": 0,
    "fullName": "string",
    "phone": "string",
    "role": "string",
    "isActive": true,
    "createdAt": "2019-08-24T14:15:22Z",
    "orders": [
      {
        "id": 0,
        "orderDate": "2019-08-24T14:15:22Z",
        "totalAmount": 0.1,
        "status": 0,
        "userId": "string",
        "applicationUser": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "items": [
          {
            "id": null,
            "orderId": null,
            "order": null,
            "laptopVariantId": null,
            "laptopVariant": null,
            "quantity": null,
            "unitPrice": null
          }
        ],
        "payments": [
          {
            "id": null,
            "orderId": null,
            "paymentMethod": null,
            "paymentStatus": null,
            "transactionId": null,
            "paidAt": null,
            "order": null
          }
        ],
        "shipping": {
          "id": 0,
          "orderId": 0,
          "address": "string",
          "city": "string",
          "country": "string",
          "postalCode": "string",
          "trackingNumber": "string",
          "shippedAt": "2019-08-24T14:15:22Z",
          "deliveredAt": "2019-08-24T14:15:22Z",
          "order": {}
        }
      }
    ],
    "ratings": [
      {
        "id": 0,
        "userId": "string",
        "laptopId": 0,
        "stars": 1,
        "comment": "string",
        "createdAt": "2019-08-24T14:15:22Z",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      }
    ],
    "cartItems": [
      {
        "id": 0,
        "applicationUserId": "string",
        "laptopVariantId": 0,
        "quantity": 1,
        "addedAt": "2019-08-24T14:15:22Z",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "laptopVariant": {
          "id": 0,
          "laptopId": 0,
          "discountId": 0,
          "ram": 2147483647,
          "storage": 2147483647,
          "price": 0.1,
          "stockQuantity": 2147483647,
          "laptop": {},
          "discount": {},
          "orderItems": [
            null
          ],
          "cartItems": [
            null
          ]
        }
      }
    ],
    "repairRequests": [
      {
        "requestId": 0,
        "applicationUserId": "string",
        "itemId": 0,
        "laptopId": 0,
        "notes": "string",
        "status": 0,
        "requestDate": "2019-08-24T14:15:22Z",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "repairServiceItem": {
          "itemId": 0,
          "name": "string",
          "repairType": "[",
          "price": 0.1,
          "estimatedTime": "string",
          "repairRequests": [
            null
          ]
        },
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      }
    ],
    "auditLogs": [
      {
        "id": 0,
        "applicationUserId": "string",
        "action": "string",
        "entity": "string",
        "entityId": 0,
        "timestamp": "2019-08-24T14:15:22Z",
        "details": "string",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        }
      }
    ],
    "refreshTokens": [
      {
        "token": "string",
        "expiresOn": "2019-08-24T14:15:22Z",
        "isExpired": true,
        "createdOn": "2019-08-24T14:15:22Z",
        "revokedOn": "2019-08-24T14:15:22Z",
        "isActive": true
      }
    ],
    "verificationCodes": [
      {
        "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
        "userId": "string",
        "code": "string",
        "type": 0,
        "destination": 0,
        "isUsed": true,
        "createdAt": "2019-08-24T14:15:22Z",
        "expiryDate": "2019-08-24T14:15:22Z",
        "attemptCount": 10,
        "maxAttempts": 1,
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        }
      }
    ],
    "emailQueue": [
      {
        "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
        "toEmail": "string",
        "toName": "string",
        "subject": "string",
        "body": "string",
        "isHtml": true,
        "emailType": 0,
        "status": 0,
        "retryCount": 0,
        "maxRetries": 0,
        "createdAt": "2019-08-24T14:15:22Z",
        "scheduledAt": "2019-08-24T14:15:22Z",
        "sentAt": "2019-08-24T14:15:22Z",
        "nextRetryAt": "2019-08-24T14:15:22Z",
        "errorMessage": "string",
        "priority": 0,
        "userId": "string",
        "templateData": "string",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        }
      }
    ]
  },
  "laptopVariant": {
    "id": 0,
    "laptopId": 0,
    "discountId": 0,
    "ram": 2147483647,
    "storage": 2147483647,
    "price": 0.1,
    "stockQuantity": 2147483647,
    "laptop": {
      "id": 0,
      "modelName": "string",
      "processor": "string",
      "gpu": "string",
      "screenSize": "string",
      "hasCamera": true,
      "hasKeyboard": true,
      "hasTouchScreen": true,
      "ports": "string",
      "description": "string",
      "notes": "string",
      "warranty": "string",
      "brandId": 0,
      "categoryId": 0,
      "brand": {
        "id": 0,
        "name": "string",
        "country": "string",
        "logoUrl": "string",
        "description": "string",
        "laptops": [
          {}
        ]
      },
      "category": {
        "id": 0,
        "name": "string",
        "description": "string",
        "laptops": [
          {}
        ]
      },
      "variants": [
        {
          "id": 0,
          "laptopId": 0,
          "discountId": 0,
          "ram": 2147483647,
          "storage": 2147483647,
          "price": 0.1,
          "stockQuantity": 2147483647,
          "laptop": {},
          "discount": {},
          "orderItems": [
            null
          ],
          "cartItems": [
            null
          ]
        }
      ],
      "images": [
        {
          "id": 0,
          "laptopId": 0,
          "imageUrl": "string",
          "isMain": true,
          "uploadedAt": "2019-08-24T14:15:22Z",
          "laptop": {}
        }
      ],
      "ratings": [
        {
          "id": 0,
          "userId": "string",
          "laptopId": 0,
          "stars": 1,
          "comment": "string",
          "createdAt": "2019-08-24T14:15:22Z",
          "user": {},
          "laptop": {}
        }
      ],
      "repairRequests": [
        {
          "requestId": 0,
          "applicationUserId": "string",
          "itemId": 0,
          "laptopId": 0,
          "notes": "string",
          "status": "[",
          "requestDate": "2019-08-24T14:15:22Z",
          "user": {},
          "repairServiceItem": {},
          "laptop": {}
        }
      ]
    },
    "discount": {
      "id": 0,
      "title": "string",
      "description": "string",
      "percentage": 100,
      "startDate": "2019-08-24T14:15:22Z",
      "endDate": "2019-08-24T14:15:22Z",
      "isActive": true,
      "laptopVariants": [
        {
          "id": 0,
          "laptopId": 0,
          "discountId": 0,
          "ram": 2147483647,
          "storage": 2147483647,
          "price": 0.1,
          "stockQuantity": 2147483647,
          "laptop": {},
          "discount": {},
          "orderItems": [
            null
          ],
          "cartItems": [
            null
          ]
        }
      ]
    },
    "orderItems": [
      {
        "id": 0,
        "orderId": 0,
        "order": {
          "id": 0,
          "orderDate": "2019-08-24T14:15:22Z",
          "totalAmount": 0.1,
          "status": "[",
          "userId": "string",
          "applicationUser": {},
          "items": [
            null
          ],
          "payments": [
            null
          ],
          "shipping": {}
        },
        "laptopVariantId": 0,
        "laptopVariant": {
          "id": 0,
          "laptopId": 0,
          "discountId": 0,
          "ram": 2147483647,
          "storage": 2147483647,
          "price": 0.1,
          "stockQuantity": 2147483647,
          "laptop": {},
          "discount": {},
          "orderItems": [
            null
          ],
          "cartItems": [
            null
          ]
        },
        "quantity": 0,
        "unitPrice": 0.1
      }
    ],
    "cartItems": [
      {
        "id": 0,
        "applicationUserId": "string",
        "laptopVariantId": 0,
        "quantity": 1,
        "addedAt": "2019-08-24T14:15:22Z",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "laptopVariant": {
          "id": 0,
          "laptopId": 0,
          "discountId": 0,
          "ram": 2147483647,
          "storage": 2147483647,
          "price": 0.1,
          "stockQuantity": 2147483647,
          "laptop": {},
          "discount": {},
          "orderItems": [
            null
          ],
          "cartItems": [
            null
          ]
        }
      }
    ]
  }
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|id|integer(int32)|false|none||none|
|applicationUserId|string¦null|false|none||none|
|laptopVariantId|integer(int32)|false|none||none|
|quantity|integer(int32)|false|none||none|
|addedAt|string(date-time)|false|none||none|
|user|[ApplicationUser](#schemaapplicationuser)|false|none||none|
|laptopVariant|[LaptopVariant](#schemalaptopvariant)|false|none||none|

<h2 id="tocS_Category">Category</h2>

<a id="schemacategory"></a>
<a id="schema_Category"></a>
<a id="tocScategory"></a>
<a id="tocscategory"></a>

```json
{
  "id": 0,
  "name": "string",
  "description": "string",
  "laptops": [
    {
      "id": 0,
      "modelName": "string",
      "processor": "string",
      "gpu": "string",
      "screenSize": "string",
      "hasCamera": true,
      "hasKeyboard": true,
      "hasTouchScreen": true,
      "ports": "string",
      "description": "string",
      "notes": "string",
      "warranty": "string",
      "brandId": 0,
      "categoryId": 0,
      "brand": {
        "id": 0,
        "name": "string",
        "country": "string",
        "logoUrl": "string",
        "description": "string",
        "laptops": [
          {
            "id": null,
            "modelName": null,
            "processor": null,
            "gpu": null,
            "screenSize": null,
            "hasCamera": null,
            "hasKeyboard": null,
            "hasTouchScreen": null,
            "ports": null,
            "description": null,
            "notes": null,
            "warranty": null,
            "brandId": null,
            "categoryId": null,
            "brand": null,
            "category": null,
            "variants": null,
            "images": null,
            "ratings": null,
            "repairRequests": null
          }
        ]
      },
      "category": {
        "id": 0,
        "name": "string",
        "description": "string",
        "laptops": [
          {
            "id": null,
            "modelName": null,
            "processor": null,
            "gpu": null,
            "screenSize": null,
            "hasCamera": null,
            "hasKeyboard": null,
            "hasTouchScreen": null,
            "ports": null,
            "description": null,
            "notes": null,
            "warranty": null,
            "brandId": null,
            "categoryId": null,
            "brand": null,
            "category": null,
            "variants": null,
            "images": null,
            "ratings": null,
            "repairRequests": null
          }
        ]
      },
      "variants": [
        {
          "id": 0,
          "laptopId": 0,
          "discountId": 0,
          "ram": 2147483647,
          "storage": 2147483647,
          "price": 0.1,
          "stockQuantity": 2147483647,
          "laptop": {
            "id": null,
            "modelName": null,
            "processor": null,
            "gpu": null,
            "screenSize": null,
            "hasCamera": null,
            "hasKeyboard": null,
            "hasTouchScreen": null,
            "ports": null,
            "description": null,
            "notes": null,
            "warranty": null,
            "brandId": null,
            "categoryId": null,
            "brand": null,
            "category": null,
            "variants": null,
            "images": null,
            "ratings": null,
            "repairRequests": null
          },
          "discount": {
            "id": null,
            "title": null,
            "description": null,
            "percentage": null,
            "startDate": null,
            "endDate": null,
            "isActive": null,
            "laptopVariants": null
          },
          "orderItems": [
            {}
          ],
          "cartItems": [
            {}
          ]
        }
      ],
      "images": [
        {
          "id": 0,
          "laptopId": 0,
          "imageUrl": "string",
          "isMain": true,
          "uploadedAt": "2019-08-24T14:15:22Z",
          "laptop": {
            "id": null,
            "modelName": null,
            "processor": null,
            "gpu": null,
            "screenSize": null,
            "hasCamera": null,
            "hasKeyboard": null,
            "hasTouchScreen": null,
            "ports": null,
            "description": null,
            "notes": null,
            "warranty": null,
            "brandId": null,
            "categoryId": null,
            "brand": null,
            "category": null,
            "variants": null,
            "images": null,
            "ratings": null,
            "repairRequests": null
          }
        }
      ],
      "ratings": [
        {
          "id": 0,
          "userId": "string",
          "laptopId": 0,
          "stars": 1,
          "comment": "string",
          "createdAt": "2019-08-24T14:15:22Z",
          "user": {
            "id": null,
            "userName": null,
            "normalizedUserName": null,
            "email": null,
            "normalizedEmail": null,
            "emailConfirmed": null,
            "passwordHash": null,
            "securityStamp": null,
            "concurrencyStamp": null,
            "phoneNumber": null,
            "phoneNumberConfirmed": null,
            "twoFactorEnabled": null,
            "lockoutEnd": null,
            "lockoutEnabled": null,
            "accessFailedCount": null,
            "fullName": null,
            "phone": null,
            "role": null,
            "isActive": null,
            "createdAt": null,
            "orders": null,
            "ratings": null,
            "cartItems": null,
            "repairRequests": null,
            "auditLogs": null,
            "refreshTokens": null,
            "verificationCodes": null,
            "emailQueue": null
          },
          "laptop": {
            "id": null,
            "modelName": null,
            "processor": null,
            "gpu": null,
            "screenSize": null,
            "hasCamera": null,
            "hasKeyboard": null,
            "hasTouchScreen": null,
            "ports": null,
            "description": null,
            "notes": null,
            "warranty": null,
            "brandId": null,
            "categoryId": null,
            "brand": null,
            "category": null,
            "variants": null,
            "images": null,
            "ratings": null,
            "repairRequests": null
          }
        }
      ],
      "repairRequests": [
        {
          "requestId": 0,
          "applicationUserId": "string",
          "itemId": 0,
          "laptopId": 0,
          "notes": "string",
          "status": 0,
          "requestDate": "2019-08-24T14:15:22Z",
          "user": {
            "id": null,
            "userName": null,
            "normalizedUserName": null,
            "email": null,
            "normalizedEmail": null,
            "emailConfirmed": null,
            "passwordHash": null,
            "securityStamp": null,
            "concurrencyStamp": null,
            "phoneNumber": null,
            "phoneNumberConfirmed": null,
            "twoFactorEnabled": null,
            "lockoutEnd": null,
            "lockoutEnabled": null,
            "accessFailedCount": null,
            "fullName": null,
            "phone": null,
            "role": null,
            "isActive": null,
            "createdAt": null,
            "orders": null,
            "ratings": null,
            "cartItems": null,
            "repairRequests": null,
            "auditLogs": null,
            "refreshTokens": null,
            "verificationCodes": null,
            "emailQueue": null
          },
          "repairServiceItem": {
            "itemId": null,
            "name": null,
            "repairType": null,
            "price": null,
            "estimatedTime": null,
            "repairRequests": null
          },
          "laptop": {
            "id": null,
            "modelName": null,
            "processor": null,
            "gpu": null,
            "screenSize": null,
            "hasCamera": null,
            "hasKeyboard": null,
            "hasTouchScreen": null,
            "ports": null,
            "description": null,
            "notes": null,
            "warranty": null,
            "brandId": null,
            "categoryId": null,
            "brand": null,
            "category": null,
            "variants": null,
            "images": null,
            "ratings": null,
            "repairRequests": null
          }
        }
      ]
    }
  ]
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|id|integer(int32)|false|none||none|
|name|string|true|none||none|
|description|string¦null|false|none||none|
|laptops|[[Laptop](#schemalaptop)]¦null|false|none||none|

<h2 id="tocS_CategoryWithCountDto">CategoryWithCountDto</h2>

<a id="schemacategorywithcountdto"></a>
<a id="schema_CategoryWithCountDto"></a>
<a id="tocScategorywithcountdto"></a>
<a id="tocscategorywithcountdto"></a>

```json
{
  "id": 0,
  "name": "string",
  "laptopsCount": 0
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|id|integer(int32)|false|none||none|
|name|string¦null|false|none||none|
|laptopsCount|integer(int32)|false|none||none|

<h2 id="tocS_ChangePasswordDto">ChangePasswordDto</h2>

<a id="schemachangepassworddto"></a>
<a id="schema_ChangePasswordDto"></a>
<a id="tocSchangepassworddto"></a>
<a id="tocschangepassworddto"></a>

```json
{
  "currentPassword": "string",
  "newPassword": "string",
  "confirmPassword": "string"
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|currentPassword|string|true|none||none|
|newPassword|string|true|none||none|
|confirmPassword|string|true|none||none|

<h2 id="tocS_ConfirmEmailWithCodeDto">ConfirmEmailWithCodeDto</h2>

<a id="schemaconfirmemailwithcodedto"></a>
<a id="schema_ConfirmEmailWithCodeDto"></a>
<a id="tocSconfirmemailwithcodedto"></a>
<a id="tocsconfirmemailwithcodedto"></a>

```json
{
  "email": "user@example.com",
  "code": "string"
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|email|string(email)|true|none||none|
|code|string|true|none||none|

<h2 id="tocS_CreateLaptopDto">CreateLaptopDto</h2>

<a id="schemacreatelaptopdto"></a>
<a id="schema_CreateLaptopDto"></a>
<a id="tocScreatelaptopdto"></a>
<a id="tocscreatelaptopdto"></a>

```json
{
  "modelName": "string",
  "processor": "string",
  "gpu": "string",
  "screenSize": "string",
  "hasCamera": true,
  "hasKeyboard": true,
  "hasTouchScreen": true,
  "ports": "string"
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|modelName|string|true|none||none|
|processor|string|true|none||none|
|gpu|string|true|none||none|
|screenSize|string|true|none||none|
|hasCamera|boolean|false|none||none|
|hasKeyboard|boolean|false|none||none|
|hasTouchScreen|boolean|false|none||none|
|ports|string|true|none||none|

<h2 id="tocS_CreateLaptopDtoServiceResponse">CreateLaptopDtoServiceResponse</h2>

<a id="schemacreatelaptopdtoserviceresponse"></a>
<a id="schema_CreateLaptopDtoServiceResponse"></a>
<a id="tocScreatelaptopdtoserviceresponse"></a>
<a id="tocscreatelaptopdtoserviceresponse"></a>

```json
{
  "isSuccess": true,
  "message": "string",
  "messageAr": "string",
  "data": {
    "modelName": "string",
    "processor": "string",
    "gpu": "string",
    "screenSize": "string",
    "hasCamera": true,
    "hasKeyboard": true,
    "hasTouchScreen": true,
    "ports": "string"
  },
  "errors": [
    "string"
  ],
  "statusCode": 0,
  "timestamp": "2019-08-24T14:15:22Z"
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|isSuccess|boolean|false|none||none|
|message|string¦null|false|none||none|
|messageAr|string¦null|false|none||none|
|data|[CreateLaptopDto](#schemacreatelaptopdto)|false|none||none|
|errors|[string]¦null|false|none||none|
|statusCode|integer(int32)|false|none||none|
|timestamp|string(date-time)|false|none||none|

<h2 id="tocS_DestinationStatus">DestinationStatus</h2>

<a id="schemadestinationstatus"></a>
<a id="schema_DestinationStatus"></a>
<a id="tocSdestinationstatus"></a>
<a id="tocsdestinationstatus"></a>

```json
0

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|*anonymous*|integer(int32)|false|none||none|

#### Enum

|Name|Value|
|---|---|
|*anonymous*|0|
|*anonymous*|1|
|*anonymous*|2|
|*anonymous*|3|

<h2 id="tocS_Discount">Discount</h2>

<a id="schemadiscount"></a>
<a id="schema_Discount"></a>
<a id="tocSdiscount"></a>
<a id="tocsdiscount"></a>

```json
{
  "id": 0,
  "title": "string",
  "description": "string",
  "percentage": 100,
  "startDate": "2019-08-24T14:15:22Z",
  "endDate": "2019-08-24T14:15:22Z",
  "isActive": true,
  "laptopVariants": [
    {
      "id": 0,
      "laptopId": 0,
      "discountId": 0,
      "ram": 2147483647,
      "storage": 2147483647,
      "price": 0.1,
      "stockQuantity": 2147483647,
      "laptop": {
        "id": 0,
        "modelName": "string",
        "processor": "string",
        "gpu": "string",
        "screenSize": "string",
        "hasCamera": true,
        "hasKeyboard": true,
        "hasTouchScreen": true,
        "ports": "string",
        "description": "string",
        "notes": "string",
        "warranty": "string",
        "brandId": 0,
        "categoryId": 0,
        "brand": {
          "id": 0,
          "name": "string",
          "country": "string",
          "logoUrl": "string",
          "description": "string",
          "laptops": [
            null
          ]
        },
        "category": {
          "id": 0,
          "name": "string",
          "description": "string",
          "laptops": [
            null
          ]
        },
        "variants": [
          {
            "id": null,
            "laptopId": null,
            "discountId": null,
            "ram": null,
            "storage": null,
            "price": null,
            "stockQuantity": null,
            "laptop": null,
            "discount": null,
            "orderItems": null,
            "cartItems": null
          }
        ],
        "images": [
          {
            "id": null,
            "laptopId": null,
            "imageUrl": null,
            "isMain": null,
            "uploadedAt": null,
            "laptop": null
          }
        ],
        "ratings": [
          {
            "id": null,
            "userId": null,
            "laptopId": null,
            "stars": null,
            "comment": null,
            "createdAt": null,
            "user": null,
            "laptop": null
          }
        ],
        "repairRequests": [
          {
            "requestId": null,
            "applicationUserId": null,
            "itemId": null,
            "laptopId": null,
            "notes": null,
            "status": null,
            "requestDate": null,
            "user": null,
            "repairServiceItem": null,
            "laptop": null
          }
        ]
      },
      "discount": {
        "id": 0,
        "title": "string",
        "description": "string",
        "percentage": 100,
        "startDate": "2019-08-24T14:15:22Z",
        "endDate": "2019-08-24T14:15:22Z",
        "isActive": true,
        "laptopVariants": [
          {
            "id": null,
            "laptopId": null,
            "discountId": null,
            "ram": null,
            "storage": null,
            "price": null,
            "stockQuantity": null,
            "laptop": null,
            "discount": null,
            "orderItems": null,
            "cartItems": null
          }
        ]
      },
      "orderItems": [
        {
          "id": 0,
          "orderId": 0,
          "order": {
            "id": null,
            "orderDate": null,
            "totalAmount": null,
            "status": null,
            "userId": null,
            "applicationUser": null,
            "items": null,
            "payments": null,
            "shipping": null
          },
          "laptopVariantId": 0,
          "laptopVariant": {
            "id": null,
            "laptopId": null,
            "discountId": null,
            "ram": null,
            "storage": null,
            "price": null,
            "stockQuantity": null,
            "laptop": null,
            "discount": null,
            "orderItems": null,
            "cartItems": null
          },
          "quantity": 0,
          "unitPrice": 0.1
        }
      ],
      "cartItems": [
        {
          "id": 0,
          "applicationUserId": "string",
          "laptopVariantId": 0,
          "quantity": 1,
          "addedAt": "2019-08-24T14:15:22Z",
          "user": {
            "id": null,
            "userName": null,
            "normalizedUserName": null,
            "email": null,
            "normalizedEmail": null,
            "emailConfirmed": null,
            "passwordHash": null,
            "securityStamp": null,
            "concurrencyStamp": null,
            "phoneNumber": null,
            "phoneNumberConfirmed": null,
            "twoFactorEnabled": null,
            "lockoutEnd": null,
            "lockoutEnabled": null,
            "accessFailedCount": null,
            "fullName": null,
            "phone": null,
            "role": null,
            "isActive": null,
            "createdAt": null,
            "orders": null,
            "ratings": null,
            "cartItems": null,
            "repairRequests": null,
            "auditLogs": null,
            "refreshTokens": null,
            "verificationCodes": null,
            "emailQueue": null
          },
          "laptopVariant": {
            "id": null,
            "laptopId": null,
            "discountId": null,
            "ram": null,
            "storage": null,
            "price": null,
            "stockQuantity": null,
            "laptop": null,
            "discount": null,
            "orderItems": null,
            "cartItems": null
          }
        }
      ]
    }
  ]
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|id|integer(int32)|false|none||none|
|title|string|true|none||none|
|description|string¦null|false|none||none|
|percentage|number(double)|false|none||none|
|startDate|string(date-time)|false|none||none|
|endDate|string(date-time)|false|none||none|
|isActive|boolean|false|none||none|
|laptopVariants|[[LaptopVariant](#schemalaptopvariant)]¦null|false|none||none|

<h2 id="tocS_EmailPriority">EmailPriority</h2>

<a id="schemaemailpriority"></a>
<a id="schema_EmailPriority"></a>
<a id="tocSemailpriority"></a>
<a id="tocsemailpriority"></a>

```json
0

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|*anonymous*|integer(int32)|false|none||none|

#### Enum

|Name|Value|
|---|---|
|*anonymous*|0|
|*anonymous*|1|
|*anonymous*|2|

<h2 id="tocS_EmailQueue">EmailQueue</h2>

<a id="schemaemailqueue"></a>
<a id="schema_EmailQueue"></a>
<a id="tocSemailqueue"></a>
<a id="tocsemailqueue"></a>

```json
{
  "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
  "toEmail": "string",
  "toName": "string",
  "subject": "string",
  "body": "string",
  "isHtml": true,
  "emailType": 0,
  "status": 0,
  "retryCount": 0,
  "maxRetries": 0,
  "createdAt": "2019-08-24T14:15:22Z",
  "scheduledAt": "2019-08-24T14:15:22Z",
  "sentAt": "2019-08-24T14:15:22Z",
  "nextRetryAt": "2019-08-24T14:15:22Z",
  "errorMessage": "string",
  "priority": 0,
  "userId": "string",
  "templateData": "string",
  "user": {
    "id": "string",
    "userName": "string",
    "normalizedUserName": "string",
    "email": "string",
    "normalizedEmail": "string",
    "emailConfirmed": true,
    "passwordHash": "string",
    "securityStamp": "string",
    "concurrencyStamp": "string",
    "phoneNumber": "string",
    "phoneNumberConfirmed": true,
    "twoFactorEnabled": true,
    "lockoutEnd": "2019-08-24T14:15:22Z",
    "lockoutEnabled": true,
    "accessFailedCount": 0,
    "fullName": "string",
    "phone": "string",
    "role": "string",
    "isActive": true,
    "createdAt": "2019-08-24T14:15:22Z",
    "orders": [
      {
        "id": 0,
        "orderDate": "2019-08-24T14:15:22Z",
        "totalAmount": 0.1,
        "status": 0,
        "userId": "string",
        "applicationUser": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "items": [
          {
            "id": null,
            "orderId": null,
            "order": null,
            "laptopVariantId": null,
            "laptopVariant": null,
            "quantity": null,
            "unitPrice": null
          }
        ],
        "payments": [
          {
            "id": null,
            "orderId": null,
            "paymentMethod": null,
            "paymentStatus": null,
            "transactionId": null,
            "paidAt": null,
            "order": null
          }
        ],
        "shipping": {
          "id": 0,
          "orderId": 0,
          "address": "string",
          "city": "string",
          "country": "string",
          "postalCode": "string",
          "trackingNumber": "string",
          "shippedAt": "2019-08-24T14:15:22Z",
          "deliveredAt": "2019-08-24T14:15:22Z",
          "order": {}
        }
      }
    ],
    "ratings": [
      {
        "id": 0,
        "userId": "string",
        "laptopId": 0,
        "stars": 1,
        "comment": "string",
        "createdAt": "2019-08-24T14:15:22Z",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      }
    ],
    "cartItems": [
      {
        "id": 0,
        "applicationUserId": "string",
        "laptopVariantId": 0,
        "quantity": 1,
        "addedAt": "2019-08-24T14:15:22Z",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "laptopVariant": {
          "id": 0,
          "laptopId": 0,
          "discountId": 0,
          "ram": 2147483647,
          "storage": 2147483647,
          "price": 0.1,
          "stockQuantity": 2147483647,
          "laptop": {},
          "discount": {},
          "orderItems": [
            null
          ],
          "cartItems": [
            null
          ]
        }
      }
    ],
    "repairRequests": [
      {
        "requestId": 0,
        "applicationUserId": "string",
        "itemId": 0,
        "laptopId": 0,
        "notes": "string",
        "status": 0,
        "requestDate": "2019-08-24T14:15:22Z",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "repairServiceItem": {
          "itemId": 0,
          "name": "string",
          "repairType": "[",
          "price": 0.1,
          "estimatedTime": "string",
          "repairRequests": [
            null
          ]
        },
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      }
    ],
    "auditLogs": [
      {
        "id": 0,
        "applicationUserId": "string",
        "action": "string",
        "entity": "string",
        "entityId": 0,
        "timestamp": "2019-08-24T14:15:22Z",
        "details": "string",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        }
      }
    ],
    "refreshTokens": [
      {
        "token": "string",
        "expiresOn": "2019-08-24T14:15:22Z",
        "isExpired": true,
        "createdOn": "2019-08-24T14:15:22Z",
        "revokedOn": "2019-08-24T14:15:22Z",
        "isActive": true
      }
    ],
    "verificationCodes": [
      {
        "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
        "userId": "string",
        "code": "string",
        "type": 0,
        "destination": 0,
        "isUsed": true,
        "createdAt": "2019-08-24T14:15:22Z",
        "expiryDate": "2019-08-24T14:15:22Z",
        "attemptCount": 10,
        "maxAttempts": 1,
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        }
      }
    ],
    "emailQueue": [
      {
        "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
        "toEmail": "string",
        "toName": "string",
        "subject": "string",
        "body": "string",
        "isHtml": true,
        "emailType": 0,
        "status": 0,
        "retryCount": 0,
        "maxRetries": 0,
        "createdAt": "2019-08-24T14:15:22Z",
        "scheduledAt": "2019-08-24T14:15:22Z",
        "sentAt": "2019-08-24T14:15:22Z",
        "nextRetryAt": "2019-08-24T14:15:22Z",
        "errorMessage": "string",
        "priority": 0,
        "userId": "string",
        "templateData": "string",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        }
      }
    ]
  }
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|id|string(uuid)|false|none||none|
|toEmail|string¦null|false|none||none|
|toName|string¦null|false|none||none|
|subject|string¦null|false|none||none|
|body|string¦null|false|none||none|
|isHtml|boolean|false|none||none|
|emailType|[EmailType](#schemaemailtype)|false|none||none|
|status|[EmailStatus](#schemaemailstatus)|false|none||none|
|retryCount|integer(int32)|false|none||none|
|maxRetries|integer(int32)|false|none||none|
|createdAt|string(date-time)|false|none||none|
|scheduledAt|string(date-time)¦null|false|none||none|
|sentAt|string(date-time)¦null|false|none||none|
|nextRetryAt|string(date-time)¦null|false|none||none|
|errorMessage|string¦null|false|none||none|
|priority|[EmailPriority](#schemaemailpriority)|false|none||none|
|userId|string¦null|false|none||none|
|templateData|string¦null|false|none||none|
|user|[ApplicationUser](#schemaapplicationuser)|false|none||none|

<h2 id="tocS_EmailStatus">EmailStatus</h2>

<a id="schemaemailstatus"></a>
<a id="schema_EmailStatus"></a>
<a id="tocSemailstatus"></a>
<a id="tocsemailstatus"></a>

```json
0

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|*anonymous*|integer(int32)|false|none||none|

#### Enum

|Name|Value|
|---|---|
|*anonymous*|0|
|*anonymous*|1|
|*anonymous*|2|
|*anonymous*|3|
|*anonymous*|4|

<h2 id="tocS_EmailType">EmailType</h2>

<a id="schemaemailtype"></a>
<a id="schema_EmailType"></a>
<a id="tocSemailtype"></a>
<a id="tocsemailtype"></a>

```json
0

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|*anonymous*|integer(int32)|false|none||none|

#### Enum

|Name|Value|
|---|---|
|*anonymous*|0|
|*anonymous*|1|
|*anonymous*|2|
|*anonymous*|3|
|*anonymous*|4|
|*anonymous*|5|

<h2 id="tocS_ForgotPasswordDto">ForgotPasswordDto</h2>

<a id="schemaforgotpassworddto"></a>
<a id="schema_ForgotPasswordDto"></a>
<a id="tocSforgotpassworddto"></a>
<a id="tocsforgotpassworddto"></a>

```json
{
  "email": "user@example.com"
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|email|string(email)|true|none||none|

<h2 id="tocS_Laptop">Laptop</h2>

<a id="schemalaptop"></a>
<a id="schema_Laptop"></a>
<a id="tocSlaptop"></a>
<a id="tocslaptop"></a>

```json
{
  "id": 0,
  "modelName": "string",
  "processor": "string",
  "gpu": "string",
  "screenSize": "string",
  "hasCamera": true,
  "hasKeyboard": true,
  "hasTouchScreen": true,
  "ports": "string",
  "description": "string",
  "notes": "string",
  "warranty": "string",
  "brandId": 0,
  "categoryId": 0,
  "brand": {
    "id": 0,
    "name": "string",
    "country": "string",
    "logoUrl": "string",
    "description": "string",
    "laptops": [
      {
        "id": 0,
        "modelName": "string",
        "processor": "string",
        "gpu": "string",
        "screenSize": "string",
        "hasCamera": true,
        "hasKeyboard": true,
        "hasTouchScreen": true,
        "ports": "string",
        "description": "string",
        "notes": "string",
        "warranty": "string",
        "brandId": 0,
        "categoryId": 0,
        "brand": {
          "id": 0,
          "name": "string",
          "country": "string",
          "logoUrl": "string",
          "description": "string",
          "laptops": [
            null
          ]
        },
        "category": {
          "id": 0,
          "name": "string",
          "description": "string",
          "laptops": [
            null
          ]
        },
        "variants": [
          {
            "id": null,
            "laptopId": null,
            "discountId": null,
            "ram": null,
            "storage": null,
            "price": null,
            "stockQuantity": null,
            "laptop": null,
            "discount": null,
            "orderItems": null,
            "cartItems": null
          }
        ],
        "images": [
          {
            "id": null,
            "laptopId": null,
            "imageUrl": null,
            "isMain": null,
            "uploadedAt": null,
            "laptop": null
          }
        ],
        "ratings": [
          {
            "id": null,
            "userId": null,
            "laptopId": null,
            "stars": null,
            "comment": null,
            "createdAt": null,
            "user": null,
            "laptop": null
          }
        ],
        "repairRequests": [
          {
            "requestId": null,
            "applicationUserId": null,
            "itemId": null,
            "laptopId": null,
            "notes": null,
            "status": null,
            "requestDate": null,
            "user": null,
            "repairServiceItem": null,
            "laptop": null
          }
        ]
      }
    ]
  },
  "category": {
    "id": 0,
    "name": "string",
    "description": "string",
    "laptops": [
      {
        "id": 0,
        "modelName": "string",
        "processor": "string",
        "gpu": "string",
        "screenSize": "string",
        "hasCamera": true,
        "hasKeyboard": true,
        "hasTouchScreen": true,
        "ports": "string",
        "description": "string",
        "notes": "string",
        "warranty": "string",
        "brandId": 0,
        "categoryId": 0,
        "brand": {
          "id": 0,
          "name": "string",
          "country": "string",
          "logoUrl": "string",
          "description": "string",
          "laptops": [
            null
          ]
        },
        "category": {
          "id": 0,
          "name": "string",
          "description": "string",
          "laptops": [
            null
          ]
        },
        "variants": [
          {
            "id": null,
            "laptopId": null,
            "discountId": null,
            "ram": null,
            "storage": null,
            "price": null,
            "stockQuantity": null,
            "laptop": null,
            "discount": null,
            "orderItems": null,
            "cartItems": null
          }
        ],
        "images": [
          {
            "id": null,
            "laptopId": null,
            "imageUrl": null,
            "isMain": null,
            "uploadedAt": null,
            "laptop": null
          }
        ],
        "ratings": [
          {
            "id": null,
            "userId": null,
            "laptopId": null,
            "stars": null,
            "comment": null,
            "createdAt": null,
            "user": null,
            "laptop": null
          }
        ],
        "repairRequests": [
          {
            "requestId": null,
            "applicationUserId": null,
            "itemId": null,
            "laptopId": null,
            "notes": null,
            "status": null,
            "requestDate": null,
            "user": null,
            "repairServiceItem": null,
            "laptop": null
          }
        ]
      }
    ]
  },
  "variants": [
    {
      "id": 0,
      "laptopId": 0,
      "discountId": 0,
      "ram": 2147483647,
      "storage": 2147483647,
      "price": 0.1,
      "stockQuantity": 2147483647,
      "laptop": {
        "id": 0,
        "modelName": "string",
        "processor": "string",
        "gpu": "string",
        "screenSize": "string",
        "hasCamera": true,
        "hasKeyboard": true,
        "hasTouchScreen": true,
        "ports": "string",
        "description": "string",
        "notes": "string",
        "warranty": "string",
        "brandId": 0,
        "categoryId": 0,
        "brand": {
          "id": 0,
          "name": "string",
          "country": "string",
          "logoUrl": "string",
          "description": "string",
          "laptops": [
            null
          ]
        },
        "category": {
          "id": 0,
          "name": "string",
          "description": "string",
          "laptops": [
            null
          ]
        },
        "variants": [
          {
            "id": null,
            "laptopId": null,
            "discountId": null,
            "ram": null,
            "storage": null,
            "price": null,
            "stockQuantity": null,
            "laptop": null,
            "discount": null,
            "orderItems": null,
            "cartItems": null
          }
        ],
        "images": [
          {
            "id": null,
            "laptopId": null,
            "imageUrl": null,
            "isMain": null,
            "uploadedAt": null,
            "laptop": null
          }
        ],
        "ratings": [
          {
            "id": null,
            "userId": null,
            "laptopId": null,
            "stars": null,
            "comment": null,
            "createdAt": null,
            "user": null,
            "laptop": null
          }
        ],
        "repairRequests": [
          {
            "requestId": null,
            "applicationUserId": null,
            "itemId": null,
            "laptopId": null,
            "notes": null,
            "status": null,
            "requestDate": null,
            "user": null,
            "repairServiceItem": null,
            "laptop": null
          }
        ]
      },
      "discount": {
        "id": 0,
        "title": "string",
        "description": "string",
        "percentage": 100,
        "startDate": "2019-08-24T14:15:22Z",
        "endDate": "2019-08-24T14:15:22Z",
        "isActive": true,
        "laptopVariants": [
          {
            "id": null,
            "laptopId": null,
            "discountId": null,
            "ram": null,
            "storage": null,
            "price": null,
            "stockQuantity": null,
            "laptop": null,
            "discount": null,
            "orderItems": null,
            "cartItems": null
          }
        ]
      },
      "orderItems": [
        {
          "id": 0,
          "orderId": 0,
          "order": {
            "id": null,
            "orderDate": null,
            "totalAmount": null,
            "status": null,
            "userId": null,
            "applicationUser": null,
            "items": null,
            "payments": null,
            "shipping": null
          },
          "laptopVariantId": 0,
          "laptopVariant": {
            "id": null,
            "laptopId": null,
            "discountId": null,
            "ram": null,
            "storage": null,
            "price": null,
            "stockQuantity": null,
            "laptop": null,
            "discount": null,
            "orderItems": null,
            "cartItems": null
          },
          "quantity": 0,
          "unitPrice": 0.1
        }
      ],
      "cartItems": [
        {
          "id": 0,
          "applicationUserId": "string",
          "laptopVariantId": 0,
          "quantity": 1,
          "addedAt": "2019-08-24T14:15:22Z",
          "user": {
            "id": null,
            "userName": null,
            "normalizedUserName": null,
            "email": null,
            "normalizedEmail": null,
            "emailConfirmed": null,
            "passwordHash": null,
            "securityStamp": null,
            "concurrencyStamp": null,
            "phoneNumber": null,
            "phoneNumberConfirmed": null,
            "twoFactorEnabled": null,
            "lockoutEnd": null,
            "lockoutEnabled": null,
            "accessFailedCount": null,
            "fullName": null,
            "phone": null,
            "role": null,
            "isActive": null,
            "createdAt": null,
            "orders": null,
            "ratings": null,
            "cartItems": null,
            "repairRequests": null,
            "auditLogs": null,
            "refreshTokens": null,
            "verificationCodes": null,
            "emailQueue": null
          },
          "laptopVariant": {
            "id": null,
            "laptopId": null,
            "discountId": null,
            "ram": null,
            "storage": null,
            "price": null,
            "stockQuantity": null,
            "laptop": null,
            "discount": null,
            "orderItems": null,
            "cartItems": null
          }
        }
      ]
    }
  ],
  "images": [
    {
      "id": 0,
      "laptopId": 0,
      "imageUrl": "string",
      "isMain": true,
      "uploadedAt": "2019-08-24T14:15:22Z",
      "laptop": {
        "id": 0,
        "modelName": "string",
        "processor": "string",
        "gpu": "string",
        "screenSize": "string",
        "hasCamera": true,
        "hasKeyboard": true,
        "hasTouchScreen": true,
        "ports": "string",
        "description": "string",
        "notes": "string",
        "warranty": "string",
        "brandId": 0,
        "categoryId": 0,
        "brand": {
          "id": 0,
          "name": "string",
          "country": "string",
          "logoUrl": "string",
          "description": "string",
          "laptops": [
            null
          ]
        },
        "category": {
          "id": 0,
          "name": "string",
          "description": "string",
          "laptops": [
            null
          ]
        },
        "variants": [
          {
            "id": null,
            "laptopId": null,
            "discountId": null,
            "ram": null,
            "storage": null,
            "price": null,
            "stockQuantity": null,
            "laptop": null,
            "discount": null,
            "orderItems": null,
            "cartItems": null
          }
        ],
        "images": [
          {
            "id": null,
            "laptopId": null,
            "imageUrl": null,
            "isMain": null,
            "uploadedAt": null,
            "laptop": null
          }
        ],
        "ratings": [
          {
            "id": null,
            "userId": null,
            "laptopId": null,
            "stars": null,
            "comment": null,
            "createdAt": null,
            "user": null,
            "laptop": null
          }
        ],
        "repairRequests": [
          {
            "requestId": null,
            "applicationUserId": null,
            "itemId": null,
            "laptopId": null,
            "notes": null,
            "status": null,
            "requestDate": null,
            "user": null,
            "repairServiceItem": null,
            "laptop": null
          }
        ]
      }
    }
  ],
  "ratings": [
    {
      "id": 0,
      "userId": "string",
      "laptopId": 0,
      "stars": 1,
      "comment": "string",
      "createdAt": "2019-08-24T14:15:22Z",
      "user": {
        "id": "string",
        "userName": "string",
        "normalizedUserName": "string",
        "email": "string",
        "normalizedEmail": "string",
        "emailConfirmed": true,
        "passwordHash": "string",
        "securityStamp": "string",
        "concurrencyStamp": "string",
        "phoneNumber": "string",
        "phoneNumberConfirmed": true,
        "twoFactorEnabled": true,
        "lockoutEnd": "2019-08-24T14:15:22Z",
        "lockoutEnabled": true,
        "accessFailedCount": 0,
        "fullName": "string",
        "phone": "string",
        "role": "string",
        "isActive": true,
        "createdAt": "2019-08-24T14:15:22Z",
        "orders": [
          {
            "id": null,
            "orderDate": null,
            "totalAmount": null,
            "status": null,
            "userId": null,
            "applicationUser": null,
            "items": null,
            "payments": null,
            "shipping": null
          }
        ],
        "ratings": [
          {
            "id": null,
            "userId": null,
            "laptopId": null,
            "stars": null,
            "comment": null,
            "createdAt": null,
            "user": null,
            "laptop": null
          }
        ],
        "cartItems": [
          {
            "id": null,
            "applicationUserId": null,
            "laptopVariantId": null,
            "quantity": null,
            "addedAt": null,
            "user": null,
            "laptopVariant": null
          }
        ],
        "repairRequests": [
          {
            "requestId": null,
            "applicationUserId": null,
            "itemId": null,
            "laptopId": null,
            "notes": null,
            "status": null,
            "requestDate": null,
            "user": null,
            "repairServiceItem": null,
            "laptop": null
          }
        ],
        "auditLogs": [
          {
            "id": null,
            "applicationUserId": null,
            "action": null,
            "entity": null,
            "entityId": null,
            "timestamp": null,
            "details": null,
            "user": null
          }
        ],
        "refreshTokens": [
          {
            "token": null,
            "expiresOn": null,
            "isExpired": null,
            "createdOn": null,
            "revokedOn": null,
            "isActive": null
          }
        ],
        "verificationCodes": [
          {
            "id": null,
            "userId": null,
            "code": null,
            "type": null,
            "destination": null,
            "isUsed": null,
            "createdAt": null,
            "expiryDate": null,
            "attemptCount": null,
            "maxAttempts": null,
            "user": null
          }
        ],
        "emailQueue": [
          {
            "id": null,
            "toEmail": null,
            "toName": null,
            "subject": null,
            "body": null,
            "isHtml": null,
            "emailType": null,
            "status": null,
            "retryCount": null,
            "maxRetries": null,
            "createdAt": null,
            "scheduledAt": null,
            "sentAt": null,
            "nextRetryAt": null,
            "errorMessage": null,
            "priority": null,
            "userId": null,
            "templateData": null,
            "user": null
          }
        ]
      },
      "laptop": {
        "id": 0,
        "modelName": "string",
        "processor": "string",
        "gpu": "string",
        "screenSize": "string",
        "hasCamera": true,
        "hasKeyboard": true,
        "hasTouchScreen": true,
        "ports": "string",
        "description": "string",
        "notes": "string",
        "warranty": "string",
        "brandId": 0,
        "categoryId": 0,
        "brand": {
          "id": 0,
          "name": "string",
          "country": "string",
          "logoUrl": "string",
          "description": "string",
          "laptops": [
            null
          ]
        },
        "category": {
          "id": 0,
          "name": "string",
          "description": "string",
          "laptops": [
            null
          ]
        },
        "variants": [
          {
            "id": null,
            "laptopId": null,
            "discountId": null,
            "ram": null,
            "storage": null,
            "price": null,
            "stockQuantity": null,
            "laptop": null,
            "discount": null,
            "orderItems": null,
            "cartItems": null
          }
        ],
        "images": [
          {
            "id": null,
            "laptopId": null,
            "imageUrl": null,
            "isMain": null,
            "uploadedAt": null,
            "laptop": null
          }
        ],
        "ratings": [
          {
            "id": null,
            "userId": null,
            "laptopId": null,
            "stars": null,
            "comment": null,
            "createdAt": null,
            "user": null,
            "laptop": null
          }
        ],
        "repairRequests": [
          {
            "requestId": null,
            "applicationUserId": null,
            "itemId": null,
            "laptopId": null,
            "notes": null,
            "status": null,
            "requestDate": null,
            "user": null,
            "repairServiceItem": null,
            "laptop": null
          }
        ]
      }
    }
  ],
  "repairRequests": [
    {
      "requestId": 0,
      "applicationUserId": "string",
      "itemId": 0,
      "laptopId": 0,
      "notes": "string",
      "status": 0,
      "requestDate": "2019-08-24T14:15:22Z",
      "user": {
        "id": "string",
        "userName": "string",
        "normalizedUserName": "string",
        "email": "string",
        "normalizedEmail": "string",
        "emailConfirmed": true,
        "passwordHash": "string",
        "securityStamp": "string",
        "concurrencyStamp": "string",
        "phoneNumber": "string",
        "phoneNumberConfirmed": true,
        "twoFactorEnabled": true,
        "lockoutEnd": "2019-08-24T14:15:22Z",
        "lockoutEnabled": true,
        "accessFailedCount": 0,
        "fullName": "string",
        "phone": "string",
        "role": "string",
        "isActive": true,
        "createdAt": "2019-08-24T14:15:22Z",
        "orders": [
          {
            "id": null,
            "orderDate": null,
            "totalAmount": null,
            "status": null,
            "userId": null,
            "applicationUser": null,
            "items": null,
            "payments": null,
            "shipping": null
          }
        ],
        "ratings": [
          {
            "id": null,
            "userId": null,
            "laptopId": null,
            "stars": null,
            "comment": null,
            "createdAt": null,
            "user": null,
            "laptop": null
          }
        ],
        "cartItems": [
          {
            "id": null,
            "applicationUserId": null,
            "laptopVariantId": null,
            "quantity": null,
            "addedAt": null,
            "user": null,
            "laptopVariant": null
          }
        ],
        "repairRequests": [
          {
            "requestId": null,
            "applicationUserId": null,
            "itemId": null,
            "laptopId": null,
            "notes": null,
            "status": null,
            "requestDate": null,
            "user": null,
            "repairServiceItem": null,
            "laptop": null
          }
        ],
        "auditLogs": [
          {
            "id": null,
            "applicationUserId": null,
            "action": null,
            "entity": null,
            "entityId": null,
            "timestamp": null,
            "details": null,
            "user": null
          }
        ],
        "refreshTokens": [
          {
            "token": null,
            "expiresOn": null,
            "isExpired": null,
            "createdOn": null,
            "revokedOn": null,
            "isActive": null
          }
        ],
        "verificationCodes": [
          {
            "id": null,
            "userId": null,
            "code": null,
            "type": null,
            "destination": null,
            "isUsed": null,
            "createdAt": null,
            "expiryDate": null,
            "attemptCount": null,
            "maxAttempts": null,
            "user": null
          }
        ],
        "emailQueue": [
          {
            "id": null,
            "toEmail": null,
            "toName": null,
            "subject": null,
            "body": null,
            "isHtml": null,
            "emailType": null,
            "status": null,
            "retryCount": null,
            "maxRetries": null,
            "createdAt": null,
            "scheduledAt": null,
            "sentAt": null,
            "nextRetryAt": null,
            "errorMessage": null,
            "priority": null,
            "userId": null,
            "templateData": null,
            "user": null
          }
        ]
      },
      "repairServiceItem": {
        "itemId": 0,
        "name": "string",
        "repairType": 0,
        "price": 0.1,
        "estimatedTime": "string",
        "repairRequests": [
          {
            "requestId": null,
            "applicationUserId": null,
            "itemId": null,
            "laptopId": null,
            "notes": null,
            "status": null,
            "requestDate": null,
            "user": null,
            "repairServiceItem": null,
            "laptop": null
          }
        ]
      },
      "laptop": {
        "id": 0,
        "modelName": "string",
        "processor": "string",
        "gpu": "string",
        "screenSize": "string",
        "hasCamera": true,
        "hasKeyboard": true,
        "hasTouchScreen": true,
        "ports": "string",
        "description": "string",
        "notes": "string",
        "warranty": "string",
        "brandId": 0,
        "categoryId": 0,
        "brand": {
          "id": 0,
          "name": "string",
          "country": "string",
          "logoUrl": "string",
          "description": "string",
          "laptops": [
            null
          ]
        },
        "category": {
          "id": 0,
          "name": "string",
          "description": "string",
          "laptops": [
            null
          ]
        },
        "variants": [
          {
            "id": null,
            "laptopId": null,
            "discountId": null,
            "ram": null,
            "storage": null,
            "price": null,
            "stockQuantity": null,
            "laptop": null,
            "discount": null,
            "orderItems": null,
            "cartItems": null
          }
        ],
        "images": [
          {
            "id": null,
            "laptopId": null,
            "imageUrl": null,
            "isMain": null,
            "uploadedAt": null,
            "laptop": null
          }
        ],
        "ratings": [
          {
            "id": null,
            "userId": null,
            "laptopId": null,
            "stars": null,
            "comment": null,
            "createdAt": null,
            "user": null,
            "laptop": null
          }
        ],
        "repairRequests": [
          {
            "requestId": null,
            "applicationUserId": null,
            "itemId": null,
            "laptopId": null,
            "notes": null,
            "status": null,
            "requestDate": null,
            "user": null,
            "repairServiceItem": null,
            "laptop": null
          }
        ]
      }
    }
  ]
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|id|integer(int32)|false|none||none|
|modelName|string|true|none||none|
|processor|string¦null|false|none||none|
|gpu|string¦null|false|none||none|
|screenSize|string¦null|false|none||none|
|hasCamera|boolean|false|none||none|
|hasKeyboard|boolean|false|none||none|
|hasTouchScreen|boolean|false|none||none|
|ports|string¦null|false|none||none|
|description|string¦null|false|none||none|
|notes|string¦null|false|none||none|
|warranty|string¦null|false|none||none|
|brandId|integer(int32)|false|none||none|
|categoryId|integer(int32)|false|none||none|
|brand|[Brand](#schemabrand)|false|none||none|
|category|[Category](#schemacategory)|false|none||none|
|variants|[[LaptopVariant](#schemalaptopvariant)]¦null|false|none||none|
|images|[[LaptopImage](#schemalaptopimage)]¦null|false|none||none|
|ratings|[[Rating](#schemarating)]¦null|false|none||none|
|repairRequests|[[RepairRequest](#schemarepairrequest)]¦null|false|none||none|

<h2 id="tocS_LaptopIEnumerableServiceResponse">LaptopIEnumerableServiceResponse</h2>

<a id="schemalaptopienumerableserviceresponse"></a>
<a id="schema_LaptopIEnumerableServiceResponse"></a>
<a id="tocSlaptopienumerableserviceresponse"></a>
<a id="tocslaptopienumerableserviceresponse"></a>

```json
{
  "isSuccess": true,
  "message": "string",
  "messageAr": "string",
  "data": [
    {
      "id": 0,
      "modelName": "string",
      "processor": "string",
      "gpu": "string",
      "screenSize": "string",
      "hasCamera": true,
      "hasKeyboard": true,
      "hasTouchScreen": true,
      "ports": "string",
      "description": "string",
      "notes": "string",
      "warranty": "string",
      "brandId": 0,
      "categoryId": 0,
      "brand": {
        "id": 0,
        "name": "string",
        "country": "string",
        "logoUrl": "string",
        "description": "string",
        "laptops": [
          {
            "id": null,
            "modelName": null,
            "processor": null,
            "gpu": null,
            "screenSize": null,
            "hasCamera": null,
            "hasKeyboard": null,
            "hasTouchScreen": null,
            "ports": null,
            "description": null,
            "notes": null,
            "warranty": null,
            "brandId": null,
            "categoryId": null,
            "brand": null,
            "category": null,
            "variants": null,
            "images": null,
            "ratings": null,
            "repairRequests": null
          }
        ]
      },
      "category": {
        "id": 0,
        "name": "string",
        "description": "string",
        "laptops": [
          {
            "id": null,
            "modelName": null,
            "processor": null,
            "gpu": null,
            "screenSize": null,
            "hasCamera": null,
            "hasKeyboard": null,
            "hasTouchScreen": null,
            "ports": null,
            "description": null,
            "notes": null,
            "warranty": null,
            "brandId": null,
            "categoryId": null,
            "brand": null,
            "category": null,
            "variants": null,
            "images": null,
            "ratings": null,
            "repairRequests": null
          }
        ]
      },
      "variants": [
        {
          "id": 0,
          "laptopId": 0,
          "discountId": 0,
          "ram": 2147483647,
          "storage": 2147483647,
          "price": 0.1,
          "stockQuantity": 2147483647,
          "laptop": {
            "id": null,
            "modelName": null,
            "processor": null,
            "gpu": null,
            "screenSize": null,
            "hasCamera": null,
            "hasKeyboard": null,
            "hasTouchScreen": null,
            "ports": null,
            "description": null,
            "notes": null,
            "warranty": null,
            "brandId": null,
            "categoryId": null,
            "brand": null,
            "category": null,
            "variants": null,
            "images": null,
            "ratings": null,
            "repairRequests": null
          },
          "discount": {
            "id": null,
            "title": null,
            "description": null,
            "percentage": null,
            "startDate": null,
            "endDate": null,
            "isActive": null,
            "laptopVariants": null
          },
          "orderItems": [
            {}
          ],
          "cartItems": [
            {}
          ]
        }
      ],
      "images": [
        {
          "id": 0,
          "laptopId": 0,
          "imageUrl": "string",
          "isMain": true,
          "uploadedAt": "2019-08-24T14:15:22Z",
          "laptop": {
            "id": null,
            "modelName": null,
            "processor": null,
            "gpu": null,
            "screenSize": null,
            "hasCamera": null,
            "hasKeyboard": null,
            "hasTouchScreen": null,
            "ports": null,
            "description": null,
            "notes": null,
            "warranty": null,
            "brandId": null,
            "categoryId": null,
            "brand": null,
            "category": null,
            "variants": null,
            "images": null,
            "ratings": null,
            "repairRequests": null
          }
        }
      ],
      "ratings": [
        {
          "id": 0,
          "userId": "string",
          "laptopId": 0,
          "stars": 1,
          "comment": "string",
          "createdAt": "2019-08-24T14:15:22Z",
          "user": {
            "id": null,
            "userName": null,
            "normalizedUserName": null,
            "email": null,
            "normalizedEmail": null,
            "emailConfirmed": null,
            "passwordHash": null,
            "securityStamp": null,
            "concurrencyStamp": null,
            "phoneNumber": null,
            "phoneNumberConfirmed": null,
            "twoFactorEnabled": null,
            "lockoutEnd": null,
            "lockoutEnabled": null,
            "accessFailedCount": null,
            "fullName": null,
            "phone": null,
            "role": null,
            "isActive": null,
            "createdAt": null,
            "orders": null,
            "ratings": null,
            "cartItems": null,
            "repairRequests": null,
            "auditLogs": null,
            "refreshTokens": null,
            "verificationCodes": null,
            "emailQueue": null
          },
          "laptop": {
            "id": null,
            "modelName": null,
            "processor": null,
            "gpu": null,
            "screenSize": null,
            "hasCamera": null,
            "hasKeyboard": null,
            "hasTouchScreen": null,
            "ports": null,
            "description": null,
            "notes": null,
            "warranty": null,
            "brandId": null,
            "categoryId": null,
            "brand": null,
            "category": null,
            "variants": null,
            "images": null,
            "ratings": null,
            "repairRequests": null
          }
        }
      ],
      "repairRequests": [
        {
          "requestId": 0,
          "applicationUserId": "string",
          "itemId": 0,
          "laptopId": 0,
          "notes": "string",
          "status": 0,
          "requestDate": "2019-08-24T14:15:22Z",
          "user": {
            "id": null,
            "userName": null,
            "normalizedUserName": null,
            "email": null,
            "normalizedEmail": null,
            "emailConfirmed": null,
            "passwordHash": null,
            "securityStamp": null,
            "concurrencyStamp": null,
            "phoneNumber": null,
            "phoneNumberConfirmed": null,
            "twoFactorEnabled": null,
            "lockoutEnd": null,
            "lockoutEnabled": null,
            "accessFailedCount": null,
            "fullName": null,
            "phone": null,
            "role": null,
            "isActive": null,
            "createdAt": null,
            "orders": null,
            "ratings": null,
            "cartItems": null,
            "repairRequests": null,
            "auditLogs": null,
            "refreshTokens": null,
            "verificationCodes": null,
            "emailQueue": null
          },
          "repairServiceItem": {
            "itemId": null,
            "name": null,
            "repairType": null,
            "price": null,
            "estimatedTime": null,
            "repairRequests": null
          },
          "laptop": {
            "id": null,
            "modelName": null,
            "processor": null,
            "gpu": null,
            "screenSize": null,
            "hasCamera": null,
            "hasKeyboard": null,
            "hasTouchScreen": null,
            "ports": null,
            "description": null,
            "notes": null,
            "warranty": null,
            "brandId": null,
            "categoryId": null,
            "brand": null,
            "category": null,
            "variants": null,
            "images": null,
            "ratings": null,
            "repairRequests": null
          }
        }
      ]
    }
  ],
  "errors": [
    "string"
  ],
  "statusCode": 0,
  "timestamp": "2019-08-24T14:15:22Z"
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|isSuccess|boolean|false|none||none|
|message|string¦null|false|none||none|
|messageAr|string¦null|false|none||none|
|data|[[Laptop](#schemalaptop)]¦null|false|none||none|
|errors|[string]¦null|false|none||none|
|statusCode|integer(int32)|false|none||none|
|timestamp|string(date-time)|false|none||none|

<h2 id="tocS_LaptopImage">LaptopImage</h2>

<a id="schemalaptopimage"></a>
<a id="schema_LaptopImage"></a>
<a id="tocSlaptopimage"></a>
<a id="tocslaptopimage"></a>

```json
{
  "id": 0,
  "laptopId": 0,
  "imageUrl": "string",
  "isMain": true,
  "uploadedAt": "2019-08-24T14:15:22Z",
  "laptop": {
    "id": 0,
    "modelName": "string",
    "processor": "string",
    "gpu": "string",
    "screenSize": "string",
    "hasCamera": true,
    "hasKeyboard": true,
    "hasTouchScreen": true,
    "ports": "string",
    "description": "string",
    "notes": "string",
    "warranty": "string",
    "brandId": 0,
    "categoryId": 0,
    "brand": {
      "id": 0,
      "name": "string",
      "country": "string",
      "logoUrl": "string",
      "description": "string",
      "laptops": [
        {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      ]
    },
    "category": {
      "id": 0,
      "name": "string",
      "description": "string",
      "laptops": [
        {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      ]
    },
    "variants": [
      {
        "id": 0,
        "laptopId": 0,
        "discountId": 0,
        "ram": 2147483647,
        "storage": 2147483647,
        "price": 0.1,
        "stockQuantity": 2147483647,
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        },
        "discount": {
          "id": 0,
          "title": "string",
          "description": "string",
          "percentage": 100,
          "startDate": "2019-08-24T14:15:22Z",
          "endDate": "2019-08-24T14:15:22Z",
          "isActive": true,
          "laptopVariants": [
            null
          ]
        },
        "orderItems": [
          {
            "id": null,
            "orderId": null,
            "order": null,
            "laptopVariantId": null,
            "laptopVariant": null,
            "quantity": null,
            "unitPrice": null
          }
        ],
        "cartItems": [
          {
            "id": null,
            "applicationUserId": null,
            "laptopVariantId": null,
            "quantity": null,
            "addedAt": null,
            "user": null,
            "laptopVariant": null
          }
        ]
      }
    ],
    "images": [
      {
        "id": 0,
        "laptopId": 0,
        "imageUrl": "string",
        "isMain": true,
        "uploadedAt": "2019-08-24T14:15:22Z",
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      }
    ],
    "ratings": [
      {
        "id": 0,
        "userId": "string",
        "laptopId": 0,
        "stars": 1,
        "comment": "string",
        "createdAt": "2019-08-24T14:15:22Z",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      }
    ],
    "repairRequests": [
      {
        "requestId": 0,
        "applicationUserId": "string",
        "itemId": 0,
        "laptopId": 0,
        "notes": "string",
        "status": 0,
        "requestDate": "2019-08-24T14:15:22Z",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "repairServiceItem": {
          "itemId": 0,
          "name": "string",
          "repairType": "[",
          "price": 0.1,
          "estimatedTime": "string",
          "repairRequests": [
            null
          ]
        },
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      }
    ]
  }
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|id|integer(int32)|false|none||none|
|laptopId|integer(int32)|false|none||none|
|imageUrl|string|true|none||none|
|isMain|boolean|false|none||none|
|uploadedAt|string(date-time)|false|none||none|
|laptop|[Laptop](#schemalaptop)|false|none||none|

<h2 id="tocS_LaptopResponseDTO">LaptopResponseDTO</h2>

<a id="schemalaptopresponsedto"></a>
<a id="schema_LaptopResponseDTO"></a>
<a id="tocSlaptopresponsedto"></a>
<a id="tocslaptopresponsedto"></a>

```json
{
  "id": 0,
  "name": "string",
  "price": 0.1,
  "category": "string",
  "images": [
    "string"
  ],
  "rate": 0.1,
  "reviewsCount": 0,
  "isDiscounted": true,
  "discountedPrice": 0.1,
  "shortDescription": "string"
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|id|integer(int32)|false|none||none|
|name|string¦null|false|none||none|
|price|number(double)|false|none||none|
|category|string¦null|false|none||none|
|images|[string]¦null|false|none||none|
|rate|number(double)|false|none||none|
|reviewsCount|integer(int32)|false|none||none|
|isDiscounted|boolean|false|none||none|
|discountedPrice|number(double)¦null|false|none||none|
|shortDescription|string¦null|false|none||none|

<h2 id="tocS_LaptopResponseDTOPagedResult">LaptopResponseDTOPagedResult</h2>

<a id="schemalaptopresponsedtopagedresult"></a>
<a id="schema_LaptopResponseDTOPagedResult"></a>
<a id="tocSlaptopresponsedtopagedresult"></a>
<a id="tocslaptopresponsedtopagedresult"></a>

```json
{
  "items": [
    {
      "id": 0,
      "name": "string",
      "price": 0.1,
      "category": "string",
      "images": [
        "string"
      ],
      "rate": 0.1,
      "reviewsCount": 0,
      "isDiscounted": true,
      "discountedPrice": 0.1,
      "shortDescription": "string"
    }
  ],
  "page": 0,
  "pageSize": 0,
  "totalCount": 0,
  "totalPages": 0,
  "hasPrevious": true,
  "hasNext": true,
  "startIndex": 0,
  "endIndex": 0
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|items|[[LaptopResponseDTO](#schemalaptopresponsedto)]¦null|false|none||none|
|page|integer(int32)|false|none||none|
|pageSize|integer(int32)|false|none||none|
|totalCount|integer(int32)|false|none||none|
|totalPages|integer(int32)|false|read-only||none|
|hasPrevious|boolean|false|read-only||none|
|hasNext|boolean|false|read-only||none|
|startIndex|integer(int32)|false|read-only||none|
|endIndex|integer(int32)|false|read-only||none|

<h2 id="tocS_LaptopResponseDTOPagedResultServiceResponse">LaptopResponseDTOPagedResultServiceResponse</h2>

<a id="schemalaptopresponsedtopagedresultserviceresponse"></a>
<a id="schema_LaptopResponseDTOPagedResultServiceResponse"></a>
<a id="tocSlaptopresponsedtopagedresultserviceresponse"></a>
<a id="tocslaptopresponsedtopagedresultserviceresponse"></a>

```json
{
  "isSuccess": true,
  "message": "string",
  "messageAr": "string",
  "data": {
    "items": [
      {
        "id": 0,
        "name": "string",
        "price": 0.1,
        "category": "string",
        "images": [
          "string"
        ],
        "rate": 0.1,
        "reviewsCount": 0,
        "isDiscounted": true,
        "discountedPrice": 0.1,
        "shortDescription": "string"
      }
    ],
    "page": 0,
    "pageSize": 0,
    "totalCount": 0,
    "totalPages": 0,
    "hasPrevious": true,
    "hasNext": true,
    "startIndex": 0,
    "endIndex": 0
  },
  "errors": [
    "string"
  ],
  "statusCode": 0,
  "timestamp": "2019-08-24T14:15:22Z"
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|isSuccess|boolean|false|none||none|
|message|string¦null|false|none||none|
|messageAr|string¦null|false|none||none|
|data|[LaptopResponseDTOPagedResult](#schemalaptopresponsedtopagedresult)|false|none||none|
|errors|[string]¦null|false|none||none|
|statusCode|integer(int32)|false|none||none|
|timestamp|string(date-time)|false|none||none|

<h2 id="tocS_LaptopServiceResponse">LaptopServiceResponse</h2>

<a id="schemalaptopserviceresponse"></a>
<a id="schema_LaptopServiceResponse"></a>
<a id="tocSlaptopserviceresponse"></a>
<a id="tocslaptopserviceresponse"></a>

```json
{
  "isSuccess": true,
  "message": "string",
  "messageAr": "string",
  "data": {
    "id": 0,
    "modelName": "string",
    "processor": "string",
    "gpu": "string",
    "screenSize": "string",
    "hasCamera": true,
    "hasKeyboard": true,
    "hasTouchScreen": true,
    "ports": "string",
    "description": "string",
    "notes": "string",
    "warranty": "string",
    "brandId": 0,
    "categoryId": 0,
    "brand": {
      "id": 0,
      "name": "string",
      "country": "string",
      "logoUrl": "string",
      "description": "string",
      "laptops": [
        {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      ]
    },
    "category": {
      "id": 0,
      "name": "string",
      "description": "string",
      "laptops": [
        {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      ]
    },
    "variants": [
      {
        "id": 0,
        "laptopId": 0,
        "discountId": 0,
        "ram": 2147483647,
        "storage": 2147483647,
        "price": 0.1,
        "stockQuantity": 2147483647,
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        },
        "discount": {
          "id": 0,
          "title": "string",
          "description": "string",
          "percentage": 100,
          "startDate": "2019-08-24T14:15:22Z",
          "endDate": "2019-08-24T14:15:22Z",
          "isActive": true,
          "laptopVariants": [
            null
          ]
        },
        "orderItems": [
          {
            "id": null,
            "orderId": null,
            "order": null,
            "laptopVariantId": null,
            "laptopVariant": null,
            "quantity": null,
            "unitPrice": null
          }
        ],
        "cartItems": [
          {
            "id": null,
            "applicationUserId": null,
            "laptopVariantId": null,
            "quantity": null,
            "addedAt": null,
            "user": null,
            "laptopVariant": null
          }
        ]
      }
    ],
    "images": [
      {
        "id": 0,
        "laptopId": 0,
        "imageUrl": "string",
        "isMain": true,
        "uploadedAt": "2019-08-24T14:15:22Z",
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      }
    ],
    "ratings": [
      {
        "id": 0,
        "userId": "string",
        "laptopId": 0,
        "stars": 1,
        "comment": "string",
        "createdAt": "2019-08-24T14:15:22Z",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      }
    ],
    "repairRequests": [
      {
        "requestId": 0,
        "applicationUserId": "string",
        "itemId": 0,
        "laptopId": 0,
        "notes": "string",
        "status": 0,
        "requestDate": "2019-08-24T14:15:22Z",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "repairServiceItem": {
          "itemId": 0,
          "name": "string",
          "repairType": "[",
          "price": 0.1,
          "estimatedTime": "string",
          "repairRequests": [
            null
          ]
        },
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      }
    ]
  },
  "errors": [
    "string"
  ],
  "statusCode": 0,
  "timestamp": "2019-08-24T14:15:22Z"
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|isSuccess|boolean|false|none||none|
|message|string¦null|false|none||none|
|messageAr|string¦null|false|none||none|
|data|[Laptop](#schemalaptop)|false|none||none|
|errors|[string]¦null|false|none||none|
|statusCode|integer(int32)|false|none||none|
|timestamp|string(date-time)|false|none||none|

<h2 id="tocS_LaptopSortBy">LaptopSortBy</h2>

<a id="schemalaptopsortby"></a>
<a id="schema_LaptopSortBy"></a>
<a id="tocSlaptopsortby"></a>
<a id="tocslaptopsortby"></a>

```json
0

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|*anonymous*|integer(int32)|false|none||none|

#### Enum

|Name|Value|
|---|---|
|*anonymous*|0|
|*anonymous*|1|
|*anonymous*|2|

<h2 id="tocS_LaptopVariant">LaptopVariant</h2>

<a id="schemalaptopvariant"></a>
<a id="schema_LaptopVariant"></a>
<a id="tocSlaptopvariant"></a>
<a id="tocslaptopvariant"></a>

```json
{
  "id": 0,
  "laptopId": 0,
  "discountId": 0,
  "ram": 2147483647,
  "storage": 2147483647,
  "price": 0.1,
  "stockQuantity": 2147483647,
  "laptop": {
    "id": 0,
    "modelName": "string",
    "processor": "string",
    "gpu": "string",
    "screenSize": "string",
    "hasCamera": true,
    "hasKeyboard": true,
    "hasTouchScreen": true,
    "ports": "string",
    "description": "string",
    "notes": "string",
    "warranty": "string",
    "brandId": 0,
    "categoryId": 0,
    "brand": {
      "id": 0,
      "name": "string",
      "country": "string",
      "logoUrl": "string",
      "description": "string",
      "laptops": [
        {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      ]
    },
    "category": {
      "id": 0,
      "name": "string",
      "description": "string",
      "laptops": [
        {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      ]
    },
    "variants": [
      {
        "id": 0,
        "laptopId": 0,
        "discountId": 0,
        "ram": 2147483647,
        "storage": 2147483647,
        "price": 0.1,
        "stockQuantity": 2147483647,
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        },
        "discount": {
          "id": 0,
          "title": "string",
          "description": "string",
          "percentage": 100,
          "startDate": "2019-08-24T14:15:22Z",
          "endDate": "2019-08-24T14:15:22Z",
          "isActive": true,
          "laptopVariants": [
            null
          ]
        },
        "orderItems": [
          {
            "id": null,
            "orderId": null,
            "order": null,
            "laptopVariantId": null,
            "laptopVariant": null,
            "quantity": null,
            "unitPrice": null
          }
        ],
        "cartItems": [
          {
            "id": null,
            "applicationUserId": null,
            "laptopVariantId": null,
            "quantity": null,
            "addedAt": null,
            "user": null,
            "laptopVariant": null
          }
        ]
      }
    ],
    "images": [
      {
        "id": 0,
        "laptopId": 0,
        "imageUrl": "string",
        "isMain": true,
        "uploadedAt": "2019-08-24T14:15:22Z",
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      }
    ],
    "ratings": [
      {
        "id": 0,
        "userId": "string",
        "laptopId": 0,
        "stars": 1,
        "comment": "string",
        "createdAt": "2019-08-24T14:15:22Z",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      }
    ],
    "repairRequests": [
      {
        "requestId": 0,
        "applicationUserId": "string",
        "itemId": 0,
        "laptopId": 0,
        "notes": "string",
        "status": 0,
        "requestDate": "2019-08-24T14:15:22Z",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "repairServiceItem": {
          "itemId": 0,
          "name": "string",
          "repairType": "[",
          "price": 0.1,
          "estimatedTime": "string",
          "repairRequests": [
            null
          ]
        },
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      }
    ]
  },
  "discount": {
    "id": 0,
    "title": "string",
    "description": "string",
    "percentage": 100,
    "startDate": "2019-08-24T14:15:22Z",
    "endDate": "2019-08-24T14:15:22Z",
    "isActive": true,
    "laptopVariants": [
      {
        "id": 0,
        "laptopId": 0,
        "discountId": 0,
        "ram": 2147483647,
        "storage": 2147483647,
        "price": 0.1,
        "stockQuantity": 2147483647,
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        },
        "discount": {
          "id": 0,
          "title": "string",
          "description": "string",
          "percentage": 100,
          "startDate": "2019-08-24T14:15:22Z",
          "endDate": "2019-08-24T14:15:22Z",
          "isActive": true,
          "laptopVariants": [
            null
          ]
        },
        "orderItems": [
          {
            "id": null,
            "orderId": null,
            "order": null,
            "laptopVariantId": null,
            "laptopVariant": null,
            "quantity": null,
            "unitPrice": null
          }
        ],
        "cartItems": [
          {
            "id": null,
            "applicationUserId": null,
            "laptopVariantId": null,
            "quantity": null,
            "addedAt": null,
            "user": null,
            "laptopVariant": null
          }
        ]
      }
    ]
  },
  "orderItems": [
    {
      "id": 0,
      "orderId": 0,
      "order": {
        "id": 0,
        "orderDate": "2019-08-24T14:15:22Z",
        "totalAmount": 0.1,
        "status": 0,
        "userId": "string",
        "applicationUser": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "items": [
          {
            "id": null,
            "orderId": null,
            "order": null,
            "laptopVariantId": null,
            "laptopVariant": null,
            "quantity": null,
            "unitPrice": null
          }
        ],
        "payments": [
          {
            "id": null,
            "orderId": null,
            "paymentMethod": null,
            "paymentStatus": null,
            "transactionId": null,
            "paidAt": null,
            "order": null
          }
        ],
        "shipping": {
          "id": 0,
          "orderId": 0,
          "address": "string",
          "city": "string",
          "country": "string",
          "postalCode": "string",
          "trackingNumber": "string",
          "shippedAt": "2019-08-24T14:15:22Z",
          "deliveredAt": "2019-08-24T14:15:22Z",
          "order": {}
        }
      },
      "laptopVariantId": 0,
      "laptopVariant": {
        "id": 0,
        "laptopId": 0,
        "discountId": 0,
        "ram": 2147483647,
        "storage": 2147483647,
        "price": 0.1,
        "stockQuantity": 2147483647,
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        },
        "discount": {
          "id": 0,
          "title": "string",
          "description": "string",
          "percentage": 100,
          "startDate": "2019-08-24T14:15:22Z",
          "endDate": "2019-08-24T14:15:22Z",
          "isActive": true,
          "laptopVariants": [
            null
          ]
        },
        "orderItems": [
          {
            "id": null,
            "orderId": null,
            "order": null,
            "laptopVariantId": null,
            "laptopVariant": null,
            "quantity": null,
            "unitPrice": null
          }
        ],
        "cartItems": [
          {
            "id": null,
            "applicationUserId": null,
            "laptopVariantId": null,
            "quantity": null,
            "addedAt": null,
            "user": null,
            "laptopVariant": null
          }
        ]
      },
      "quantity": 0,
      "unitPrice": 0.1
    }
  ],
  "cartItems": [
    {
      "id": 0,
      "applicationUserId": "string",
      "laptopVariantId": 0,
      "quantity": 1,
      "addedAt": "2019-08-24T14:15:22Z",
      "user": {
        "id": "string",
        "userName": "string",
        "normalizedUserName": "string",
        "email": "string",
        "normalizedEmail": "string",
        "emailConfirmed": true,
        "passwordHash": "string",
        "securityStamp": "string",
        "concurrencyStamp": "string",
        "phoneNumber": "string",
        "phoneNumberConfirmed": true,
        "twoFactorEnabled": true,
        "lockoutEnd": "2019-08-24T14:15:22Z",
        "lockoutEnabled": true,
        "accessFailedCount": 0,
        "fullName": "string",
        "phone": "string",
        "role": "string",
        "isActive": true,
        "createdAt": "2019-08-24T14:15:22Z",
        "orders": [
          {
            "id": null,
            "orderDate": null,
            "totalAmount": null,
            "status": null,
            "userId": null,
            "applicationUser": null,
            "items": null,
            "payments": null,
            "shipping": null
          }
        ],
        "ratings": [
          {
            "id": null,
            "userId": null,
            "laptopId": null,
            "stars": null,
            "comment": null,
            "createdAt": null,
            "user": null,
            "laptop": null
          }
        ],
        "cartItems": [
          {
            "id": null,
            "applicationUserId": null,
            "laptopVariantId": null,
            "quantity": null,
            "addedAt": null,
            "user": null,
            "laptopVariant": null
          }
        ],
        "repairRequests": [
          {
            "requestId": null,
            "applicationUserId": null,
            "itemId": null,
            "laptopId": null,
            "notes": null,
            "status": null,
            "requestDate": null,
            "user": null,
            "repairServiceItem": null,
            "laptop": null
          }
        ],
        "auditLogs": [
          {
            "id": null,
            "applicationUserId": null,
            "action": null,
            "entity": null,
            "entityId": null,
            "timestamp": null,
            "details": null,
            "user": null
          }
        ],
        "refreshTokens": [
          {
            "token": null,
            "expiresOn": null,
            "isExpired": null,
            "createdOn": null,
            "revokedOn": null,
            "isActive": null
          }
        ],
        "verificationCodes": [
          {
            "id": null,
            "userId": null,
            "code": null,
            "type": null,
            "destination": null,
            "isUsed": null,
            "createdAt": null,
            "expiryDate": null,
            "attemptCount": null,
            "maxAttempts": null,
            "user": null
          }
        ],
        "emailQueue": [
          {
            "id": null,
            "toEmail": null,
            "toName": null,
            "subject": null,
            "body": null,
            "isHtml": null,
            "emailType": null,
            "status": null,
            "retryCount": null,
            "maxRetries": null,
            "createdAt": null,
            "scheduledAt": null,
            "sentAt": null,
            "nextRetryAt": null,
            "errorMessage": null,
            "priority": null,
            "userId": null,
            "templateData": null,
            "user": null
          }
        ]
      },
      "laptopVariant": {
        "id": 0,
        "laptopId": 0,
        "discountId": 0,
        "ram": 2147483647,
        "storage": 2147483647,
        "price": 0.1,
        "stockQuantity": 2147483647,
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        },
        "discount": {
          "id": 0,
          "title": "string",
          "description": "string",
          "percentage": 100,
          "startDate": "2019-08-24T14:15:22Z",
          "endDate": "2019-08-24T14:15:22Z",
          "isActive": true,
          "laptopVariants": [
            null
          ]
        },
        "orderItems": [
          {
            "id": null,
            "orderId": null,
            "order": null,
            "laptopVariantId": null,
            "laptopVariant": null,
            "quantity": null,
            "unitPrice": null
          }
        ],
        "cartItems": [
          {
            "id": null,
            "applicationUserId": null,
            "laptopVariantId": null,
            "quantity": null,
            "addedAt": null,
            "user": null,
            "laptopVariant": null
          }
        ]
      }
    }
  ]
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|id|integer(int32)|false|none||none|
|laptopId|integer(int32)|false|none||none|
|discountId|integer(int32)¦null|false|none||none|
|ram|integer(int32)|false|none||none|
|storage|integer(int32)|false|none||none|
|price|number(double)|false|none||none|
|stockQuantity|integer(int32)|false|none||none|
|laptop|[Laptop](#schemalaptop)|false|none||none|
|discount|[Discount](#schemadiscount)|false|none||none|
|orderItems|[[OrderItem](#schemaorderitem)]¦null|false|none||none|
|cartItems|[[CartItem](#schemacartitem)]¦null|false|none||none|

<h2 id="tocS_ObjectServiceResponse">ObjectServiceResponse</h2>

<a id="schemaobjectserviceresponse"></a>
<a id="schema_ObjectServiceResponse"></a>
<a id="tocSobjectserviceresponse"></a>
<a id="tocsobjectserviceresponse"></a>

```json
{
  "isSuccess": true,
  "message": "string",
  "messageAr": "string",
  "data": null,
  "errors": [
    "string"
  ],
  "statusCode": 0,
  "timestamp": "2019-08-24T14:15:22Z"
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|isSuccess|boolean|false|none||none|
|message|string¦null|false|none||none|
|messageAr|string¦null|false|none||none|
|data|null|false|none||none|
|errors|[string]¦null|false|none||none|
|statusCode|integer(int32)|false|none||none|
|timestamp|string(date-time)|false|none||none|

<h2 id="tocS_Order">Order</h2>

<a id="schemaorder"></a>
<a id="schema_Order"></a>
<a id="tocSorder"></a>
<a id="tocsorder"></a>

```json
{
  "id": 0,
  "orderDate": "2019-08-24T14:15:22Z",
  "totalAmount": 0.1,
  "status": 0,
  "userId": "string",
  "applicationUser": {
    "id": "string",
    "userName": "string",
    "normalizedUserName": "string",
    "email": "string",
    "normalizedEmail": "string",
    "emailConfirmed": true,
    "passwordHash": "string",
    "securityStamp": "string",
    "concurrencyStamp": "string",
    "phoneNumber": "string",
    "phoneNumberConfirmed": true,
    "twoFactorEnabled": true,
    "lockoutEnd": "2019-08-24T14:15:22Z",
    "lockoutEnabled": true,
    "accessFailedCount": 0,
    "fullName": "string",
    "phone": "string",
    "role": "string",
    "isActive": true,
    "createdAt": "2019-08-24T14:15:22Z",
    "orders": [
      {
        "id": 0,
        "orderDate": "2019-08-24T14:15:22Z",
        "totalAmount": 0.1,
        "status": 0,
        "userId": "string",
        "applicationUser": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "items": [
          {
            "id": null,
            "orderId": null,
            "order": null,
            "laptopVariantId": null,
            "laptopVariant": null,
            "quantity": null,
            "unitPrice": null
          }
        ],
        "payments": [
          {
            "id": null,
            "orderId": null,
            "paymentMethod": null,
            "paymentStatus": null,
            "transactionId": null,
            "paidAt": null,
            "order": null
          }
        ],
        "shipping": {
          "id": 0,
          "orderId": 0,
          "address": "string",
          "city": "string",
          "country": "string",
          "postalCode": "string",
          "trackingNumber": "string",
          "shippedAt": "2019-08-24T14:15:22Z",
          "deliveredAt": "2019-08-24T14:15:22Z",
          "order": {}
        }
      }
    ],
    "ratings": [
      {
        "id": 0,
        "userId": "string",
        "laptopId": 0,
        "stars": 1,
        "comment": "string",
        "createdAt": "2019-08-24T14:15:22Z",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      }
    ],
    "cartItems": [
      {
        "id": 0,
        "applicationUserId": "string",
        "laptopVariantId": 0,
        "quantity": 1,
        "addedAt": "2019-08-24T14:15:22Z",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "laptopVariant": {
          "id": 0,
          "laptopId": 0,
          "discountId": 0,
          "ram": 2147483647,
          "storage": 2147483647,
          "price": 0.1,
          "stockQuantity": 2147483647,
          "laptop": {},
          "discount": {},
          "orderItems": [
            null
          ],
          "cartItems": [
            null
          ]
        }
      }
    ],
    "repairRequests": [
      {
        "requestId": 0,
        "applicationUserId": "string",
        "itemId": 0,
        "laptopId": 0,
        "notes": "string",
        "status": 0,
        "requestDate": "2019-08-24T14:15:22Z",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "repairServiceItem": {
          "itemId": 0,
          "name": "string",
          "repairType": "[",
          "price": 0.1,
          "estimatedTime": "string",
          "repairRequests": [
            null
          ]
        },
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      }
    ],
    "auditLogs": [
      {
        "id": 0,
        "applicationUserId": "string",
        "action": "string",
        "entity": "string",
        "entityId": 0,
        "timestamp": "2019-08-24T14:15:22Z",
        "details": "string",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        }
      }
    ],
    "refreshTokens": [
      {
        "token": "string",
        "expiresOn": "2019-08-24T14:15:22Z",
        "isExpired": true,
        "createdOn": "2019-08-24T14:15:22Z",
        "revokedOn": "2019-08-24T14:15:22Z",
        "isActive": true
      }
    ],
    "verificationCodes": [
      {
        "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
        "userId": "string",
        "code": "string",
        "type": 0,
        "destination": 0,
        "isUsed": true,
        "createdAt": "2019-08-24T14:15:22Z",
        "expiryDate": "2019-08-24T14:15:22Z",
        "attemptCount": 10,
        "maxAttempts": 1,
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        }
      }
    ],
    "emailQueue": [
      {
        "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
        "toEmail": "string",
        "toName": "string",
        "subject": "string",
        "body": "string",
        "isHtml": true,
        "emailType": 0,
        "status": 0,
        "retryCount": 0,
        "maxRetries": 0,
        "createdAt": "2019-08-24T14:15:22Z",
        "scheduledAt": "2019-08-24T14:15:22Z",
        "sentAt": "2019-08-24T14:15:22Z",
        "nextRetryAt": "2019-08-24T14:15:22Z",
        "errorMessage": "string",
        "priority": 0,
        "userId": "string",
        "templateData": "string",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        }
      }
    ]
  },
  "items": [
    {
      "id": 0,
      "orderId": 0,
      "order": {
        "id": 0,
        "orderDate": "2019-08-24T14:15:22Z",
        "totalAmount": 0.1,
        "status": 0,
        "userId": "string",
        "applicationUser": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "items": [
          {
            "id": null,
            "orderId": null,
            "order": null,
            "laptopVariantId": null,
            "laptopVariant": null,
            "quantity": null,
            "unitPrice": null
          }
        ],
        "payments": [
          {
            "id": null,
            "orderId": null,
            "paymentMethod": null,
            "paymentStatus": null,
            "transactionId": null,
            "paidAt": null,
            "order": null
          }
        ],
        "shipping": {
          "id": 0,
          "orderId": 0,
          "address": "string",
          "city": "string",
          "country": "string",
          "postalCode": "string",
          "trackingNumber": "string",
          "shippedAt": "2019-08-24T14:15:22Z",
          "deliveredAt": "2019-08-24T14:15:22Z",
          "order": {}
        }
      },
      "laptopVariantId": 0,
      "laptopVariant": {
        "id": 0,
        "laptopId": 0,
        "discountId": 0,
        "ram": 2147483647,
        "storage": 2147483647,
        "price": 0.1,
        "stockQuantity": 2147483647,
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        },
        "discount": {
          "id": 0,
          "title": "string",
          "description": "string",
          "percentage": 100,
          "startDate": "2019-08-24T14:15:22Z",
          "endDate": "2019-08-24T14:15:22Z",
          "isActive": true,
          "laptopVariants": [
            null
          ]
        },
        "orderItems": [
          {
            "id": null,
            "orderId": null,
            "order": null,
            "laptopVariantId": null,
            "laptopVariant": null,
            "quantity": null,
            "unitPrice": null
          }
        ],
        "cartItems": [
          {
            "id": null,
            "applicationUserId": null,
            "laptopVariantId": null,
            "quantity": null,
            "addedAt": null,
            "user": null,
            "laptopVariant": null
          }
        ]
      },
      "quantity": 0,
      "unitPrice": 0.1
    }
  ],
  "payments": [
    {
      "id": 0,
      "orderId": 0,
      "paymentMethod": 0,
      "paymentStatus": 0,
      "transactionId": "string",
      "paidAt": "2019-08-24T14:15:22Z",
      "order": {
        "id": 0,
        "orderDate": "2019-08-24T14:15:22Z",
        "totalAmount": 0.1,
        "status": 0,
        "userId": "string",
        "applicationUser": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "items": [
          {
            "id": null,
            "orderId": null,
            "order": null,
            "laptopVariantId": null,
            "laptopVariant": null,
            "quantity": null,
            "unitPrice": null
          }
        ],
        "payments": [
          {
            "id": null,
            "orderId": null,
            "paymentMethod": null,
            "paymentStatus": null,
            "transactionId": null,
            "paidAt": null,
            "order": null
          }
        ],
        "shipping": {
          "id": 0,
          "orderId": 0,
          "address": "string",
          "city": "string",
          "country": "string",
          "postalCode": "string",
          "trackingNumber": "string",
          "shippedAt": "2019-08-24T14:15:22Z",
          "deliveredAt": "2019-08-24T14:15:22Z",
          "order": {}
        }
      }
    }
  ],
  "shipping": {
    "id": 0,
    "orderId": 0,
    "address": "string",
    "city": "string",
    "country": "string",
    "postalCode": "string",
    "trackingNumber": "string",
    "shippedAt": "2019-08-24T14:15:22Z",
    "deliveredAt": "2019-08-24T14:15:22Z",
    "order": {
      "id": 0,
      "orderDate": "2019-08-24T14:15:22Z",
      "totalAmount": 0.1,
      "status": 0,
      "userId": "string",
      "applicationUser": {
        "id": "string",
        "userName": "string",
        "normalizedUserName": "string",
        "email": "string",
        "normalizedEmail": "string",
        "emailConfirmed": true,
        "passwordHash": "string",
        "securityStamp": "string",
        "concurrencyStamp": "string",
        "phoneNumber": "string",
        "phoneNumberConfirmed": true,
        "twoFactorEnabled": true,
        "lockoutEnd": "2019-08-24T14:15:22Z",
        "lockoutEnabled": true,
        "accessFailedCount": 0,
        "fullName": "string",
        "phone": "string",
        "role": "string",
        "isActive": true,
        "createdAt": "2019-08-24T14:15:22Z",
        "orders": [
          {}
        ],
        "ratings": [
          {}
        ],
        "cartItems": [
          {}
        ],
        "repairRequests": [
          {}
        ],
        "auditLogs": [
          {}
        ],
        "refreshTokens": [
          {}
        ],
        "verificationCodes": [
          {}
        ],
        "emailQueue": [
          {}
        ]
      },
      "items": [
        {
          "id": 0,
          "orderId": 0,
          "order": {},
          "laptopVariantId": 0,
          "laptopVariant": {},
          "quantity": 0,
          "unitPrice": 0.1
        }
      ],
      "payments": [
        {
          "id": 0,
          "orderId": 0,
          "paymentMethod": "[",
          "paymentStatus": "[",
          "transactionId": "string",
          "paidAt": "2019-08-24T14:15:22Z",
          "order": {}
        }
      ],
      "shipping": {
        "id": 0,
        "orderId": 0,
        "address": "string",
        "city": "string",
        "country": "string",
        "postalCode": "string",
        "trackingNumber": "string",
        "shippedAt": "2019-08-24T14:15:22Z",
        "deliveredAt": "2019-08-24T14:15:22Z",
        "order": {
          "id": null,
          "orderDate": null,
          "totalAmount": null,
          "status": null,
          "userId": null,
          "applicationUser": null,
          "items": null,
          "payments": null,
          "shipping": null
        }
      }
    }
  }
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|id|integer(int32)|false|none||none|
|orderDate|string(date-time)|false|none||none|
|totalAmount|number(double)|false|none||none|
|status|[OrderStatus](#schemaorderstatus)|true|none||none|
|userId|string¦null|false|none||none|
|applicationUser|[ApplicationUser](#schemaapplicationuser)|false|none||none|
|items|[[OrderItem](#schemaorderitem)]¦null|false|none||none|
|payments|[[Payment](#schemapayment)]¦null|false|none||none|
|shipping|[Shipping](#schemashipping)|false|none||none|

<h2 id="tocS_OrderItem">OrderItem</h2>

<a id="schemaorderitem"></a>
<a id="schema_OrderItem"></a>
<a id="tocSorderitem"></a>
<a id="tocsorderitem"></a>

```json
{
  "id": 0,
  "orderId": 0,
  "order": {
    "id": 0,
    "orderDate": "2019-08-24T14:15:22Z",
    "totalAmount": 0.1,
    "status": 0,
    "userId": "string",
    "applicationUser": {
      "id": "string",
      "userName": "string",
      "normalizedUserName": "string",
      "email": "string",
      "normalizedEmail": "string",
      "emailConfirmed": true,
      "passwordHash": "string",
      "securityStamp": "string",
      "concurrencyStamp": "string",
      "phoneNumber": "string",
      "phoneNumberConfirmed": true,
      "twoFactorEnabled": true,
      "lockoutEnd": "2019-08-24T14:15:22Z",
      "lockoutEnabled": true,
      "accessFailedCount": 0,
      "fullName": "string",
      "phone": "string",
      "role": "string",
      "isActive": true,
      "createdAt": "2019-08-24T14:15:22Z",
      "orders": [
        {
          "id": 0,
          "orderDate": "2019-08-24T14:15:22Z",
          "totalAmount": 0.1,
          "status": "[",
          "userId": "string",
          "applicationUser": {},
          "items": [
            null
          ],
          "payments": [
            null
          ],
          "shipping": {}
        }
      ],
      "ratings": [
        {
          "id": 0,
          "userId": "string",
          "laptopId": 0,
          "stars": 1,
          "comment": "string",
          "createdAt": "2019-08-24T14:15:22Z",
          "user": {},
          "laptop": {}
        }
      ],
      "cartItems": [
        {
          "id": 0,
          "applicationUserId": "string",
          "laptopVariantId": 0,
          "quantity": 1,
          "addedAt": "2019-08-24T14:15:22Z",
          "user": {},
          "laptopVariant": {}
        }
      ],
      "repairRequests": [
        {
          "requestId": 0,
          "applicationUserId": "string",
          "itemId": 0,
          "laptopId": 0,
          "notes": "string",
          "status": "[",
          "requestDate": "2019-08-24T14:15:22Z",
          "user": {},
          "repairServiceItem": {},
          "laptop": {}
        }
      ],
      "auditLogs": [
        {
          "id": 0,
          "applicationUserId": "string",
          "action": "string",
          "entity": "string",
          "entityId": 0,
          "timestamp": "2019-08-24T14:15:22Z",
          "details": "string",
          "user": {}
        }
      ],
      "refreshTokens": [
        {
          "token": "string",
          "expiresOn": "2019-08-24T14:15:22Z",
          "isExpired": true,
          "createdOn": "2019-08-24T14:15:22Z",
          "revokedOn": "2019-08-24T14:15:22Z",
          "isActive": true
        }
      ],
      "verificationCodes": [
        {
          "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
          "userId": "string",
          "code": "string",
          "type": "[",
          "destination": "[",
          "isUsed": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "expiryDate": "2019-08-24T14:15:22Z",
          "attemptCount": 10,
          "maxAttempts": 1,
          "user": {}
        }
      ],
      "emailQueue": [
        {
          "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
          "toEmail": "string",
          "toName": "string",
          "subject": "string",
          "body": "string",
          "isHtml": true,
          "emailType": "[",
          "status": "[",
          "retryCount": 0,
          "maxRetries": 0,
          "createdAt": "2019-08-24T14:15:22Z",
          "scheduledAt": "2019-08-24T14:15:22Z",
          "sentAt": "2019-08-24T14:15:22Z",
          "nextRetryAt": "2019-08-24T14:15:22Z",
          "errorMessage": "string",
          "priority": "[",
          "userId": "string",
          "templateData": "string",
          "user": {}
        }
      ]
    },
    "items": [
      {
        "id": 0,
        "orderId": 0,
        "order": {
          "id": 0,
          "orderDate": "2019-08-24T14:15:22Z",
          "totalAmount": 0.1,
          "status": "[",
          "userId": "string",
          "applicationUser": {},
          "items": [
            null
          ],
          "payments": [
            null
          ],
          "shipping": {}
        },
        "laptopVariantId": 0,
        "laptopVariant": {
          "id": 0,
          "laptopId": 0,
          "discountId": 0,
          "ram": 2147483647,
          "storage": 2147483647,
          "price": 0.1,
          "stockQuantity": 2147483647,
          "laptop": {},
          "discount": {},
          "orderItems": [
            null
          ],
          "cartItems": [
            null
          ]
        },
        "quantity": 0,
        "unitPrice": 0.1
      }
    ],
    "payments": [
      {
        "id": 0,
        "orderId": 0,
        "paymentMethod": 0,
        "paymentStatus": 0,
        "transactionId": "string",
        "paidAt": "2019-08-24T14:15:22Z",
        "order": {
          "id": 0,
          "orderDate": "2019-08-24T14:15:22Z",
          "totalAmount": 0.1,
          "status": "[",
          "userId": "string",
          "applicationUser": {},
          "items": [
            null
          ],
          "payments": [
            null
          ],
          "shipping": {}
        }
      }
    ],
    "shipping": {
      "id": 0,
      "orderId": 0,
      "address": "string",
      "city": "string",
      "country": "string",
      "postalCode": "string",
      "trackingNumber": "string",
      "shippedAt": "2019-08-24T14:15:22Z",
      "deliveredAt": "2019-08-24T14:15:22Z",
      "order": {
        "id": 0,
        "orderDate": "2019-08-24T14:15:22Z",
        "totalAmount": 0.1,
        "status": 0,
        "userId": "string",
        "applicationUser": {
          "id": null,
          "userName": null,
          "normalizedUserName": null,
          "email": null,
          "normalizedEmail": null,
          "emailConfirmed": null,
          "passwordHash": null,
          "securityStamp": null,
          "concurrencyStamp": null,
          "phoneNumber": null,
          "phoneNumberConfirmed": null,
          "twoFactorEnabled": null,
          "lockoutEnd": null,
          "lockoutEnabled": null,
          "accessFailedCount": null,
          "fullName": null,
          "phone": null,
          "role": null,
          "isActive": null,
          "createdAt": null,
          "orders": null,
          "ratings": null,
          "cartItems": null,
          "repairRequests": null,
          "auditLogs": null,
          "refreshTokens": null,
          "verificationCodes": null,
          "emailQueue": null
        },
        "items": [
          {}
        ],
        "payments": [
          {}
        ],
        "shipping": {
          "id": null,
          "orderId": null,
          "address": null,
          "city": null,
          "country": null,
          "postalCode": null,
          "trackingNumber": null,
          "shippedAt": null,
          "deliveredAt": null,
          "order": null
        }
      }
    }
  },
  "laptopVariantId": 0,
  "laptopVariant": {
    "id": 0,
    "laptopId": 0,
    "discountId": 0,
    "ram": 2147483647,
    "storage": 2147483647,
    "price": 0.1,
    "stockQuantity": 2147483647,
    "laptop": {
      "id": 0,
      "modelName": "string",
      "processor": "string",
      "gpu": "string",
      "screenSize": "string",
      "hasCamera": true,
      "hasKeyboard": true,
      "hasTouchScreen": true,
      "ports": "string",
      "description": "string",
      "notes": "string",
      "warranty": "string",
      "brandId": 0,
      "categoryId": 0,
      "brand": {
        "id": 0,
        "name": "string",
        "country": "string",
        "logoUrl": "string",
        "description": "string",
        "laptops": [
          {}
        ]
      },
      "category": {
        "id": 0,
        "name": "string",
        "description": "string",
        "laptops": [
          {}
        ]
      },
      "variants": [
        {
          "id": 0,
          "laptopId": 0,
          "discountId": 0,
          "ram": 2147483647,
          "storage": 2147483647,
          "price": 0.1,
          "stockQuantity": 2147483647,
          "laptop": {},
          "discount": {},
          "orderItems": [
            null
          ],
          "cartItems": [
            null
          ]
        }
      ],
      "images": [
        {
          "id": 0,
          "laptopId": 0,
          "imageUrl": "string",
          "isMain": true,
          "uploadedAt": "2019-08-24T14:15:22Z",
          "laptop": {}
        }
      ],
      "ratings": [
        {
          "id": 0,
          "userId": "string",
          "laptopId": 0,
          "stars": 1,
          "comment": "string",
          "createdAt": "2019-08-24T14:15:22Z",
          "user": {},
          "laptop": {}
        }
      ],
      "repairRequests": [
        {
          "requestId": 0,
          "applicationUserId": "string",
          "itemId": 0,
          "laptopId": 0,
          "notes": "string",
          "status": "[",
          "requestDate": "2019-08-24T14:15:22Z",
          "user": {},
          "repairServiceItem": {},
          "laptop": {}
        }
      ]
    },
    "discount": {
      "id": 0,
      "title": "string",
      "description": "string",
      "percentage": 100,
      "startDate": "2019-08-24T14:15:22Z",
      "endDate": "2019-08-24T14:15:22Z",
      "isActive": true,
      "laptopVariants": [
        {
          "id": 0,
          "laptopId": 0,
          "discountId": 0,
          "ram": 2147483647,
          "storage": 2147483647,
          "price": 0.1,
          "stockQuantity": 2147483647,
          "laptop": {},
          "discount": {},
          "orderItems": [
            null
          ],
          "cartItems": [
            null
          ]
        }
      ]
    },
    "orderItems": [
      {
        "id": 0,
        "orderId": 0,
        "order": {
          "id": 0,
          "orderDate": "2019-08-24T14:15:22Z",
          "totalAmount": 0.1,
          "status": "[",
          "userId": "string",
          "applicationUser": {},
          "items": [
            null
          ],
          "payments": [
            null
          ],
          "shipping": {}
        },
        "laptopVariantId": 0,
        "laptopVariant": {
          "id": 0,
          "laptopId": 0,
          "discountId": 0,
          "ram": 2147483647,
          "storage": 2147483647,
          "price": 0.1,
          "stockQuantity": 2147483647,
          "laptop": {},
          "discount": {},
          "orderItems": [
            null
          ],
          "cartItems": [
            null
          ]
        },
        "quantity": 0,
        "unitPrice": 0.1
      }
    ],
    "cartItems": [
      {
        "id": 0,
        "applicationUserId": "string",
        "laptopVariantId": 0,
        "quantity": 1,
        "addedAt": "2019-08-24T14:15:22Z",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "laptopVariant": {
          "id": 0,
          "laptopId": 0,
          "discountId": 0,
          "ram": 2147483647,
          "storage": 2147483647,
          "price": 0.1,
          "stockQuantity": 2147483647,
          "laptop": {},
          "discount": {},
          "orderItems": [
            null
          ],
          "cartItems": [
            null
          ]
        }
      }
    ]
  },
  "quantity": 0,
  "unitPrice": 0.1
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|id|integer(int32)|false|none||none|
|orderId|integer(int32)|false|none||none|
|order|[Order](#schemaorder)|false|none||none|
|laptopVariantId|integer(int32)|false|none||none|
|laptopVariant|[LaptopVariant](#schemalaptopvariant)|false|none||none|
|quantity|integer(int32)|false|none||none|
|unitPrice|number(double)|false|none||none|

<h2 id="tocS_OrderStatus">OrderStatus</h2>

<a id="schemaorderstatus"></a>
<a id="schema_OrderStatus"></a>
<a id="tocSorderstatus"></a>
<a id="tocsorderstatus"></a>

```json
0

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|*anonymous*|integer(int32)|false|none||none|

#### Enum

|Name|Value|
|---|---|
|*anonymous*|0|
|*anonymous*|1|
|*anonymous*|2|
|*anonymous*|3|

<h2 id="tocS_Payment">Payment</h2>

<a id="schemapayment"></a>
<a id="schema_Payment"></a>
<a id="tocSpayment"></a>
<a id="tocspayment"></a>

```json
{
  "id": 0,
  "orderId": 0,
  "paymentMethod": 0,
  "paymentStatus": 0,
  "transactionId": "string",
  "paidAt": "2019-08-24T14:15:22Z",
  "order": {
    "id": 0,
    "orderDate": "2019-08-24T14:15:22Z",
    "totalAmount": 0.1,
    "status": 0,
    "userId": "string",
    "applicationUser": {
      "id": "string",
      "userName": "string",
      "normalizedUserName": "string",
      "email": "string",
      "normalizedEmail": "string",
      "emailConfirmed": true,
      "passwordHash": "string",
      "securityStamp": "string",
      "concurrencyStamp": "string",
      "phoneNumber": "string",
      "phoneNumberConfirmed": true,
      "twoFactorEnabled": true,
      "lockoutEnd": "2019-08-24T14:15:22Z",
      "lockoutEnabled": true,
      "accessFailedCount": 0,
      "fullName": "string",
      "phone": "string",
      "role": "string",
      "isActive": true,
      "createdAt": "2019-08-24T14:15:22Z",
      "orders": [
        {
          "id": 0,
          "orderDate": "2019-08-24T14:15:22Z",
          "totalAmount": 0.1,
          "status": "[",
          "userId": "string",
          "applicationUser": {},
          "items": [
            null
          ],
          "payments": [
            null
          ],
          "shipping": {}
        }
      ],
      "ratings": [
        {
          "id": 0,
          "userId": "string",
          "laptopId": 0,
          "stars": 1,
          "comment": "string",
          "createdAt": "2019-08-24T14:15:22Z",
          "user": {},
          "laptop": {}
        }
      ],
      "cartItems": [
        {
          "id": 0,
          "applicationUserId": "string",
          "laptopVariantId": 0,
          "quantity": 1,
          "addedAt": "2019-08-24T14:15:22Z",
          "user": {},
          "laptopVariant": {}
        }
      ],
      "repairRequests": [
        {
          "requestId": 0,
          "applicationUserId": "string",
          "itemId": 0,
          "laptopId": 0,
          "notes": "string",
          "status": "[",
          "requestDate": "2019-08-24T14:15:22Z",
          "user": {},
          "repairServiceItem": {},
          "laptop": {}
        }
      ],
      "auditLogs": [
        {
          "id": 0,
          "applicationUserId": "string",
          "action": "string",
          "entity": "string",
          "entityId": 0,
          "timestamp": "2019-08-24T14:15:22Z",
          "details": "string",
          "user": {}
        }
      ],
      "refreshTokens": [
        {
          "token": "string",
          "expiresOn": "2019-08-24T14:15:22Z",
          "isExpired": true,
          "createdOn": "2019-08-24T14:15:22Z",
          "revokedOn": "2019-08-24T14:15:22Z",
          "isActive": true
        }
      ],
      "verificationCodes": [
        {
          "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
          "userId": "string",
          "code": "string",
          "type": "[",
          "destination": "[",
          "isUsed": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "expiryDate": "2019-08-24T14:15:22Z",
          "attemptCount": 10,
          "maxAttempts": 1,
          "user": {}
        }
      ],
      "emailQueue": [
        {
          "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
          "toEmail": "string",
          "toName": "string",
          "subject": "string",
          "body": "string",
          "isHtml": true,
          "emailType": "[",
          "status": "[",
          "retryCount": 0,
          "maxRetries": 0,
          "createdAt": "2019-08-24T14:15:22Z",
          "scheduledAt": "2019-08-24T14:15:22Z",
          "sentAt": "2019-08-24T14:15:22Z",
          "nextRetryAt": "2019-08-24T14:15:22Z",
          "errorMessage": "string",
          "priority": "[",
          "userId": "string",
          "templateData": "string",
          "user": {}
        }
      ]
    },
    "items": [
      {
        "id": 0,
        "orderId": 0,
        "order": {
          "id": 0,
          "orderDate": "2019-08-24T14:15:22Z",
          "totalAmount": 0.1,
          "status": "[",
          "userId": "string",
          "applicationUser": {},
          "items": [
            null
          ],
          "payments": [
            null
          ],
          "shipping": {}
        },
        "laptopVariantId": 0,
        "laptopVariant": {
          "id": 0,
          "laptopId": 0,
          "discountId": 0,
          "ram": 2147483647,
          "storage": 2147483647,
          "price": 0.1,
          "stockQuantity": 2147483647,
          "laptop": {},
          "discount": {},
          "orderItems": [
            null
          ],
          "cartItems": [
            null
          ]
        },
        "quantity": 0,
        "unitPrice": 0.1
      }
    ],
    "payments": [
      {
        "id": 0,
        "orderId": 0,
        "paymentMethod": 0,
        "paymentStatus": 0,
        "transactionId": "string",
        "paidAt": "2019-08-24T14:15:22Z",
        "order": {
          "id": 0,
          "orderDate": "2019-08-24T14:15:22Z",
          "totalAmount": 0.1,
          "status": "[",
          "userId": "string",
          "applicationUser": {},
          "items": [
            null
          ],
          "payments": [
            null
          ],
          "shipping": {}
        }
      }
    ],
    "shipping": {
      "id": 0,
      "orderId": 0,
      "address": "string",
      "city": "string",
      "country": "string",
      "postalCode": "string",
      "trackingNumber": "string",
      "shippedAt": "2019-08-24T14:15:22Z",
      "deliveredAt": "2019-08-24T14:15:22Z",
      "order": {
        "id": 0,
        "orderDate": "2019-08-24T14:15:22Z",
        "totalAmount": 0.1,
        "status": 0,
        "userId": "string",
        "applicationUser": {
          "id": null,
          "userName": null,
          "normalizedUserName": null,
          "email": null,
          "normalizedEmail": null,
          "emailConfirmed": null,
          "passwordHash": null,
          "securityStamp": null,
          "concurrencyStamp": null,
          "phoneNumber": null,
          "phoneNumberConfirmed": null,
          "twoFactorEnabled": null,
          "lockoutEnd": null,
          "lockoutEnabled": null,
          "accessFailedCount": null,
          "fullName": null,
          "phone": null,
          "role": null,
          "isActive": null,
          "createdAt": null,
          "orders": null,
          "ratings": null,
          "cartItems": null,
          "repairRequests": null,
          "auditLogs": null,
          "refreshTokens": null,
          "verificationCodes": null,
          "emailQueue": null
        },
        "items": [
          {}
        ],
        "payments": [
          {}
        ],
        "shipping": {
          "id": null,
          "orderId": null,
          "address": null,
          "city": null,
          "country": null,
          "postalCode": null,
          "trackingNumber": null,
          "shippedAt": null,
          "deliveredAt": null,
          "order": null
        }
      }
    }
  }
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|id|integer(int32)|false|none||none|
|orderId|integer(int32)|false|none||none|
|paymentMethod|[PaymentMethod](#schemapaymentmethod)|true|none||none|
|paymentStatus|[PaymentStatus](#schemapaymentstatus)|true|none||none|
|transactionId|string¦null|false|none||none|
|paidAt|string(date-time)¦null|false|none||none|
|order|[Order](#schemaorder)|false|none||none|

<h2 id="tocS_PaymentMethod">PaymentMethod</h2>

<a id="schemapaymentmethod"></a>
<a id="schema_PaymentMethod"></a>
<a id="tocSpaymentmethod"></a>
<a id="tocspaymentmethod"></a>

```json
0

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|*anonymous*|integer(int32)|false|none||none|

#### Enum

|Name|Value|
|---|---|
|*anonymous*|0|
|*anonymous*|1|
|*anonymous*|2|

<h2 id="tocS_PaymentStatus">PaymentStatus</h2>

<a id="schemapaymentstatus"></a>
<a id="schema_PaymentStatus"></a>
<a id="tocSpaymentstatus"></a>
<a id="tocspaymentstatus"></a>

```json
0

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|*anonymous*|integer(int32)|false|none||none|

#### Enum

|Name|Value|
|---|---|
|*anonymous*|0|
|*anonymous*|1|
|*anonymous*|2|
|*anonymous*|3|

<h2 id="tocS_Rating">Rating</h2>

<a id="schemarating"></a>
<a id="schema_Rating"></a>
<a id="tocSrating"></a>
<a id="tocsrating"></a>

```json
{
  "id": 0,
  "userId": "string",
  "laptopId": 0,
  "stars": 1,
  "comment": "string",
  "createdAt": "2019-08-24T14:15:22Z",
  "user": {
    "id": "string",
    "userName": "string",
    "normalizedUserName": "string",
    "email": "string",
    "normalizedEmail": "string",
    "emailConfirmed": true,
    "passwordHash": "string",
    "securityStamp": "string",
    "concurrencyStamp": "string",
    "phoneNumber": "string",
    "phoneNumberConfirmed": true,
    "twoFactorEnabled": true,
    "lockoutEnd": "2019-08-24T14:15:22Z",
    "lockoutEnabled": true,
    "accessFailedCount": 0,
    "fullName": "string",
    "phone": "string",
    "role": "string",
    "isActive": true,
    "createdAt": "2019-08-24T14:15:22Z",
    "orders": [
      {
        "id": 0,
        "orderDate": "2019-08-24T14:15:22Z",
        "totalAmount": 0.1,
        "status": 0,
        "userId": "string",
        "applicationUser": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "items": [
          {
            "id": null,
            "orderId": null,
            "order": null,
            "laptopVariantId": null,
            "laptopVariant": null,
            "quantity": null,
            "unitPrice": null
          }
        ],
        "payments": [
          {
            "id": null,
            "orderId": null,
            "paymentMethod": null,
            "paymentStatus": null,
            "transactionId": null,
            "paidAt": null,
            "order": null
          }
        ],
        "shipping": {
          "id": 0,
          "orderId": 0,
          "address": "string",
          "city": "string",
          "country": "string",
          "postalCode": "string",
          "trackingNumber": "string",
          "shippedAt": "2019-08-24T14:15:22Z",
          "deliveredAt": "2019-08-24T14:15:22Z",
          "order": {}
        }
      }
    ],
    "ratings": [
      {
        "id": 0,
        "userId": "string",
        "laptopId": 0,
        "stars": 1,
        "comment": "string",
        "createdAt": "2019-08-24T14:15:22Z",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      }
    ],
    "cartItems": [
      {
        "id": 0,
        "applicationUserId": "string",
        "laptopVariantId": 0,
        "quantity": 1,
        "addedAt": "2019-08-24T14:15:22Z",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "laptopVariant": {
          "id": 0,
          "laptopId": 0,
          "discountId": 0,
          "ram": 2147483647,
          "storage": 2147483647,
          "price": 0.1,
          "stockQuantity": 2147483647,
          "laptop": {},
          "discount": {},
          "orderItems": [
            null
          ],
          "cartItems": [
            null
          ]
        }
      }
    ],
    "repairRequests": [
      {
        "requestId": 0,
        "applicationUserId": "string",
        "itemId": 0,
        "laptopId": 0,
        "notes": "string",
        "status": 0,
        "requestDate": "2019-08-24T14:15:22Z",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "repairServiceItem": {
          "itemId": 0,
          "name": "string",
          "repairType": "[",
          "price": 0.1,
          "estimatedTime": "string",
          "repairRequests": [
            null
          ]
        },
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      }
    ],
    "auditLogs": [
      {
        "id": 0,
        "applicationUserId": "string",
        "action": "string",
        "entity": "string",
        "entityId": 0,
        "timestamp": "2019-08-24T14:15:22Z",
        "details": "string",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        }
      }
    ],
    "refreshTokens": [
      {
        "token": "string",
        "expiresOn": "2019-08-24T14:15:22Z",
        "isExpired": true,
        "createdOn": "2019-08-24T14:15:22Z",
        "revokedOn": "2019-08-24T14:15:22Z",
        "isActive": true
      }
    ],
    "verificationCodes": [
      {
        "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
        "userId": "string",
        "code": "string",
        "type": 0,
        "destination": 0,
        "isUsed": true,
        "createdAt": "2019-08-24T14:15:22Z",
        "expiryDate": "2019-08-24T14:15:22Z",
        "attemptCount": 10,
        "maxAttempts": 1,
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        }
      }
    ],
    "emailQueue": [
      {
        "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
        "toEmail": "string",
        "toName": "string",
        "subject": "string",
        "body": "string",
        "isHtml": true,
        "emailType": 0,
        "status": 0,
        "retryCount": 0,
        "maxRetries": 0,
        "createdAt": "2019-08-24T14:15:22Z",
        "scheduledAt": "2019-08-24T14:15:22Z",
        "sentAt": "2019-08-24T14:15:22Z",
        "nextRetryAt": "2019-08-24T14:15:22Z",
        "errorMessage": "string",
        "priority": 0,
        "userId": "string",
        "templateData": "string",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        }
      }
    ]
  },
  "laptop": {
    "id": 0,
    "modelName": "string",
    "processor": "string",
    "gpu": "string",
    "screenSize": "string",
    "hasCamera": true,
    "hasKeyboard": true,
    "hasTouchScreen": true,
    "ports": "string",
    "description": "string",
    "notes": "string",
    "warranty": "string",
    "brandId": 0,
    "categoryId": 0,
    "brand": {
      "id": 0,
      "name": "string",
      "country": "string",
      "logoUrl": "string",
      "description": "string",
      "laptops": [
        {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      ]
    },
    "category": {
      "id": 0,
      "name": "string",
      "description": "string",
      "laptops": [
        {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      ]
    },
    "variants": [
      {
        "id": 0,
        "laptopId": 0,
        "discountId": 0,
        "ram": 2147483647,
        "storage": 2147483647,
        "price": 0.1,
        "stockQuantity": 2147483647,
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        },
        "discount": {
          "id": 0,
          "title": "string",
          "description": "string",
          "percentage": 100,
          "startDate": "2019-08-24T14:15:22Z",
          "endDate": "2019-08-24T14:15:22Z",
          "isActive": true,
          "laptopVariants": [
            null
          ]
        },
        "orderItems": [
          {
            "id": null,
            "orderId": null,
            "order": null,
            "laptopVariantId": null,
            "laptopVariant": null,
            "quantity": null,
            "unitPrice": null
          }
        ],
        "cartItems": [
          {
            "id": null,
            "applicationUserId": null,
            "laptopVariantId": null,
            "quantity": null,
            "addedAt": null,
            "user": null,
            "laptopVariant": null
          }
        ]
      }
    ],
    "images": [
      {
        "id": 0,
        "laptopId": 0,
        "imageUrl": "string",
        "isMain": true,
        "uploadedAt": "2019-08-24T14:15:22Z",
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      }
    ],
    "ratings": [
      {
        "id": 0,
        "userId": "string",
        "laptopId": 0,
        "stars": 1,
        "comment": "string",
        "createdAt": "2019-08-24T14:15:22Z",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      }
    ],
    "repairRequests": [
      {
        "requestId": 0,
        "applicationUserId": "string",
        "itemId": 0,
        "laptopId": 0,
        "notes": "string",
        "status": 0,
        "requestDate": "2019-08-24T14:15:22Z",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "repairServiceItem": {
          "itemId": 0,
          "name": "string",
          "repairType": "[",
          "price": 0.1,
          "estimatedTime": "string",
          "repairRequests": [
            null
          ]
        },
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      }
    ]
  }
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|id|integer(int32)|false|none||none|
|userId|string¦null|false|none||none|
|laptopId|integer(int32)|false|none||none|
|stars|integer(int32)|false|none||none|
|comment|string¦null|false|none||none|
|createdAt|string(date-time)|false|none||none|
|user|[ApplicationUser](#schemaapplicationuser)|false|none||none|
|laptop|[Laptop](#schemalaptop)|false|none||none|

<h2 id="tocS_RefreshToken">RefreshToken</h2>

<a id="schemarefreshtoken"></a>
<a id="schema_RefreshToken"></a>
<a id="tocSrefreshtoken"></a>
<a id="tocsrefreshtoken"></a>

```json
{
  "token": "string",
  "expiresOn": "2019-08-24T14:15:22Z",
  "isExpired": true,
  "createdOn": "2019-08-24T14:15:22Z",
  "revokedOn": "2019-08-24T14:15:22Z",
  "isActive": true
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|token|string¦null|false|none||none|
|expiresOn|string(date-time)|false|none||none|
|isExpired|boolean|false|read-only||none|
|createdOn|string(date-time)|false|none||none|
|revokedOn|string(date-time)¦null|false|none||none|
|isActive|boolean|false|read-only||none|

<h2 id="tocS_RefreshTokenDto">RefreshTokenDto</h2>

<a id="schemarefreshtokendto"></a>
<a id="schema_RefreshTokenDto"></a>
<a id="tocSrefreshtokendto"></a>
<a id="tocsrefreshtokendto"></a>

```json
{
  "refreshToken": "string"
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|refreshToken|string|true|none||none|

<h2 id="tocS_RegisterDto">RegisterDto</h2>

<a id="schemaregisterdto"></a>
<a id="schema_RegisterDto"></a>
<a id="tocSregisterdto"></a>
<a id="tocsregisterdto"></a>

```json
{
  "userName": "string",
  "email": "user@example.com",
  "fullName": "string",
  "password": "string",
  "confirmPassword": "string"
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|userName|string|true|none||none|
|email|string(email)|true|none||none|
|fullName|string|true|none||none|
|password|string|true|none||none|
|confirmPassword|string|true|none||none|

<h2 id="tocS_RepairRequest">RepairRequest</h2>

<a id="schemarepairrequest"></a>
<a id="schema_RepairRequest"></a>
<a id="tocSrepairrequest"></a>
<a id="tocsrepairrequest"></a>

```json
{
  "requestId": 0,
  "applicationUserId": "string",
  "itemId": 0,
  "laptopId": 0,
  "notes": "string",
  "status": 0,
  "requestDate": "2019-08-24T14:15:22Z",
  "user": {
    "id": "string",
    "userName": "string",
    "normalizedUserName": "string",
    "email": "string",
    "normalizedEmail": "string",
    "emailConfirmed": true,
    "passwordHash": "string",
    "securityStamp": "string",
    "concurrencyStamp": "string",
    "phoneNumber": "string",
    "phoneNumberConfirmed": true,
    "twoFactorEnabled": true,
    "lockoutEnd": "2019-08-24T14:15:22Z",
    "lockoutEnabled": true,
    "accessFailedCount": 0,
    "fullName": "string",
    "phone": "string",
    "role": "string",
    "isActive": true,
    "createdAt": "2019-08-24T14:15:22Z",
    "orders": [
      {
        "id": 0,
        "orderDate": "2019-08-24T14:15:22Z",
        "totalAmount": 0.1,
        "status": 0,
        "userId": "string",
        "applicationUser": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "items": [
          {
            "id": null,
            "orderId": null,
            "order": null,
            "laptopVariantId": null,
            "laptopVariant": null,
            "quantity": null,
            "unitPrice": null
          }
        ],
        "payments": [
          {
            "id": null,
            "orderId": null,
            "paymentMethod": null,
            "paymentStatus": null,
            "transactionId": null,
            "paidAt": null,
            "order": null
          }
        ],
        "shipping": {
          "id": 0,
          "orderId": 0,
          "address": "string",
          "city": "string",
          "country": "string",
          "postalCode": "string",
          "trackingNumber": "string",
          "shippedAt": "2019-08-24T14:15:22Z",
          "deliveredAt": "2019-08-24T14:15:22Z",
          "order": {}
        }
      }
    ],
    "ratings": [
      {
        "id": 0,
        "userId": "string",
        "laptopId": 0,
        "stars": 1,
        "comment": "string",
        "createdAt": "2019-08-24T14:15:22Z",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      }
    ],
    "cartItems": [
      {
        "id": 0,
        "applicationUserId": "string",
        "laptopVariantId": 0,
        "quantity": 1,
        "addedAt": "2019-08-24T14:15:22Z",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "laptopVariant": {
          "id": 0,
          "laptopId": 0,
          "discountId": 0,
          "ram": 2147483647,
          "storage": 2147483647,
          "price": 0.1,
          "stockQuantity": 2147483647,
          "laptop": {},
          "discount": {},
          "orderItems": [
            null
          ],
          "cartItems": [
            null
          ]
        }
      }
    ],
    "repairRequests": [
      {
        "requestId": 0,
        "applicationUserId": "string",
        "itemId": 0,
        "laptopId": 0,
        "notes": "string",
        "status": 0,
        "requestDate": "2019-08-24T14:15:22Z",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "repairServiceItem": {
          "itemId": 0,
          "name": "string",
          "repairType": "[",
          "price": 0.1,
          "estimatedTime": "string",
          "repairRequests": [
            null
          ]
        },
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      }
    ],
    "auditLogs": [
      {
        "id": 0,
        "applicationUserId": "string",
        "action": "string",
        "entity": "string",
        "entityId": 0,
        "timestamp": "2019-08-24T14:15:22Z",
        "details": "string",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        }
      }
    ],
    "refreshTokens": [
      {
        "token": "string",
        "expiresOn": "2019-08-24T14:15:22Z",
        "isExpired": true,
        "createdOn": "2019-08-24T14:15:22Z",
        "revokedOn": "2019-08-24T14:15:22Z",
        "isActive": true
      }
    ],
    "verificationCodes": [
      {
        "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
        "userId": "string",
        "code": "string",
        "type": 0,
        "destination": 0,
        "isUsed": true,
        "createdAt": "2019-08-24T14:15:22Z",
        "expiryDate": "2019-08-24T14:15:22Z",
        "attemptCount": 10,
        "maxAttempts": 1,
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        }
      }
    ],
    "emailQueue": [
      {
        "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
        "toEmail": "string",
        "toName": "string",
        "subject": "string",
        "body": "string",
        "isHtml": true,
        "emailType": 0,
        "status": 0,
        "retryCount": 0,
        "maxRetries": 0,
        "createdAt": "2019-08-24T14:15:22Z",
        "scheduledAt": "2019-08-24T14:15:22Z",
        "sentAt": "2019-08-24T14:15:22Z",
        "nextRetryAt": "2019-08-24T14:15:22Z",
        "errorMessage": "string",
        "priority": 0,
        "userId": "string",
        "templateData": "string",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        }
      }
    ]
  },
  "repairServiceItem": {
    "itemId": 0,
    "name": "string",
    "repairType": 0,
    "price": 0.1,
    "estimatedTime": "string",
    "repairRequests": [
      {
        "requestId": 0,
        "applicationUserId": "string",
        "itemId": 0,
        "laptopId": 0,
        "notes": "string",
        "status": 0,
        "requestDate": "2019-08-24T14:15:22Z",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "repairServiceItem": {
          "itemId": 0,
          "name": "string",
          "repairType": "[",
          "price": 0.1,
          "estimatedTime": "string",
          "repairRequests": [
            null
          ]
        },
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      }
    ]
  },
  "laptop": {
    "id": 0,
    "modelName": "string",
    "processor": "string",
    "gpu": "string",
    "screenSize": "string",
    "hasCamera": true,
    "hasKeyboard": true,
    "hasTouchScreen": true,
    "ports": "string",
    "description": "string",
    "notes": "string",
    "warranty": "string",
    "brandId": 0,
    "categoryId": 0,
    "brand": {
      "id": 0,
      "name": "string",
      "country": "string",
      "logoUrl": "string",
      "description": "string",
      "laptops": [
        {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      ]
    },
    "category": {
      "id": 0,
      "name": "string",
      "description": "string",
      "laptops": [
        {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      ]
    },
    "variants": [
      {
        "id": 0,
        "laptopId": 0,
        "discountId": 0,
        "ram": 2147483647,
        "storage": 2147483647,
        "price": 0.1,
        "stockQuantity": 2147483647,
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        },
        "discount": {
          "id": 0,
          "title": "string",
          "description": "string",
          "percentage": 100,
          "startDate": "2019-08-24T14:15:22Z",
          "endDate": "2019-08-24T14:15:22Z",
          "isActive": true,
          "laptopVariants": [
            null
          ]
        },
        "orderItems": [
          {
            "id": null,
            "orderId": null,
            "order": null,
            "laptopVariantId": null,
            "laptopVariant": null,
            "quantity": null,
            "unitPrice": null
          }
        ],
        "cartItems": [
          {
            "id": null,
            "applicationUserId": null,
            "laptopVariantId": null,
            "quantity": null,
            "addedAt": null,
            "user": null,
            "laptopVariant": null
          }
        ]
      }
    ],
    "images": [
      {
        "id": 0,
        "laptopId": 0,
        "imageUrl": "string",
        "isMain": true,
        "uploadedAt": "2019-08-24T14:15:22Z",
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      }
    ],
    "ratings": [
      {
        "id": 0,
        "userId": "string",
        "laptopId": 0,
        "stars": 1,
        "comment": "string",
        "createdAt": "2019-08-24T14:15:22Z",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      }
    ],
    "repairRequests": [
      {
        "requestId": 0,
        "applicationUserId": "string",
        "itemId": 0,
        "laptopId": 0,
        "notes": "string",
        "status": 0,
        "requestDate": "2019-08-24T14:15:22Z",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "repairServiceItem": {
          "itemId": 0,
          "name": "string",
          "repairType": "[",
          "price": 0.1,
          "estimatedTime": "string",
          "repairRequests": [
            null
          ]
        },
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      }
    ]
  }
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|requestId|integer(int32)|false|none||none|
|applicationUserId|string¦null|false|none||none|
|itemId|integer(int32)|false|none||none|
|laptopId|integer(int32)¦null|false|none||none|
|notes|string¦null|false|none||none|
|status|[RepairRequestStatus](#schemarepairrequeststatus)|true|none||none|
|requestDate|string(date-time)|false|none||none|
|user|[ApplicationUser](#schemaapplicationuser)|false|none||none|
|repairServiceItem|[RepairServiceItem](#schemarepairserviceitem)|false|none||none|
|laptop|[Laptop](#schemalaptop)|false|none||none|

<h2 id="tocS_RepairRequestStatus">RepairRequestStatus</h2>

<a id="schemarepairrequeststatus"></a>
<a id="schema_RepairRequestStatus"></a>
<a id="tocSrepairrequeststatus"></a>
<a id="tocsrepairrequeststatus"></a>

```json
0

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|*anonymous*|integer(int32)|false|none||none|

#### Enum

|Name|Value|
|---|---|
|*anonymous*|0|
|*anonymous*|1|
|*anonymous*|2|
|*anonymous*|3|

<h2 id="tocS_RepairServiceItem">RepairServiceItem</h2>

<a id="schemarepairserviceitem"></a>
<a id="schema_RepairServiceItem"></a>
<a id="tocSrepairserviceitem"></a>
<a id="tocsrepairserviceitem"></a>

```json
{
  "itemId": 0,
  "name": "string",
  "repairType": 0,
  "price": 0.1,
  "estimatedTime": "string",
  "repairRequests": [
    {
      "requestId": 0,
      "applicationUserId": "string",
      "itemId": 0,
      "laptopId": 0,
      "notes": "string",
      "status": 0,
      "requestDate": "2019-08-24T14:15:22Z",
      "user": {
        "id": "string",
        "userName": "string",
        "normalizedUserName": "string",
        "email": "string",
        "normalizedEmail": "string",
        "emailConfirmed": true,
        "passwordHash": "string",
        "securityStamp": "string",
        "concurrencyStamp": "string",
        "phoneNumber": "string",
        "phoneNumberConfirmed": true,
        "twoFactorEnabled": true,
        "lockoutEnd": "2019-08-24T14:15:22Z",
        "lockoutEnabled": true,
        "accessFailedCount": 0,
        "fullName": "string",
        "phone": "string",
        "role": "string",
        "isActive": true,
        "createdAt": "2019-08-24T14:15:22Z",
        "orders": [
          {
            "id": null,
            "orderDate": null,
            "totalAmount": null,
            "status": null,
            "userId": null,
            "applicationUser": null,
            "items": null,
            "payments": null,
            "shipping": null
          }
        ],
        "ratings": [
          {
            "id": null,
            "userId": null,
            "laptopId": null,
            "stars": null,
            "comment": null,
            "createdAt": null,
            "user": null,
            "laptop": null
          }
        ],
        "cartItems": [
          {
            "id": null,
            "applicationUserId": null,
            "laptopVariantId": null,
            "quantity": null,
            "addedAt": null,
            "user": null,
            "laptopVariant": null
          }
        ],
        "repairRequests": [
          {
            "requestId": null,
            "applicationUserId": null,
            "itemId": null,
            "laptopId": null,
            "notes": null,
            "status": null,
            "requestDate": null,
            "user": null,
            "repairServiceItem": null,
            "laptop": null
          }
        ],
        "auditLogs": [
          {
            "id": null,
            "applicationUserId": null,
            "action": null,
            "entity": null,
            "entityId": null,
            "timestamp": null,
            "details": null,
            "user": null
          }
        ],
        "refreshTokens": [
          {
            "token": null,
            "expiresOn": null,
            "isExpired": null,
            "createdOn": null,
            "revokedOn": null,
            "isActive": null
          }
        ],
        "verificationCodes": [
          {
            "id": null,
            "userId": null,
            "code": null,
            "type": null,
            "destination": null,
            "isUsed": null,
            "createdAt": null,
            "expiryDate": null,
            "attemptCount": null,
            "maxAttempts": null,
            "user": null
          }
        ],
        "emailQueue": [
          {
            "id": null,
            "toEmail": null,
            "toName": null,
            "subject": null,
            "body": null,
            "isHtml": null,
            "emailType": null,
            "status": null,
            "retryCount": null,
            "maxRetries": null,
            "createdAt": null,
            "scheduledAt": null,
            "sentAt": null,
            "nextRetryAt": null,
            "errorMessage": null,
            "priority": null,
            "userId": null,
            "templateData": null,
            "user": null
          }
        ]
      },
      "repairServiceItem": {
        "itemId": 0,
        "name": "string",
        "repairType": 0,
        "price": 0.1,
        "estimatedTime": "string",
        "repairRequests": [
          {
            "requestId": null,
            "applicationUserId": null,
            "itemId": null,
            "laptopId": null,
            "notes": null,
            "status": null,
            "requestDate": null,
            "user": null,
            "repairServiceItem": null,
            "laptop": null
          }
        ]
      },
      "laptop": {
        "id": 0,
        "modelName": "string",
        "processor": "string",
        "gpu": "string",
        "screenSize": "string",
        "hasCamera": true,
        "hasKeyboard": true,
        "hasTouchScreen": true,
        "ports": "string",
        "description": "string",
        "notes": "string",
        "warranty": "string",
        "brandId": 0,
        "categoryId": 0,
        "brand": {
          "id": 0,
          "name": "string",
          "country": "string",
          "logoUrl": "string",
          "description": "string",
          "laptops": [
            null
          ]
        },
        "category": {
          "id": 0,
          "name": "string",
          "description": "string",
          "laptops": [
            null
          ]
        },
        "variants": [
          {
            "id": null,
            "laptopId": null,
            "discountId": null,
            "ram": null,
            "storage": null,
            "price": null,
            "stockQuantity": null,
            "laptop": null,
            "discount": null,
            "orderItems": null,
            "cartItems": null
          }
        ],
        "images": [
          {
            "id": null,
            "laptopId": null,
            "imageUrl": null,
            "isMain": null,
            "uploadedAt": null,
            "laptop": null
          }
        ],
        "ratings": [
          {
            "id": null,
            "userId": null,
            "laptopId": null,
            "stars": null,
            "comment": null,
            "createdAt": null,
            "user": null,
            "laptop": null
          }
        ],
        "repairRequests": [
          {
            "requestId": null,
            "applicationUserId": null,
            "itemId": null,
            "laptopId": null,
            "notes": null,
            "status": null,
            "requestDate": null,
            "user": null,
            "repairServiceItem": null,
            "laptop": null
          }
        ]
      }
    }
  ]
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|itemId|integer(int32)|false|none||none|
|name|string|true|none||none|
|repairType|[RepairType](#schemarepairtype)|true|none||none|
|price|number(double)|false|none||none|
|estimatedTime|string¦null|false|none||none|
|repairRequests|[[RepairRequest](#schemarepairrequest)]¦null|false|none||none|

<h2 id="tocS_RepairType">RepairType</h2>

<a id="schemarepairtype"></a>
<a id="schema_RepairType"></a>
<a id="tocSrepairtype"></a>
<a id="tocsrepairtype"></a>

```json
0

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|*anonymous*|integer(int32)|false|none||none|

#### Enum

|Name|Value|
|---|---|
|*anonymous*|0|
|*anonymous*|1|
|*anonymous*|2|

<h2 id="tocS_ResendVerificationCodeDto">ResendVerificationCodeDto</h2>

<a id="schemaresendverificationcodedto"></a>
<a id="schema_ResendVerificationCodeDto"></a>
<a id="tocSresendverificationcodedto"></a>
<a id="tocsresendverificationcodedto"></a>

```json
{
  "email": "user@example.com",
  "verificationType": 0
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|email|string(email)|true|none||none|
|verificationType|[VerificationCodeType](#schemaverificationcodetype)|true|none||none|

<h2 id="tocS_ResetPasswordWithCodeDto">ResetPasswordWithCodeDto</h2>

<a id="schemaresetpasswordwithcodedto"></a>
<a id="schema_ResetPasswordWithCodeDto"></a>
<a id="tocSresetpasswordwithcodedto"></a>
<a id="tocsresetpasswordwithcodedto"></a>

```json
{
  "email": "user@example.com",
  "code": "string",
  "newPassword": "string",
  "confirmPassword": "string"
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|email|string(email)|true|none||none|
|code|string|true|none||none|
|newPassword|string|true|none||none|
|confirmPassword|string|true|none||none|

<h2 id="tocS_RevokeTokenDto">RevokeTokenDto</h2>

<a id="schemarevoketokendto"></a>
<a id="schema_RevokeTokenDto"></a>
<a id="tocSrevoketokendto"></a>
<a id="tocsrevoketokendto"></a>

```json
{
  "refreshToken": "string"
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|refreshToken|string|true|none||none|

<h2 id="tocS_Shipping">Shipping</h2>

<a id="schemashipping"></a>
<a id="schema_Shipping"></a>
<a id="tocSshipping"></a>
<a id="tocsshipping"></a>

```json
{
  "id": 0,
  "orderId": 0,
  "address": "string",
  "city": "string",
  "country": "string",
  "postalCode": "string",
  "trackingNumber": "string",
  "shippedAt": "2019-08-24T14:15:22Z",
  "deliveredAt": "2019-08-24T14:15:22Z",
  "order": {
    "id": 0,
    "orderDate": "2019-08-24T14:15:22Z",
    "totalAmount": 0.1,
    "status": 0,
    "userId": "string",
    "applicationUser": {
      "id": "string",
      "userName": "string",
      "normalizedUserName": "string",
      "email": "string",
      "normalizedEmail": "string",
      "emailConfirmed": true,
      "passwordHash": "string",
      "securityStamp": "string",
      "concurrencyStamp": "string",
      "phoneNumber": "string",
      "phoneNumberConfirmed": true,
      "twoFactorEnabled": true,
      "lockoutEnd": "2019-08-24T14:15:22Z",
      "lockoutEnabled": true,
      "accessFailedCount": 0,
      "fullName": "string",
      "phone": "string",
      "role": "string",
      "isActive": true,
      "createdAt": "2019-08-24T14:15:22Z",
      "orders": [
        {
          "id": 0,
          "orderDate": "2019-08-24T14:15:22Z",
          "totalAmount": 0.1,
          "status": "[",
          "userId": "string",
          "applicationUser": {},
          "items": [
            null
          ],
          "payments": [
            null
          ],
          "shipping": {}
        }
      ],
      "ratings": [
        {
          "id": 0,
          "userId": "string",
          "laptopId": 0,
          "stars": 1,
          "comment": "string",
          "createdAt": "2019-08-24T14:15:22Z",
          "user": {},
          "laptop": {}
        }
      ],
      "cartItems": [
        {
          "id": 0,
          "applicationUserId": "string",
          "laptopVariantId": 0,
          "quantity": 1,
          "addedAt": "2019-08-24T14:15:22Z",
          "user": {},
          "laptopVariant": {}
        }
      ],
      "repairRequests": [
        {
          "requestId": 0,
          "applicationUserId": "string",
          "itemId": 0,
          "laptopId": 0,
          "notes": "string",
          "status": "[",
          "requestDate": "2019-08-24T14:15:22Z",
          "user": {},
          "repairServiceItem": {},
          "laptop": {}
        }
      ],
      "auditLogs": [
        {
          "id": 0,
          "applicationUserId": "string",
          "action": "string",
          "entity": "string",
          "entityId": 0,
          "timestamp": "2019-08-24T14:15:22Z",
          "details": "string",
          "user": {}
        }
      ],
      "refreshTokens": [
        {
          "token": "string",
          "expiresOn": "2019-08-24T14:15:22Z",
          "isExpired": true,
          "createdOn": "2019-08-24T14:15:22Z",
          "revokedOn": "2019-08-24T14:15:22Z",
          "isActive": true
        }
      ],
      "verificationCodes": [
        {
          "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
          "userId": "string",
          "code": "string",
          "type": "[",
          "destination": "[",
          "isUsed": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "expiryDate": "2019-08-24T14:15:22Z",
          "attemptCount": 10,
          "maxAttempts": 1,
          "user": {}
        }
      ],
      "emailQueue": [
        {
          "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
          "toEmail": "string",
          "toName": "string",
          "subject": "string",
          "body": "string",
          "isHtml": true,
          "emailType": "[",
          "status": "[",
          "retryCount": 0,
          "maxRetries": 0,
          "createdAt": "2019-08-24T14:15:22Z",
          "scheduledAt": "2019-08-24T14:15:22Z",
          "sentAt": "2019-08-24T14:15:22Z",
          "nextRetryAt": "2019-08-24T14:15:22Z",
          "errorMessage": "string",
          "priority": "[",
          "userId": "string",
          "templateData": "string",
          "user": {}
        }
      ]
    },
    "items": [
      {
        "id": 0,
        "orderId": 0,
        "order": {
          "id": 0,
          "orderDate": "2019-08-24T14:15:22Z",
          "totalAmount": 0.1,
          "status": "[",
          "userId": "string",
          "applicationUser": {},
          "items": [
            null
          ],
          "payments": [
            null
          ],
          "shipping": {}
        },
        "laptopVariantId": 0,
        "laptopVariant": {
          "id": 0,
          "laptopId": 0,
          "discountId": 0,
          "ram": 2147483647,
          "storage": 2147483647,
          "price": 0.1,
          "stockQuantity": 2147483647,
          "laptop": {},
          "discount": {},
          "orderItems": [
            null
          ],
          "cartItems": [
            null
          ]
        },
        "quantity": 0,
        "unitPrice": 0.1
      }
    ],
    "payments": [
      {
        "id": 0,
        "orderId": 0,
        "paymentMethod": 0,
        "paymentStatus": 0,
        "transactionId": "string",
        "paidAt": "2019-08-24T14:15:22Z",
        "order": {
          "id": 0,
          "orderDate": "2019-08-24T14:15:22Z",
          "totalAmount": 0.1,
          "status": "[",
          "userId": "string",
          "applicationUser": {},
          "items": [
            null
          ],
          "payments": [
            null
          ],
          "shipping": {}
        }
      }
    ],
    "shipping": {
      "id": 0,
      "orderId": 0,
      "address": "string",
      "city": "string",
      "country": "string",
      "postalCode": "string",
      "trackingNumber": "string",
      "shippedAt": "2019-08-24T14:15:22Z",
      "deliveredAt": "2019-08-24T14:15:22Z",
      "order": {
        "id": 0,
        "orderDate": "2019-08-24T14:15:22Z",
        "totalAmount": 0.1,
        "status": 0,
        "userId": "string",
        "applicationUser": {
          "id": null,
          "userName": null,
          "normalizedUserName": null,
          "email": null,
          "normalizedEmail": null,
          "emailConfirmed": null,
          "passwordHash": null,
          "securityStamp": null,
          "concurrencyStamp": null,
          "phoneNumber": null,
          "phoneNumberConfirmed": null,
          "twoFactorEnabled": null,
          "lockoutEnd": null,
          "lockoutEnabled": null,
          "accessFailedCount": null,
          "fullName": null,
          "phone": null,
          "role": null,
          "isActive": null,
          "createdAt": null,
          "orders": null,
          "ratings": null,
          "cartItems": null,
          "repairRequests": null,
          "auditLogs": null,
          "refreshTokens": null,
          "verificationCodes": null,
          "emailQueue": null
        },
        "items": [
          {}
        ],
        "payments": [
          {}
        ],
        "shipping": {
          "id": null,
          "orderId": null,
          "address": null,
          "city": null,
          "country": null,
          "postalCode": null,
          "trackingNumber": null,
          "shippedAt": null,
          "deliveredAt": null,
          "order": null
        }
      }
    }
  }
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|id|integer(int32)|false|none||none|
|orderId|integer(int32)|false|none||none|
|address|string|true|none||none|
|city|string¦null|false|none||none|
|country|string¦null|false|none||none|
|postalCode|string¦null|false|none||none|
|trackingNumber|string¦null|false|none||none|
|shippedAt|string(date-time)¦null|false|none||none|
|deliveredAt|string(date-time)¦null|false|none||none|
|order|[Order](#schemaorder)|false|none||none|

<h2 id="tocS_SortDirection">SortDirection</h2>

<a id="schemasortdirection"></a>
<a id="schema_SortDirection"></a>
<a id="tocSsortdirection"></a>
<a id="tocssortdirection"></a>

```json
0

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|*anonymous*|integer(int32)|false|none||none|

#### Enum

|Name|Value|
|---|---|
|*anonymous*|0|
|*anonymous*|1|

<h2 id="tocS_TokenRequestDto">TokenRequestDto</h2>

<a id="schematokenrequestdto"></a>
<a id="schema_TokenRequestDto"></a>
<a id="tocStokenrequestdto"></a>
<a id="tocstokenrequestdto"></a>

```json
{
  "email": "string",
  "password": "string"
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|email|string|true|none||none|
|password|string|true|none||none|

<h2 id="tocS_UpdateLaptopDto">UpdateLaptopDto</h2>

<a id="schemaupdatelaptopdto"></a>
<a id="schema_UpdateLaptopDto"></a>
<a id="tocSupdatelaptopdto"></a>
<a id="tocsupdatelaptopdto"></a>

```json
{
  "modelName": "string",
  "processor": "string",
  "gpu": "string",
  "screenSize": "string",
  "hasCamera": true,
  "hasKeyboard": true,
  "hasTouchScreen": true,
  "ports": "string",
  "id": 0
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|modelName|string|true|none||none|
|processor|string|true|none||none|
|gpu|string|true|none||none|
|screenSize|string|true|none||none|
|hasCamera|boolean|false|none||none|
|hasKeyboard|boolean|false|none||none|
|hasTouchScreen|boolean|false|none||none|
|ports|string|true|none||none|
|id|integer(int32)|false|none||none|

<h2 id="tocS_VerificationCode">VerificationCode</h2>

<a id="schemaverificationcode"></a>
<a id="schema_VerificationCode"></a>
<a id="tocSverificationcode"></a>
<a id="tocsverificationcode"></a>

```json
{
  "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
  "userId": "string",
  "code": "string",
  "type": 0,
  "destination": 0,
  "isUsed": true,
  "createdAt": "2019-08-24T14:15:22Z",
  "expiryDate": "2019-08-24T14:15:22Z",
  "attemptCount": 10,
  "maxAttempts": 1,
  "user": {
    "id": "string",
    "userName": "string",
    "normalizedUserName": "string",
    "email": "string",
    "normalizedEmail": "string",
    "emailConfirmed": true,
    "passwordHash": "string",
    "securityStamp": "string",
    "concurrencyStamp": "string",
    "phoneNumber": "string",
    "phoneNumberConfirmed": true,
    "twoFactorEnabled": true,
    "lockoutEnd": "2019-08-24T14:15:22Z",
    "lockoutEnabled": true,
    "accessFailedCount": 0,
    "fullName": "string",
    "phone": "string",
    "role": "string",
    "isActive": true,
    "createdAt": "2019-08-24T14:15:22Z",
    "orders": [
      {
        "id": 0,
        "orderDate": "2019-08-24T14:15:22Z",
        "totalAmount": 0.1,
        "status": 0,
        "userId": "string",
        "applicationUser": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "items": [
          {
            "id": null,
            "orderId": null,
            "order": null,
            "laptopVariantId": null,
            "laptopVariant": null,
            "quantity": null,
            "unitPrice": null
          }
        ],
        "payments": [
          {
            "id": null,
            "orderId": null,
            "paymentMethod": null,
            "paymentStatus": null,
            "transactionId": null,
            "paidAt": null,
            "order": null
          }
        ],
        "shipping": {
          "id": 0,
          "orderId": 0,
          "address": "string",
          "city": "string",
          "country": "string",
          "postalCode": "string",
          "trackingNumber": "string",
          "shippedAt": "2019-08-24T14:15:22Z",
          "deliveredAt": "2019-08-24T14:15:22Z",
          "order": {}
        }
      }
    ],
    "ratings": [
      {
        "id": 0,
        "userId": "string",
        "laptopId": 0,
        "stars": 1,
        "comment": "string",
        "createdAt": "2019-08-24T14:15:22Z",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      }
    ],
    "cartItems": [
      {
        "id": 0,
        "applicationUserId": "string",
        "laptopVariantId": 0,
        "quantity": 1,
        "addedAt": "2019-08-24T14:15:22Z",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "laptopVariant": {
          "id": 0,
          "laptopId": 0,
          "discountId": 0,
          "ram": 2147483647,
          "storage": 2147483647,
          "price": 0.1,
          "stockQuantity": 2147483647,
          "laptop": {},
          "discount": {},
          "orderItems": [
            null
          ],
          "cartItems": [
            null
          ]
        }
      }
    ],
    "repairRequests": [
      {
        "requestId": 0,
        "applicationUserId": "string",
        "itemId": 0,
        "laptopId": 0,
        "notes": "string",
        "status": 0,
        "requestDate": "2019-08-24T14:15:22Z",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        },
        "repairServiceItem": {
          "itemId": 0,
          "name": "string",
          "repairType": "[",
          "price": 0.1,
          "estimatedTime": "string",
          "repairRequests": [
            null
          ]
        },
        "laptop": {
          "id": 0,
          "modelName": "string",
          "processor": "string",
          "gpu": "string",
          "screenSize": "string",
          "hasCamera": true,
          "hasKeyboard": true,
          "hasTouchScreen": true,
          "ports": "string",
          "description": "string",
          "notes": "string",
          "warranty": "string",
          "brandId": 0,
          "categoryId": 0,
          "brand": {},
          "category": {},
          "variants": [
            null
          ],
          "images": [
            null
          ],
          "ratings": [
            null
          ],
          "repairRequests": [
            null
          ]
        }
      }
    ],
    "auditLogs": [
      {
        "id": 0,
        "applicationUserId": "string",
        "action": "string",
        "entity": "string",
        "entityId": 0,
        "timestamp": "2019-08-24T14:15:22Z",
        "details": "string",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        }
      }
    ],
    "refreshTokens": [
      {
        "token": "string",
        "expiresOn": "2019-08-24T14:15:22Z",
        "isExpired": true,
        "createdOn": "2019-08-24T14:15:22Z",
        "revokedOn": "2019-08-24T14:15:22Z",
        "isActive": true
      }
    ],
    "verificationCodes": [
      {
        "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
        "userId": "string",
        "code": "string",
        "type": 0,
        "destination": 0,
        "isUsed": true,
        "createdAt": "2019-08-24T14:15:22Z",
        "expiryDate": "2019-08-24T14:15:22Z",
        "attemptCount": 10,
        "maxAttempts": 1,
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        }
      }
    ],
    "emailQueue": [
      {
        "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
        "toEmail": "string",
        "toName": "string",
        "subject": "string",
        "body": "string",
        "isHtml": true,
        "emailType": 0,
        "status": 0,
        "retryCount": 0,
        "maxRetries": 0,
        "createdAt": "2019-08-24T14:15:22Z",
        "scheduledAt": "2019-08-24T14:15:22Z",
        "sentAt": "2019-08-24T14:15:22Z",
        "nextRetryAt": "2019-08-24T14:15:22Z",
        "errorMessage": "string",
        "priority": 0,
        "userId": "string",
        "templateData": "string",
        "user": {
          "id": "string",
          "userName": "string",
          "normalizedUserName": "string",
          "email": "string",
          "normalizedEmail": "string",
          "emailConfirmed": true,
          "passwordHash": "string",
          "securityStamp": "string",
          "concurrencyStamp": "string",
          "phoneNumber": "string",
          "phoneNumberConfirmed": true,
          "twoFactorEnabled": true,
          "lockoutEnd": "2019-08-24T14:15:22Z",
          "lockoutEnabled": true,
          "accessFailedCount": 0,
          "fullName": "string",
          "phone": "string",
          "role": "string",
          "isActive": true,
          "createdAt": "2019-08-24T14:15:22Z",
          "orders": [
            null
          ],
          "ratings": [
            null
          ],
          "cartItems": [
            null
          ],
          "repairRequests": [
            null
          ],
          "auditLogs": [
            null
          ],
          "refreshTokens": [
            null
          ],
          "verificationCodes": [
            null
          ],
          "emailQueue": [
            null
          ]
        }
      }
    ]
  }
}

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|id|string(uuid)|false|none||none|
|userId|string|true|none||none|
|code|string|true|none||none|
|type|[VerificationCodeType](#schemaverificationcodetype)|false|none||none|
|destination|[DestinationStatus](#schemadestinationstatus)|false|none||none|
|isUsed|boolean|false|none||none|
|createdAt|string(date-time)|false|none||none|
|expiryDate|string(date-time)|false|none||none|
|attemptCount|integer(int32)|false|none||none|
|maxAttempts|integer(int32)|false|none||none|
|user|[ApplicationUser](#schemaapplicationuser)|false|none||none|

<h2 id="tocS_VerificationCodeType">VerificationCodeType</h2>

<a id="schemaverificationcodetype"></a>
<a id="schema_VerificationCodeType"></a>
<a id="tocSverificationcodetype"></a>
<a id="tocsverificationcodetype"></a>

```json
0

```

### Attribute

|Name|Type|Required|Restrictions|Title|Description|
|---|---|---|---|---|---|
|*anonymous*|integer(int32)|false|none||none|

#### Enum

|Name|Value|
|---|---|
|*anonymous*|0|
|*anonymous*|1|
|*anonymous*|2|
|*anonymous*|3|


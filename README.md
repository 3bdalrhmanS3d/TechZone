# Authentication API

This API provides endpoints for user authentication and authorization, including registration, login, email verification, password reset, token management, and logout.

---

## Base URL

```
https://techzoneeg.runasp.net/api/Auth
```

---

## Endpoints

### 1. Register

**POST** `/register`

Register a new user.

#### Request Body

```json
{
  "userName": "string",
  "email": "user@example.com",
  "fullName": "string",
  "password": "string",
  "confirmPassword": "string"
}
```

#### Responses

* **200 OK** – Registration successful.
* **400 Bad Request** – Invalid input.
* **409 Conflict** – User already exists.
* **500 Internal Server Error** – Server error.

---

### 2. Login

**POST** `/login`

Authenticate user and receive a JWT token.

#### Request Body

```json
{
  "email": "string",
  "password": "string"
}
```

#### Success Response

```json
{
  "isSuccess": true,
  "message": "Login successful",
  "data": {
    "message": "string",
    "isAuthenticated": true,
    "username": "string",
    "email": "string",
    "roles": ["string"],
    "token": "jwt-token-here",
    "emailConfirmed": true,
    "refreshTokenExpiration": "2025-09-15T09:45:52.329Z"
  }
}
```

* **200 OK** – Login successful.
* **400 Bad Request** – Invalid input.
* **401 Unauthorized** – Invalid credentials.
* **403 Forbidden** – Access denied.
* **500 Internal Server Error** – Server error.

---

### 3. Confirm Email

**POST** `/confirm-email`

Confirm user’s email using verification code.

#### Request Body

```json
{
  "email": "user@example.com",
  "code": "string"
}
```

#### Responses

* **200 OK** – Email confirmed.
* **400 Bad Request** – Invalid request.
* **404 Not Found** – User not found.
* **500 Internal Server Error** – Server error.

---

### 4. Resend Verification Code

**POST** `/resend-verification-code`

Resend verification code to user.

#### Request Body

```json
{
  "email": "user@example.com",
  "verificationType": 0
}
```

#### Responses

* **200 OK** – Verification code sent.
* **400 Bad Request** – Invalid request.
* **500 Internal Server Error** – Server error.

---

### 5. Forgot Password

**POST** `/forgot-password`

Send password reset code to user’s email.

#### Request Body

```json
{
  "email": "user@example.com"
}
```

#### Responses

* **200 OK** – Reset code sent.
* **400 Bad Request** – Invalid request.
* **500 Internal Server Error** – Server error.

---

### 6. Reset Password

**POST** `/reset-password`

Reset user’s password using reset code.

#### Request Body

```json
{
  "email": "user@example.com",
  "code": "string",
  "newPassword": "string",
  "confirmPassword": "string"
}
```

#### Responses

* **200 OK** – Password reset successful.
* **400 Bad Request** – Invalid request.
* **404 Not Found** – User not found.
* **500 Internal Server Error** – Server error.

---

### 7. Change Password

**POST** `/change-password`

Change password for authenticated users.

#### Request Body

```json
{
  "currentPassword": "string",
  "newPassword": "string",
  "confirmPassword": "string"
}
```

#### Responses

* **200 OK** – Password changed successfully.
* **400 Bad Request** – Invalid request.
* **401 Unauthorized** – User not authenticated.
* **404 Not Found** – User not found.
* **500 Internal Server Error** – Server error.

---

### 8. Refresh Token

**POST** `/refresh-token`

Generate a new JWT token using refresh token.

#### Request Body

```json
{
  "refreshToken": "string"
}
```

#### Responses

* **200 OK** – Token refreshed successfully.
* **400 Bad Request** – Invalid request.
* **401 Unauthorized** – Invalid/expired token.
* **500 Internal Server Error** – Server error.

---

### 9. Revoke Token

**POST** `/revoke-token`

Revoke a refresh token.

#### Request Body

```json
{
  "refreshToken": "string"
}
```

#### Responses

* **200 OK** – Token revoked.
* **400 Bad Request** – Invalid request.
* **401 Unauthorized** – User not authenticated.
* **404 Not Found** – Token not found.
* **500 Internal Server Error** – Server error.

---

### 10. Logout

**POST** `/logout`

Logout the authenticated user.

#### Responses

* **200 OK** – Logout successful.
* **500 Internal Server Error** – Server error.

---

### 11. Status

**GET** `/status`

Check API/service status.

#### Responses

* **200 OK** – Returns status string.
* **500 Internal Server Error** – Server error.

---

## Common Response Schema

Most endpoints return this structure:

```json
{
  "isSuccess": true,
  "message": "string",
  "messageAr": "string",
  "data": "object | boolean | string",
  "errors": ["string"],
  "statusCode": 0,
  "timestamp": "2025-09-15T09:45:52.385Z"
}
```

---

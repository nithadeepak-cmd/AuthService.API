# AuthService.API 🔐

A standalone Authentication & Authorization Web API built using **ASP.NET Core Web API**, **JWT**, **Entity Framework Core**, and **Role-Based Authorization**.

This service is designed to be reused by multiple applications (e.g., E-Commerce, Healthcare, etc.) as a centralized authentication system.

---

## 🚀 Features

- User Registration
- User Login with JWT Token
- Password hashing (BCrypt)
- JWT Authentication
- Role-based Authorization
- Protected endpoints using `[Authorize]`
- Admin-only endpoints
- Swagger for API testing
- Entity Framework Core with SQL Server

---

## 🧱 Tech Stack

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT (JSON Web Tokens)
- BCrypt.Net
- Swagger / Swashbuckle

---

## 🔐 Authentication Flow

1. User registers → password is hashed and saved
2. User logs in → JWT token is generated
3. Client sends JWT token in `Authorization` header
4. API validates token and roles
5. Protected endpoints allow/deny access

---

## 📌 API Endpoints

### Auth

| Method | Endpoint | Description |
|------|--------|------------|
| POST | `/api/Auth/register` | Register new user |
| POST | `/api/Auth/login` | Login and get JWT token |
| GET | `/api/Auth/profile` | Get logged-in user profile |
| GET | `/api/Auth/admin-only` | Admin-only endpoint |

---

## 🔑 JWT Header Format

Authorization: Bearer <your_jwt_token>

## ⚙️ Configuration

Update `appsettings.json`:

```json
"JwtSettings": {
  "Key": "YOUR_SECRET_KEY",
  "Issuer": "AuthService",
  "Audience": "AuthServiceUsers",
  "ExpiresInMinutes": 60
}
Update connection string:

json
Copy code
"ConnectionStrings": {
  "DefaultConnection": "your-sql-server-connection-string"
}
▶️ Run the Project
bash
Copy code
dotnet restore
dotnet ef database update
dotnet run
Open Swagger:

bash
Copy code
https://localhost:<port>/swagger
🧪 Testing
Use Swagger to test endpoints

Login → copy JWT token

Use token in protected endpoints

Role-based authorization can be verified via Admin endpoint

📌 Future Enhancements

Refresh Tokens

Multiple application role mapping

Email verification

Password reset

OAuth / External login

👨‍💻 Author
Built as part of an advanced learning project to demonstrate real-world authentication service design.

---

Project Status :

✔ Authentication Service Completed  
✔ Ready to be integrated with MVC or other frontend applications
AuthService.API – README
🛡️ AuthService.API

A simple .NET Web API project demonstrating:

User registration & login

JWT token generation

Role-based authorization

In-memory user store (no database yet)

Clean architecture with Controllers, Services, DTOs & Interfaces

This is part of my learning journey to understand API development, JWT authentication, middleware, and secure service communication.

🚀 Features Implemented
✅ 1. User Registration & Login

Users can register and log in using simple DTO models.

✅ 2. JWT Authentication

When the user logs in:

JWT token is generated

Token contains custom claims

Token expiry is configurable

✅ 3. In-Memory User Storage

Since database integration is planned later, the app currently uses:

A temporary in-memory collection

Good for learning API + JWT flow

Will be replaced by EF Core + SQL Server later

🔑 JWT Configuration (Learning Purpose Only)

The JWT key is stored in appsettings.json:

"JWTSettings": {
  "Key": "MySuperStrongSecretKey_1234567890_09876554321",
  "Issuer": "AuthService",
  "Audience": "AuthServiceUsers",
  "ExpiresInMinutes": 60
}

⚠️ Security Note (Important for Interview):

This JWT key is intentionally kept inside appsettings.json for learning and demo purposes.
In real production applications, secrets must never be stored in GitHub.
Proper secure storage includes:

.NET User Secrets (local development)

Environment Variables

Azure Key Vault / AWS Secrets Manager

This note shows interviewers that you understand professional security practices.

🧱 Project Structure
AuthService.API/
│── Controllers/
│── DTOs/
│── Interfaces/
│── Models/
│── Services/
│── Properties/
│── appsettings.json
│── Program.cs
│── Auth logic files...

🛠️ Technologies Used

.NET 7 / .NET 8 / .NET 10 (VS 2026 environment)

C# Web API

JWT Authentication

Dependency Injection

In-Memory Storage

Clean Layered Structure

📌 Upcoming Enhancements

These will be added as I continue learning:

🔜 JWT Authentication Middleware (custom middleware)

🔜 Integrate Database (SQL Server + EF Core)

🔜 Refresh Tokens

🔜 User Roles (Admin/User)

🔜 Logging & Exception Handling Middleware
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
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

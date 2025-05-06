# 🍽️ Restaurant POS Application

A simple restaurant Point of Sale (POS) system built with **C#** and **ASP.NET Core 8** using **Rider IDE**.

It supports user authentication, menu and table management, order handling, and billing, with a clean layered architecture.

---

## 📑 Table of Contents
- [Features](#-features)
- [Installation & Setup](#-installation--setup)
- [Dependencies](#-dependencies)
- [Project Structure](#-project-structure)
- [Contribution Guide](#-contribution-guide)
- [Credits](#-credits)

---

## ✨ Features

✅ User Authentication (`/api/Auth`)  
✅ User Management (Admin)  
✅ Menu Management (Admin)  
✅ Table Management (Server)  
✅ Order Management (Server)  
✅ Billing (Server)  
✅ Dashboard Pages  
✅ Frontend with Bootstrap  
✅ Backend Architecture

---

## 💻 Installation & Setup

```bash
git clone https://github.com/your-username/restaurant-pos.git
cd restaurant-pos
```
Configure MySQL in appsettings.json
Apply migrations:
```bash
dotnet ef database update
```
Run the project:
```bash
dotnet run
```
Visit http://localhost:7206/ or Any other http://localhost:PORT/

---
## 🔧 Dependencies
- .NET 8  
- ASP.NET Core MVC  
- EF Core  
- MySQL  
- BCrypt.Net  
- Bootstrap

---
## 📂 Project Structure
```plaintext
/Controllers         → API controllers (Auth, Menu, Table)
/Models             → Entity models (User, MenuItem, Table)
/Services           → Business logic services
/Services/Interfaces → Service interfaces
/DTOs              → Request and response DTOs
/Data              → Database context and migrations
/wwwroot           → Static files (css, js, images)
/Views             → Razor views (if used)
/appsettings.json  → Configuration file
Program.cs         → App startup
```
---


## 🤝 Contribution Guide
1. Fork the repository  
2. Create a feature branch (`git checkout -b feature/YourFeature`)  
3. Commit changes (`git commit -m 'Add feature'`)  
4. Push to the branch (`git push origin feature/YourFeature`)  
5. Open a Pull Request

---

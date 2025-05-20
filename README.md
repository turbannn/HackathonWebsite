# 🧠 Hackathon Management System

A modular and role-based web platform designed for managing tasks and ratings during hackathons.

Built with a clear **three-layer architecture** (DAL → BLL → Web), this project supports secure task rating, user profiles, and role-specific interaction patterns. The focus is on simplicity, portability, and extensibility.

---

## 🛠 Tech Stack

### Architecture

- **DAL (Data Access Layer)**  
  Built with Entity Framework Core using **SQLite** for lightweight and portable storage.  
  Designed to minimize setup complexity.  
  - **Entities**:
    - `HackathonTask`
    - `User`

- **BLL (Business Logic Layer)**  
  Encapsulates core logic, validation, and DTO mappings.

- **Web (Presentation Layer)**  
  ASP.NET Core MVC application providing RESTful endpoints and dynamic Razor views.

---

### Features & Libraries

- 🔐 **JWT Authentication**  
  - Access-token based  
  - Role-aware access control

- 🔄 **AutoMapper**  
  - Smooth mapping between entities and DTOs  
  - Reduces boilerplate code

- ✅ **FluentValidation**  
  - Clean model validation  
  - _"Personally, I find it a powerful and expressive tool I enjoy using."_

---

## 🎨 Styling & UI

The UI design emphasizes clarity and user-role awareness:

- **Profile and task pages**  
  - White central containers  
  - Rounded corners and subtle shadows  
  - Dark radial gradient background

- **Responsive design**  
  - Adaptive spacing and layout for mobile screens

- **Home page animation**  
  - Smooth text fade-in introducing the hackathon  
  - Snowflake animation replaced with modern subtle theming

All styles are split into:
- `profileStyle.css`
- `homeStyle.css`

---

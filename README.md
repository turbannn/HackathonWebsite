📚 Introduction
This project is a Hackathon Task Management Web Application designed to streamline the process of creating, rating, and managing tasks within a hackathon environment. Built using ASP.NET Core MVC, it features a clear role-based access system supporting Admins, Teachers, and Users.

The platform enables:

Task creation and description input

Rating of tasks by authorized roles

User-specific task overviews

Secure role-based interface customization

Its goal is to provide a clean and efficient tool for hackathon coordinators and participants to interact in a structured, intuitive environment.



🛠 Tech Stack
The application is built with a three-layer architecture — DAL, BLL, and Web — for clarity and separation of concerns:

DAL (Data Access Layer): Entity Framework Core using SQLite as the database engine. Chosen for its lightweight nature and ease of use in portable, demo-friendly projects. The database consists of two primary entities:

HackathonTask

User

BLL (Business Logic Layer): Handles application logic, validation, DTO mapping, and service composition.

Web (Presentation Layer): ASP.NET Core MVC project responsible for routing, views, and user interaction.

Additional components:

JWT Authentication (Access Token only): Ensures secure, stateless authentication across protected endpoints.

AutoMapper: Simplifies object mapping between entities and DTOs. It helps reduce boilerplate code and improves maintainability.

FluentValidation: Used for validating incoming models. A powerful and expressive library — "I personally like it for its clarity and flexibility." 😊


🎨 Styling & UI
The frontend is designed with attention to visual clarity and role-oriented UX, including:

Profile pages and task-related views styled with a soft, modern look:

White content containers on a radial dark blue background

Rounded corners and subtle shadows

Responsive design with layout adjustments for smaller screens

Home page features a smooth animated entrance, gradually revealing hackathon info to enhance first-time impressions.

Snowflake effects and animated transitions are used sparingly to give the UI a lightweight and modern feel.

All custom styles are organized within profileStyle.css and homeStyle.css.

# 🚗 Driving License Management System

<p align="center">

![C#](https://img.shields.io/badge/C%23-.NET-blue?logo=csharp)
![Windows Forms](https://img.shields.io/badge/Windows%20Forms-Desktop%20Application-success)
![SQL Server](https://img.shields.io/badge/Database-SQL%20Server-red)
![Architecture](https://img.shields.io/badge/Architecture-3--Tier-orange)
![Status](https://img.shields.io/badge/Status-Completed-brightgreen)

</p>

A comprehensive **Driving License Management System** developed using **C#**, **Windows Forms**, **SQL Server**, and **ADO.NET** following a **Three-Tier Architecture**.

The application digitizes the workflow of a driving license department by managing citizens, drivers, licenses, applications, driving tests, international licenses, detained licenses, and system users through an intuitive desktop interface.

---

# 📑 Table of Contents

* [Overview](#-overview)
* [Features](#-features)
* [Application Modules](#-application-modules)
* [Architecture](#-architecture)
* [Technologies](#-technologies)
* [Project Structure](#-project-structure)
* [Getting Started](#-getting-started)
* [Default Login](#-default-login)
* [Future Improvements](#-future-improvements)
* [Author](#-author)

---

# 📖 Overview

The Driving License Management System provides an integrated solution for managing every stage of the driving license lifecycle, from creating citizen records to issuing, renewing, replacing, and detaining driving licenses.

The project emphasizes:

* Clean architecture
* Separation of concerns
* Object-Oriented Programming
* Reusable business logic
* SQL Server integration
* User-friendly desktop interface

---

# ✨ Features

## 👤 People Management

* Add new people
* Update personal information
* Delete records
* Search and filter
* View person details

---

## 👥 User Management

* Create users
* Edit user information
* Change passwords
* Enable or disable users
* Manage system access

---

## 📄 License Applications

* New Local Driving License Applications
* International License Applications
* Renew Driving License
* Replace Lost License
* Replace Damaged License
* Release Detained License

---

## 🚘 License Management

* Issue licenses
* Renew licenses
* Replace licenses
* Detain licenses
* Release detained licenses
* View license history

---

## 🌍 International Licenses

* Issue international licenses
* Display international license information
* View issued international licenses

---

## 🧪 Test Management

* Vision Tests
* Written Tests
* Street Tests
* Schedule appointments
* Record test results
* Retake failed tests

---

## 🚗 Driver Management

* Register drivers
* View driver information
* Driver license history

---

## ⚙️ Application Types

* Manage application types
* Configure application fees

---

# 🏗️ Architecture

The project follows a **Three-Tier Architecture** to ensure maintainability and separation of responsibilities.

```text
Presentation Layer
        │
        ▼
Business Layer
        │
        ▼
Data Access Layer
        │
        ▼
SQL Server Database
```

### Presentation Layer

Responsible for the user interface and user interaction.

### Business Layer

Contains business rules, validations, and application logic.

### Data Access Layer

Handles communication with SQL Server using ADO.NET.

---

# 🛠️ Technologies

* C#
* .NET Framework
* Windows Forms
* SQL Server
* ADO.NET
* Three-Tier Architecture
* Object-Oriented Programming (OOP)

---

# 📂 Project Structure

```text
Driving-License-Management
│
├── DVLD-Presentation-Layer
│
├── DVLD-Business-Layer
│
├── DVLD-DataAccess-Layer
│
└── DVLD-DB
```

---

# 🚀 Getting Started

### Clone the repository

```bash
git clone https://github.com/HazemRagabElsayed/Driving-License-Management.git
```

### Setup

1. Restore the SQL Server database.
2. Update the connection string.
3. Open the solution in Visual Studio.
4. Build the solution.
5. Run the application.

---

# 🔐 Default Login

| Username     | Password |
| ------------ | -------- |
| **Msaqer77** | **1234** |

---

# 📸 Screenshots(To be Added)

Create a folder named **screenshots** and add application screenshots.

Example:

* Login
* Dashboard
* People Management
* Users
* Driver Licenses
* Test Appointments
* International Licenses
* License History
* Detained Licenses

---

# 🎯 Learning Objectives

This project demonstrates practical experience with:

* Three-Tier Architecture
* Windows Forms Development
* SQL Server Database Design
* ADO.NET
* CRUD Operations
* Object-Oriented Programming
* Layered Software Design
* Desktop Application Development

---

# 🚀 Future Improvements

* Role-Based Authorization
* Dashboard Analytics
* Report Generation
* Export to PDF & Excel
* Audit Logging
* Email Notifications

---

# 👨‍💻 Author

**Hazem Ragab Elsayed**

Software Engineer passionate about Desktop Applications, Databases, and Software Architecture.

---

<p align="center">

⭐ If you like this project, consider giving it a star!

</p>

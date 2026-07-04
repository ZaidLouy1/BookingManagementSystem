# Booking Management Service

A full-stack Booking Management System developed using **ASP.NET Core Web API**, **Angular**, and **SQL Server**.
The application allows users to create, retrieve, search, and cancel bookings for shared
resources while preventing overlapping reservations. The solution also implements an Audit Logging 
extension and includes unit tests for the core business logic.

-----------------------------------------------------------------------------------------------------------------------------------------

# Technology Stack

## Backend

- ASP.NET Core Web API (.NET 10)
- Entity Framework Core
- SQL Server
- Swagger / OpenAPI

## Frontend

- Angular
- TypeScript
- Bootstrap
- Angular HttpClient

## Testing

- xUnit

-------------------------------------------------------------------------------------------------------------------------------------------------------------------

# Features

## Backend

- Create Booking
- Retrieve All Bookings
- Retrieve Booking by ID
- Search Bookings by Resource and Date Range
- Cancel Booking (Soft Delete)
- Prevent Overlapping Bookings
- Audit Logging
- Swagger API Documentation

## Frontend

- View all bookings
- Create a booking
- Search bookings by resource
- Cancel active bookings
- Responsive Bootstrap interface

----------------------------------------------------------------------------------------------------------------------------------------------------------------------

# API Endpoints

| Method | Endpoint | Description |
|---------|----------|-------------|
| POST | `/api/Bookings` | Create a booking |
| GET | `/api/Bookings/GetAll` | Retrieve all bookings |
| GET | `/api/Bookings/{id}` | Retrieve booking by ID |
| GET | `/api/Bookings/GetByCriteria` | Search bookings |
| DELETE | `/api/Bookings/{id}` | Cancel a booking |
| GET | `/api/Bookings/AuditLogs` | Retrieve audit history |

-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

# Business Rules

## Booking Validation

The system enforces the following rules:

- A booking must have a valid start and end time.
- StartDateTime must be earlier than EndDateTime.
- A resource cannot have overlapping active bookings.
- Cancelled bookings no longer block future bookings.

The overlap rule is defined as:


NewStart < ExistingEnd
AND
NewEnd > ExistingStart

This allows a booking to begin exactly when another booking ends.

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

# Extension Task

## Auditability

The selected extension task is **Audit Logging**.

Every booking lifecycle event is recorded:

- Booking Created
- Booking Cancelled

Each audit record stores:

- Booking ID
- User Name
- Action
- Timestamp

The implementation provides a foundation that can later evolve toward event sourcing with minimal architectural changes.

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

# Unit Testing

The project includes unit tests covering the core business logic, including:

- Booking creation validation
- Overlap detection
- Booking cancellation
- Business rule validation

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

# Project Structure

BookingManagementService
│
├── BookingManagementService.API
│   ├── Controllers
│   ├── Models
│   ├── DTOs
│   ├── Data
│   ├── Services
│   ├── Migrations
│   └── Program.cs
│
├── BookingManagementFrontend
│   ├── Components
│   ├── Services
│   ├── Models
│   └── Angular Configuration
│
├── BookingManagement.Tests
│
└── README.md


-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

# Running the Project

## Backend

1. Configure the SQL Server connection string.
2. Apply Entity Framework migrations.
3. Run the API.
4. Open Swagger.


https://localhost:44340/swagger


-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

## Frontend

http://localhost:4201/

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

# Screenshots

- Home Page
- Create Booking
- Search Booking
- Swagger API
- Audit Logs
....
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

# Future Improvements

- Authentication & Authorization
- Pagination
  

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

# Author

Zaid Al-Masri

Princess Sumaya University for Technology

Software Engineering










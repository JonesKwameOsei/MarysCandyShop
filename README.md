# 🍬 Mary's Candy Shop - Inventory Management System

A **modern console-based CRUD application** built with **C# 13.0 (.NET 9)** that demonstrates advanced software development practices through a sophisticated candy shop inventory management system.

![.NET](https://img.shields.io/badge/.NET-9.0-blue) ![C#](https://img.shields.io/badge/C%23-13.0-purple) ![SQLite](https://img.shields.io/badge/SQLite-Database-green) ![Spectre.Console](https://img.shields.io/badge/Spectre.Console-UI-orange)

## 🚀 Latest Features & Updates

### **🔄 Recent Major Changes**
- **Database Migration**: Transitioned from CSV file storage to **SQLite database** for improved performance and data integrity
- **Interface Implementation**: Added `IProductsController` interface for better testability and dependency injection readiness
- **Enhanced CRUD Operations**: Complete database-backed CRUD functionality with parameterized queries
- **Improved Error Handling**: Comprehensive exception handling with user-friendly feedback using Spectre.Console
- **Data Seeding**: Automatic database initialization with sample candy products
- **Advanced Validation**: Custom validation response objects with detailed error messaging

### **📦 Core Features**
- **Product Management**: Full CRUD operations for candy inventory with SQLite persistence
- **Dual Product Types**: 
  - 🍫 **Chocolate Bars** with cocoa percentage tracking (0-99%)
  - 🍭 **Lollipops** with customizable shape descriptions
- **Beautiful Console UI**: Enhanced interface powered by [Spectre.Console](https://spectreconsole.net/) with styled panels and tables
- **Robust Validation**: Multi-layer input validation with custom response objects
- **Database Persistence**: SQLite database with automatic schema creation and migration
- **Unit Testing**: Comprehensive XUnit test suite ensuring code reliability
- **Business Logic Separation**: Clean architecture with distinct responsibility layers

## 🛠️ Technical Architecture

### **🏗️ Project Structure**
```
MarysCandyShop/
├── Models/
│   └── ValidationResponse.cs      # Custom validation response objects
├── ProductController.cs           # Database operations & business logic
├── UserInterface.cs              # Console interaction & menu system
├── Validation.cs                 # Input validation layer
├── Product.cs                    # Abstract product classes & implementations
├── DataSeed.cs                   # Database initialization & sample data
├── Helpers.cs                    # Utility functions & ID generation
├── Configuration.cs              # Application configuration
├── Enums.cs                      # Application enumerations
└── Program.cs                    # Application entry point
```

### **🎯 Key Design Patterns**
- **Repository Pattern**: `ProductController` acts as repository for data access
- **Interface Segregation**: `IProductsController` interface for contract definition
- **Abstract Factory**: `Product` base class with concrete implementations
- **Strategy Pattern**: Validation strategies for different input types
- **Dependency Injection Ready**: Interface-based design for future DI integration

## 💾 Database Schema

### **Products Table**
```sql
CREATE TABLE Products (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Type INTEGER NOT NULL,              -- 0: ChocolateBar, 1: Lollipop
    Name TEXT NOT NULL,
    Price REAL NOT NULL,
    CocoaPercentage INTEGER NULL,       -- Only for Chocolate Bars
    Shape TEXT NULL                     -- Only for Lollipops
);
```

## 📋 Enhanced Validation Rules

### **Product Names**
- ✅ Letters and spaces only (no numbers)
- ✅ Maximum 20 characters
- ✅ Cannot be empty or whitespace

### **Pricing**
- ✅ Range: $0.01 - $9,999.00
- ✅ Decimal precision supported
- ✅ Automatic currency formatting

### **Cocoa Percentage** (Chocolate Bars)
- ✅ Integer values: 0-99%
- ✅ Required for chocolate products
- ✅ Realistic percentage ranges

### **Shapes** (Lollipops)
- ✅ Descriptive text (letters/spaces only)
- ✅ Maximum 20 characters
- ✅ Creative shape descriptions encouraged

## 🎯 Learning Objectives Demonstrated

### **Object-Oriented Programming**
- **Inheritance & Polymorphism**: Abstract `Product` class with `ChocolateBar` and `Lollipop` implementations
- **Encapsulation**: Private fields with controlled access through properties
- **Interface Implementation**: `IProductsController` for contract-based programming

### **Database Development**
- **SQLite Integration**: Local database with Entity Framework-style operations
- **Parameterized Queries**: SQL injection prevention
- **Schema Management**: Automatic table creation and data migration

### **Modern C# Features**
- **Nullable Reference Types**: Enhanced null safety with C# 13.0
- **Pattern Matching**: Advanced switch expressions and type checking
- **Records & Value Objects**: Validation response objects
- **Global Using Statements**: Streamlined namespace management

### **Software Engineering Practices**
- **Separation of Concerns**: Distinct layers for UI, business logic, and data access
- **Single Responsibility Principle**: Focused class responsibilities
- **Open/Closed Principle**: Extensible product types through inheritance
- **Dependency Inversion**: Interface-based design

## 💻 Technologies & Dependencies

### **Core Framework**
- **C# 13.0** - Latest language features and performance improvements
- **.NET 9** - Modern runtime with enhanced performance
- **Microsoft.Data.Sqlite** (v9.0.8) - Database connectivity and operations

### **UI & Experience**
- **Spectre.Console** (v0.50.0) - Rich console UI with styling and interactions
- **ANSI Color Support** - Enhanced visual feedback and error messaging

### **Testing & Quality**
- **XUnit** (v2.9.2) - Modern unit testing framework
- **Microsoft.NET.Test.Sdk** (v17.12.0) - Test execution and discovery
- **Coverlet.Collector** (v6.0.2) - Code coverage analysis

## 🚀 Getting Started

### **Prerequisites**
- .NET 9 SDK or later
- SQLite support (included with .NET)
- Windows Terminal (recommended for best visual experience)

### **Installation & Running**
```bash
# Clone the repository
git clone https://github.com/JonesKwameOsei/MarysCandyShop.git

# Navigate to project directory
cd MarysCandyShop/MarysCandyShop

# Restore dependencies
dotnet restore

# Run the application
dotnet run
```

### **Running Tests**
```bash
# Navigate to test project
cd MarysCandyShop.UnitTest

# Run all tests
dotnet test

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"
```

## 🔒 Data Security & Best Practices

- **Parameterized Queries**: Protection against SQL injection attacks
- **Input Validation**: Multi-layer validation with detailed error responses
- **Error Handling**: Graceful exception handling with user-friendly messages
- **Data Integrity**: Database constraints and validation rules
- **Gitignore Configuration**: Sensitive files excluded from version control

## 📈 Future Enhancements

- **Web API Integration**: RESTful API for remote access
- **Entity Framework**: ORM integration for advanced database operations
- **Authentication**: User management and role-based access
- **Reporting**: Sales analytics and inventory reports
- **Cloud Deployment**: Azure/AWS hosting capabilities

## 🤝 Contributing

This project serves as an educational example of modern C# development practices. Contributions that enhance the learning experience are welcome!

---

*Perfect for developers learning C# fundamentals, object-oriented programming, database integration, and modern console application development with .NET 9.*

## 📄 License

This project is open source and available for everyone.
Feel free to use, and modify.
```
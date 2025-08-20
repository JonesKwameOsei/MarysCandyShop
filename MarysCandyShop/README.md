# 🍬 Mary's Candy Shop - Inventory Management System

A **console-based CRUD application** built with **C# (.NET 9)** that demonstrates modern software development practices through a candy shop inventory management system.

## 🚀 Features

- **Product Management**: Full CRUD operations for candy inventory
- **Dual Product Types**: 
  - 🍫 **Chocolate Bars** with cocoa percentage tracking (0-99%)
  - 🍭 **Lollipops** with customizable shape descriptions
- **Enhanced Console UI**: Beautiful interface powered by [Spectre.Console](https://spectreconsole.net/)
- **Robust Validation**: Comprehensive input validation with user-friendly error messages
- **File Persistence**: CSV-based data storage with automatic file handling
- **Unit Testing**: XUnit test suite ensuring code reliability

## 🛠️ Technical Highlights

- **Object-Oriented Design**: Abstract base classes with polymorphic inheritance
- **Separation of Concerns**: Clean architecture with distinct layers:
  - `UserInterface` - Console interaction and menu system
  - `ProductController` - Business logic and file operations  
  - `Validation` - Input validation with custom response objects
  - `Models` - Product entities and validation responses
- **Modern C# Features**: Utilizes C# 13.0 and .NET 9 capabilities
- **Error Handling**: Comprehensive exception handling and user feedback
- **Data Seeding**: Pre-populated sample data for immediate testing

## 📋 Core Validation Rules

- **Product Names**: Letters and spaces only, max 20 characters (no numbers allowed)
- **Prices**: $0.01 - $9,999.00 range with decimal precision
- **Cocoa Percentage**: Integer values 0-99% for chocolate products
- **Shapes**: Descriptive text for lollipops (letters/spaces only)

## 🏗️ Architecture Pattern
```
MarysCandyShop/
├── Models/                  # Data models and validation responses
├── UserInterface.cs         # Console UI and user interaction
├── ProductController.cs     # Business logic and file I/O
├── Validation.cs            # Input validation layer
├── Product.cs               # Abstract product classes
└── Tests/                   # Unit test suite
```

## 🎯 Learning Objectives Demonstrated

- **Inheritance & Polymorphism**: Abstract classes with concrete implementations
- **Validation Patterns**: Custom validation with detailed error responses  
- **File I/O Operations**: CSV reading/writing with error handling
- **Console Application Development**: Interactive menu systems
- **Unit Testing**: Test-driven validation logic
- **Modern C# Practices**: Nullable reference types, pattern matching

## 💻 Technologies Used

- **C# 13.0** / **.NET 9**
- **Spectre.Console** - Enhanced console UI
- **XUnit** - Unit testing framework
- **CSV File Storage** - Simple data persistence

---

*Perfect for developers learning C# fundamentals, object-oriented programming, and console application development.*
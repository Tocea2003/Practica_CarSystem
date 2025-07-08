# 🚗 Car Management System

[![.NET](https://img.shields.io/badge/.NET-8.0-purple)](https://dotnet.microsoft.com/)
[![Vue.js](https://img.shields.io/badge/Vue.js-3.0-green)](https://vuejs.org/)
[![TypeScript](https://img.shields.io/badge/TypeScript-5.0-blue)](https://www.typescriptlang.org/)
[![MySQL](https://img.shields.io/badge/MySQL-8.0-orange)](https://www.mysql.com/)

A modern full-stack web application for car inventory management, built with Vue.js 3 (TypeScript) frontend and .NET 8 backend.

## 📋 Table of Contents

- [🚀 Features](#-features)
- [🛠️ Tech Stack](#️-tech-stack)
- [📁 Project Structure](#-project-structure)
- [⚡ Quick Start](#-quick-start)
- [📚 API Documentation](#-api-documentation)
- [🎨 Screenshots](#-screenshots)
- [🤝 Contributing](#-contributing)
- [📄 License](#-license)

## 🚀 Features

### ✅ Core CRUD Operations:
- **🔍 View all cars** - GET `/api/masini`
- **➕ Add new car** - POST `/api/masini`
- **✏️ Edit existing car** - PUT `/api/masini/{id}`
- **🗑️ Delete car** - DELETE `/api/masini/{id}`
- **🔎 Search by ID** - GET `/api/masini/{id}`
- **✅ Data validation** - Frontend & Backend validation
- **🎨 Complete UI** - Built with PrimeVue components

### ✨ Enhanced Features:
- 🎨 **Modern responsive design** with minimalist UI/UX
- 🔔 **Toast notifications** for all user actions
- ⚠️ **Confirmation dialogs** for destructive operations
- ⚡ **Real-time validation** with error messages
- 🌱 **Auto-seeded database** on first run
- 🌐 **CORS configured** for development
- 🔄 **Auto-refresh** after CRUD operations
- 🔢 **Clean number formatting** for car year input
- 🌙 **Dark overlay modals** with maximum visibility

## 🛠️ Tech Stack

### Frontend
- **[Vue.js 3](https://vuejs.org/)** - Progressive JavaScript framework
- **[TypeScript](https://www.typescriptlang.org/)** - Typed JavaScript
- **[PrimeVue](https://primevue.org/)** - Rich UI component library
- **[Pinia](https://pinia.vuejs.org/)** - State management
- **[Axios](https://axios-http.com/)** - HTTP client
- **[Vite](https://vitejs.dev/)** - Fast build tool

### Backend
- **[.NET 8](https://dotnet.microsoft.com/)** - Cross-platform framework
- **[Entity Framework Core](https://docs.microsoft.com/en-us/ef/)** - ORM
- **[MySQL](https://www.mysql.com/)** - Database
- **[Pomelo.EntityFrameworkCore.MySql](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql)** - MySQL provider
- **[Swagger](https://swagger.io/)** - API documentation

## 📁 Project Structure

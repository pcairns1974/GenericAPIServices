
# Advanced HTTP Client Services

This repository contains a collection of advanced HTTP client services designed for flexible and robust API interaction in .NET applications. The services facilitate generic API calls, support varied data types and identifiers, and handle errors effectively.

## Features

- **Generic API Methods**: Flexible methods for API calls with support for various data types.
- **Support for Multiple IDs**: Methods support single or multiple IDs (e.g., `int`, `Guid`, `string`).
- **Robust Error Handling**: Includes custom exception handling for HTTP and JSON processing errors.
- **Expression-Based Filtering**: For count retrieval with LINQ-style expressions.

## Services

### 1. Generic Get Method
Retrieve a single resource of type `T` using one or multiple IDs.
```csharp
public async Task<T> Get<T>(string endpoint, params object[] ids) where T : class { ... }
```

### 2. Generic GetAll Method
Fetch all resources of type `T` from a specified endpoint.
```csharp
public async Task<IEnumerable<T>> GetAll<T>(string endpoint) where T : class { ... }
```

### 3. Generic GetAllByIds Method
Retrieve a collection of resources of type `T`, filtered by one or more IDs.
```csharp
public async Task<IEnumerable<T>> GetAllByIds<T>(string endpoint, params object[] ids) where T : class { ... }
```

### 4. Generic GetCount Method
Get the count of resources of type `T` that match a specified filter.
```csharp
public async Task<int> GetCount<T>(string endpoint, Expression<Func<T, bool>> filter) where T : class { ... }
```

## Sample Usage

### Sample 1: GetAllByIds with One ID
```csharp
// Retrieve products by category ID
var products = await GetAllByIds<Product>("https://api.example.com/products", 10);
```

### Sample 2: GetAllByIds with Two IDs
```csharp
// Retrieve items by category and supplier IDs
var items = await GetAllByIds<Item>("https://api.example.com/items", 20, 5);
```

### Sample 3: GetAllByIds with Mixed ID Types
```csharp
// Retrieve orders by customer ID and order date
var orders = await GetAllByIds<Order>("https://api.example.com/orders", 123, "2023-12-01");
```

## Custom Exceptions

- **ApiException**: For API request-related errors.
- **ApiRequestException**: For handling API request failures with detailed information.

## Installation

Include the service files in your .NET project and ensure dependencies like `Newtonsoft.Json` are installed.

## Contribution

Contributions are welcome. Please adhere to standard pull request guidelines for enhancements or bug fixes.

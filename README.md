# YojigenPoint.VaultPrime

[![MIT License](https://img.shields.io/badge/License-MIT-blue.svg?style=for-the-badge)](https://choosealicense.com/licenses/mit/)
[![Nuget](https://img.shields.io/nuget/v/YojigenPoint.VaultPrime?style=for-the-badge&color=purple)](https://www.nuget.org/packages/YojigenPoint.VaultPrime/)

**YojigenPoint.VaultPrime** is a lightweight, modern, and secure core utility library for .NET, designed to accelerate development by providing a powerful set of high-performance helpers and extensions. This library is built with zero external dependencies, ensuring it can be added to any project without unnecessary bloat.

## Features

-   **Fluent Extension Methods:** A collection of clean, readable, and performant extension methods for common types:
    -   `StringExtensions`: Robust methods for string manipulation and validation.
    -   `DateTimeExtensions`: Clear, UTC-based helpers for date and time comparisons.
    -   `UriExtensions`: Utilities for parsing and manipulating URIs.
-   **High-Performance Helpers:** A set of static helper classes for common, complex tasks:
    -   `GuidGenerator`: Includes a high-performance **COMB GUID** generator (`GenerateCombGuid()`) for creating sequential GUIDs, perfect for improving database index performance.
    -   `PasswordGenerator`: A utility for generating **cryptographically strong** random passwords with configurable rules, using `RandomNumberGenerator` for security. Also includes a simple password strength evaluator.

## Installation

This library is intended for distribution as a NuGet package. You can add it to your project using the .NET CLI:

```bash
dotnet add package YojigenPoint.VaultPrime
```

## Usage Examples
Here are a few examples of how VaultPrime can simplify your code.

### Generate a Sequential GUID
```
using YojigenPoint.VaultPrime.Helpers;

// Generates a new GUID where the last 6 bytes are a timestamp,
// making it ideal for use as a primary key in a database.
var newUserId = GuidGenerator.GenerateCombGuid();
```

### Generate a Secure Password
```
using YojigenPoint.VaultPrime.Helpers;

// Generate a strong, 16-character password with default rules.
var strongPassword = PasswordGenerator.Generate(16);
// Example output: "R@3&tK!p#9sB*wE7"
```

### Fluent String Replacement
```
using YojigenPoint.VaultPrime.Extensions;

var originalText = "Hello world, this is a test world.";
// Replaces only the last occurrence of "world".
var newText = originalText.ReplaceLast("world", "planet");
// Result: "Hello world, this is a test planet."
```

## Contributing
Contributions, issues, and feature requests are welcome. Feel free to check the [issues page](https://github.com/YojigenPoint/YojigenPoint.VaultPrime/issues) if you want to contribute.

### License
This project is licensed under the **MIT License**. See the [LICENSE.md](https://github.com/yojigenpoint/YojigenPoint.VaultPrime/blob/master/README.md) file for details.

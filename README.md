# SOLID Principles in C#

[![GitHub Sponsor](https://img.shields.io/badge/Sponsor-❤️-ff69b4?style=for-the-badge&logo=github&logoColor=white)](https://github.com/sponsors/CyberEgo)
[![Ko-Fi](https://img.shields.io/badge/Ko--fi-F16061?style=for-the-badge&logo=ko-fi&logoColor=white)](https://ko-fi.com/cyberego)

> 💡 **If this repository helped you learn SOLID principles, consider sponsoring to support more educational content!**

A comprehensive guide and implementation examples of SOLID principles in C#.

## 📚 What are SOLID Principles?

SOLID is an acronym for five design principles that make software designs more understandable, flexible, and maintainable:

- **S** - Single Responsibility Principle (SRP)
- **O** - Open/Closed Principle (OCP)
- **L** - Liskov Substitution Principle (LSP)
- **I** - Interface Segregation Principle (ISP)
- **D** - Dependency Inversion Principle (DIP)

## 🗂️ Repository Structure

```
├── docs/                          # Detailed documentation for each principle
│   ├── 01-single-responsibility.md
│   ├── 02-open-closed.md
│   ├── 03-liskov-substitution.md
│   ├── 04-interface-segregation.md
│   └── 05-dependency-inversion.md
├── src/                           # C# implementation examples
│   ├── 1-SingleResponsibility/
│   ├── 2-OpenClosed/
│   ├── 3-LiskovSubstitution/
│   ├── 4-InterfaceSegregation/
│   └── 5-DependencyInversion/
└── examples/                      # Real-world scenarios and anti-patterns
    ├── BadExamples/
    └── GoodExamples/
```

## 🚀 Quick Start

### Prerequisites
- .NET 6.0 or higher
- Any IDE (Visual Studio, VS Code, Rider, etc.)

### Running the Interactive Demo
```bash
# Clone the repository
git clone <your-repo-url>
cd SOLID-CSHARP

# Navigate to the source directory
cd src

# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run the interactive demonstration
dotnet run
```

The demo program provides an interactive menu to explore each SOLID principle with both violation and compliant examples.

### Manual Exploration
1. Clone this repository
2. Open in your favorite C# IDE (Visual Studio, VS Code, Rider)
3. Navigate through each principle's folder to see implementations
4. Read the documentation in the `docs/` folder for detailed explanations

## 📖 Learning Path

1. **Start with the documentation** - Read each principle's explanation in the `docs/` folder
2. **Review the code examples** - Examine both bad and good implementations
3. **Run the examples** - Execute the code to see the principles in action
4. **Practice** - Try implementing your own examples

## 🎯 Key Benefits of Following SOLID

- **Maintainability**: Easier to modify and extend code
- **Testability**: Code becomes more testable and mockable
- **Flexibility**: Better adaptation to changing requirements
- **Readability**: Cleaner, more understandable code structure
- **Reusability**: Components can be easily reused across projects

## 🤝 Contributing

Feel free to contribute by:
- Adding more examples
- Improving documentation
- Fixing bugs or typos
- Suggesting new scenarios

## 📝 License

This project is open source and available under the [MIT License](LICENSE).

---

*"The goal of software architecture is to minimize the human resources required to build and maintain the required system."* - Robert C. Martin
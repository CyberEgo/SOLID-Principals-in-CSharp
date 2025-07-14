# Single Responsibility Principle (SRP)

> "A class should have only one reason to change."
> 
> *- Robert C. Martin*

## 📖 Definition

The Single Responsibility Principle states that a class should have only one job or responsibility. This means that a class should only have one reason to change. When a class has multiple responsibilities, changes to one responsibility can affect the other responsibilities within the same class.

## 🎯 Why is SRP Important?

1. **Easier Maintenance**: Changes to one responsibility don't affect others
2. **Better Testing**: Smaller, focused classes are easier to test
3. **Improved Readability**: Code is more understandable when classes have clear purposes
4. **Reduced Coupling**: Classes become less dependent on each other
5. **Enhanced Reusability**: Single-purpose classes can be reused more easily

## ❌ Bad Example

```csharp
// This class violates SRP - it has multiple responsibilities
public class User
{
    public string Name { get; set; }
    public string Email { get; set; }
    
    // Responsibility 1: User data management
    public void UpdateProfile(string name, string email)
    {
        Name = name;
        Email = email;
    }
    
    // Responsibility 2: Email sending
    public void SendEmail(string message)
    {
        // Email sending logic
        Console.WriteLine($"Sending email to {Email}: {message}");
    }
    
    // Responsibility 3: Database operations
    public void SaveToDatabase()
    {
        // Database saving logic
        Console.WriteLine($"Saving user {Name} to database");
    }
    
    // Responsibility 4: Data validation
    public bool ValidateEmail()
    {
        return Email.Contains("@");
    }
}
```

**Problems with this approach:**
- If email sending logic changes, we need to modify the User class
- If database structure changes, we need to modify the User class
- If validation rules change, we need to modify the User class
- The class is difficult to test because it does too many things
- High coupling between different concerns

## ✅ Good Example

```csharp
// Each class has a single responsibility
public class User
{
    public string Name { get; set; }
    public string Email { get; set; }
    
    public void UpdateProfile(string name, string email)
    {
        Name = name;
        Email = email;
    }
}

public class EmailService
{
    public void SendEmail(string email, string message)
    {
        Console.WriteLine($"Sending email to {email}: {message}");
    }
}

public class UserRepository
{
    public void Save(User user)
    {
        Console.WriteLine($"Saving user {user.Name} to database");
    }
    
    public User GetById(int id)
    {
        // Database retrieval logic
        return new User();
    }
}

public class EmailValidator
{
    public bool IsValid(string email)
    {
        return !string.IsNullOrEmpty(email) && email.Contains("@");
    }
}

// Usage example
public class UserService
{
    private readonly EmailService _emailService;
    private readonly UserRepository _userRepository;
    private readonly EmailValidator _emailValidator;
    
    public UserService(EmailService emailService, UserRepository userRepository, EmailValidator emailValidator)
    {
        _emailService = emailService;
        _userRepository = userRepository;
        _emailValidator = emailValidator;
    }
    
    public void RegisterUser(User user)
    {
        if (_emailValidator.IsValid(user.Email))
        {
            _userRepository.Save(user);
            _emailService.SendEmail(user.Email, "Welcome!");
        }
    }
}
```

**Benefits of this approach:**
- Each class has a single, well-defined responsibility
- Changes to email logic only affect the EmailService
- Changes to database logic only affect the UserRepository
- Each class can be tested independently
- Classes can be reused in different contexts

## 🔍 How to Identify SRP Violations

Ask these questions about your classes:

1. **Multiple Reasons to Change**: Does this class change for more than one reason?
2. **Multiple Actors**: Does this class serve more than one actor or stakeholder?
3. **Descriptive Names**: Can you describe the class's responsibility in a single sentence without using "and" or "or"?
4. **Method Grouping**: Do the methods in this class operate on different types of data?

## 💡 Best Practices

1. **Keep Classes Small**: Smaller classes are easier to understand and maintain
2. **Clear Naming**: Use descriptive names that clearly indicate the class's single responsibility
3. **Cohesion**: Ensure all methods in a class work together toward the same goal
4. **Regular Refactoring**: Regularly review and refactor classes that have grown too large

## 🧪 Testing Benefits

With SRP, testing becomes much easier:

```csharp
[Test]
public void EmailValidator_Should_Return_True_For_Valid_Email()
{
    // Arrange
    var validator = new EmailValidator();
    
    // Act
    var result = validator.IsValid("test@example.com");
    
    // Assert
    Assert.True(result);
}

[Test]
public void UserRepository_Should_Save_User()
{
    // Arrange
    var repository = new UserRepository();
    var user = new User { Name = "John", Email = "john@example.com" };
    
    // Act & Assert
    Assert.DoesNotThrow(() => repository.Save(user));
}
```

## 🎯 Real-World Example

Consider a report generation system:

**Bad (Multiple Responsibilities):**
```csharp
public class Report
{
    // Data gathering
    public List<Sale> GetSalesData() { /* ... */ }
    
    // Formatting
    public string FormatAsHtml(List<Sale> sales) { /* ... */ }
    public string FormatAsPdf(List<Sale> sales) { /* ... */ }
    
    // Output
    public void PrintReport(string content) { /* ... */ }
    public void EmailReport(string content, string recipient) { /* ... */ }
}
```

**Good (Single Responsibilities):**
```csharp
public class SalesDataService
{
    public List<Sale> GetSalesData() { /* ... */ }
}

public class HtmlReportFormatter
{
    public string Format(List<Sale> sales) { /* ... */ }
}

public class PdfReportFormatter
{
    public string Format(List<Sale> sales) { /* ... */ }
}

public class ReportPrinter
{
    public void Print(string content) { /* ... */ }
}

public class ReportEmailer
{
    public void Email(string content, string recipient) { /* ... */ }
}
```

## 📚 Next Steps

- Review the code examples in `src/1-SingleResponsibility/`
- Practice identifying SRP violations in existing code
- Move on to the [Open/Closed Principle](02-open-closed.md)

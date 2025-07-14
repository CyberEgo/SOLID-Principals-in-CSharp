# Dependency Inversion Principle (DIP)

> "High-level modules should not depend on low-level modules. Both should depend on abstractions. Abstractions should not depend on details. Details should depend on abstractions."
> 
> *- Robert C. Martin*

## 📖 Definition

The Dependency Inversion Principle consists of two key concepts:

1. **High-level modules should not depend on low-level modules** - Both should depend on abstractions (interfaces)
2. **Abstractions should not depend on details** - Details should depend on abstractions

This principle is about inverting the traditional dependency flow where high-level classes directly depend on low-level classes.

## 🎯 Why is DIP Important?

1. **Flexibility**: Easy to swap implementations without changing high-level code
2. **Testability**: Can easily mock dependencies for unit testing
3. **Maintainability**: Changes in low-level modules don't affect high-level modules
4. **Reusability**: High-level modules can work with different implementations
5. **Decoupling**: Reduces tight coupling between modules

## ❌ Bad Example

```csharp
// Low-level modules (concrete implementations)
public class EmailService
{
    public void SendEmail(string to, string subject, string body)
    {
        Console.WriteLine($"Sending email to {to}: {subject}");
    }
}

public class SqlUserRepository
{
    public User GetUser(int id)
    {
        Console.WriteLine($"Getting user {id} from SQL database");
        return new User { Id = id, Name = "John Doe", Email = "john@example.com" };
    }
    
    public void SaveUser(User user)
    {
        Console.WriteLine($"Saving user {user.Name} to SQL database");
    }
}

// High-level module directly depends on low-level modules
public class UserService
{
    private readonly EmailService _emailService;           // Direct dependency
    private readonly SqlUserRepository _userRepository;    // Direct dependency
    
    public UserService()
    {
        _emailService = new EmailService();                // Tight coupling
        _userRepository = new SqlUserRepository();         // Tight coupling
    }
    
    public void RegisterUser(string name, string email)
    {
        var user = new User { Name = name, Email = email };
        _userRepository.SaveUser(user);
        _emailService.SendEmail(email, "Welcome!", "Welcome to our service!");
    }
    
    public User GetUser(int id)
    {
        return _userRepository.GetUser(id);
    }
}

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
```

**Problems with this approach:**
- `UserService` is tightly coupled to specific implementations
- Hard to test - can't mock `EmailService` or `SqlUserRepository`
- Hard to change - switching to different email service requires code changes
- Violates Open/Closed Principle - need to modify `UserService` to use different implementations

## ✅ Good Example

```csharp
// Define abstractions (interfaces)
public interface IEmailService
{
    void SendEmail(string to, string subject, string body);
}

public interface IUserRepository
{
    User GetUser(int id);
    void SaveUser(User user);
}

// Low-level modules implement abstractions
public class EmailService : IEmailService
{
    public void SendEmail(string to, string subject, string body)
    {
        Console.WriteLine($"Sending email to {to}: {subject}");
    }
}

public class SmsService : IEmailService  // Alternative implementation
{
    public void SendEmail(string to, string subject, string body)
    {
        Console.WriteLine($"Sending SMS to {to}: {body}");
    }
}

public class SqlUserRepository : IUserRepository
{
    public User GetUser(int id)
    {
        Console.WriteLine($"Getting user {id} from SQL database");
        return new User { Id = id, Name = "John Doe", Email = "john@example.com" };
    }
    
    public void SaveUser(User user)
    {
        Console.WriteLine($"Saving user {user.Name} to SQL database");
    }
}

public class MongoUserRepository : IUserRepository  // Alternative implementation
{
    public User GetUser(int id)
    {
        Console.WriteLine($"Getting user {id} from MongoDB");
        return new User { Id = id, Name = "Jane Doe", Email = "jane@example.com" };
    }
    
    public void SaveUser(User user)
    {
        Console.WriteLine($"Saving user {user.Name} to MongoDB");
    }
}

// High-level module depends on abstractions
public class UserService
{
    private readonly IEmailService _emailService;
    private readonly IUserRepository _userRepository;
    
    // Dependencies injected through constructor
    public UserService(IEmailService emailService, IUserRepository userRepository)
    {
        _emailService = emailService;
        _userRepository = userRepository;
    }
    
    public void RegisterUser(string name, string email)
    {
        var user = new User { Name = name, Email = email };
        _userRepository.SaveUser(user);
        _emailService.SendEmail(email, "Welcome!", "Welcome to our service!");
    }
    
    public User GetUser(int id)
    {
        return _userRepository.GetUser(id);
    }
}

// Composition root (where dependencies are wired up)
public class Program
{
    public static void Main()
    {
        // Can easily swap implementations
        IEmailService emailService = new EmailService();
        IUserRepository userRepository = new SqlUserRepository();
        
        var userService = new UserService(emailService, userRepository);
        userService.RegisterUser("John Doe", "john@example.com");
        
        // Or use different implementations
        IEmailService smsService = new SmsService();
        IUserRepository mongoRepository = new MongoUserRepository();
        
        var userServiceWithDifferentDeps = new UserService(smsService, mongoRepository);
        userServiceWithDifferentDeps.RegisterUser("Jane Doe", "jane@example.com");
    }
}

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
```

**Benefits of this approach:**
- `UserService` depends only on abstractions, not concrete implementations
- Easy to test by injecting mock implementations
- Easy to swap implementations without changing `UserService`
- Follows Open/Closed Principle - open for extension, closed for modification

## 🔧 Dependency Injection Patterns

### Constructor Injection (Recommended)
```csharp
public class OrderService
{
    private readonly IPaymentProcessor _paymentProcessor;
    private readonly IOrderRepository _orderRepository;
    private readonly IEmailService _emailService;
    
    public OrderService(
        IPaymentProcessor paymentProcessor,
        IOrderRepository orderRepository,
        IEmailService emailService)
    {
        _paymentProcessor = paymentProcessor ?? throw new ArgumentNullException(nameof(paymentProcessor));
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
    }
    
    public void ProcessOrder(Order order)
    {
        _paymentProcessor.ProcessPayment(order.Total);
        _orderRepository.Save(order);
        _emailService.SendEmail(order.CustomerEmail, "Order Confirmation", "Your order has been processed");
    }
}
```

### Property Injection (Less Common)
```csharp
public class OrderService
{
    public IPaymentProcessor PaymentProcessor { get; set; }
    public IOrderRepository OrderRepository { get; set; }
    public IEmailService EmailService { get; set; }
    
    public void ProcessOrder(Order order)
    {
        if (PaymentProcessor == null) throw new InvalidOperationException("PaymentProcessor not set");
        if (OrderRepository == null) throw new InvalidOperationException("OrderRepository not set");
        if (EmailService == null) throw new InvalidOperationException("EmailService not set");
        
        PaymentProcessor.ProcessPayment(order.Total);
        OrderRepository.Save(order);
        EmailService.SendEmail(order.CustomerEmail, "Order Confirmation", "Your order has been processed");
    }
}
```

### Method Injection (For Optional Dependencies)
```csharp
public class OrderService
{
    private readonly IPaymentProcessor _paymentProcessor;
    private readonly IOrderRepository _orderRepository;
    
    public OrderService(IPaymentProcessor paymentProcessor, IOrderRepository orderRepository)
    {
        _paymentProcessor = paymentProcessor;
        _orderRepository = orderRepository;
    }
    
    public void ProcessOrder(Order order, IEmailService emailService = null)
    {
        _paymentProcessor.ProcessPayment(order.Total);
        _orderRepository.Save(order);
        
        // Optional notification
        emailService?.SendEmail(order.CustomerEmail, "Order Confirmation", "Your order has been processed");
    }
}
```

## 🎯 Real-World Example: Logging System

**Bad (Violates DIP):**
```csharp
public class FileLogger
{
    public void Log(string message)
    {
        File.AppendAllText("log.txt", $"{DateTime.Now}: {message}\n");
    }
}

public class OrderProcessor
{
    private readonly FileLogger _logger = new FileLogger(); // Direct dependency
    
    public void ProcessOrder(Order order)
    {
        _logger.Log("Processing order...");
        // Process order logic
        _logger.Log("Order processed successfully");
    }
}
```

**Good (Follows DIP):**
```csharp
public interface ILogger
{
    void Log(string message);
    void LogError(string message, Exception exception = null);
    void LogWarning(string message);
}

public class FileLogger : ILogger
{
    private readonly string _filePath;
    
    public FileLogger(string filePath = "log.txt")
    {
        _filePath = filePath;
    }
    
    public void Log(string message)
    {
        File.AppendAllText(_filePath, $"{DateTime.Now}: INFO - {message}\n");
    }
    
    public void LogError(string message, Exception exception = null)
    {
        var errorMessage = exception != null ? $"{message} - {exception}" : message;
        File.AppendAllText(_filePath, $"{DateTime.Now}: ERROR - {errorMessage}\n");
    }
    
    public void LogWarning(string message)
    {
        File.AppendAllText(_filePath, $"{DateTime.Now}: WARNING - {message}\n");
    }
}

public class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"{DateTime.Now}: INFO - {message}");
    }
    
    public void LogError(string message, Exception exception = null)
    {
        var errorMessage = exception != null ? $"{message} - {exception}" : message;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"{DateTime.Now}: ERROR - {errorMessage}");
        Console.ResetColor();
    }
    
    public void LogWarning(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"{DateTime.Now}: WARNING - {message}");
        Console.ResetColor();
    }
}

public class DatabaseLogger : ILogger
{
    private readonly string _connectionString;
    
    public DatabaseLogger(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public void Log(string message)
    {
        // Insert log entry into database
        Console.WriteLine($"DB Log: {DateTime.Now}: INFO - {message}");
    }
    
    public void LogError(string message, Exception exception = null)
    {
        var errorMessage = exception != null ? $"{message} - {exception}" : message;
        Console.WriteLine($"DB Log: {DateTime.Now}: ERROR - {errorMessage}");
    }
    
    public void LogWarning(string message)
    {
        Console.WriteLine($"DB Log: {DateTime.Now}: WARNING - {message}");
    }
}

public class OrderProcessor
{
    private readonly ILogger _logger;
    
    public OrderProcessor(ILogger logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    public void ProcessOrder(Order order)
    {
        try
        {
            _logger.Log($"Processing order {order.Id}...");
            
            if (order.Total <= 0)
            {
                _logger.LogWarning($"Order {order.Id} has invalid total: {order.Total}");
                return;
            }
            
            // Process order logic here
            
            _logger.Log($"Order {order.Id} processed successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to process order {order.Id}", ex);
            throw;
        }
    }
}

// Usage with different loggers
public class Program
{
    public static void Main()
    {
        var order = new Order { Id = 1, Total = 100.00m };
        
        // Use file logger
        var fileLogger = new FileLogger("orders.log");
        var orderProcessor1 = new OrderProcessor(fileLogger);
        orderProcessor1.ProcessOrder(order);
        
        // Use console logger
        var consoleLogger = new ConsoleLogger();
        var orderProcessor2 = new OrderProcessor(consoleLogger);
        orderProcessor2.ProcessOrder(order);
        
        // Use database logger
        var dbLogger = new DatabaseLogger("connectionString");
        var orderProcessor3 = new OrderProcessor(dbLogger);
        orderProcessor3.ProcessOrder(order);
    }
}
```

## 🏗️ IoC Container Example

```csharp
// Simple IoC container implementation
public interface IContainer
{
    void Register<TInterface, TImplementation>() 
        where TImplementation : class, TInterface;
    T Resolve<T>();
}

public class SimpleContainer : IContainer
{
    private readonly Dictionary<Type, Type> _registrations = new();
    
    public void Register<TInterface, TImplementation>() 
        where TImplementation : class, TInterface
    {
        _registrations[typeof(TInterface)] = typeof(TImplementation);
    }
    
    public T Resolve<T>()
    {
        return (T)Resolve(typeof(T));
    }
    
    private object Resolve(Type type)
    {
        if (_registrations.TryGetValue(type, out var implementationType))
        {
            var constructors = implementationType.GetConstructors();
            var constructor = constructors.First();
            var parameters = constructor.GetParameters();
            
            if (parameters.Length == 0)
            {
                return Activator.CreateInstance(implementationType);
            }
            
            var args = parameters.Select(p => Resolve(p.ParameterType)).ToArray();
            return Activator.CreateInstance(implementationType, args);
        }
        
        throw new InvalidOperationException($"Type {type.Name} is not registered");
    }
}

// Usage
public class Program
{
    public static void Main()
    {
        var container = new SimpleContainer();
        
        // Register dependencies
        container.Register<ILogger, ConsoleLogger>();
        container.Register<IUserRepository, SqlUserRepository>();
        container.Register<IEmailService, EmailService>();
        
        // Resolve high-level service (dependencies automatically injected)
        var userService = container.Resolve<UserService>();
        userService.RegisterUser("John Doe", "john@example.com");
    }
}
```

## 🧪 Testing with DIP

```csharp
[TestFixture]
public class UserServiceTests
{
    private Mock<IEmailService> _mockEmailService;
    private Mock<IUserRepository> _mockUserRepository;
    private UserService _userService;
    
    [SetUp]
    public void Setup()
    {
        _mockEmailService = new Mock<IEmailService>();
        _mockUserRepository = new Mock<IUserRepository>();
        _userService = new UserService(_mockEmailService.Object, _mockUserRepository.Object);
    }
    
    [Test]
    public void RegisterUser_ShouldSaveUserAndSendEmail()
    {
        // Arrange
        var name = "John Doe";
        var email = "john@example.com";
        
        // Act
        _userService.RegisterUser(name, email);
        
        // Assert
        _mockUserRepository.Verify(r => r.SaveUser(It.Is<User>(u => u.Name == name && u.Email == email)), Times.Once);
        _mockEmailService.Verify(e => e.SendEmail(email, "Welcome!", "Welcome to our service!"), Times.Once);
    }
    
    [Test]
    public void GetUser_ShouldReturnUserFromRepository()
    {
        // Arrange
        var userId = 1;
        var expectedUser = new User { Id = userId, Name = "John Doe", Email = "john@example.com" };
        _mockUserRepository.Setup(r => r.GetUser(userId)).Returns(expectedUser);
        
        // Act
        var result = _userService.GetUser(userId);
        
        // Assert
        Assert.AreEqual(expectedUser, result);
        _mockUserRepository.Verify(r => r.GetUser(userId), Times.Once);
    }
}
```

## 🔍 How to Identify DIP Violations

Look for these patterns:

1. **Direct instantiation** of concrete classes with `new` keyword
2. **Static method calls** to concrete implementations
3. **Hard-coded dependencies** that can't be changed
4. **Difficult testing** due to concrete dependencies
5. **Cascading changes** when low-level modules change

## 💡 Best Practices

1. **Depend on abstractions**: Always depend on interfaces, not concrete classes
2. **Constructor injection**: Prefer constructor injection for required dependencies
3. **Composition root**: Have one place where all dependencies are wired up
4. **Interface segregation**: Keep interfaces focused and minimal
5. **Avoid service locator**: Don't use service locator pattern as it hides dependencies

## ⚠️ Common Pitfalls

1. **Over-abstraction**: Don't create interfaces for everything, only when you need flexibility
2. **Leaky abstractions**: Ensure abstractions don't expose implementation details
3. **Circular dependencies**: Be careful not to create circular dependency chains
4. **Poor naming**: Use clear, intention-revealing names for interfaces

## 📚 Summary

The Dependency Inversion Principle is fundamental to creating flexible, testable, and maintainable code. By depending on abstractions rather than concrete implementations, you can:

- Easily swap implementations
- Write comprehensive unit tests
- Reduce coupling between modules
- Follow the Open/Closed Principle
- Create more flexible architectures

## 📚 Next Steps

- Review the code examples in `src/5-DependencyInversion/`
- Practice identifying and refactoring DIP violations in existing code
- Explore dependency injection frameworks like Microsoft.Extensions.DependencyInjection
- Learn about advanced patterns like Decorator, Strategy, and Factory that work well with DIP

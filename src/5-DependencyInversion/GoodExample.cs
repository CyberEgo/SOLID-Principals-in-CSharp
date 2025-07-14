// Dependency Inversion Principle - GOOD Example
// This code follows DIP by depending on abstractions rather than concrete implementations

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SolidPrinciples.DependencyInversion.Good
{
    // Abstractions (interfaces) that define contracts
    
    public interface IUserRepository
    {
        void Save(User user);
        User GetById(int id);
        User GetByEmail(string email);
        void Delete(int id);
        IEnumerable<User> GetAll();
    }
    
    public interface IEmailService
    {
        void SendEmail(string to, string subject, string body);
        void SendEmail(string to, string subject, string body, List<string> attachments);
    }
    
    public interface ISmsService
    {
        void SendSms(string phoneNumber, string message);
    }
    
    public interface ILogger
    {
        void Log(string message);
        void LogWarning(string message);
        void LogError(string message);
        void LogError(string message, Exception exception);
    }
    
    public interface INotificationService
    {
        void SendWelcomeNotification(User user);
        void SendOrderConfirmation(string customerId, Order order);
        void SendPaymentConfirmation(string customerId, Payment payment);
    }
    
    public interface IOrderRepository
    {
        void Save(Order order);
        Order GetById(int id);
        IEnumerable<Order> GetByCustomerId(string customerId);
    }
    
    public interface IPaymentRepository
    {
        void Save(Payment payment);
        Payment GetById(int id);
        IEnumerable<Payment> GetByCustomerId(string customerId);
    }
    
    public interface IPaymentProcessor
    {
        PaymentResult ProcessPayment(decimal amount, string paymentMethod);
    }
    
    // Domain models
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public User()
        {
            CreatedAt = DateTime.Now;
        }
    }
    
    public class Order
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public string ProductId { get; set; }
        public decimal Amount { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        
        public Order()
        {
            OrderDate = DateTime.Now;
            Status = "Pending";
        }
    }
    
    public class Payment
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        
        public Payment()
        {
            PaymentDate = DateTime.Now;
        }
    }
    
    public class PaymentResult
    {
        public bool IsSuccess { get; set; }
        public string TransactionId { get; set; }
        public string Message { get; set; }
        public Exception Error { get; set; }
        
        public static PaymentResult Success(string transactionId)
        {
            return new PaymentResult
            {
                IsSuccess = true,
                TransactionId = transactionId,
                Message = "Payment processed successfully"
            };
        }
        
        public static PaymentResult Failure(string message, Exception error = null)
        {
            return new PaymentResult
            {
                IsSuccess = false,
                Message = message,
                Error = error
            };
        }
    }
    
    // Concrete implementations of abstractions (low-level modules)
    
    public class SqlUserRepository : IUserRepository
    {
        private readonly string _connectionString;
        private readonly ILogger _logger;
        
        public SqlUserRepository(string connectionString, ILogger logger)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        public void Save(User user)
        {
            _logger.Log($"Saving user {user.Email} to SQL database");
            Console.WriteLine($"💾 SQL: INSERT INTO Users (Name, Email, Phone) VALUES ('{user.Name}', '{user.Email}', '{user.Phone}')");
            Console.WriteLine($"Connection: {_connectionString}");
        }
        
        public User GetById(int id)
        {
            _logger.Log($"Retrieving user {id} from SQL database");
            Console.WriteLine($"🔍 SQL: SELECT * FROM Users WHERE Id = {id}");
            return new User { Id = id, Name = "John Doe", Email = "john@email.com", Phone = "+1234567890" };
        }
        
        public User GetByEmail(string email)
        {
            _logger.Log($"Retrieving user by email {email} from SQL database");
            Console.WriteLine($"🔍 SQL: SELECT * FROM Users WHERE Email = '{email}'");
            return new User { Id = 1, Name = "John Doe", Email = email, Phone = "+1234567890" };
        }
        
        public void Delete(int id)
        {
            _logger.Log($"Deleting user {id} from SQL database");
            Console.WriteLine($"🗑️ SQL: DELETE FROM Users WHERE Id = {id}");
        }
        
        public IEnumerable<User> GetAll()
        {
            _logger.Log("Retrieving all users from SQL database");
            Console.WriteLine("🔍 SQL: SELECT * FROM Users");
            return new List<User>
            {
                new User { Id = 1, Name = "John Doe", Email = "john@email.com" },
                new User { Id = 2, Name = "Jane Smith", Email = "jane@email.com" }
            };
        }
    }
    
    public class MongoUserRepository : IUserRepository
    {
        private readonly string _connectionString;
        private readonly ILogger _logger;
        
        public MongoUserRepository(string connectionString, ILogger logger)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        public void Save(User user)
        {
            _logger.Log($"Saving user {user.Email} to MongoDB");
            Console.WriteLine($"💾 MongoDB: db.users.insertOne({user.Name}, {user.Email}, {user.Phone})");
            Console.WriteLine($"Connection: {_connectionString}");
        }
        
        public User GetById(int id)
        {
            _logger.Log($"Retrieving user {id} from MongoDB");
            Console.WriteLine($"🔍 MongoDB: db.users.findOne({{_id: {id}}})");
            return new User { Id = id, Name = "John Doe", Email = "john@email.com", Phone = "+1234567890" };
        }
        
        public User GetByEmail(string email)
        {
            _logger.Log($"Retrieving user by email {email} from MongoDB");
            Console.WriteLine($"🔍 MongoDB: db.users.findOne({{email: '{email}'}})");
            return new User { Id = 1, Name = "John Doe", Email = email, Phone = "+1234567890" };
        }
        
        public void Delete(int id)
        {
            _logger.Log($"Deleting user {id} from MongoDB");
            Console.WriteLine($"🗑️ MongoDB: db.users.deleteOne({{_id: {id}}})");
        }
        
        public IEnumerable<User> GetAll()
        {
            _logger.Log("Retrieving all users from MongoDB");
            Console.WriteLine("🔍 MongoDB: db.users.find({})");
            return new List<User>
            {
                new User { Id = 1, Name = "John Doe", Email = "john@email.com" },
                new User { Id = 2, Name = "Jane Smith", Email = "jane@email.com" }
            };
        }
    }
    
    public class SmtpEmailService : IEmailService
    {
        private readonly string _smtpServer;
        private readonly int _port;
        private readonly ILogger _logger;
        
        public SmtpEmailService(string smtpServer, int port, ILogger logger)
        {
            _smtpServer = smtpServer ?? throw new ArgumentNullException(nameof(smtpServer));
            _port = port;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        public void SendEmail(string to, string subject, string body)
        {
            _logger.Log($"Sending email to {to} via SMTP");
            Console.WriteLine($"📧 SMTP Email sent via {_smtpServer}:{_port}");
            Console.WriteLine($"To: {to}, Subject: {subject}");
        }
        
        public void SendEmail(string to, string subject, string body, List<string> attachments)
        {
            _logger.Log($"Sending email with {attachments.Count} attachments to {to} via SMTP");
            Console.WriteLine($"📧 SMTP Email with attachments sent via {_smtpServer}:{_port}");
            Console.WriteLine($"To: {to}, Subject: {subject}, Attachments: {string.Join(", ", attachments)}");
        }
    }
    
    public class SendGridEmailService : IEmailService
    {
        private readonly string _apiKey;
        private readonly ILogger _logger;
        
        public SendGridEmailService(string apiKey, ILogger logger)
        {
            _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        public void SendEmail(string to, string subject, string body)
        {
            _logger.Log($"Sending email to {to} via SendGrid");
            Console.WriteLine($"📧 SendGrid Email sent using API");
            Console.WriteLine($"To: {to}, Subject: {subject}");
        }
        
        public void SendEmail(string to, string subject, string body, List<string> attachments)
        {
            _logger.Log($"Sending email with {attachments.Count} attachments to {to} via SendGrid");
            Console.WriteLine($"📧 SendGrid Email with attachments sent using API");
            Console.WriteLine($"To: {to}, Subject: {subject}, Attachments: {string.Join(", ", attachments)}");
        }
    }
    
    public class TwilioSmsService : ISmsService
    {
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly ILogger _logger;
        
        public TwilioSmsService(string accountSid, string authToken, ILogger logger)
        {
            _accountSid = accountSid ?? throw new ArgumentNullException(nameof(accountSid));
            _authToken = authToken ?? throw new ArgumentNullException(nameof(authToken));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        public void SendSms(string phoneNumber, string message)
        {
            _logger.Log($"Sending SMS to {phoneNumber} via Twilio");
            Console.WriteLine($"📱 Twilio SMS sent to {phoneNumber}");
            Console.WriteLine($"Message: {message}");
        }
    }
    
    public class FileLogger : ILogger
    {
        private readonly string _logFilePath;
        
        public FileLogger(string logFilePath)
        {
            _logFilePath = logFilePath ?? throw new ArgumentNullException(nameof(logFilePath));
        }
        
        public void Log(string message)
        {
            var logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [INFO] {message}";
            Console.WriteLine($"📝 FILE LOG: {logEntry}");
        }
        
        public void LogWarning(string message)
        {
            var logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [WARN] {message}";
            Console.WriteLine($"⚠️ FILE LOG: {logEntry}");
        }
        
        public void LogError(string message)
        {
            var logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [ERROR] {message}";
            Console.WriteLine($"❌ FILE LOG: {logEntry}");
        }
        
        public void LogError(string message, Exception exception)
        {
            var logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [ERROR] {message} - Exception: {exception.Message}";
            Console.WriteLine($"❌ FILE LOG: {logEntry}");
        }
    }
    
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"📝 CONSOLE LOG: [INFO] {message}");
        }
        
        public void LogWarning(string message)
        {
            Console.WriteLine($"⚠️ CONSOLE LOG: [WARN] {message}");
        }
        
        public void LogError(string message)
        {
            Console.WriteLine($"❌ CONSOLE LOG: [ERROR] {message}");
        }
        
        public void LogError(string message, Exception exception)
        {
            Console.WriteLine($"❌ CONSOLE LOG: [ERROR] {message} - Exception: {exception.Message}");
        }
    }
    
    public class CompositeNotificationService : INotificationService
    {
        private readonly IEmailService _emailService;
        private readonly ISmsService _smsService;
        private readonly ILogger _logger;
        
        public CompositeNotificationService(IEmailService emailService, ISmsService smsService, ILogger logger)
        {
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _smsService = smsService ?? throw new ArgumentNullException(nameof(smsService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        public void SendWelcomeNotification(User user)
        {
            _logger.Log($"Sending welcome notification to user {user.Email}");
            
            _emailService.SendEmail(user.Email, "Welcome!", 
                $"Welcome {user.Name}! Your account has been created successfully.");
            
            if (!string.IsNullOrEmpty(user.Phone))
            {
                _smsService.SendSms(user.Phone, $"Hi {user.Name}! Welcome to our platform.");
            }
        }
        
        public void SendOrderConfirmation(string customerId, Order order)
        {
            _logger.Log($"Sending order confirmation for order {order.Id}");
            
            _emailService.SendEmail($"customer{customerId}@email.com", "Order Confirmation",
                $"Your order #{order.Id} for ${order.Amount} has been confirmed.");
        }
        
        public void SendPaymentConfirmation(string customerId, Payment payment)
        {
            _logger.Log($"Sending payment confirmation for payment {payment.Id}");
            
            _emailService.SendEmail($"customer{customerId}@email.com", "Payment Confirmation",
                $"Your payment of ${payment.Amount} has been processed successfully. Transaction ID: {payment.Id}");
        }
    }
    
    // High-level modules that depend on abstractions (following DIP)
    
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly INotificationService _notificationService;
        private readonly ILogger _logger;
        
        // Dependencies injected through constructor
        public UserService(IUserRepository userRepository, INotificationService notificationService, ILogger logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        public void RegisterUser(string name, string email, string phone)
        {
            try
            {
                _logger.Log($"Starting user registration for {email}");
                
                var user = new User
                {
                    Name = name,
                    Email = email,
                    Phone = phone
                };
                
                _userRepository.Save(user);
                _logger.Log($"User {email} saved successfully");
                
                _notificationService.SendWelcomeNotification(user);
                _logger.Log($"Welcome notification sent to {email}");
                
                _logger.Log($"User registration completed for {email}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error during user registration for {email}", ex);
                throw;
            }
        }
        
        public User GetUser(int id)
        {
            try
            {
                _logger.Log($"Retrieving user {id}");
                var user = _userRepository.GetById(id);
                _logger.Log($"User {id} retrieved successfully");
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving user {id}", ex);
                throw;
            }
        }
        
        public void DeleteUser(int id)
        {
            try
            {
                _logger.Log($"Deleting user {id}");
                _userRepository.Delete(id);
                _logger.Log($"User {id} deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting user {id}", ex);
                throw;
            }
        }
        
        public IEnumerable<User> GetAllUsers()
        {
            try
            {
                _logger.Log("Retrieving all users");
                var users = _userRepository.GetAll();
                _logger.Log($"Retrieved {users.Count()} users");
                return users;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error retrieving all users", ex);
                throw;
            }
        }
    }
    
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly INotificationService _notificationService;
        private readonly ILogger _logger;
        
        public OrderService(IOrderRepository orderRepository, INotificationService notificationService, ILogger logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        public void ProcessOrder(string customerId, string productId, decimal amount)
        {
            try
            {
                _logger.Log($"Processing order for customer {customerId}");
                
                var order = new Order
                {
                    CustomerId = customerId,
                    ProductId = productId,
                    Amount = amount,
                    Status = "Confirmed"
                };
                
                _orderRepository.Save(order);
                _logger.Log($"Order saved for customer {customerId}");
                
                _notificationService.SendOrderConfirmation(customerId, order);
                _logger.Log($"Order confirmation sent for customer {customerId}");
                
                _logger.Log($"Order processing completed for customer {customerId}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing order for customer {customerId}", ex);
                throw;
            }
        }
    }
    
    public class PaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPaymentProcessor _paymentProcessor;
        private readonly INotificationService _notificationService;
        private readonly ILogger _logger;
        
        public PaymentService(IPaymentRepository paymentRepository, IPaymentProcessor paymentProcessor,
                            INotificationService notificationService, ILogger logger)
        {
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
            _paymentProcessor = paymentProcessor ?? throw new ArgumentNullException(nameof(paymentProcessor));
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        public void ProcessPayment(string customerId, decimal amount, string paymentMethod)
        {
            try
            {
                _logger.Log($"Processing payment of ${amount} for customer {customerId}");
                
                var paymentResult = _paymentProcessor.ProcessPayment(amount, paymentMethod);
                
                if (paymentResult.IsSuccess)
                {
                    var payment = new Payment
                    {
                        CustomerId = customerId,
                        Amount = amount,
                        PaymentMethod = paymentMethod,
                        Status = "Completed"
                    };
                    
                    _paymentRepository.Save(payment);
                    _logger.Log($"Payment saved for customer {customerId}");
                    
                    _notificationService.SendPaymentConfirmation(customerId, payment);
                    _logger.Log($"Payment confirmation sent for customer {customerId}");
                    
                    _logger.Log($"Payment processing completed for customer {customerId}");
                }
                else
                {
                    _logger.LogError($"Payment failed for customer {customerId}: {paymentResult.Message}");
                    throw new InvalidOperationException($"Payment failed: {paymentResult.Message}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing payment for customer {customerId}", ex);
                throw;
            }
        }
    }
    
    // Composition root - where all dependencies are wired up
    public class DIContainer
    {
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();
        
        public void RegisterSingleton<TInterface, TImplementation>(TImplementation implementation)
            where TImplementation : class, TInterface
        {
            _services[typeof(TInterface)] = implementation;
        }
        
        public T GetService<T>()
        {
            if (_services.TryGetValue(typeof(T), out var service))
            {
                return (T)service;
            }
            throw new InvalidOperationException($"Service of type {typeof(T).Name} is not registered");
        }
    }
    
    public class ApplicationCompositionRoot
    {
        public static DIContainer ConfigureServices()
        {
            var container = new DIContainer();
            
            // Configure logger
            var logger = new ConsoleLogger(); // Could easily switch to FileLogger
            container.RegisterSingleton<ILogger, ConsoleLogger>(logger);
            
            // Configure repositories
            var userRepository = new SqlUserRepository("Server=localhost;Database=Users;", logger);
            // Could easily switch to: var userRepository = new MongoUserRepository("mongodb://localhost", logger);
            container.RegisterSingleton<IUserRepository, SqlUserRepository>(userRepository);
            
            // Configure email service
            var emailService = new SmtpEmailService("smtp.gmail.com", 587, logger);
            // Could easily switch to: var emailService = new SendGridEmailService("api_key", logger);
            container.RegisterSingleton<IEmailService, SmtpEmailService>(emailService);
            
            // Configure SMS service
            var smsService = new TwilioSmsService("account_sid", "auth_token", logger);
            container.RegisterSingleton<ISmsService, TwilioSmsService>(smsService);
            
            // Configure notification service
            var notificationService = new CompositeNotificationService(emailService, smsService, logger);
            container.RegisterSingleton<INotificationService, CompositeNotificationService>(notificationService);
            
            return container;
        }
    }
    
    // Mock implementations for testing
    public class MockUserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();
        private int _nextId = 1;
        
        public void Save(User user)
        {
            if (user.Id == 0)
            {
                user.Id = _nextId++;
            }
            _users.Add(user);
        }
        
        public User GetById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }
        
        public User GetByEmail(string email)
        {
            return _users.FirstOrDefault(u => u.Email == email);
        }
        
        public void Delete(int id)
        {
            var user = GetById(id);
            if (user != null)
            {
                _users.Remove(user);
            }
        }
        
        public IEnumerable<User> GetAll()
        {
            return _users.ToList();
        }
    }
    
    public class MockEmailService : IEmailService
    {
        public List<EmailMessage> SentEmails { get; } = new List<EmailMessage>();
        
        public void SendEmail(string to, string subject, string body)
        {
            SentEmails.Add(new EmailMessage { To = to, Subject = subject, Body = body });
        }
        
        public void SendEmail(string to, string subject, string body, List<string> attachments)
        {
            SentEmails.Add(new EmailMessage { To = to, Subject = subject, Body = body, Attachments = attachments });
        }
        
        public class EmailMessage
        {
            public string To { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
            public List<string> Attachments { get; set; } = new List<string>();
        }
    }
    
    public class MockLogger : ILogger
    {
        public List<LogEntry> LogEntries { get; } = new List<LogEntry>();
        
        public void Log(string message)
        {
            LogEntries.Add(new LogEntry { Level = "INFO", Message = message, Timestamp = DateTime.Now });
        }
        
        public void LogWarning(string message)
        {
            LogEntries.Add(new LogEntry { Level = "WARN", Message = message, Timestamp = DateTime.Now });
        }
        
        public void LogError(string message)
        {
            LogEntries.Add(new LogEntry { Level = "ERROR", Message = message, Timestamp = DateTime.Now });
        }
        
        public void LogError(string message, Exception exception)
        {
            LogEntries.Add(new LogEntry 
            { 
                Level = "ERROR", 
                Message = message, 
                Exception = exception, 
                Timestamp = DateTime.Now 
            });
        }
        
        public class LogEntry
        {
            public string Level { get; set; }
            public string Message { get; set; }
            public Exception Exception { get; set; }
            public DateTime Timestamp { get; set; }
        }
    }
    
    // Usage examples demonstrating the benefits
    public class DIPComplianceExamples
    {
        public static void DemonstrateFlexibility()
        {
            Console.WriteLine("=== Demonstrating Flexibility with Different Implementations ===\n");
            
            // Configuration 1: SQL + SMTP + File Logging
            Console.WriteLine("🔧 Configuration 1: SQL Database + SMTP Email + File Logging");
            var logger1 = new FileLogger("app.log");
            var userRepo1 = new SqlUserRepository("sql_connection", logger1);
            var emailService1 = new SmtpEmailService("smtp.server.com", 587, logger1);
            var smsService1 = new TwilioSmsService("sid", "token", logger1);
            var notificationService1 = new CompositeNotificationService(emailService1, smsService1, logger1);
            var userService1 = new UserService(userRepo1, notificationService1, logger1);
            
            userService1.RegisterUser("John Doe", "john@email.com", "+1234567890");
            Console.WriteLine();
            
            // Configuration 2: MongoDB + SendGrid + Console Logging
            Console.WriteLine("🔧 Configuration 2: MongoDB + SendGrid Email + Console Logging");
            var logger2 = new ConsoleLogger();
            var userRepo2 = new MongoUserRepository("mongodb_connection", logger2);
            var emailService2 = new SendGridEmailService("sendgrid_api_key", logger2);
            var smsService2 = new TwilioSmsService("sid", "token", logger2);
            var notificationService2 = new CompositeNotificationService(emailService2, smsService2, logger2);
            var userService2 = new UserService(userRepo2, notificationService2, logger2);
            
            userService2.RegisterUser("Jane Smith", "jane@email.com", "+1987654321");
            Console.WriteLine();
            
            Console.WriteLine("✅ Same high-level code works with completely different implementations!");
        }
        
        public static void DemonstrateTestability()
        {
            Console.WriteLine("=== Demonstrating Easy Testing with Mocks ===\n");
            
            // Create mock implementations for testing
            var mockLogger = new MockLogger();
            var mockUserRepo = new MockUserRepository();
            var mockEmailService = new MockEmailService();
            var mockSmsService = new TwilioSmsService("test_sid", "test_token", mockLogger); // Could create mock
            var mockNotificationService = new CompositeNotificationService(mockEmailService, mockSmsService, mockLogger);
            
            // Create service with mocked dependencies
            var userService = new UserService(mockUserRepo, mockNotificationService, mockLogger);
            
            // Test user registration
            userService.RegisterUser("Test User", "test@email.com", "+1111111111");
            
            // Verify behavior using mocks
            Console.WriteLine("📊 Test Results:");
            Console.WriteLine($"✅ Users in repository: {mockUserRepo.GetAll().Count()}");
            Console.WriteLine($"✅ Emails sent: {mockEmailService.SentEmails.Count}");
            Console.WriteLine($"✅ Log entries: {mockLogger.LogEntries.Count}");
            
            var sentEmail = mockEmailService.SentEmails.FirstOrDefault();
            if (sentEmail != null)
            {
                Console.WriteLine($"✅ Email sent to: {sentEmail.To} with subject: {sentEmail.Subject}");
            }
            
            var lastLogEntry = mockLogger.LogEntries.LastOrDefault();
            if (lastLogEntry != null)
            {
                Console.WriteLine($"✅ Last log entry: {lastLogEntry.Message}");
            }
            
            Console.WriteLine("\n✅ Testing is easy with dependency injection and mocks!");
        }
        
        public static void DemonstrateCompositionRoot()
        {
            Console.WriteLine("\n=== Demonstrating Composition Root Pattern ===\n");
            
            // Configure all dependencies in one place
            var container = ApplicationCompositionRoot.ConfigureServices();
            
            // Create services using the container
            var userRepository = container.GetService<IUserRepository>();
            var notificationService = container.GetService<INotificationService>();
            var logger = container.GetService<ILogger>();
            
            var userService = new UserService(userRepository, notificationService, logger);
            
            // Use the service
            userService.RegisterUser("Composed User", "composed@email.com", "+1555555555");
            
            Console.WriteLine("✅ All dependencies configured in composition root!");
            Console.WriteLine("✅ Services are loosely coupled and easily configurable!");
        }
        
        public static void DemonstrateConfigurationChanges()
        {
            Console.WriteLine("\n=== Demonstrating Easy Configuration Changes ===\n");
            
            Console.WriteLine("📝 To switch from SQL to MongoDB:");
            Console.WriteLine("  ✅ Change one line in composition root");
            Console.WriteLine("  ✅ No changes needed in UserService");
            Console.WriteLine("  ✅ No recompilation of business logic");
            
            Console.WriteLine("\n📧 To switch from SMTP to SendGrid:");
            Console.WriteLine("  ✅ Change one line in composition root");
            Console.WriteLine("  ✅ No changes needed in UserService");
            Console.WriteLine("  ✅ No changes needed in NotificationService");
            
            Console.WriteLine("\n📊 To add caching:");
            Console.WriteLine("  ✅ Create CachedUserRepository implementing IUserRepository");
            Console.WriteLine("  ✅ Inject original repository into cached version");
            Console.WriteLine("  ✅ Change one line in composition root");
            Console.WriteLine("  ✅ Zero changes to existing business logic");
            
            Console.WriteLine("\n🔍 To add logging to database:");
            Console.WriteLine("  ✅ Create DatabaseLogger implementing ILogger");
            Console.WriteLine("  ✅ Change one line in composition root");
            Console.WriteLine("  ✅ All services automatically use new logger");
        }
        
        public static void DemonstrateAllBenefits()
        {
            Console.WriteLine("=== GOOD Example: Following Dependency Inversion Principle ===\n");
            
            DemonstrateFlexibility();
            DemonstrateTestability();
            DemonstrateCompositionRoot();
            DemonstrateConfigurationChanges();
            
            Console.WriteLine("\n📋 Benefits of Dependency Inversion:");
            Console.WriteLine("✅ High-level modules depend on abstractions, not concrete implementations");
            Console.WriteLine("✅ Easy to swap implementations without changing business logic");
            Console.WriteLine("✅ Excellent testability with mock objects");
            Console.WriteLine("✅ Loose coupling between components");
            Console.WriteLine("✅ Configuration centralized in composition root");
            Console.WriteLine("✅ Follows Open/Closed Principle - open for extension, closed for modification");
            Console.WriteLine("✅ Easy to add cross-cutting concerns (caching, logging, monitoring)");
            Console.WriteLine("✅ Better separation of concerns");
            Console.WriteLine("✅ More maintainable and flexible architecture");
        }
    }
}

// Dependency Inversion Principle - BAD Example
// This code violates DIP by having high-level modules directly depend on low-level modules

using System;
using System.Collections.Generic;
using System.IO;

namespace SolidPrinciples.DependencyInversion.Bad
{
    // Low-level modules (concrete implementations)
    
    public class SqlDatabase
    {
        private readonly string _connectionString;
        
        public SqlDatabase(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public void SaveUser(string userData)
        {
            Console.WriteLine($"💾 Saving user to SQL database: {userData}");
            Console.WriteLine($"Connection: {_connectionString}");
        }
        
        public string GetUser(int id)
        {
            Console.WriteLine($"🔍 Retrieving user {id} from SQL database");
            return $"User data from SQL DB for ID {id}";
        }
        
        public void DeleteUser(int id)
        {
            Console.WriteLine($"🗑️ Deleting user {id} from SQL database");
        }
    }
    
    public class EmailService
    {
        private readonly string _smtpServer;
        private readonly int _port;
        
        public EmailService(string smtpServer, int port)
        {
            _smtpServer = smtpServer;
            _port = port;
        }
        
        public void SendEmail(string to, string subject, string body)
        {
            Console.WriteLine($"📧 Sending email via {_smtpServer}:{_port}");
            Console.WriteLine($"To: {to}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Body: {body}");
        }
    }
    
    public class FileLogger
    {
        private readonly string _logFilePath;
        
        public FileLogger(string logFilePath)
        {
            _logFilePath = logFilePath;
        }
        
        public void Log(string message)
        {
            Console.WriteLine($"📝 Writing to log file {_logFilePath}: {message}");
            // In real implementation, would write to file
            File.AppendAllText(_logFilePath, $"{DateTime.Now}: {message}\n");
        }
        
        public void LogError(string error)
        {
            Console.WriteLine($"❌ Writing error to log file {_logFilePath}: {error}");
        }
    }
    
    public class SmsService
    {
        private readonly string _apiKey;
        private readonly string _apiUrl;
        
        public SmsService(string apiKey, string apiUrl)
        {
            _apiKey = apiKey;
            _apiUrl = apiUrl;
        }
        
        public void SendSms(string phoneNumber, string message)
        {
            Console.WriteLine($"📱 Sending SMS via {_apiUrl}");
            Console.WriteLine($"To: {phoneNumber}");
            Console.WriteLine($"Message: {message}");
        }
    }
    
    // High-level module that directly depends on low-level modules (VIOLATES DIP)
    public class UserService
    {
        private readonly SqlDatabase _database;      // Direct dependency on concrete class!
        private readonly EmailService _emailService; // Direct dependency on concrete class!
        private readonly FileLogger _logger;         // Direct dependency on concrete class!
        private readonly SmsService _smsService;     // Direct dependency on concrete class!
        
        // Constructor creates dependencies directly (tight coupling)
        public UserService()
        {
            _database = new SqlDatabase("Server=localhost;Database=Users;"); // Hard-coded!
            _emailService = new EmailService("smtp.gmail.com", 587);         // Hard-coded!
            _logger = new FileLogger("C:\\logs\\application.log");           // Hard-coded!
            _smsService = new SmsService("api_key_123", "https://sms-api.com"); // Hard-coded!
        }
        
        public void RegisterUser(string name, string email, string phone)
        {
            try
            {
                _logger.Log($"Starting user registration for {email}");
                
                // Create user data
                var userData = $"Name: {name}, Email: {email}, Phone: {phone}";
                
                // Save to database
                _database.SaveUser(userData);
                _logger.Log($"User {email} saved to database");
                
                // Send welcome email
                _emailService.SendEmail(email, "Welcome!", $"Welcome {name}! Your account has been created.");
                _logger.Log($"Welcome email sent to {email}");
                
                // Send SMS notification
                _smsService.SendSms(phone, $"Hi {name}! Your account is ready.");
                _logger.Log($"SMS notification sent to {phone}");
                
                _logger.Log($"User registration completed for {email}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error during user registration: {ex.Message}");
                throw;
            }
        }
        
        public string GetUser(int id)
        {
            try
            {
                _logger.Log($"Retrieving user {id}");
                var userData = _database.GetUser(id);
                _logger.Log($"User {id} retrieved successfully");
                return userData;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving user {id}: {ex.Message}");
                throw;
            }
        }
        
        public void DeleteUser(int id)
        {
            try
            {
                _logger.Log($"Deleting user {id}");
                _database.DeleteUser(id);
                _logger.Log($"User {id} deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting user {id}: {ex.Message}");
                throw;
            }
        }
    }
    
    // Another high-level module with similar problems
    public class OrderService
    {
        private readonly SqlDatabase _database;      // Same concrete dependency!
        private readonly EmailService _emailService; // Same concrete dependency!
        private readonly FileLogger _logger;         // Same concrete dependency!
        
        public OrderService()
        {
            // Duplicating the same dependencies - code duplication!
            _database = new SqlDatabase("Server=localhost;Database=Orders;");
            _emailService = new EmailService("smtp.gmail.com", 587);
            _logger = new FileLogger("C:\\logs\\orders.log");
        }
        
        public void ProcessOrder(string customerId, string productId, decimal amount)
        {
            try
            {
                _logger.Log($"Processing order for customer {customerId}");
                
                var orderData = $"Customer: {customerId}, Product: {productId}, Amount: {amount}";
                _database.SaveUser(orderData); // Reusing user method for orders - not ideal!
                
                _emailService.SendEmail($"customer{customerId}@email.com", 
                    "Order Confirmation", 
                    $"Your order for ${amount} has been processed.");
                
                _logger.Log($"Order processed for customer {customerId}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing order: {ex.Message}");
                throw;
            }
        }
    }
    
    // Payment service with even more concrete dependencies
    public class PaymentService
    {
        private readonly SqlDatabase _database;
        private readonly EmailService _emailService;
        private readonly SmsService _smsService;
        private readonly FileLogger _logger;
        
        // Even more tight coupling!
        public PaymentService()
        {
            _database = new SqlDatabase("Server=localhost;Database=Payments;");
            _emailService = new EmailService("smtp.company.com", 25); // Different SMTP settings!
            _smsService = new SmsService("payment_api_key", "https://payment-sms.com");
            _logger = new FileLogger("C:\\logs\\payments.log");
        }
        
        public void ProcessPayment(string customerId, decimal amount)
        {
            try
            {
                _logger.Log($"Processing payment of ${amount} for customer {customerId}");
                
                var paymentData = $"Customer: {customerId}, Amount: {amount}, Date: {DateTime.Now}";
                _database.SaveUser(paymentData);
                
                // Send confirmation email
                _emailService.SendEmail($"customer{customerId}@email.com",
                    "Payment Confirmation",
                    $"Your payment of ${amount} has been processed successfully.");
                
                // Send SMS confirmation
                _smsService.SendSms($"+1234567890", $"Payment of ${amount} confirmed.");
                
                _logger.Log($"Payment of ${amount} processed successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Payment processing failed: {ex.Message}");
                throw;
            }
        }
    }
    
    // Usage examples showing the problems
    public class DIPViolationExamples
    {
        public static void DemonstrateProblems()
        {
            Console.WriteLine("=== BAD Example: Violating Dependency Inversion Principle ===\n");
            
            try
            {
                // Create services - each creates its own dependencies internally
                var userService = new UserService();
                var orderService = new OrderService();
                var paymentService = new PaymentService();
                
                Console.WriteLine("🔧 Services created with hard-coded dependencies\n");
                
                // Use the services
                userService.RegisterUser("John Doe", "john@email.com", "+1234567890");
                Console.WriteLine();
                
                orderService.ProcessOrder("CUST001", "PROD123", 99.99m);
                Console.WriteLine();
                
                paymentService.ProcessPayment("CUST001", 99.99m);
                
                Console.WriteLine("\n❌ Problems with this approach:");
                Console.WriteLine("1. UserService is tightly coupled to SqlDatabase, EmailService, FileLogger, and SmsService");
                Console.WriteLine("2. Cannot easily switch to different database (e.g., MongoDB)");
                Console.WriteLine("3. Cannot easily switch to different email provider");
                Console.WriteLine("4. Cannot easily switch to different logging mechanism");
                Console.WriteLine("5. Hard to unit test - cannot mock dependencies");
                Console.WriteLine("6. Code duplication across services");
                Console.WriteLine("7. Configuration is hard-coded");
                Console.WriteLine("8. Violates Single Responsibility Principle");
                Console.WriteLine("9. Changes to low-level modules force changes to high-level modules");
                Console.WriteLine("10. Cannot reuse services with different configurations");
                
                Console.WriteLine("\n💥 Testing Problems:");
                Console.WriteLine("- Cannot test UserService without actual database");
                Console.WriteLine("- Cannot test without sending real emails");
                Console.WriteLine("- Cannot test without writing to actual log files");
                Console.WriteLine("- Cannot test without making real SMS API calls");
                
                Console.WriteLine("\n🔧 Deployment Problems:");
                Console.WriteLine("- Database connection string is hard-coded");
                Console.WriteLine("- SMTP settings are hard-coded");
                Console.WriteLine("- Log file paths are hard-coded");
                Console.WriteLine("- API keys are hard-coded in source code");
                
                Console.WriteLine("\n🔄 Maintenance Problems:");
                Console.WriteLine("- To change database, must modify all service classes");
                Console.WriteLine("- To change email provider, must modify all service classes");
                Console.WriteLine("- To add caching, must modify all service classes");
                Console.WriteLine("- Cannot easily add cross-cutting concerns (logging, monitoring)");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
                Console.WriteLine("This might happen if log directory doesn't exist or permissions are insufficient");
            }
        }
        
        public static void DemonstrateTestingDifficulties()
        {
            Console.WriteLine("\n=== Testing Difficulties ===");
            
            // This is how difficult testing becomes with tight coupling
            Console.WriteLine("❌ To test UserService.RegisterUser(), we would need:");
            Console.WriteLine("  - A real SQL Server database running");
            Console.WriteLine("  - A real SMTP server for sending emails");
            Console.WriteLine("  - Write permissions to create log files");
            Console.WriteLine("  - A real SMS API with valid credentials");
            Console.WriteLine("  - Internet connection for external services");
            
            Console.WriteLine("\n❌ Test isolation problems:");
            Console.WriteLine("  - Tests would send real emails to users");
            Console.WriteLine("  - Tests would send real SMS messages");
            Console.WriteLine("  - Tests would create real database records");
            Console.WriteLine("  - Tests would be slow due to external dependencies");
            Console.WriteLine("  - Tests would fail if external services are down");
            
            Console.WriteLine("\n❌ Configuration problems:");
            Console.WriteLine("  - Cannot test with different database configurations");
            Console.WriteLine("  - Cannot test with different email settings");
            Console.WriteLine("  - Cannot test error scenarios easily");
            Console.WriteLine("  - Cannot test with mock data");
        }
        
        public static void DemonstrateChangeImpact()
        {
            Console.WriteLine("\n=== Change Impact Analysis ===");
            
            Console.WriteLine("📝 If we want to change from SQL Server to MongoDB:");
            Console.WriteLine("  ❌ Must modify UserService class");
            Console.WriteLine("  ❌ Must modify OrderService class");
            Console.WriteLine("  ❌ Must modify PaymentService class");
            Console.WriteLine("  ❌ Must recompile all service classes");
            Console.WriteLine("  ❌ Risk of introducing bugs in working code");
            
            Console.WriteLine("\n📧 If we want to change email providers:");
            Console.WriteLine("  ❌ Must modify all classes that send emails");
            Console.WriteLine("  ❌ Must update SMTP settings in multiple places");
            Console.WriteLine("  ❌ Must test all email functionality again");
            
            Console.WriteLine("\n📊 If we want to add logging to a database instead of files:");
            Console.WriteLine("  ❌ Must modify every class that logs");
            Console.WriteLine("  ❌ Must add database logging logic to each class");
            Console.WriteLine("  ❌ Violates Open/Closed Principle");
            
            Console.WriteLine("\n🔄 If we want to add caching:");
            Console.WriteLine("  ❌ Must modify every class that accesses data");
            Console.WriteLine("  ❌ Must add caching logic to each class");
            Console.WriteLine("  ❌ Increases complexity of each class");
        }
        
        public static void DemonstrateAllProblems()
        {
            DemonstrateProblems();
            DemonstrateTestingDifficulties();
            DemonstrateChangeImpact();
            
            Console.WriteLine("\n📋 Summary of DIP Violations:");
            Console.WriteLine("❌ High-level modules depend on low-level modules");
            Console.WriteLine("❌ Concrete dependencies instead of abstractions");
            Console.WriteLine("❌ Hard-coded configurations");
            Console.WriteLine("❌ Tight coupling between layers");
            Console.WriteLine("❌ Difficult to test in isolation");
            Console.WriteLine("❌ Code duplication across services");
            Console.WriteLine("❌ Violates Open/Closed Principle");
            Console.WriteLine("❌ Changes cascade through multiple layers");
        }
    }
}

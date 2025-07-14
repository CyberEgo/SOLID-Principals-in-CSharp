// Single Responsibility Principle - GOOD Example
// Each class has a single, well-defined responsibility

using System;

namespace SolidPrinciples.SingleResponsibility.Good
{
    /// <summary>
    /// User class with single responsibility: managing user data
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateCreated { get; set; }
        
        public User() { }
        
        public User(string name, string email)
        {
            Name = name;
            Email = email;
            DateCreated = DateTime.Now;
        }
        
        // Only responsibility: managing user data
        public void UpdateProfile(string name, string email)
        {
            Name = name;
            Email = email;
        }
        
        public override string ToString()
        {
            return $"User: {Name} ({Email})";
        }
    }
    
    /// <summary>
    /// EmailService with single responsibility: sending emails
    /// </summary>
    public class EmailService
    {
        public void SendWelcomeEmail(User user)
        {
            Console.WriteLine($"📧 Sending welcome email to {user.Email}");
            Console.WriteLine("Subject: Welcome to our platform!");
            Console.WriteLine($"Dear {user.Name}, thank you for joining us!");
        }
        
        public void SendPasswordResetEmail(User user, string resetToken)
        {
            Console.WriteLine($"📧 Sending password reset email to {user.Email}");
            Console.WriteLine("Subject: Password Reset Request");
            Console.WriteLine($"Reset token: {resetToken}");
        }
        
        public void SendNotificationEmail(User user, string subject, string message)
        {
            Console.WriteLine($"📧 Sending notification to {user.Email}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
        }
    }
    
    /// <summary>
    /// UserRepository with single responsibility: database operations for users
    /// </summary>
    public class UserRepository
    {
        public void Save(User user)
        {
            Console.WriteLine($"💾 Saving user {user.Name} to database");
            Console.WriteLine($"SQL: INSERT INTO Users (Name, Email, DateCreated) VALUES ('{user.Name}', '{user.Email}', '{user.DateCreated}')");
        }
        
        public User GetById(int id)
        {
            Console.WriteLine($"💾 Retrieving user with ID {id} from database");
            return new User { Id = id, Name = "Retrieved User", Email = "user@example.com", DateCreated = DateTime.Now.AddDays(-10) };
        }
        
        public void Update(User user)
        {
            Console.WriteLine($"💾 Updating user {user.Name} in database");
            Console.WriteLine($"SQL: UPDATE Users SET Name='{user.Name}', Email='{user.Email}' WHERE Id={user.Id}");
        }
        
        public void Delete(int userId)
        {
            Console.WriteLine($"💾 Deleting user with ID {userId} from database");
            Console.WriteLine($"SQL: DELETE FROM Users WHERE Id={userId}");
        }
    }
    
    /// <summary>
    /// UserValidator with single responsibility: validating user data
    /// </summary>
    public class UserValidator
    {
        public bool IsValidEmail(string email)
        {
            return !string.IsNullOrEmpty(email) && 
                   email.Contains("@") && 
                   email.Contains(".") && 
                   email.IndexOf("@") < email.LastIndexOf(".");
        }
        
        public bool IsValidName(string name)
        {
            return !string.IsNullOrEmpty(name) && 
                   name.Length >= 2 && 
                   name.Length <= 50;
        }
        
        public bool IsValidUser(User user)
        {
            return IsValidName(user.Name) && IsValidEmail(user.Email);
        }
        
        public ValidationResult ValidateUser(User user)
        {
            var result = new ValidationResult();
            
            if (!IsValidName(user.Name))
                result.AddError("Name must be between 2 and 50 characters");
                
            if (!IsValidEmail(user.Email))
                result.AddError("Email format is invalid");
                
            return result;
        }
    }
    
    /// <summary>
    /// UserFormatter with single responsibility: formatting user data for display
    /// </summary>
    public class UserFormatter
    {
        public string FormatForDisplay(User user)
        {
            return $"👤 {user.Name} ({user.Email}) - Member since: {user.DateCreated:yyyy-MM-dd}";
        }
        
        public string FormatForExport(User user)
        {
            return $"{user.Id},{user.Name},{user.Email},{user.DateCreated:yyyy-MM-dd HH:mm:ss}";
        }
        
        public string FormatForEmail(User user)
        {
            return $"Dear {user.Name}";
        }
    }
    
    /// <summary>
    /// BusinessRules with single responsibility: user-related business logic
    /// </summary>
    public class UserBusinessRules
    {
        public bool CanSendWelcomeEmail(User user)
        {
            // Business rule: Can send welcome email if user was created less than 7 days ago
            return (DateTime.Now - user.DateCreated).TotalDays <= 7;
        }
        
        public bool CanResetPassword(User user)
        {
            // Business rule: Can reset password if user exists and email is valid
            return user != null && !string.IsNullOrEmpty(user.Email);
        }
        
        public bool IsActiveUser(User user)
        {
            // Business rule: User is active if created more than 1 day ago
            return (DateTime.Now - user.DateCreated).TotalDays >= 1;
        }
    }
    
    /// <summary>
    /// UserService coordinates different services - this is where the business workflow happens
    /// </summary>
    public class UserService
    {
        private readonly EmailService _emailService;
        private readonly UserRepository _userRepository;
        private readonly UserValidator _userValidator;
        private readonly UserBusinessRules _businessRules;
        
        public UserService(EmailService emailService, UserRepository userRepository, 
                          UserValidator userValidator, UserBusinessRules businessRules)
        {
            _emailService = emailService;
            _userRepository = userRepository;
            _userValidator = userValidator;
            _businessRules = businessRules;
        }
        
        public RegistrationResult RegisterUser(string name, string email)
        {
            var user = new User(name, email);
            var validationResult = _userValidator.ValidateUser(user);
            
            if (!validationResult.IsValid)
            {
                return new RegistrationResult { Success = false, Errors = validationResult.Errors };
            }
            
            _userRepository.Save(user);
            
            if (_businessRules.CanSendWelcomeEmail(user))
            {
                _emailService.SendWelcomeEmail(user);
            }
            
            return new RegistrationResult { Success = true, User = user };
        }
        
        public void UpdateUserProfile(int userId, string newName, string newEmail)
        {
            var user = _userRepository.GetById(userId);
            if (user != null)
            {
                user.UpdateProfile(newName, newEmail);
                
                if (_userValidator.IsValidUser(user))
                {
                    _userRepository.Update(user);
                }
                else
                {
                    throw new ArgumentException("Invalid user data");
                }
            }
        }
    }
    
    // Supporting classes
    public class ValidationResult
    {
        public bool IsValid => Errors.Count == 0;
        public List<string> Errors { get; } = new List<string>();
        
        public void AddError(string error)
        {
            Errors.Add(error);
        }
    }
    
    public class RegistrationResult
    {
        public bool Success { get; set; }
        public User User { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
    
    // Usage example demonstrating the benefits
    public class UserManagementExample
    {
        public static void DemonstrateBenefits()
        {
            Console.WriteLine("=== GOOD Example: Each class has a single responsibility ===\n");
            
            // Each service has a single responsibility
            var emailService = new EmailService();
            var userRepository = new UserRepository();
            var userValidator = new UserValidator();
            var businessRules = new UserBusinessRules();
            var userFormatter = new UserFormatter();
            
            // UserService coordinates the workflow
            var userService = new UserService(emailService, userRepository, userValidator, businessRules);
            
            // Register a new user
            var result = userService.RegisterUser("Jane Doe", "jane@example.com");
            
            if (result.Success)
            {
                Console.WriteLine("✅ User registered successfully!");
                Console.WriteLine(userFormatter.FormatForDisplay(result.User));
            }
            else
            {
                Console.WriteLine("❌ Registration failed:");
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"  - {error}");
                }
            }
            
            Console.WriteLine("\nBenefits of this approach:");
            Console.WriteLine("✅ Each class has a single, clear responsibility");
            Console.WriteLine("✅ Easy to test each component independently");
            Console.WriteLine("✅ Changes to one responsibility don't affect others");
            Console.WriteLine("✅ Code is more maintainable and readable");
            Console.WriteLine("✅ Classes can be reused in different contexts");
            
            // Demonstrate individual testing capability
            Console.WriteLine("\n=== Individual component testing ===");
            TestUserValidator(userValidator);
            TestUserFormatter(userFormatter);
        }
        
        private static void TestUserValidator(UserValidator validator)
        {
            Console.WriteLine("\n🧪 Testing UserValidator:");
            Console.WriteLine($"Valid email 'test@example.com': {validator.IsValidEmail("test@example.com")}");
            Console.WriteLine($"Invalid email 'invalid-email': {validator.IsValidEmail("invalid-email")}");
            Console.WriteLine($"Valid name 'John': {validator.IsValidName("John")}");
            Console.WriteLine($"Invalid name 'J': {validator.IsValidName("J")}");
        }
        
        private static void TestUserFormatter(UserFormatter formatter)
        {
            Console.WriteLine("\n🧪 Testing UserFormatter:");
            var testUser = new User("Test User", "test@example.com");
            Console.WriteLine($"Display format: {formatter.FormatForDisplay(testUser)}");
            Console.WriteLine($"Export format: {formatter.FormatForExport(testUser)}");
        }
    }
}

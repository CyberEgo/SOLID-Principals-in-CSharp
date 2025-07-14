// Single Responsibility Principle - BAD Example
// This class violates SRP by having multiple responsibilities

using System;

namespace SolidPrinciples.SingleResponsibility.Bad
{
    /// <summary>
    /// This User class violates SRP by handling multiple concerns:
    /// - User data management
    /// - Email sending
    /// - Database operations
    /// - Data validation
    /// </summary>
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateCreated { get; set; }
        
        // Responsibility 1: User data management
        public void UpdateProfile(string name, string email)
        {
            Name = name;
            Email = email;
        }
        
        // Responsibility 2: Email sending
        public void SendWelcomeEmail()
        {
            // Email sending logic - should not be in User class
            Console.WriteLine($"Sending welcome email to {Email}");
            Console.WriteLine("Subject: Welcome to our platform!");
            Console.WriteLine("Body: Thank you for joining us...");
        }
        
        public void SendPasswordResetEmail(string resetToken)
        {
            Console.WriteLine($"Sending password reset email to {Email}");
            Console.WriteLine($"Reset token: {resetToken}");
        }
        
        // Responsibility 3: Database operations
        public void SaveToDatabase()
        {
            // Database saving logic - should not be in User class
            Console.WriteLine($"Saving user {Name} to database");
            Console.WriteLine($"INSERT INTO Users (Name, Email, DateCreated) VALUES ('{Name}', '{Email}', '{DateCreated}')");
        }
        
        public void DeleteFromDatabase()
        {
            Console.WriteLine($"Deleting user {Name} from database");
            Console.WriteLine($"DELETE FROM Users WHERE Email = '{Email}'");
        }
        
        // Responsibility 4: Data validation
        public bool ValidateEmail()
        {
            // Email validation logic - should not be in User class
            return !string.IsNullOrEmpty(Email) && Email.Contains("@") && Email.Contains(".");
        }
        
        public bool ValidateName()
        {
            return !string.IsNullOrEmpty(Name) && Name.Length >= 2;
        }
        
        // Responsibility 5: Business rules
        public bool CanSendEmail()
        {
            return ValidateEmail() && (DateTime.Now - DateCreated).TotalDays >= 1;
        }
        
        // Responsibility 6: Formatting/Display
        public string FormatForDisplay()
        {
            return $"User: {Name} ({Email}) - Created: {DateCreated:yyyy-MM-dd}";
        }
    }
    
    // Usage example showing the problems
    public class UserManagementExample
    {
        public static void DemonstrateProblems()
        {
            Console.WriteLine("=== BAD Example: User class with multiple responsibilities ===\n");
            
            var user = new User
            {
                Name = "John Doe",
                Email = "john@example.com",
                DateCreated = DateTime.Now.AddDays(-5)
            };
            
            // All these different concerns are mixed in one class
            user.UpdateProfile("John Smith", "johnsmith@example.com");
            user.SaveToDatabase();
            user.SendWelcomeEmail();
            
            Console.WriteLine($"Email valid: {user.ValidateEmail()}");
            Console.WriteLine($"Can send email: {user.CanSendEmail()}");
            Console.WriteLine(user.FormatForDisplay());
            
            Console.WriteLine("\nProblems with this approach:");
            Console.WriteLine("1. User class has too many reasons to change");
            Console.WriteLine("2. Hard to test individual responsibilities");
            Console.WriteLine("3. High coupling between different concerns");
            Console.WriteLine("4. Violates single responsibility principle");
        }
    }
}

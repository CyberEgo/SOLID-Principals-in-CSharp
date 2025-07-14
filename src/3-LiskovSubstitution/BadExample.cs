// Liskov Substitution Principle - BAD Example
// This code violates LSP by having derived classes that don't properly substitute their base class

using System;
using System.Collections.Generic;

namespace SolidPrinciples.LiskovSubstitution.Bad
{
    // Classic LSP violation: Rectangle/Square problem
    public class Rectangle
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }
        
        public Rectangle() { }
        
        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }
        
        public int CalculateArea()
        {
            return Width * Height;
        }
        
        public override string ToString()
        {
            return $"Rectangle: {Width} x {Height} (Area: {CalculateArea()})";
        }
    }
    
    // This Square class violates LSP
    public class Square : Rectangle
    {
        public override int Width
        {
            get => base.Width;
            set
            {
                base.Width = value;
                base.Height = value; // This changes the expected behavior!
            }
        }
        
        public override int Height
        {
            get => base.Height;
            set
            {
                base.Width = value;  // This changes the expected behavior!
                base.Height = value;
            }
        }
        
        public Square(int side) : base(side, side) { }
        
        public override string ToString()
        {
            return $"Square: {Width} x {Height} (Area: {CalculateArea()})";
        }
    }
    
    // Bird hierarchy that violates LSP
    public abstract class Bird
    {
        public string Name { get; set; }
        
        public Bird(string name)
        {
            Name = name;
        }
        
        public virtual void Eat()
        {
            Console.WriteLine($"{Name} is eating...");
        }
        
        public virtual void Fly()
        {
            Console.WriteLine($"{Name} is flying...");
        }
        
        public virtual void MakeSound()
        {
            Console.WriteLine($"{Name} is making a sound...");
        }
    }
    
    public class Duck : Bird
    {
        public Duck() : base("Duck") { }
        
        public override void Fly()
        {
            Console.WriteLine($"{Name} is flying gracefully...");
        }
        
        public override void MakeSound()
        {
            Console.WriteLine($"{Name} says: Quack!");
        }
    }
    
    public class Eagle : Bird
    {
        public Eagle() : base("Eagle") { }
        
        public override void Fly()
        {
            Console.WriteLine($"{Name} is soaring high...");
        }
        
        public override void MakeSound()
        {
            Console.WriteLine($"{Name} says: Screech!");
        }
    }
    
    // This violates LSP - Penguin can't fly!
    public class Penguin : Bird
    {
        public Penguin() : base("Penguin") { }
        
        public override void Fly()
        {
            throw new NotSupportedException("Penguins cannot fly!"); // LSP violation!
        }
        
        public override void MakeSound()
        {
            Console.WriteLine($"{Name} says: Honk!");
        }
    }
    
    // This violates LSP - Ostrich can't fly!
    public class Ostrich : Bird
    {
        public Ostrich() : base("Ostrich") { }
        
        public override void Fly()
        {
            throw new InvalidOperationException("Ostriches cannot fly!"); // LSP violation!
        }
        
        public override void MakeSound()
        {
            Console.WriteLine($"{Name} says: Boom!");
        }
    }
    
    // File processing hierarchy that violates LSP
    public abstract class FileProcessor
    {
        public virtual void ProcessFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("File path cannot be empty");
                
            DoProcessFile(filePath);
        }
        
        protected abstract void DoProcessFile(string filePath);
    }
    
    public class TextFileProcessor : FileProcessor
    {
        protected override void DoProcessFile(string filePath)
        {
            Console.WriteLine($"Processing text file: {filePath}");
        }
    }
    
    // This violates LSP by strengthening preconditions
    public class SecureFileProcessor : FileProcessor
    {
        public override void ProcessFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("File path cannot be empty");
                
            // LSP violation: strengthens preconditions
            if (!filePath.EndsWith(".secure"))
                throw new ArgumentException("Only .secure files are allowed!");
                
            if (filePath.Length > 100)
                throw new ArgumentException("File path too long for secure processing!");
                
            DoProcessFile(filePath);
        }
        
        protected override void DoProcessFile(string filePath)
        {
            Console.WriteLine($"Processing secure file: {filePath}");
        }
    }
    
    // Account hierarchy that violates LSP
    public class Account
    {
        public decimal Balance { get; protected set; }
        public string AccountNumber { get; set; }
        
        public Account(string accountNumber, decimal initialBalance = 0)
        {
            AccountNumber = accountNumber;
            Balance = initialBalance;
        }
        
        public virtual void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive");
                
            if (amount > Balance)
                throw new InvalidOperationException("Insufficient funds");
                
            Balance -= amount;
            Console.WriteLine($"Withdrew ${amount}. New balance: ${Balance}");
        }
        
        public virtual void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive");
                
            Balance += amount;
            Console.WriteLine($"Deposited ${amount}. New balance: ${Balance}");
        }
    }
    
    // This violates LSP by changing withdrawal behavior
    public class ReadOnlyAccount : Account
    {
        public ReadOnlyAccount(string accountNumber, decimal initialBalance) 
            : base(accountNumber, initialBalance) { }
        
        public override void Withdraw(decimal amount)
        {
            throw new NotSupportedException("Cannot withdraw from read-only account!"); // LSP violation!
        }
        
        public override void Deposit(decimal amount)
        {
            throw new NotSupportedException("Cannot deposit to read-only account!"); // LSP violation!
        }
    }
    
    // Usage examples showing the problems
    public class LSPViolationExamples
    {
        public static void DemonstrateRectangleSquareProblem()
        {
            Console.WriteLine("=== Rectangle/Square LSP Violation ===");
            
            // This works fine with Rectangle
            Rectangle rect = new Rectangle(5, 4);
            Console.WriteLine($"Original: {rect}");
            
            rect.Width = 6;
            Console.WriteLine($"After setting width to 6: {rect}");
            Console.WriteLine($"Expected: Rectangle: 6 x 4 (Area: 24)");
            
            Console.WriteLine();
            
            // This breaks with Square substituted for Rectangle
            Rectangle square = new Square(5); // Square is substituted for Rectangle
            Console.WriteLine($"Original: {square}");
            
            square.Width = 6; // This unexpectedly changes both width AND height!
            Console.WriteLine($"After setting width to 6: {square}");
            Console.WriteLine($"Expected: Rectangle: 6 x 5 (Area: 30)");
            Console.WriteLine($"Actual: Square: 6 x 6 (Area: 36) - UNEXPECTED!");
            
            Console.WriteLine("\n❌ Problem: Square changes the behavior expected from Rectangle");
        }
        
        public static void DemonstrateBirdProblem()
        {
            Console.WriteLine("\n=== Bird Hierarchy LSP Violation ===");
            
            var birds = new List<Bird>
            {
                new Duck(),
                new Eagle(),
                new Penguin(), // This will cause problems!
                new Ostrich()  // This will also cause problems!
            };
            
            Console.WriteLine("Trying to make all birds fly:");
            
            foreach (var bird in birds)
            {
                try
                {
                    bird.Fly(); // This will throw exceptions for Penguin and Ostrich!
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error with {bird.Name}: {ex.Message}");
                }
            }
            
            Console.WriteLine("\n❌ Problem: Not all birds can fly, violating LSP");
        }
        
        public static void DemonstrateFileProcessorProblem()
        {
            Console.WriteLine("\n=== File Processor LSP Violation ===");
            
            var processors = new List<FileProcessor>
            {
                new TextFileProcessor(),
                new SecureFileProcessor() // This has stricter requirements!
            };
            
            var testFiles = new[] { "document.txt", "data.xml", "secret.secure" };
            
            foreach (var processor in processors)
            {
                Console.WriteLine($"\nTesting {processor.GetType().Name}:");
                
                foreach (var file in testFiles)
                {
                    try
                    {
                        processor.ProcessFile(file);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"❌ Error processing {file}: {ex.Message}");
                    }
                }
            }
            
            Console.WriteLine("\n❌ Problem: SecureFileProcessor strengthens preconditions");
        }
        
        public static void DemonstrateAccountProblem()
        {
            Console.WriteLine("\n=== Account Hierarchy LSP Violation ===");
            
            var accounts = new List<Account>
            {
                new Account("ACC001", 1000),
                new ReadOnlyAccount("RO001", 500) // This will cause problems!
            };
            
            Console.WriteLine("Trying to withdraw from all accounts:");
            
            foreach (var account in accounts)
            {
                try
                {
                    Console.WriteLine($"\nAccount {account.AccountNumber} - Balance: ${account.Balance}");
                    account.Withdraw(100);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error: {ex.Message}");
                }
            }
            
            Console.WriteLine("\n❌ Problem: ReadOnlyAccount cannot be substituted for Account");
        }
        
        public static void DemonstrateAllProblems()
        {
            Console.WriteLine("=== BAD Example: Violating Liskov Substitution Principle ===\n");
            
            DemonstrateRectangleSquareProblem();
            DemonstrateBirdProblem();
            DemonstrateFileProcessorProblem();
            DemonstrateAccountProblem();
            
            Console.WriteLine("\n📋 Summary of LSP Violations:");
            Console.WriteLine("❌ Square changes Rectangle's expected behavior");
            Console.WriteLine("❌ Penguin and Ostrich throw exceptions when asked to fly");
            Console.WriteLine("❌ SecureFileProcessor adds stricter requirements");
            Console.WriteLine("❌ ReadOnlyAccount throws exceptions for basic operations");
            Console.WriteLine("❌ Client code cannot safely substitute derived classes");
            Console.WriteLine("❌ Polymorphism breaks down due to unexpected behavior");
        }
    }
}

// Liskov Substitution Principle - GOOD Example
// This code follows LSP by ensuring derived classes can properly substitute their base classes

using System;
using System.Collections.Generic;
using System.Linq;

namespace SolidPrinciples.LiskovSubstitution.Good
{
    // Solution 1: Common interface instead of inheritance for shapes
    public interface IShape
    {
        double CalculateArea();
        double CalculatePerimeter();
        string GetShapeInfo();
    }
    
    public class Rectangle : IShape
    {
        public double Width { get; }
        public double Height { get; }
        
        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
        }
        
        public double CalculateArea()
        {
            return Width * Height;
        }
        
        public double CalculatePerimeter()
        {
            return 2 * (Width + Height);
        }
        
        public string GetShapeInfo()
        {
            return $"Rectangle: {Width} x {Height}";
        }
        
        // Method to create new rectangle with different dimensions
        public Rectangle WithDimensions(double width, double height)
        {
            return new Rectangle(width, height);
        }
    }
    
    public class Square : IShape
    {
        public double Side { get; }
        
        public Square(double side)
        {
            Side = side;
        }
        
        public double CalculateArea()
        {
            return Side * Side;
        }
        
        public double CalculatePerimeter()
        {
            return 4 * Side;
        }
        
        public string GetShapeInfo()
        {
            return $"Square: {Side} x {Side}";
        }
        
        // Method to create new square with different side
        public Square WithSide(double side)
        {
            return new Square(side);
        }
    }
    
    // Solution 2: Proper bird hierarchy using interfaces
    public abstract class Bird
    {
        public string Name { get; }
        
        protected Bird(string name)
        {
            Name = name;
        }
        
        public virtual void Eat()
        {
            Console.WriteLine($"{Name} is eating...");
        }
        
        public abstract void MakeSound();
        public abstract void Move();
    }
    
    // Separate interfaces for capabilities
    public interface IFlyable
    {
        void Fly();
        int MaxFlightAltitude { get; }
    }
    
    public interface ISwimmable
    {
        void Swim();
        int MaxDivingDepth { get; }
    }
    
    public interface IRunnable
    {
        void Run();
        int MaxRunningSpeed { get; }
    }
    
    public class Duck : Bird, IFlyable, ISwimmable
    {
        public int MaxFlightAltitude => 1000; // meters
        public int MaxDivingDepth => 2; // meters
        
        public Duck() : base("Duck") { }
        
        public override void MakeSound()
        {
            Console.WriteLine($"{Name} says: Quack!");
        }
        
        public override void Move()
        {
            Console.WriteLine($"{Name} is waddling...");
        }
        
        public void Fly()
        {
            Console.WriteLine($"{Name} is flying gracefully up to {MaxFlightAltitude}m...");
        }
        
        public void Swim()
        {
            Console.WriteLine($"{Name} is swimming and can dive up to {MaxDivingDepth}m...");
        }
    }
    
    public class Eagle : Bird, IFlyable
    {
        public int MaxFlightAltitude => 6000; // meters
        
        public Eagle() : base("Eagle") { }
        
        public override void MakeSound()
        {
            Console.WriteLine($"{Name} says: Screech!");
        }
        
        public override void Move()
        {
            Console.WriteLine($"{Name} is walking on ground...");
        }
        
        public void Fly()
        {
            Console.WriteLine($"{Name} is soaring high up to {MaxFlightAltitude}m...");
        }
    }
    
    public class Penguin : Bird, ISwimmable
    {
        public int MaxDivingDepth => 200; // meters
        
        public Penguin() : base("Penguin") { }
        
        public override void MakeSound()
        {
            Console.WriteLine($"{Name} says: Honk!");
        }
        
        public override void Move()
        {
            Console.WriteLine($"{Name} is waddling...");
        }
        
        public void Swim()
        {
            Console.WriteLine($"{Name} is swimming expertly and can dive up to {MaxDivingDepth}m...");
        }
    }
    
    public class Ostrich : Bird, IRunnable
    {
        public int MaxRunningSpeed => 70; // km/h
        
        public Ostrich() : base("Ostrich") { }
        
        public override void MakeSound()
        {
            Console.WriteLine($"{Name} says: Boom!");
        }
        
        public override void Move()
        {
            Console.WriteLine($"{Name} is walking...");
        }
        
        public void Run()
        {
            Console.WriteLine($"{Name} is running at speeds up to {MaxRunningSpeed} km/h...");
        }
    }
    
    // Solution 3: Proper file processor hierarchy
    public abstract class FileProcessor
    {
        // Base class defines minimum contract
        public virtual ProcessingResult ProcessFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return ProcessingResult.Failure("File path cannot be empty");
                
            return DoProcessFile(filePath);
        }
        
        protected abstract ProcessingResult DoProcessFile(string filePath);
        
        public abstract string GetSupportedFileTypes();
    }
    
    public class TextFileProcessor : FileProcessor
    {
        protected override ProcessingResult DoProcessFile(string filePath)
        {
            Console.WriteLine($"Processing text file: {filePath}");
            return ProcessingResult.Success($"Text file {filePath} processed successfully");
        }
        
        public override string GetSupportedFileTypes()
        {
            return "Text files (.txt, .log, .md)";
        }
    }
    
    public class ImageFileProcessor : FileProcessor
    {
        protected override ProcessingResult DoProcessFile(string filePath)
        {
            Console.WriteLine($"Processing image file: {filePath}");
            return ProcessingResult.Success($"Image file {filePath} processed successfully");
        }
        
        public override string GetSupportedFileTypes()
        {
            return "Image files (.jpg, .png, .gif)";
        }
    }
    
    // This processor can weaken preconditions (accept more) but doesn't strengthen them
    public class UniversalFileProcessor : FileProcessor
    {
        public override ProcessingResult ProcessFile(string filePath)
        {
            // Can handle empty file paths by creating default
            if (string.IsNullOrEmpty(filePath))
            {
                filePath = "default.tmp";
                Console.WriteLine("Empty file path provided, using default.tmp");
            }
                
            return DoProcessFile(filePath);
        }
        
        protected override ProcessingResult DoProcessFile(string filePath)
        {
            Console.WriteLine($"Processing any file type: {filePath}");
            return ProcessingResult.Success($"File {filePath} processed with universal processor");
        }
        
        public override string GetSupportedFileTypes()
        {
            return "All file types";
        }
    }
    
    // Solution 4: Proper account hierarchy
    public abstract class Account
    {
        public decimal Balance { get; protected set; }
        public string AccountNumber { get; }
        
        protected Account(string accountNumber, decimal initialBalance = 0)
        {
            AccountNumber = accountNumber;
            Balance = initialBalance;
        }
        
        public virtual TransactionResult Deposit(decimal amount)
        {
            if (amount <= 0)
                return TransactionResult.Failure("Amount must be positive");
                
            Balance += amount;
            Console.WriteLine($"Deposited ${amount}. New balance: ${Balance}");
            return TransactionResult.Success($"Deposited ${amount}");
        }
        
        // Abstract method - each account type defines its own withdrawal rules
        public abstract TransactionResult Withdraw(decimal amount);
        
        public virtual decimal GetAvailableBalance()
        {
            return Balance;
        }
    }
    
    public class CheckingAccount : Account
    {
        private readonly decimal _overdraftLimit;
        
        public CheckingAccount(string accountNumber, decimal initialBalance = 0, decimal overdraftLimit = 0) 
            : base(accountNumber, initialBalance)
        {
            _overdraftLimit = overdraftLimit;
        }
        
        public override TransactionResult Withdraw(decimal amount)
        {
            if (amount <= 0)
                return TransactionResult.Failure("Amount must be positive");
                
            if (amount > Balance + _overdraftLimit)
                return TransactionResult.Failure("Insufficient funds including overdraft");
                
            Balance -= amount;
            Console.WriteLine($"Withdrew ${amount}. New balance: ${Balance}");
            return TransactionResult.Success($"Withdrew ${amount}");
        }
        
        public override decimal GetAvailableBalance()
        {
            return Balance + _overdraftLimit;
        }
    }
    
    public class SavingsAccount : Account
    {
        private readonly decimal _minimumBalance;
        
        public SavingsAccount(string accountNumber, decimal initialBalance = 0, decimal minimumBalance = 0) 
            : base(accountNumber, initialBalance)
        {
            _minimumBalance = minimumBalance;
        }
        
        public override TransactionResult Withdraw(decimal amount)
        {
            if (amount <= 0)
                return TransactionResult.Failure("Amount must be positive");
                
            if (Balance - amount < _minimumBalance)
                return TransactionResult.Failure($"Cannot withdraw. Minimum balance of ${_minimumBalance} required");
                
            Balance -= amount;
            Console.WriteLine($"Withdrew ${amount}. New balance: ${Balance}");
            return TransactionResult.Success($"Withdrew ${amount}");
        }
        
        public override decimal GetAvailableBalance()
        {
            return Math.Max(0, Balance - _minimumBalance);
        }
    }
    
    // For read-only accounts, use composition instead of inheritance
    public class AccountViewService
    {
        private readonly Account _account;
        
        public AccountViewService(Account account)
        {
            _account = account ?? throw new ArgumentNullException(nameof(account));
        }
        
        public decimal Balance => _account.Balance;
        public string AccountNumber => _account.AccountNumber;
        public decimal AvailableBalance => _account.GetAvailableBalance();
        
        public string GetAccountSummary()
        {
            return $"Account {AccountNumber}: Balance ${Balance}, Available ${AvailableBalance}";
        }
    }
    
    // Supporting classes
    public class ProcessingResult
    {
        public bool IsSuccess { get; }
        public string Message { get; }
        public Exception Exception { get; }
        
        private ProcessingResult(bool isSuccess, string message, Exception exception = null)
        {
            IsSuccess = isSuccess;
            Message = message;
            Exception = exception;
        }
        
        public static ProcessingResult Success(string message = "Operation completed successfully")
        {
            return new ProcessingResult(true, message);
        }
        
        public static ProcessingResult Failure(string message, Exception exception = null)
        {
            return new ProcessingResult(false, message, exception);
        }
    }
    
    public class TransactionResult
    {
        public bool IsSuccess { get; }
        public string Message { get; }
        
        private TransactionResult(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
        
        public static TransactionResult Success(string message)
        {
            return new TransactionResult(true, message);
        }
        
        public static TransactionResult Failure(string message)
        {
            return new TransactionResult(false, message);
        }
    }
    
    // Service classes that work with the hierarchies
    public class BirdWatcher
    {
        public void ObserveBird(Bird bird)
        {
            Console.WriteLine($"\n🔍 Observing {bird.Name}:");
            bird.Eat();
            bird.MakeSound();
            bird.Move();
        }
        
        public void ObserveFlyingBirds(IEnumerable<IFlyable> flyingBirds)
        {
            Console.WriteLine("\n✈️ Watching flying birds:");
            foreach (var bird in flyingBirds)
            {
                bird.Fly();
            }
        }
        
        public void ObserveSwimmingBirds(IEnumerable<ISwimmable> swimmingBirds)
        {
            Console.WriteLine("\n🏊 Watching swimming birds:");
            foreach (var bird in swimmingBirds)
            {
                bird.Swim();
            }
        }
        
        public void ObserveRunningBirds(IEnumerable<IRunnable> runningBirds)
        {
            Console.WriteLine("\n🏃 Watching running birds:");
            foreach (var bird in runningBirds)
            {
                bird.Run();
            }
        }
    }
    
    public class FileProcessingService
    {
        public void ProcessFiles(IEnumerable<FileProcessor> processors, IEnumerable<string> filePaths)
        {
            foreach (var processor in processors)
            {
                Console.WriteLine($"\n📁 Using {processor.GetType().Name} ({processor.GetSupportedFileTypes()}):");
                
                foreach (var filePath in filePaths)
                {
                    var result = processor.ProcessFile(filePath);
                    if (result.IsSuccess)
                    {
                        Console.WriteLine($"✅ {result.Message}");
                    }
                    else
                    {
                        Console.WriteLine($"❌ {result.Message}");
                    }
                }
            }
        }
    }
    
    public class BankingService
    {
        public void ProcessTransactions(IEnumerable<Account> accounts)
        {
            foreach (var account in accounts)
            {
                Console.WriteLine($"\n💳 Processing account {account.AccountNumber}:");
                Console.WriteLine($"Initial balance: ${account.Balance}");
                Console.WriteLine($"Available balance: ${account.GetAvailableBalance()}");
                
                // These operations work the same for all account types
                var depositResult = account.Deposit(100);
                Console.WriteLine($"Deposit result: {depositResult.Message}");
                
                var withdrawResult = account.Withdraw(50);
                Console.WriteLine($"Withdrawal result: {withdrawResult.Message}");
                
                Console.WriteLine($"Final balance: ${account.Balance}");
            }
        }
    }
    
    // Usage examples demonstrating LSP compliance
    public class LSPComplianceExamples
    {
        public static void DemonstrateShapePolymorphism()
        {
            Console.WriteLine("=== Shape Polymorphism (LSP Compliant) ===");
            
            var shapes = new List<IShape>
            {
                new Rectangle(5, 4),
                new Square(5)
            };
            
            foreach (var shape in shapes)
            {
                Console.WriteLine($"{shape.GetShapeInfo()} - Area: {shape.CalculateArea()}, Perimeter: {shape.CalculatePerimeter()}");
            }
            
            Console.WriteLine("✅ All shapes can be used interchangeably through IShape interface");
        }
        
        public static void DemonstrateBirdBehavior()
        {
            Console.WriteLine("\n=== Bird Behavior (LSP Compliant) ===");
            
            var birdWatcher = new BirdWatcher();
            
            var allBirds = new List<Bird>
            {
                new Duck(),
                new Eagle(),
                new Penguin(),
                new Ostrich()
            };
            
            // All birds can be observed the same way
            foreach (var bird in allBirds)
            {
                birdWatcher.ObserveBird(bird);
            }
            
            // Specific capabilities are handled separately
            var flyingBirds = allBirds.OfType<IFlyable>();
            birdWatcher.ObserveFlyingBirds(flyingBirds);
            
            var swimmingBirds = allBirds.OfType<ISwimmable>();
            birdWatcher.ObserveSwimmingBirds(swimmingBirds);
            
            var runningBirds = allBirds.OfType<IRunnable>();
            birdWatcher.ObserveRunningBirds(runningBirds);
            
            Console.WriteLine("✅ Birds with different capabilities are handled appropriately");
        }
        
        public static void DemonstrateFileProcessing()
        {
            Console.WriteLine("\n=== File Processing (LSP Compliant) ===");
            
            var processors = new List<FileProcessor>
            {
                new TextFileProcessor(),
                new ImageFileProcessor(),
                new UniversalFileProcessor()
            };
            
            var testFiles = new[] { "document.txt", "image.jpg", "", "data.xml" };
            
            var processingService = new FileProcessingService();
            processingService.ProcessFiles(processors, testFiles);
            
            Console.WriteLine("✅ All processors can handle the same base contract properly");
        }
        
        public static void DemonstrateBankingOperations()
        {
            Console.WriteLine("\n=== Banking Operations (LSP Compliant) ===");
            
            var accounts = new List<Account>
            {
                new CheckingAccount("CHK001", 1000, 500), // $500 overdraft
                new SavingsAccount("SAV001", 1000, 100)   // $100 minimum balance
            };
            
            var bankingService = new BankingService();
            bankingService.ProcessTransactions(accounts);
            
            // Demonstrate read-only access using composition
            Console.WriteLine("\n👁️ Read-only account access:");
            var accountView = new AccountViewService(accounts[0]);
            Console.WriteLine(accountView.GetAccountSummary());
            
            Console.WriteLine("✅ All account types follow the same contract");
        }
        
        public static void DemonstrateAllBenefits()
        {
            Console.WriteLine("=== GOOD Example: Following Liskov Substitution Principle ===\n");
            
            DemonstrateShapePolymorphism();
            DemonstrateBirdBehavior();
            DemonstrateFileProcessing();
            DemonstrateBankingOperations();
            
            Console.WriteLine("\n📋 Benefits of LSP Compliance:");
            Console.WriteLine("✅ Derived classes can be safely substituted for base classes");
            Console.WriteLine("✅ Polymorphism works correctly without unexpected behavior");
            Console.WriteLine("✅ Client code doesn't need to know about specific implementations");
            Console.WriteLine("✅ Contracts are respected throughout the inheritance hierarchy");
            Console.WriteLine("✅ No surprising exceptions or behavior changes");
            Console.WriteLine("✅ Code is more reliable and maintainable");
        }
    }
}

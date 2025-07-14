// Interface Segregation Principle - BAD Example
// This code violates ISP by having large, monolithic interfaces that force unnecessary dependencies

using System;
using System.Collections.Generic;

namespace SolidPrinciples.InterfaceSegregation.Bad
{
    // Fat interface that violates ISP - not all implementers need all methods
    public interface IWorker
    {
        // Basic work operations
        void Work();
        void TakeBreak();
        
        // Human-specific operations
        void Eat();
        void Sleep();
        void GoToMeeting();
        
        // Developer-specific operations
        void WriteCode();
        void TestCode();
        void DeployCode();
        void ReviewCode();
        
        // Manager-specific operations
        void HireEmployee();
        void FireEmployee();
        void ConductPerformanceReview();
        
        // Robot-specific operations
        void Recharge();
        void RunDiagnostics();
        void UpdateFirmware();
        
        // Maintenance operations
        void PerformMaintenance();
        void ReplaceComponent();
    }
    
    // Human worker can implement most methods but not robot-specific ones
    public class HumanDeveloper : IWorker
    {
        public string Name { get; set; }
        
        public HumanDeveloper(string name)
        {
            Name = name;
        }
        
        // Basic operations
        public void Work()
        {
            Console.WriteLine($"{Name} is working on development tasks...");
        }
        
        public void TakeBreak()
        {
            Console.WriteLine($"{Name} is taking a coffee break...");
        }
        
        // Human-specific operations
        public void Eat()
        {
            Console.WriteLine($"{Name} is eating lunch...");
        }
        
        public void Sleep()
        {
            Console.WriteLine($"{Name} is sleeping...");
        }
        
        public void GoToMeeting()
        {
            Console.WriteLine($"{Name} is attending a meeting...");
        }
        
        // Developer operations
        public void WriteCode()
        {
            Console.WriteLine($"{Name} is writing code...");
        }
        
        public void TestCode()
        {
            Console.WriteLine($"{Name} is testing code...");
        }
        
        public void DeployCode()
        {
            Console.WriteLine($"{Name} is deploying code...");
        }
        
        public void ReviewCode()
        {
            Console.WriteLine($"{Name} is reviewing code...");
        }
        
        // Forced to implement methods that don't apply
        public void HireEmployee()
        {
            throw new NotSupportedException("Developers cannot hire employees!"); // ISP violation!
        }
        
        public void FireEmployee()
        {
            throw new NotSupportedException("Developers cannot fire employees!"); // ISP violation!
        }
        
        public void ConductPerformanceReview()
        {
            throw new NotSupportedException("Developers don't conduct performance reviews!"); // ISP violation!
        }
        
        // Robot-specific operations that humans can't do
        public void Recharge()
        {
            throw new NotSupportedException("Humans don't recharge!"); // ISP violation!
        }
        
        public void RunDiagnostics()
        {
            throw new NotSupportedException("Humans don't run diagnostics!"); // ISP violation!
        }
        
        public void UpdateFirmware()
        {
            throw new NotSupportedException("Humans don't update firmware!"); // ISP violation!
        }
        
        public void PerformMaintenance()
        {
            throw new NotSupportedException("Developers don't perform maintenance!"); // ISP violation!
        }
        
        public void ReplaceComponent()
        {
            throw new NotSupportedException("Humans don't replace components!"); // ISP violation!
        }
    }
    
    // Robot worker forced to implement human-specific methods
    public class RobotWorker : IWorker
    {
        public string Model { get; set; }
        
        public RobotWorker(string model)
        {
            Model = model;
        }
        
        public void Work()
        {
            Console.WriteLine($"Robot {Model} is working...");
        }
        
        public void TakeBreak()
        {
            Console.WriteLine($"Robot {Model} is in standby mode...");
        }
        
        // Forced to implement human operations
        public void Eat()
        {
            throw new NotSupportedException("Robots don't eat!"); // ISP violation!
        }
        
        public void Sleep()
        {
            throw new NotSupportedException("Robots don't sleep!"); // ISP violation!
        }
        
        public void GoToMeeting()
        {
            throw new NotSupportedException("Robots don't attend meetings!"); // ISP violation!
        }
        
        // Robot can do some development work
        public void WriteCode()
        {
            Console.WriteLine($"Robot {Model} is generating code...");
        }
        
        public void TestCode()
        {
            Console.WriteLine($"Robot {Model} is running automated tests...");
        }
        
        public void DeployCode()
        {
            Console.WriteLine($"Robot {Model} is deploying code...");
        }
        
        public void ReviewCode()
        {
            Console.WriteLine($"Robot {Model} is analyzing code...");
        }
        
        // Management operations robots can't do
        public void HireEmployee()
        {
            throw new NotSupportedException("Robots cannot hire employees!"); // ISP violation!
        }
        
        public void FireEmployee()
        {
            throw new NotSupportedException("Robots cannot fire employees!"); // ISP violation!
        }
        
        public void ConductPerformanceReview()
        {
            throw new NotSupportedException("Robots don't conduct reviews!"); // ISP violation!
        }
        
        // Robot-specific operations
        public void Recharge()
        {
            Console.WriteLine($"Robot {Model} is recharging battery...");
        }
        
        public void RunDiagnostics()
        {
            Console.WriteLine($"Robot {Model} is running system diagnostics...");
        }
        
        public void UpdateFirmware()
        {
            Console.WriteLine($"Robot {Model} is updating firmware...");
        }
        
        public void PerformMaintenance()
        {
            Console.WriteLine($"Robot {Model} is performing self-maintenance...");
        }
        
        public void ReplaceComponent()
        {
            Console.WriteLine($"Robot {Model} is replacing a component...");
        }
    }
    
    // Manager forced to implement technical operations
    public class ProjectManager : IWorker
    {
        public string Name { get; set; }
        
        public ProjectManager(string name)
        {
            Name = name;
        }
        
        public void Work()
        {
            Console.WriteLine($"Manager {Name} is managing projects...");
        }
        
        public void TakeBreak()
        {
            Console.WriteLine($"Manager {Name} is taking a break...");
        }
        
        // Human operations
        public void Eat()
        {
            Console.WriteLine($"Manager {Name} is having a business lunch...");
        }
        
        public void Sleep()
        {
            Console.WriteLine($"Manager {Name} is sleeping...");
        }
        
        public void GoToMeeting()
        {
            Console.WriteLine($"Manager {Name} is conducting a meeting...");
        }
        
        // Forced to implement technical operations they might not do
        public void WriteCode()
        {
            throw new NotSupportedException("Managers typically don't write code!"); // ISP violation!
        }
        
        public void TestCode()
        {
            throw new NotSupportedException("Managers don't test code!"); // ISP violation!
        }
        
        public void DeployCode()
        {
            throw new NotSupportedException("Managers don't deploy code!"); // ISP violation!
        }
        
        public void ReviewCode()
        {
            Console.WriteLine($"Manager {Name} is reviewing code quality...");
        }
        
        // Management operations
        public void HireEmployee()
        {
            Console.WriteLine($"Manager {Name} is hiring a new employee...");
        }
        
        public void FireEmployee()
        {
            Console.WriteLine($"Manager {Name} is letting someone go...");
        }
        
        public void ConductPerformanceReview()
        {
            Console.WriteLine($"Manager {Name} is conducting performance review...");
        }
        
        // Robot operations that don't apply
        public void Recharge()
        {
            throw new NotSupportedException("Managers don't recharge!"); // ISP violation!
        }
        
        public void RunDiagnostics()
        {
            throw new NotSupportedException("Managers don't run diagnostics!"); // ISP violation!
        }
        
        public void UpdateFirmware()
        {
            throw new NotSupportedException("Managers don't update firmware!"); // ISP violation!
        }
        
        public void PerformMaintenance()
        {
            throw new NotSupportedException("Managers don't perform maintenance!"); // ISP violation!
        }
        
        public void ReplaceComponent()
        {
            throw new NotSupportedException("Managers don't replace components!"); // ISP violation!
        }
    }
    
    // Another fat interface example - Document operations
    public interface IDocument
    {
        // Basic operations
        void Open();
        void Save();
        void Close();
        
        // Display operations
        void Print();
        void Preview();
        
        // Communication operations
        void Email();
        void Fax();
        
        // Scanning operations
        void Scan();
        void OCR();
        
        // Security operations
        void Encrypt();
        void Decrypt();
        void AddDigitalSignature();
        
        // Conversion operations
        void ConvertToPdf();
        void ConvertToWord();
        void ConvertToExcel();
        
        // Backup operations
        void Backup();
        void Restore();
        
        // Collaboration operations
        void ShareWithTeam();
        void AddComments();
        void TrackChanges();
    }
    
    // Simple text editor forced to implement methods it doesn't support
    public class SimpleTextEditor : IDocument
    {
        private string _content;
        
        public void Open()
        {
            Console.WriteLine("Opening text file...");
            _content = "Sample text content";
        }
        
        public void Save()
        {
            Console.WriteLine("Saving text file...");
        }
        
        public void Close()
        {
            Console.WriteLine("Closing text file...");
        }
        
        public void Print()
        {
            Console.WriteLine("Printing text document...");
        }
        
        public void Preview()
        {
            Console.WriteLine("Previewing text document...");
        }
        
        // Forced to implement methods it doesn't support
        public void Email()
        {
            throw new NotSupportedException("Simple text editor doesn't support email!"); // ISP violation!
        }
        
        public void Fax()
        {
            throw new NotSupportedException("Simple text editor doesn't support fax!"); // ISP violation!
        }
        
        public void Scan()
        {
            throw new NotSupportedException("Text editor cannot scan!"); // ISP violation!
        }
        
        public void OCR()
        {
            throw new NotSupportedException("Text editor doesn't support OCR!"); // ISP violation!
        }
        
        public void Encrypt()
        {
            throw new NotSupportedException("Simple text editor doesn't support encryption!"); // ISP violation!
        }
        
        public void Decrypt()
        {
            throw new NotSupportedException("Simple text editor doesn't support decryption!"); // ISP violation!
        }
        
        public void AddDigitalSignature()
        {
            throw new NotSupportedException("Text editor doesn't support digital signatures!"); // ISP violation!
        }
        
        public void ConvertToPdf()
        {
            throw new NotSupportedException("Text editor doesn't convert to PDF!"); // ISP violation!
        }
        
        public void ConvertToWord()
        {
            throw new NotSupportedException("Text editor doesn't convert to Word!"); // ISP violation!
        }
        
        public void ConvertToExcel()
        {
            throw new NotSupportedException("Text editor doesn't convert to Excel!"); // ISP violation!
        }
        
        public void Backup()
        {
            throw new NotSupportedException("Text editor doesn't support backup!"); // ISP violation!
        }
        
        public void Restore()
        {
            throw new NotSupportedException("Text editor doesn't support restore!"); // ISP violation!
        }
        
        public void ShareWithTeam()
        {
            throw new NotSupportedException("Text editor doesn't support team sharing!"); // ISP violation!
        }
        
        public void AddComments()
        {
            throw new NotSupportedException("Text editor doesn't support comments!"); // ISP violation!
        }
        
        public void TrackChanges()
        {
            throw new NotSupportedException("Text editor doesn't track changes!"); // ISP violation!
        }
    }
    
    // Usage examples showing the problems
    public class ISPViolationExamples
    {
        public static void DemonstrateWorkerProblems()
        {
            Console.WriteLine("=== Worker Interface Violations ===");
            
            var workers = new List<IWorker>
            {
                new HumanDeveloper("Alice"),
                new RobotWorker("R2D2"),
                new ProjectManager("Bob")
            };
            
            Console.WriteLine("Trying to make all workers do basic work:");
            foreach (var worker in workers)
            {
                try
                {
                    worker.Work();
                    worker.TakeBreak();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error: {ex.Message}");
                }
            }
            
            Console.WriteLine("\nTrying to make all workers eat:");
            foreach (var worker in workers)
            {
                try
                {
                    worker.Eat();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error: {ex.Message}");
                }
            }
            
            Console.WriteLine("\nTrying to make all workers write code:");
            foreach (var worker in workers)
            {
                try
                {
                    worker.WriteCode();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error: {ex.Message}");
                }
            }
        }
        
        public static void DemonstrateDocumentProblems()
        {
            Console.WriteLine("\n=== Document Interface Violations ===");
            
            var documents = new List<IDocument>
            {
                new SimpleTextEditor()
            };
            
            foreach (var doc in documents)
            {
                Console.WriteLine($"\nTesting {doc.GetType().Name}:");
                
                // Basic operations work
                try
                {
                    doc.Open();
                    doc.Save();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error: {ex.Message}");
                }
                
                // Many operations fail
                try
                {
                    doc.Email();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error: {ex.Message}");
                }
                
                try
                {
                    doc.Encrypt();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error: {ex.Message}");
                }
                
                try
                {
                    doc.ConvertToPdf();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error: {ex.Message}");
                }
            }
        }
        
        public static void DemonstrateAllProblems()
        {
            Console.WriteLine("=== BAD Example: Violating Interface Segregation Principle ===\n");
            
            DemonstrateWorkerProblems();
            DemonstrateDocumentProblems();
            
            Console.WriteLine("\n📋 Problems with Fat Interfaces:");
            Console.WriteLine("❌ Classes forced to implement methods they don't need");
            Console.WriteLine("❌ Many methods throw NotSupportedException");
            Console.WriteLine("❌ High coupling - changes affect unrelated implementations");
            Console.WriteLine("❌ Difficult to test - need to mock large interfaces");
            Console.WriteLine("❌ Violates Single Responsibility Principle");
            Console.WriteLine("❌ Interface becomes a 'god interface' that does everything");
            Console.WriteLine("❌ Client code depends on methods it doesn't use");
        }
    }
}

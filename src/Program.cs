using System;
using SolidPrinciples.SingleResponsibility.Good;
using SolidPrinciples.OpenClosed.Good;
using SolidPrinciples.LiskovSubstitution.Good;
using SolidPrinciples.InterfaceSegregation.Good;
using SolidPrinciples.DependencyInversion.Good;

namespace SolidPrinciples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("====================================================");
            Console.WriteLine("🏗️  SOLID PRINCIPLES DEMONSTRATION IN C#");
            Console.WriteLine("====================================================");
            Console.WriteLine();
            
            Console.WriteLine("This demonstration shows both BAD and GOOD examples of each SOLID principle:");
            Console.WriteLine("📐 S - Single Responsibility Principle");
            Console.WriteLine("🔓 O - Open/Closed Principle");
            Console.WriteLine("🔄 L - Liskov Substitution Principle");
            Console.WriteLine("🧩 I - Interface Segregation Principle");
            Console.WriteLine("🔗 D - Dependency Inversion Principle");
            Console.WriteLine();
            
            while (true)
            {
                ShowMenu();
                var choice = Console.ReadLine();
                
                switch (choice?.ToUpper())
                {
                    case "1":
                        DemonstrateSingleResponsibility();
                        break;
                    case "2":
                        DemonstrateOpenClosed();
                        break;
                    case "3":
                        DemonstrateLiskovSubstitution();
                        break;
                    case "4":
                        DemonstrateInterfaceSegregation();
                        break;
                    case "5":
                        DemonstrateDependencyInversion();
                        break;
                    case "A":
                        DemonstrateAllPrinciples();
                        break;
                    case "Q":
                        Console.WriteLine("👋 Thank you for exploring SOLID principles!");
                        return;
                    default:
                        Console.WriteLine("❌ Invalid choice. Please try again.");
                        break;
                }
                
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }
        
        static void ShowMenu()
        {
            Console.WriteLine("====================================================");
            Console.WriteLine("📚 SOLID PRINCIPLES MENU");
            Console.WriteLine("====================================================");
            Console.WriteLine("1. 📐 Single Responsibility Principle (SRP)");
            Console.WriteLine("2. 🔓 Open/Closed Principle (OCP)");
            Console.WriteLine("3. 🔄 Liskov Substitution Principle (LSP)");
            Console.WriteLine("4. 🧩 Interface Segregation Principle (ISP)");
            Console.WriteLine("5. 🔗 Dependency Inversion Principle (DIP)");
            Console.WriteLine("A. 🎯 Demonstrate ALL Principles");
            Console.WriteLine("Q. 🚪 Quit");
            Console.WriteLine("====================================================");
            Console.Write("Enter your choice: ");
        }
        
        static void DemonstrateSingleResponsibility()
        {
            Console.WriteLine("📐 SINGLE RESPONSIBILITY PRINCIPLE");
            Console.WriteLine("====================================================");
            Console.WriteLine("\"A class should have only one reason to change.\"");
            Console.WriteLine();
            
            // Show bad example
            SingleResponsibility.Bad.UserManagementExample.DemonstrateProblems();
            Console.WriteLine();
            
            // Show good example
            SingleResponsibility.Good.UserManagementExample.DemonstrateBenefits();
        }
        
        static void DemonstrateOpenClosed()
        {
            Console.WriteLine("🔓 OPEN/CLOSED PRINCIPLE");
            Console.WriteLine("====================================================");
            Console.WriteLine("\"Software entities should be open for extension but closed for modification.\"");
            Console.WriteLine();
            
            // Show bad example
            OpenClosed.Bad.OCPViolationExample.DemonstrateProblems();
            Console.WriteLine();
            
            // Show good example
            OpenClosed.Good.OCPComplianceExample.DemonstrateBenefits();
        }
        
        static void DemonstrateLiskovSubstitution()
        {
            Console.WriteLine("🔄 LISKOV SUBSTITUTION PRINCIPLE");
            Console.WriteLine("====================================================");
            Console.WriteLine("\"Objects of a superclass should be replaceable with objects of its subclasses without breaking the application.\"");
            Console.WriteLine();
            
            // Show bad example
            LiskovSubstitution.Bad.LSPViolationExamples.DemonstrateAllProblems();
            Console.WriteLine();
            
            // Show good example
            LiskovSubstitution.Good.LSPComplianceExamples.DemonstrateAllBenefits();
        }
        
        static void DemonstrateInterfaceSegregation()
        {
            Console.WriteLine("🧩 INTERFACE SEGREGATION PRINCIPLE");
            Console.WriteLine("====================================================");
            Console.WriteLine("\"Clients should not be forced to depend upon interfaces that they do not use.\"");
            Console.WriteLine();
            
            // Show bad example
            InterfaceSegregation.Bad.ISPViolationExamples.DemonstrateAllProblems();
            Console.WriteLine();
            
            // Show good example
            InterfaceSegregation.Good.ISPComplianceExamples.DemonstrateAllBenefits();
        }
        
        static void DemonstrateDependencyInversion()
        {
            Console.WriteLine("🔗 DEPENDENCY INVERSION PRINCIPLE");
            Console.WriteLine("====================================================");
            Console.WriteLine("\"High-level modules should not depend on low-level modules. Both should depend on abstractions.\"");
            Console.WriteLine();
            
            // Show bad example
            DependencyInversion.Bad.DIPViolationExamples.DemonstrateAllProblems();
            Console.WriteLine();
            
            // Show good example
            DependencyInversion.Good.DIPComplianceExamples.DemonstrateAllBenefits();
        }
        
        static void DemonstrateAllPrinciples()
        {
            Console.WriteLine("🎯 DEMONSTRATING ALL SOLID PRINCIPLES");
            Console.WriteLine("====================================================");
            Console.WriteLine();
            
            // Demonstrate each principle briefly
            Console.WriteLine("📐 1. SINGLE RESPONSIBILITY PRINCIPLE");
            Console.WriteLine("Each class has one reason to change");
            SingleResponsibility.Good.UserManagementExample.DemonstrateBenefits();
            Console.WriteLine("\n" + new string('─', 60) + "\n");
            
            Console.WriteLine("🔓 2. OPEN/CLOSED PRINCIPLE");
            Console.WriteLine("Open for extension, closed for modification");
            OpenClosed.Good.OCPComplianceExample.DemonstrateBenefits();
            Console.WriteLine("\n" + new string('─', 60) + "\n");
            
            Console.WriteLine("🔄 3. LISKOV SUBSTITUTION PRINCIPLE");
            Console.WriteLine("Subtypes must be substitutable for their base types");
            LiskovSubstitution.Good.LSPComplianceExamples.DemonstrateAllBenefits();
            Console.WriteLine("\n" + new string('─', 60) + "\n");
            
            Console.WriteLine("🧩 4. INTERFACE SEGREGATION PRINCIPLE");
            Console.WriteLine("Many specific interfaces are better than one general-purpose interface");
            InterfaceSegregation.Good.ISPComplianceExamples.DemonstrateAllBenefits();
            Console.WriteLine("\n" + new string('─', 60) + "\n");
            
            Console.WriteLine("🔗 5. DEPENDENCY INVERSION PRINCIPLE");
            Console.WriteLine("Depend on abstractions, not concretions");
            DependencyInversion.Good.DIPComplianceExamples.DemonstrateAllBenefits();
            
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("🎉 SOLID PRINCIPLES COMPLETE!");
            Console.WriteLine("These principles work together to create maintainable, flexible, and robust code.");
            Console.WriteLine(new string('=', 60));
        }
    }
}

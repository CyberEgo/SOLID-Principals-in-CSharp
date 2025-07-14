// Open/Closed Principle - BAD Example
// This code violates OCP by requiring modification every time a new shape is added

using System;

namespace SolidPrinciples.OpenClosed.Bad
{
    // Shape classes without common interface
    public class Rectangle
    {
        public double Width { get; set; }
        public double Height { get; set; }
    }
    
    public class Circle
    {
        public double Radius { get; set; }
    }
    
    public class Triangle
    {
        public double Base { get; set; }
        public double Height { get; set; }
    }
    
    // This class violates OCP - it needs to be modified every time we add a new shape
    public class AreaCalculator
    {
        public double CalculateArea(object shape)
        {
            if (shape is Rectangle rectangle)
            {
                return rectangle.Width * rectangle.Height;
            }
            else if (shape is Circle circle)
            {
                return Math.PI * circle.Radius * circle.Radius;
            }
            else if (shape is Triangle triangle)
            {
                return 0.5 * triangle.Base * triangle.Height;
            }
            // Every time we add a new shape, we need to modify this method!
            // This violates the Open/Closed Principle
            
            throw new ArgumentException($"Unknown shape type: {shape.GetType().Name}");
        }
        
        public double CalculatePerimeter(object shape)
        {
            if (shape is Rectangle rectangle)
            {
                return 2 * (rectangle.Width + rectangle.Height);
            }
            else if (shape is Circle circle)
            {
                return 2 * Math.PI * circle.Radius;
            }
            else if (shape is Triangle triangle)
            {
                // For simplicity, assuming equilateral triangle
                return 3 * triangle.Base;
            }
            // Again, we need to modify this method for every new shape!
            
            throw new ArgumentException($"Unknown shape type: {shape.GetType().Name}");
        }
    }
    
    // Discount calculation also violates OCP
    public class DiscountCalculator
    {
        public decimal ApplyDiscount(decimal amount, string customerType)
        {
            if (customerType == "Regular")
            {
                return amount; // No discount
            }
            else if (customerType == "Premium")
            {
                return amount * 0.9m; // 10% discount
            }
            else if (customerType == "VIP")
            {
                return amount * 0.8m; // 20% discount
            }
            // Adding new customer types requires modifying this method
            
            throw new ArgumentException($"Unknown customer type: {customerType}");
        }
    }
    
    // Report generator also violates OCP
    public class ReportGenerator
    {
        public string GenerateReport(object data, string format)
        {
            if (format == "PDF")
            {
                return GeneratePdfReport(data);
            }
            else if (format == "Excel")
            {
                return GenerateExcelReport(data);
            }
            else if (format == "HTML")
            {
                return GenerateHtmlReport(data);
            }
            // Adding new formats requires modifying this method
            
            throw new ArgumentException($"Unknown format: {format}");
        }
        
        private string GeneratePdfReport(object data)
        {
            return $"PDF Report: {data}";
        }
        
        private string GenerateExcelReport(object data)
        {
            return $"Excel Report: {data}";
        }
        
        private string GenerateHtmlReport(object data)
        {
            return $"HTML Report: {data}";
        }
    }
    
    // Usage example showing the problems
    public class OCPViolationExample
    {
        public static void DemonstrateProblems()
        {
            Console.WriteLine("=== BAD Example: Violating Open/Closed Principle ===\n");
            
            var calculator = new AreaCalculator();
            var discountCalculator = new DiscountCalculator();
            var reportGenerator = new ReportGenerator();
            
            // Create shapes
            var rectangle = new Rectangle { Width = 5, Height = 3 };
            var circle = new Circle { Radius = 2 };
            var triangle = new Triangle { Base = 4, Height = 3 };
            
            // Calculate areas
            Console.WriteLine($"Rectangle area: {calculator.CalculateArea(rectangle)}");
            Console.WriteLine($"Circle area: {calculator.CalculateArea(circle):F2}");
            Console.WriteLine($"Triangle area: {calculator.CalculateArea(triangle)}");
            
            // Calculate discounts
            decimal amount = 100m;
            Console.WriteLine($"\nRegular customer discount: ${discountCalculator.ApplyDiscount(amount, "Regular")}");
            Console.WriteLine($"Premium customer discount: ${discountCalculator.ApplyDiscount(amount, "Premium")}");
            Console.WriteLine($"VIP customer discount: ${discountCalculator.ApplyDiscount(amount, "VIP")}");
            
            // Generate reports
            var data = "Sample data";
            Console.WriteLine($"\nPDF Report: {reportGenerator.GenerateReport(data, "PDF")}");
            Console.WriteLine($"Excel Report: {reportGenerator.GenerateReport(data, "Excel")}");
            
            Console.WriteLine("\n❌ Problems with this approach:");
            Console.WriteLine("1. Adding new shapes requires modifying AreaCalculator");
            Console.WriteLine("2. Adding new customer types requires modifying DiscountCalculator");
            Console.WriteLine("3. Adding new report formats requires modifying ReportGenerator");
            Console.WriteLine("4. Risk of breaking existing functionality when adding new features");
            Console.WriteLine("5. Violates Single Responsibility Principle as well");
            Console.WriteLine("6. Code becomes harder to maintain as more types are added");
            
            // Simulate adding a new shape - this would require code changes
            Console.WriteLine("\n💡 To add a Hexagon shape, we would need to:");
            Console.WriteLine("1. Create a Hexagon class");
            Console.WriteLine("2. Modify AreaCalculator.CalculateArea() method");
            Console.WriteLine("3. Modify AreaCalculator.CalculatePerimeter() method");
            Console.WriteLine("4. Risk introducing bugs in existing shape calculations");
        }
    }
}

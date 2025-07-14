// Open/Closed Principle - GOOD Example
// This code follows OCP by being open for extension but closed for modification

using System;
using System.Collections.Generic;
using System.Linq;

namespace SolidPrinciples.OpenClosed.Good
{
    // Abstract base class defines the contract for all shapes
    public abstract class Shape
    {
        public abstract double CalculateArea();
        public abstract double CalculatePerimeter();
        public abstract string GetShapeInfo();
    }
    
    // Each shape implements its own calculations
    public class Rectangle : Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }
        
        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
        }
        
        public override double CalculateArea()
        {
            return Width * Height;
        }
        
        public override double CalculatePerimeter()
        {
            return 2 * (Width + Height);
        }
        
        public override string GetShapeInfo()
        {
            return $"Rectangle: {Width} x {Height}";
        }
    }
    
    public class Circle : Shape
    {
        public double Radius { get; set; }
        
        public Circle(double radius)
        {
            Radius = radius;
        }
        
        public override double CalculateArea()
        {
            return Math.PI * Radius * Radius;
        }
        
        public override double CalculatePerimeter()
        {
            return 2 * Math.PI * Radius;
        }
        
        public override string GetShapeInfo()
        {
            return $"Circle: radius {Radius}";
        }
    }
    
    public class Triangle : Shape
    {
        public double Base { get; set; }
        public double Height { get; set; }
        public double Side1 { get; set; }
        public double Side2 { get; set; }
        
        public Triangle(double baseLength, double height, double side1, double side2)
        {
            Base = baseLength;
            Height = height;
            Side1 = side1;
            Side2 = side2;
        }
        
        public override double CalculateArea()
        {
            return 0.5 * Base * Height;
        }
        
        public override double CalculatePerimeter()
        {
            return Base + Side1 + Side2;
        }
        
        public override string GetShapeInfo()
        {
            return $"Triangle: base {Base}, height {Height}";
        }
    }
    
    // NEW SHAPE: Adding this doesn't require modifying existing code!
    public class Hexagon : Shape
    {
        public double Side { get; set; }
        
        public Hexagon(double side)
        {
            Side = side;
        }
        
        public override double CalculateArea()
        {
            return (3 * Math.Sqrt(3) / 2) * Side * Side;
        }
        
        public override double CalculatePerimeter()
        {
            return 6 * Side;
        }
        
        public override string GetShapeInfo()
        {
            return $"Hexagon: side {Side}";
        }
    }
    
    // The calculator is now closed for modification but open for extension
    public class AreaCalculator
    {
        public double CalculateArea(Shape shape)
        {
            return shape.CalculateArea();
        }
        
        public double CalculatePerimeter(Shape shape)
        {
            return shape.CalculatePerimeter();
        }
        
        public double CalculateTotalArea(IEnumerable<Shape> shapes)
        {
            return shapes.Sum(shape => shape.CalculateArea());
        }
        
        public ShapeReport GenerateReport(IEnumerable<Shape> shapes)
        {
            return new ShapeReport
            {
                TotalShapes = shapes.Count(),
                TotalArea = CalculateTotalArea(shapes),
                TotalPerimeter = shapes.Sum(s => s.CalculatePerimeter()),
                ShapeDetails = shapes.Select(s => new ShapeDetail
                {
                    Type = s.GetType().Name,
                    Info = s.GetShapeInfo(),
                    Area = s.CalculateArea(),
                    Perimeter = s.CalculatePerimeter()
                }).ToList()
            };
        }
    }
    
    // Strategy pattern for discount calculation - follows OCP
    public interface IDiscountStrategy
    {
        decimal ApplyDiscount(decimal amount);
        string GetDiscountDescription();
    }
    
    public class NoDiscount : IDiscountStrategy
    {
        public decimal ApplyDiscount(decimal amount)
        {
            return amount;
        }
        
        public string GetDiscountDescription()
        {
            return "No discount applied";
        }
    }
    
    public class PercentageDiscount : IDiscountStrategy
    {
        private readonly decimal _percentage;
        private readonly string _description;
        
        public PercentageDiscount(decimal percentage, string description = "")
        {
            _percentage = percentage;
            _description = string.IsNullOrEmpty(description) 
                ? $"{percentage}% discount" 
                : description;
        }
        
        public decimal ApplyDiscount(decimal amount)
        {
            return amount * (1 - _percentage / 100);
        }
        
        public string GetDiscountDescription()
        {
            return _description;
        }
    }
    
    public class FixedAmountDiscount : IDiscountStrategy
    {
        private readonly decimal _discountAmount;
        
        public FixedAmountDiscount(decimal discountAmount)
        {
            _discountAmount = discountAmount;
        }
        
        public decimal ApplyDiscount(decimal amount)
        {
            return Math.Max(0, amount - _discountAmount);
        }
        
        public string GetDiscountDescription()
        {
            return $"${_discountAmount} off";
        }
    }
    
    // NEW DISCOUNT TYPE: Can add without modifying existing code!
    public class BuyOneGetOneDiscount : IDiscountStrategy
    {
        public decimal ApplyDiscount(decimal amount)
        {
            return amount * 0.5m; // 50% off for BOGO
        }
        
        public string GetDiscountDescription()
        {
            return "Buy One Get One (50% off)";
        }
    }
    
    public class PriceCalculator
    {
        public PriceResult CalculatePrice(decimal basePrice, IDiscountStrategy discountStrategy)
        {
            var discountedPrice = discountStrategy.ApplyDiscount(basePrice);
            return new PriceResult
            {
                OriginalPrice = basePrice,
                DiscountedPrice = discountedPrice,
                Savings = basePrice - discountedPrice,
                DiscountDescription = discountStrategy.GetDiscountDescription()
            };
        }
    }
    
    // Strategy pattern for report generation - follows OCP
    public interface IReportFormatter
    {
        string GenerateReport(object data);
        string GetFormatName();
    }
    
    public class PdfReportFormatter : IReportFormatter
    {
        public string GenerateReport(object data)
        {
            return $"📄 PDF Report Generated\n{FormatContent(data)}";
        }
        
        public string GetFormatName() => "PDF";
        
        private string FormatContent(object data)
        {
            return $"PDF-formatted content: {data}";
        }
    }
    
    public class ExcelReportFormatter : IReportFormatter
    {
        public string GenerateReport(object data)
        {
            return $"📊 Excel Report Generated\n{FormatContent(data)}";
        }
        
        public string GetFormatName() => "Excel";
        
        private string FormatContent(object data)
        {
            return $"Excel-formatted content with formulas: {data}";
        }
    }
    
    public class HtmlReportFormatter : IReportFormatter
    {
        public string GenerateReport(object data)
        {
            return $"🌐 HTML Report Generated\n{FormatContent(data)}";
        }
        
        public string GetFormatName() => "HTML";
        
        private string FormatContent(object data)
        {
            return $"<html><body><h1>Report</h1><p>{data}</p></body></html>";
        }
    }
    
    // NEW REPORT FORMAT: Can add without modifying existing code!
    public class JsonReportFormatter : IReportFormatter
    {
        public string GenerateReport(object data)
        {
            return $"🔗 JSON Report Generated\n{FormatContent(data)}";
        }
        
        public string GetFormatName() => "JSON";
        
        private string FormatContent(object data)
        {
            return $"{{ \"report\": \"{data}\", \"timestamp\": \"{DateTime.Now}\" }}";
        }
    }
    
    public class ReportGenerator
    {
        private readonly List<IReportFormatter> _formatters;
        
        public ReportGenerator()
        {
            _formatters = new List<IReportFormatter>();
        }
        
        public void RegisterFormatter(IReportFormatter formatter)
        {
            _formatters.Add(formatter);
        }
        
        public string GenerateReport(object data, string format)
        {
            var formatter = _formatters.FirstOrDefault(f => 
                f.GetFormatName().Equals(format, StringComparison.OrdinalIgnoreCase));
                
            if (formatter == null)
            {
                throw new ArgumentException($"Unknown format: {format}. Available formats: {string.Join(", ", _formatters.Select(f => f.GetFormatName()))}");
            }
            
            return formatter.GenerateReport(data);
        }
        
        public List<string> GetAvailableFormats()
        {
            return _formatters.Select(f => f.GetFormatName()).ToList();
        }
    }
    
    // Supporting classes
    public class ShapeReport
    {
        public int TotalShapes { get; set; }
        public double TotalArea { get; set; }
        public double TotalPerimeter { get; set; }
        public List<ShapeDetail> ShapeDetails { get; set; }
        
        public override string ToString()
        {
            var details = string.Join("\n", ShapeDetails.Select(d => 
                $"  - {d.Type}: {d.Info} (Area: {d.Area:F2}, Perimeter: {d.Perimeter:F2})"));
            return $"Shape Report:\n{details}\nTotal Area: {TotalArea:F2}, Total Perimeter: {TotalPerimeter:F2}";
        }
    }
    
    public class ShapeDetail
    {
        public string Type { get; set; }
        public string Info { get; set; }
        public double Area { get; set; }
        public double Perimeter { get; set; }
    }
    
    public class PriceResult
    {
        public decimal OriginalPrice { get; set; }
        public decimal DiscountedPrice { get; set; }
        public decimal Savings { get; set; }
        public string DiscountDescription { get; set; }
        
        public override string ToString()
        {
            return $"Original: ${OriginalPrice:F2}, Final: ${DiscountedPrice:F2}, Savings: ${Savings:F2} ({DiscountDescription})";
        }
    }
    
    // Usage example demonstrating the benefits
    public class OCPComplianceExample
    {
        public static void DemonstrateBenefits()
        {
            Console.WriteLine("=== GOOD Example: Following Open/Closed Principle ===\n");
            
            // Create shapes - including the new Hexagon!
            var shapes = new List<Shape>
            {
                new Rectangle(5, 3),
                new Circle(2),
                new Triangle(4, 3, 3, 5),
                new Hexagon(2) // New shape added without modifying existing code!
            };
            
            // Shape calculations work with all shapes
            var calculator = new AreaCalculator();
            Console.WriteLine("🔶 Shape Calculations:");
            foreach (var shape in shapes)
            {
                Console.WriteLine($"{shape.GetShapeInfo()} - Area: {shape.CalculateArea():F2}, Perimeter: {shape.CalculatePerimeter():F2}");
            }
            
            var report = calculator.GenerateReport(shapes);
            Console.WriteLine($"\n📊 {report}");
            
            // Discount calculations with different strategies
            var priceCalculator = new PriceCalculator();
            decimal basePrice = 100m;
            
            var discountStrategies = new List<IDiscountStrategy>
            {
                new NoDiscount(),
                new PercentageDiscount(10, "Premium member discount"),
                new PercentageDiscount(20, "VIP member discount"),
                new FixedAmountDiscount(15),
                new BuyOneGetOneDiscount() // New discount type!
            };
            
            Console.WriteLine("\n💰 Price Calculations:");
            foreach (var strategy in discountStrategies)
            {
                var result = priceCalculator.CalculatePrice(basePrice, strategy);
                Console.WriteLine($"  {result}");
            }
            
            // Report generation with different formats
            var reportGenerator = new ReportGenerator();
            
            // Register formatters
            reportGenerator.RegisterFormatter(new PdfReportFormatter());
            reportGenerator.RegisterFormatter(new ExcelReportFormatter());
            reportGenerator.RegisterFormatter(new HtmlReportFormatter());
            reportGenerator.RegisterFormatter(new JsonReportFormatter()); // New format!
            
            var reportData = "Sales data for Q4 2024";
            Console.WriteLine("\n📄 Report Generation:");
            
            foreach (var format in reportGenerator.GetAvailableFormats())
            {
                Console.WriteLine(reportGenerator.GenerateReport(reportData, format));
                Console.WriteLine();
            }
            
            Console.WriteLine("✅ Benefits of this approach:");
            Console.WriteLine("✅ New shapes can be added without modifying existing code");
            Console.WriteLine("✅ New discount strategies can be added easily");
            Console.WriteLine("✅ New report formats can be added without changes");
            Console.WriteLine("✅ Existing functionality remains stable and tested");
            Console.WriteLine("✅ Easy to extend and maintain");
            Console.WriteLine("✅ Follows Single Responsibility Principle as well");
        }
    }
}

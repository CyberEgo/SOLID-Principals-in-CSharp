# Open/Closed Principle (OCP)

> "Software entities should be open for extension, but closed for modification."
> 
> *- Bertrand Meyer*

## 📖 Definition

The Open/Closed Principle states that software entities (classes, modules, functions, etc.) should be open for extension but closed for modification. This means you should be able to add new functionality without changing existing code.

## 🎯 Why is OCP Important?

1. **Stability**: Existing code remains untouched, reducing the risk of introducing bugs
2. **Maintainability**: New features can be added without modifying existing, tested code
3. **Flexibility**: Easy to extend functionality to meet new requirements
4. **Testability**: Existing tests remain valid when new features are added
5. **Reusability**: Components can be extended and reused in different contexts

## ❌ Bad Example

```csharp
// This class violates OCP - we need to modify it every time we add a new shape
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
        // Every time we add a new shape, we need to modify this method
        else if (shape is Triangle triangle)
        {
            return 0.5 * triangle.Base * triangle.Height;
        }
        
        throw new ArgumentException("Unknown shape");
    }
}

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
```

**Problems with this approach:**
- Adding a new shape requires modifying the `AreaCalculator` class
- Risk of breaking existing functionality when adding new features
- Violates the Single Responsibility Principle as well
- Code becomes harder to maintain as more shapes are added

## ✅ Good Example

```csharp
// Abstract base class defines the contract
public abstract class Shape
{
    public abstract double CalculateArea();
}

// Each shape implements its own area calculation
public class Rectangle : Shape
{
    public double Width { get; set; }
    public double Height { get; set; }
    
    public override double CalculateArea()
    {
        return Width * Height;
    }
}

public class Circle : Shape
{
    public double Radius { get; set; }
    
    public override double CalculateArea()
    {
        return Math.PI * Radius * Radius;
    }
}

public class Triangle : Shape
{
    public double Base { get; set; }
    public double Height { get; set; }
    
    public override double CalculateArea()
    {
        return 0.5 * Base * Height;
    }
}

// The calculator is now closed for modification but open for extension
public class AreaCalculator
{
    public double CalculateArea(Shape shape)
    {
        return shape.CalculateArea();
    }
    
    public double CalculateTotalArea(IEnumerable<Shape> shapes)
    {
        return shapes.Sum(shape => shape.CalculateArea());
    }
}

// Adding a new shape doesn't require modifying existing code
public class Hexagon : Shape
{
    public double Side { get; set; }
    
    public override double CalculateArea()
    {
        return (3 * Math.Sqrt(3) / 2) * Side * Side;
    }
}
```

**Benefits of this approach:**
- Adding new shapes doesn't require modifying existing code
- Each shape is responsible for its own area calculation
- The system is easily extensible
- Existing code remains stable and tested

## 🔧 Alternative Approach Using Interfaces

```csharp
// Using interfaces for even more flexibility
public interface IShape
{
    double CalculateArea();
}

public interface IPerimeter
{
    double CalculatePerimeter();
}

// Shapes can implement multiple interfaces
public class Rectangle : IShape, IPerimeter
{
    public double Width { get; set; }
    public double Height { get; set; }
    
    public double CalculateArea()
    {
        return Width * Height;
    }
    
    public double CalculatePerimeter()
    {
        return 2 * (Width + Height);
    }
}

public class Circle : IShape, IPerimeter
{
    public double Radius { get; set; }
    
    public double CalculateArea()
    {
        return Math.PI * Radius * Radius;
    }
    
    public double CalculatePerimeter()
    {
        return 2 * Math.PI * Radius;
    }
}

// Calculator can work with any shape
public class GeometryCalculator
{
    public double CalculateArea(IShape shape)
    {
        return shape.CalculateArea();
    }
    
    public double CalculatePerimeter(IPerimeter shape)
    {
        return shape.CalculatePerimeter();
    }
}
```

## 🎯 Real-World Example: Payment Processing

**Bad (Violates OCP):**
```csharp
public class PaymentProcessor
{
    public void ProcessPayment(string paymentType, decimal amount)
    {
        if (paymentType == "CreditCard")
        {
            // Credit card processing logic
            Console.WriteLine($"Processing ${amount} via Credit Card");
        }
        else if (paymentType == "PayPal")
        {
            // PayPal processing logic
            Console.WriteLine($"Processing ${amount} via PayPal");
        }
        // Adding new payment methods requires modifying this method
        else if (paymentType == "Bitcoin")
        {
            Console.WriteLine($"Processing ${amount} via Bitcoin");
        }
        else
        {
            throw new ArgumentException("Unsupported payment type");
        }
    }
}
```

**Good (Follows OCP):**
```csharp
public abstract class PaymentMethod
{
    public abstract void ProcessPayment(decimal amount);
}

public class CreditCardPayment : PaymentMethod
{
    public string CardNumber { get; set; }
    
    public override void ProcessPayment(decimal amount)
    {
        Console.WriteLine($"Processing ${amount} via Credit Card ending in {CardNumber.Substring(CardNumber.Length - 4)}");
    }
}

public class PayPalPayment : PaymentMethod
{
    public string Email { get; set; }
    
    public override void ProcessPayment(decimal amount)
    {
        Console.WriteLine($"Processing ${amount} via PayPal for {Email}");
    }
}

public class BitcoinPayment : PaymentMethod
{
    public string WalletAddress { get; set; }
    
    public override void ProcessPayment(decimal amount)
    {
        Console.WriteLine($"Processing ${amount} via Bitcoin to {WalletAddress}");
    }
}

public class PaymentProcessor
{
    public void ProcessPayment(PaymentMethod paymentMethod, decimal amount)
    {
        paymentMethod.ProcessPayment(amount);
    }
}
```

## 🏗️ Strategy Pattern Implementation

The Strategy pattern is a common way to implement OCP:

```csharp
public interface IDiscountStrategy
{
    decimal ApplyDiscount(decimal amount);
}

public class NoDiscount : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal amount)
    {
        return amount;
    }
}

public class PercentageDiscount : IDiscountStrategy
{
    private readonly decimal _percentage;
    
    public PercentageDiscount(decimal percentage)
    {
        _percentage = percentage;
    }
    
    public decimal ApplyDiscount(decimal amount)
    {
        return amount * (1 - _percentage / 100);
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
}

public class PriceCalculator
{
    public decimal CalculatePrice(decimal basePrice, IDiscountStrategy discountStrategy)
    {
        return discountStrategy.ApplyDiscount(basePrice);
    }
}

// Usage
var calculator = new PriceCalculator();
var price = 100m;

var noDiscount = calculator.CalculatePrice(price, new NoDiscount());
var withPercentage = calculator.CalculatePrice(price, new PercentageDiscount(10));
var withFixed = calculator.CalculatePrice(price, new FixedAmountDiscount(15));
```

## 🔍 How to Identify OCP Violations

Look for these patterns in your code:

1. **Long if-else or switch statements** that check types or conditions
2. **Frequent modifications** to the same class when adding new features
3. **Hardcoded dependencies** that don't allow for extension
4. **Classes that know too much** about concrete implementations

## 💡 Best Practices

1. **Use Abstraction**: Create interfaces or abstract classes to define contracts
2. **Dependency Injection**: Use DI to make your code more flexible
3. **Strategy Pattern**: Use strategies to encapsulate varying behavior
4. **Plugin Architecture**: Design systems that can accept new plugins
5. **Configuration over Code**: Use configuration files to extend behavior

## 🧪 Testing Benefits

With OCP, testing becomes more focused:

```csharp
[Test]
public void Rectangle_Should_Calculate_Area_Correctly()
{
    // Arrange
    var rectangle = new Rectangle { Width = 5, Height = 3 };
    
    // Act
    var area = rectangle.CalculateArea();
    
    // Assert
    Assert.AreEqual(15, area);
}

[Test]
public void AreaCalculator_Should_Calculate_Any_Shape()
{
    // Arrange
    var calculator = new AreaCalculator();
    var shapes = new List<Shape>
    {
        new Rectangle { Width = 5, Height = 3 },
        new Circle { Radius = 2 }
    };
    
    // Act
    var totalArea = calculator.CalculateTotalArea(shapes);
    
    // Assert
    Assert.AreEqual(15 + Math.PI * 4, totalArea, 0.001);
}
```

## ⚠️ Common Pitfalls

1. **Over-abstraction**: Don't create abstractions for everything; only when extension is likely
2. **Premature optimization**: Don't design for extension that will never come
3. **Complexity**: Balance extensibility with simplicity
4. **Performance**: Sometimes OCP can introduce performance overhead

## 📚 Next Steps

- Review the code examples in `src/2-OpenClosed/`
- Practice identifying extension points in existing code
- Move on to the [Liskov Substitution Principle](03-liskov-substitution.md)

# Liskov Substitution Principle (LSP)

> "Objects of a superclass should be replaceable with objects of a subclass without breaking the application."
> 
> *- Barbara Liskov*

## 📖 Definition

The Liskov Substitution Principle states that objects of a superclass should be replaceable with objects of any of its subclasses without altering the correctness of the program. In other words, if S is a subtype of T, then objects of type T may be replaced with objects of type S without breaking the program.

## 🎯 Why is LSP Important?

1. **Reliability**: Ensures that inheritance hierarchies are well-designed
2. **Polymorphism**: Enables proper use of polymorphism in object-oriented design
3. **Maintainability**: Prevents unexpected behavior when substituting objects
4. **Testability**: Allows for easier testing with mock objects and stubs
5. **Code Reusability**: Ensures that base class code can work with any derived class

## ❌ Bad Example

```csharp
// This violates LSP - Rectangle behavior changes unexpectedly when substituted with Square
public class Rectangle
{
    public virtual int Width { get; set; }
    public virtual int Height { get; set; }
    
    public int CalculateArea()
    {
        return Width * Height;
    }
}

public class Square : Rectangle
{
    public override int Width
    {
        get => base.Width;
        set
        {
            base.Width = value;
            base.Height = value; // This changes the behavior unexpectedly
        }
    }
    
    public override int Height
    {
        get => base.Height;
        set
        {
            base.Width = value;  // This changes the behavior unexpectedly
            base.Height = value;
        }
    }
}

// This code works fine with Rectangle but fails with Square
public class AreaCalculator
{
    public void DemonstrateIssue(Rectangle rectangle)
    {
        rectangle.Width = 5;
        rectangle.Height = 4;
        
        // Expected: 20, but if rectangle is actually a Square, we get 16
        Console.WriteLine($"Area: {rectangle.CalculateArea()}");
        Console.WriteLine($"Width: {rectangle.Width}, Height: {rectangle.Height}");
    }
}
```

**Problems with this approach:**
- Square changes the expected behavior of Rectangle
- Setting width affects height, which violates expectations
- Client code cannot safely substitute Square for Rectangle
- The "is-a" relationship is violated (Square is-a Rectangle conceptually, but not behaviorally)

## ✅ Good Example - Approach 1: Common Interface

```csharp
// Define a common interface that both shapes can implement
public interface IShape
{
    int CalculateArea();
    int CalculatePerimeter();
}

public class Rectangle : IShape
{
    public int Width { get; set; }
    public int Height { get; set; }
    
    public int CalculateArea()
    {
        return Width * Height;
    }
    
    public int CalculatePerimeter()
    {
        return 2 * (Width + Height);
    }
}

public class Square : IShape
{
    public int Side { get; set; }
    
    public int CalculateArea()
    {
        return Side * Side;
    }
    
    public int CalculatePerimeter()
    {
        return 4 * Side;
    }
}

// Client code works with the interface
public class ShapeCalculator
{
    public void CalculateShapeProperties(IShape shape)
    {
        Console.WriteLine($"Area: {shape.CalculateArea()}");
        Console.WriteLine($"Perimeter: {shape.CalculatePerimeter()}");
    }
}
```

## ✅ Good Example - Approach 2: Immutable Objects

```csharp
// Make objects immutable to avoid unexpected state changes
public abstract class Shape
{
    public abstract int CalculateArea();
    public abstract int CalculatePerimeter();
}

public class Rectangle : Shape
{
    public int Width { get; }
    public int Height { get; }
    
    public Rectangle(int width, int height)
    {
        Width = width;
        Height = height;
    }
    
    public override int CalculateArea()
    {
        return Width * Height;
    }
    
    public override int CalculatePerimeter()
    {
        return 2 * (Width + Height);
    }
    
    // Return new instance instead of modifying current one
    public Rectangle WithDimensions(int width, int height)
    {
        return new Rectangle(width, height);
    }
}

public class Square : Shape
{
    public int Side { get; }
    
    public Square(int side)
    {
        Side = side;
    }
    
    public override int CalculateArea()
    {
        return Side * Side;
    }
    
    public override int CalculatePerimeter()
    {
        return 4 * Side;
    }
    
    public Square WithSide(int side)
    {
        return new Square(side);
    }
}
```

## 🎯 Real-World Example: Bird Hierarchy

**Bad (Violates LSP):**
```csharp
public class Bird
{
    public virtual void Fly()
    {
        Console.WriteLine("Flying...");
    }
}

public class Duck : Bird
{
    public override void Fly()
    {
        Console.WriteLine("Duck flying...");
    }
}

public class Penguin : Bird
{
    public override void Fly()
    {
        throw new NotSupportedException("Penguins can't fly!"); // Violates LSP
    }
}

// This will work with Duck but throw an exception with Penguin
public class BirdWatcher
{
    public void WatchBird(Bird bird)
    {
        bird.Fly(); // Might throw exception if bird is a Penguin
    }
}
```

**Good (Follows LSP):**
```csharp
public abstract class Bird
{
    public abstract void Eat();
    public abstract void Move();
}

public interface IFlyable
{
    void Fly();
}

public interface ISwimmable
{
    void Swim();
}

public class Duck : Bird, IFlyable, ISwimmable
{
    public override void Eat()
    {
        Console.WriteLine("Duck eating...");
    }
    
    public override void Move()
    {
        Console.WriteLine("Duck moving...");
    }
    
    public void Fly()
    {
        Console.WriteLine("Duck flying...");
    }
    
    public void Swim()
    {
        Console.WriteLine("Duck swimming...");
    }
}

public class Penguin : Bird, ISwimmable
{
    public override void Eat()
    {
        Console.WriteLine("Penguin eating...");
    }
    
    public override void Move()
    {
        Console.WriteLine("Penguin waddling...");
    }
    
    public void Swim()
    {
        Console.WriteLine("Penguin swimming...");
    }
}

// Separate services for different capabilities
public class BirdWatcher
{
    public void WatchBird(Bird bird)
    {
        bird.Eat();
        bird.Move();
    }
    
    public void WatchFlyingBird(IFlyable flyingBird)
    {
        flyingBird.Fly();
    }
    
    public void WatchSwimmingBird(ISwimmable swimmingBird)
    {
        swimmingBird.Swim();
    }
}
```

## 🔧 Contract Rules for LSP

To follow LSP, derived classes must adhere to these contract rules:

### Preconditions
```csharp
public abstract class FileProcessor
{
    // Base class precondition: file must exist
    public virtual void ProcessFile(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException();
        
        DoProcessFile(filePath);
    }
    
    protected abstract void DoProcessFile(string filePath);
}

public class ImageProcessor : FileProcessor
{
    // ✅ Good: Can weaken preconditions (accept more inputs)
    public override void ProcessFile(string filePath)
    {
        // Can accept files that don't exist (will create them)
        DoProcessFile(filePath);
    }
    
    protected override void DoProcessFile(string filePath)
    {
        Console.WriteLine($"Processing image: {filePath}");
    }
}

public class ConfigProcessor : FileProcessor
{
    // ❌ Bad: Strengthens preconditions (accepts fewer inputs)
    public override void ProcessFile(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException();
        
        if (!filePath.EndsWith(".config"))
            throw new ArgumentException("Must be a config file"); // This violates LSP
        
        DoProcessFile(filePath);
    }
    
    protected override void DoProcessFile(string filePath)
    {
        Console.WriteLine($"Processing config: {filePath}");
    }
}
```

### Postconditions
```csharp
public abstract class Calculator
{
    // Base class postcondition: result must be positive
    public virtual double Calculate(double input)
    {
        var result = DoCalculate(input);
        if (result < 0)
            throw new InvalidOperationException("Result must be positive");
        return result;
    }
    
    protected abstract double DoCalculate(double input);
}

public class SquareCalculator : Calculator
{
    // ✅ Good: Strengthens postconditions (guarantees more)
    protected override double DoCalculate(double input)
    {
        var result = input * input;
        // Always returns a value >= 0, which is stronger than > 0
        return result;
    }
}

public class DifferenceCalculator : Calculator
{
    // ❌ Bad: Weakens postconditions (guarantees less)
    protected override double DoCalculate(double input)
    {
        // This can return negative values, violating the base class contract
        return input - 10;
    }
}
```

## 🔍 How to Identify LSP Violations

Look for these patterns:

1. **Exception throwing** in derived classes for base class methods
2. **Strengthened preconditions** in derived classes
3. **Weakened postconditions** in derived classes
4. **Type checking** before using polymorphic objects
5. **Empty or no-op implementations** in derived classes

## 💡 Design Strategies for LSP

### 1. Favor Composition over Inheritance
```csharp
// Instead of inheritance hierarchy that might violate LSP
public class Vehicle
{
    private readonly IEngine _engine;
    private readonly IMovement _movement;
    
    public Vehicle(IEngine engine, IMovement movement)
    {
        _engine = engine;
        _movement = movement;
    }
    
    public void Start() => _engine.Start();
    public void Move() => _movement.Move();
}

public interface IEngine
{
    void Start();
}

public interface IMovement
{
    void Move();
}
```

### 2. Use Generic Constraints
```csharp
public interface IProcessor<T> where T : class
{
    void Process(T item);
}

public class StringProcessor : IProcessor<string>
{
    public void Process(string item)
    {
        Console.WriteLine($"Processing string: {item}");
    }
}
```

## 🧪 Testing for LSP Compliance

```csharp
[TestFixture]
public class LSPComplianceTests
{
    [Test]
    public void AllShapes_ShouldCalculateAreaCorrectly()
    {
        // Arrange
        var shapes = new List<IShape>
        {
            new Rectangle(5, 4),
            new Square(3)
        };
        
        // Act & Assert
        foreach (var shape in shapes)
        {
            var area = shape.CalculateArea();
            Assert.IsTrue(area > 0, "Area should be positive for all shapes");
        }
    }
    
    [Test]
    public void AllBirds_ShouldEatAndMove()
    {
        // Arrange
        var birds = new List<Bird>
        {
            new Duck(),
            new Penguin()
        };
        
        // Act & Assert
        foreach (var bird in birds)
        {
            Assert.DoesNotThrow(() => bird.Eat());
            Assert.DoesNotThrow(() => bird.Move());
        }
    }
}
```

## ⚠️ Common Mistakes

1. **Is-a vs. Behaves-like-a**: Just because something "is-a" conceptually doesn't mean it should inherit
2. **Empty implementations**: Throwing `NotImplementedException` or doing nothing violates LSP
3. **Changing behavior drastically**: Derived classes should enhance, not completely change behavior
4. **Breaking contracts**: Violating preconditions and postconditions established by base class

## 📚 Next Steps

- Review the code examples in `src/3-LiskovSubstitution/`
- Practice designing inheritance hierarchies that respect LSP
- Move on to the [Interface Segregation Principle](04-interface-segregation.md)

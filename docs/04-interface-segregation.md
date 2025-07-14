# Interface Segregation Principle (ISP)

> "Clients should not be forced to depend upon interfaces that they do not use."
> 
> *- Robert C. Martin*

## 📖 Definition

The Interface Segregation Principle states that no client should be forced to depend on methods it does not use. This principle encourages the creation of smaller, more focused interfaces rather than large, monolithic ones. It's better to have many specific interfaces than one general-purpose interface.

## 🎯 Why is ISP Important?

1. **Reduced Coupling**: Clients only depend on what they actually need
2. **Better Maintainability**: Changes to unused methods don't affect clients
3. **Improved Testability**: Easier to create mocks and stubs for smaller interfaces
4. **Enhanced Flexibility**: Clients can implement only the interfaces they need
5. **Clearer Code**: Interfaces have focused, well-defined purposes

## ❌ Bad Example

```csharp
// This "fat" interface violates ISP - not all implementers need all methods
public interface IWorker
{
    void Work();
    void Eat();
    void Sleep();
    void TakeBreak();
    void AttendMeeting();
    void WriteCode();
    void TestCode();
    void DeployCode();
}

// Human worker can implement all methods
public class HumanWorker : IWorker
{
    public void Work() => Console.WriteLine("Human working...");
    public void Eat() => Console.WriteLine("Human eating...");
    public void Sleep() => Console.WriteLine("Human sleeping...");
    public void TakeBreak() => Console.WriteLine("Human taking break...");
    public void AttendMeeting() => Console.WriteLine("Human attending meeting...");
    public void WriteCode() => Console.WriteLine("Human writing code...");
    public void TestCode() => Console.WriteLine("Human testing code...");
    public void DeployCode() => Console.WriteLine("Human deploying code...");
}

// Robot worker is forced to implement methods it doesn't need
public class RobotWorker : IWorker
{
    public void Work() => Console.WriteLine("Robot working...");
    public void Eat() => throw new NotImplementedException("Robots don't eat!"); // Violates LSP too
    public void Sleep() => throw new NotImplementedException("Robots don't sleep!");
    public void TakeBreak() => throw new NotImplementedException("Robots don't take breaks!");
    public void AttendMeeting() => Console.WriteLine("Robot attending meeting...");
    public void WriteCode() => Console.WriteLine("Robot writing code...");
    public void TestCode() => Console.WriteLine("Robot testing code...");
    public void DeployCode() => Console.WriteLine("Robot deploying code...");
}

// Client is forced to know about methods it doesn't use
public class WorkManager
{
    public void ManageWork(IWorker worker)
    {
        worker.Work();
        // We only care about work, but we're forced to depend on the entire interface
    }
}
```

**Problems with this approach:**
- Robot is forced to implement methods it doesn't need
- Changes to eating/sleeping methods affect all implementers
- Clients depend on methods they don't use
- Interface is too broad and unfocused

## ✅ Good Example

```csharp
// Split the fat interface into smaller, focused interfaces
public interface IWorkable
{
    void Work();
}

public interface IEatable
{
    void Eat();
}

public interface ISleepable
{
    void Sleep();
}

public interface IBreakable
{
    void TakeBreak();
}

public interface IMeetingAttendee
{
    void AttendMeeting();
}

public interface IDeveloper
{
    void WriteCode();
    void TestCode();
    void DeployCode();
}

// Human worker implements only relevant interfaces
public class HumanWorker : IWorkable, IEatable, ISleepable, IBreakable, IMeetingAttendee, IDeveloper
{
    public void Work() => Console.WriteLine("Human working...");
    public void Eat() => Console.WriteLine("Human eating...");
    public void Sleep() => Console.WriteLine("Human sleeping...");
    public void TakeBreak() => Console.WriteLine("Human taking break...");
    public void AttendMeeting() => Console.WriteLine("Human attending meeting...");
    public void WriteCode() => Console.WriteLine("Human writing code...");
    public void TestCode() => Console.WriteLine("Human testing code...");
    public void DeployCode() => Console.WriteLine("Human deploying code...");
}

// Robot worker implements only what it can do
public class RobotWorker : IWorkable, IMeetingAttendee, IDeveloper
{
    public void Work() => Console.WriteLine("Robot working...");
    public void AttendMeeting() => Console.WriteLine("Robot attending meeting...");
    public void WriteCode() => Console.WriteLine("Robot writing code...");
    public void TestCode() => Console.WriteLine("Robot testing code...");
    public void DeployCode() => Console.WriteLine("Robot deploying code...");
}

// Clients depend only on what they need
public class WorkManager
{
    public void ManageWork(IWorkable worker)
    {
        worker.Work(); // Only depends on IWorkable
    }
}

public class DevelopmentManager
{
    public void ManageDevelopment(IDeveloper developer)
    {
        developer.WriteCode();
        developer.TestCode();
        developer.DeployCode();
    }
}

public class HRManager
{
    public void ManageHumanNeeds(IEatable eater, ISleepable sleeper, IBreakable breaker)
    {
        eater.Eat();
        sleeper.Sleep();
        breaker.TakeBreak();
    }
}
```

**Benefits of this approach:**
- Each interface has a single, focused responsibility
- Implementers only implement what they actually can do
- Clients depend only on what they need
- Changes to one interface don't affect unrelated code

## 🎯 Real-World Example: Document Processing

**Bad (Violates ISP):**
```csharp
public interface IDocument
{
    void Open();
    void Save();
    void Close();
    void Print();
    void Fax();
    void Email();
    void Scan();
    void Copy();
    void Encrypt();
    void Compress();
    void ConvertToPdf();
    void ConvertToWord();
}

// Simple text editor doesn't need all these features
public class TextEditor : IDocument
{
    public void Open() => Console.WriteLine("Opening text file...");
    public void Save() => Console.WriteLine("Saving text file...");
    public void Close() => Console.WriteLine("Closing text file...");
    public void Print() => Console.WriteLine("Printing text file...");
    
    // Forced to implement methods it doesn't support
    public void Fax() => throw new NotSupportedException();
    public void Email() => throw new NotSupportedException();
    public void Scan() => throw new NotSupportedException();
    public void Copy() => throw new NotSupportedException();
    public void Encrypt() => throw new NotSupportedException();
    public void Compress() => throw new NotSupportedException();
    public void ConvertToPdf() => throw new NotSupportedException();
    public void ConvertToWord() => throw new NotSupportedException();
}
```

**Good (Follows ISP):**
```csharp
// Core document operations
public interface IDocument
{
    void Open();
    void Save();
    void Close();
}

// Optional capabilities as separate interfaces
public interface IPrintable
{
    void Print();
}

public interface IFaxable
{
    void Fax();
}

public interface IEmailable
{
    void Email();
}

public interface IScannable
{
    void Scan();
}

public interface ICopyable
{
    void Copy();
}

public interface IEncryptable
{
    void Encrypt();
}

public interface ICompressible
{
    void Compress();
}

public interface IPdfConvertible
{
    void ConvertToPdf();
}

public interface IWordConvertible
{
    void ConvertToWord();
}

// Simple text editor implements only what it supports
public class TextEditor : IDocument, IPrintable, IPdfConvertible
{
    public void Open() => Console.WriteLine("Opening text file...");
    public void Save() => Console.WriteLine("Saving text file...");
    public void Close() => Console.WriteLine("Closing text file...");
    public void Print() => Console.WriteLine("Printing text file...");
    public void ConvertToPdf() => Console.WriteLine("Converting to PDF...");
}

// Advanced document processor implements many capabilities
public class AdvancedDocumentProcessor : IDocument, IPrintable, IFaxable, IEmailable, 
    IScannable, ICopyable, IEncryptable, ICompressible, IPdfConvertible, IWordConvertible
{
    public void Open() => Console.WriteLine("Opening advanced document...");
    public void Save() => Console.WriteLine("Saving advanced document...");
    public void Close() => Console.WriteLine("Closing advanced document...");
    public void Print() => Console.WriteLine("Printing advanced document...");
    public void Fax() => Console.WriteLine("Faxing document...");
    public void Email() => Console.WriteLine("Emailing document...");
    public void Scan() => Console.WriteLine("Scanning document...");
    public void Copy() => Console.WriteLine("Copying document...");
    public void Encrypt() => Console.WriteLine("Encrypting document...");
    public void Compress() => Console.WriteLine("Compressing document...");
    public void ConvertToPdf() => Console.WriteLine("Converting to PDF...");
    public void ConvertToWord() => Console.WriteLine("Converting to Word...");
}

// Clients depend only on what they need
public class DocumentManager
{
    public void BasicDocumentOperations(IDocument document)
    {
        document.Open();
        document.Save();
        document.Close();
    }
    
    public void PrintDocument(IPrintable printable)
    {
        printable.Print();
    }
    
    public void EmailDocument(IEmailable emailable)
    {
        emailable.Email();
    }
}
```

## 🔧 Interface Composition Patterns

### Role Interfaces
```csharp
// Small, role-based interfaces
public interface IReader
{
    string Read();
}

public interface IWriter
{
    void Write(string content);
}

public interface IValidator
{
    bool IsValid(string content);
}

// Compose interfaces for specific use cases
public interface IFileProcessor : IReader, IWriter, IValidator
{
    // Inherits Read(), Write(), and IsValid()
}

// Or use specific combinations
public class ReadOnlyFileProcessor : IReader, IValidator
{
    public string Read() => "File content";
    public bool IsValid(string content) => !string.IsNullOrEmpty(content);
}

public class WriteOnlyFileProcessor : IWriter, IValidator
{
    public void Write(string content) => Console.WriteLine($"Writing: {content}");
    public bool IsValid(string content) => !string.IsNullOrEmpty(content);
}
```

### Capability Interfaces
```csharp
public interface IDisposable
{
    void Dispose();
}

public interface IConfigurable
{
    void Configure(Dictionary<string, object> settings);
}

public interface ILoggable
{
    void Log(string message);
}

// Services can implement multiple capabilities
public class DatabaseService : IDisposable, IConfigurable, ILoggable
{
    public void Dispose() => Console.WriteLine("Disposing database connection");
    public void Configure(Dictionary<string, object> settings) => Console.WriteLine("Configuring database");
    public void Log(string message) => Console.WriteLine($"DB Log: {message}");
}
```

## 🔍 How to Identify ISP Violations

Look for these patterns:

1. **Large interfaces** with many unrelated methods
2. **NotImplementedException** in interface implementations
3. **Empty implementations** that do nothing
4. **Type checking** before calling interface methods
5. **Comments like "not applicable"** in implementations

## 💡 Strategies for Interface Design

### 1. Start Small and Grow
```csharp
// Start with minimal interface
public interface IBasicCache
{
    void Set(string key, object value);
    T Get<T>(string key);
}

// Add capabilities as needed
public interface IAdvancedCache : IBasicCache
{
    void Remove(string key);
    void Clear();
    bool Contains(string key);
}

public interface IDistributedCache : IAdvancedCache
{
    void SetWithExpiry(string key, object value, TimeSpan expiry);
    Task<T> GetAsync<T>(string key);
}
```

### 2. Use Command Pattern
```csharp
public interface ICommand
{
    void Execute();
}

public interface IUndoableCommand : ICommand
{
    void Undo();
}

public class SaveCommand : IUndoableCommand
{
    public void Execute() => Console.WriteLine("Saving...");
    public void Undo() => Console.WriteLine("Undoing save...");
}

public class PrintCommand : ICommand
{
    public void Execute() => Console.WriteLine("Printing...");
    // No undo needed for printing
}
```

## 🧪 Testing with ISP

```csharp
[TestFixture]
public class InterfaceSegregationTests
{
    [Test]
    public void TextEditor_ShouldImplementOnlyNeededInterfaces()
    {
        // Arrange
        var editor = new TextEditor();
        
        // Act & Assert
        Assert.IsInstanceOf<IDocument>(editor);
        Assert.IsInstanceOf<IPrintable>(editor);
        Assert.IsInstanceOf<IPdfConvertible>(editor);
        
        // Should not implement interfaces it doesn't need
        Assert.IsNotInstanceOf<IFaxable>(editor);
        Assert.IsNotInstanceOf<IScannable>(editor);
    }
    
    [Test]
    public void DocumentManager_ShouldWorkWithSpecificCapabilities()
    {
        // Arrange
        var manager = new DocumentManager();
        var editor = new TextEditor();
        
        // Act & Assert
        Assert.DoesNotThrow(() => manager.BasicDocumentOperations(editor));
        Assert.DoesNotThrow(() => manager.PrintDocument(editor));
        
        // This would not compile - editor doesn't implement IFaxable
        // manager.FaxDocument(editor); // Compilation error
    }
}
```

## ⚠️ Balancing ISP with Practicality

### When to Group Methods Together
```csharp
// These methods are closely related and often used together
public interface IRepository<T>
{
    void Add(T entity);      // These operations are
    void Update(T entity);   // closely related and
    void Delete(T entity);   // often used together
    T GetById(int id);
}

// But separate query operations if they're optional
public interface IQueryableRepository<T> : IRepository<T>
{
    IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
    IEnumerable<T> GetAll();
    int Count();
}
```

### Avoiding Over-Segregation
```csharp
// Don't go too far with segregation
// ❌ Too granular
public interface IAdder { void Add(T item); }
public interface IRemover { void Remove(T item); }
public interface ICounter { int Count { get; } }

// ✅ Reasonable grouping
public interface ICollection<T>
{
    void Add(T item);
    void Remove(T item);
    int Count { get; }
}
```

## 📚 Next Steps

- Review the code examples in `src/4-InterfaceSegregation/`
- Practice breaking down large interfaces in existing code
- Move on to the [Dependency Inversion Principle](05-dependency-inversion.md)

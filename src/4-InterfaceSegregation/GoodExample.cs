// Interface Segregation Principle - GOOD Example
// This code follows ISP by having small, focused interfaces that clients can implement as needed

using System;
using System.Collections.Generic;
using System.Linq;

namespace SolidPrinciples.InterfaceSegregation.Good
{
    // Small, focused interfaces instead of one large interface
    
    // Core work interface
    public interface IWorkable
    {
        void Work();
        void TakeBreak();
    }
    
    // Human-specific capabilities
    public interface IHuman
    {
        void Eat();
        void Sleep();
    }
    
    public interface IMeetingAttendee
    {
        void AttendMeeting();
        void ScheduleMeeting(DateTime dateTime, string topic);
    }
    
    // Development capabilities
    public interface IDeveloper
    {
        void WriteCode();
        void TestCode();
        void ReviewCode();
    }
    
    public interface IDeploymentCapable
    {
        void DeployCode();
        void RollbackDeployment();
    }
    
    // Management capabilities
    public interface IManager
    {
        void HireEmployee();
        void ConductPerformanceReview();
    }
    
    public interface ITeamLead
    {
        void AssignTasks();
        void MonitorProgress();
    }
    
    // Robot-specific capabilities
    public interface IRobot
    {
        void Recharge();
        void RunDiagnostics();
        void UpdateFirmware();
    }
    
    public interface IMaintainable
    {
        void PerformMaintenance();
        void ReplaceComponent(string componentName);
    }
    
    // Communication capabilities
    public interface IEmailCapable
    {
        void SendEmail(string to, string subject, string body);
    }
    
    public interface INotifiable
    {
        void SendNotification(string message);
    }
    
    // Implementations that only implement what they need
    
    public class HumanDeveloper : IWorkable, IHuman, IMeetingAttendee, IDeveloper, IDeploymentCapable, IEmailCapable
    {
        public string Name { get; }
        
        public HumanDeveloper(string name)
        {
            Name = name;
        }
        
        // IWorkable
        public void Work()
        {
            Console.WriteLine($"{Name} is working on development tasks...");
        }
        
        public void TakeBreak()
        {
            Console.WriteLine($"{Name} is taking a coffee break...");
        }
        
        // IHuman
        public void Eat()
        {
            Console.WriteLine($"{Name} is eating lunch...");
        }
        
        public void Sleep()
        {
            Console.WriteLine($"{Name} is sleeping...");
        }
        
        // IMeetingAttendee
        public void AttendMeeting()
        {
            Console.WriteLine($"{Name} is attending the meeting...");
        }
        
        public void ScheduleMeeting(DateTime dateTime, string topic)
        {
            Console.WriteLine($"{Name} scheduled a meeting for {dateTime}: {topic}");
        }
        
        // IDeveloper
        public void WriteCode()
        {
            Console.WriteLine($"{Name} is writing elegant code...");
        }
        
        public void TestCode()
        {
            Console.WriteLine($"{Name} is writing and running tests...");
        }
        
        public void ReviewCode()
        {
            Console.WriteLine($"{Name} is reviewing pull requests...");
        }
        
        // IDeploymentCapable
        public void DeployCode()
        {
            Console.WriteLine($"{Name} is deploying to production...");
        }
        
        public void RollbackDeployment()
        {
            Console.WriteLine($"{Name} is rolling back the deployment...");
        }
        
        // IEmailCapable
        public void SendEmail(string to, string subject, string body)
        {
            Console.WriteLine($"{Name} sent email to {to}: {subject}");
        }
    }
    
    public class RobotWorker : IWorkable, IRobot, IDeveloper, IDeploymentCapable, IMaintainable, INotifiable
    {
        public string Model { get; }
        
        public RobotWorker(string model)
        {
            Model = model;
        }
        
        // IWorkable
        public void Work()
        {
            Console.WriteLine($"Robot {Model} is executing assigned tasks...");
        }
        
        public void TakeBreak()
        {
            Console.WriteLine($"Robot {Model} entering standby mode...");
        }
        
        // IRobot
        public void Recharge()
        {
            Console.WriteLine($"Robot {Model} is recharging battery (85% complete)...");
        }
        
        public void RunDiagnostics()
        {
            Console.WriteLine($"Robot {Model} running system diagnostics... All systems nominal.");
        }
        
        public void UpdateFirmware()
        {
            Console.WriteLine($"Robot {Model} updating firmware to latest version...");
        }
        
        // IDeveloper
        public void WriteCode()
        {
            Console.WriteLine($"Robot {Model} is generating optimized code...");
        }
        
        public void TestCode()
        {
            Console.WriteLine($"Robot {Model} is running comprehensive automated tests...");
        }
        
        public void ReviewCode()
        {
            Console.WriteLine($"Robot {Model} is analyzing code for bugs and optimizations...");
        }
        
        // IDeploymentCapable
        public void DeployCode()
        {
            Console.WriteLine($"Robot {Model} is deploying with zero-downtime strategy...");
        }
        
        public void RollbackDeployment()
        {
            Console.WriteLine($"Robot {Model} is performing automatic rollback...");
        }
        
        // IMaintainable
        public void PerformMaintenance()
        {
            Console.WriteLine($"Robot {Model} is performing self-maintenance routines...");
        }
        
        public void ReplaceComponent(string componentName)
        {
            Console.WriteLine($"Robot {Model} is replacing {componentName}...");
        }
        
        // INotifiable
        public void SendNotification(string message)
        {
            Console.WriteLine($"Robot {Model} sending system notification: {message}");
        }
    }
    
    public class ProjectManager : IWorkable, IHuman, IMeetingAttendee, IManager, ITeamLead, IEmailCapable
    {
        public string Name { get; }
        
        public ProjectManager(string name)
        {
            Name = name;
        }
        
        // IWorkable
        public void Work()
        {
            Console.WriteLine($"Manager {Name} is coordinating project activities...");
        }
        
        public void TakeBreak()
        {
            Console.WriteLine($"Manager {Name} is taking a strategic thinking break...");
        }
        
        // IHuman
        public void Eat()
        {
            Console.WriteLine($"Manager {Name} is having a working lunch...");
        }
        
        public void Sleep()
        {
            Console.WriteLine($"Manager {Name} is getting rest for tomorrow's challenges...");
        }
        
        // IMeetingAttendee
        public void AttendMeeting()
        {
            Console.WriteLine($"Manager {Name} is facilitating the team meeting...");
        }
        
        public void ScheduleMeeting(DateTime dateTime, string topic)
        {
            Console.WriteLine($"Manager {Name} scheduled team meeting for {dateTime}: {topic}");
        }
        
        // IManager
        public void HireEmployee()
        {
            Console.WriteLine($"Manager {Name} is interviewing and hiring new talent...");
        }
        
        public void ConductPerformanceReview()
        {
            Console.WriteLine($"Manager {Name} is conducting quarterly performance reviews...");
        }
        
        // ITeamLead
        public void AssignTasks()
        {
            Console.WriteLine($"Manager {Name} is distributing tasks based on team strengths...");
        }
        
        public void MonitorProgress()
        {
            Console.WriteLine($"Manager {Name} is tracking project milestones and team progress...");
        }
        
        // IEmailCapable
        public void SendEmail(string to, string subject, string body)
        {
            Console.WriteLine($"Manager {Name} sent strategic email to {to}: {subject}");
        }
    }
    
    public class TechnicalLead : IWorkable, IHuman, IMeetingAttendee, IDeveloper, IDeploymentCapable, ITeamLead, IEmailCapable
    {
        public string Name { get; }
        
        public TechnicalLead(string name)
        {
            Name = name;
        }
        
        // IWorkable
        public void Work()
        {
            Console.WriteLine($"Tech Lead {Name} is architecting solutions and mentoring team...");
        }
        
        public void TakeBreak()
        {
            Console.WriteLine($"Tech Lead {Name} is taking a brief break from code reviews...");
        }
        
        // IHuman
        public void Eat()
        {
            Console.WriteLine($"Tech Lead {Name} is grabbing a quick bite while debugging...");
        }
        
        public void Sleep()
        {
            Console.WriteLine($"Tech Lead {Name} is getting rest (finally!)...");
        }
        
        // IMeetingAttendee
        public void AttendMeeting()
        {
            Console.WriteLine($"Tech Lead {Name} is presenting technical solutions...");
        }
        
        public void ScheduleMeeting(DateTime dateTime, string topic)
        {
            Console.WriteLine($"Tech Lead {Name} scheduled architecture review for {dateTime}: {topic}");
        }
        
        // IDeveloper
        public void WriteCode()
        {
            Console.WriteLine($"Tech Lead {Name} is writing critical system components...");
        }
        
        public void TestCode()
        {
            Console.WriteLine($"Tech Lead {Name} is designing comprehensive test strategies...");
        }
        
        public void ReviewCode()
        {
            Console.WriteLine($"Tech Lead {Name} is conducting thorough code reviews...");
        }
        
        // IDeploymentCapable
        public void DeployCode()
        {
            Console.WriteLine($"Tech Lead {Name} is overseeing production deployment...");
        }
        
        public void RollbackDeployment()
        {
            Console.WriteLine($"Tech Lead {Name} is executing emergency rollback procedures...");
        }
        
        // ITeamLead
        public void AssignTasks()
        {
            Console.WriteLine($"Tech Lead {Name} is assigning tasks based on technical complexity...");
        }
        
        public void MonitorProgress()
        {
            Console.WriteLine($"Tech Lead {Name} is tracking technical debt and code quality...");
        }
        
        // IEmailCapable
        public void SendEmail(string to, string subject, string body)
        {
            Console.WriteLine($"Tech Lead {Name} sent technical update to {to}: {subject}");
        }
    }
    
    // Document system with segregated interfaces
    
    // Core document operations
    public interface IDocument
    {
        void Open();
        void Save();
        void Close();
        string GetContent();
    }
    
    // Optional document capabilities
    public interface IPrintable
    {
        void Print();
        void PrintPreview();
    }
    
    public interface IEmailableDocument
    {
        void EmailDocument(string to, string subject);
    }
    
    public interface ISecureDocument
    {
        void Encrypt(string password);
        void Decrypt(string password);
        void AddDigitalSignature();
    }
    
    public interface IConvertibleDocument
    {
        void ConvertToPdf();
        void ConvertToWord();
    }
    
    public interface ICollaborativeDocument
    {
        void ShareWithTeam(List<string> teamMembers);
        void AddComment(string comment, string author);
        void TrackChanges();
    }
    
    public interface IBackupCapable
    {
        void Backup();
        void Restore(DateTime backupDate);
    }
    
    // Document implementations
    
    public class SimpleTextEditor : IDocument, IPrintable
    {
        private string _content = string.Empty;
        
        // IDocument
        public void Open()
        {
            Console.WriteLine("📄 Opening text file...");
            _content = "Sample text content loaded";
        }
        
        public void Save()
        {
            Console.WriteLine("💾 Saving text file...");
        }
        
        public void Close()
        {
            Console.WriteLine("❌ Closing text file...");
        }
        
        public string GetContent()
        {
            return _content;
        }
        
        // IPrintable
        public void Print()
        {
            Console.WriteLine("🖨️ Printing simple text document...");
        }
        
        public void PrintPreview()
        {
            Console.WriteLine("👁️ Showing print preview of text document...");
        }
    }
    
    public class AdvancedWordProcessor : IDocument, IPrintable, IEmailableDocument, 
                                        ISecureDocument, IConvertibleDocument, ICollaborativeDocument, IBackupCapable
    {
        private string _content = string.Empty;
        private bool _isEncrypted = false;
        
        // IDocument
        public void Open()
        {
            Console.WriteLine("📄 Opening advanced document...");
            _content = "Rich document content with formatting";
        }
        
        public void Save()
        {
            Console.WriteLine("💾 Saving document with all formatting...");
        }
        
        public void Close()
        {
            Console.WriteLine("❌ Closing advanced document...");
        }
        
        public string GetContent()
        {
            return _isEncrypted ? "[ENCRYPTED CONTENT]" : _content;
        }
        
        // IPrintable
        public void Print()
        {
            Console.WriteLine("🖨️ Printing formatted document with headers and footers...");
        }
        
        public void PrintPreview()
        {
            Console.WriteLine("👁️ Showing rich print preview with layout...");
        }
        
        // IEmailableDocument
        public void EmailDocument(string to, string subject)
        {
            Console.WriteLine($"📧 Emailing document to {to} with subject: {subject}");
        }
        
        // ISecureDocument
        public void Encrypt(string password)
        {
            Console.WriteLine("🔒 Encrypting document with AES-256...");
            _isEncrypted = true;
        }
        
        public void Decrypt(string password)
        {
            Console.WriteLine("🔓 Decrypting document...");
            _isEncrypted = false;
        }
        
        public void AddDigitalSignature()
        {
            Console.WriteLine("✍️ Adding digital signature for authenticity...");
        }
        
        // IConvertibleDocument
        public void ConvertToPdf()
        {
            Console.WriteLine("📑 Converting to PDF with embedded fonts...");
        }
        
        public void ConvertToWord()
        {
            Console.WriteLine("📝 Exporting to Word format...");
        }
        
        // ICollaborativeDocument
        public void ShareWithTeam(List<string> teamMembers)
        {
            Console.WriteLine($"👥 Sharing document with team: {string.Join(", ", teamMembers)}");
        }
        
        public void AddComment(string comment, string author)
        {
            Console.WriteLine($"💬 Added comment by {author}: {comment}");
        }
        
        public void TrackChanges()
        {
            Console.WriteLine("📝 Enabling change tracking for collaboration...");
        }
        
        // IBackupCapable
        public void Backup()
        {
            Console.WriteLine("💾 Creating versioned backup...");
        }
        
        public void Restore(DateTime backupDate)
        {
            Console.WriteLine($"⏮️ Restoring from backup dated {backupDate:yyyy-MM-dd}");
        }
    }
    
    public class PdfViewer : IDocument, IPrintable, IEmailableDocument
    {
        // IDocument
        public void Open()
        {
            Console.WriteLine("📄 Opening PDF document...");
        }
        
        public void Save()
        {
            Console.WriteLine("💾 PDF is read-only, creating copy...");
        }
        
        public void Close()
        {
            Console.WriteLine("❌ Closing PDF viewer...");
        }
        
        public string GetContent()
        {
            return "PDF content (read-only)";
        }
        
        // IPrintable
        public void Print()
        {
            Console.WriteLine("🖨️ Printing PDF with high fidelity...");
        }
        
        public void PrintPreview()
        {
            Console.WriteLine("👁️ Showing PDF print preview...");
        }
        
        // IEmailableDocument
        public void EmailDocument(string to, string subject)
        {
            Console.WriteLine($"📧 Emailing PDF attachment to {to}: {subject}");
        }
    }
    
    // Service classes that work with segregated interfaces
    
    public class WorkforceManager
    {
        public void ManageBasicWork(IEnumerable<IWorkable> workers)
        {
            Console.WriteLine("\n👔 Managing basic work activities:");
            foreach (var worker in workers)
            {
                worker.Work();
                worker.TakeBreak();
            }
        }
        
        public void OrganizeMeetings(IEnumerable<IMeetingAttendee> attendees, DateTime dateTime, string topic)
        {
            Console.WriteLine($"\n📅 Organizing meeting for {dateTime}: {topic}");
            foreach (var attendee in attendees)
            {
                attendee.ScheduleMeeting(dateTime, topic);
            }
        }
        
        public void ManageDevelopmentTeam(IEnumerable<IDeveloper> developers)
        {
            Console.WriteLine("\n💻 Managing development activities:");
            foreach (var developer in developers)
            {
                developer.WriteCode();
                developer.TestCode();
                developer.ReviewCode();
            }
        }
        
        public void HandleDeployments(IEnumerable<IDeploymentCapable> deployers)
        {
            Console.WriteLine("\n🚀 Handling deployment activities:");
            foreach (var deployer in deployers)
            {
                deployer.DeployCode();
            }
        }
        
        public void MaintainRobots(IEnumerable<IRobot> robots)
        {
            Console.WriteLine("\n🤖 Maintaining robot workforce:");
            foreach (var robot in robots)
            {
                robot.RunDiagnostics();
                robot.Recharge();
            }
        }
    }
    
    public class DocumentManager
    {
        public void ProcessBasicDocuments(IEnumerable<IDocument> documents)
        {
            Console.WriteLine("\n📁 Processing basic document operations:");
            foreach (var doc in documents)
            {
                doc.Open();
                Console.WriteLine($"Content: {doc.GetContent()}");
                doc.Save();
                doc.Close();
            }
        }
        
        public void PrintDocuments(IEnumerable<IPrintable> printableDocuments)
        {
            Console.WriteLine("\n🖨️ Printing documents:");
            foreach (var doc in printableDocuments)
            {
                doc.PrintPreview();
                doc.Print();
            }
        }
        
        public void SecureDocuments(IEnumerable<ISecureDocument> secureDocuments, string password)
        {
            Console.WriteLine("\n🔒 Securing sensitive documents:");
            foreach (var doc in secureDocuments)
            {
                doc.Encrypt(password);
                doc.AddDigitalSignature();
            }
        }
        
        public void ShareDocuments(IEnumerable<ICollaborativeDocument> collaborativeDocs, List<string> team)
        {
            Console.WriteLine("\n👥 Sharing documents with team:");
            foreach (var doc in collaborativeDocs)
            {
                doc.ShareWithTeam(team);
                doc.TrackChanges();
            }
        }
    }
    
    // Usage examples demonstrating the benefits
    public class ISPComplianceExamples
    {
        public static void DemonstrateWorkforceManagement()
        {
            Console.WriteLine("=== Workforce Management (ISP Compliant) ===");
            
            var workforce = new List<object>
            {
                new HumanDeveloper("Alice"),
                new RobotWorker("R2D2"),
                new ProjectManager("Bob"),
                new TechnicalLead("Carol")
            };
            
            var workforceManager = new WorkforceManager();
            
            // All workers can do basic work
            var allWorkers = workforce.OfType<IWorkable>();
            workforceManager.ManageBasicWork(allWorkers);
            
            // Only meeting attendees participate in meetings
            var meetingAttendees = workforce.OfType<IMeetingAttendee>();
            workforceManager.OrganizeMeetings(meetingAttendees, DateTime.Now.AddDays(1), "Sprint Planning");
            
            // Only developers do development work
            var developers = workforce.OfType<IDeveloper>();
            workforceManager.ManageDevelopmentTeam(developers);
            
            // Only deployment-capable entities handle deployments
            var deployers = workforce.OfType<IDeploymentCapable>();
            workforceManager.HandleDeployments(deployers);
            
            // Only robots need maintenance
            var robots = workforce.OfType<IRobot>();
            workforceManager.MaintainRobots(robots);
            
            Console.WriteLine("\n✅ Each interface is used only by relevant implementations");
        }
        
        public static void DemonstrateDocumentManagement()
        {
            Console.WriteLine("\n=== Document Management (ISP Compliant) ===");
            
            var documents = new List<IDocument>
            {
                new SimpleTextEditor(),
                new AdvancedWordProcessor(),
                new PdfViewer()
            };
            
            var documentManager = new DocumentManager();
            
            // All documents support basic operations
            documentManager.ProcessBasicDocuments(documents);
            
            // Only printable documents can be printed
            var printableDocuments = documents.OfType<IPrintable>();
            documentManager.PrintDocuments(printableDocuments);
            
            // Only secure documents can be encrypted
            var secureDocuments = documents.OfType<ISecureDocument>();
            documentManager.SecureDocuments(secureDocuments, "SecurePassword123");
            
            // Only collaborative documents support team sharing
            var collaborativeDocs = documents.OfType<ICollaborativeDocument>();
            var team = new List<string> { "Alice", "Bob", "Carol" };
            documentManager.ShareDocuments(collaborativeDocs, team);
            
            Console.WriteLine("\n✅ Each document type implements only relevant capabilities");
        }
        
        public static void DemonstrateFlexibleComposition()
        {
            Console.WriteLine("\n=== Flexible Interface Composition ===");
            
            // Show how different combinations of interfaces provide different capabilities
            var techLead = new TechnicalLead("David");
            
            Console.WriteLine("Tech Lead capabilities:");
            if (techLead is IWorkable worker)
                Console.WriteLine("✓ Can work and take breaks");
            if (techLead is IDeveloper developer)
                Console.WriteLine("✓ Can write, test, and review code");
            if (techLead is ITeamLead teamLead)
                Console.WriteLine("✓ Can assign tasks and monitor progress");
            if (techLead is IDeploymentCapable deployer)
                Console.WriteLine("✓ Can deploy and rollback code");
            if (techLead is IMeetingAttendee attendee)
                Console.WriteLine("✓ Can attend and schedule meetings");
            if (techLead is IEmailCapable emailer)
                Console.WriteLine("✓ Can send emails");
            
            // Robot has different combination
            var robot = new RobotWorker("C3PO");
            Console.WriteLine("\nRobot capabilities:");
            if (robot is IWorkable)
                Console.WriteLine("✓ Can work and take breaks");
            if (robot is IDeveloper)
                Console.WriteLine("✓ Can write, test, and review code");
            if (robot is IRobot)
                Console.WriteLine("✓ Can recharge and run diagnostics");
            if (robot is IMaintainable)
                Console.WriteLine("✓ Can perform maintenance");
            if (robot is INotifiable)
                Console.WriteLine("✓ Can send notifications");
            
            Console.WriteLine("\n✅ Each role has exactly the interfaces it needs");
        }
        
        public static void DemonstrateAllBenefits()
        {
            Console.WriteLine("=== GOOD Example: Following Interface Segregation Principle ===\n");
            
            DemonstrateWorkforceManagement();
            DemonstrateDocumentManagement();
            DemonstrateFlexibleComposition();
            
            Console.WriteLine("\n📋 Benefits of Interface Segregation:");
            Console.WriteLine("✅ Classes implement only the interfaces they need");
            Console.WriteLine("✅ No NotSupportedException - all methods are meaningful");
            Console.WriteLine("✅ High cohesion - related methods are grouped together");
            Console.WriteLine("✅ Low coupling - changes to one interface don't affect others");
            Console.WriteLine("✅ Easy to test - small, focused interfaces are easy to mock");
            Console.WriteLine("✅ Flexible composition - mix and match capabilities as needed");
            Console.WriteLine("✅ Clear contracts - each interface has a single, well-defined purpose");
            Console.WriteLine("✅ Easy to extend - add new capabilities without changing existing code");
        }
    }
}

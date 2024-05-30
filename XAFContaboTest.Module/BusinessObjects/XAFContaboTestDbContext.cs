using DevExpress.ExpressApp.EFCore.Updating;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.ExpressApp.Design;
using DevExpress.ExpressApp.EFCore.DesignTime;
using DevExpress.Persistent.BaseImpl.EF.StateMachine;

namespace XAFContaboTest.Module.BusinessObjects;

// This code allows our Model Editor to get relevant EF Core metadata at design time.
// For details, please refer to https://supportcenter.devexpress.com/ticket/details/t933891.
public class XAFContaboTestContextInitializer : DbContextTypesInfoInitializerBase {
	protected override DbContext CreateDbContext() {
		var optionsBuilder = new DbContextOptionsBuilder<XAFContaboTestEFCoreDbContext>()
            .UseSqlServer(";")
            .UseChangeTrackingProxies()
            .UseObjectSpaceLinkProxies();
        return new XAFContaboTestEFCoreDbContext(optionsBuilder.Options);
	}
}
//This factory creates DbContext for design-time services. For example, it is required for database migration.
public class XAFContaboTestDesignTimeDbContextFactory : IDesignTimeDbContextFactory<XAFContaboTestEFCoreDbContext> {
	public XAFContaboTestEFCoreDbContext CreateDbContext(string[] args) {
		throw new InvalidOperationException("Make sure that the database connection string and connection provider are correct. After that, uncomment the code below and remove this exception.");
		//var optionsBuilder = new DbContextOptionsBuilder<XAFContaboTestEFCoreDbContext>();
		//optionsBuilder.UseSqlServer("Integrated Security=SSPI;Data Source=(localdb)\\mssqllocaldb;Initial Catalog=XAFContaboTest");
        //optionsBuilder.UseChangeTrackingProxies();
        //optionsBuilder.UseObjectSpaceLinkProxies();
		//return new XAFContaboTestEFCoreDbContext(optionsBuilder.Options);
	}
}
[TypesInfoInitializer(typeof(XAFContaboTestContextInitializer))]
public class XAFContaboTestEFCoreDbContext : DbContext {
	public XAFContaboTestEFCoreDbContext(DbContextOptions<XAFContaboTestEFCoreDbContext> options) : base(options) {
	}
	//public DbSet<ModuleInfo> ModulesInfo { get; set; }
    public DbSet<StateMachine> StateMachines { get; set; }
    public DbSet<StateMachineState> StateMachineStates { get; set; }
    public DbSet<StateMachineTransition> StateMachineTransitions { get; set; }
    public DbSet<StateMachineAppearance> StateMachineAppearances { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangingAndChangedNotificationsWithOriginalValues);
        modelBuilder.UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction);
        modelBuilder.Entity<StateMachine>()
            .HasMany(t => t.States)
            .WithOne(t => t.StateMachine)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

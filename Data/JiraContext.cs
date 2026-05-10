using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JiraMinimal.Data;

public class JiraContext : IdentityDbContext<ApplicationUser>
{
    public JiraContext(DbContextOptions<JiraContext> options) : base(options)
    {
    }

    // Таблицы (DbSet)
    public DbSet<Department> Departments { get; set; } = null!;
    public DbSet<UserDepartment> UserDepartments { get; set; } = null!;
    public DbSet<Project> Projects { get; set; } = null!;
    public DbSet<IssueStatus> IssueStatuses { get; set; } = null!;
    public DbSet<IssuePriority> IssuePriorities { get; set; } = null!;
    public DbSet<Issue> Issues { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<Notification> Notifications { get; set; } = null!;
    public DbSet<DepartmentWorkflow> DepartmentWorkflows { get; set; } = null!;
    public DbSet<IssueDepartmentTransfer> IssueDepartmentTransfers { get; set; } = null!;
    public DbSet<IssueHistory> IssueHistories { get; set; } = null!;

   protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // Связь: Пользователь -> Назначенные задачи (исполнитель)
    modelBuilder.Entity<ApplicationUser>()
        .HasMany(u => u.AssignedIssues)
        .WithOne(i => i.Assignee)
        .HasForeignKey(i => i.AssigneeId)
        .OnDelete(DeleteBehavior.Restrict);  // не удалять задачи при удалении пользователя

    // Связь: Пользователь -> Созданные задачи (автор)
    modelBuilder.Entity<ApplicationUser>()
        .HasMany(u => u.ReportedIssues)
        .WithOne(i => i.Reporter)
        .HasForeignKey(i => i.ReporterId)
        .OnDelete(DeleteBehavior.Restrict);

    // Связь: Задача -> родительская задача (самореференция)
    modelBuilder.Entity<Issue>()
        .HasOne(i => i.ParentIssue)
        .WithMany(i => i.ChildIssues)
        .HasForeignKey(i => i.ParentIssueId)
        .OnDelete(DeleteBehavior.Restrict);

    // Связь: Отдел -> Руководитель (пользователь)
    modelBuilder.Entity<Department>()
        .HasOne(d => d.Head)
        .WithMany()
        .HasForeignKey(d => d.HeadUserId)
        .OnDelete(DeleteBehavior.SetNull);

    // Связь: DepartmentWorkflow -> статус источника
    modelBuilder.Entity<DepartmentWorkflow>()
        .HasOne(w => w.FromStatus)
        .WithMany(s => s.FromStatusWorkflows)
        .HasForeignKey(w => w.FromStatusId)
        .OnDelete(DeleteBehavior.Restrict);

    // Связь: DepartmentWorkflow -> статус назначения
    modelBuilder.Entity<DepartmentWorkflow>()
        .HasOne(w => w.ToStatus)
        .WithMany(s => s.ToStatusWorkflows)
        .HasForeignKey(w => w.ToStatusId)
        .OnDelete(DeleteBehavior.Restrict);
}
}
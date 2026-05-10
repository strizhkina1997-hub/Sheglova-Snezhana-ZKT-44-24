using System.ComponentModel.DataAnnotations;

namespace JiraMinimal.Data;

public class Issue
{
    public int IssueId { get; set; }

    [Required(ErrorMessage = "Укажите название задачи!")]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "Длина от 3 до 200 символов")]
    public string Title { get; set; } = string.Empty;

    [StringLength(4000)]
    public string? Description { get; set; }

    // Связь с проектом
    public int ProjectId { get; set; }
   public virtual Project Project { get; set; } = null!;

    // Связь со статусом
    public int IssueStatusId { get; set; }
  public virtual IssueStatus IssueStatus { get; set; } = null!;

    // Тип задачи (строка: Task, Bug, Story, Epic)
    [StringLength(30)]
    public string Type { get; set; } = "Task";

    // Приоритет
    public int IssuePriorityId { get; set; }
   public virtual IssuePriority IssuePriority { get; set; } = null!;

    // Текущий отдел, где находится задача
    public int CurrentDepartmentId { get; set; }
   public virtual Department CurrentDepartment { get; set; } = null!;

    // Исполнитель
    public string? AssigneeId { get; set; }
   public virtual ApplicationUser? Assignee { get; set; }

    // Автор
    public string ReporterId { get; set; } = string.Empty;
    public virtual ApplicationUser Reporter { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DueDate { get; set; }

    // Родительская задача (для подзадач)
    public int? ParentIssueId { get; set; }
    public virtual Issue? ParentIssue { get; set; }

    // Коллекция подзадач
   public virtual ICollection<Issue> ChildIssues { get; set; } = new List<Issue>();

    // Комментарии, история, уведомления, передачи
   public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public virtual ICollection<IssueHistory> IssueHistories { get; set; } = new List<IssueHistory>();
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
  public virtual ICollection<IssueDepartmentTransfer> DepartmentTransfers { get; set; } = new List<IssueDepartmentTransfer>();
}
using System.ComponentModel.DataAnnotations;

namespace JiraMinimal.Data;

public class Project
{
    public int ProjectId { get; set; }

    [Required(ErrorMessage = "Укажите название проекта!")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина от 3 до 100 символов")]
    public string Name { get; set; } = string.Empty;

    [StringLength(2000)]
    public string? Description { get; set; }

    // Владелец проекта
    public string OwnerId { get; set; } = string.Empty;
    public virtual ApplicationUser Owner { get; set; } = null!;

    // Отдел, к которому привязан проект
    public int DepartmentId { get; set; }
    public virtual Department Department { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Задачи в проекте
   public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();
}
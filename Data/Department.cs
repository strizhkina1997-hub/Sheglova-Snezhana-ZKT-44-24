using System.ComponentModel.DataAnnotations;

namespace JiraMinimal.Data;

public class Department
{
    public int DepartmentId { get; set; }

    [Required(ErrorMessage = "Укажите название отдела!")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина от 3 до 100 символов")]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    // Руководитель отдела (ссылка на пользователя, может быть не назначен)
    public string? HeadUserId { get; set; }
    public virtual ApplicationUser? Head { get; set; }
public virtual ICollection<UserDepartment> UserDepartments { get; set; } = new List<UserDepartment>();
public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();
}
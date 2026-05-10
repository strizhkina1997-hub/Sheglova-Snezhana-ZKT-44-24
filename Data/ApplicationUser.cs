using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace JiraMinimal.Data;

public class ApplicationUser : IdentityUser
{
    [Required(ErrorMessage = "Укажите полное имя!")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина от 3 до 100 символов")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите роль!")]
    [StringLength(50)]
    public string Role { get; set; } = "User"; // По умолчанию

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Связи с отделами
public virtual ICollection<UserDepartment> UserDepartments { get; set; } = new List<UserDepartment>();
public virtual ICollection<Issue> AssignedIssues { get; set; } = new List<Issue>();
public virtual ICollection<Issue> ReportedIssues { get; set; } = new List<Issue>();
public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
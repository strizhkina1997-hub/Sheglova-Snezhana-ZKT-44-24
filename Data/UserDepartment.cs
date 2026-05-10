namespace JiraMinimal.Data;

public class UserDepartment
{
    public int UserDepartmentId { get; set; }

    // Внешние ключи
    public string UserId { get; set; } = string.Empty;
    public int DepartmentId { get; set; }

    // Навигационные свойства
   public virtual ApplicationUser User { get; set; } = null!;
public virtual Department Department { get; set; } = null!;


    public bool IsPrimary { get; set; }  // Основной отдел
}
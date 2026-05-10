using System.ComponentModel.DataAnnotations;

namespace JiraMinimal.Data;

public class IssuePriority
{
    public int IssuePriorityId { get; set; }

    [Required(ErrorMessage = "Укажите название приоритета!")]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;

    [StringLength(20)]
    public string? Color { get; set; }

    public int Ordinal { get; set; } // Очерёдность для сортировки

    public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();
}
using System.ComponentModel.DataAnnotations;

namespace JiraMinimal.Data;

public class IssueStatus
{
    public int IssueStatusId { get; set; }

    [Required(ErrorMessage = "Укажите название статуса!")]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;

    public int Ordinal { get; set; }     // Очерёдность
    public bool IsFinal { get; set; }    // Итоговый

   public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();
    public virtual ICollection<DepartmentWorkflow> FromStatusWorkflows { get; set; } = new List<DepartmentWorkflow>();
    public virtual ICollection<DepartmentWorkflow> ToStatusWorkflows { get; set; } = new List<DepartmentWorkflow>();
}
using System.ComponentModel.DataAnnotations;
namespace JiraMinimal.Data;

public class DepartmentWorkflow
{
    public int DepartmentWorkflowId { get; set; }

    public int? FromDepartmentId { get; set; }
    public virtual Department? FromDepartment { get; set; }

    public int FromStatusId { get; set; }
    public virtual IssueStatus FromStatus { get; set; } = null!;

    public int ToDepartmentId { get; set; }
    public virtual Department ToDepartment { get; set; } = null!;

    public int ToStatusId { get; set; }
    public virtual IssueStatus ToStatus { get; set; } = null!;

    [StringLength(255)]
    public string? AllowedRoles { get; set; }
}
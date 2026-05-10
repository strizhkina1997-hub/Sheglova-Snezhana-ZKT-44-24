using System.ComponentModel.DataAnnotations;

namespace JiraMinimal.Data;

public class IssueDepartmentTransfer
{
    public int IssueDepartmentTransferId { get; set; }

    public int IssueId { get; set; }
   public virtual Issue Issue { get; set; } = null!;

    public int FromDepartmentId { get; set; }
    public virtual Department FromDepartment { get; set; } = null!;

    public int ToDepartmentId { get; set; }
    public virtual Department ToDepartment { get; set; } = null!;

    public string TransferredByUserId { get; set; } = string.Empty;
    public virtual ApplicationUser TransferredByUser { get; set; } = null!;

    public DateTime TransferredAt { get; set; } = DateTime.UtcNow;

    [StringLength(2000)]
    public string? Comment { get; set; }
}
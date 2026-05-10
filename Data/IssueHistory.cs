using System.ComponentModel.DataAnnotations;


namespace JiraMinimal.Data;

public class IssueHistory
{
    public int IssueHistoryId { get; set; }

    public int IssueId { get; set; }
    public virtual Issue Issue { get; set; } = null!;

    public string ChangedByUserId { get; set; } = string.Empty;
    public virtual ApplicationUser ChangedByUser { get; set; } = null!;

    [StringLength(50)]
    public string FieldName { get; set; } = string.Empty;

    [StringLength(255)]
    public string? OldValue { get; set; }

    [StringLength(255)]
    public string? NewValue { get; set; }

    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
}
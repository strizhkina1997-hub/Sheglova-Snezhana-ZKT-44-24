namespace JiraMinimal.Data;

public class Notification
{
    public int NotificationId { get; set; }

    public string UserId { get; set; } = string.Empty;
   public virtual ApplicationUser User { get; set; } = null!;

    public int IssueId { get; set; }
    public virtual Issue Issue { get; set; } = null!;

    public string Message { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
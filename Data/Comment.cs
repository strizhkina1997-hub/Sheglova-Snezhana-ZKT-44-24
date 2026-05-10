using System.ComponentModel.DataAnnotations;

namespace JiraMinimal.Data;

public class Comment
{
    public int CommentId { get; set; }

    public int IssueId { get; set; }
    public virtual Issue Issue { get; set; } = null!;

    public string AuthorId { get; set; } = string.Empty;
    public virtual ApplicationUser Author { get; set; } = null!;
    [Required, StringLength(4000)]
    public string Text { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
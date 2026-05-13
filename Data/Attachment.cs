using System.ComponentModel.DataAnnotations;

namespace JiraMinimal.Data;

public class Attachment
{
    public int AttachmentId { get; set; }

    [Required]
    public int IssueId { get; set; }
    public Issue Issue { get; set; } = null!;

    [Required, StringLength(255)]
    public string FileName { get; set; } = string.Empty;

    [Required, StringLength(500)]
    public string FilePath { get; set; } = string.Empty;   // относительный путь от wwwroot

    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
}
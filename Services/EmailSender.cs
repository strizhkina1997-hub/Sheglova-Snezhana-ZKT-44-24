using Microsoft.AspNetCore.Identity;
using JiraMinimal.Data;

namespace JiraMinimal.Services;

public class EmailSender : IEmailSender<ApplicationUser>
{
    public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink) =>
        Task.CompletedTask;

    public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode) =>
        Task.CompletedTask;

    public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink) =>
        Task.CompletedTask;
}
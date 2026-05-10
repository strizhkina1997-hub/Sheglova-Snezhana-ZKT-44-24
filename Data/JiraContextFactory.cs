using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace JiraMinimal.Data;

public class JiraContextFactory : IDesignTimeDbContextFactory<JiraContext>
{
    public JiraContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<JiraContext>();
        optionsBuilder.UseSqlite("Data Source=jira.db");

        return new JiraContext(optionsBuilder.Options);
    }
}
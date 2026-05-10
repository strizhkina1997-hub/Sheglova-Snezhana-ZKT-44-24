using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JiraMinimal.Data;

public class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<JiraContext>();

        try
        {
            // Если статусы уже есть – база заполнена, выходим
            if (context.IssueStatuses.Any())
                return;

            // 1. Статусы задач
            context.IssueStatuses.AddRange(
                new IssueStatus { Name = "К выполнению", Ordinal = 1, IsFinal = false },
                new IssueStatus { Name = "В работе", Ordinal = 2, IsFinal = false },
                new IssueStatus { Name = "Готово", Ordinal = 3, IsFinal = true },
                new IssueStatus { Name = "Отклонено", Ordinal = 4, IsFinal = true }
            );

            // 2. Приоритеты
            context.IssuePriorities.AddRange(
                new IssuePriority { Name = "Низкий", Ordinal = 1, Color = "#8BC34A" },
                new IssuePriority { Name = "Средний", Ordinal = 2, Color = "#FFC107" },
                new IssuePriority { Name = "Высокий", Ordinal = 3, Color = "#FF9800" },
                new IssuePriority { Name = "Критический", Ordinal = 4, Color = "#F44336" }
            );

            // 3. Отделы
            var departments = new[]
            {
                new Department { Name = "Отдел развития и автоматизации", Description = "Разработка и автоматизация" },
                new Department { Name = "Управление обработки нарядов", Description = "Тестирование и контроль" }
            };
            context.Departments.AddRange(departments);
            context.SaveChanges();

            // 4. Роли
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            if (!roleManager.Roles.Any())
            {
                var adminRole = new IdentityRole("Administrator");
                var userRole = new IdentityRole("User");
                roleManager.CreateAsync(adminRole).Wait();
                roleManager.CreateAsync(userRole).Wait();
            }

            // 5. Администратор
            var adminUser = new ApplicationUser
            {
                UserName = "admin@jira.ru",
                Email = "admin@jira.ru",
                FullName = "Администратор Системы",
                Role = "Admin",
                CreatedAt = DateTime.UtcNow
            };

            if (userManager.FindByEmailAsync(adminUser.Email).Result == null)
            {
                var result = userManager.CreateAsync(adminUser, "Admin123!").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(adminUser, "Administrator").Wait();
                    context.UserDepartments.Add(new UserDepartment
                    {
                        UserId = adminUser.Id,
                        DepartmentId = departments[0].DepartmentId,
                        IsPrimary = true
                    });
                    Console.WriteLine("Администратор успешно создан.");
                }
                else
                {
                    foreach (var err in result.Errors)
                        Console.WriteLine($"Ошибка при создании админа: {err.Description}");
                }
            }
            else
            {
                Console.WriteLine("Администратор уже существует.");
            }

            // 6. Обычный пользователь
            var testUser = new ApplicationUser
            {
                UserName = "user@jira.ru",
                Email = "user@jira.ru",
                FullName = "Тестовый Пользователь",
                Role = "User",
                CreatedAt = DateTime.UtcNow
            };

            if (userManager.FindByEmailAsync(testUser.Email).Result == null)
            {
                var result = userManager.CreateAsync(testUser, "User123!").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(testUser, "User").Wait();
                    context.UserDepartments.Add(new UserDepartment
                    {
                        UserId = testUser.Id,
                        DepartmentId = departments[1].DepartmentId,
                        IsPrimary = true
                    });
                    Console.WriteLine("Обычный пользователь успешно создан.");
                }
                else
                {
                    foreach (var err in result.Errors)
                        Console.WriteLine($"Ошибка при создании пользователя: {err.Description}");
                }
            }

            context.SaveChanges();
            Console.WriteLine("SeedData выполнена успешно.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"КРИТИЧЕСКАЯ ОШИБКА в SeedData: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
        }
    }
}
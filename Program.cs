using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using JiraMinimal.Data;
using JiraMinimal.Components;
using JiraMinimal.Components.Account;
using JiraMinimal.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Сервисы Blazor с интерактивным серверным рендерингом
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// 2. Фабрика контекста базы данных JiraContext (без ленивой загрузки)
builder.Services.AddDbContextFactory<JiraContext>(options =>
    options.UseSqlite("Data Source=jira.db"));

// 3. Настройка аутентификации (куки) — обязательно для Identity
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
.AddIdentityCookies();

// 4. Identity с нашим классом пользователя, ролями и хранилищем
builder.Services.AddIdentityCore<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<JiraContext>()
.AddSignInManager()
.AddDefaultTokenProviders();

// 5. Сервисы для Identity UI
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<IEmailSender<ApplicationUser>, EmailSender>();

// 6. Авторизация (для атрибутов [Authorize])
builder.Services.AddAuthorization();

// 7. Адаптер QuickGrid для Entity Framework (опционально)
builder.Services.AddQuickGridEntityFrameworkAdapter();

var app = builder.Build();

// 8. Инициализация базы данных и начальные данные
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<JiraContext>();
    context.Database.EnsureCreated();
    SeedData.Initialize(services);
}

// 9. Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Подключаем аутентификацию и авторизацию
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

// 10. Маршруты Blazor
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// 11. Дополнительные конечные точки Identity (Identity UI)
app.MapAdditionalIdentityEndpoints();

app.Run();
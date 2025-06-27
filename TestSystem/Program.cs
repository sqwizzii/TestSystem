using Microsoft.EntityFrameworkCore;
using TestSystem.Data;
using TestSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// Додаємо Razor Pages
builder.Services.AddRazorPages();




// Додаємо DbContext з PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Додаємо сервіси
builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();

var app = builder.Build();

// Обробка помилок
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

// Підключення статичних файлів (css, js, зображення)
app.UseStaticFiles();

// Роутінг
app.UseRouting();

app.UseAuthorization();

// Підключення Razor Pages
app.MapRazorPages();

// Підключення контролерів
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Teacher}/{action=CreateTest}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Teacher}/{action=AllTests}/{id?}");


app.Run();

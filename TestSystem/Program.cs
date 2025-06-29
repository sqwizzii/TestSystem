using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using TestSystem.Data;
using TestSystem.Models;

//var builder = WebApplication.CreateBuilder(args);


//// Add services to the container.
//builder.Services.AddRazorPages();

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error");
//}

//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//    db.Database.Migrate();

//    if (!db.Tests.Any())
//    {
//        db.Tests.Add(new Test
//        {
//            Title = "Ѕазовий тест",
//            Questions = new List<Question>
//            {
//                new Question
//                {
//                    Text = "яка столиц€ ”крањни?",
//                    Type = QuestionType.SingleChoice,
//                    Answers = new List<Answer>
//                    {
//                        new Answer { Text = " ињв", IsCorrect = true },
//                        new Answer { Text = "Ћьв≥в", IsCorrect = false },
//                        new Answer { Text = "Ћуцьк", IsCorrect = false }
//                    }
//                }
//            }
//        });
//        db.SaveChanges();
//    }
//}



//app.UseStaticFiles();

//app.UseRouting();

//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(policy =>
//    {
//        policy.AllowAnyOrigin()
//              .AllowAnyHeader()
//              .AllowAnyMethod();
//    });
//});

//app.UseCors();

//app.UseAuthorization();

//app.MapRazorPages();

//app.Run();



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Configure the HTTP request pipeline.



builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();

    if (!db.Tests.Any())
    {
        db.Tests.Add(new Test
        {
            Title = "Ѕазовий тест",
            Questions = new List<Question>
            {
                new Question
                {
                    Text = "яка столиц€ ”крањни?",
                    Type = QuestionType.SingleChoice,
                    Answers = new List<Answer>
                    {
                        new Answer { Text = " ињв", IsCorrect = true },
                        new Answer { Text = "Ћьв≥в", IsCorrect = false },
                        new Answer { Text = "Ћуцьк", IsCorrect = false }
                    }
                }
            }
        });
        db.SaveChanges();
    }
}

app.UseRouting();
app.UseCors();
app.UseAuthorization();

app.MapRazorPages();

app.MapControllers();
app.Run();

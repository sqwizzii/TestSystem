using Microsoft.EntityFrameworkCore;
using TestSystem.Data;
using TestSystem.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


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

    if (app.Environment.IsDevelopment())
    {
        db.Answers.RemoveRange(db.Answers);
        db.Questions.RemoveRange(db.Questions);
        db.Tests.RemoveRange(db.Tests);
        db.UserAttempts.RemoveRange(db.UserAttempts);

        db.SaveChanges();
    }

    if (!db.Tests.Any())
    {
        db.Tests.Add(new Test
        {
            Title = "Базовий тест",
            Questions = new List<Question>
            {
                new Question
                {
                    Text = "Яка столиця України?",
                    Type = QuestionType.SingleChoice,
                    Answers = new List<Answer>
                    {
                        new Answer { Text = "Київ", IsCorrect = true },
                        new Answer { Text = "Львів", IsCorrect = false },
                        new Answer { Text = "Луцьк", IsCorrect = false }
                    }
                },
                new Question
                {
                    Text = "Що із перечислених назв являється фруктом?",
                    Type = QuestionType.MultipleChoice,
                    Answers = new List<Answer>
                    {
                        new Answer { Text = "Яблуко", IsCorrect = true },
                        new Answer { Text = "Груша", IsCorrect = true },
                        new Answer { Text = "Помідор", IsCorrect = false }
                    }
                }
            }
        });

        db.Tests.Add(new Test
        {
            Title = "Базовий тест 2",
            Questions = new List<Question>
            {
                new Question
                {
                    Text = "Виберіть слова у ских є буква \'ч\'.",
                    Type = QuestionType.MultipleChoice,
                    Answers = new List<Answer>
                    {
                        new Answer { Text = "Леопард", IsCorrect = false },
                        new Answer { Text = "Наряд", IsCorrect = false },
                        new Answer { Text = "Ковпачок", IsCorrect = true },
                        new Answer { Text = "Журавлі", IsCorrect = false },
                        new Answer { Text = "Кольчуга", IsCorrect = true }
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

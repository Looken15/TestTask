using System;
using Microsoft.EntityFrameworkCore;
using testTask.Models.Simple;
using System.Linq;

namespace testTask.Models.Context
{
    public class MainContext : DbContext
    {
        public DbSet<SpecialtyRequirements> SpecialtyRequirements { get; set; }
        public DbSet<EducationKind> EducationKinds { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<TestForm> TestForms { get; set; }
        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }

    public static class StartData
    {
        public static void Initialize(MainContext context)
        {
            if (!context.EducationKinds.Any())
            {
                context.EducationKinds.AddRange(
                    new EducationKind { Name = "Среднее общее" },
                    new EducationKind { Name = "Среднее профессиональное" },
                    new EducationKind { Name = "Высшее" }
                    );
            }
            if (!context.Subjects.Any())
            {
                context.Subjects.AddRange(
                    new Subject { Name = "математика" },
                    new Subject { Name = "информатика и ИКТ" },
                    new Subject { Name = "русский язык" },
                    new Subject { Name = "математические основы информационных технологий" },
                    new Subject { Name = "информационные технологии в профессиональной деятельности" },
                    new Subject { Name = "физика" },
                    new Subject { Name = "химия" },
                    new Subject { Name = "биология" },
                    new Subject { Name = "география" },
                    new Subject { Name = "страноведение" },
                    new Subject { Name = "экология и природопользование" }
                    );
            }
            if (!context.TestForms.Any())
            {
                context.TestForms.AddRange(
                    new TestForm { Name = "ЕГЭ" },
                    new TestForm { Name = "экзамен" }
                    );
            }
            context.SaveChanges();
        }
    }
}

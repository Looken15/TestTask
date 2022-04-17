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
                    new EducationKind { Name = "������� �����" },
                    new EducationKind { Name = "������� ����������������" },
                    new EducationKind { Name = "������" }
                    );
            }
            if (!context.Subjects.Any())
            {
                context.Subjects.AddRange(
                    new Subject { Name = "����������" },
                    new Subject { Name = "����������� � ���" },
                    new Subject { Name = "������� ����" },
                    new Subject { Name = "�������������� ������ �������������� ����������" },
                    new Subject { Name = "�������������� ���������� � ���������������� ������������" },
                    new Subject { Name = "������" },
                    new Subject { Name = "�����" },
                    new Subject { Name = "��������" },
                    new Subject { Name = "���������" },
                    new Subject { Name = "�������������" },
                    new Subject { Name = "�������� � ������������������" }
                    );
            }
            if (!context.TestForms.Any())
            {
                context.TestForms.AddRange(
                    new TestForm { Name = "���" },
                    new TestForm { Name = "�������" }
                    );
            }
            context.SaveChanges();
        }
    }
}

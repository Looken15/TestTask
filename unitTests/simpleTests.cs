using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testTask.Interfaces;
using testTask.Controllers;
using testTask.Models.Simple;
using Xunit;
using Moq;

namespace unitTests
{
    public class simpleTests
    {
        [Fact]
        public void CountTest()
        {
            var mockServ = new Mock<IScoresService>();

            var mockRepo = new Mock<IScoresRepository>();
            mockRepo.Setup(x => x.GetAllRequirements()).Returns(GetTest());

            var controller = new ScoresController(mockServ.Object, mockRepo.Object);
            var model = controller.GetAllRequirements();

            Assert.Equal(4, model.Count());
        }

        [Fact]
        public void PriorityTest()
        {
            var mockServ = new Mock<IScoresService>();

            var mockRepo = new Mock<IScoresRepository>();
            mockRepo.Setup(x => x.GetAllRequirements()).Returns(GetTest());

            var controller = new ScoresController(mockServ.Object, mockRepo.Object);
            var model = controller.GetAllRequirements();

            Assert.Equal(1, model.Count(x => x.Priority == 1));
            Assert.Equal(2, model.Count(x => x.Priority == 2));
            Assert.Equal(1, model.Count(x => x.Priority == 3));
        }

        [Fact]
        public void SamePriorityTest()
        {
            var mockServ = new Mock<IScoresService>();

            var mockRepo = new Mock<IScoresRepository>();
            mockRepo.Setup(x => x.GetAllRequirements()).Returns(GetTest());

            var controller = new ScoresController(mockServ.Object, mockRepo.Object);
            var model = controller.GetAllRequirements();

            var groups = model.GroupBy(x => new { x.Priority, x.SpecialtyCode, x.EducationKind }).Where(x => x.Count() > 1);
            foreach (var group in groups)
            {
                var groupList = group.ToList();
                Assert.True(groupList.Count(x => x.ReplaceSubject == null) == 1);
            }
        }

        private IQueryable<SpecialtyRequirements> GetTest()
        {
            var testForms = new List<TestForm>
            {
                new TestForm
                {
                    Id = 1,
                    Name = "ЕГЭ"
                },
                new TestForm
                {
                    Id = 2,
                    Name = "Экзамен"
                }
            };

            return new List<SpecialtyRequirements>()
            {
                new SpecialtyRequirements
                {
                    SpecialtyCode = "ММ_О_ГБ",
                    EducationKind = new List<EducationKind>
                    {
                        new EducationKind
                        {
                            Id = 1,
                            Name = "Среднее общее"
                        },
                    },
                    Subject = new Subject
                    {
                        Id = 1,
                        Name = "математика"
                    },
                    TestForm = testForms,
                    MinScore = 50,
                    Priority = 1
                },
                new SpecialtyRequirements
                {
                    SpecialtyCode = "ММ_О_ГБ",
                    EducationKind = new List<EducationKind>
                    {
                        new EducationKind
                        {
                            Id = 1,
                            Name = "Среднее общее"
                        },
                    },
                    Subject = new Subject
                    {
                        Id = 2,
                        Name = "информатика и ИКТ"
                    },
                    TestForm = testForms,
                    MinScore = 50,
                    Priority = 2
                },
                new SpecialtyRequirements
                {
                    SpecialtyCode = "ММ_О_ГБ",
                    EducationKind = new List<EducationKind>
                    {
                        new EducationKind
                        {
                            Id = 1,
                            Name = "Среднее общее"
                        },
                    },
                    Subject = new Subject
                    {
                        Id = 2,
                        Name = "информатика и ИКТ"
                    },
                    ReplaceSubject = new Subject
                    {
                        Id = 3,
                        Name = "физика"
                    },
                    TestForm = testForms,
                    MinScore = 50,
                    Priority = 2
                },
                new SpecialtyRequirements
                {
                    SpecialtyCode = "ММ_О_ГБ",
                    EducationKind = new List<EducationKind>
                    {
                        new EducationKind
                        {
                            Id = 1,
                            Name = "Среднее общее"
                        },
                    },
                    Subject = new Subject
                    {
                        Id = 4,
                        Name = "русский язык"
                    },
                    TestForm = testForms,
                    MinScore = 50,
                    Priority = 3
                }
            }.AsQueryable();
        }
    }
}

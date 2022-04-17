using OfficeOpenXml;
using System;
using System.IO;
using System.Linq;
using testTask.Interfaces;
using testTask.Models.Context;
using testTask.Models.Simple;

namespace testTask.Repository
{
    public class ScoresRepository : IScoresRepository
    {
        private readonly MainContext v_context;

        public ScoresRepository(MainContext context)
        {
            v_context = context;
        }

        public void Add(SpecialtyRequirements item)
        {
            v_context.SpecialtyRequirements.Add(item);
            v_context.SaveChanges();
        }

        public IQueryable<SpecialtyRequirements> GetAllRequirements()
        {
            return v_context.SpecialtyRequirements.AsQueryable();
        }

        public IQueryable<SpecialtyRequirements> GetRequirements(string code, string[] kind)
        {
            var kinds = kind.Select(x => GetEducationKind(x)).Select(x => x.Id);
            return GetAllRequirements().Where(x => x.SpecialtyCode.Trim().ToLower() == code.Trim().ToLower()
                && x.EducationKind.All(y => kinds.Contains(y.Id)));
        }

        public void DeleteAllRequirements()
        {
            v_context.SpecialtyRequirements.RemoveRange(GetAllRequirements());
            v_context.SaveChanges();
        }

        public EducationKind GetEducationKind(string name)
        {
            return v_context.EducationKinds.FirstOrDefault(x => x.Name.Trim().ToLower() == name.Trim().ToLower());
        }

        public Subject GetSubject(string name)
        {
            return v_context.Subjects.FirstOrDefault(x => x.Name.Trim().ToLower() == name.Trim().ToLower());
        }

        public TestForm GetTestForm(string name)
        {
            return v_context.TestForms.FirstOrDefault(x => x.Name.Trim().ToLower() == name.Trim().ToLower());
        }
    }
}

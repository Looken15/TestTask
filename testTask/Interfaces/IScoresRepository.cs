using System;
using System.Linq;
using testTask.Models.Simple;

namespace testTask.Interfaces
{
    public interface IScoresRepository
    {
        void Add(SpecialtyRequirements item);
        IQueryable<SpecialtyRequirements> GetAllRequirements();
        IQueryable<SpecialtyRequirements> GetRequirements(string code, string[] kind);
        void DeleteAllRequirements();
        EducationKind GetEducationKind(string name);
        Subject GetSubject(string name);
        TestForm GetTestForm(string name);
    }
}

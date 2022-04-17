using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace testTask.Models.Simple
{
    public class SpecialtyRequirements
    {
        public SpecialtyRequirements()
        {
            EducationKind = new HashSet<EducationKind>();
            TestForm = new HashSet<TestForm>();
        }

        public int Id { get; set; }

        public string SpecialtyCode { get; set; }

        public virtual ICollection<EducationKind> EducationKind { get; set; }

        public Subject Subject { get; set; }

        public Subject ReplaceSubject { get; set; }

        public virtual ICollection<TestForm> TestForm { get; set; }

        public int MinScore { get; set; }

        public int Priority { get; set; }
    }
}

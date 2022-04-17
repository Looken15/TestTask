using System;
using System.Collections.Generic;

namespace testTask.Models.Simple
{
    public class EducationKind
    {
        public EducationKind()
        {
            SpecialtyRequirements = new HashSet<SpecialtyRequirements>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<SpecialtyRequirements> SpecialtyRequirements { get; set; }
    }
}

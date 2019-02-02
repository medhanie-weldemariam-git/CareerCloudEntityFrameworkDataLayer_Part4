using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantSkillLogic : BaseLogic<ApplicantSkillPoco>
    {
        public ApplicantSkillLogic(IDataRepository<ApplicantSkillPoco> dataRepository): base(dataRepository)
        {

        }

        protected override void Verify(ApplicantSkillPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (var p in pocos)
            {
                if (p.StartMonth > 12)
                {
                    exceptions.Add(new ValidationException(101, $"StartMonth for ApplicantSkill {p.StartMonth} cannot be greater than 12."));
                }

                if (p.EndMonth > 12)
                {
                    exceptions.Add(new ValidationException(102, $"EndMonth for ApplicantSkill {p.EndMonth} cannot be greater than 12."));
                }

                if (p.StartYear < 1900)
                {
                    exceptions.Add(new ValidationException(103, $"StartYear for ApplicantSkill {p.StartYear} cannot be less than 1900."));
                }

                if (p.EndYear < p.StartYear)
                {
                    exceptions.Add(new ValidationException(104, $"EndYear for ApplicantSkill {p.EndYear} cannot be less than StartYear."));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        public override void Add(ApplicantSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(ApplicantSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }

}

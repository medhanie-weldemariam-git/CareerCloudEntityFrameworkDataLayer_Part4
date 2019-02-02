using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantEducationLogic : BaseLogic<ApplicantEducationPoco>
    {
        public ApplicantEducationLogic(IDataRepository<ApplicantEducationPoco> dataRepository) : base(dataRepository)
        {

        }

        protected override void Verify(ApplicantEducationPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach(var p in pocos)
            {
                if (string.IsNullOrEmpty(p.Major))
                {
                    exceptions.Add(new ValidationException(107, $"Major for ApplicantEducation {p.Major} cannot be empty."));
                }
                else if (p.Major.Length < 3)
                {
                    exceptions.Add(new ValidationException(107, $"Major for ApplicantEducation {p.Major} must be at least 3 characters."));
                }

                if(p.StartDate > DateTime.Now)
                {
                    exceptions.Add(new ValidationException(108, $"Major for ApplicantEducation {p.StartDate} must be before today."));
                }

                if (p.CompletionDate < p.StartDate)
                {
                    exceptions.Add(new ValidationException(109, $"Major for ApplicantEducation {p.CompletionDate} must be later than start date."));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        public override void Add(ApplicantEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(ApplicantEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

    }
}

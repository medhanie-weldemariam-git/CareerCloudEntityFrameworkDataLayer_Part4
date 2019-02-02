using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantProfileLogic : BaseLogic<ApplicantProfilePoco>
    {
        public ApplicantProfileLogic(IDataRepository<ApplicantProfilePoco> dataRepository): base(dataRepository)
        {

        }

        protected override void Verify(ApplicantProfilePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (var p in pocos)
            {
                if (p.CurrentSalary < 0)
                {
                    exceptions.Add(new ValidationException(111, $"CurrentSalary for ApplicantProfile {p.CurrentSalary} cannot be negative."));
                }

                if (p.CurrentRate < 0)
                {
                    exceptions.Add(new ValidationException(112, $"CurrentRate for ApplicantProfile {p.CurrentRate} cannot be negative."));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        public override void Add(ApplicantProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(ApplicantProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}

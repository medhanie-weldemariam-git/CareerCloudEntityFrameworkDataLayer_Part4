using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyDescriptionLogic : BaseLogic<CompanyDescriptionPoco>
    {
        public CompanyDescriptionLogic(IDataRepository<CompanyDescriptionPoco> dataRepository): base(dataRepository)
        {

        }

        protected override void Verify(CompanyDescriptionPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (var p in pocos)
            {
                if(p.CompanyDescription?.Length < 3)
                {
                    exceptions.Add(new ValidationException(107, $"CompanyDescription for CompanyDescription {p.CompanyDescription} must be at greater than 2 characters."));
                }
                if (p.CompanyName?.Length < 3)
                {
                    exceptions.Add(new ValidationException(106, $"CompanyName for CompanyDescription {p.CompanyName} must be at greater than 2 characters."));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        public override void Add(CompanyDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}

using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyLocationLogic : BaseLogic<CompanyLocationPoco>
    {
        public CompanyLocationLogic(IDataRepository<CompanyLocationPoco> dataRepository): base(dataRepository)
        {
                
        }

        protected override void Verify(CompanyLocationPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (var p in pocos)
            {
                if (string.IsNullOrEmpty(p.CountryCode))
                {
                    exceptions.Add(new ValidationException(500, $"CountryCode for CompanyLocation {p.CountryCode} cannot be empty."));
                }
                if (string.IsNullOrEmpty(p.Province))
                {
                    exceptions.Add(new ValidationException(501, $"Province for CompanyLocation {p.Province} cannot be empty."));
                }
                if (string.IsNullOrEmpty(p.Street))
                {
                    exceptions.Add(new ValidationException(502, $"Street for CompanyLocation {p.Street} cannot be empty."));
                }
                if (string.IsNullOrEmpty(p.City))
                {
                    exceptions.Add(new ValidationException(503, $"City for CompanyLocation {p.City} cannot be empty."));
                }
                if (string.IsNullOrEmpty(p.PostalCode))
                {
                    exceptions.Add(new ValidationException(504, $"PostalCode for CompanyLocation {p.PostalCode} cannot be empty."));
                }
                if (exceptions.Count > 0)
                {
                    throw new AggregateException(exceptions);
                }
            }
        }

        public override void Add(CompanyLocationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(CompanyLocationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}

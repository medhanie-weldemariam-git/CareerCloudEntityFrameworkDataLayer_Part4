using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyProfileLogic : BaseLogic<CompanyProfilePoco>
    {
        public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> dataRepository): base(dataRepository)
        {

        }

        protected override void Verify(CompanyProfilePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (var p in pocos)
            {
                string[] domain = { ".ca", ".com", ".biz" };
                
                if (domain.Any(x => p.CompanyWebsite?.EndsWith(x)==false))
                {
                    exceptions.Add(new ValidationException(600, $"CompanyWebsite for CompanyProfile {p.CompanyWebsite} must end with the following extensions – "+ ".ca" + "," + ".com" + "," + ".biz" + "."));
                }

                string[] phoneComponents = p.ContactPhone?.Split('-');
                if (phoneComponents?.Length < 3)
                {
                    exceptions.Add(new ValidationException(601, $"PhoneNumber for SecurityLogin {p.Id} is not in the required format."));
                }
                else
                {
                    if (phoneComponents?[0].Length != 3)
                    {
                        exceptions.Add(new ValidationException(601, $"PhoneNumber for SecurityLogin {p.Id} is not in the required format."));
                    }
                    else if (phoneComponents?[1].Length != 3)
                    {
                        exceptions.Add(new ValidationException(601, $"PhoneNumber for SecurityLogin {p.Id} is not in the required format."));
                    }
                    else if (phoneComponents?[2].Length != 4)
                    {
                        exceptions.Add(new ValidationException(601, $"PhoneNumber for SecurityLogin {p.Id} is not in the required format."));
                    }
                }
            }
            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        public override void Add(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}

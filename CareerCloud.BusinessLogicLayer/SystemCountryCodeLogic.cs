using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class SystemCountryCodeLogic
    {
        private IDataRepository<SystemCountryCodePoco> _repository;
        public SystemCountryCodeLogic(IDataRepository<SystemCountryCodePoco> dataRepository)
        {
            _repository = dataRepository;
        }

        public void Verify(SystemCountryCodePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (var p in pocos)
            {
                if (string.IsNullOrEmpty(p.Code))
                {
                    exceptions.Add(new ValidationException(900, $"Code for SystemCountryCode {p.Code} cannot be empty."));
                }
                if (string.IsNullOrEmpty(p.Name))
                {
                    exceptions.Add(new ValidationException(901, $"Name for SystemCountryCode {p.Name} cannot be empty."));
                }
            }
            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }


        public void Update(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);
            _repository.Update(pocos);
        }

        public void Add(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);
            _repository.Add(pocos);
        }

        public SystemCountryCodePoco Get(string id)
        {
            return _repository.GetSingle(c => c.Code == id);
        }

        public virtual List<SystemCountryCodePoco> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public void Delete(SystemCountryCodePoco[] pocos)
        {
            _repository.Remove(pocos);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Exceptions;

namespace Domain.ValueObjects
{
    public class Email:ValueObject
    {
        private Email()
        {

        }

        public string UserName { get; private set; }
        public string Company { get; private set; }
        public string DomainName { get; private set; }
        public static Email For(string email)
        {
            var _email = new Email();
            try
            {
                
                var index=email.IndexOf("@",StringComparison.Ordinal);
                _email.UserName = email.Substring(0, index);
                var dotIndex = email.LastIndexOf(".", index + 1);
                _email.Company = email.Substring(index + 1, dotIndex);
                _email.DomainName = email.Substring(dotIndex + 1);
                return _email;
            }
            catch (Exception e)
            {
                throw new EmailNotMatchException(email, e);
            }
        }

        public static implicit operator string(Email email)
        {
            return email.ToString();
        }

        public static explicit operator Email(string email)
        {
            return For(email);
        }
        public override string ToString()
        {
            return $"{UserName}@{Company}.{DomainName}";
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return UserName;
            yield return Company;
            yield return DomainName;
        }
    }
}

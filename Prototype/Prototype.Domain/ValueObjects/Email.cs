using System.Collections.Generic;

namespace Prototype.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public string Valor { get; }

        protected Email()
        {
        }

        private Email(string valor)
        {
            this.Valor = valor;
        }

        public static Email Create(string valor)
        {         
            return new Email(valor);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Valor;
        }
    }
}

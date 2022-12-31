using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prototype.Shared.CustomValidators
{
    
    public class ValidEnumValidator<T> where T : Enum
    {
        private readonly Enum undefined;

        public ValidEnumValidator(Enum undefined)
        {
            this.undefined = undefined;
        }

        public ValidEnumValidator() : this(null)
        {
        }

        public List<T> Obter() =>
            undefined == null ?
            (Enum.GetValues(typeof(T)).Cast<T>()).ToList() :
            (Enum.GetValues(typeof(T)).Cast<T>()).Where(w => !(w).Equals(undefined)).ToList();

        public override string ToString()
        {
            var valoresValidos = undefined == null ?
            (Enum.GetValues(typeof(T)).Cast<T>()).ToList() :
            (Enum.GetValues(typeof(T)).Cast<T>()).Where(w => !(w).Equals(undefined)).ToList();
            return string.Join(", ", valoresValidos.Select(s => $"{((T)s)} - {s}"));
        }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace ZValidation
{
    public class ZResponse
    {
        public IEnumerable<string> Errors 
        {
            get { return PropertyErrors.SelectMany(fe => fe.Value.Select(e => e)); }
        }
        public Dictionary<string, List<string>> PropertyErrors { get; private set; } = new Dictionary<string, List<string>>();
        public bool IsSuccessful
        {
            get { return Errors.Count() == 0; }
        }

        public ZResponse() { }

        public void AddPropertyError(string propertyName, string error)
        {
            if (this.PropertyErrors.ContainsKey(propertyName))
                this.PropertyErrors[propertyName].Add(error);
            else
                this.PropertyErrors.Add(propertyName, new List<string>() { error });
        }
    }
}

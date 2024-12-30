using System;

namespace ZValidation
{
    public class ZType<T>
    {
        public readonly string PropertyName;
        public readonly T Value;
        private Func<string, string, bool> _addError { get; }
        public ZType(string propertyName, T input, Func<string, string, bool> addError)
        {
            this.PropertyName = propertyName;
            this.Value = input;
            this._addError = addError;
        }

        public void CreateError(string error)
        {
            this._addError(PropertyName, error);
        }
    }
}

using System;

namespace ZValidation
{
    public class ZType<T>
    {
        internal readonly string PropertyName;
        internal readonly T Value;
        private Func<string, string, bool> _addError { get; }
        public ZType(string propertyName, T input, Func<string, string, bool> addError)
        {
            this.PropertyName = propertyName;
            this.Value = input;
            this._addError = addError;
        }

        internal void CreateError(string error)
        {
            this._addError(PropertyName, error);
        }
    }
}

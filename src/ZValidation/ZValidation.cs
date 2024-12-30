using System;
using System.Linq.Expressions;

namespace ZValidation
{
    public class ZValidation<T>
    {
        private readonly T _input;
        public ZResponse Response = new ZResponse();
        public bool IsSuccessful
        {
            get { return Response.IsSuccessful; }
        }

        public ZValidation(T input)
        {
            this._input = input;
        }

        public ZType<TProperty> For<TProperty>(Expression<Func<T, TProperty>> expression, string propertyName = null)
        {
            var prop = typeof(T) != typeof(TProperty) ? (expression.Body as MemberExpression).Member.Name : "Field";
            var value = typeof(T) != typeof(TProperty) ? (_input.GetType().GetProperty(prop) != null ? (TProperty)_input.GetType().GetProperty(prop).GetValue(_input) : (TProperty)_input.GetType().GetField(prop).GetValue(_input)) : (TProperty)(object)_input;
            return new ZType<TProperty>(propertyName ?? prop, value, AddError);
        }

        private bool AddError(string propertyName, string error)
        {
            this.Response.AddPropertyError(propertyName, error);
            return true;
        }
    }
}

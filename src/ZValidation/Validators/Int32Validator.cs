using System;

namespace ZValidation
{
    public static class Int32Validator
    {
        public static ZType<Int32> Min(this ZType<Int32> input, Int32 value, string error = null)
        {
            if (input.Value < value)
                input.CreateError(error ?? $"{input.PropertyName} is less than {value}");
            return input;
        }

        public static ZType<Int32> Max(this ZType<Int32> input, Int32 value, string error = null)
        {
            if (input.Value > value)
                input.CreateError(error ?? $"{input.PropertyName} is greater than {value}");
            return input;
        }
    }
}

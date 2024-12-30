using System;

namespace ZValidation
{
    public static class StringValidator
    {
        public static ZType<string> Required(this ZType<string> input, string error = null)
        {
            if (string.IsNullOrEmpty(input.Value))
                input.CreateError(error ?? $"{input.PropertyName} {ErrorMessages.IS_REQUIRED}");
            return input;
        }

        public static ZType<string> Length(this ZType<string> input, int length, string error = null)
        {
            if (input.Value == null)
                input.CreateError(error ?? $"{input.PropertyName} {ErrorMessages.IS_REQUIRED}");
            else if (input.Value.Length != length)
                input.CreateError(error ?? $"{input.PropertyName} should be {length} character{(length > 1 ? "s" : "")}");

            return input;
        }

        public static ZType<string> LengthMin(this ZType<string> input, int length, string error = null)
        {
            if (input.Value == null)
                input.CreateError(error ?? $"{input.PropertyName} {ErrorMessages.IS_REQUIRED}");
            else if (input.Value.Length < length)
                input.CreateError(error ?? $"{input.PropertyName} should be at least {length} character{(length > 1 ? "s" : "")}");

            return input;
        }

        public static ZType<string> LengthMax(this ZType<string> input, int length, string error = null)
        {
            if (input.Value == null)
                input.CreateError(error ?? $"{input.PropertyName} {ErrorMessages.IS_REQUIRED}");
            else if (input.Value.Length > length)
                input.CreateError(error ?? $"{input.PropertyName} should be at most {length} character{(length > 1 ? "s" : "")}");

            return input;
        }

        public static ZType<string> LengthBetween(this ZType<string> input, int min, int max, string error = null)
        {
            if (input.Value == null)
                input.CreateError(error ?? $"{input.PropertyName} {ErrorMessages.IS_REQUIRED}");
            else if (input.Value.Length < min || input.Value.Length > max)
                input.CreateError(error ?? $"{input.PropertyName} should be between {min} and {max} characters");

            return input;
        }

        public static ZType<string> Contains(this ZType<string> input, string text, string error = null)
        {
            if (input.Value == null)
                input.CreateError(error ?? $"{input.PropertyName} {ErrorMessages.IS_REQUIRED}");
            else if (!input.Value.Contains(text))
                input.CreateError(error ?? $"{input.PropertyName} does not contain {text}");

            return input;
        }

        public static ZType<string> StartsWith(this ZType<string> input, string text, string error = null)
        {
            if (input.Value == null)
                input.CreateError(error ?? $"{input.PropertyName} {ErrorMessages.IS_REQUIRED}");
            else if (!input.Value.StartsWith(text))
                input.CreateError(error ?? $"{input.PropertyName} does not start with {text}");

            return input;
        }

        public static ZType<string> EndsWith(this ZType<string> input, string text, string error = null)
        {
            if (input.Value == null)
                input.CreateError(error ?? $"{input.PropertyName} {ErrorMessages.IS_REQUIRED}");
            else if (!input.Value.EndsWith(text))
                input.CreateError(error ?? $"{input.PropertyName} does not end with {text}");

            return input;
        }

        public static ZType<string> Regex(this ZType<string> input, int min, int max, string error = null)
        {
            // TODO
            throw new NotImplementedException();
        }

        public static ZType<string> Email(this ZType<string> input, int min, int max, string error = null)
        {
            // TODO
            throw new NotImplementedException();
        }

        public static ZType<string> Url(this ZType<string> input, int min, int max, string error = null)
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}

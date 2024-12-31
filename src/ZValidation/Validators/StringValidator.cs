using System;
using System.Text.RegularExpressions;

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
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("Text is mandatory for Contains validation", "text");

            if (input.Value == null)
                input.CreateError(error ?? $"{input.PropertyName} {ErrorMessages.IS_REQUIRED}");
            else if (!input.Value.Contains(text))
                input.CreateError(error ?? $"{input.PropertyName} does not contain {text}");

            return input;
        }

        public static ZType<string> StartsWith(this ZType<string> input, string text, string error = null)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("Text is mandatory for StartsWith validation", "text");

            if (input.Value == null)
                input.CreateError(error ?? $"{input.PropertyName} {ErrorMessages.IS_REQUIRED}");
            else if (!input.Value.StartsWith(text))
                input.CreateError(error ?? $"{input.PropertyName} does not start with {text}");

            return input;
        }

        public static ZType<string> EndsWith(this ZType<string> input, string text, string error = null)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("Text is mandatory for EndsWith validation", "text");

            if (input.Value == null)
                input.CreateError(error ?? $"{input.PropertyName} {ErrorMessages.IS_REQUIRED}");
            else if (!input.Value.EndsWith(text))
                input.CreateError(error ?? $"{input.PropertyName} does not end with {text}");

            return input;
        }

        public static ZType<string> Regex(this ZType<string> input, string pattern, string error = null)
        {
            if (string.IsNullOrEmpty(pattern))
                throw new ArgumentException("Pattern is mandatory for Regex validation", "pattern");
            
            if (input.Value == null)
                input.CreateError(error ?? $"{input.PropertyName} {ErrorMessages.IS_REQUIRED}");
            else if (!System.Text.RegularExpressions.Regex.IsMatch(input.Value, pattern, RegexOptions.None, TimeSpan.FromMilliseconds(250)))
                input.CreateError(error ?? $"{input.PropertyName} does not match the Regex: {pattern}");
            
            return input;
        }

        public static ZType<string> Email(this ZType<string> input, string error = null)
        {
            if (input.Value == null)
                input.CreateError(error ?? $"{input.PropertyName} {ErrorMessages.IS_REQUIRED}");
            else if (!System.Text.RegularExpressions.Regex.IsMatch(input.Value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
                input.CreateError(error ?? $"{input.PropertyName} is not a valid email");

            return input;
        }

        public static ZType<string> Url(this ZType<string> input, string error = null)
        {
            if (input.Value == null)
                input.CreateError(error ?? $"{input.PropertyName} {ErrorMessages.IS_REQUIRED}");
            else if (!Uri.IsWellFormedUriString(input.Value, UriKind.Absolute))
                input.CreateError(error ?? $"{input.PropertyName} is not a valid URL");

            return input;
        }
    }
}

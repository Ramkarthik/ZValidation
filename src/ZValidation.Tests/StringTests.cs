namespace ZValidation.Tests
{
    public class StringTests
    {

        public StringTests()
        {

        }

        [Fact]
        public void StringIsRequired()
        {
            var test = new TestObject();
            var objectValidation = new ZValidation<TestObject>(test);
            objectValidation.For(x => x.NullString).Required();
            objectValidation.For(x => x.EmptyString).Required(error: "Empty string is mandatory");
            objectValidation.For(x => x.FirstName, propertyName: "First name").Required();

            var stringValidation = new ZValidation<string>(string.Empty);
            stringValidation.For(x => x).Required();

            var successTest = new TestObject() { FirstName = "Test" };
            var successObjectValidation = new ZValidation<TestObject>(successTest);
            successObjectValidation.For(x => x.FirstName).Required();

            Assert.False(objectValidation.IsSuccessful);
            Assert.Equal("Total errors = " + 3, "Total errors = " + objectValidation.Response.Errors.Count());
            Assert.Equal("Total property errors = " + 3, "Total property errors = " + objectValidation.Response.PropertyErrors.Count());
            Assert.Equal("NullString", objectValidation.Response.PropertyErrors.First().Key);
            Assert.Equal("EmptyString", objectValidation.Response.PropertyErrors.Skip(1).First().Key);
            Assert.Equal("First name", objectValidation.Response.PropertyErrors.Skip(2).First().Key);
            Assert.Equal($"NullString {ErrorMessages.IS_REQUIRED}", objectValidation.Response.Errors.First());
            Assert.Equal($"Empty string is mandatory", objectValidation.Response.Errors.Skip(1).First());
            Assert.Equal($"First name {ErrorMessages.IS_REQUIRED}", objectValidation.Response.Errors.Skip(2).First());


            Assert.False(stringValidation.IsSuccessful);
            Assert.Equal($"Field {ErrorMessages.IS_REQUIRED}", stringValidation.Response.Errors.First());

            Assert.True(successObjectValidation.IsSuccessful);
            Assert.Equal("Total errors = " + 0, "Total errors = " + successObjectValidation.Response.Errors.Count());
        }

        [Fact]
        public void StringLengthTest()
        {
            var i = 0;
            while (i < 100)
            {
                var validationLength = new Random().Next(0, 100);
                var stringLength = new Random().Next(0, 100);
                var test = new TestObject() { FirstName = new string('1', stringLength) };
                var objectValidation = new ZValidation<TestObject>(test);
                objectValidation.For(x => x.FirstName).Length(validationLength);
                Assert.Equal(stringLength == validationLength, objectValidation.IsSuccessful);
                if (stringLength != validationLength)
                {
                    Assert.Equal($"FirstName should be {validationLength} character{(validationLength > 1 ? "s" : "")}", objectValidation.Response.Errors.First());
                    Assert.Equal($"FirstName should be {validationLength} character{(validationLength > 1 ? "s" : "")}", objectValidation.Response.PropertyErrors.First().Value.First());
                }
                i++;
            }
        }

        [Fact]
        public void StringMinLengthTest()
        {
            var i = 0;
            while (i < 100)
            {
                var min = new Random().Next(0, 100);
                var stringLength = new Random().Next(0, 100);
                var test = new TestObject() { FirstName = new string('1', stringLength) };
                var objectValidation = new ZValidation<TestObject>(test);
                objectValidation.For(x => x.FirstName).LengthMin(min);
                Assert.Equal(stringLength >= min, objectValidation.IsSuccessful);
                if (stringLength < min)
                {
                    Assert.Equal($"FirstName should be at least {min} character{(min > 1 ? "s" : "")}", objectValidation.Response.Errors.First());
                    Assert.Equal($"FirstName should be at least {min} character{(min > 1 ? "s" : "")}", objectValidation.Response.PropertyErrors.First().Value.First());
                }
                i++;
            }
        }

        [Fact]
        public void StringMaxLengthTest()
        {
            var i = 0;
            while (i < 100)
            {
                var max = new Random().Next(0, 100);
                var stringLength = new Random().Next(0, 100);
                var test = new TestObject() { FirstName = new string('1', stringLength) };
                var objectValidation = new ZValidation<TestObject>(test);
                objectValidation.For(x => x.FirstName).LengthMax(max);
                Assert.Equal(stringLength <= max, objectValidation.IsSuccessful);
                if (stringLength > max)
                {
                    Assert.Equal($"FirstName should be at most {max} character{(max > 1 ? "s" : "")}", objectValidation.Response.Errors.First());
                    Assert.Equal($"FirstName should be at most {max} character{(max > 1 ? "s" : "")}", objectValidation.Response.PropertyErrors.First().Value.First());
                }
                i++;
            }
        }

        [Fact]
        public void StringLengthBetweenTest()
        {
            var i = 0;
            while (i < 100)
            {
                var start = new Random().Next(0, 100);
                var end = new Random().Next(0, 100);
                var stringLength = new Random().Next(0, 100);
                var test = new TestObject() { FirstName = new string('1', stringLength) };
                var objectValidation = new ZValidation<TestObject>(test);
                objectValidation.For(x => x.FirstName).LengthBetween(start, end);
                Assert.Equal(start <= stringLength && stringLength <= end, objectValidation.IsSuccessful);
                if (start > stringLength || stringLength > end)
                {
                    Assert.Equal($"FirstName should be between {start} and {end} characters", objectValidation.Response.Errors.First());
                    Assert.Equal($"FirstName should be between {start} and {end} characters", objectValidation.Response.PropertyErrors.First().Value.First());
                }
                i++;
            }
        }

        [Fact]
        public void StringShouldContain()
        {
            var input = "blahblahShouldContainThisTextblahblah";
            var test = new TestObject() { FirstName = input };
            var objectValidation = new ZValidation<TestObject>(test);
            objectValidation.For(x => x.NullString).Required();
            objectValidation.For(x => x.EmptyString).Required(error: "Empty string is mandatory");
            objectValidation.For(x => x.FirstName, propertyName: "First name").Contains("NotFound", "Oops, does not contain NotFound");

            var stringValidation = new ZValidation<string>(input);
            stringValidation.For(x => x).Contains("NotFound");

            var successTest = new TestObject() { FirstName = input };
            var successObjectValidation = new ZValidation<TestObject>(successTest);
            successObjectValidation.For(x => x.FirstName).Contains("ShouldContainThisText");

            Assert.False(objectValidation.IsSuccessful);
            Assert.Equal("Total errors = " + 3, "Total errors = " + objectValidation.Response.Errors.Count());
            Assert.Equal("Total property errors = " + 3, "Total property errors = " + objectValidation.Response.PropertyErrors.Count());
            Assert.Equal("NullString", objectValidation.Response.PropertyErrors.First().Key);
            Assert.Equal("EmptyString", objectValidation.Response.PropertyErrors.Skip(1).First().Key);
            Assert.Equal("First name", objectValidation.Response.PropertyErrors.Skip(2).First().Key);
            Assert.Equal($"NullString {ErrorMessages.IS_REQUIRED}", objectValidation.Response.Errors.First());
            Assert.Equal($"Empty string is mandatory", objectValidation.Response.Errors.Skip(1).First());
            Assert.Equal($"Oops, does not contain NotFound", objectValidation.Response.Errors.Skip(2).First());


            Assert.False(stringValidation.IsSuccessful);
            Assert.Equal($"Field does not contain NotFound", stringValidation.Response.Errors.First());

            Assert.True(successObjectValidation.IsSuccessful);
            Assert.Equal("Total errors = " + 0, "Total errors = " + successObjectValidation.Response.Errors.Count());
        }

        [Fact]
        public void StringShouldStartWith()
        {
            var input = "ThisTextblahblah";
            var test = new TestObject() { FirstName = input };
            var objectValidation = new ZValidation<TestObject>(test);
            objectValidation.For(x => x.NullString).Required();
            objectValidation.For(x => x.EmptyString).Required(error: "Empty string is mandatory");
            objectValidation.For(x => x.FirstName, propertyName: "First name").StartsWith("NotFound", "Oops, does not start with NotFound");

            var stringValidation = new ZValidation<string>(input);
            stringValidation.For(x => x).StartsWith("NotFound");

            var successTest = new TestObject() { FirstName = input };
            var successObjectValidation = new ZValidation<TestObject>(successTest);
            successObjectValidation.For(x => x.FirstName).StartsWith("ThisText");

            Assert.False(objectValidation.IsSuccessful);
            Assert.Equal("Total errors = " + 3, "Total errors = " + objectValidation.Response.Errors.Count());
            Assert.Equal("Total property errors = " + 3, "Total property errors = " + objectValidation.Response.PropertyErrors.Count());
            Assert.Equal("NullString", objectValidation.Response.PropertyErrors.First().Key);
            Assert.Equal("EmptyString", objectValidation.Response.PropertyErrors.Skip(1).First().Key);
            Assert.Equal("First name", objectValidation.Response.PropertyErrors.Skip(2).First().Key);
            Assert.Equal($"NullString {ErrorMessages.IS_REQUIRED}", objectValidation.Response.Errors.First());
            Assert.Equal($"Empty string is mandatory", objectValidation.Response.Errors.Skip(1).First());
            Assert.Equal($"Oops, does not start with NotFound", objectValidation.Response.Errors.Skip(2).First());


            Assert.False(stringValidation.IsSuccessful);
            Assert.Equal($"Field does not start with NotFound", stringValidation.Response.Errors.First());

            Assert.True(successObjectValidation.IsSuccessful);
            Assert.Equal("Total errors = " + 0, "Total errors = " + successObjectValidation.Response.Errors.Count());
        }

        [Fact]
        public void StringShouldEndWith()
        {
            var input = "blahblahThisText";
            var test = new TestObject() { FirstName = input };
            var objectValidation = new ZValidation<TestObject>(test);
            objectValidation.For(x => x.NullString).Required();
            objectValidation.For(x => x.EmptyString).Required(error: "Empty string is mandatory");
            objectValidation.For(x => x.FirstName, propertyName: "First name").EndsWith("NotFound", "Oops, does not end with NotFound");

            var stringValidation = new ZValidation<string>(input);
            stringValidation.For(x => x).EndsWith("NotFound");

            var successTest = new TestObject() { FirstName = input };
            var successObjectValidation = new ZValidation<TestObject>(successTest);
            successObjectValidation.For(x => x.FirstName).EndsWith("ThisText");

            Assert.False(objectValidation.IsSuccessful);
            Assert.Equal("Total errors = " + 3, "Total errors = " + objectValidation.Response.Errors.Count());
            Assert.Equal("Total property errors = " + 3, "Total property errors = " + objectValidation.Response.PropertyErrors.Count());
            Assert.Equal("NullString", objectValidation.Response.PropertyErrors.First().Key);
            Assert.Equal("EmptyString", objectValidation.Response.PropertyErrors.Skip(1).First().Key);
            Assert.Equal("First name", objectValidation.Response.PropertyErrors.Skip(2).First().Key);
            Assert.Equal($"NullString {ErrorMessages.IS_REQUIRED}", objectValidation.Response.Errors.First());
            Assert.Equal($"Empty string is mandatory", objectValidation.Response.Errors.Skip(1).First());
            Assert.Equal($"Oops, does not end with NotFound", objectValidation.Response.Errors.Skip(2).First());


            Assert.False(stringValidation.IsSuccessful);
            Assert.Equal($"Field does not end with NotFound", stringValidation.Response.Errors.First());

            Assert.True(successObjectValidation.IsSuccessful);
            Assert.Equal("Total errors = " + 0, "Total errors = " + successObjectValidation.Response.Errors.Count());
        }

    }
}
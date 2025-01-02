using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZValidation.Tests
{
    public class Int32Tests
    {
        [Fact]
        public void Int32MinTest()
        {
            var i = 0;
            while (i < 100)
            {
                int min = new Random().Next();
                var test = new TestObject() { Age = new Random().Next() };
                var objectValidation = new ZValidation<TestObject>(test);
                objectValidation.For(x => x.Age).Min(min);

                if (test.Age < min)
                {
                    Assert.Equal($"Age is less than {min}", objectValidation.Response.Errors.First());
                    Assert.Equal($"Age is less than {min}", objectValidation.Response.PropertyErrors.First().Value.First());
                }
                
                i++;
            }

            var successTestObject = new TestObject() { Age = 10 };
            var successObjectValidation = new ZValidation<TestObject>(successTestObject);
            successObjectValidation.For(x => x.Age).Min(10);
            Assert.True(successObjectValidation.IsSuccessful);

            var successIntValidation = new ZValidation<int>(20);
            successIntValidation.For(x => x).Min(20);
            Assert.True(successIntValidation.IsSuccessful);
        }

        [Fact]
        public void Int32MaxTest()
        {
            var i = 0;
            while (i < 100)
            {
                int max = new Random().Next();
                var test = new TestObject() { Age = new Random().Next() };
                var objectValidation = new ZValidation<TestObject>(test);
                objectValidation.For(x => x.Age).Max(max);

                if (test.Age > max)
                {
                    Assert.Equal($"Age is greater than {max}", objectValidation.Response.Errors.First());
                    Assert.Equal($"Age is greater than {max}", objectValidation.Response.PropertyErrors.First().Value.First());
                }

                i++;
            }

            var successTestObject = new TestObject() { Age = 10 };
            var successObjectValidation = new ZValidation<TestObject>(successTestObject);
            successObjectValidation.For(x => x.Age).Max(10);
            Assert.True(successObjectValidation.IsSuccessful);

            var successIntValidation = new ZValidation<int>(20);
            successIntValidation.For(x => x).Max(20);
            Assert.True(successIntValidation.IsSuccessful);
        }
    }
}

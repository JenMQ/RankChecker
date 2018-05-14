namespace RC.Common.Infrastructure.ValidationRules.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass()]
    public class UrlValidationRuleTests
    {
        [TestMethod()]
        public void UrlValidationRule_ValidateTest_InvalidValue()
        {
            var validator = new UrlValidationRule();

            // Empty String
            var result = validator.Validate(string.Empty, null);
            Assert.IsFalse(result.IsValid);

            // Null String
            result = validator.Validate(null, null);
            Assert.IsFalse(result.IsValid);

            // Invalid Type Passed: Not a string, i.e. int
            result = validator.Validate(100, null);
            Assert.IsFalse(result.IsValid);

            // Invalid Uri: Invalid Chars - backslash
            result = validator.Validate("www.somedomain.com\\somefolder", null);
            Assert.IsFalse(result.IsValid);

            // Invalid Uri: Invalid Chars - pipe
            result = validator.Validate("www.somedomain.com/somefolder/q?exp=a|b", null);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod()]
        public void UrlValidationRule_ValidateTest_ValidValue()
        {
            var validator = new UrlValidationRule();
            var result = validator.Validate("www.somedomain.com", null);
            Assert.IsTrue(result.IsValid);
        }
    }
}
namespace RC.Common.Infrastructure.ValidationRules.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass()]
    public class StringIsNotEmptyValidationRuleTests
    {
        [TestMethod()]
        public void StringIsNotEmptyValidationRule_ValidateTest_InvalidValue()
        {
            var validator = new StringIsNotEmptyValidationRule();

            // Empty String
            var result = validator.Validate(string.Empty, null);
            Assert.IsFalse(result.IsValid);

            // Null String
            result = validator.Validate(null, null);
            Assert.IsFalse(result.IsValid);

            // Invalid type: bool
            result = validator.Validate(true, null);
            Assert.IsFalse(result.IsValid);

            // Invalid type: int
            result = validator.Validate(100, null);
            Assert.IsFalse(result.IsValid);
        }
        [TestMethod()]
        public void StringIsNotEmptyValidationRule_ValidateTest_ValidValue()
        {
            var validator = new StringIsNotEmptyValidationRule();

            // String in English
            var result = validator.Validate("Thank you", null);
            Assert.IsTrue(result.IsValid);

            // String with different character
            result = validator.Validate("Danke schön", null);
            Assert.IsTrue(result.IsValid);
        }
    }
}
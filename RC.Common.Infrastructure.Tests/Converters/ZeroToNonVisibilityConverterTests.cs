namespace RC.Common.Infrastructure.Converters.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Windows;

    [TestClass()]
    public class ZeroToNonVisibilityConverterTests
    {
        [TestMethod()]
        public void ConvertTest_Default()
        {
            var defaultTrueValue = Visibility.Visible;
            var defaultFalseValue = Visibility.Collapsed;
            var ztvConverter = new ZeroToNonVisibilityConverter();

            Assert.AreEqual(defaultFalseValue, ztvConverter.Convert(0, typeof(Visibility), null, null));
            Assert.AreEqual(defaultTrueValue, ztvConverter.Convert(13, typeof(Visibility), null, null));
            Assert.AreEqual(defaultTrueValue, ztvConverter.Convert(100, typeof(Visibility), null, null));
        }

        [TestMethod()]
        public void ConvertTest_DifferentFalseValue()
        {
            var defaultTrueValue = Visibility.Visible;
            var newFalseValue = Visibility.Hidden;
            var ztvConverter = new ZeroToNonVisibilityConverter() { FalseValue = newFalseValue };

            Assert.AreEqual(newFalseValue, ztvConverter.Convert(0, typeof(Visibility), null, null));
            Assert.AreEqual(defaultTrueValue, ztvConverter.Convert(100, typeof(Visibility), null, null));
        }

        [TestMethod()]
        public void ConvertTest_ReverseTrueAndFalseValue()
        {
            var newTrueValue = Visibility.Visible;
            var newFalseValue = Visibility.Collapsed;
            var ztvConverter = new ZeroToNonVisibilityConverter() { TrueValue = newTrueValue, FalseValue = newFalseValue };

            Assert.AreEqual(newFalseValue, ztvConverter.Convert(0, typeof(Visibility), null, null));
            Assert.AreEqual(newTrueValue, ztvConverter.Convert(13, typeof(Visibility), null, null));
            Assert.AreEqual(newTrueValue, ztvConverter.Convert(100, typeof(Visibility), null, null));
        }
    }
}
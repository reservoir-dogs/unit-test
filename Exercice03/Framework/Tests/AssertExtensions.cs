using DeepEqual;
using DeepEqual.Formatting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Framework.Tests
{
    public static class AssertExtensions
    {
        public static void AreDeepEqual(this Assert source, object actuel, object attendued)
        {
            var builder = new ComparisonBuilder();

            builder.CustomComparisons.Add(new WithoutCircularityComparison(builder.ComplexObjectComparison));
            var comparison = builder.Create();
            var context = new ComparisonContext();

            var (result, newContext) = comparison.Compare(context, actuel, attendued);

            if (result == ComparisonResult.Fail)
                Assert.Fail(new DeepEqualExceptionMessageBuilder(newContext, builder.GetFormatterFactory()).GetMessage());
        }
    }
}

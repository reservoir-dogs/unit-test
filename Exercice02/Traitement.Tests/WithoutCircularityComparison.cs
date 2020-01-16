using DeepEqual;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Traitement.Tests
{
    public class WithoutCircularityComparison : IComparison
    {
        private readonly IList<ValueTuple<object, object>> verifiedComparisons;
        private readonly IComparison baseComparison;

        public WithoutCircularityComparison(IComparison baseComparison)
          : this(baseComparison, new List<ValueTuple<object, object>>())
        {
            this.baseComparison = baseComparison;
        }

        internal WithoutCircularityComparison(IComparison baseComparison, IList<ValueTuple<object, object>> verifiedComparisons)
        {
            this.baseComparison = baseComparison;
            this.verifiedComparisons = verifiedComparisons;
        }

        public bool CanCompare(Type type1, Type type2)
        {
            return !IsEnumerable(type1) && !IsEnumerable(type2) && baseComparison.CanCompare(type1, type2);
        }

        public (ComparisonResult result, IComparisonContext context) Compare(IComparisonContext context, object value1, object value2)
        {
            var result = (ComparisonResult.Pass, context);

            if (!verifiedComparisons.Contains((value1, value2)) && !verifiedComparisons.Contains((value2, value1)))
            {
                verifiedComparisons.Add((value1, value2));

                result = baseComparison.Compare(context, value1, value2);
            }

            return result;
        }

        public virtual bool IsEnumerable(Type type)
        {
            var result = typeof(IEnumerable).IsAssignableFrom(type);

            return result;
        }
    }
}

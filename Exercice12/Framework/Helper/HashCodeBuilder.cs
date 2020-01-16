using System;
using System.Linq.Expressions;

namespace Framework.Helper
{
    public class HashCodeBuilder<T>
    {
        private readonly T target;
        private int hashCode = 13;

        public HashCodeBuilder(T target)
        {
            this.target = target;
        }

        public int HashCode
        {
            get
            {
                return hashCode;
            }
        }

        public HashCodeBuilder<T> With<TProperty>(Expression<Func<T, TProperty>> propertyOrField)
        {
            var expression = propertyOrField.Body as MemberExpression;
            if (expression == null)
            {
                throw new ArgumentException("Expecting Property or Field Expression of an object");
            }

            var func = propertyOrField.Compile();
            var value = func(target);
            hashCode += (hashCode * 397) ^ ((value == null) ? 0 : value.GetHashCode());

            return this;
        }
    }
}

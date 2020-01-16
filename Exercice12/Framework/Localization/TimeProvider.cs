using System;

namespace Framework.Localization
{
    public abstract class TimeProvider
    {
        private static TimeProvider current = DefaultTimeProvider.Instance;

        public static TimeProvider Current
        {
            get { return current; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                current = value;
            }
        }

        public abstract DateTime UtcNow { get; }

        public static void ResetToDefault()
        {
            current = DefaultTimeProvider.Instance;
        }
    }
}

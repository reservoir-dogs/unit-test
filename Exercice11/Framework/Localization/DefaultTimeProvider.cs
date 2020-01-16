using System;

namespace Framework.Localization
{
    public class DefaultTimeProvider : TimeProvider
    {
        public static readonly TimeProvider Instance = new DefaultTimeProvider();

        public override DateTime UtcNow => DateTime.UtcNow;
    }
}

using NodaTime;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure
{
    public class NodaTimeInterval
    {
        public virtual int Id { get; set; }

        public virtual Interval Interval { get; set; }
        public virtual Interval IntervalWithNullStart { get; set; }
        public virtual Interval IntervalWithNullEnd { get; set; }
        public virtual Interval IntervalWithNullStartEnd { get; set; }
        public virtual Interval? NullableInterval { get; set; }
        public virtual Interval? NullableIntervalWithNull { get; set; }
    }
}
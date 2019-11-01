using NodaTime;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure
{
    public class NodaTimeOffsetTime
    {
        public virtual int Id { get; set; }

        public virtual OffsetTime OffsetTime { get; set; }
        public virtual OffsetTime? NullableOffsetTime { get; set; }
        public virtual OffsetTime? NullableOffsetTimeWithNull { get; set; }
    }
}
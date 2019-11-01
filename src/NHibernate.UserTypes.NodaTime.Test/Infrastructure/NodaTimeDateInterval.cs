using NodaTime;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure
{
    public class NodaTimeDateInterval
    {
        public virtual int Id { get; set; }

        public virtual DateInterval DateInterval { get; set; }
        public virtual DateInterval? NullableDateInterval { get; set; }
        public virtual DateInterval? NullableDateIntervalWithNull { get; set; }
    }
}
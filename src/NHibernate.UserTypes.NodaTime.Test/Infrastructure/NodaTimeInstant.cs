using NodaTime;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure
{
    public class NodaTimeInstant
    {
        public virtual int Id { get; set; }

        public virtual Instant Instant { get; set; }
        public virtual Instant? NullableInstant { get; set; }
        public virtual Instant? NullableInstantWithNull { get; set; }
    }
}
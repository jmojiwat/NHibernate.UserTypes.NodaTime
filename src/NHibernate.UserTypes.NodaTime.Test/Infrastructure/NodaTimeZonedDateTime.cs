using NodaTime;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure
{
    public class NodaTimeZonedDateTime
    {
        public virtual int Id { get; set; }

        public virtual ZonedDateTime ZonedDateTime { get; set; }
        public virtual ZonedDateTime? NullableZonedDateTime { get; set; }
        public virtual ZonedDateTime? NullableZonedDateTimeWithNull { get; set; }
    }
}
using NodaTime;

namespace NHibernate.UserTypes.NodaTime.Tests.Infrastructure
{
    public class NodaTimeDateTimeZone
    {
        public virtual int Id { get; set; }

        public virtual DateTimeZone DateTimeZone { get; set; }
        public virtual DateTimeZone NullableDateTimeZone { get; set; }
        public virtual DateTimeZone NullableDateTimeZoneWithNull { get; set; }
    }
}

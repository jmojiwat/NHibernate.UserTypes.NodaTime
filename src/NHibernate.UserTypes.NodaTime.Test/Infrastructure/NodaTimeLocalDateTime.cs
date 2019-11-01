using NodaTime;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure
{
    public class NodaTimeLocalDateTime
    {
        public virtual int Id { get; set; }

        public virtual LocalDateTime LocalDateTime { get; set; }
        public virtual LocalDateTime? NullableLocalDateTime { get; set; }
        public virtual LocalDateTime? NullableLocalDateTimeWithNull { get; set; }
    }
}
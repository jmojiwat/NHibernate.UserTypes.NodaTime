using NodaTime;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure
{
    public class NodaTimeOffsetDateTime
    {
        public virtual int Id { get; set; }

        public virtual OffsetDateTime OffsetDateTime { get; set; }
        public virtual OffsetDateTime? NullableOffsetDateTime { get; set; }
        public virtual OffsetDateTime? NullableOffsetDateTimeWithNull { get; set; }
    }
}
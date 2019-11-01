using NodaTime;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure
{
    public class NodaTimeOffsetDate
    {
        public virtual int Id { get; set; }

        public virtual OffsetDate OffsetDate { get; set; }
        public virtual OffsetDate? NullableOffsetDate { get; set; }
        public virtual OffsetDate? NullableOffsetDateWithNull { get; set; }
    }
}
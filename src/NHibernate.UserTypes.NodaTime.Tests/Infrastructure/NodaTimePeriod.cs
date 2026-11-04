using NodaTime;

namespace NHibernate.UserTypes.NodaTime.Tests.Infrastructure
{
    public class NodaTimePeriod
    {
        public virtual int Id { get; set; }

        public virtual Period Period { get; set; }
        public virtual Period NullablePeriod { get; set; }
        public virtual Period NullablePeriodWithNull { get; set; }
    }
}
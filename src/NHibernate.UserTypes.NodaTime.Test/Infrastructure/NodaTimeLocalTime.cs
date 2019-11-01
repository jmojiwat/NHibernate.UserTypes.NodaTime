using NodaTime;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure
{
    public class NodaTimeLocalTime
    {
        public virtual int Id { get; set; }

        public virtual LocalTime LocalTime { get; set; }
        public virtual LocalTime? NullableLocalTime { get; set; }
        public virtual LocalTime? NullableLocalTimeWithNull { get; set; }
    }
}
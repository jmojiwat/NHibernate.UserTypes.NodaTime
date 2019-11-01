using NodaTime;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure
{
    public class NodaTimeDuration
    {
        public virtual int Id { get; set; }

        public virtual Duration Duration { get; set; }
        public virtual Duration? NullableDuration { get; set; }
        public virtual Duration? NullableDurationWithNull { get; set; }
    }
}
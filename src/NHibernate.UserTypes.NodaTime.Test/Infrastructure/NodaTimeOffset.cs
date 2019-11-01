using NodaTime;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure
{
    public class NodaTimeOffset
    {
        public virtual int Id { get; set; }

        public virtual Offset Offset { get; set; }
        public virtual Offset? NullableOffset { get; set; }
        public virtual Offset? NullableOffsetWithNull { get; set; }
    }
}
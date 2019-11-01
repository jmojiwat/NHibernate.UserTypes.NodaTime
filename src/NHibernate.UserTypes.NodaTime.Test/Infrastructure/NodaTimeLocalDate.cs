using NodaTime;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure
{
    public class NodaTimeLocalDate
    {
        public virtual int Id { get; set; }

        public virtual LocalDate LocalDate { get; set; }
        public virtual LocalDate? NullableLocalDate { get; set; }
        public virtual LocalDate? NullableLocalDateWithNull { get; set; }
    }
}
using NodaTime;

namespace NHibernate.UserTypes.NodaTime.Tests.Infrastructure
{
    public class NodaTimeYearMonth
    {
        public virtual int Id { get; set; }

        public virtual YearMonth YearMonth { get; set; }
        public virtual YearMonth? NullableYearMonth { get; set; }
        public virtual YearMonth? NullableYearMonthWithNull { get; set; }
    }
}
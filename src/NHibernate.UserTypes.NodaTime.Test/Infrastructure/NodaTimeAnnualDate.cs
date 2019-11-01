using NodaTime;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure
{
    public class NodaTimeAnnualDate
    {
        public virtual int Id { get; set; }

        public virtual AnnualDate AnnualDate { get; set; }
        public virtual AnnualDate? NullableAnnualDate { get; set; }
        public virtual AnnualDate? NullableAnnualDateWithNull { get; set; }
    }
}
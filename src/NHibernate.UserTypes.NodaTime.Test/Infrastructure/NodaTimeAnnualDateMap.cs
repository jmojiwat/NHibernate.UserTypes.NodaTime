using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure
{
    public class NodaTimeAnnualDateMap : ClassMapping<NodaTimeAnnualDate>
    {
        public NodaTimeAnnualDateMap()
        {
            Table("NodaTimeAnnualDates");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            Property(p => p.AnnualDate, 
                m =>
                {
                    m.Columns(
                        cm => cm.Name("AnnualDateMonth"),
                        cm => cm.Name("AnnualDateDay"));
                    m.Type<NodaTimeAnnualDateUserType>();
                });

            Property(p => p.NullableAnnualDate, 
                m =>
                {
                    m.Columns(
                        cm => cm.Name("NullableAnnualDateMonth"),
                        cm => cm.Name("NullableAnnualDateDay"));
                    m.Type<NodaTimeAnnualDateUserType>();
                });

            Property(p => p.NullableAnnualDateWithNull, 
                m =>
                {
                    m.Columns(
                        cm => cm.Name("NullableAnnualDateWithNullMonth"),
                        cm => cm.Name("NullableAnnualDateWithNullDay"));
                    m.Type<NodaTimeAnnualDateUserType>();
                });
        }
    }
}
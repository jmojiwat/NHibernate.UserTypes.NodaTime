using NHibernate.Mapping.ByCode;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure.WithMappingHelpers
{
    public class NodaTimeAnnualDateMap : NodaTimeClassMapping<NodaTimeAnnualDate>
    {
        public NodaTimeAnnualDateMap()
        {
            Table("NodaTimeAnnualDates");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            AnnualDateProperty(p => p.AnnualDate);
            AnnualDateProperty(p => p.NullableAnnualDate);
            AnnualDateProperty(p => p.NullableAnnualDateWithNull);
        }

    }
}
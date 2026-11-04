using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.UserTypes.NodaTime.Tests.Infrastructure.WithNormalMappings
{
    public class NodaTimeAnnualDateMap : ClassMapping<NodaTimeAnnualDate>
    {
        public NodaTimeAnnualDateMap()
        {
            Table("NodaTimeAnnualDates");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            Property(p => p.AnnualDate, 
                m => m.Type<NodaTimeAnnualDateUserType>());

            Property(p => p.NullableAnnualDate, 
                m => m.Type<NodaTimeAnnualDateUserType>());

            Property(p => p.NullableAnnualDateWithNull, 
                m => m.Type<NodaTimeAnnualDateUserType>());
        }

    }
}
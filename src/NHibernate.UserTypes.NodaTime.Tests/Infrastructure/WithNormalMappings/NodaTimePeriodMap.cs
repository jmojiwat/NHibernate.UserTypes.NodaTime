using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.UserTypes.NodaTime.Tests.Infrastructure.WithNormalMappings
{
    public class NodaTimePeriodMap : ClassMapping<NodaTimePeriod>
    {
        public NodaTimePeriodMap()
        {
            Table("NodaTimePeriod");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            Property(p => p.Period, 
                m => m.Type<NodaTimePeriodUserType>());

            Property(p => p.NullablePeriod, 
                m => m.Type<NodaTimePeriodUserType>());

            Property(p => p.NullablePeriodWithNull, 
                m => m.Type<NodaTimePeriodUserType>());
        }
    }
}

using NHibernate.Mapping.ByCode;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure.WithMappingHelpers
{
    public class NodaTimePeriodMap : NodaTimeClassMapping<NodaTimePeriod>
    {
        public NodaTimePeriodMap()
        {
            Table("NodaTimePeriod");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            PeriodProperty(p => p.Period);
            PeriodProperty(p => p.NullablePeriod);
            PeriodProperty(p => p.NullablePeriodWithNull);
       }
    }
}
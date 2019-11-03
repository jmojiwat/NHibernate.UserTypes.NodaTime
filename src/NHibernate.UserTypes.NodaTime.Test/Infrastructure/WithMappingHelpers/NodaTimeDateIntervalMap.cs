using NHibernate.Mapping.ByCode;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure.WithMappingHelpers
{
    public class NodaTimeDateIntervalMap : NodaTimeClassMapping<NodaTimeDateInterval>
    {
        public NodaTimeDateIntervalMap()
        {
            Table("NodaTimeDateInterval");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            DateIntervalProperty(p => p.DateInterval);
            DateIntervalProperty(p => p.NullableDateInterval);
            DateIntervalProperty(p => p.NullableDateIntervalWithNull);
        }
    }
}
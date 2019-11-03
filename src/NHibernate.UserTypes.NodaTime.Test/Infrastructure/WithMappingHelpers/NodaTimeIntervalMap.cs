using NHibernate.Mapping.ByCode;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure.WithMappingHelpers
{
    public class NodaTimeIntervalMap : NodaTimeClassMapping<NodaTimeInterval>
    {
        public NodaTimeIntervalMap()
        {
            Table("NodaTimeInterval");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            IntervalProperty(p => p.Interval);
            IntervalProperty(p => p.IntervalWithNullStart);
            IntervalProperty(p => p.IntervalWithNullEnd);
            IntervalProperty(p => p.IntervalWithNullStartEnd);
            IntervalProperty(p => p.NullableInterval);
            IntervalProperty(p => p.NullableIntervalWithNull);
        }
    }
}
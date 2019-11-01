using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure
{
    public class NodaTimeIntervalMap : ClassMapping<NodaTimeInterval>
    {
        public NodaTimeIntervalMap()
        {
            Table("NodaTimeInterval");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            Property(p => p.Interval, 
                m =>
                {
                    m.Columns(
                        cm => cm.Name("IntervalInterval"),
                        cm => cm.Name("IntervalStart"),
                        cm => cm.Name("IntervalEnd"));
                    m.Type<NodaTimeIntervalUserType>();
                });


            Property(p => p.IntervalWithNullStart, 
                m =>
                {
                    m.Columns(
                        cm => cm.Name("IntervalWithNullStartInterval"),
                        cm => cm.Name("IntervalWithNullStartStart"),
                        cm => cm.Name("IntervalWithNullStartEnd"));
                    m.Type<NodaTimeIntervalUserType>();
                });

            Property(p => p.IntervalWithNullEnd, 
                m =>
                {
                    m.Columns(
                        cm => cm.Name("IntervalWithNullEndInterval"),
                        cm => cm.Name("IntervalWithNullEndStart"),
                        cm => cm.Name("IntervalWithNullEndEnd"));
                    m.Type<NodaTimeIntervalUserType>();
                });

            Property(p => p.IntervalWithNullStartEnd, 
                m =>
                {
                    m.Columns(
                        cm => cm.Name("IntervalWithNullStartEndInterval"),
                        cm => cm.Name("IntervalWithNullStartEndStart"),
                        cm => cm.Name("IntervalWithNullStartEndEnd"));
                    m.Type<NodaTimeIntervalUserType>();
                });

            Property(p => p.NullableInterval, 
                m =>
                {
                    m.Columns(
                        cm => cm.Name("NullableIntervalInterval"),
                        cm => cm.Name("NullableIntervalStart"),
                        cm => cm.Name("NullableIntervalEnd"));
                    m.Type<NodaTimeIntervalUserType>();
                });

            Property(p => p.NullableIntervalWithNull, 
                m =>
                {
                    m.Columns(
                        cm => cm.Name("NullableIntervalWithNullInterval"),
                        cm => cm.Name("NullableIntervalWithNullStart"),
                        cm => cm.Name("NullableIntervalWithNullEnd"));
                    m.Type<NodaTimeIntervalUserType>();
                });
        }
    }
}
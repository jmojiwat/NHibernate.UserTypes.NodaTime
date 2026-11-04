using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.UserTypes.NodaTime.Tests.Infrastructure.WithNormalMappings
{
    public class NodaTimeDateIntervalMap : ClassMapping<NodaTimeDateInterval>
    {
        public NodaTimeDateIntervalMap()
        {
            Table("NodaTimeDateInterval");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            Property(p => p.DateInterval, 
                m =>
                {
                    m.Columns(
                        cm => cm.Name("DateIntervalStart"),
                        cm => cm.Name("DateIntervalEnd"));
                    m.Type<NodaTimeDateIntervalUserType>();
                });

            Property(p => p.NullableDateInterval, 
                m =>
                {
                    m.Columns(
                        cm => cm.Name("NullableDateIntervalStart"),
                        cm => cm.Name("NullableDateIntervalEnd"));
                    m.Type<NodaTimeDateIntervalUserType>();
                });

            Property(p => p.NullableDateIntervalWithNull, 
                m =>
                {
                    m.Columns(
                        cm => cm.Name("NullableDateIntervalStartWithNull"),
                        cm => cm.Name("NullableDateIntervalEndWithNull"));
                    m.Type<NodaTimeDateIntervalUserType>();
                });
        }
    }
}

using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure.WithNormalMappings
{
    public class NodaTimeOffsetTimeMap : ClassMapping<NodaTimeOffsetTime>
    {
        public NodaTimeOffsetTimeMap()
        {
            Table("NodaTimeOffsetTime");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            Property(p => p.OffsetTime, 
                m =>
                {
                    m.Columns(
                        cm => cm.Name("OffsetTimeLocalTime"),
                        cm => cm.Name("OffsetTimeOffset"));
                    m.Type<NodaTimeOffsetTimeUserType>();
                });

            Property(p => p.NullableOffsetTime, 
                m =>
                {
                    m.Columns(
                        cm => cm.Name("NullableOffsetTimeLocalTime"),
                        cm => cm.Name("NullableOffsetTimeOffset"));
                    m.Type<NodaTimeOffsetTimeUserType>();
                });

            Property(p => p.NullableOffsetTimeWithNull, 
                m =>
                {
                    m.Columns(
                        cm => cm.Name("NullableOffsetTimeLocalTimeWithNull"),
                        cm => cm.Name("NullableOffsetTimeOffsetWithNull"));
                    m.Type<NodaTimeOffsetTimeUserType>();
                });
       }
    }
}
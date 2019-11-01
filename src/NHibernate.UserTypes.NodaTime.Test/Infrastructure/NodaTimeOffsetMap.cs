using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure
{
    public class NodaTimeOffsetMap : ClassMapping<NodaTimeOffset>
    {
        public NodaTimeOffsetMap()
        {
            Table("NodaTimeOffset");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            Property(p => p.Offset, 
                m =>
                {
                    m.Column(cm => cm.Name("Offset"));
                    m.Type<NodaTimeOffsetUserType>();
                });

            Property(p => p.NullableOffset, 
                m =>
                {
                    m.Column(cm => cm.Name("NullableOffset"));
                    m.Type<NodaTimeOffsetUserType>();
                });

            Property(p => p.NullableOffsetWithNull, 
                m =>
                {
                    m.Column(cm => cm.Name("NullableOffsetWithNull"));
                    m.Type<NodaTimeOffsetUserType>();
                });
       }
    }
}
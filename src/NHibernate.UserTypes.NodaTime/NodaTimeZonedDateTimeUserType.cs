using System;
using System.Data.Common;
using NHibernate.Engine;
using NHibernate.Type;
using NodaTime;

namespace NHibernate.UserTypes.NodaTime
{
    public class NodaTimeZonedDateTimeUserType : ICompositeUserType
    {
        public object GetPropertyValue(object component, int property)
        {
            var zonedDateTime = (ZonedDateTime)component;
            switch (property)
            {
                case 0:
                    return zonedDateTime.LocalDateTime.InUtc().ToInstant().ToUnixTimeTicks();
                case 1:
                    return zonedDateTime.Zone;
                case 2:
                    return zonedDateTime.Calendar;
                default:
                    throw new ArgumentOutOfRangeException(nameof(property));
            }
        }

        public void SetPropertyValue(object component, int property, object value)
        {
            throw new InvalidOperationException($"{nameof(ZonedDateTime)} is an immutable object. SetPropertyValue is not supported");
        }

        public new bool Equals(object x, object y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
            {
                return false;
            }

            var lhs = (ZonedDateTime)x;
            var rhs = (ZonedDateTime)y;

            return lhs.Equals(rhs);
        }

        public int GetHashCode(object x) => ((ZonedDateTime)x).GetHashCode();

        public object NullSafeGet(DbDataReader dr, string[] names, ISessionImplementor session, object owner)
        {
            var localDateTimeTicks = (long?) NHibernateUtil.Int64.NullSafeGet(dr, names[0], session);
            var zoneId = (string) NHibernateUtil.String.NullSafeGet(dr, names[1], session);
            var calendarId = (string) NHibernateUtil.String.NullSafeGet(dr, names[2], session);
            
            if(localDateTimeTicks == null || string.IsNullOrEmpty(zoneId) || string.IsNullOrEmpty(calendarId))
            {
                return null;
            }
            else
            {
                var instant = Instant.FromUnixTimeTicks(localDateTimeTicks.Value);
                var zone = DateTimeZoneProviders.Tzdb[zoneId];
                var calendar = CalendarSystem.ForId(calendarId);
                
                return new ZonedDateTime(instant, zone, calendar);
            }    
        }

        public void NullSafeSet(DbCommand cmd, object value, int index, bool[] settable, ISessionImplementor session)
        {
            var zonedDateTime = (ZonedDateTime?)value;
            var ticks = zonedDateTime?.LocalDateTime.InUtc().ToInstant().ToUnixTimeTicks();
            var zoneId = zonedDateTime?.Zone.Id;
            var calendarId = zonedDateTime?.Calendar.Id;
            
            if (settable[0])
            {
                NHibernateUtil.Int64.NullSafeSet(cmd, ticks, index++, session);
            }
            if (settable[1])
            {
                NHibernateUtil.String.NullSafeSet(cmd, zoneId, index++, session);
            }
            if (settable[2])
            {
                NHibernateUtil.String.NullSafeSet(cmd, calendarId, index, session);
            }
        }

        public object DeepCopy(object value) => value;

        public object Disassemble(object value, ISessionImplementor session) => value;

        public object Assemble(object cached, ISessionImplementor session, object owner) => cached;

        public object Replace(object original, object target, ISessionImplementor session, object owner) => original;

        public string[] PropertyNames => new[] {"LocalDateTime", "Zone", "Calendar"};
        public IType[] PropertyTypes => new IType[] {NHibernateUtil.Int64, NHibernateUtil.String, NHibernateUtil.String};
        public System.Type ReturnedClass => typeof(ZonedDateTime);
        public bool IsMutable => false;
    }
}
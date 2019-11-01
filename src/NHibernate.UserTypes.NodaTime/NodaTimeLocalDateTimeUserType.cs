using System;
using System.Data.Common;
using NHibernate.Engine;
using NHibernate.Type;
using NodaTime;

namespace NHibernate.UserTypes.NodaTime
{
    public class NodaTimeLocalDateTimeUserType : ICompositeUserType
    {
        public object GetPropertyValue(object component, int property)
        {
            var localDateTime = (LocalDateTime) component;
            switch (property)
            {
                case 0:
                    return localDateTime;
                case 1:
                    return localDateTime.Calendar;
                default:
                    throw new ArgumentOutOfRangeException(nameof(property));
            }
        }

        public void SetPropertyValue(object component, int property, object value)
        {
            throw new InvalidOperationException($"{nameof(LocalDateTime)} is an immutable object. SetPropertyValue is not supported");
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

            var lhs = (LocalDateTime) x;
            var rhs = (LocalDateTime) y;

            return lhs.Equals(rhs);
        }

        public int GetHashCode(object x) => ((LocalDateTime) x).GetHashCode();

        public object NullSafeGet(DbDataReader dr, string[] names, ISessionImplementor session, object owner)
        {
            var ticks = (long?) NHibernateUtil.Int64.NullSafeGet(dr, names[0], session, owner);
            var calendarId = (string) NHibernateUtil.String.NullSafeGet(dr, names[1], session, owner);

            if (ticks == null || string.IsNullOrEmpty(calendarId))
            {
                return null;
            }
            return Instant.FromUnixTimeTicks(ticks.Value).InUtc().LocalDateTime.WithCalendar(CalendarSystem.ForId(calendarId));
        }

        public void NullSafeSet(DbCommand cmd, object value, int index, bool[] settable, ISessionImplementor session)
        {
            var localDateTime = (LocalDateTime?) value;
            var ticks = localDateTime?.InUtc().ToInstant().ToUnixTimeTicks();
            var calendarId = localDateTime?.Calendar.Id;

            if (settable[0])
            {
                NHibernateUtil.Int64.NullSafeSet(cmd, ticks, index++, session);
            }
            if (settable[1])
            {
                NHibernateUtil.String.NullSafeSet(cmd, calendarId, index, session);
            }
        }

        public object DeepCopy(object value) => value;

        public object Disassemble(object value, ISessionImplementor session) => value;

        public object Assemble(object cached, ISessionImplementor session, object owner) => cached;

        public object Replace(object original, object target, ISessionImplementor session, object owner) => original;

        public string[] PropertyNames => new[] {"LocalDateTime", "Calendar"};
        public IType[] PropertyTypes => new IType[] {NHibernateUtil.Int64, NHibernateUtil.String};
        public System.Type ReturnedClass => typeof(LocalDateTime);
        public bool IsMutable => false;
    }
}
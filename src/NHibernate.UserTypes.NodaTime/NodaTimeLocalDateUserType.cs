using System;
using System.Data.Common;
using NHibernate.Engine;
using NHibernate.Type;
using NodaTime;

namespace NHibernate.UserTypes.NodaTime
{
    public class NodaTimeLocalDateUserType : ICompositeUserType
    {
        public object GetPropertyValue(object component, int property)
        {
            var localDate = (LocalDate) component;
            switch (property)
            {
                case 0:
                    return localDate;
                case 1:
                    return localDate.Calendar;
                default:
                    throw new ArgumentOutOfRangeException(nameof(property));
            }
        }

        public void SetPropertyValue(object component, int property, object value)
        {
            throw new InvalidOperationException($"{nameof(LocalDate)} is an immutable object. SetPropertyValue is not supported");
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

            var lhs = (LocalDate) x;
            var rhs = (LocalDate) y;

            return lhs.Equals(rhs);
        }

        public int GetHashCode(object x) => ((LocalDate) x).GetHashCode();

        public object NullSafeGet(DbDataReader dr, string[] names, ISessionImplementor session, object owner)
        {
            var ticks = (long?) NHibernateUtil.Int64.NullSafeGet(dr, names[0], session, owner);
            var calendarId = (string) NHibernateUtil.String.NullSafeGet(dr, names[1], session, owner);

            if (ticks == null || string.IsNullOrEmpty(calendarId))
            {
                return null;
            }
            return Instant.FromUnixTimeTicks(ticks.Value).InUtc().Date.WithCalendar(CalendarSystem.ForId(calendarId));
        }

        public void NullSafeSet(DbCommand cmd, object value, int index, bool[] settable, ISessionImplementor session)
        {
            var localDate = (LocalDate?) value;
            var ticks = localDate?.AtMidnight().InUtc().ToInstant().ToUnixTimeTicks();
            var calendarId = localDate?.Calendar.Id;

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

        public string[] PropertyNames => new[] {"LocalDate", "Calendar"};
        public IType[] PropertyTypes => new IType[] {NHibernateUtil.Int64, NHibernateUtil.String};
        public System.Type ReturnedClass => typeof(LocalDate);
        public bool IsMutable => false;
    }
}
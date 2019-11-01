using System;
using System.Data.Common;
using NHibernate.Engine;
using NHibernate.Type;
using NodaTime;

namespace NHibernate.UserTypes.NodaTime
{
    public class NodaTimeDateIntervalUserType : ICompositeUserType
    {
        public object GetPropertyValue(object component, int property)
        {
            var dateInterval = (DateInterval) component;
            switch (property)
            {
                case 0:
                    return dateInterval.Start;
                case 1:
                    return dateInterval.End;
                case 2:
                    return dateInterval.Calendar;
                default:
                    throw new ArgumentOutOfRangeException(nameof(property));
            }
        }

        public void SetPropertyValue(object component, int property, object value)
        {
            throw new InvalidOperationException($"{nameof(DateInterval)} is an immutable object. SetPropertyValue is not supported");
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

            var lhs = (DateInterval) x;
            var rhs = (DateInterval) y;

            return lhs.Equals(rhs);
        }

        public int GetHashCode(object x) => ((DateInterval) x).GetHashCode();

        public object NullSafeGet(DbDataReader dr, string[] names, ISessionImplementor session, object owner)
        {
            var startTicks = (long?) NHibernateUtil.Int64.NullSafeGet(dr, names[0], session, owner);
            var endTicks = (long?) NHibernateUtil.Int64.NullSafeGet(dr, names[1], session, owner);
            var calendarId = (string) NHibernateUtil.String.NullSafeGet(dr, names[2], session, owner);

            if (startTicks == null || endTicks == null || string.IsNullOrEmpty(calendarId))
            {
                return null;
            }
            return new DateInterval(
                Instant.FromUnixTimeTicks(startTicks.Value).InUtc().Date.WithCalendar(CalendarSystem.ForId(calendarId)),
                Instant.FromUnixTimeTicks(endTicks.Value).InUtc().Date.WithCalendar(CalendarSystem.ForId(calendarId)));
        }

        public void NullSafeSet(DbCommand cmd, object value, int index, bool[] settable, ISessionImplementor session)
        {
            var dateInterval = (DateInterval) value;
            var startTicks = dateInterval?.Start.AtStartOfDayInZone(DateTimeZone.Utc).ToInstant().ToUnixTimeTicks();
            var endTicks = dateInterval?.End.AtStartOfDayInZone(DateTimeZone.Utc).ToInstant().ToUnixTimeTicks();
            var calendarId = dateInterval?.Calendar.Id;

            if (settable[0])
            {
                NHibernateUtil.Int64.NullSafeSet(cmd, startTicks, index++, session);
            }
            if (settable[1])
            {
                NHibernateUtil.Int64.NullSafeSet(cmd, endTicks, index++, session);
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

        public string[] PropertyNames => new[] {"Start", "End", "Calendar"};
        public IType[] PropertyTypes => new IType[] {NHibernateUtil.Int64, NHibernateUtil.Int64, NHibernateUtil.String};
        public System.Type ReturnedClass => typeof(DateInterval);
        public bool IsMutable => false;
    }
}
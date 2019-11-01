using System;
using System.Data.Common;
using NHibernate.Engine;
using NHibernate.Type;
using NodaTime;

namespace NHibernate.UserTypes.NodaTime
{
    public class NodaTimeOffsetDateUserType : ICompositeUserType
    {
        public object GetPropertyValue(object component, int property)
        {
            var offsetDate = (OffsetDate) component;
            switch (property)
            {
                case 0:
                    return offsetDate.Date;
                case 1:
                    return offsetDate.Offset;
                case 2:
                    return offsetDate.Calendar;
                default:
                    throw new ArgumentOutOfRangeException(nameof(property));
            }
        }

        public void SetPropertyValue(object component, int property, object value)
        {
            throw new InvalidOperationException($"{nameof(OffsetDate)} is an immutable object. SetPropertyValue is not supported");
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

            var lhs = (OffsetDate) x;
            var rhs = (OffsetDate) y;

            return lhs.Equals(rhs);
        }

        public int GetHashCode(object x) => ((OffsetDate) x).GetHashCode();

        public object NullSafeGet(DbDataReader dr, string[] names, ISessionImplementor session, object owner)
        {
            var localDateTicks = (long?) NHibernateUtil.Int64.NullSafeGet(dr, names[0], session);
            var offsetNanoseconds = (long?) NHibernateUtil.Int64.NullSafeGet(dr, names[1], session);
            var calendarId = (string) NHibernateUtil.String.NullSafeGet(dr, names[2], session);

            if (localDateTicks == null || offsetNanoseconds == null || string.IsNullOrEmpty(calendarId))
            {
                return null;
            }

            var localDate = Instant.FromUnixTimeTicks(localDateTicks.Value).InUtc().Date;
            localDate = localDate.WithCalendar(CalendarSystem.ForId(calendarId));

            var offset = Offset.FromNanoseconds(offsetNanoseconds.Value);

            return new OffsetDate(localDate, offset);
        }

        public void NullSafeSet(DbCommand cmd, object value, int index, bool[] settable, ISessionImplementor session)
        {
            var offsetDateTime = (OffsetDate?) value;
            var localDate = offsetDateTime?.Date;
            var offset = offsetDateTime?.Offset;
            var calendar = offsetDateTime?.Calendar;

            if (settable[0])
            {
                NHibernateUtil.Int64.NullSafeSet(cmd, localDate?.AtMidnight().InUtc().ToInstant().ToUnixTimeTicks(), index++, session);
            }

            if (settable[1])
            {
                NHibernateUtil.Int64.NullSafeSet(cmd, offset?.Nanoseconds, index++, session);
            }

            if (settable[2])
            {
                NHibernateUtil.String.NullSafeSet(cmd, calendar?.Id, index, session);
            }
        }

        public object DeepCopy(object value) => value;

        public object Disassemble(object value, ISessionImplementor session) => value;

        public object Assemble(object cached, ISessionImplementor session, object owner) => cached;

        public object Replace(object original, object target, ISessionImplementor session, object owner) => original;

        public string[] PropertyNames => new[] {"LocalDate", "Offset", "Calendar"};
        public IType[] PropertyTypes => new IType[] {NHibernateUtil.Int64, NHibernateUtil.Int64, NHibernateUtil.String};
        public System.Type ReturnedClass => typeof(OffsetDate);
        public bool IsMutable => false;
    }
}
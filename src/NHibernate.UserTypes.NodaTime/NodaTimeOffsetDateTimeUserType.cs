using System;
using System.Data.Common;
using NHibernate.Engine;
using NHibernate.Type;
using NodaTime;

namespace NHibernate.UserTypes.NodaTime
{
    public class NodaTimeOffsetDateTimeUserType : ICompositeUserType
    {
        public object GetPropertyValue(object component, int property)
        {
            var offsetDateTime = (OffsetDateTime)component;
            switch (property)
            {
                case 0:
                    return offsetDateTime.LocalDateTime;
                case 1:
                    return offsetDateTime.Offset;
                case 2:
                    return offsetDateTime.Calendar;
                default:
                    throw new ArgumentOutOfRangeException(nameof(property));
            }
        }

        public void SetPropertyValue(object component, int property, object value)
        {
            throw new InvalidOperationException($"{nameof(OffsetDateTime)} is an immutable object. SetPropertyValue is not supported");
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

            var lhs = (OffsetDateTime)x;
            var rhs = (OffsetDateTime)y;

            return lhs.Equals(rhs);
        }

        public int GetHashCode(object x) => ((OffsetDateTime)x).GetHashCode();

        public object NullSafeGet(DbDataReader dr, string[] names, ISessionImplementor session, object owner)
        {
            var localDateTimeTicks = (long?) NHibernateUtil.Int64.NullSafeGet(dr, names[0], session);
            var offsetNanoseconds = (long?) NHibernateUtil.Int64.NullSafeGet(dr, names[1], session);
            var calendarId = (string)NHibernateUtil.String.NullSafeGet(dr, names[2], session);
            
            if(localDateTimeTicks == null || offsetNanoseconds == null || string.IsNullOrEmpty(calendarId))
            {
                return null;
            }
            else
            {
                var localDateTime = Instant.FromUnixTimeTicks(localDateTimeTicks.Value).InUtc().LocalDateTime;
                localDateTime = localDateTime.WithCalendar(CalendarSystem.ForId(calendarId));
                
                var offset = Offset.FromNanoseconds(offsetNanoseconds.Value);
                
                return new OffsetDateTime(localDateTime, offset);
            }
        }

        public void NullSafeSet(DbCommand cmd, object value, int index, bool[] settable, ISessionImplementor session)
        {
            var offsetDateTime = (OffsetDateTime?)value;
            var localDateTime = offsetDateTime?.LocalDateTime;
            var offset = offsetDateTime?.Offset;
            var calendar = offsetDateTime?.Calendar;

            if (settable[0])
            {
                NHibernateUtil.Int64.NullSafeSet(cmd, localDateTime?.InUtc().ToInstant().ToUnixTimeTicks(), index++, session);
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
        public System.Type ReturnedClass => typeof(OffsetDateTime);
        public bool IsMutable => false;
    }
}
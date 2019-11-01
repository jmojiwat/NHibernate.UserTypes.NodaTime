using System;
using System.Data.Common;
using NHibernate.Engine;
using NHibernate.Type;
using NodaTime;

namespace NHibernate.UserTypes.NodaTime
{
    public class NodaTimePeriodUserType : ICompositeUserType
    {
        public object GetPropertyValue(object component, int property)
        {
            var period = (Period)component;
            switch (property)
            {
                case 0:
                    return period.Days;
                case 1:
                    return period.Hours;
                case 2:
                    return period.Milliseconds;
                case 3:
                    return period.Minutes;
                case 4:
                    return period.Months;
                case 5:
                    return period.Nanoseconds;
                case 6:
                    return period.Seconds;
                case 7:
                    return period.Ticks;
                case 8:
                    return period.Weeks;
                case 9:
                    return period.Years;
                default:
                    throw new ArgumentOutOfRangeException(nameof(property));
            }
        }

        public void SetPropertyValue(object component, int property, object value)
        {
            throw new InvalidOperationException($"{nameof(Period)} is an immutable object. SetPropertyValue is not supported");
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

            var lhs = (Period)x;
            var rhs = (Period)y;

            return lhs.Equals(rhs);
        }

        public int GetHashCode(object x) => ((Period)x).GetHashCode();

        public object NullSafeGet(DbDataReader dr, string[] names, ISessionImplementor session, object owner)
        {
            var days = (int?)NHibernateUtil.Int32.NullSafeGet(dr, names[0], session, owner);
            var hours = (long?)NHibernateUtil.Int64.NullSafeGet(dr, names[1], session, owner);
            var milliseconds = (long?)NHibernateUtil.Int64.NullSafeGet(dr, names[2], session, owner);
            var minutes = (long?)NHibernateUtil.Int64.NullSafeGet(dr, names[3], session, owner);

            var months = (int?)NHibernateUtil.Int32.NullSafeGet(dr, names[4], session, owner);
            var nanoseconds = (long?)NHibernateUtil.Int64.NullSafeGet(dr, names[5], session, owner);
            var seconds = (long?)NHibernateUtil.Int64.NullSafeGet(dr, names[6], session, owner);
            var ticks = (long?)NHibernateUtil.Int64.NullSafeGet(dr, names[7], session, owner);

            var weeks = (int?)NHibernateUtil.Int32.NullSafeGet(dr, names[8], session, owner);
            var years = (int?)NHibernateUtil.Int32.NullSafeGet(dr, names[9], session, owner);
            
            if(days == null
               && hours == null
               && milliseconds == null
               && minutes == null
               && months == null
               && nanoseconds == null
               && seconds == null
               && ticks == null
               && weeks == null
               && years == null)
            {
                return null;
            }
            else
            {
                return new PeriodBuilder
                    {
                        Years = years ?? 0,
                        Months = months ?? 0,
                        Weeks = weeks ?? 0,
                        Days = days ?? 0,
                        Hours = hours ?? 0,
                        Minutes = minutes ?? 0,
                        Seconds = seconds ?? 0,
                        Milliseconds = milliseconds ?? 0,
                        Ticks = ticks ?? 0,
                        Nanoseconds = nanoseconds ?? 0
                    }
                    .Build();
            }
        }

        public void NullSafeSet(DbCommand cmd, object value, int index, bool[] settable, ISessionImplementor session)
        {
            var period = (Period)value;
            
            if (settable[0])
            {
                NHibernateUtil.Int32.NullSafeSet(cmd, period?.Days, index++, session);
            }

            if (settable[1])
            {
                NHibernateUtil.Int64.NullSafeSet(cmd, period?.Hours, index++, session);
            }

            if (settable[2])
            {
                NHibernateUtil.Int64.NullSafeSet(cmd, period?.Milliseconds, index++, session);
            }

            if (settable[3])
            {
                NHibernateUtil.Int64.NullSafeSet(cmd, period?.Minutes, index++, session);
            }

            if (settable[4])
            {
                NHibernateUtil.Int32.NullSafeSet(cmd, period?.Months, index++, session);
            }

            if (settable[5])
            {
                NHibernateUtil.Int64.NullSafeSet(cmd, period?.Nanoseconds, index++, session);
            }

            if (settable[6])
            {
                NHibernateUtil.Int64.NullSafeSet(cmd, period?.Seconds, index++, session);
            }

            if (settable[7])
            {
                NHibernateUtil.Int64.NullSafeSet(cmd, period?.Ticks, index++, session);
            }

            if (settable[8])
            {
                NHibernateUtil.Int32.NullSafeSet(cmd, period?.Weeks, index++, session);
            }

            if (settable[9])
            {
                NHibernateUtil.Int32.NullSafeSet(cmd, period?.Years, index, session);
            }
        }

        public object DeepCopy(object value) => value;

        public object Disassemble(object value, ISessionImplementor session) => value;

        public object Assemble(object cached, ISessionImplementor session, object owner) => cached;

        public object Replace(object original, object target, ISessionImplementor session, object owner) => original;

        public string[] PropertyNames => new[]
        {
            "Days", "Hours", "Milliseconds", "Minutes", "Months", "Nanoseconds", "Seconds", "Ticks", "Weeks", "Years"
        };

        public IType[] PropertyTypes => new IType[]
        {
            NHibernateUtil.Int32, NHibernateUtil.Int64, NHibernateUtil.Int64, NHibernateUtil.Int64, NHibernateUtil.Int32,
            NHibernateUtil.Int64, NHibernateUtil.Int64, NHibernateUtil.Int64, NHibernateUtil.Int32, NHibernateUtil.Int32
        };

        public System.Type ReturnedClass => typeof(Period);
        public bool IsMutable => false;
    }
}
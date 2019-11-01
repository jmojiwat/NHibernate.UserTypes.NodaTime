using System;
using System.Data.Common;
using NHibernate.Engine;
using NHibernate.Type;
using NodaTime;

namespace NHibernate.UserTypes.NodaTime
{
    public class NodaTimeOffsetTimeUserType : ICompositeUserType
    {
        public object GetPropertyValue(object component, int property)
        {
            var (localTime, offset) = (OffsetTime)component;
            switch (property)
            {
                case 0:
                    return localTime;
                case 1:
                    return offset;
                default:
                    throw new ArgumentOutOfRangeException(nameof(property));
            }
        }

        public void SetPropertyValue(object component, int property, object value)
        {
            throw new InvalidOperationException($"{nameof(OffsetTime)} is an immutable object. SetPropertyValue is not supported");
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

            var lhs = (OffsetTime)x;
            var rhs = (OffsetTime)y;

            return lhs.Equals(rhs);
        }

        public int GetHashCode(object x) => ((OffsetTime)x).GetHashCode();

        public object NullSafeGet(DbDataReader dr, string[] names, ISessionImplementor session, object owner)
        {
            var localTimeTicks = (long?) NHibernateUtil.Int64.NullSafeGet(dr, names[0], session);
            var offsetNanoseconds = (long?) NHibernateUtil.Int64.NullSafeGet(dr, names[1], session);
            
            if(localTimeTicks == null || offsetNanoseconds == null)
            {
                return null;
            }
            else
            {
                var localTime = LocalTime.FromTicksSinceMidnight(localTimeTicks.Value);
                var offset = Offset.FromNanoseconds(offsetNanoseconds.Value);
                
                return new OffsetTime(localTime, offset);
            }
        }

        public void NullSafeSet(DbCommand cmd, object value, int index, bool[] settable, ISessionImplementor session)
        {
            var offsetTime = (OffsetTime?)value;
            var localTime = offsetTime?.TimeOfDay;
            var offset = offsetTime?.Offset;

            if (settable[0])
            {
                NHibernateUtil.Int64.NullSafeSet(cmd, localTime?.TickOfDay, index++, session);
            }
            if (settable[1])
            {
                NHibernateUtil.Int64.NullSafeSet(cmd, offset?.Nanoseconds, index, session);
            }
        }

        public object DeepCopy(object value) => value;

        public object Disassemble(object value, ISessionImplementor session) => value;

        public object Assemble(object cached, ISessionImplementor session, object owner) => cached;

        public object Replace(object original, object target, ISessionImplementor session, object owner) => original;

        public string[] PropertyNames => new[] {"LocalDate", "Offset"};
        public IType[] PropertyTypes => new IType[] {NHibernateUtil.Int64, NHibernateUtil.Int64};
        public System.Type ReturnedClass => typeof(OffsetTime);
        public bool IsMutable => false;
    }
}
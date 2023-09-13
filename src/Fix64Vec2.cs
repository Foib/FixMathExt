using System;
using System.Globalization;
using System.Numerics;
using System.Text;

namespace FixMath.NET
{
    public struct Fix64Vec2 : IEquatable<Fix64Vec2>, IFormattable
    {
        public Fix64 X;
        public Fix64 Y;

        public static Fix64Vec2 Zero => new Fix64Vec2(Fix64.Zero, Fix64.Zero);
        public static Fix64Vec2 One => new Fix64Vec2(Fix64.One, Fix64.One);
        public static Fix64Vec2 UnitX => new Fix64Vec2(Fix64.One, Fix64.Zero);
        public static Fix64Vec2 UnitY => new Fix64Vec2(Fix64.Zero, Fix64.One);
        public override int GetHashCode()
        {
            int h1 = X.GetHashCode();
            int h2 = Y.GetHashCode();
            return (((h1 << 5) + h1) ^ h2);
        }
        public override bool Equals(object obj)
        {
            if (!(obj is Fix64Vec2))
                return false;
            return Equals((Fix64Vec2)obj);
        }
        public override string ToString()
        {
            return ToString("G", CultureInfo.CurrentCulture);
        }
        public string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }
        public string ToString(string format, IFormatProvider formatProvider)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string numberGroupSeparator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;
            stringBuilder.Append('<');
            stringBuilder.Append(X.ToString());
            stringBuilder.Append(numberGroupSeparator);
            stringBuilder.Append(' ');
            stringBuilder.Append(Y.ToString());
            stringBuilder.Append('>');
            return stringBuilder.ToString();
        }
        public Fix64 Length()
        {
            Fix64 num = X * X + Y * Y;
            return Fix64.Sqrt(num);
        }
        public Fix64 LengthSquared()
        {
            return X * X + Y * Y;
        }
        public static Fix64 Distance(Fix64Vec2 value1, Fix64Vec2 value2)
        {
            Fix64 num1 = value1.X - value2.X;
            Fix64 num2 = value1.Y - value2.Y;
            Fix64 num3 = num1 * num1 + num2 * num2;
            return Fix64.Sqrt(num3);
        }
        public static Fix64 DistanceSquared(Fix64Vec2 value1, Fix64Vec2 value2)
        {
            Fix64 num1 = value1.X - value2.X;
            Fix64 num2 = value1.Y - value2.Y;
            return num1 * num1 + num2 * num2;
        }
        public static Fix64Vec2 Normalize(Fix64Vec2 value1)
        {
            Fix64 num1 = value1.X * value1.X + value1.Y * value1.Y;
            Fix64 num2 = Fix64.One / Fix64.Sqrt(num1);
            return new Fix64Vec2(value1.X * num2, value1.Y * num2);
        }
        public static Fix64Vec2 Reflect(Fix64Vec2 vector, Fix64Vec2 normal)
        {
            Fix64 two = new Fix64(2);
            Fix64 num = vector.X * normal.X + vector.Y * normal.Y;
            return new Fix64Vec2(vector.X - two * num * normal.X, vector.Y - two * num * normal.Y);
        }
        public static Fix64Vec2 Clamp(Fix64Vec2 value1, Fix64Vec2 min, Fix64Vec2 max)
        {
            Fix64 x = value1.X;
            x = (x > max.X) ? max.X : x;
            x = (x < min.X) ? min.X : x;
            Fix64 y = value1.Y;
            y = (y > max.Y) ? max.Y : y;
            y = (y < min.Y) ? min.Y : y;
            return new Fix64Vec2(x, y);
        }
        public static Fix64Vec2 Lerp(Fix64Vec2 value1, Fix64Vec2 value2, Fix64 amount)
        {
            return new Fix64Vec2(value1.X + (value2.X - value1.X) * amount, value1.Y + (value2.Y - value1.Y) * amount);
        }
        public static Fix64Vec2 Transform(Fix64Vec2 position, Fix64Mat3x2 matrix)
        {
            return new Fix64Vec2(position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M31, position.X * matrix.M12 + position.Y * matrix.M22 + matrix.M32);
        }
        //public static Fix64Vec2 Transform(Fix64Vec2 position, Fix64Mat4x4 matrix)
        //{
        //    return new Fix64Vec2(position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M41, position.X * matrix.M12 + position.Y * matrix.M22 + matrix.M42);
        //}
        public static Fix64Vec2 TransformNormal(Fix64Vec2 normal, Fix64Mat3x2 matrix)
        {
            return new Fix64Vec2(normal.X * matrix.M11 + normal.Y * matrix.M21, normal.X * matrix.M12 + normal.Y * matrix.M22);
        }
        //public static Fix64Vec2 TransformNormal(Fix64Vec2 normal, Fix64Mat4x4 matrix)
        //{
        //    return new Fix64Vec2(normal.X * matrix.M11 + normal.Y * matrix.M21, normal.X * matrix.M12 + normal.Y * matrix.M22);
        //}
        //public static Fix64Vec2 Transform(Fix64Vec2 value, Fix64Quat rotation)
        //{
        //    Fix64 num = rotation.X + rotation.X;
        //    Fix64 num2 = rotation.Y + rotation.Y;
        //    Fix64 num3 = rotation.Z + rotation.Z;
        //    Fix64 num4 = rotation.W * num3;
        //    Fix64 num5 = rotation.X * num;
        //    Fix64 num6 = rotation.X * num2;
        //    Fix64 num7 = rotation.Y * num2;
        //    Fix64 num8 = rotation.Z * num3;
        //    return new Fix64Vec2(value.X * (Fix64.One - num7 - num8) + value.Y * (num6 - num4), value.X * (num6 + num4) + value.Y * (Fix64.One - num5 - num8));
        //}
        public static Fix64Vec2 Add(Fix64Vec2 left, Fix64Vec2 right)
        {
            return left + right;
        }
        public static Fix64Vec2 Subtract(Fix64Vec2 left, Fix64Vec2 right)
        {
            return left - right;
        }
        public static Fix64Vec2 Multiply(Fix64Vec2 left, Fix64Vec2 right)
        {
            return left * right;
        }
        public static Fix64Vec2 Multiply(Fix64Vec2 left, Fix64 right)
        {
            return left * right;
        }
        public static Fix64Vec2 Multiply(Fix64 left, Fix64Vec2 right)
        {
            return left * right;
        }
        public static Fix64Vec2 Multiply(Fix64Vec2 vec, Fix64Mat3x2 mat)
        {
            return new Fix64Vec2(vec.X * mat.M11 + vec.Y * mat.M21 + mat.M31, vec.X * mat.M12 + vec.Y * mat.M22 + mat.M32);
        }
        public static Fix64Vec2 Divide(Fix64Vec2 left, Fix64Vec2 right)
        {
            return left / right;
        }
        public static Fix64Vec2 Divide(Fix64Vec2 left, Fix64 divisor)
        {
            return left / divisor;
        }
        public static Fix64Vec2 Negate(Fix64Vec2 value)
        {
            return -value;
        }
        public Fix64Vec2(Fix64 value) : this(value, value)
        {
        }
        public Fix64Vec2(Fix64 x, Fix64 y)
        {
            X = x;
            Y = y;
        }
        public Fix64Vec2(int value) : this(value, value)
        {
        }
        public Fix64Vec2(int x, int y)
        {
            X = (Fix64)x;
            Y = (Fix64)y;
        }
        public Fix64Vec2(long value) : this(value, value)
        {
        }
        public Fix64Vec2(long x, long y)
        {
            X = (Fix64)x;
            Y = (Fix64)y;
        }
        public static explicit operator Vector2(Fix64Vec2 value)
        {
            return new Vector2((float)value.X, (float)value.Y);
        }
        public static explicit operator Fix64Vec2(Vector2 value)
        {
            return new Fix64Vec2((Fix64)value.X, (Fix64)value.Y);
        }
        //public void CopyTo(float[] array, int index)
        //{
        //    if (array == null)
        //    {
        //        throw new NullReferenceException(SR.GetString("Arg_NullArgumentNullRef"));
        //    }

        //    if (index < 0 || index >= array.Length)
        //    {
        //        throw new ArgumentOutOfRangeException(SR.GetString("Arg_ArgumentOutOfRangeException", index));
        //    }

        //    if (array.Length - index < 2)
        //    {
        //        throw new ArgumentException(SR.GetString("Arg_ElementsInSourceIsGreaterThanDestination", index));
        //    }

        //    array[index] = X;
        //    array[index + 1] = Y;
        //}
        public bool Equals(Fix64Vec2 other)
        {
            if (X == other.X)
            {
                return Y == other.Y;
            }

            return false;
        }
        public static Fix64 Dot(Fix64Vec2 value1, Fix64Vec2 value2)
        {
            return value1.X * value2.X + value1.Y * value2.Y;
        }
        public static Fix64Vec2 Min(Fix64Vec2 value1, Fix64Vec2 value2)
        {
            return new Fix64Vec2((value1.X < value2.X) ? value1.X : value2.X, (value1.Y < value2.Y) ? value1.Y : value2.Y);
        }
        public static Fix64Vec2 Max(Fix64Vec2 value1, Fix64Vec2 value2)
        {
            return new Fix64Vec2((value1.X > value2.X) ? value1.X : value2.X, (value1.Y > value2.Y) ? value1.Y : value2.Y);
        }
        public static Fix64Vec2 Abs(Fix64Vec2 value)
        {
            return new Fix64Vec2(Fix64.Abs(value.X), Fix64.Abs(value.Y));
        }
        public static Fix64Vec2 SquareRoot(Fix64Vec2 value)
        {
            return new Fix64Vec2(Fix64.Sqrt(value.X), Fix64.Sqrt(value.Y));
        }
        public static Fix64Vec2 operator +(Fix64Vec2 value1, Fix64Vec2 value2)
        {
            return new Fix64Vec2(value1.X + value2.X, value1.Y + value2.Y);
        }
        public static Fix64Vec2 operator -(Fix64Vec2 left, Fix64Vec2 right)
        {
            return new Fix64Vec2(left.X - right.X, left.Y - right.Y);
        }
        public static Fix64Vec2 operator -(Fix64Vec2 value)
        {
            return Zero - value;
        }
        public static Fix64Vec2 operator *(Fix64Vec2 left, Fix64Vec2 right)
        {
            return new Fix64Vec2(left.X * right.X, left.Y * right.Y);
        }
        public static Fix64Vec2 operator *(Fix64Vec2 left, Fix64 right)
        {
            return new Fix64Vec2(left.X * right, left.Y * right);
        }
        public static Fix64Vec2 operator *(Fix64 left, Fix64Vec2 right)
        {
            return new Fix64Vec2(left * right.X, left * right.Y);
        }
        public static Fix64Vec2 operator *(Fix64Vec2 vec, Fix64Mat3x2 mat)
        {
            return new Fix64Vec2(vec.X * mat.M11 + vec.Y * mat.M21 + mat.M31, vec.X * mat.M12 + vec.Y * mat.M22 + mat.M32);
        }
        public static Fix64Vec2 operator *(Fix64Mat3x2 mat, Fix64Vec2 vec)
        {
            return new Fix64Vec2(vec.X * mat.M11 + vec.Y * mat.M21 + mat.M31, vec.X * mat.M12 + vec.Y * mat.M22 + mat.M32);
        }
        public static Fix64Vec2 operator /(Fix64Vec2 left, Fix64Vec2 right)
        {
            return new Fix64Vec2(left.X / right.X, left.Y / right.Y);
        }
        public static Fix64Vec2 operator /(Fix64Vec2 value1, Fix64 value2)
        {
            return new Fix64Vec2(value1.X / value2, value1.Y / value2);
        }
        public static bool operator ==(Fix64Vec2 left, Fix64Vec2 right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(Fix64Vec2 left, Fix64Vec2 right)
        {
            return !left.Equals(right);
        }
    }
}
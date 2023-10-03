using System;
using System.Globalization;
using System.Text;

namespace FixMathExt;

public struct Fix64Vec3 : IEquatable<Fix64Vec3>, IFormattable
{
    public Fix64 X;
    public Fix64 Y;
    public Fix64 Z;

    public static Fix64Vec3 Zero => new();

    public static Fix64Vec3 One => new(1, 1, 1);

    public static Fix64Vec3 UnitX => new(1, 0, 0);
    public static Fix64Vec3 UnitY => new(0, 1, 0);
    public static Fix64Vec3 UnitZ => new(0, 0, 1);

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
    }

    public override bool Equals(object obj)
    {
        return obj is Fix64Vec3 vec && Equals(vec);
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
        var stringBuilder = new StringBuilder();
        var numberGroupSeparator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;
        stringBuilder.Append('<');
        stringBuilder.Append(X.ToString());
        stringBuilder.Append(numberGroupSeparator);
        stringBuilder.Append(' ');
        stringBuilder.Append(Y.ToString());
        stringBuilder.Append(numberGroupSeparator);
        stringBuilder.Append(' ');
        stringBuilder.Append(Z.ToString());
        stringBuilder.Append('>');
        return stringBuilder.ToString();
    }

    public Fix64 Length()
    {
        return Fix64.Sqrt(X * X + Y * Y + Z * Z);
    }

    public Fix64 LengthSquared()
    {
        return X * X + Y * Y + Z * Z;
    }

    public static Fix64 Distance(Fix64Vec3 value1, Fix64Vec3 value2)
    {
        var num1 = value1.X - value2.X;
        var num2 = value1.Y - value2.Y;
        var num3 = value1.Z - value2.Z;
        return Fix64.Sqrt(num1 * num1 + num2 * num2 + num3 * num3);
    }

    public static Fix64 DistanceSquared(Fix64Vec3 value1, Fix64Vec3 value2)
    {
        var num1 = value1.X - value2.X;
        var num2 = value1.Y - value2.Y;
        var num3 = value1.Z - value2.Z;
        return num1 * num1 + num2 * num2 + num3 * num3;
    }

    public static Fix64Vec3 Normalize(Fix64Vec3 value)
    {
        if (value == Zero)
            return Zero;
        var num1 = Fix64.One / Fix64.Sqrt(value.X * value.X + value.Y * value.Y + value.Z * value.Z);
        return new Fix64Vec3(value.X * num1, value.Y * num1, value.Z * num1);
    }

    public static Fix64Vec3 Cross(Fix64Vec3 vector1, Fix64Vec3 vector2)
    {
        return new Fix64Vec3(
            vector1.Y * vector2.Z - vector1.Z * vector2.Y,
            vector1.Z * vector2.X - vector1.X * vector2.Z,
            vector1.X * vector2.Y - vector1.Y * vector2.X);
    }

    public static Fix64Vec3 Reflect(Fix64Vec3 vector, Fix64Vec3 normal)
    {
        var num1 = vector.X * normal.X + vector.Y * normal.Y + vector.Z * normal.Z;
        var num2 = normal.X * num1 * 2;
        var num3 = normal.Y * num1 * 2;
        var num4 = normal.Z * num1 * 2;
        return new Fix64Vec3(vector.X - num2, vector.Y - num3, vector.Z - num4);
    }

    public static Fix64Vec3 Clamp(Fix64Vec3 value1, Fix64Vec3 min, Fix64Vec3 max)
    {
        var x1 = value1.X;
        var num1 = x1 > max.X ? max.X : x1;
        var x2 = num1 < min.X ? min.X : num1;
        var y1 = value1.Y;
        var num2 = y1 > max.Y ? max.Y : y1;
        var y2 = num2 < min.Y ? min.Y : num2;
        var z1 = value1.Z;
        var num3 = z1 > max.Z ? max.Z : z1;
        var z2 = num3 < min.Z ? min.Z : num3;
        return new(x2, y2, z2);
    }

    public static Fix64Vec3 Lerp(Fix64Vec3 value1, Fix64Vec3 value2, Fix64 amount)
    {
        return new(value1.X + (value2.X - value1.X) * amount, value1.Y + (value2.Y - value1.Y) * amount,
            value1.Z + (value2.Z - value1.Z) * amount);
    }

    //public static Fix64Vec3 Transform(Fix64Vec3 position, Fix64Mat4x4 matrix) => new Fix64Vec3(position.X * matrix.M11 + position.Y * matrix.M21 + position.Z * matrix.M31 + matrix.M41, position.X * matrix.M12 + position.Y * matrix.M22 + position.Z * matrix.M32 + matrix.M42, position.X * matrix.M13 + position.Y * matrix.M23 + position.Z * matrix.M33 + matrix.M43);
    //public static Fix64Vec3 TransformNormal(Fix64Vec3 normal, Fix64Mat4x4 matrix) => new Fix64Vec3(normal.X * matrix.M11 + normal.Y * matrix.M21 + normal.Z * matrix.M31, normal.X * matrix.M12 + normal.Y * matrix.M22 + normal.Z * matrix.M32, normal.X * matrix.M13 + normal.Y * matrix.M23 + normal.Z * matrix.M33);
    //public static Fix64Vec3 Transform(Fix64Vec3 value, Fix64Quat rotation)
    //{
    //    Fix64 num1 = rotation.X + rotation.X;
    //    Fix64 num2 = rotation.Y + rotation.Y;
    //    Fix64 num3 = rotation.Z + rotation.Z;
    //    Fix64 num4 = rotation.W * num1;
    //    Fix64 num5 = rotation.W * num2;
    //    Fix64 num6 = rotation.W * num3;
    //    Fix64 num7 = rotation.X * num1;
    //    Fix64 num8 = rotation.X * num2;
    //    Fix64 num9 = rotation.X * num3;
    //    Fix64 num10 = rotation.Y * num2;
    //    Fix64 num11 = rotation.Y * num3;
    //    Fix64 num12 = rotation.Z * num3;
    //    return new Fix64Vec3(value.X * (Fix64.One - num10 - num12) + value.Y * (num8 - num6) + value.Z * (num9 + num5), value.X * (num8 + num6) + value.Y * (Fix64.One - num7 - num12) + value.Z * (num11 - num4), value.X * (num9 - num5) + value.Y * (num11 + num4) + value.Z * (Fix64.One - num7 - num10));
    //}
    public static Fix64Vec3 Add(Fix64Vec3 left, Fix64Vec3 right)
    {
        return left + right;
    }

    public static Fix64Vec3 Subtract(Fix64Vec3 left, Fix64Vec3 right)
    {
        return left - right;
    }

    public static Fix64Vec3 Multiply(Fix64Vec3 left, Fix64Vec3 right)
    {
        return left * right;
    }

    public static Fix64Vec3 Multiply(Fix64Vec3 left, Fix64 right)
    {
        return left * right;
    }

    public static Fix64Vec3 Multiply(Fix64 left, Fix64Vec3 right)
    {
        return left * right;
    }

    public static Fix64Vec3 Divide(Fix64Vec3 left, Fix64Vec3 right)
    {
        return left / right;
    }

    public static Fix64Vec3 Divide(Fix64Vec3 left, Fix64 divisor)
    {
        return left / divisor;
    }

    public static Fix64Vec3 Negate(Fix64Vec3 value)
    {
        return -value;
    }

    public Fix64Vec3(Fix64 value)
        : this(value, value, value)
    {
    }

    public Fix64Vec3(Fix64Vec2 value, Fix64 z)
        : this(value.X, value.Y, z)
    {
    }

    public Fix64Vec3(Fix64 x, Fix64 y, Fix64 z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public void CopyTo(Fix64[] array)
    {
        CopyTo(array, 0);
    }

    public void CopyTo(Fix64[] array, int index)
    {
        if (array == null)
            throw new NullReferenceException();
        if (index < 0 || index >= array.Length)
            throw new ArgumentOutOfRangeException();
        if (array.Length - index < 3)
            throw new ArgumentException();
        array[index] = X;
        array[index + 1] = Y;
        array[index + 2] = Z;
    }

    public bool Equals(Fix64Vec3 other)
    {
        return X == other.X && Y == other.Y && Z == other.Z;
    }

    public static Fix64 Dot(Fix64Vec3 vector1, Fix64Vec3 vector2)
    {
        return vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z;
    }

    public static Fix64Vec3 Min(Fix64Vec3 value1, Fix64Vec3 value2)
    {
        return new(value1.X < value2.X ? value1.X : value2.X, value1.Y < value2.Y ? value1.Y : value2.Y,
            value1.Z < value2.Z ? value1.Z : value2.Z);
    }

    public static Fix64Vec3 Max(Fix64Vec3 value1, Fix64Vec3 value2)
    {
        return new(value1.X > value2.X ? value1.X : value2.X, value1.Y > value2.Y ? value1.Y : value2.Y,
            value1.Z > value2.Z ? value1.Z : value2.Z);
    }

    public static Fix64Vec3 Abs(Fix64Vec3 value)
    {
        return new(Fix64.Abs(value.X), Fix64.Abs(value.Y), Fix64.Abs(value.Z));
    }

    public static Fix64Vec3 SquareRoot(Fix64Vec3 value)
    {
        return new(Fix64.Sqrt(value.X), Fix64.Sqrt(value.Y), Fix64.Sqrt(value.Z));
    }

    public static Fix64Vec3 operator +(Fix64Vec3 left, Fix64Vec3 right)
    {
        return new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
    }

    public static Fix64Vec3 operator -(Fix64Vec3 left, Fix64Vec3 right)
    {
        return new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
    }

    public static Fix64Vec3 operator -(Fix64Vec3 value)
    {
        return Zero - value;
    }

    public static Fix64Vec3 operator *(Fix64Vec3 left, Fix64Vec3 right)
    {
        return new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
    }

    public static Fix64Vec3 operator *(Fix64Vec3 left, Fix64 right)
    {
        return left * new Fix64Vec3(right);
    }

    public static Fix64Vec3 operator *(Fix64 left, Fix64Vec3 right)
    {
        return new Fix64Vec3(left) * right;
    }

    public static Fix64Vec3 operator /(Fix64Vec3 left, Fix64Vec3 right)
    {
        if (right == Zero)
            throw new DivideByZeroException();
        return new(left.X / right.X, left.Y / right.Y, left.Z / right.Z);
    }

    public static Fix64Vec3 operator /(Fix64Vec3 value1, Fix64 value2)
    {
        if (value2 == Fix64.Zero)
            throw new DivideByZeroException();
        var num = Fix64.One / value2;
        return new Fix64Vec3(value1.X * num, value1.Y * num, value1.Z * num);
    }

    public static bool operator ==(Fix64Vec3 left, Fix64Vec3 right)
    {
        return left.X == right.X && left.Y == right.Y && left.Z == right.Z;
    }

    public static bool operator !=(Fix64Vec3 left, Fix64Vec3 right)
    {
        return left.X != right.X || left.Y != right.Y || left.Z != right.Z;
    }
}
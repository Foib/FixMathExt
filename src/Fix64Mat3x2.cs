using System;
using System.Globalization;

namespace FixMathExt
{
    public struct Fix64Mat3x2 : IEquatable<Fix64Mat3x2>
    {
        public Fix64 M11;
        public Fix64 M12;
        public Fix64 M21;
        public Fix64 M22;
        public Fix64 M31;
        public Fix64 M32;

        private static readonly Fix64Mat3x2 _identity = new Fix64Mat3x2(Fix64.One, Fix64.Zero, Fix64.Zero, Fix64.One, Fix64.Zero, Fix64.Zero);

        public static Fix64Mat3x2 Identity
        {
            get => _identity;
        }

        public bool IsIdentity
        {
            get => M11 == Fix64.One && M22 == Fix64.One && M12 == Fix64.Zero && M21 == Fix64.Zero && M31 == Fix64.Zero && M32 == Fix64.Zero;
        }

        public Fix64Vec2 Translation
        {
            get => new Fix64Vec2(M31, M32);
            set
            {
                M31 = value.X;
                M32 = value.Y;
            }
        }

        public Fix64Mat3x2(Fix64 m11, Fix64 m12, Fix64 m21, Fix64 m22, Fix64 m31, Fix64 m32)
        {
            M11 = m11;
            M12 = m12;
            M21 = m21;
            M22 = m22;
            M31 = m31;
            M32 = m32;
        }

        public static Fix64Mat3x2 CreateTranslation(Fix64Vec2 position)
        {
            Fix64Mat3x2 translation;
            translation.M11 = Fix64.One;
            translation.M12 = Fix64.Zero;
            translation.M21 = Fix64.Zero;
            translation.M22 = Fix64.One;
            translation.M31 = position.X;
            translation.M32 = position.Y;
            return translation;
        }

        public static Fix64Mat3x2 CreateTranslation(Fix64 xPosition, Fix64 yPosition)
        {
            Fix64Mat3x2 translation;
            translation.M11 = Fix64.One;
            translation.M12 = Fix64.Zero;
            translation.M21 = Fix64.Zero;
            translation.M22 = Fix64.One;
            translation.M31 = xPosition;
            translation.M32 = yPosition;
            return translation;
        }

        public static Fix64Mat3x2 CreateScale(Fix64 xScale, Fix64 yScale)
        {
            Fix64Mat3x2 scale;
            scale.M11 = xScale;
            scale.M12 = Fix64.Zero;
            scale.M21 = Fix64.Zero;
            scale.M22 = yScale;
            scale.M31 = Fix64.Zero;
            scale.M32 = Fix64.Zero;
            return scale;
        }

        public static Fix64Mat3x2 CreateScale(Fix64 xScale, Fix64 yScale, Fix64Vec2 centerPoint)
        {
            Fix64 num1 = centerPoint.X * (Fix64.One - xScale);
            Fix64 num2 = centerPoint.Y * (Fix64.One - yScale);
            Fix64Mat3x2 scale;
            scale.M11 = xScale;
            scale.M12 = Fix64.Zero;
            scale.M21 = Fix64.Zero;
            scale.M22 = yScale;
            scale.M31 = num1;
            scale.M32 = num2;
            return scale;
        }

        public static Fix64Mat3x2 CreateScale(Fix64Vec2 scales)
        {
            Fix64Mat3x2 scale;
            scale.M11 = scales.X;
            scale.M12 = Fix64.Zero;
            scale.M21 = Fix64.Zero;
            scale.M22 = scales.Y;
            scale.M31 = Fix64.Zero;
            scale.M32 = Fix64.Zero;
            return scale;
        }

        public static Fix64Mat3x2 CreateScale(Fix64Vec2 scales, Fix64Vec2 centerPoint)
        {
            Fix64 num1 = centerPoint.X * (Fix64.One - scales.X);
            Fix64 num2 = centerPoint.Y * (Fix64.One - scales.Y);
            Fix64Mat3x2 scale;
            scale.M11 = scales.X;
            scale.M12 = Fix64.Zero;
            scale.M21 = Fix64.Zero;
            scale.M22 = scales.Y;
            scale.M31 = num1;
            scale.M32 = num2;
            return scale;
        }

        public static Fix64Mat3x2 CreateScale(Fix64 scale, Fix64Vec2 centerPoint)
        {
            Fix64 num1 = centerPoint.X * (Fix64.One - scale);
            Fix64 num2 = centerPoint.Y * (Fix64.One - scale);
            Fix64Mat3x2 scale1;
            scale1.M11 = scale;
            scale1.M12 = Fix64.Zero;
            scale1.M21 = Fix64.Zero;
            scale1.M22 = scale;
            scale1.M31 = num1;
            scale1.M32 = num2;
            return scale1;
        }

        public static Fix64Mat3x2 CreateScale(Fix64 scale)
        {
            Fix64Mat3x2 scale1;
            scale1.M11 = scale;
            scale1.M12 = Fix64.Zero;
            scale1.M21 = Fix64.Zero;
            scale1.M22 = scale;
            scale1.M31 = Fix64.Zero;
            scale1.M32 = Fix64.Zero;
            return scale1;
        }

        public static Fix64Mat3x2 CreateSkew(Fix64 radiansX, Fix64 radiansY)
        {
            Fix64 num1 = Fix64.Tan(radiansX);
            Fix64 num2 = Fix64.Tan(radiansY);
            Fix64Mat3x2 skew;
            skew.M11 = Fix64.One;
            skew.M12 = num2;
            skew.M21 = num1;
            skew.M22 = Fix64.One;
            skew.M31 = Fix64.Zero;
            skew.M32 = Fix64.Zero;
            return skew;
        }

        public static Fix64Mat3x2 CreateSkew(Fix64 radiansX, Fix64 radiansY, Fix64Vec2 centerPoint)
        {
            Fix64 num1 = Fix64.Tan(radiansX);
            Fix64 num2 = Fix64.Tan(radiansY);
            Fix64 num3 = -centerPoint.Y * num1;
            Fix64 num4 = -centerPoint.X * num2;
            Fix64Mat3x2 skew;
            skew.M11 = Fix64.One;
            skew.M12 = num2;
            skew.M21 = num1;
            skew.M22 = Fix64.One;
            skew.M31 = num3;
            skew.M32 = num4;
            return skew;
        }

        public static Fix64Mat3x2 CreateRotation(Fix64 radians)
        {
            Fix64 a = Fix64.FromRaw(0x124D1);
            Fix64 b = Fix64.FromRaw(0x1921E9072);
            Fix64 c = Fix64.FromRaw(0x19220DA15);
            Fix64 d = Fix64.FromRaw(0x3243E45B7);

            radians = Fix64.Remainder(radians, Fix64.PiTimes2);
            Fix64 num1;
            Fix64 num2;
            if (radians > -a && radians < a)
            {
                num1 = Fix64.One;
                num2 = Fix64.Zero;
            }
            else if (radians > b && radians < Fix64.FromRaw(0x19220DA15))
            {
                num1 = Fix64.Zero;
                num2 = Fix64.One;
            }
            else if (radians < -d || radians > d)
            {
                num1 = -Fix64.One;
                num2 = Fix64.Zero;
            }
            else if (radians > -c && radians < -b)
            {
                num1 = Fix64.Zero;
                num2 = -Fix64.One;
            }
            else
            {
                num1 = Fix64.Cos(radians);
                num2 = Fix64.Sin(radians);
            }
            Fix64Mat3x2 rotation;
            rotation.M11 = num1;
            rotation.M12 = num2;
            rotation.M21 = -num2;
            rotation.M22 = num1;
            rotation.M31 = Fix64.Zero;
            rotation.M32 = Fix64.Zero;
            return rotation;
        }

        public static Fix64Mat3x2 CreateRotation(Fix64 radians, Fix64Vec2 centerPoint)
        {
            Fix64 a = Fix64.FromRaw(0x124D1);
            Fix64 b = Fix64.FromRaw(0x1921E9072);
            Fix64 c = Fix64.FromRaw(0x19220DA15);
            Fix64 d = Fix64.FromRaw(0x3243E45B7);

            radians = Fix64.Remainder(radians, Fix64.PiTimes2);
            Fix64 num1;
            Fix64 num2;
            if (radians > -a && radians < a)
            {
                num1 = Fix64.One;
                num2 = Fix64.Zero;
            }
            else if (radians > b && radians < c)
            {
                num1 = Fix64.Zero;
                num2 = Fix64.One;
            }
            else if (radians < -d || radians > d)
            {
                num1 = -Fix64.One;
                num2 = Fix64.Zero;
            }
            else if (radians > -c && radians < -b)
            {
                num1 = Fix64.Zero;
                num2 = -Fix64.One;
            }
            else
            {
                num1 = Fix64.Cos(radians);
                num2 = Fix64.Sin(radians);
            }
            Fix64 num3 = (centerPoint.X * (Fix64.One - num1) + centerPoint.Y * num2);
            Fix64 num4 = (centerPoint.Y * (Fix64.One - num1) - centerPoint.X * num2);
            Fix64Mat3x2 rotation;
            rotation.M11 = num1;
            rotation.M12 = num2;
            rotation.M21 = -num2;
            rotation.M22 = num1;
            rotation.M31 = num3;
            rotation.M32 = num4;
            return rotation;
        }

        public Fix64 GetDeterminant() => M11 * M22 - M21 * M12;

        public static bool Invert(Fix64Mat3x2 matrix, out Fix64Mat3x2 result)
        {
            Fix64 num1 = matrix.M11 * matrix.M22 - matrix.M21 * matrix.M12;
            if (Fix64.Abs(num1) == Fix64.Zero)
            {
                result = new Fix64Mat3x2(Fix64.Zero, Fix64.Zero, Fix64.Zero, Fix64.Zero, Fix64.Zero, Fix64.Zero);
                return false;
            }
            Fix64 num2 = Fix64.One / num1;
            result.M11 = matrix.M22 * num2;
            result.M12 = -matrix.M12 * num2;
            result.M21 = -matrix.M21 * num2;
            result.M22 = matrix.M11 * num2;
            result.M31 = (matrix.M21 * matrix.M32 - matrix.M31 * matrix.M22) * num2;
            result.M32 = (matrix.M31 * matrix.M12 - matrix.M11 * matrix.M32) * num2;
            return true;
        }

        public static Fix64Mat3x2 Lerp(Fix64Mat3x2 matrix1, Fix64Mat3x2 matrix2, Fix64 amount)
        {
            Fix64Mat3x2 matrix3x2;
            matrix3x2.M11 = matrix1.M11 + (matrix2.M11 - matrix1.M11) * amount;
            matrix3x2.M12 = matrix1.M12 + (matrix2.M12 - matrix1.M12) * amount;
            matrix3x2.M21 = matrix1.M21 + (matrix2.M21 - matrix1.M21) * amount;
            matrix3x2.M22 = matrix1.M22 + (matrix2.M22 - matrix1.M22) * amount;
            matrix3x2.M31 = matrix1.M31 + (matrix2.M31 - matrix1.M31) * amount;
            matrix3x2.M32 = matrix1.M32 + (matrix2.M32 - matrix1.M32) * amount;
            return matrix3x2;
        }

        public static Fix64Mat3x2 Negate(Fix64Mat3x2 value)
        {
            Fix64Mat3x2 matrix3x2;
            matrix3x2.M11 = -value.M11;
            matrix3x2.M12 = -value.M12;
            matrix3x2.M21 = -value.M21;
            matrix3x2.M22 = -value.M22;
            matrix3x2.M31 = -value.M31;
            matrix3x2.M32 = -value.M32;
            return matrix3x2;
        }

        public static Fix64Mat3x2 Add(Fix64Mat3x2 value1, Fix64Mat3x2 value2)
        {
            Fix64Mat3x2 matrix3x2;
            matrix3x2.M11 = value1.M11 + value2.M11;
            matrix3x2.M12 = value1.M12 + value2.M12;
            matrix3x2.M21 = value1.M21 + value2.M21;
            matrix3x2.M22 = value1.M22 + value2.M22;
            matrix3x2.M31 = value1.M31 + value2.M31;
            matrix3x2.M32 = value1.M32 + value2.M32;
            return matrix3x2;
        }

        public static Fix64Mat3x2 Subtract(Fix64Mat3x2 value1, Fix64Mat3x2 value2)
        {
            Fix64Mat3x2 matrix3x2;
            matrix3x2.M11 = value1.M11 - value2.M11;
            matrix3x2.M12 = value1.M12 - value2.M12;
            matrix3x2.M21 = value1.M21 - value2.M21;
            matrix3x2.M22 = value1.M22 - value2.M22;
            matrix3x2.M31 = value1.M31 - value2.M31;
            matrix3x2.M32 = value1.M32 - value2.M32;
            return matrix3x2;
        }

        public static Fix64Mat3x2 Multiply(Fix64Mat3x2 value1, Fix64Mat3x2 value2)
        {
            Fix64Mat3x2 matrix3x2;
            matrix3x2.M11 = (value1.M11 * value2.M11 + value1.M12 * value2.M21);
            matrix3x2.M12 = (value1.M11 * value2.M12 + value1.M12 * value2.M22);
            matrix3x2.M21 = (value1.M21 * value2.M11 + value1.M22 * value2.M21);
            matrix3x2.M22 = (value1.M21 * value2.M12 + value1.M22 * value2.M22);
            matrix3x2.M31 = (value1.M31 * value2.M11 + value1.M32 * value2.M21) + value2.M31;
            matrix3x2.M32 = (value1.M31 * value2.M12 + value1.M32 * value2.M22) + value2.M32;
            return matrix3x2;
        }

        public static Fix64Mat3x2 Multiply(Fix64Mat3x2 value1, Fix64 value2)
        {
            Fix64Mat3x2 matrix3x2;
            matrix3x2.M11 = value1.M11 * value2;
            matrix3x2.M12 = value1.M12 * value2;
            matrix3x2.M21 = value1.M21 * value2;
            matrix3x2.M22 = value1.M22 * value2;
            matrix3x2.M31 = value1.M31 * value2;
            matrix3x2.M32 = value1.M32 * value2;
            return matrix3x2;
        }

        public static Fix64Mat3x2 operator -(Fix64Mat3x2 value)
        {
            Fix64Mat3x2 matrix3x2;
            matrix3x2.M11 = -value.M11;
            matrix3x2.M12 = -value.M12;
            matrix3x2.M21 = -value.M21;
            matrix3x2.M22 = -value.M22;
            matrix3x2.M31 = -value.M31;
            matrix3x2.M32 = -value.M32;
            return matrix3x2;
        }

        public static Fix64Mat3x2 operator +(Fix64Mat3x2 value1, Fix64Mat3x2 value2)
        {
            Fix64Mat3x2 matrix3x2;
            matrix3x2.M11 = value1.M11 + value2.M11;
            matrix3x2.M12 = value1.M12 + value2.M12;
            matrix3x2.M21 = value1.M21 + value2.M21;
            matrix3x2.M22 = value1.M22 + value2.M22;
            matrix3x2.M31 = value1.M31 + value2.M31;
            matrix3x2.M32 = value1.M32 + value2.M32;
            return matrix3x2;
        }

        public static Fix64Mat3x2 operator -(Fix64Mat3x2 value1, Fix64Mat3x2 value2)
        {
            Fix64Mat3x2 matrix3x2;
            matrix3x2.M11 = value1.M11 - value2.M11;
            matrix3x2.M12 = value1.M12 - value2.M12;
            matrix3x2.M21 = value1.M21 - value2.M21;
            matrix3x2.M22 = value1.M22 - value2.M22;
            matrix3x2.M31 = value1.M31 - value2.M31;
            matrix3x2.M32 = value1.M32 - value2.M32;
            return matrix3x2;
        }

        public static Fix64Mat3x2 operator *(Fix64Mat3x2 value1, Fix64Mat3x2 value2)
        {
            Fix64Mat3x2 matrix3x2;
            matrix3x2.M11 = (value1.M11 * value2.M11 + value1.M12 * value2.M21);
            matrix3x2.M12 = (value1.M11 * value2.M12 + value1.M12 * value2.M22);
            matrix3x2.M21 = (value1.M21 * value2.M11 + value1.M22 * value2.M21);
            matrix3x2.M22 = (value1.M21 * value2.M12 + value1.M22 * value2.M22);
            matrix3x2.M31 = (value1.M31 * value2.M11 + value1.M32 * value2.M21) + value2.M31;
            matrix3x2.M32 = (value1.M31 * value2.M12 + value1.M32 * value2.M22) + value2.M32;
            return matrix3x2;
        }

        public static Fix64Mat3x2 operator *(Fix64Mat3x2 value1, Fix64 value2)
        {
            Fix64Mat3x2 matrix3x2;
            matrix3x2.M11 = value1.M11 * value2;
            matrix3x2.M12 = value1.M12 * value2;
            matrix3x2.M21 = value1.M21 * value2;
            matrix3x2.M22 = value1.M22 * value2;
            matrix3x2.M31 = value1.M31 * value2;
            matrix3x2.M32 = value1.M32 * value2;
            return matrix3x2;
        }

        public static bool operator ==(Fix64Mat3x2 value1, Fix64Mat3x2 value2) => value1.M11 == value2.M11 && value1.M22 == value2.M22 && value1.M12 == value2.M12 && value1.M21 == value2.M21 && value1.M31 == value2.M31 && value1.M32 == value2.M32;

        public static bool operator !=(Fix64Mat3x2 value1, Fix64Mat3x2 value2) => value1.M11 != value2.M11 || value1.M12 != value2.M12 || value1.M21 != value2.M21 || value1.M22 != value2.M22 || value1.M31 != value2.M31 || value1.M32 != value2.M32;

        public bool Equals(Fix64Mat3x2 other) => M11 == other.M11 && M22 == other.M22 && M12 == other.M12 && M21 == other.M21 && M31 == other.M31 && M32 == other.M32;

        public override bool Equals(object obj) => obj is Fix64Mat3x2 other && Equals(other);

        public override string ToString()
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            return string.Format(currentCulture, "{{ {{M11:{0} M12:{1}}} {{M21:{2} M22:{3}}} {{M31:{4} M32:{5}}} }}", M11.ToString(), M12.ToString(), M21.ToString(), M22.ToString(), M31.ToString(), M32.ToString());
        }

        public override int GetHashCode() => M11.GetHashCode() + M12.GetHashCode() + M21.GetHashCode() + M22.GetHashCode() + M31.GetHashCode() + M32.GetHashCode();
    }
}
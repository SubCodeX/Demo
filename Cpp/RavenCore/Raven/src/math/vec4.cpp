#include "vec4.h"

namespace Raven
{
	namespace MATH
	{

		vec4::vec4(const float& x, const float& y, const float& z, const float& w)
		{
			this->x = x;
			this->y = y;
			this->z = z;
			this->w = w;
		}

		vec4& vec4::add(const vec4& other)
		{
			x += other.x;
			y += other.y;
			z += other.z;
			w += other.w;

			return *this;
		}

		vec4& vec4::sub(const vec4& other)
		{
			x -= other.x;
			y -= other.y;
			z -= other.z;
			w -= other.w;

			return *this;
		}

		vec4& vec4::mul(const vec4& other)
		{
			x *= other.x;
			y *= other.y;
			z *= other.z;
			w *= other.w;

			return *this;
		}

		vec4& vec4::div(const vec4& other)
		{
			x /= other.x;
			y /= other.y;
			z /= other.z;
			w /= other.w;

			return *this;
		}

		vec4 operator+(vec4 left, const vec4& right) { return left.add(right); }
		vec4 operator-(vec4 left, const vec4& right) { return left.sub(right); }
		vec4 operator*(vec4 left, const vec4& right) { return left.mul(right); }
		vec4 operator/(vec4 left, const vec4& right) { return left.div(right); }

		vec4& vec4::operator+=(const vec4& other) { return add(other); }
		vec4& vec4::operator-=(const vec4& other) { return sub(other); }
		vec4& vec4::operator*=(const vec4& other) { return mul(other); }
		vec4& vec4::operator/=(const vec4& other) { return div(other); }

		bool vec4::operator==(const vec4& other) { return (x == other.x && y == other.y && z == other.z && w == other.w); }
		bool vec4::operator!=(const vec4& other) { return (x != other.x || y != other.y || z != other.z || w != other.w); }

		std::ostream& operator<<(std::ostream& stream, const vec4& vector)
		{
			stream << "(" << vector.x << "," << vector.y << "," << vector.z << "," << vector.w << ")";
			return stream;
		}

	}
}
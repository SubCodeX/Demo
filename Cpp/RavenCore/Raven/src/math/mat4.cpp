#include "mat4.h"

namespace Raven
{
	namespace MATH
	{
		mat4::mat4()
		{
			for (int i = 0; i < 16; i += 1)
			{
				elements[i] = 0;
			}
		}

		mat4::mat4(float diagonal)
		{
			for (int i = 0; i < 16; i += 1)
			{
				elements[i] = 0;
			}
			elements[0 + 0 * 4] = diagonal;
			elements[1 + 1 * 4] = diagonal;
			elements[2 + 2 * 4] = diagonal;
			elements[3 + 3 * 4] = diagonal;
		}

		mat4 mat4::identity()
		{
			return mat4(1.0f);
		}

		mat4& mat4::multiply(const mat4& other)
		{
			float data[16];
			for (int y = 0; y < 4; y += 1)
			{
				for (int x = 0; x < 4; x += 1)
				{
					float sum = 0.0f;
					for (int e = 0; e < 4; e += 1)
					{
						sum += elements[x + e * 4] * other.elements[e + y * 4];
					}
					data[x + y * 4] = sum;
				}
			}

			memcpy(elements, data, 16 * 4);

			return *this;
		}

		mat4 operator*(mat4 left, const mat4& right)
		{
			return left.multiply(right);
			
		}

		vec3 mat4::multiply(const vec3& other) const
		{
			return vec3(	columns[0].x * other.x + columns[1].x * other.y + columns[2].x * other.z + columns[3].x,
							columns[0].y * other.x + columns[1].y * other.y + columns[2].y * other.z + columns[3].y,
							columns[0].z * other.x + columns[1].z * other.y + columns[2].z * other.z + columns[3].z							
						);
		}

		vec3 operator*(const mat4& left, const vec3& right)
		{
			return left.multiply(right);
		}

		vec4 mat4::multiply(const vec4& other) const
		{
			return vec4(	columns[0].x * other.x + columns[1].x * other.y + columns[2].x * other.z + columns[3].x * other.w,
							columns[0].y * other.x + columns[1].y * other.y + columns[2].y * other.z + columns[3].y * other.w,
							columns[0].z * other.x + columns[1].z * other.y + columns[2].z * other.z + columns[3].z * other.w,
							columns[0].w * other.x + columns[1].w * other.y + columns[2].w * other.z + columns[3].w * other.w
						);
		}

		vec4 operator*(const mat4& left, const vec4& right)
		{
			return left.multiply(right);
		}

		mat4& mat4::operator*=(const mat4& other)
		{
			return multiply(other);
		}

		mat4 mat4::orthographic(float left, float right, float bottom, float top, float near, float far)
		{
			mat4 result(1.0f);

			result.elements[0 + 0 * 4] = 2.0f / (right - left);
			result.elements[1 + 1 * 4] = 2.0f / (top - bottom);
			result.elements[2 + 2 * 4] = 2.0f / (near - far);

			result.elements[3 + 0 * 4] = (left + right) / (left - right);
			result.elements[3 + 1 * 4] = (bottom + top) / (bottom - top);
			result.elements[3 + 2 * 4] = (far + near) / (far - near);

			return result;
		}
		
		mat4 mat4::perspective(float fieldOfView, float aspectRatio, float near, float far)
		{
			mat4 result(1.0f);

			float e00 = 1.0f / tan(toRadians(0.5f * fieldOfView));
			float e11 = e00 / aspectRatio;
			float e22 = (near + far) / (near - far);
			float e32 = -1.0f;
			float e23 = (2.0f * near * far) / (near - far);

			result.elements[0 + 0 * 4] = e00;
			result.elements[1 + 1 * 4] = e11;
			result.elements[2 + 2 * 4] = e22;
			result.elements[3 + 2 * 4] = e32;
			result.elements[2 + 3 * 4] = e23;

			return result;
		}

		mat4 mat4::translate(const vec3& translation)
		{
			mat4 result(1.0f);

			result.elements[0 + 3 * 4] = translation.x;
			result.elements[1 + 3 * 4] = translation.y;
			result.elements[2 + 3 * 4] = translation.z;

			return result;
		}

		mat4 mat4::rotate(float angle, const vec3& axis)
		{
			mat4 result(1.0f);

			float radians = toRadians(angle);
			float sine = sin(radians);
			float cosine = cos(radians);
			float invertedCosine = 1.0f - cosine;

			float rotationVectorX = axis.x;
			float rotationVectorY = axis.y;
			float rotationVectorZ = axis.z;

			result.elements[0 + 0 * 4] = rotationVectorX * invertedCosine + cosine;
			result.elements[1 + 0 * 4] = rotationVectorY * rotationVectorX * invertedCosine + rotationVectorZ * sine;
			result.elements[2 + 0 * 4] = rotationVectorX * rotationVectorZ * invertedCosine - rotationVectorY * sine;
			
			result.elements[0 + 1 * 4] = rotationVectorX * rotationVectorY * invertedCosine - rotationVectorZ * sine;
			result.elements[1 + 1 * 4] = rotationVectorY * invertedCosine + cosine;
			result.elements[2 + 1 * 4] = rotationVectorY * rotationVectorZ * invertedCosine + rotationVectorX * sine;

			result.elements[0 + 2 * 4] = rotationVectorX * rotationVectorZ * invertedCosine + rotationVectorY * sine;
			result.elements[1 + 2 * 4] = rotationVectorY * rotationVectorZ * invertedCosine - rotationVectorX * sine;
			result.elements[2 + 2 * 4] = rotationVectorZ * invertedCosine + cosine;

			return result;
		}

		mat4 mat4::scale(const vec3& scaling)
		{
			mat4 result(1.0f);

			result.elements[0 + 0 * 4] = scaling.x;
			result.elements[1 + 1 * 4] = scaling.y;
			result.elements[2 + 2 * 4] = scaling.z;

			return result;
		}

		std::ostream& operator<<(std::ostream& stream, const mat4& matrix)
		{
			//Needs to transpose first, no func for that yet
			stream << "\n(" << matrix.columns[0] << "\n " << matrix.columns[1] << "\n " << matrix.columns[2] << "\n " << matrix.columns[3] << ")\n";
			return stream;
		}

	}
}
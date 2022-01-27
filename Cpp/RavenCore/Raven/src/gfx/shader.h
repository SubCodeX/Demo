#pragma once

#include <iostream>
#include <vector>
#include <GL\glew.h>

#include "..\math\math.h"
#include "..\util\fileIO.h"

namespace Raven
{
	namespace GFX
	{
		class Shader
		{
		public:
			Shader(const char* vspFilePath, const char* fspFilePath);
			~Shader();

			void enable() const;
			void disable() const;
			
			void setUniform1f(const GLchar* name, float value);
			void setUniform1fv(const GLchar* name, float* value, int count);
			void setUniform1i(const GLchar* name, int value);
			void setUniform1iv(const GLchar* name, int* value, int count);
			void setUniform2fv(const GLchar* name, const Raven::MATH::vec2& vector);
			void setUniform3fv(const GLchar* name, const Raven::MATH::vec3& vector);
			void setUniform4fv(const GLchar* name, const Raven::MATH::vec4& vector);
			void setUniformMat4(const GLchar* name, const Raven::MATH::mat4& matrix);

		private:
			GLuint _ID;
			GLint getUniformLocation(const GLchar* name);

			const char* _vspFilePath;
			const char* _fspFilePath;

			GLuint load();
		};
	}
}
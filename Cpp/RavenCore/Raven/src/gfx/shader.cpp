#include "shader.h"

namespace Raven
{
	namespace GFX
	{

		Shader::Shader(const char* vspFilePath, const char* fspFilePath)
		{
			_vspFilePath = vspFilePath;
			_fspFilePath = fspFilePath;

			_ID = load();
		}
		Shader::~Shader()
		{
			glDeleteProgram(_ID);
		}

		void Shader::enable() const
		{
			glUseProgram(_ID);
		}

		void Shader::disable() const
		{
			glUseProgram(0);
		}
		
		void Shader::setUniform1f(const GLchar* name, float value)
		{
			glUniform1f(getUniformLocation(name), value);
		}

		void Shader::setUniform1fv(const GLchar* name, float* value, int count)
		{
			glUniform1fv(getUniformLocation(name), count, value);
		}

		void Shader::setUniform1i(const GLchar* name, int value)
		{
			glUniform1i(getUniformLocation(name), value);
		}

		void Shader::setUniform1iv(const GLchar* name, int* value, int count)
		{
			glUniform1iv(getUniformLocation(name), count, value);
		}

		void Shader::setUniform2fv(const GLchar* name, const Raven::MATH::vec2& vector)
		{
			glUniform2f(getUniformLocation(name), vector.x, vector.y);
		}

		void Shader::setUniform3fv(const GLchar* name, const Raven::MATH::vec3& vector)
		{
			glUniform3f(getUniformLocation(name), vector.x, vector.y, vector.z);
		}

		void Shader::setUniform4fv(const GLchar* name, const Raven::MATH::vec4& vector)
		{
			glUniform4f(getUniformLocation(name), vector.x, vector.y, vector.z, vector.w);
		}

		void Shader::setUniformMat4(const GLchar* name, const Raven::MATH::mat4& matrix)
		{
			glUniformMatrix4fv(getUniformLocation(name), 1, GL_FALSE, matrix.elements);
		}

		GLint Shader::getUniformLocation(const GLchar* name)
		{
			return glGetUniformLocation(_ID, name);
		}

		GLuint Shader::load()
		{
			GLuint shaderProgram	= glCreateProgram();

			GLuint vertexProgram	= glCreateShader(GL_VERTEX_SHADER);
			GLuint fragmentProgram	= glCreateShader(GL_FRAGMENT_SHADER);

			std::string vertexProgramSource = Raven::UTIL::readFileTXT(_vspFilePath);
			std::string fragmentProgramSource = Raven::UTIL::readFileTXT(_fspFilePath);

			const char * vertexProgramSourceCSTR = vertexProgramSource.c_str();
			const char * fragmentProgramSourceCSTR = fragmentProgramSource.c_str();

			glShaderSource(vertexProgram, 1, &vertexProgramSourceCSTR, NULL);
			glShaderSource(fragmentProgram, 1, &fragmentProgramSourceCSTR, NULL);

			glCompileShader(vertexProgram);
			GLint vertexProgramCompileResult;
			glGetShaderiv(vertexProgram, GL_COMPILE_STATUS, &vertexProgramCompileResult);
			
			if (vertexProgramCompileResult == GL_FALSE)
			{
				GLint length;
				glGetShaderiv(vertexProgram, GL_INFO_LOG_LENGTH, &length);
				std::vector<char> error(length);
				glGetShaderInfoLog(vertexProgram, length, &length, &error[0]);
				std::cout << "GLSL Vertex     :" << std::endl << &error[0] << std::endl;
				glDeleteShader(vertexProgram);
				return 0; //Fail;
			}
						
			glCompileShader(fragmentProgram);
			GLint fragmentProgramCompileResult;
			glGetShaderiv(fragmentProgram, GL_COMPILE_STATUS, &fragmentProgramCompileResult);
			
			if (fragmentProgramCompileResult == GL_FALSE)
			{
				GLint length;
				glGetShaderiv(fragmentProgram, GL_INFO_LOG_LENGTH, &length);
				std::vector<char> error(length);
				glGetShaderInfoLog(fragmentProgram, length, &length, &error[0]);
				std::cout << "GLSL Fragment   :" << std::endl << &error[0] << std::endl;
				glDeleteShader(fragmentProgram);
				return 0; //Fail;
			}

			glAttachShader(shaderProgram, vertexProgram);
			glAttachShader(shaderProgram, fragmentProgram);

			glLinkProgram(shaderProgram);
			glValidateProgram(shaderProgram);

			glDeleteShader(vertexProgram);
			glDeleteShader(fragmentProgram);

			return shaderProgram;
		}

	}
}
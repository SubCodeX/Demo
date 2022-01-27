#include "windows.h"


namespace Raven {
	namespace GFX {

		Windows::Windows(const char* title, int width, int height)
		{
			_title = title;
			_width = width;
			_height = height;
			if (!init())
			{
				glfwTerminate();
			}
		}

		Windows::~Windows()
		{
			glfwTerminate();
		}

		bool Windows::init()
		{
			if (glfwInit() == GL_FALSE)
			{
				std::cout << "GLFW Error      : Failed to initialize GLFW!" << std::endl;
				return false;
			}
			else
			{
				std::cout << "GLFW            : Up and running!" << std::endl;

				_window = glfwCreateWindow(_width, _height, _title, NULL, NULL);
				
				if (!_window)
				{
					std::cout << "GLFW Error      : Could not create window!" << std::endl;
					glfwTerminate();
					return false;
				}

				glfwMakeContextCurrent(_window);
				glfwSetWindowUserPointer(_window, this);
				glfwSetWindowSizeCallback(_window, windowResize_callback);
				glfwSetKeyCallback(_window, keyboard_keypress_callback);
				glfwSetMouseButtonCallback(_window, mouse_button_callback);
				glfwSetCursorPosCallback(_window, mouse_position_callback);
				glfwSwapInterval(0.0);

				if (glewInit() != GLEW_OK)
				{
					std::cout << "GLEW Error      : Failed to initialize GLEW!" << std::endl;
					return false;
				}
				else
				{
					std::cout << "GLEW            : Up and running!" << std::endl;
				}

				std::cout << "OpenGL          : Version & Driver (" << glGetString(GL_VERSION) << ")" << std::endl;

				return true;
			}			
		}

		bool Windows::isClosed() const
		{
			return glfwWindowShouldClose(_window) == 1; // Kind of casting to bool
		}

		void Windows::clear() const
		{
			glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
		}

		void Windows::update()
		{
			GLenum error_gl = glGetError();
			if (error_gl != GL_NO_ERROR)
			{
				std::cout << "GL ERROR : " << error_gl << std::endl;
			}

			glfwPollEvents();
			glfwSwapBuffers(_window);
		}


		//STATIC FUNCTIONS 
		void Windows::windowResize_callback(GLFWwindow *window, int width, int height)
		{
			glViewport(0, 0, width, height);
		}

		void Windows::keyboard_keypress_callback(GLFWwindow* window, int key, int scancode, int action, int mods)
		{
			Windows *win = (Windows*) glfwGetWindowUserPointer(window);
			win->keyboard.setKey(key, action != GLFW_RELEASE);
		}

		void Windows::mouse_position_callback(GLFWwindow* window, double xpos, double ypos)
		{
			Windows *win = (Windows*)glfwGetWindowUserPointer(window);
			win->mouse.setXY(xpos, ypos);
		}

		void Windows::mouse_button_callback(GLFWwindow* window, int button, int action, int mods)
		{
			Windows *win = (Windows*)glfwGetWindowUserPointer(window);
			win->mouse.setButton(button, action != GLFW_RELEASE);
		}
	}
}
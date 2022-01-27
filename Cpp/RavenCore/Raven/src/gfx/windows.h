#pragma once

#include <iostream>
#include <GL\glew.h>
#include <GLFW\glfw3.h>

#include "..\io\io.h"

namespace Raven {
	namespace GFX {

		class Windows
		{

		//Members
		public:		

			Raven::IO::Keyboard keyboard;
			Raven::IO::Mouse mouse;

		private:
			int _width, _height;
			const char *_title;

			GLFWwindow *_window;
			bool _closed;


		//Methods
		public:
			Windows(const char* name, int width, int height);
			~Windows();
			bool isClosed() const;

			void clear() const;
			void update();

			inline int getWidth() const { return _width; };
			inline int getHeight() const { return _height; };

		private:
			bool init();

		//Statics
		public:
		private:
			static void windowResize_callback(GLFWwindow *window, int width, int height);
			static void keyboard_keypress_callback(GLFWwindow* window, int key, int scancode, int action, int mods);
			static void mouse_position_callback(GLFWwindow* window, double xpos, double ypos);
			static void mouse_button_callback(GLFWwindow* window, int button, int action, int mods);
		};
	}
}
#include "src\gfx\gfx.h"
#include "src\math\math.h"
#include "src\util\timer.h"

#include <FreeImage.h>
#include <time.h>

const int WINDOW_WIDTH = 1024;
const int WINDOW_HEIGHT = 1024;

int main()
{
	std::cout << "Raven Test App" << std::endl << "--------------" << std::endl;
	
	Raven::GFX::Windows window("Raven GFX", WINDOW_WIDTH, WINDOW_HEIGHT);
		
	Raven::GFX::Shader* shady = new Raven::GFX::Shader("shaders/test.vsp", "shaders/test.fsp");
	Raven::GFX::Shader& shader = *shady;

	Raven::GFX::TileLayer layer(&shader);
	Raven::GFX::Texture* texture1 = new Raven::GFX::Texture("res/SCX32x32x24.png");
	Raven::GFX::Texture* texture2 = new Raven::GFX::Texture("res/SCX32x32x24_2.png");
	Raven::GFX::Texture* texture3 = new Raven::GFX::Texture("res/SCX32x32x24_3.png");
	Raven::GFX::Texture* texture4 = new Raven::GFX::Texture("res/SCX32x32x24_4.png");


	float tileSize = 0.0625f;    // 32x32 1024
	//float tileSize = 0.03125f;   // 64x64 4096
	//float tileSize = 0.015625f;  // 128x128 16384
	//float tileSize = 0.0078125;  // 256x256 65536
	//float tileSize = 0.00390625; // 512x512 262144

	for (float y = -1.0; y < 1.0; y += tileSize)
	{
		for (float x = -1.0; x < 1.0; x += tileSize)
		{
			float C = (rand() % 2550) / 2550.0;
			float R = (rand() % 2550) / 2550.0;
			float G = (rand() % 2550) / 2550.0;
			float B = (rand() % 2550) / 2550.0;
			//layer.add(new Raven::GFX::Sprite(x, y, tileSize, tileSize, Raven::MATH::vec4(C, 1.0f - C, 1.0f - C, 1.0f)));
			//layer.add(new Raven::GFX::Sprite(x, y, tileSize, tileSize, texture1));
			//Raven::MATH::vec4 RC = Raven::MATH::vec4(C, C, C, 1.0f);
			Raven::MATH::vec4 RC = Raven::MATH::vec4(R, G, B, 1.0f);
			switch (rand() % 5)
			{
			
			case 0: layer.add(new Raven::GFX::Sprite(x, y, tileSize, tileSize, texture1, RC)); break;
			case 1: layer.add(new Raven::GFX::Sprite(x, y, tileSize, tileSize, texture2, RC)); break;
			case 2: layer.add(new Raven::GFX::Sprite(x, y, tileSize, tileSize, texture3, RC)); break;
			case 3: layer.add(new Raven::GFX::Sprite(x, y, tileSize, tileSize, texture4, RC)); break;
			case 4: layer.add(new Raven::GFX::Sprite(x, y, tileSize, tileSize, Raven::MATH::vec4(C, 1.0f - C, 1.0f - C, 1.0f))); break;
				/*
			case 0: layer.add(new Raven::GFX::Sprite(x, y, tileSize, tileSize, texture1, RC)); break;
			case 1: layer.add(new Raven::GFX::Sprite(x, y, tileSize, tileSize, texture2, RC)); break;
			case 2: layer.add(new Raven::GFX::Sprite(x, y, tileSize, tileSize, texture3, RC)); break;
			case 3: layer.add(new Raven::GFX::Sprite(x, y, tileSize, tileSize, texture4, RC)); break;
			case 4: layer.add(new Raven::GFX::Sprite(x, y, tileSize, tileSize, RC)); break;
			*/
			}			
		}
	}

	
	GLint texIDs[] =
	{
		0, 1, 2, 3, 4, 5, 6, 7, 8, 9
	};

	shader.enable();
	shader.setUniform1iv("textures", texIDs, 10);
	
	Raven::UTIL::Timer timer;
	timer.reset();
	int FPS = 0;
	while (!window.isClosed())
	{
		
		double x = window.mouse.getX();
		double y = window.getHeight() - window.mouse.getY();
		shader.enable();
		shader.setUniform2fv("mousePosition", Raven::MATH::vec2(((float)x / (double(WINDOW_WIDTH) / 2.0)) - 1.0f, ((float)y / (double(WINDOW_HEIGHT) / 2.0)) - 1.0f));
		
		window.clear();
			
		layer.render();

		window.update();

		FPS += 1;
		if (timer.getTime() > 1000.0f)
		{
			printf("%i FPS\n", FPS);
			timer.reset();
			FPS = 0;
		}		
	}

	delete texture1;
	delete texture2;
	delete texture3;
	delete texture4;

	//system("PAUSE");	
	return 0;
}

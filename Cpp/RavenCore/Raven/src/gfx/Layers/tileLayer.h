#pragma once

#include "layer.h"
#include "..\batchRenderer2D.h"

namespace Raven
{
	namespace GFX
	{

		class TileLayer : public Layer
		{
		public:
			TileLayer(Shader* shader);		
			~TileLayer();
		};

	}
}
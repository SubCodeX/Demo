#pragma once

#include <deque>
#include "staticSprite.h"
#include "renderer2D.h"
//#include "renderable2D.h"

namespace Raven {
	namespace GFX
	{

		class SimpleRenderer2D : public Renderer2D
		{
		private:
			std::deque<StaticSprite*> _renderQue;
		public:
			void submit(const Renderable2D* renderable) override;
			void flush() override;

		};

	}
}
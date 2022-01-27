#pragma once

#include "..\renderable2D.h"

namespace Raven
{
	namespace GFX
	{

		class Group : public Renderable2D
		{
		public:
			Group(const Raven::MATH::mat4& transformationMatrix);
			void add(Renderable2D*  renderable);
			void submit(Renderer2D* renderer) const override;
		private:
			std::vector<Renderable2D*> _renderables;
			Raven::MATH::mat4 _transformationMatrix;
		};

	}
}
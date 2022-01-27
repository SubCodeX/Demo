#include "group.h"

namespace Raven
{
	namespace GFX
	{

		Group::Group(const Raven::MATH::mat4& transformationMatrix)
			: _transformationMatrix(transformationMatrix)
		{
		}

		void Group::add(Renderable2D* renderable)
		{
			_renderables.push_back(renderable);
		}

		void Group::submit(Renderer2D* renderer) const
		{
			renderer->pushTransform(_transformationMatrix);
			for (const Renderable2D* renderable : _renderables)
			{
				renderable->submit(renderer);
			}
			renderer->popTransform();
		}

	}
}
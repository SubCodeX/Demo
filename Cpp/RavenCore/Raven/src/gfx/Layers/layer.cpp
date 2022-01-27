#include "layer.h"

namespace Raven
{
	namespace GFX
	{
		Layer::Layer(Renderer2D* renderer, Shader* shader, Raven::MATH::mat4 projectionMatrix)
			: _renderer(renderer), _shader(shader), _projectionMatrix(projectionMatrix)
		{
			_shader->enable();
			_shader->setUniformMat4("mxProjection", _projectionMatrix);
			_shader->disable();
		}

		Layer::~Layer()
		{
			delete _shader;
			delete _renderer;

			for (int i = 0; i < _renderables.size(); i += 1)
			{
				delete _renderables[i];
			}
		}
		void Layer::add(Renderable2D* renderable)
		{
			_renderables.push_back(renderable);
		}

		void Layer::render()
		{
			_shader->enable();
			_renderer->begin();
			for (const Renderable2D* renderable : _renderables)
			{
				renderable->submit(_renderer);
			}
			_renderer->end();
			_renderer->flush();
			_shader->disable(); 
		}

	}
}
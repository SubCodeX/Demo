#include "simpleRenderer2D.h"
#include "renderable2D.h"

namespace Raven {
	namespace GFX
	{
		void SimpleRenderer2D::submit(const Renderable2D* renderable)
		{
			_renderQue.push_back((StaticSprite*) renderable);
		}

		void SimpleRenderer2D::flush()
		{
			while (!_renderQue.empty())
			{
				const StaticSprite* sprite = _renderQue.front();

				sprite->getVAO()->bind();
				sprite->getIBO()->bind();

				sprite->getShader().setUniformMat4("mxModel", Raven::MATH::mat4::translate(sprite->getPosition()));
				glDrawElements(GL_TRIANGLES, sprite->getIBO()->getCount(), GL_UNSIGNED_SHORT, 0);

				sprite->getIBO()->unbind();
				sprite->getVAO()->unbind();

				_renderQue.pop_front();
			}
		}
		
	}
}
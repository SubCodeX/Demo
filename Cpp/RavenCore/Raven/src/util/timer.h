#pragma once

#include <chrono>
using namespace std;
using namespace chrono;

namespace Raven
{
	namespace UTIL
	{
		class Timer
		{
		public:
			
			void reset()
			{
				start = end = high_resolution_clock::now();
			}

			float getTimeAndReset()
			{
				end = high_resolution_clock::now();
				duration<float> elapsedMS = end - start;
				start = end;
				elapsedMS *= 1000.0f;
				return elapsedMS.count();
			}			

			float getTime()
			{
				end = high_resolution_clock::now();
				duration<float> elapsedMS = end - start;
				elapsedMS *= 1000.0f;
				return elapsedMS.count();
			}

		private:
			time_point<high_resolution_clock> start, end;
		};
	}
}
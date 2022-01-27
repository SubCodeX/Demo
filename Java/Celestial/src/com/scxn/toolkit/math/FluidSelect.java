package com.scxn.toolkit.math;

public class FluidSelect
{
	public Fluid selection[];
		
	public Fluid[] selectX(Fluid[] array, float minimum, float maximum)
	{
		int minIndex = -1;
		int maxIndex = -2;
		float min = Float.MAX_VALUE;
		float max = -Float.MAX_VALUE;
		
		int length = array.length;	
		if (length > 0)
		{
			for (int i = 0; i < length; i+=1)
			{
				float val = array[i].getX();	
				
				if (val >= minimum && val < maximum)
				{
					if (val <= min)
					{
						min = val;
						minIndex = i;
					}
					if (val >= max)
					{
						max = val;
						maxIndex = i;
					}
				}				
			}
		}
		
		if (maxIndex >= minIndex)
		{
			int selectionLength = (maxIndex - minIndex) +1;
			if (selectionLength > 0)
			{
				selection = new Fluid[selectionLength];
				for (int i = 0; i < selectionLength; i+=1 )
				{
					selection[i] = array[minIndex + i];
				}
				return selection;
			}
		}
		
		selection = new Fluid[0];
		return selection;
	}
	
	public Fluid[] selectY(Fluid[] array, float minimum, float maximum)
	{
		int minIndex = -1;
		int maxIndex = -2;
		float min = Float.MAX_VALUE;
		float max = -Float.MAX_VALUE;
		
		int length = array.length;	
		if (length > 0)
		{
			for (int i = 0; i < length; i+=1)
			{
				float val = array[i].getY();
				if (val >= minimum && val < maximum)
				{
					if (val <= min)
					{
						min = val;
						minIndex = i;
					}
					if (val >= max)
					{
						max = val;
						maxIndex = i;
					}
				}
			}
		}
		
		if (maxIndex >= minIndex)
		{
			int selectionLength = (maxIndex - minIndex) +1;
			if (selectionLength > 0)
			{
				selection = new Fluid[selectionLength];
				for (int i = 0; i < selectionLength; i+=1 )
				{
					selection[i] = array[minIndex + i];
				}
				return selection;
			}
		}
		
		selection = new Fluid[0];
		return selection;
	}
	
}

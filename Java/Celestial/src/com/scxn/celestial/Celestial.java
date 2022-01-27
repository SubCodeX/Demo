package com.scxn.celestial;

import java.util.Random;

import com.scxn.toolkit.display.Display;
import com.scxn.toolkit.math.Fluid;
import com.scxn.toolkit.math.FluidSelect;
import com.scxn.toolkit.math.FluidSort;

public class Celestial
{

	private static Display		disp_1;
		
	private static Random		rand	= new Random();
	
	public static void main(String args[])
	{
		
		System.out.println("Celestial 0.01 - Boot sequence");
		System.out.println("----------------------------------------");
		System.out.println("Creating window");
		System.out.println("Position : 50,50");
		System.out.println("Size     : 800x800");
		System.out.println("Scale    : 1:1");

		disp_1 = new Display("Celestial Primary Display", 50, 50, 320, 200, 5, true);

		System.out.println("Done!\n");
		System.out.println("----------------------------------------");
		
		int reset = 0;
		while (true)
		{
			disp_1.updateWindow();
			if (reset==0)
			{
				disp_1.Clear(0xFF000000);	
				disp_1.Test(true, true, true, false);
				reset = 256;
			}
			else 
				{
					reset -= 1;
					//disp_1.HighPass();
					disp_1.LowPass();
				}
			disp_1.Render();
			
//			try
//			{
//				Thread.sleep(10);
//			} catch (InterruptedException e)
//			{
//				// TODO Auto-generated catch block
//				e.printStackTrace();
//			}
		}

	}

}

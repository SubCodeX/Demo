package com.scxn.toolkit.display;

import java.awt.Canvas;
import java.awt.Color;
import java.awt.Dimension;
import java.awt.Graphics;
import java.awt.Insets;
import java.awt.Point;
import java.awt.image.BufferStrategy;
import java.awt.image.BufferedImage;
import java.awt.image.DataBufferInt;
import java.util.Random;

import javax.swing.JFrame;

/*
 * Provides a basic JFrame window with pixel and sprite functions
 */

public class Display extends Canvas
{
	private static final long	serialVersionUID	= 1L;
	private static Random		rand				= new Random();

	private final int			WIDTH;
	private final int			HEIGHT;
	
	private final boolean		BORDER_VISIBLE;
	
	private final int			SCALE;
	private final int			CONTEXT_SIZE;
	
	private int			contextX;
	private int			contextY;

	private JFrame				window;
	private Dimension			windowSize;

	private BufferedImage		frameContext;
	private int					framePixels[];
	private BufferStrategy		frameBuffer;
	private Graphics			frame;
	
	public Display(String title, int x, int y, int width, int height, int scale, boolean border)
	{
		this.WIDTH = width;
		this.HEIGHT = height;
		
		this.BORDER_VISIBLE = border;
		
		this.SCALE = scale;
		this.CONTEXT_SIZE = this.WIDTH * this.HEIGHT;
		
		windowSize = new Dimension(this.WIDTH * this.SCALE, this.HEIGHT
				* this.SCALE);
		setPreferredSize(windowSize);
		window = new JFrame();

		window.setResizable(false);
		window.setUndecorated(!this.BORDER_VISIBLE);
		window.setTitle(title);
		window.add(this);
		window.pack();
		window.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		window.setLocation(x, y);
		window.setVisible(true);
		
		Point windowPosition = window.getLocationOnScreen();		
		Insets contextInsets = window.getInsets();
		
		this.contextX = windowPosition.x + contextInsets.left;
		this.contextY = windowPosition.y + contextInsets.top;

		frameContext = new BufferedImage(this.WIDTH, this.HEIGHT,
				BufferedImage.TYPE_INT_RGB);
		framePixels = ((DataBufferInt) frameContext.getRaster().getDataBuffer())
				.getData();

		frameBuffer = getBufferStrategy();
		if (frameBuffer == null)
		{
			createBufferStrategy(3);
			frameBuffer = getBufferStrategy();
		}

		frame = frameBuffer.getDrawGraphics();
		frame.setColor(Color.BLACK);
		frame.fillRect(0, 0, getWidth(), getHeight());
		frame.dispose();
		frameBuffer.show();
	}

	public void Clear(int color)
	{
		for (int i = 0; i < framePixels.length; i += 1)
			framePixels[i] = color;
	}

	public void Render()
	{
		frame = frameBuffer.getDrawGraphics();
		frame.drawImage(frameContext, 0, 0, getWidth(), getHeight(), null);
		frame.dispose();
		frameBuffer.show();
	}

	public void Test()
	{
		for (int i = 0; i < framePixels.length; i += 1)
		{
			int cComp = rand.nextInt(256);
			framePixels[i] = (cComp<<16)|(cComp<<8)|cComp;
		}			
	}
	
	public void Test(boolean colorR, boolean colorG, boolean colorB, boolean sync)
	{
		for (int i = 0; i < framePixels.length; i += 1)
		{
			if (sync)
			{
			int cComp = rand.nextInt(256);
			int cRGB = 0;
			if (colorR) cRGB = cRGB|(cComp<<16);
			if (colorG) cRGB = cRGB|(cComp<<8);
			if (colorB) cRGB = cRGB|(cComp);
			framePixels[i] = cRGB;
			}
			else
			{
				int cR = rand.nextInt(256);
				int cG = rand.nextInt(256);
				int cB = rand.nextInt(256);
				int cRGB = 0;
				if (colorR) cRGB = cRGB|(cR<<16);
				if (colorG) cRGB = cRGB|(cG<<8);
				if (colorB) cRGB = cRGB|(cB);
				framePixels[i] = cRGB;
			}
		}			
	}
	
	public void Smooth()
	{
		int w = this.WIDTH;
		int h = this.HEIGHT;
		
		int[] buffer = new int[framePixels.length];
		
		for (int x = 0; x < w; x+=1)
		{
			for (int y = 0; y < h; y+=1)
			{
				int R = 0;
				int G = 0;
				int B = 0;
				
				for (int sx=-1; sx <= 1; sx+=1)
				{
					for (int sy=-1; sy <= 1; sy+=1)
					{
						int Color = getPixel(x+sx, y+sy);
						R += getRedComponent(Color);
						G += getGreenComponent(Color);
						B += getBlueComponent(Color);
					}
				}
				
				R /= 9; if (R > 255) R = 255;
				G /= 9; if (G > 255) G = 255;
				B /= 9; if (B > 255) B = 255;
				
				buffer[x + (y * this.WIDTH)] = (R<<16)|(G<<8)|B;
				
			}
		}
		
		for (int i = 0; i < framePixels.length; i+=1) framePixels[i] = buffer[i];
	}
	
	public void HighPass()
	{
		int w = this.WIDTH;
		int h = this.HEIGHT;
		
		int[] buffer = new int[framePixels.length];
		
		for (int x = 0; x < w; x+=1)
		{
			for (int y = 0; y < h; y+=1)
			{
				int Color = getPixel(x, y);
				int R = getRedComponent(Color);
				int G = getGreenComponent(Color);
				int B = getBlueComponent(Color);
				
				if (R < 255) R+=1;
				if (G < 255) G+=1;
				if (B < 255) B+=1;
				
				buffer[x + (y * this.WIDTH)] = (R<<16)|(G<<8)|B;				
			}
		}
		
		for (int i = 0; i < framePixels.length; i+=1) framePixels[i] = buffer[i];
	}
	
	public void LowPass()
	{
		int w = this.WIDTH;
		int h = this.HEIGHT;
		
		int[] buffer = new int[framePixels.length];
		
		for (int x = 0; x < w; x+=1)
		{
			for (int y = 0; y < h; y+=1)
			{
				int Color = getPixel(x, y);
				int R = getRedComponent(Color);
				int G = getGreenComponent(Color);
				int B = getBlueComponent(Color);
				
				if (R > 0) R-=1;
				if (G > 0) G-=1;
				if (B > 0) B-=1;
				
				buffer[x + (y * this.WIDTH)] = (R<<16)|(G<<8)|B;				
			}
		}
		
		for (int i = 0; i < framePixels.length; i+=1) framePixels[i] = buffer[i];
	}
	
	
	public void updateWindow()
	{
		Point windowPosition = window.getLocationOnScreen();		
		Insets contextInsets = window.getInsets();
		
		this.contextX = windowPosition.x + contextInsets.left;
		this.contextY = windowPosition.y + contextInsets.top;
	}
	
	public int getContextX()
	{
		return this.contextX;
	}
	
	public int getContextY()
	{
		return this.contextY;
	}
	
	public void setPosition(int x, int y)
	{
		window.setLocation(x, y);
		window.requestFocus();
	}

	public void setPixel(int x, int y, int color)
	{
		int index = x + (y * this.WIDTH);
		if (index < 0 || index >= this.CONTEXT_SIZE)
			return;
		framePixels[index] = color & 0x00FFFFFF;
	}
	
	public int getPixel(int x, int y)
	{
		int index = x + (y * this.WIDTH);
		if (index < 0 || index >= this.CONTEXT_SIZE)
			return 0;
		return framePixels[index];
	}
	
	public int getRedComponent(int Color)
	{
		return (Color >> 16 & 0x000000FF);
	}
	
	public int getGreenComponent(int Color)
	{
		return (Color >> 8 & 0x000000FF);
	}
	
	public int getBlueComponent(int Color)
	{
		return (Color & 0x000000FF);
	}
}

package com.scxn.toolkit.math;

public class Vector
{

	private float x;
	private float y;
	
	public Vector()
	{
		this.x = 1.0f;
		this.y = 0.0f;
	}
	
	public Vector(float x, float y)
	{
		this.x = x;
		this.y = y;
	}
	public Vector(Vector v)
	{
		this.x = v.x;
		this.y = v.y;
	}
	
	public void zero()
	{
		this.x = 0;
		this.y = 0;
	}
	
	public void normalize()
	{
		float length = this.getLength();
		this.x /= length;
		this.y /= length;
	}
	
	public void setAngle(float angle)
	{
		float length = this.getLength();
		this.x = (float)Math.cos(angle) * length;
		this.y = (float)Math.sin(angle) * length;
	}
	
	public float getAngle()
	{
		return (float)Math.atan2(y, x);
	}
	
	public void setDegAngle(float degAngle)
	{
		float angle = degAngle * (float)Math.PI / 180.0f;
		float length = this.getLength();
		this.x = (float)Math.cos(angle) *  length;
		this.y = (float)Math.sin(angle) *  length;
	}
	
	public float getDegAngle()
	{
		return (float)(Math.atan2(y, x) * 180.0f / Math.PI);
	}
	
	public void setLength(float length)
	{
		float angle = this.getAngle();		
		this.x = (float)Math.cos(angle) * length;
		this.y = (float)Math.sin(angle) * length;		
	}
	
	public float getLength()
	{
		return (float)Math.sqrt((x*x)+(y*y));
	}
	
	public Vector add(Vector v)
	{
		return new Vector(this.x + v.x, this.y + v.y);
	}
	
	public Vector sub(Vector v)
	{
		return new Vector(this.x - v.x, this.y - v.y);
	}
	
	public Vector mul(float r)
	{
		return new Vector(this.x * r, this.y * r);
	}
	
	public Vector div(float r)
	{
		return new Vector(this.x / r, this.y / r);
	}
	
	public void addTo(Vector v)
	{
		this.x += v.x;
		this.y += v.y;
	}
	
	public void subFrom(Vector v)
	{
		this.x -= v.x;
		this.y -= v.y;
	}
	
	public void mulBy(float r)
	{
		this.x *= r;
		this.y *= r;
	}
	
	public void divBy(float r)
	{
		this.x /= r;
		this.y /= r;
	}
	

	public float getX()
	{
		return x;
	}

	public void setX(float x)
	{
		this.x = x;
	}

	public float getY()
	{
		return y;
	}

	public void setY(float y)
	{
		this.y = y;
	}
	
	
	
}

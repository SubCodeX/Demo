package com.scxn.toolkit.math;

public class Fluid 
{
	private float viscosity;
	
	private Vector position;
	private Vector velocity;
	private Vector acceleration;
	
	public int gridX, gridY;
	private int index;
	
	private float radius;
	private float mass;
	private float gridSize = 1;
	
	
	
	public Fluid(int index)
	{
		this.viscosity = 0.9f;
		this.position = new Vector(0.0f, 0.0f);
		this.velocity = new Vector(0.0f, 0.0f);
		this.acceleration = new Vector(0.0f, 0.0f);
		this.radius = 1.0f;
		this.mass = 1.0f;	
		this.gridX = 1;
		this.gridY = 1;
		this.index = index;
	}
	
	public Fluid(Vector position, Vector velocity, Vector acceleration)
	{
		this.position = position;
		this.velocity = velocity;
		this.acceleration = acceleration;
	}
	
	public Fluid(float xPosition, float yPosition, float xVelocity, float yVelocity, float xAcceleration, float yAcceleration)
	{
		this.position = new Vector(xPosition, yPosition);
		this.velocity = new Vector(xVelocity, yVelocity);
		this.acceleration = new Vector(xAcceleration, yAcceleration);
	}
	
	public void setGridSize(float size)
	{
		this.gridSize = size;
	}
	
	private void updateGrid()
	{
		this.gridX = (int)(this.position.getX()/this.gridSize);
		this.gridY = (int)(this.position.getY()/this.gridSize);
	}
	
	public int ID()
	{
		return this.index;
	}
	
	public void ID(int id)
	{
		this.index = id;
	}
	
	public void setPosition(float x, float y)
	{
		this.position.setX(x);
		this.position.setY(y);
		
		this.updateGrid();
	}
	
	public float getX()
	{
		return this.position.getX();
	}
	
	public float getY()
	{
		return this.position.getY();
	}
	
	public void setX(float x)
	{
		this.position.setX(x);
		this.updateGrid();
	}
	
	public void setY(float y)
	{
		this.position.setY(y);
		this.updateGrid();
	}
	
	public float angleTo(Fluid p)
	{
		return (float)Math.atan2(p.position.getY() - this.position.getY(), p.position.getX() - this.position.getX());
	}
	
	public float distanceTo(Fluid p)
	{
		float deltaX = p.position.getX() - this.position.getX();
		float deltaY = p.position.getY() - this.position.getY();
		return (float)Math.sqrt((deltaX*deltaX)+(deltaY*deltaY));
	}
	
	public void TenseTo(Fluid F)
	{
		if(this.distanceTo(F) > radius) return;
		Vector A = new Vector(F.position.sub(this.position));
		A.setLength(A.getLength()-(this.viscosity*this.radius));
		//if (A.getLength()<0.0f) A.setLength(A.getLength() * A.getLength());
		//A.setAngle(this.angleTo(F));
		
		this.velocity.mulBy(0.995f);
		this.acceleration.addTo(A);
	}
	
	public void Update(float deltaTime)
	{
		this.velocity.addTo(this.acceleration.div(this.mass));
		this.position.addTo(this.velocity.mul(deltaTime));
		
		this.updateGrid();
		
		this.acceleration.zero();
	}
	
	public void setRadius(float R)
	{
		this.radius = R;
	}
	
}
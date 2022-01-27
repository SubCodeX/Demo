package com.scxn.toolkit.math;

import com.scxn.toolkit.math.Vector;

public class Particle
{
	public Vector position;
	public Vector velocity;
	
	private float mass;
	private float radius;
	private float friction;

/*
	public Particle(float xPosition, float yPosition, float xVelocity, float yVelocity)
	{
		this.position = new Vector (xPosition, yPosition);
		this.velocity = new Vector (xVelocity, yVelocity);		
	}
*/
	
	public Particle(float xPosition, float yPosition, float speed, float direction)
	{
		this.position = new Vector (xPosition, yPosition);
		this.velocity = new Vector ();
		this.velocity.setLength(speed);
		this.velocity.setDegAngle(direction);
		this.mass = 0.1f;
		this.radius = 0.0f;
		this.friction = 1.0f;
	}
	
	public Particle(float xPosition, float yPosition, float speed, float direction, float mass, float radius, float friction)
	{
		this.position = new Vector (xPosition, yPosition);
		this.velocity = new Vector ();
		this.velocity.setLength(speed);
		this.velocity.setDegAngle(direction);
		this.mass = mass;
		this.radius = radius;
		this.friction = friction;
	}
	
	public void setRadius(float r)
	{
		this.radius = r;
	}
	
	public float getRadius()
	{
		return this.radius;
	}
	
	public void Accelerate(Vector v)
	{
		this.velocity.addTo(v);
	}
	
	public void AccelerateTo(Particle p, float force)
	{
		Vector v = new Vector();
		v.setLength(force);
		v.setAngle(this.angleTo(p));
		this.velocity.addTo(v);
	}
	
	public float angleTo(Particle p)
	{
		return (float)Math.atan2(p.position.getY() - this.position.getY(), p.position.getX() - this.position.getX());
	}
	
	public float distanceTo(Particle p)
	{
		float deltaX = p.position.getX() - this.position.getX();
		float deltaY = p.position.getY() - this.position.getY();
		return (float)Math.sqrt((deltaX*deltaX)+(deltaY*deltaY));
	}
	
	public void gravitateTo(Particle p)
	{
		Vector gravity = new Vector();
		float distance = this.distanceTo(p);
		
		gravity.setLength(p.mass / (distance * distance));
		gravity.setAngle(this.angleTo(p));
		
		this.velocity.addTo(gravity);		
	}
	
	public void springTo(Particle p, float length, float k)
	{
		Vector distance = new Vector(p.position.sub(this.position));
		distance.setLength(distance.getLength() - length);
		Vector spring = new Vector(distance.mul(k));
		
		
		/*float distance = this.distanceTo(p);
		distance -= length;		
		
		Vector spring = new Vector(1, 0);
		spring.setLength(distance * k);
		spring.setAngle(this.angleTo(p));
		
		//if (show) System.out.println("spring acc = " + spring.getLength());
		*/
		this.velocity.addTo(spring);
	}
	
	public void Update()
	{
		this.velocity.mulBy(friction);
		this.position.addTo(this.velocity);
	}

}

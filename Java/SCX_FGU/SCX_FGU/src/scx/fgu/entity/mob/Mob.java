package scx.fgu.entity.mob;

import scx.fgu.entity.Entity;
import scx.fgu.graphics.Sprite;

public abstract class Mob extends Entity {

	protected Sprite sprite;
	protected int direction = 0;
	protected boolean moving = false;
	
	public void move(int xDelta, int yDelta) {
		if (xDelta > 0) direction = 1;
		if (yDelta > 0) direction = 2;
		if (xDelta < 0) direction = 3;
		if (yDelta < 0) direction = 0;
		
		
		if (!collision()) {
			x += xDelta;
			y += yDelta;
		}
	}
	
	public void update() {
	}
	
	private boolean collision() {
		return false;
	}
	
	public void render() {
	}
	
}

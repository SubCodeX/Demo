package scx.fgu.entity.mob;

import scx.fgu.graphics.Screen;
import scx.fgu.input.Keyboard;

public class Player extends Mob {
	
	private Keyboard input;
	private int anim = 0;
	private boolean walking = false;
	
	

	public Player(Keyboard input) {
		this.input = input;
	}
	
	public Player(int x, int y, Keyboard input) {
		this.x = x;
		this.y = y;
		this.input = input;
	}
	
	public void update() {
		int xDelta = 0, yDelta = 0;
		if (anim < 8192) anim += 1;
		else anim -= 8192;
		if (input.up) yDelta -= 1;
		if (input.down) yDelta += 1;
		if (input.left) xDelta -= 1;
		if (input.right) xDelta += 1;
		
		if (xDelta != 0 || yDelta != 0) {
			move(xDelta, yDelta);
			walking = true;
		} else {
			walking = false;
		}
	}
	
	
	public static int animspeed = 60;
	public static int animspeed_half = animspeed / 2;
	public void render(Screen screen) {
		if (direction == 0) {
			if (walking) {
				if (anim % animspeed > animspeed_half) {
					screen.renderPlayer(x, y, sprite.player_up_1);
				} else {
					screen.renderPlayer(x, y, sprite.player_up_2);
				}
			} else {
				screen.renderPlayer(x, y, sprite.player_up);
			}
		}
		if (direction == 1) {
			if (walking) {
				if (anim % animspeed > animspeed_half) {
					screen.renderPlayer(x, y, sprite.player_right_1);
				} else {
					screen.renderPlayer(x, y, sprite.player_right_2);
				}
			} else {
				screen.renderPlayer(x, y, sprite.player_right);
			}
		}
		if (direction == 2) {
			if (walking) {
				if (anim % animspeed > animspeed_half) {
					screen.renderPlayer(x, y, sprite.player_down_1);
				} else {
					screen.renderPlayer(x, y, sprite.player_down_2);
				}
			} else {
				screen.renderPlayer(x, y, sprite.player_down);
			}
		}
		if (direction == 3) {
			if (walking) {
				if (anim % animspeed > animspeed_half) {
					screen.renderPlayer(x, y, sprite.player_left_1);
				} else {
					screen.renderPlayer(x, y, sprite.player_left_2);
				}
			} else {
				screen.renderPlayer(x, y, sprite.player_left);
			}
		}
	}
	
}

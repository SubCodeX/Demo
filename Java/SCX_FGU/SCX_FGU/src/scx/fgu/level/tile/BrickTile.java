package scx.fgu.level.tile;

import scx.fgu.graphics.Screen;
import scx.fgu.graphics.Sprite;

public class BrickTile extends Tile {

	public BrickTile(Sprite sprite) {
		super(sprite);
	}
	
	public void render(int x, int y, Screen screen) {
		screen.renderTile(x << 4, y << 4, this);
	}
	
	public boolean solid() {
		return true;
	}

}
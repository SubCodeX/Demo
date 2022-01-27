package scx.fgu.level;

import scx.fgu.graphics.Screen;
import scx.fgu.level.tile.Tile;

public class Level {
	
	protected int width, height;
	protected int[] tilesInt;
	protected int[] tiles;
	
	public Level(int width, int height) {
		this.width = width;
		this.height = height;
		tilesInt = new int[width * height];
		generateLevel();
	}
	
	public Level(String path) {
		loadLevel(path);
		generateLevel();
	}
	
	protected void generateLevel() {
	}
	
	protected void loadLevel(String path) {
	}
	
	public void update() {
	}
	
	private void time() {
	}
	
	public void render(int xScroll, int yScroll, Screen screen) {
		screen.setOffset(xScroll, yScroll);
		int xUpperLeft = xScroll >> 4;
		int yUpperLeft = yScroll >> 4;
		int xLowerRight = (xScroll + screen.width + 16) >> 4;
		int yLowerRight = (yScroll + screen.height + 16) >> 4;
		
		for (int y = yUpperLeft; y < yLowerRight; y += 1) {
			for (int x = xUpperLeft; x < xLowerRight; x += 1) {
				getTile(x, y).render(x, y, screen);
			}
		}
	}
	
	//Brick : 0xFF000000
	//Grass : 0xFF00FF00
	//Flower : 0xFFFFFF00
	//Stone : 0xFF808080	
	public Tile getTile(int x, int y) {
		if (x < 0 || x >= width || y < 0 || y >= height) return Tile.nill;
		if (tiles[x + y * width] == 0xFF000000) return Tile.brick;
		if (tiles[x + y * width] == 0xFF00FF00) return Tile.grass;
		if (tiles[x + y * width] == 0xFFFFFF00) return Tile.flower;
		if (tiles[x + y * width] == 0xFF808080) return Tile.rock;
		
		return Tile.nill;
	}

}
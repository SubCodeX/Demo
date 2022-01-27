package scx.fgu.graphics;

import java.util.Random;

import scx.fgu.entity.mob.Player;
import scx.fgu.level.tile.Tile;

public class Screen {

	public int width;
	public int height;
	public int[] pixels;
	public final int TILE_MAP_SIZE = 64;
	public final int TILE_MAP_SIZE_MASK = TILE_MAP_SIZE - 1;
	
	public int xOffset, yOffset;
	
	
	public int[] tiles = new int[TILE_MAP_SIZE * TILE_MAP_SIZE];
	
	private Random random = new Random();
	
	public Screen(int width, int height) {
		this.width = width;
		this.height = height;
		pixels = new int[width*height];
		
		for (int i = 0; i < TILE_MAP_SIZE * TILE_MAP_SIZE; i+=1) {
			tiles[i] = random.nextInt(0xFFFFFF);
		}
	}
	
	public void clear() {
		for (int i = 0; i < pixels.length; i++) {
			pixels[i] = 0;
		}
	}
	
	public void renderTile(int xPosition, int yPosition, Tile tile) {
		xPosition -= xOffset;
		yPosition -= yOffset;
		for (int y = 0; y < tile.sprite.SIZE; y += 1) {
			int yAbsolute = y + yPosition;
			for (int x = 0; x < tile.sprite.SIZE; x += 1) {
				int xAbsolute = x + xPosition;
				if (xAbsolute < -tile.sprite.SIZE || xAbsolute >= width || yAbsolute < 0 || yAbsolute >= height) break;
				if (xAbsolute < 0) xAbsolute = 0;
				pixels[xAbsolute + yAbsolute  *width] = tile.sprite.pixels[x + y * tile.sprite.SIZE];
			}
		}
	}
	
	public void renderPlayer(int xPosition, int yPosition, Sprite sprite) {
		xPosition -= xOffset;
		yPosition -= yOffset;
		for (int y = 0; y < 16; y += 1) {
			int yAbsolute = y + yPosition;
			for (int x = 0; x < 16; x += 1) {
				int xAbsolute = x + xPosition;
				if (xAbsolute < -16 || xAbsolute >= width || yAbsolute < 0 || yAbsolute >= height) break;
				if (xAbsolute < 0) xAbsolute = 0;
				int pixelColour = sprite.pixels[x + y * sprite.SIZE];
				if (pixelColour != 0xFFFF00FF) pixels[xAbsolute + yAbsolute  *width] = pixelColour;
			}
		}		
	}
	
	
	public void setOffset(int xOffset, int yOffset) {
		this.xOffset = xOffset;
		this.yOffset = yOffset;
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
}

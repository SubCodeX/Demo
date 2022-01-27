package scx.fgu.graphics;

public class Sprite {
	
	public final int SIZE;
	private int x, y;
	public int[] pixels;
	private SpriteSheet sheet;
	
	public static Sprite grass = new Sprite(16, 0, 0, SpriteSheet.tiles);
	public static Sprite flower = new Sprite(16, 1, 0, SpriteSheet.tiles);
	public static Sprite rock = new Sprite(16, 2, 0, SpriteSheet.tiles);
	public static Sprite brick = new Sprite(16, 3, 0, SpriteSheet.tiles);
	public static Sprite nill =  new Sprite(16, 0xFF7777);
	
	public static Sprite player_up = new Sprite(16, 12, 13, SpriteSheet.tiles);
	public static Sprite player_right = new Sprite(16, 13, 13, SpriteSheet.tiles);
	public static Sprite player_down = new Sprite(16, 14, 13, SpriteSheet.tiles);
	public static Sprite player_left = new Sprite(16, 15, 13, SpriteSheet.tiles);
	
	public static Sprite player_up_1 = new Sprite(16, 12, 14, SpriteSheet.tiles);
	public static Sprite player_up_2 = new Sprite(16, 12, 15, SpriteSheet.tiles);
	
	public static Sprite player_right_1 = new Sprite(16, 13, 14, SpriteSheet.tiles);
	public static Sprite player_right_2 = new Sprite(16, 13, 15, SpriteSheet.tiles);
	
	public static Sprite player_down_1 = new Sprite(16, 14, 14, SpriteSheet.tiles);
	public static Sprite player_down_2 = new Sprite(16, 14, 15, SpriteSheet.tiles);
	
	public static Sprite player_left_1 = new Sprite(16, 15, 14, SpriteSheet.tiles);
	public static Sprite player_left_2 = new Sprite(16, 15, 15, SpriteSheet.tiles);
	
	public Sprite(int size, int x, int y, SpriteSheet sheet) {
		SIZE = size;
		pixels = new int[SIZE * SIZE];
		this.x = x * size;
		this.y = y * size;
		this.sheet = sheet;
		load();
	}
	
	public Sprite(int size, int colour) {
		SIZE = size;
		pixels = new int[SIZE*SIZE];
		setColour(colour);
	}
	
	private void setColour(int colour) {
		for (int i = 0; i < SIZE*SIZE; i += 1) {
			pixels[i] = colour;
		}
	}
	
	private void load() {
		for (int y = 0; y < SIZE; y += 1) {
			for (int x = 0; x < SIZE; x += 1) {
				pixels[x + y * SIZE] = sheet.pixels[(x + this.x) + (y + this.y) * sheet.SIZE];
			}
		}
	}

}

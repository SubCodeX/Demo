package scx.fgu;

import java.awt.Canvas;
import java.awt.Dimension;
import java.awt.Graphics;
import java.awt.image.BufferStrategy;
import java.awt.image.BufferedImage;
import java.awt.image.DataBufferInt;

import javax.swing.JFrame;

import scx.fgu.entity.mob.Player;
import scx.fgu.graphics.Screen;
import scx.fgu.input.Keyboard;
import scx.fgu.level.Level;
import scx.fgu.level.RandomLevel;
import scx.fgu.level.SpawnLevel;

public class Game extends Canvas implements Runnable {
	
	private static final long serialVersionUID = 1L;
	public static int scr_width = 320;
	public static int scr_height = 200;
	public static int scr_upscale = 3;
	public static String scr_title = "SCX - From Ground Up";
	
	private Thread thread;
	private JFrame frame;
	private Keyboard key;
	private Level level;
	private Player player;
	private boolean running = false;
	
	private Screen screen;
	
	private BufferedImage scr_image = new BufferedImage(scr_width, scr_height, BufferedImage.TYPE_INT_RGB);
	private int[] scr_pixels = ((DataBufferInt)scr_image.getRaster().getDataBuffer()).getData();
	
	public Game() {
		Dimension scr_size = new Dimension(scr_width * scr_upscale, scr_height * scr_upscale);
		setPreferredSize(scr_size);
		
		screen = new Screen(scr_width, scr_height);
		frame = new JFrame();		
		key = new Keyboard();
		level = new SpawnLevel("/textures/level.png");
		player = new Player(16, 16, key);
		
		addKeyListener(key);
	}
	
	public synchronized void start() {
		running = true;
		thread = new Thread(this, "Display");
		thread.start();
	}
	
	public synchronized void stop() {
		running = false;
		try {
			thread.join();
		} catch (InterruptedException e) {
			e.printStackTrace();
		}
	}
	
	public void run() {
		
		long previousTimestamp = System.nanoTime();
		final double limitTime = 1000000000.0 / 60.0;
		double changeTime = 0.0;
		
		long timerPerSecond = System.currentTimeMillis();
		int framesPerSecond = 0;
		int updatesPerSecond = 0;
		
		requestFocus();
		while (running) {
			long currentTimestamp = System.nanoTime();
			changeTime += (currentTimestamp - previousTimestamp) / limitTime;
			previousTimestamp = currentTimestamp;
			while (changeTime >= 1.0) {
				update();
				updatesPerSecond += 1;
				changeTime -= 1.0;
			}
			render();
			framesPerSecond += 1;
			
			if (System.currentTimeMillis() - timerPerSecond > 1000) {
				timerPerSecond += 1000;
				//System.out.println(updatesPerSecond + " UPS / " + framesPerSecond + " FPS");
				frame.setTitle(scr_title + " (" + updatesPerSecond + " UPS / " + framesPerSecond + " FPS)");
				updatesPerSecond = 0;
				framesPerSecond = 0;
			}
			
		}
		stop();
	}
	
		
	public void update() {
		key.update();	
		player.update();

	}
	
	public void render() {
		BufferStrategy scr_buffer_strategy = getBufferStrategy();
		if (scr_buffer_strategy == null) {
			createBufferStrategy(3);
			return;
		}
		
		screen.clear();
		int xScroll = (player.x - screen.width / 2) - 8;
		int yScroll = (player.y - screen.height / 2) - 8;
		level.render(xScroll, yScroll, screen);
		player.render(screen);
		
		for (int i = 0; i < scr_pixels.length; i++) {
			scr_pixels[i] = screen.pixels[i];
		}
		
		Graphics scr_graphics = scr_buffer_strategy.getDrawGraphics();
		scr_graphics.drawImage(scr_image, 0, 0, getWidth(), getHeight(), null);
		scr_graphics.dispose();
		scr_buffer_strategy.show();
	}
	
	public static void main(String[] args) {
		Game game = new Game();
		game.frame.setResizable(false);
		game.frame.setTitle("SCX - From Ground Up");
		game.frame.add(game);
		game.frame.pack();
		game.frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		game.frame.setLocationRelativeTo(null);
		game.frame.setVisible(true);
		
		game.start();
	}
	
}

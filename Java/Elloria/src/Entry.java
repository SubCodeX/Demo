

public class Entry
{
    public static final int    WIDTH  = 800;
    public static final int    HEIGHT = 600;
    public static final String TITLE  = "SCX 3D";
    public static final double FPS    = 50;

    private boolean            isRunning;
    private Game game;

    public Entry()
    {
        System.out.println(RenderUtil.getOpenGLVersion());
        RenderUtil.initGraphics();
        this.isRunning = false;
        game = new Game();
    }

    public void start()
    {
        if (isRunning)
        {
            return;
        }

        run();
    }

    public void stop()
    {
        if (!isRunning)
        {
            return;
        }

        isRunning = false;
    }

    private void run()
    {
        isRunning = true;

        Timer fpsTimer = new Timer(FPS);
        int fpsCounter = 0;
        Timer fpsReport = new Timer(1.0);
        

        while (isRunning)
        {

            if (fpsTimer.check())
            {
                fpsCounter++;
                Time.setDelta(fpsTimer.getDeltaInSeconds());
                update();
                render();
            }

            if (fpsReport.check())
            {
                System.out.println("FPS : " + fpsCounter);
                fpsCounter = 0;
            }

            if (Window.isCloseRequested())
            {
                stop();
            }
        }

        clean();
    }

    private void render()
    {
        RenderUtil.clearScreen();
        game.render();
        Window.render();
    }

    private void update()
    {
        Input.update();
        game.input();
        game.update();
    }

    private void clean()
    {
        Window.dispose();
    }

    public static void main(String[] args)
    {
        System.out.println("Helloes!! =P");
        Window.CreateWindow(WIDTH, HEIGHT, TITLE);

        Entry game = new Entry();
        game.start();

        System.out.println("Byes!! =P");
    }

}

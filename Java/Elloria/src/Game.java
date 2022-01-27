import org.lwjgl.input.Keyboard;

public class Game
{
    private Mesh mesh;
    private Shader shader;
    
    public Game()
    {
        mesh = new Mesh();
        shader = new Shader();
        Vertex[] data = new Vertex[] {new Vertex(new Vector3f(1, 0, 0)),
                                      new Vertex(new Vector3f(0, 0, 0)),
                                      new Vertex(new Vector3f(0.5f, 1, 0)),
                                      
                                      new Vertex(new Vector3f(0, 0, 0)),
                                      new Vertex(new Vector3f(-1, 0, 0)),
                                      new Vertex(new Vector3f(-0.5f, 1, 0)),
                                      
                                      new Vertex(new Vector3f(0.5f, -1, 0)),
                                      new Vertex(new Vector3f(-0.5f, -1, 0)),
                                      new Vertex(new Vector3f(0, 0, 0)),};
        
        mesh.addVertices(data);
        
        shader.addVertexShader(ResourceLoader.loadShader("basicVertex.pvs"));
        shader.addFragmentShader(ResourceLoader.loadShader("basicFragment.pfs"));
        shader.compileShader();
        

    }

    public void input()
    {
        if(Input.getKeyDown(Keyboard.KEY_UP)) System.out.println("UP arrow key pressed");
        if(Input.getKeyUp(Keyboard.KEY_UP)) System.out.println("UP arrow key released");

        if(Input.getButtonDown(1)) System.out.println("Right mouse button pressed @ " + Input.getMousePosition().toString());
        if(Input.getButtonUp(1)) System.out.println("Right mouse button released @ " + Input.getMousePosition().toString());
    }

    public void update()
    {

    }

    public void render()
    {
        shader.bind();
        mesh.draw();
    }

}

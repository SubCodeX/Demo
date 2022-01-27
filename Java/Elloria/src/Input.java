import java.util.ArrayList;

import org.lwjgl.input.Keyboard;
import org.lwjgl.input.Mouse;

public class Input
{
    public static final int           NO_KEYCODES    = 256;
    public static final int           NO_BUTTONCODES = 16;
    private static ArrayList<Integer> currentKeys    = new ArrayList<Integer>();
    private static ArrayList<Integer> downKeys       = new ArrayList<Integer>();
    private static ArrayList<Integer> upKeys         = new ArrayList<Integer>();
    private static ArrayList<Integer> currentButtons = new ArrayList<Integer>();
    private static ArrayList<Integer> downButtons    = new ArrayList<Integer>();
    private static ArrayList<Integer> upButtons      = new ArrayList<Integer>();

    public static void update()
    {
        // KEYBOARD SCAN
        downKeys.clear();
        for (int i = 0; i < NO_KEYCODES; i++)
        {
            if (getKey(i) && !currentKeys.contains(i))
            {
                downKeys.add(i);
            }
        }

        upKeys.clear();
        for (int i = 0; i < NO_KEYCODES; i++)
        {
            if (!getKey(i) && currentKeys.contains(i))
            {
                upKeys.add(i);
            }
        }

        currentKeys.clear();
        for (int i = 0; i < NO_KEYCODES; i++)
        {
            if (getKey(i))
            {
                currentKeys.add(i);
            }
        }

        // MOUSE SCAN
        downButtons.clear();
        for (int i = 0; i < NO_BUTTONCODES; i++)
        {
            if (getButton(i) && !currentButtons.contains(i))
            {
                downButtons.add(i);
            }
        }

        upButtons.clear();
        for (int i = 0; i < NO_BUTTONCODES; i++)
        {
            if (!getButton(i) && currentButtons.contains(i))
            {
                upButtons.add(i);
            }
        }

        currentButtons.clear();
        for (int i = 0; i < NO_BUTTONCODES; i++)
        {
            if (getButton(i))
            {
                currentButtons.add(i);
            }
        }
    }
    
    public static Vector2f getMousePosition()
    {
        return new Vector2f(Mouse.getX(), Mouse.getY());
    }

    public static boolean getKey(int keyCode)
    {
        return Keyboard.isKeyDown(keyCode);
    }

    public static boolean getKeyDown(int keyCode)
    {
        return downKeys.contains(keyCode);
    }

    public static boolean getKeyUp(int keyCode)
    {
        return upKeys.contains(keyCode);
    }

    public static boolean getButton(int buttonCode)
    {
        return Mouse.isButtonDown(buttonCode);
    }

    public static boolean getButtonDown(int buttonCode)
    {
        return downButtons.contains(buttonCode);
    }

    public static boolean getButtonUp(int buttonCode)
    {
        return upButtons.contains(buttonCode);
    }
}

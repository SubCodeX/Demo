public class Matrix4f
{
    private float[][] e;

    public Matrix4f()
    {
        e = new float[4][4];
    }

    public Matrix4f setIdentity()
    {
        e[0][0]=1; e[1][0]=0; e[2][0]=0; e[3][0]=0;
        e[0][1]=0; e[1][1]=1; e[2][1]=0; e[3][1]=0;
        e[0][2]=0; e[1][2]=0; e[2][2]=1; e[3][2]=0;
        e[0][3]=0; e[1][3]=0; e[2][3]=0; e[3][3]=1;
        
        return this;
    }
    
    public Matrix4f mul(Matrix4f m)
    {
        Matrix4f r = new Matrix4f();
        
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                r.set(i, j, e[i][0] * m.get(0, j) +
                            e[i][1] * m.get(1, j) +
                            e[i][2] * m.get(2, j) +
                            e[i][3] * m.get(3, j));
            }
        }
        
        return r;
    }

    public float[][] getM()
    {
        return e;
    }

    public float get(int x, int y)
    {
        return e[x][y];
    }

    public void setM(float[][] m)
    {
        this.e = m;
    }

    public void set(int x, int y, float value)
    {
        e[x][y] = value;
    }
}

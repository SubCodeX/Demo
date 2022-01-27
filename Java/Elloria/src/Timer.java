
public class Timer
{
    private long limit;
    private long next;
    private long current;
    private long last;
    private long delta;
    
    public Timer(double intervalsPerSecond)
    {
        limit = (long)(1000000000.0 / intervalsPerSecond);
        last = current = System.nanoTime();
        next = current + limit;
    }
    
    public boolean check()
    {
        current = System.nanoTime();
        delta = current - last;
        if (current > next)
        {
            last = current;
            next += limit;
            return true;
        }
        
        return false;
    }
    
    public long getDelta()
    {
        return delta;
    }
    
    public double getDeltaInSeconds()
    {
        return (double)delta / 1000000000.0;
    }
    
}

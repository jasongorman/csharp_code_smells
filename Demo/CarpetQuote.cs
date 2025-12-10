namespace Demo;

public class CarpetQuote
{
    public double Calculate(double length, double width, double pricePerSqMetre, bool roundUpArea)
    {
        double area = length * width;

        if (roundUpArea)
        {
            area = (float)Math.Ceiling(area);
        }
        
        return area * pricePerSqMetre;
    }
}
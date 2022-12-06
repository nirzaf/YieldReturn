public struct Circle
{
    public double Radius { get; set; }
    private double Area => Math.PI * Radius * Radius;
    
    private double Circumference => 2 * Math.PI * Radius;
    
    //Get Area of Circle
    public double GetArea()
    {
        return Area;
    }
    
    //Get Circumference of Circle
    public double GetCircumference()
    {
        return Circumference;
    }
}
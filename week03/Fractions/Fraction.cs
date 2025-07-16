using System;

class Fraction
{
    private int _top, _bottom;
    public Fraction()
    {
        this.SetTop(1);
        this.SetBottom(1);
    }

    public Fraction(int wholeNumber)
    {
        this.SetTop(wholeNumber);
        this.SetBottom(1);
    }

    public Fraction(int top, int bottom)
    {
        this.SetTop(top);
        this.SetBottom(bottom);
    }

    public int GetTop()
    {
        return _top;
    }

    public void SetTop(int top)
    {
        _top = top;
    }

    public int GetBottom()
    {
        return _bottom;
    }

    public void SetBottom(int bottom)
    {
        _bottom = bottom;
    }

    public string GetFractionString()
    {
        return $"{this.GetTop()}/{this.GetBottom()}"; 
    }

    public double GetDecimalValue()
    {
        return (double)this.GetTop() / (double)this.GetBottom(); 
    }
    
}
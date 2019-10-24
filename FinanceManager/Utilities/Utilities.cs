using System;

public static class Utilities
{
    public static int ParseInt(string strNumber)
    {
        if(!int.TryParse(strNumber, out int number))
        {
            throw new Exception("Number could not be parsed");
        }
        
        return number;
    }
}
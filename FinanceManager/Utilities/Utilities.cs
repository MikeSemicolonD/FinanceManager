using FinanceManager.DAL;
using System;
using System.Web.Mvc;
using System.Web.Security;

/// <summary>
/// Any parsing functions that need to be simplified are stored here
/// </summary>
public static class Utilities
{

    public static string GetUsersUID(string email)
    {
        UserAdapter userAdapter = new UserAdapter();
        return userAdapter.GetUIDByEmail(email);
    }

    public static int ParseInt(string strNumber)
    {
        if(!int.TryParse(strNumber, out int number))
        {
            throw new Exception("Integer could not be parsed");
        }
        
        return number;
    }

    public static decimal ParseDecimal(string strDecimal)
    {
        if(!decimal.TryParse(strDecimal, out decimal number))
        {
            throw new Exception("Double could not be parsed");
        }

        return number;
    }

    public static DateTime ParseDateTime(string strDateTime)
    {
        if (!DateTime.TryParse(strDateTime, out DateTime number))
        {
            throw new Exception("DateTime could not be parsed");
        }

        return number;
    }

    public static bool ParseBool(string strBool)
    {
        if (!bool.TryParse(strBool, out bool boolean))
        {
            throw new Exception("Boolean could not be parsed");
        }

        return boolean;
    }
}
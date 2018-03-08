using System;
using System.Collections.Generic;

internal class Bases
{
    protected string input;
    protected List<string> reservedWords;
    public Token tokErr = new Token();

    protected bool huboerror = false;

    public virtual float Error(Exception ex, dynamic tok, string msg)
    {

        tokErr = tok;
        huboerror = true;
        Console.WriteLine("ERR => " + tokErr.Type + ": " + msg);
        return -1;
    }

    public class EOLException: Exception
    {
        public EOLException(String message): base(message)
        {
        }

        public EOLException(String message, Exception innerException): base(message, innerException)
        {
        }
    }

    public class ChoosingTokenException: Exception
    {
        public ChoosingTokenException(String message) : base(message)
        {
        }

        public ChoosingTokenException(String message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public class FactorException : Exception
    {
        public FactorException(string message) : base(message)
        {
        }

        public FactorException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
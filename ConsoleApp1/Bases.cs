using System;
using System.Collections.Generic;

internal class Bases
{
    protected string input;
    protected List<string> reservedWords;
    protected Token tokErr = new Token();

    protected bool huboerror = false;

    public virtual float Error(dynamic tok, string msg)
    {
        tokErr = tok;
        huboerror = true;
        Console.WriteLine("ERR => " + tokErr.Type + ": " + msg);
        return -1;
    }
}
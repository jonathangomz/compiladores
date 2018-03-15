using System;
using System.Collections.Generic;

internal class CompilerBase
{
    protected string input;
    protected Dictionary<string, TokenType> reservedWords;

    public Token tokErr = new Token(); //No se ocupa en el contexto actual del proyecto 
    protected bool huboerror = false;  //No se ocupa en el contexto actual del proyecto

    public class LexerException: Exception
    {
        public LexerException(String message): base("ERR>> "+message)
        {
        }
        public LexerException(String message, Exception innerException): base("ERR>> " + message, innerException)
        {
        }
    }

    public class ParserException: Exception
    {
        public ParserException(String message) : base("ERR>> " + message)
        {
        }
        public ParserException(String message, Exception innerException) : base("ERR>> " + message, innerException)
        {
        }
    }
}
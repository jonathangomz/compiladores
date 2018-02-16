using System;
using System.Collections.Generic;

public class Test
{

    public static List<String> reservedWords = new List<String>();
    public static String input;

    public Test()
	{
	}

    public static void Main()
    {
        reservedWords.Add("if");
        input = "a+b";
        Lexer l = new Lexer();
        Token t = l.NextToken(input, reservedWords);
        while (t.Type != TokenType.TOKEN_NONE)
        {
            t = l.NextToken(input, reservedWords);
            Console.WriteLine("regresó: "+t);
            Console.WriteLine("" + t.Text + t.Type);
            Console.ReadKey();
        }
    }
}

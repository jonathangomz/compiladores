using System;
using System.Collections;
using System.Collections.Generic;

public class Test
{

    public List<string> reservedWords = new List<string>();
    public string input;

    public Test()
	{
	}

    public void Main()
    {
        reservedWords.Add("if");
        input = "a+b";
        Lexer l = new Lexer();
        Token t = l.NextToken(input, reservedWords);
        while (t.Type != TokenType.TOKEN_NONE)
        {
            Console.WriteLine("" + t.Text + t.Type);
            Console.ReadKey();
        }
    }
}

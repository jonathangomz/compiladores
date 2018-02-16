using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Lexer
{
    public int index = 0;
    public int edo = 0;
    public Regex letter = new Regex(@"[a-zA-Z]");
    public Regex digit = new Regex(@"[0-9]");

    public Token NextToken(string input, List<string> reservedWords)
    {

        for (int i = index; i < input.Length; i++)
        {
            char c = input[i];
            Console.WriteLine("C: "+c);
            Console.WriteLine("edo: " + edo);
            Console.WriteLine("i: " + i);
            Console.WriteLine("index: " + index);
            Console.WriteLine("input: " + input);
            while ((c == ' ' || c == '\t') && edo == 0)
            {
                i++;
            }
            if (i >= input.Length)
            {
                return new Token();
            }
            else if ((edo == 0 || edo == 5 || edo == 3) && c == '+')
            {
                edo = 0;
                index = i + 1;
                return new Token(TokenType.SUM, input.Substring(i, i));
            }
            else if ((edo == 0 || edo == 5 || edo == 3) && c == '-')
            {
                edo = 0;
                index = i + 1;
                return new Token(TokenType.RES, input.Substring(i, i));
            }
            else if ((edo == 0 || edo == 3) && letter.IsMatch(c.ToString()))
            {
                Console.WriteLine("entró: " + c);
                edo = 3;
                Console.WriteLine("entró edo: " + edo);
                index = i + 1;
                Console.WriteLine("entró index: " + index);
                return new Token(TokenType.ID, input.Substring(i, i+1));
            }
            else if ((edo == 0 || edo == 5) && digit.IsMatch(c.ToString()))
            {
                edo = 5;
                index = i + 1;
                return new Token(TokenType.NUM, input.Substring(i, i + 1));
            }
        }
        return new Token();
    }

    public void getTokens()
    {
    }

}
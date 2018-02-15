using System;
using System.Collections;
using System.Collections.Generic;

public class Lexer
{
    public int index = 0;

    public Token NextToken(String input, List<String> reservedWords)
    {
        int edo = 0;

        for (int i = index; i < input.Length; i++)
        {
            int c = input[i];
            while ((c == ' ' || c == '\t') && edo == 0)
            {
                i++;
            }
            if (i >= input.Length)
            {
                return new Token();
            }
            else if (edo == 0 && c == '+')
            {
                edo = 3; index = i + 1;
                return new Token(TokenType.SUM, input.Substring(i, i + 1));
            }
        }
        return new Token();

    }

    public void getTokens()
    {
    }

}
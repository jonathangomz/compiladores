using System;

public class Lexer
{
    int index = 0;


    public Token NextToken(string input, List<string> reserverdWords)
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
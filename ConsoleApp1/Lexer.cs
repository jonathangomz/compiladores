using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

internal class Lexer
{
    public int index = 0;
    public int edo = 0;

    public string text = "";
    public TokenType tokenType = TokenType.TOKEN_NONE;

    public Regex simbol = new Regex(@"[+, *,\/,\-, (, ), [,\],{,}]");

    public Lexer()
    {
    }

    public Token NextToken(string input, List<string> reservedWords)
    {
        for (int i = index; i < input.Length; i++)
        {
            char c = input[i];
            Console.Write("c: " + c + " edo: " + edo + " index: " + index + " => ");
            while ((c == ' ' || c == '\t') && edo == 0)
            {
                i++;
            }
            if (i >= input.Length)
            {
                return new Token();
            }
            // **SUM**
            if (edo == 0 && c == '+')
            {
                edo = 0;
                return new Token(TokenType.SUM, input.Substring(index, ++index - i));
            }
            // **RES**
            if (edo == 0 && c == '-')
            {
                edo = 0;
                return new Token(TokenType.RES, input.Substring(index, ++index - i));
            }
            // **DIV**
            if (edo == 0 && c == '/')
            {
                edo = 0;
                return new Token(TokenType.DIV, input.Substring(index, ++index - i));
            }
            // **MUL**
            if (edo == 0 && c == '*')
            {
                edo = 0;
                return new Token(TokenType.MUL, input.Substring(index, ++index - i));
            }
            // **ID**
            // Si es un ID y está al final de la línea
            if (((edo == 0 && Char.IsLetter(c)) || (edo == 3 && Char.IsLetterOrDigit(c))) && i + 1 >= input.Length)
            {
                i++;
                edo = 0;
                text = input.Substring(index, i - index).Trim();
                tokenType = TokenType.ID;
                index = i;
                return new Token(tokenType, text);
            }
            // Si comienza un ID o continua sigue al siguiente caracter
            if ((edo == 0 && Char.IsLetter(c)) || (edo == 3 && Char.IsLetterOrDigit(c)))
            {
                edo = 3;
                continue;
            }
            //Si el siguiente caracter es otro simbolo regresa el texto del ID
            if (edo == 3 && simbol.IsMatch(c.ToString()))
            {
                edo = 0;
                text = input.Substring(index, i - index).Trim();
                tokenType = TokenType.ID;
                index = i;
                return new Token(tokenType, text);
            }
            // **NUM**
            // Si es un NUM y está al final de la línea
            if (((edo == 0 && Char.IsNumber(c)) || (edo == 5 && Char.IsNumber(c))) && i + 1 >= input.Length)
            {
                i++;
                edo = 0;
                text = input.Substring(index, i - index).Trim();
                tokenType = TokenType.NUM;
                index = i;
                return new Token(tokenType, text);
            }
            // Si comienza un NUM o continua sigue al siguiente caracter
            if ((edo == 0 || edo == 5) && Char.IsNumber(c))
            {
                edo = 5;
                continue;
            }
            //Si el siguiente caracter es otro simbolo regresa el texto del NUM
            if (edo == 5 && simbol.IsMatch(c.ToString()))
            {
                edo = 0;
                text = input.Substring(index, i - index).Trim();
                tokenType = TokenType.NUM;
                index = i;
                return new Token(tokenType, text);
            }
        }
        return new Token();
    }

    public void getTokens()
    {
    }
}
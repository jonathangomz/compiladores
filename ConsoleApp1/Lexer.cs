using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

internal class Lexer: Bases
{
/* Variables del método NextToken() */
    internal int index = 0;
    internal int edo = 0;
    // Parámetros variables para crear el Token()
    internal string text = "";
    internal TokenType tokenType = TokenType.TOKEN_NONE;

/* Variables del método ListOfToken() */
    public List<Token> list = new List<Token>();

/* Regular Expressions */
    internal Regex simbol = new Regex(@"[+, *,\/,\-, (, ), [,\],{,}]");

    /* Constructor */
    public Lexer(string input, List<string> reservedWords)
    {
        this.input = input;
        this.reservedWords = reservedWords;
    }
    public Lexer()
    {
    }

/* NextToken(string, List<string>) returns Token */
    public Token NextToken(string input, List<string> reservedWords)
    {
        for (int i = index; i < input.Length; i++)
        {
            char c = input[i];
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
            if (edo == 0 && c == '(')
            {
                edo = 0;
                return new Token(TokenType.PARA, input.Substring(index, ++index - i));
            }
            if (edo == 0 && c == ')')
            {
                edo = 0;
                return new Token(TokenType.PARC, input.Substring(index, ++index - i));
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
            else
            {
                throw new ChoosingTokenException(string.Format("No se reconoce el Token {0}", c));
            }
        }
        return new Token(TokenType.EOL, "");
    }

/* ListOfToken(string, List<string>) returns List<Token> */
    public List<Token> ListOfToken()
    {
        Token t = NextToken(input, reservedWords);
        while (t.Type != TokenType.EOL)
        {
            list.Add(t);
            t = NextToken(input, reservedWords);
        }
        return list;
    }
}
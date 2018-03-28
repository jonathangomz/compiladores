using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

internal class Lexer: CompilerBase
{
/* Variables del método NextToken() */
    internal int index = 0;
    internal float edo = 0;
    // Parámetros variables para crear el Token()
    internal string text = "";
    internal TokenType tokenType = TokenType.TOKEN_NONE;

/* Variables del método ListOfToken() */
    public List<Token> list = new List<Token>();

/* Regular Expressions */
    Regex simbol = new Regex(@"[+, *,\/,\-, (, ), [,\],{,}, %, ^, !, =, \|, >, <, ;]");

    /* Constructor */
    public Lexer()
    {
    }

/* NextToken(string, List<string>) returns Token */
    public Token NextToken()
    {
        for (int i = index; i < input.Length; i++)
        {
            char c = input[i];
            while ((c == ' ' || c == '\t') && edo == 0)
            {
                i++;
                c = input[i];
            }
            if (i >= input.Length)
            {
                return new Token();
            }
            // **SUMA**
            if (edo == 0 && c == '+')
                return new Token(TokenType.SUM, input.Substring(index, ++index - i));
            // **RESTA**
            if (edo == 0 && c == '-')
                return new Token(TokenType.RES, input.Substring(index, ++index - i));
            // **DIVISIÓN**
            if (edo == 0 && c == '/')
                return new Token(TokenType.DIV, input.Substring(index, ++index - i));
            // **MULTIPLICACIÓN**
            if (edo == 0 && c == '*')
                return new Token(TokenType.MUL, input.Substring(index, ++index - i));
            // **MÓDULO**
            if(edo == 0 &&  c == '%')
                return new Token(TokenType.MOD, input.Substring(index, ++index - i));
            // **POTENCIA**
            if(edo == 0 && c == '^')
                return new Token(TokenType.POT, input.Substring(index, ++index - i));
            // **PUNTO Y COMA
            if (edo == 0 && c == ';')
                return new Token(TokenType.SEMICOLON, input.Substring(index, ++index - i));
            // **IGUAL O ASIGNACIÓN**
            if(edo == 0 && c == '=')
            {
                edo = 1;
                continue;
            }
            // *ASIGNACIÓN
            if(edo == 1 && c != '=')
            {
                edo = 0;
                text = input.Substring(index, i - index).Trim();
                index = i;
                tokenType = TokenType.ASIGNA;
                return new Token(tokenType, text);
            }
            // *IGUAL
            if (edo == 1 && c == '=')
            {
                edo = 0;
                return new Token(TokenType.EQUAL, input.Substring(index, ++index - i));
            }
            // **MENOR, MAYOR O DIFERENTE**
            if (edo == 0 && (c == '<' || c == '>' || c == '!'))
            {
                if(c == '<')
                    edo = 2.1F;
                if (c == '>')
                    edo = 2.2F;
                if (c == '!')
                    edo = 2.3F;
                continue;
            }
            // *MENOR
            if (edo == 2.1F && c != '=')
            {
                edo = 0;
                return new Token(TokenType.LESS, input.Substring(index-1, ++index - i));
            }
            // *MENOR O IGUAL
            if (edo == 2.1F && c == '=')
            {
                i++;
                edo = 0;
                text = input.Substring(index, i - index).Trim();
                index = i;
                tokenType = TokenType.LESS_EQUAL;
                return new Token(tokenType, text);
            }
            // *MAYOR
            if (edo == 2.2F && c != '=')
            {
                edo = 0;
                return new Token(TokenType.GREATER, input.Substring(index, ++index - i));
            }
            // MAYOR O IGUAL
            if (edo == 2.2F && c == '=')
            {
                i++;
                edo = 0;
                text = input.Substring(index, i - index).Trim();
                index = i;
                tokenType = TokenType.GREATER_EQUAL;
                return new Token(tokenType, text);
            }
            // *NEGACIÓN
            if (edo == 2.3F && c != '=')
            {
                edo = 0;
                return new Token(TokenType.NOT, input.Substring(index, ++index - i));
            }
            // *DIFERENTE
            if (edo == 2.3F && c == '=')
            {
                i++;
                edo = 0;
                text = input.Substring(index, i - index).Trim();
                index = i;
                tokenType = TokenType.NOT_EQUAL;
                return new Token(tokenType, text);
            }
            // **PARÉNTESIS DE APERTURA**
            if (edo == 0 && c == '(')
                return new Token(TokenType.PARA, input.Substring(index, ++index - i));
            // **PARÉNTESIS DE CIERRE**
            if (edo == 0 && c == ')')
                return new Token(TokenType.PARC, input.Substring(index, ++index - i));
            // **LLAVE DE APERTURA**
            if(edo == 0 && c == '{')
                return new Token(TokenType.LLAVEA, input.Substring(index, ++index - i));
            // **LLAVE DE CIERRE
            if (edo == 0 && c == '}')
                return new Token(TokenType.LLAVEC, input.Substring(index, ++index - i));
            // **CORCHETE DE APERTURA**
            if (edo == 0 && c == '[')
                return new Token(TokenType.CORCHETEA, input.Substring(index, ++index - i));
            // **CORCHETE DE CIERRE
            if (edo == 0 && c == ']')
                return new Token(TokenType.CORCHETEC, input.Substring(index, ++index - i));
            // **COMA**
            if (edo == 0 && c == ',')
                return new Token(TokenType.COMA, input.Substring(index, ++index - i));
            // **STRING**
            if((edo == 0 && c == '"') || (edo == 10 && c != '"'))
            {
                edo = 10;
                continue;
            }
            if(edo == 10 && c == '"')
            {
                i++;
                edo = 0;
                text = input.Substring(index, i - index).Trim();
                index = i;
                return new Token(TokenType.CHAR_CONST, text);
            }
            // **ID**
            // Si es un ID y está al final de la línea
            if (((edo == 0 && Char.IsLetter(c)) || (edo == 3 && Char.IsLetterOrDigit(c))) && i + 1 >= input.Length)
            {
                i++;
                edo = 0;
                text = input.Substring(index, i - index).Trim();
                index = i;
                if (reservedWords.ContainsKey(text))
                    tokenType = reservedWords[text];
                else
                    tokenType = TokenType.ID;
                return new Token(tokenType, text);
            }
            // Si comienza un ID o continua sigue al siguiente caracter
            if ((edo == 0 && Char.IsLetter(c)) || (edo == 3 && (Char.IsLetterOrDigit(c) || c == '_')))
            {
                edo = 3;
                continue;
            }
            //Si el siguiente caracter es otro simbolo regresa el texto del ID
            if (edo == 3 && simbol.IsMatch(c.ToString()))
            {
                edo = 0;
                text = input.Substring(index, i - index).Trim();
                index = i;
                if (reservedWords.ContainsKey(text))
                    tokenType = reservedWords[text];
                else
                    tokenType = TokenType.ID;
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
            if ((edo == 0 || edo == 5) && (Char.IsNumber(c) || c == '.'))
            {
                edo = 5;
                continue;
            }
            //Si el siguiente caracter es otro simbolo regresa el texto del NUM
            if (edo == 5 && simbol.IsMatch(c.ToString()))
            {
                edo = 0;
                text = input.Substring(index, i - index).Trim();
                if (text.Contains("."))
                    tokenType = TokenType.FLOAT_CONST;
                else
                    tokenType = TokenType.INT_CONST;
                index = i;
                return new Token(tokenType, text);
            }
            else
            {
                throw new ParserException(string.Format("No se reconoce el Token {0}", c));
            }
        }
        return new Token(TokenType.EOL, "");
    }

/* ListOfToken(string, List<string>) returns List<Token> */
    public List<Token> ListOfToken()
    {
        Token t = NextToken();
        while (t.Type != TokenType.EOL)
        {
            list.Add(t);
            t = NextToken();
        }
        return list;
    }

    public bool CheckInput(String input) => simbol.IsMatch(input);
}

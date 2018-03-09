using System;
using System.Collections.Generic;

internal class Parser: Lexer
{
    bool advance    = true;         // Para llevar un control del avance de los tokens a analizar
    Token tok       = new Token();
    Stack<Token> PAR = new Stack<Token>();

    /* Constructores */
    public Parser()
    {
    }

    public float Tester()
    {
        tok = NextToken(); // Por mientras **
        return Out();
    }

 /* Métodos de la Clase */
    public float Expression() //=> Arreglar lo de paréntesis de clausura
    {
        do
        {
            Termino();
            if (advance)
            {
                tok = NextToken(); advance = false;
            }
            if (tok.Type == TokenType.SUM || tok.Type == TokenType.RES)
            {
                tok = NextToken(); advance = true;
            }
        } while(advance);
        // El primer método debe de llevar esto al final**
        if ((huboerror || tok.Type != TokenType.EOL) && PAR.Count == 0)
            throw new ParserException(string.Format("Error al final de la línea => '{0}' <<({1})", tok.Text, tok.Type));
        else return 1;
    }

    public float Termino()
    {
        if (huboerror) return -1;
        do
        {
            Factor();
            if (advance)
            {
                tok = NextToken(); advance = false;
            }
            if (tok.Type == TokenType.MUL || tok.Type == TokenType.DIV)
            {
                tok = NextToken(); advance = true;
            }
        } while (advance);
        return 1;
    }

    public float Factor()
    {
        if (huboerror) return -1;

        if (tok.Type == TokenType.ID) // Si es ID
        {
            advance = true;
            return 1;
        }
        if (tok.Type == TokenType.NUM) // Si es NUM
        {
            advance = true;
            return 1;
        }
        if (tok.Type == TokenType.PARA) // Si es paréntesis de apertura
        {
            PAR.Push(tok);
            tok = NextToken(); // Por mientras **
            Expression();
            if (tok.Type == TokenType.PARC) // Deberá tener paréntesis de cierre
            {
                PAR.Pop();
                advance = true;
                return 1;
            }
            else
            {
                throw new ParserException(string.Format("Se esperaba PARC, se obtuvo => '{0}' <<({1})", tok.Text, tok.Type));
            }
        }
        else
        {
            throw new ParserException(string.Format("Se esperaba ID || NUM se obtuvo => '{0}' <<({1})", tok.Text, tok.Type));
        }
    }

    public float Out()
    {
        if (huboerror) return -1;
        tok = NextToken();
        if (tok.Type != TokenType.PARA)
            throw new ParserException(string.Format("Se esperaba PARA, se obtuvo => '{0}' <<({1})", tok.Text, tok.Type));
        do
        {
            Expression();
            if (advance)
            {
                tok = NextToken(); advance = false;
            }
            if (tok.Type == TokenType.COMA)
            {
                tok = NextToken(); advance = true;
            }
        } while (advance);
        if (NextToken().Type != TokenType.PARA)
            throw new ParserException(string.Format("Se esperaba PARC, se obtuvo => '{0}' <<({1})", tok.Text, tok.Type));
        return 1;
    }
}
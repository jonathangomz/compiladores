using System;
using System.Collections.Generic;

internal class Parser: Lexer
{
    bool advance    = true;         // Para llevar un control del avance de los tokens a analizar

    Token tok       = new Token();

    Stack<Token> PAR = new Stack<Token>();

    /* Constructores */
    public Parser(string input, List<string> reservedWords)
    {
        
    }

    public Parser()
    {
    }

 /* Métodos de la Clase */
    public virtual float Expression(string input, List<string> reservedWords) //=> Arreglar lo de paréntesis de clausura
    {
        tok = NextToken(input, reservedWords);
        do
        {
            Termino(input, reservedWords);
            if (advance)
            {
                tok = NextToken(input, reservedWords); advance = false;
            }
            if (tok.Type == TokenType.SUM || tok.Type == TokenType.RES)
            {
                tok = NextToken(input, reservedWords); advance = true;
            }
        } while(advance);
        // El primer método debe de llevar esto al final**
        if (huboerror) return -1;
        else return 1;
    }

    public float Termino(string input, List<string> reservedWords)
    {
        if (huboerror) return -1;
        do
        {
            Factor(input, reservedWords);
            if (advance)
            {
                tok = NextToken(input, reservedWords); advance = false;
            }
            if (tok.Type == TokenType.MUL || tok.Type == TokenType.DIV)
            {
                tok = NextToken(input, reservedWords); advance = true;
            }
        } while (advance);
        return 1;
    }

    private float Factor(string input, List<string> reservedWords)
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
            Expression(input, reservedWords);
            if (tok.Type == TokenType.PARC) // Deberá tener paréntesis de cierre
            {
                PAR.Pop();
                return 1;
            }
            else
            {
                return Error(tok, "No se encontró paréntesis de cierre");
            }
        }
        else
        {
            throw new FactorException(string.Format("Se esperaba un ID o NUM se obtuvo => {0}", tok.Type));
        }
    }

    public float Error(dynamic tok, string msg)
    {
        tokErr = tok;
        Console.WriteLine("ERR => " + tokErr.Type+": "+msg);
        huboerror = true;
        return -1;
    }
}
using System;
using System.Collections.Generic;

internal class Parser
{
    string input; 
    List<string> reservedWords;

    bool huboerror  = false;        // Si ocurre un error no sigue avanzando
    bool advance    = false;        //Para llevar un control del avance de los tokens a analizar

    Lexer l         = new Lexer();
    Token tok       = new Token();

    /* Constructores */
    public Parser(string input, List<string> reservedWords)
    {
        this.input          = input;
        this.reservedWords  = reservedWords;
        tok                 = l.NextToken(input, reservedWords);
    }
    public Parser(){ }

 /* Métodos de la Clase */
    public float Expression()
    {
        do
        {
            Termino();
            if (advance)
            {
                tok = l.NextToken(input, reservedWords); advance = false;
            }
            if (tok.Type == TokenType.SUM || tok.Type == TokenType.RES)
            {
                tok = l.NextToken(input, reservedWords); advance = true;
            }
        } while(advance);
        // El primer método debe de llevar esto al final**
        if (huboerror) return 0;
        else return 1;
    }

    public float Termino()
    {
        if (huboerror) return 0;
        do
        {
            Factor();
            if (advance)
            {
                tok = l.NextToken(input, reservedWords); advance = false;
            }
            if (tok.Type == TokenType.MUL || tok.Type == TokenType.DIV)
            {
                tok = l.NextToken(input, reservedWords); advance = true;
            }
        } while (advance);
        return 1;
    }

    private float Factor()
    {
        if (huboerror) return 0;

        if (tok.Type != TokenType.ID)
        {
            Console.WriteLine("ERR");
            huboerror = true;
            return 0;
        }
        else
        {
            advance = true;
            return 1;
        }
    }
}
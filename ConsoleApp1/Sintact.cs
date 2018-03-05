using System;
using System.Collections.Generic;

internal class Parser
{
    string input; 
    List<string> reservedWords;
    bool huboerror = false; // Si ocurre un error no sigue avanzando
    bool advance = false;
    Lexer l = new Lexer();
    Token tok;

    /* Constructores */
    public Parser(string input, List<string> reservedWords)
    {
        this.input = input;
        this.reservedWords = reservedWords;
        this.tok = l.NextToken(input, reservedWords);
    }
    public Parser(){ }

 /* Métodos de la Clase */
    public float Expression()
    {
        if (huboerror) return 0;
        do
        {
            Termino();
            if (tok.Type == TokenType.SUM)
                advance = true;
            tok = l.NextToken(input, reservedWords);
        } while(tok.Type == TokenType.SUM);
        return 1;
    }

    public float Termino()
    {
        if (huboerror) return 0;
        do
        {
            Factor();
            if (tok.Type == TokenType.MUL)
                advance = true;
            tok = l.NextToken(input, reservedWords);
        } while (tok.Type == TokenType.MUL);
        return 1;
    }

    private float Factor()
    {
        if (huboerror)
            return 0;

        if (tok.Type != TokenType.ID)
        {
            Console.WriteLine("ERR");
            huboerror = true;
            System.Environment.Exit(-1);
            return 0;
        }
        else
            return 1;
    }
}
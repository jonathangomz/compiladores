using System;
using System.Collections.Generic;

internal class Sintact
{
    string input; 
    List<string> reservedWords;

    public Sintact(string input, List<string> reservedWords)
    {
        this.input = input;
        this.reservedWords = reservedWords;
    }
    public Sintact(){ }

    public void Expression()
    {
        Termino();
        Token t = Lexer.NextToken(input, reservedWords);
    }

    public void Termino()
    {
        Factor();
    }

    private void Factor()
    {

    }
}
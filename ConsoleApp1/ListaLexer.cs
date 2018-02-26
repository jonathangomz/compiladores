using System;
using System.Collections.Generic;

internal class ListaLexer
{
    List<Token> list = new List<Token>();
    public ListaLexer()
    {
    }

    public List<Token> getListaToken(string input, List<string> reservedWords)
    {
        Lexer l = new Lexer();
        Token t = l.NextToken(input, reservedWords);
        while (t.Type != TokenType.TOKEN_NONE)
        {
            Console.WriteLine("" + t.Text + t.Type);
            list.Add(t);
            t = l.NextToken(input, reservedWords);
        }
        return list;
    }
}
using System.Collections.Generic;

internal class Compiler
{
    string input;
    List<string> reservedWords;

    Lexer lex   = new Lexer();
    Parser par  = new Parser();

    public Compiler(string input, List<string> reservedWords)
    {
        this.input = input;
        this.reservedWords = reservedWords;
    }
}
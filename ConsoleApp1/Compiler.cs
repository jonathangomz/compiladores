using System;
using System.Collections.Generic;

internal class Compiler: Parser
{

    public Compiler(string input, List<string> reservedWords)
    {
        this.input = input;
        this.reservedWords = reservedWords;
    }
}
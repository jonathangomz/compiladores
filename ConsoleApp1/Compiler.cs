using System;
using System.Collections.Generic;

internal class Compiler: Parser
{

    public Compiler(string input, Dictionary<string, TokenType> reservedWords)
    {
        this.input = input;
        this.reservedWords = reservedWords;
    }

    public Compiler(string input)
    {
        this.input = input;
        reservedWords = new Dictionary<string, TokenType>()
        {
            { "if",TokenType.IF },
            { "else",TokenType.ELSE },
            { "do",TokenType.DO },
            { "while",TokenType.WHILE },
            { "switch",TokenType.SWITCH },
            { "case",TokenType.CASE },
            { "for",TokenType.FOR },
            { "return",TokenType.RETURN },
            { "sen",TokenType.SEN },
            { "cos",TokenType.COS },
            { "tan",TokenType.TAN }
        };
    }
}
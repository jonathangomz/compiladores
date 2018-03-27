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
            // CONDICIONALES
            { "if",             TokenType.IF },
            { "else",           TokenType.ELSE },
            { "switch",         TokenType.SWITCH },
            { "case",           TokenType.CASE },

            // CICLOS
            { "do",             TokenType.DO },
            { "while",          TokenType.WHILE },
            { "for",            TokenType.FOR },

            //GENÉRICOS
            { "print",          TokenType.PRINT },
            { "return",         TokenType.RETURN },

            // OPERACIONES
            { "sen",            TokenType.SEN },
            { "cos",            TokenType.COS },
            { "tan",            TokenType.TAN },

            //TIPOS
            { "int",            TokenType.INT },
            { "float",          TokenType.FLOAT },
            { "char",           TokenType.CHAR },
            
            // MAIN
            { "program",       TokenType.PROGRAM },
        };
    }
}
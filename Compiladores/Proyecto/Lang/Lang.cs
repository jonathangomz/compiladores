using System;
using System.Collections.Generic;
using Proyecto.Data;

namespace Proyecto.Lang
{
    public class Lang : Parser.Parser
    {
        public Lang(string input, Dictionary<string, TokenType> reservedWords)
        {
            this.input = input;
            this.reservedWords = reservedWords;
        }

        public Lang(string input)
        {
            this.input = input;
            reservedWords = new Dictionary<string, TokenType>()
            {
                { "if",             TokenType.IF },
                { "else",           TokenType.ELSE },
                { "switch",         TokenType.SWITCH },
                { "case",           TokenType.CASE },
                { "do",             TokenType.DO },
                { "while",          TokenType.WHILE },
                { "for",            TokenType.FOR },
                { "print",          TokenType.PRINT },
                { "return",         TokenType.RETURN },
                { "sen",            TokenType.SEN },
                { "cos",            TokenType.COS },
                { "tan",            TokenType.TAN },
                { "int",            TokenType.TAN },
                { "float",          TokenType.TAN },
                { "char",           TokenType.TAN },
                { "int_const",      TokenType.TAN },
                { "float_const",    TokenType.TAN },
                { "char_const",     TokenType.TAN },
            };
        }
    }
}


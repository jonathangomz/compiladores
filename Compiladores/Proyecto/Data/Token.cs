using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proyecto.Data
{
    public class Token
    {
        public TokenType TokenType { get; private set; }

        public String TokenValue { get; private set; }

        public Token()
        {
            TokenType = TokenType.TOKEN_NONE;
            TokenValue = "not assigned";
        }

        public Token(TokenType Type, String Token)
        {
            TokenType = Type;
            TokenValue = Token;
        }

    }
}

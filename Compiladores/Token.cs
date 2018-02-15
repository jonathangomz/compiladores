using System;

public class Token
{
    TokenType type;
    String text;

    public Token()
    {
        type = TokenType.TOKEN_NONE;
        text = "not assigned";
    }

    public Token(TokenType t, String text)
    {
        this.type = t;
        this.text = text;
    }

    public TokenType Type
    {
        get { return type; }
        set { type = value; }
    }

    public string Text
    {
        get { return text; }
        set { text = value; }
    }
}
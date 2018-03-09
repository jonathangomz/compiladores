public class Token
{
    string text;

    public Token()
    {
        Type = TokenType.TOKEN_NONE;
        text = "not assigned";
    }

    public Token(TokenType t, string text)
    {
        Type = t;
        this.text = text;
    }

    public TokenType Type { get; set; }

    public string Text
    {
        get { return text; }
        set { text = value; }
    }
}
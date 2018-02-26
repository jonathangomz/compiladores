using System;
using System.Collections.Generic;

using SharpRaven;
using SharpRaven.Data;

public class Compiladores
{

    public static List<String> reservedWords = new List<String>();
    public static String input;

    public Compiladores()
    {
    }

    public static void Main()
    {
        var ravenClient = new RavenClient("https://b75d8a0984ba48c39a2ae86070f9eb04:0ccab29913eb4a34b8c0013ab7036637@sentry.io/291909");

        //ravenClient.Capture(new SentryEvent("Hello World!"));

        ListaLexer l = new ListaLexer();

        input = "a+b*123";
        reservedWords.Add("if");

        List<Token> test = l.getListaToken(input, reservedWords);
        Console.WriteLine(test.GetRange(0,1));
        Console.ReadKey();
    }
}

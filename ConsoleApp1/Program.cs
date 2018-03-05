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
        try
        {
            input = "a+b";
            reservedWords.Add("if");
            Parser par = new Parser(input, reservedWords); 
            Lexer lex = new Lexer();
            List<Token> listTokens = lex.ListOfToken(input, reservedWords);

            float r = par.Expression();
            listTokens.ForEach(delegate (Token tok)
            {
                Console.WriteLine(tok.Text + " => " + tok.Type);
            });

            Console.WriteLine(r);
        }
        catch (Exception ex)
        {
            string ask = "n";
            Console.WriteLine("¿Guardar el error? y/n (Default no)");
            do
            {
                ask = Console.ReadLine();
            } while (!ask.Equals("y") && !ask.Equals("n"));
            if (ask.Equals("y"))
            {
                ravenClient.Capture(new SentryEvent(ex));
                ravenClient.ErrorOnCapture = exception =>
                {
                    Console.WriteLine("Ocurrió un error al capturar el... error.");
                };
            }
        }
        Console.ReadKey();
    }
}

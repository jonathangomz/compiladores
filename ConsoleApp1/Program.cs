using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using SharpRaven;
using SharpRaven.Data;

public class Compiladores
{

    public static List<String> reservedWords = new List<String>();
    public static String input;

    public static Regex yes = new Regex(@"(?i)^(y|Y|YES|Yes|yes)$");
    public static Regex no = new Regex(@"(?i)^(n|N|NO|No|no)$");

    public Compiladores()
    {
    }

    public static void Main()
    {
        var ravenClient = new RavenClient("https://b75d8a0984ba48c39a2ae86070f9eb04:0ccab29913eb4a34b8c0013ab7036637@sentry.io/291909");

        try
        {
            /* PROGRAMA PRINCIPAL */
            while (true)
            {
                Console.WriteLine("Introduce el comando: ");

                // Se introduce un comando
                input = Console.ReadLine();

                // Se crea el listado de palabras reservadas (no terminado)
                reservedWords.Add("if");

                // Se inicializan las clases que ejecutarán el chequeo
                Compiler compi = new Compiler(input, reservedWords); // Working...
                Parser par = new Parser();
                Lexer lex = new Lexer();

                // Se obtiene la lista de Tokens del texto ingresado
                List<Token> listTokens = lex.ListOfToken(input, reservedWords);

                // Se imprimen los Tokens para comparación
                listTokens.ForEach(delegate (Token tok)
                {
                    Console.WriteLine(tok.Text + " => " + tok.Type);
                });

                // Se ejecuta el chequeo sintáctico
                float r = par.Expression(input, reservedWords);

                // Se imprime el resultado. En caso de ser 1 la sintaxis es correcta, de ser 0 es incorrecta
                Console.WriteLine("Respuesta => "+r);

                // Pregunta para salir o continuar
                Console.ReadKey();
                AskToExit();
            }
            /*FIN DE PROGRAMA PRINCIPAL */
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERR => "+ex.Message);
            SaveError(ravenClient, ex);
        }
    }

    private static void Exit()
    {
        Environment.Exit(-1);
    }

    private static void AskToExit()
    {
        do
        {
            Console.WriteLine("¿Desea ingresar otro comando? y/n");
            string ansStr = Console.ReadLine();
            if (no.IsMatch(ansStr))
                Exit();
            else if (yes.IsMatch(ansStr))
            {
                Console.WriteLine(" ---- NUEVO COMANDO ----");
                break;
            }
            else
                Console.WriteLine("No se reconoce respuesta => " + ansStr);
        } while (true);
    }

    private static void SaveError(RavenClient ravenClient, Exception ex)
    {
        // Pregunta si se desea guardar el error
        string ask;
        while(true)
        {
            Console.WriteLine("¿Guardar el error? y/[n]");
            ask = Console.ReadLine();

            // Si se desea guardar
            if (yes.IsMatch(ask))
            {
                // Se envía el error
                ravenClient.Capture(new SentryEvent(ex));

                // Si ocurre un error al enviar se muestra
                ravenClient.ErrorOnCapture = exception =>
                {
                    Console.WriteLine("Ocurrió un error al capturar el... error.");
                };
                break;
            }

            // Si no se desea guardar
            else if (no.IsMatch(ask))
            {
                Console.WriteLine("No se guardó");
                break;
            }

            // Si no se reconoce la respuesta
            else Console.WriteLine("No se reconoce comando ingresado => " + ask);
        }
    }
}

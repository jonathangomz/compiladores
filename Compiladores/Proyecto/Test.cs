using System;
using System.Collections.Generic;
using Proyecto.Data;

namespace Proyecto
{
    public class Test
    {

        public static void Main()
        {
            Console.WriteLine("Introduce el comando: ");

            string input = Console.ReadLine();


            Parser.Parser par = new Lang.Lang(input);
            Lexer.Lexer lex = new Lang.Lang(input);
            List<Token> listTokens = lex.ListOfToken();

            listTokens.ForEach(delegate (Token tok)
            {
                Console.WriteLine(tok.TokenValue + " => " + tok.TokenType);
            });

            // Se ejecuta el chequeo sintáctico
            float r = par.TestParser();
            string result = "";

            if (r == 1)
                result = "Bien";
            else
                result = "Mal";
            // Se imprime el resultado. En caso de ser 1 la sintaxis es correcta, de ser 0 es incorrecta
            Console.WriteLine("Respuesta => " + result);
            Console.ReadKey();
        }
    }
}
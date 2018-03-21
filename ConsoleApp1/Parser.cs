using System;
using System.Collections.Generic;

internal class Parser: Lexer
{
    bool advance    = true;         // Para llevar un control del avance de los tokens a analizar
    Token tok       = new Token();
    Stack<Token> PAR = new Stack<Token>();

/* 
         * * Constructores 
 */
    public Parser()
    {
    }

/*
         * * THE TESTER METHOD
         
 * Description: This method is used to test all the program
 *              just is necessary change the method inside. 
 */
    public float Tester()
    {
        tok = NextToken(); // Por mientras **
        return E();
    }

 /* Métodos de la Clase */
    public float Expression() //=> Arreglar lo de paréntesis de clausura
    {
        do
        {
            Termino();
            if (advance)
                Advance(false);
            if (tok.Type == TokenType.SUM || tok.Type == TokenType.RES)
                Advance(true);
        } while(advance);
        // El primer método debe de llevar esto al final**
        if (tok.Type != TokenType.EOL && PAR.Count == 0)
            throw new ParserException(string.Format("Error al final de la línea => '{0}' <<({1})", tok.Text, tok.Type));
        else
            return 1;
    }

    public float Termino()
    {
        do
        {
            Factor();
            if (advance)
                Advance(false);
            if (tok.Type == TokenType.MUL || tok.Type == TokenType.DIV)
                Advance(true);
        } while (advance);
        return 1;
    }

    public float Factor()
    {

        if (tok.Type == TokenType.ID) // Si es ID
            return Advance(1);
        if (tok.Type == TokenType.NUM) // Si es NUM
            return Advance(1);
        if (tok.Type == TokenType.PARA)
            return Par();
        else
            throw new ParserException(string.Format("Se esperaba ID || NUM se obtuvo => '{0}' <<({1})", tok.Text, tok.Type));
    }

/*
 * 
 *      **NUEVO MÉTODO (NOT FUNCTIONAL) ----------------------------->
 * 
 */
    public float E()
    {
        do
        {
            E1();
            if (advance)
                Advance(false);
            if (tok.Type == TokenType.OR)
                Advance(false);
        } while (advance);
        if ((tok.Type != TokenType.EOL || tok.Type == TokenType.PARC) && PAR.Count == 0)
            throw new ParserException(string.Format("Error al final de la línea => '{0}' <<({1})", tok.Text, tok.Type));
        return 1;
    }

    public float E1()
    {
        do
        {
            E2();
            if (advance)
                Advance(false);
            if (tok.Type == TokenType.AND)
                Advance(true);
        } while (advance);
        return 1;
    }

    public float E2()
    {
        if (tok.Type == TokenType.NOT_EQUAL)
            Advance(adv: true);
        return E3();
    }

    public float E3()
    {
        do
        {
            E4();
            if (advance)
                Advance(false);
            if(tok.Type == TokenType.GREATER        ||
                tok.Type == TokenType.LESS          ||
                tok.Type == TokenType.EQUAL         ||
                tok.Type == TokenType.GREATER_EQUAL ||
                tok.Type == TokenType.LESS_EQUAL)
            {
                Advance(true);
            }
        } while (advance);
        return 1;
    }

    public float E4()
    {
        do
        {
            E5();
            if (advance)
                Advance(false);
            if(tok.Type == TokenType.SUM || tok.Type == TokenType.RES)
                Advance(true);
        } while (advance);
        return 1;
    }

    public float E5()
    {
        do
        {
            E6();
            if (advance)
                Advance(false);
            if (tok.Type == TokenType.DIV || tok.Type == TokenType.MUL || tok.Type == TokenType.MOD)
                Advance(true);
        } while (advance);
        return 1;
    }

    public float E6()
    {
        do
        {
            E7();
            if (advance)
                Advance(false);
            if (tok.Type == TokenType.POT)
                Advance(true);
        } while (advance);
        return 1;
    }

    public float E7()
    {
        if (tok.Type == TokenType.SUM ||
            tok.Type == TokenType.RES)
        {
            Advance(true);
        }
        return Op();
    }

    public float Op()
    {
        // Si es un CONST ------- */
        if (tok.Type == TokenType.INT_CONST ||
            tok.Type == TokenType.CHAR_CONST ||
            tok.Type == TokenType.FLOAT_CONST ||
            tok.Type == TokenType.STRING_CONST)
        {
            return Const();
        }
        // Si es un ID ----------*/
        if (tok.Type == TokenType.ID)
        {
            tok = NextToken();
            if (tok.Type == TokenType.PARA)
                return Par();
            else
                return Advance(0);
        }
        // Si es una Expresion -----*/
        if (tok.Type == TokenType.PARA)
            return Par();
        else
            throw new ParserException(string.Format("Se esperaba ID | CONST | PARA, se obtuvo => '{0}' << ({1})", tok.Text, tok.Type));
    }

    public float Const()
    {
        if (tok.Type == TokenType.INT_CONST)
            return Advance(1);
        if (tok.Type == TokenType.CHAR_CONST)
            return Advance(1);
        if (tok.Type == TokenType.FLOAT_CONST)
            return Advance(1);
        if (tok.Type == TokenType.STRING_CONST)
            return Advance(1);
        else
            throw new ParserException(string.Format("Se esperaba un CONST se obtuvo => '{0}' <<({1})", tok.Text, tok.Type));
    }

    public float Out()
    {
        tok = NextToken();
        if (tok.Type != TokenType.PARA)
            throw new ParserException(string.Format("Se esperaba PARA, se obtuvo => '{0}' <<({1})", tok.Text, tok.Type));
        do
        {
            Expression();
            if (advance)
                Advance(false);
            if (tok.Type == TokenType.COMA)
                Advance(true);
        } while (advance);
        if (NextToken().Type != TokenType.PARA)
            throw new ParserException(string.Format("Se esperaba PARC, se obtuvo => '{0}' <<({1})", tok.Text, tok.Type));
        return 1;
    }

/*
 *
 *  EXTRAS
 *  Description: Reduce lines of code above using this methods.
 * 
 */
    public float Par()
    {
        /*
         *  Check Paréntesis 
         */
        PAR.Push(tok);
        Advance(false);
        E();
        if (tok.Type == TokenType.PARC) // Deberá tener paréntesis de cierre
        {
            PAR.Pop();
            Advance(false);
            if (tok.Type == TokenType.PARA)
                Par();
            return Advance(0);
        }
        else
        {
            throw new ParserException(string.Format("Se esperaba PARC, se obtuvo => '{0}' <<({1})", tok.Text, tok.Type));
        }
    }
    public float Advance(byte i)
    {
        /*
         *  This method is used
         *  in final ways of the code.
         */
        if (i==0)
            advance = false;
        else
            advance = true;
        return 1;
    }
    public void Advance(bool adv)
    {
        /*
         *  This method is used
         *  to advance the token.
         */
        tok = NextToken();
        advance = adv;
    }
}
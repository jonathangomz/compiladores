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
        return Inicio();
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
            throw new ParserException(string.Format("Error al final de la línea "+errorToken, tok.Text, tok.Type));
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
            throw new ParserException(string.Format("Se esperaba ID || NUM se obtuvo " + errorToken, tok.Text, tok.Type));
    }

    /*
     * 
     *      **NUEVO MÉTODO (NOT FUNCTIONAL) ----------------------------->
     * 
     */
    public float Inicio()
    {
        if (tok.Type == TokenType.PROGRAM)
        {
            Advance(false);
            if (tok.Type == TokenType.ID)
            {
                Advance(false);
                return Bloque();
            }
            else
                throw new ParserException(string.Format("Se esperaba un ID, se obutvo " + errorToken, tok.Text, tok.Type));
        }
        else
            throw new ParserException(string.Format("Se esperaba PROGRAM, se obtuvo " + errorToken, tok.Text, tok.Type));
    }

    public float Bloque()
    {
        if (tok.Type == TokenType.LLAVEA)
        {
            Advance(false);
            Orden();
            if (tok.Type == TokenType.LLAVEC)
                return 1;
            else
                throw new ParserException(string.Format("Se esperaba LLAVEC, se obtuvo " + errorToken, tok.Text, tok.Type));
        }
        else
            throw new ParserException(string.Format("Se esperaba LLAVEA, se obutvo " + errorToken, tok.Text, tok.Type));
    }

    public float Orden()
    {
        if (Tipo())
            return Dec_var();
        if (tok.Type == TokenType.INPUT)
            return In();
        if (tok.Type == TokenType.PRINT)
            return Out();
        if (tok.Type == TokenType.LLAVEA)
            return Bloque();
        if (tok.Type == TokenType.IF)
            return Bloque_if();
        if (tok.Type == TokenType.DO)
            return Bloque_do();
        if (tok.Type == TokenType.WHILE)
            return Bloque_while();
        if (tok.Type == TokenType.FOR)
            return Bloque_for();
        if (tok.Type == TokenType.SWITCH)
            return Bloque_switch();
        if (tok.Type == TokenType.RETURN)
        {
            Advance(false);
            if (tok.Type != TokenType.SEMICOLON)
            {
                E();
                if (tok.Type == TokenType.SEMICOLON)
                {
                    Advance(false);
                    return 1;
                }
                else
                    throw new ParserException(string.Format("Se esperaba SEMICOLON, se obtuvo " + errorToken, tok.Text, tok.Type));
            }
            else
            {
                Advance(false);
                return 1;
            }
        }
        if (tok.Type == TokenType.ID)
        {
            Variable();
            if (tok.Type == TokenType.ASIGNA)
            {
                E();
                if (tok.Type == TokenType.SEMICOLON)
                    return 1;
                else
                    throw new ParserException(string.Format("Se esperaba SEMICOLON, se obtuvo " + errorToken, tok.Text, tok.Type));
            }
            else
                throw new ParserException(string.Format("Se esperaba ASIGN, se obtuvo " + errorToken, tok.Text, tok.Type));
        }
        else
            throw new ParserException(string.Format("No se reconoce ninguna llamada. ERR " + errorToken, tok.Text, tok.Type));
    }

    public float Dec_var()
    {
        do
        {
            if(advance)
                Advance(false);
            if (tok.Type == TokenType.ID)
            {
                Advance(false);
                if (tok.Type == TokenType.CORCHETEA)
                {
                    Advance(false);
                    if (Const())
                    {
                        if (tok.Type == TokenType.CORCHETEC)
                        {
                            Advance(false);
                            if (tok.Type == TokenType.COMA)
                                Advance(true);
                        }
                        else
                            throw new ParserException(string.Format("Se esperaba CORCHETEC, se obtuvo " + errorToken, tok.Text, tok.Type));
                    }
                    else
                        throw new ParserException(string.Format("Se esperaba CONST, se obtuvo " + errorToken, tok.Text, tok.Type));
                    
                }
                if (tok.Type != TokenType.SEMICOLON)
                    throw new ParserException(string.Format("Se esperaba SEMICOLON, se obtuvo " + errorToken, tok.Text, tok.Type));
            }
            else
                throw new ParserException(string.Format("Se esperaba ID, se obtuvo " + errorToken, tok.Text, tok.Type));
        } while (advance);
        return 1;
    }

    public float Bloque_if()
    {
        Advance(false);
        if (tok.Type == TokenType.PARA)
        {
            E();
            if (tok.Type == TokenType.PARC)
            {
                Bloque();
                if (tok.Type == TokenType.ELSE)
                    Bloque();
                return 1;
            }
            else
                throw new ParserException(string.Format("Se esperaba PARC, se obtuvo " + errorToken, tok.Text, tok.Type));
        }
        else
            throw new ParserException(string.Format("Se esperaba PARA, se obtuvo " + errorToken, tok.Text, tok.Type));
    }

    public float Bloque_do()
    {
        Advance(false);
        Bloque();
        if (tok.Type == TokenType.WHILE)
        {
            Advance(false);
            if (tok.Type == TokenType.PARA)
            {
                Par();
                if (tok.Type == TokenType.SEMICOLON)
                    return 1;
                else
                    throw new ParserException(string.Format("Se esperaba SEMICOLON, se obtuvo " +errorToken, tok.Text, tok.Type));
            }
            else
                throw new ParserException(string.Format("Se esperaba PARA, se obtuvo " + errorToken, tok.Text, tok.Type));
        }
        else
            throw new ParserException(string.Format("Se esperaba WHILE, se obtuvo " + errorToken, tok.Text, tok.Type));
    }

    public float Bloque_while()
    {
        if (tok.Type == TokenType.PARA)
        {
            Par();
        }
        return Bloque();
    }

    public float Bloque_for()
    {
        Advance(false);
        if (tok.Type == TokenType.PARA)
        {
            Advance(false);
            if (tok.Type == TokenType.ID)
            {
                Advance(false);
                if (tok.Type == TokenType.ASIGNA)
                {
                    Advance(false);
                    E();
                }
                else
                    throw new ParserException(string.Format("Se esperaba ASIGNA, se obtuvo " + errorToken, tok.Text, tok.Type));
            }
            else if (tok.Type != TokenType.SEMICOLON)
            {
                throw new ParserException(string.Format("Se esperaba SEMICOLON, se obtuvo " + errorToken, tok.Text, tok.Type));
            }

            Advance(false);
            if(tok.Type != TokenType.SEMICOLON) { }
            {
                E();
                if(tok.Type != TokenType.SEMICOLON)
                    throw new ParserException(string.Format("Se esperaba SEMICOLON, se obtuvo " + errorToken, tok.Text, tok.Type));
            }

            Advance(false);
            if (tok.Type == TokenType.ID)
            {
                Advance(false);
                if (tok.Type == TokenType.ASIGNA)
                {
                    Advance(false);
                    E();
                }
                else
                    throw new ParserException(string.Format("Se esperaba ASIGNA, se obtuvo " + errorToken, tok.Text, tok.Type));
            }
            if (tok.Type == TokenType.PARC)
                return 1;
            else
                throw new ParserException(string.Format("Se esperaba PARC, se obtuvo " + errorToken, tok.Text, tok.Type));
        }
        else
            throw new ParserException(string.Format("Se esperaba PARA, se obtuvo " + errorToken, tok.Text, tok.Type));
    }

    public float Bloque_switch()
    {
        Advance(false);
        if (tok.Type == TokenType.PARA)
        {
            Par();
            if (tok.Type == TokenType.LLAVEA)
            {
                Advance(false);
                do
                {
                    if (advance)
                        Advance(false);
                    if (tok.Type == TokenType.CASE)
                    {
                        Advance(false);
                        E();
                        if (tok.Type == TokenType.DOUBLE_POINT)
                            Advance(true);
                        else
                            throw new ParserException("test");
                    }
                    if (tok.Type == TokenType.DEFAULT)
                    {
                        Advance(false);
                        if (tok.Type == TokenType.DOUBLE_POINT)
                            Advance(true);
                        else
                            throw new ParserException("test");
                    }
                    else
                        Bloque();
                } while (tok.Type == TokenType.LLAVEC);
                Advance(false);
                return 1;
            }
            else
                throw new ParserException(string.Format("Se esperaba un LLAVEA, se obtuvo " + errorToken, tok.Text, tok.Type));
        }
        else
            throw new ParserException(string.Format("Se esperaba un PARA, se obtuvo " + errorToken, tok.Text, tok.Type));
    }
    
    public float E()
    {
        do
        {
            E1();
            if (advance)
                Advance(false);
            if (tok.Type == TokenType.OR)
                Advance(true);
        } while (advance);
        if ((tok.Type != TokenType.EOL || tok.Type == TokenType.PARC) && PAR.Count == 0)
            throw new ParserException(string.Format("Error al final de la línea " + errorToken, tok.Text, tok.Type));
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
            Advance(true);
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
            Advance(false);
        }
        return Op();
    }

    public float Op()
    {
        // Si es un CONST ------- */
        if (Const())
        {
            return 1;
        }
        // Si es un ID ----------*/
        if (tok.Type == TokenType.ID)
        {
            tok = NextToken();
            if (tok.Type == TokenType.PARA)
                return Par();
            else
                return 1;
        }
        // Si es una Expresion -----*/
        if (tok.Type == TokenType.PARA)
            return Par();
        else
            throw new ParserException(string.Format("Se esperaba ID | CONST | PARA, se obtuvo " + errorToken, tok.Text, tok.Type));
    }

    public float In()
    {
        Advance(false);
        if (tok.Type == TokenType.PARA)
        {
            do
            {
                Variable();
                if (advance)
                    Advance(false);
                if (tok.Type == TokenType.COMA)
                    Advance(true);
            } while (advance);
            if (NextToken().Type == TokenType.PARA)
                return 1;
            else
                throw new ParserException(string.Format("Se esperaba PARC, se obtuvo " + errorToken, tok.Text, tok.Type));
        }
        else
            throw new ParserException(string.Format("Se esperaba PARA, se obtuvo " + errorToken, tok.Text, tok.Type));
    }

    public float Out()
    {
        Advance(false);
        if (tok.Type != TokenType.PARA)
        {
            do
            {
                Expression();
                if (advance)
                    Advance(false);
                if (tok.Type == TokenType.COMA)
                    Advance(true);
            } while (advance);
            if (NextToken().Type == TokenType.PARA)
                return 1;
            else
                throw new ParserException(string.Format("Se esperaba PARC, se obtuvo " + errorToken, tok.Text, tok.Type));
        }
        else
            throw new ParserException(string.Format("Se esperaba PARA, se obtuvo " + errorToken, tok.Text, tok.Type));
    }

    public bool Tipo()
    {
        if (tok.Type == TokenType.CHAR ||
           tok.Type == TokenType.FLOAT ||
           tok.Type == TokenType.INT)
        {
            Advance(false);
            return true;
        }
        else
            return false;
    }

    public bool Const()
    {
        if (tok.Type == TokenType.INT_CONST ||
            tok.Type == TokenType.CHAR_CONST ||
            tok.Type == TokenType.FLOAT_CONST)
        {
            Advance(false);
            return true;
        }
        else
            return false;
    }

    public float Variable()
    {
        Advance(false);
        if (tok.Type == TokenType.CORCHETEA)
        {
            Advance(false);
            if (Const())
            {
                if (tok.Type == TokenType.CORCHETEC) {
                    Advance(false);
                    return 1;
                }
                else
                    throw new ParserException(string.Format("Se esperaba CORCHETEC, se obtuvo " + errorToken, tok.Text, tok.Type));
            }
            else
                throw new ParserException(string.Format("Se esperaba CONST, se obtuvo " + errorToken, tok.Text, tok.Type));
        }
        else
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
            return 1;
        }
        else
        {
            throw new ParserException(string.Format("Se esperaba PARC, se obtuvo " + errorToken, tok.Text, tok.Type));
        }
    }
    public float Advance(byte i)
    {
        /*
         *  This method is used
         *  in final ways of the code.
         */
        if (i==1)
            advance = true;
        else
            advance = false;
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
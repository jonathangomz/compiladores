using System;
using System.Collections.Generic;
using Proyecto.Data;

namespace Proyecto.Parser
{

    public class Parser : Lexer.Lexer
    {
        bool advance = true;
        Data.Token token = new Data.Token();
        Stack<Data.Token> PAR = new Stack<Data.Token>();

        string result = "";

        public Parser()
        {
        }

        public float TestParser()
        {
            token = NextToken();
            return E();
        }

        //Metodos
        public int Expression()
        {
            do
            {
                Termino();
                if (advance)
                    Advance(false);
                if (token.TokenType == TokenType.SUM || token.TokenType == TokenType.RES)
                    Advance(true);
                    
            } while (advance);
            if (token.TokenType != TokenType.EOL && PAR.Count == 0)
                return 0;
            return 1;
        }

        public int Termino()
        {
            do
            {
                Factor();
                if (advance)
                    Advance(false);
                if (token.TokenType == TokenType.MUL || token.TokenType == TokenType.DIV)
                    Advance(true);
            } while (advance);
            return 1;
        }

        public int Factor()
        {
            if (token.TokenType == TokenType.ID)
                return Advance(1);
            if (token.TokenType == TokenType.NUM) 
                return Advance(1);
            if (token.TokenType == TokenType.PARA)
                return checkPAR();
            else
                return 0;
        }

        public int E()
        {
            do
            {
                E1();
                if (advance)
                    Advance(false);
                if (token.TokenType == TokenType.OR)
                    Advance(false);
                    
            } while (advance);
            if ((token.TokenType != TokenType.EOL || token.TokenType == TokenType.PARC) && PAR.Count == 0)
                return 0;
            return 1;
        }

        public int E1()
        {
            do
            {
                E2();
                if (advance)
                    Advance(false);
                if (token.TokenType == TokenType.AND)
                    Advance(true);
            } while (advance);
            return 1;
        }

        public int E2()
        {
            if (token.TokenType == TokenType.NOT_EQUAL)
                Advance(adv: true);
            return E3();
        }

        public int E3()
        {
            do
            {
                E4();
                if (advance)
                    Advance(false);
                if (token.TokenType == TokenType.GREATER ||
                    token.TokenType == TokenType.LESS ||
                    token.TokenType == TokenType.EQUAL ||
                    token.TokenType == TokenType.GREATER_EQUAL ||
                    token.TokenType == TokenType.LESS_EQUAL)
                {
                    Advance(true);
                }
            } while (advance);
            return 1;
        }

        public int E4()
        {
            do
            {
                E5();
                if (advance)
                    Advance(false);
                if (token.TokenType == TokenType.SUM || token.TokenType == TokenType.RES)
                    Advance(true);
            } while (advance);
            return 1;
        }

        public int E5()
        {
            do
            {
                E6();
                if (advance)
                    Advance(false);
                if (token.TokenType == TokenType.DIV || token.TokenType == TokenType.MUL || token.TokenType == TokenType.MOD)
                    Advance(true);
            } while (advance);
            return 1;
        }

        public int E6()
        {
            do
            {
                E7();
                if (advance)
                    Advance(false);
                if (token.TokenType == TokenType.POT)
                    Advance(true);
            } while (advance);
            return 1;
        }

        public int E7()
        {
            if (token.TokenType == TokenType.SUM ||
                token.TokenType == TokenType.RES)
            {
                Advance(true);
            }
            return OP();
        }

        public int OP()
        {
            // Caso: CONS
            if (token.TokenType == TokenType.INT_CONST ||
                token.TokenType == TokenType.CHAR_CONST ||
                token.TokenType == TokenType.FLOAT_CONST ||
                token.TokenType == TokenType.STRING_CONST)
            {
                return CONS();
            }
            // Caso: ID
            if (token.TokenType == TokenType.ID)
            {
                token = NextToken();
                if (token.TokenType == TokenType.PARA)
                    return checkPAR();
                else
                    return Advance(0);
            }
            // Caso: Expresion
            if (token.TokenType == TokenType.PARA)
                return checkPAR();
            else
                return 0;
        }

        public int CONS()
        {
            if (token.TokenType == TokenType.INT_CONST)
                return Advance(1);
            if (token.TokenType == TokenType.CHAR_CONST)
                return Advance(1);
            if (token.TokenType == TokenType.FLOAT_CONST)
                return Advance(1);
            if (token.TokenType == TokenType.STRING_CONST)
                return Advance(1);
            else
                return 0;
        }

        public int OUT()
        {
            token = NextToken();
            if (token.TokenType != TokenType.PARA)
                
            do
            {
                Expression();
                if (advance)
                    Advance(false);
                if (token.TokenType == TokenType.COMA)
                    Advance(true);
                    
            } while (advance);
            if (NextToken().TokenType != TokenType.PARA)
                return 0;
            return 1;
        }

        public int checkPAR()
        {
            PAR.Push(token);
            Advance(false);
            E();
            if (token.TokenType == TokenType.PARC)
            {
                PAR.Pop();
                Advance(false);
                if (token.TokenType == TokenType.PARA)
                    checkPAR();
                return Advance(0);
            }
            else
            {
                return 0;
            }
        }
        public int Advance(byte i)
        {
            if (i == 0)
                advance = false;
            else
                advance = true;
            return 1;
        }
        public void Advance(bool adv)
        {
            token = NextToken();
            advance = adv;
        }
    }
}
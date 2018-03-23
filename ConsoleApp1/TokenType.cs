public enum TokenType
{
    //MAIN
    PROGRAM         = 1,
    ID              = 10,
    //OPERATORS
    MUL             = 2,
    SUM             = 3,
    MOD             = 4,
    DIV             = 5,
    RES             = 6,
    POT             = 7,
/*
    INT_CONST       = 5,
    CHAR_CONST      = 6,
    FLOAT_CONST     = 7,
    STRING_CONST    = 8,
*/
    NUM             = 9,
    //GROUPER
    PARA            = 12,
    PARC            = 13,
    LLAVEA          = 14,
    LLAVEC          = 15,
    CORCHETEA       = 16,
    CORCHETEC       = 17,
    //CONDITIONALS
    IF              = 20,
    ELSE            = 21,
    SWITCH          = 22,
    CASE            = 23,
    DEFAULT         = 24,
    GREATER         = 30,
    LESS            = 31,
    EQUAL           = 32,
    NOT             = 33,
    NOT_EQUAL       = 34,
    GREATER_EQUAL   = 35,
    LESS_EQUAL      = 36,
    OR              = 37,
    AND             = 38,
    //SEPARATORS
    COMA            = 40,
    //ASIGNATORS
    ASIGNA          = 45,
    //LOOPS
    DO              = 50,
    WHILE           = 51,
    FOR             = 52,
    //ARITHMETIC
    SEN             = 60,
    COS             = 61,
    TAN             = 62,
    //RESERVERD WORDS AND TOOLS FOR DEBUGGING
    RETURN          = 70,
    SEMICOLON       = 75,
    DOUBLE_POINT    = 76,
    EOL             = 80,
    TOKEN_NONE      = 90,
    RESERVED        = 100,
    // FUNCTIONS
    PRINT           = 200,
    INPUT           = 201,
    // TIPOS
    INT             = 500,
    FLOAT           = 501,
    CHAR            = 502,
    INT_CONST       = 550,
    FLOAT_CONST     = 551,
    CHAR_CONST      = 552
}
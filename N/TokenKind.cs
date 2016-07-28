namespace N
{
    public enum TokenKind
    {
        Whitespace,
        Tab,
        NewLine,
        DoubleQuote,
        SingleQuote,
        Ampersand,
        DollarSign,
        EuroSign,
        NumberSign,
        Exclamation,
        RBrace,
        LBrace,
        RCurlyBrace,
        LCurlyBrace,
        RSquareBrace,
        LSquareBrace,
        Asterisk,
        ForwardSlash,
        Backslash,
        GreaterThan,
        LessThan,
        SectionSign,
        Plus,
        Hyphen,
        Caret,
        Underscore,
        Dot,
        Comma,
        Colon,
        Semicolon,
        Pipe,
        CurrencySign,
        Tilde,
        GraveAccent,
        Diacritical,
        Equals,
        NumericLiteral,
        StringLiteral,
        CharLiteral,
        Identifier,
        Micro,

        // Increments
        Increment,                      // x++
        Decrement,                      // x--
        Power,                          // 4 ** 2 = 4 * 4 = 16

        // Assignments
        AdditionAssignment,             // x += y
        SubtractionAssignment,          // x -= y
        MultiplicationAssignment,       // x *= y
        DivisionAssignment,             // x /= y

        BitwiseAnd = Ampersand,         // v1: "x & y" v2: "x AND y"
        BitwiseOr = Pipe,               // v1: "x | y" v2: "x OR y"
        BitwiseExclusiveAnd,
        BitwiseExclusiveOr,
        BitwiseShiftRight,
        BitwiseShiftLeft,
    }
}
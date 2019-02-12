namespace CRBasic.PyBasic
{
    /// <summary>
    /// Data Types for encoded literals. 
    /// </summary>
    public enum DataTypes
    {
        StartOfLine = 0,
        EndOfStatement = 1, // : end of statement
        String = 2,         // $ String literal
        Number = 3,         // % 32 bit integer 
        Text = 6,           // raw text (probably a REMark)
        Variable = 7,       // variable reference - 32-bit ID of variable
        Token = 8,          // 24-bit comamnd token (making this a 32 bit number in the data stream)
        Delimiter = 9,      // commas and other delimiters in lists
        Operator = 11,
        EndOfLine = 99,     // end of program line
    }


}
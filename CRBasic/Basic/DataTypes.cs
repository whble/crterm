namespace CRBasic.Basic
{
    /// <summary>
    /// Data Types for encoded literals. 
    /// </summary>
    public enum DataTypes
    {
        EndOfLine = 0,      // end of program line
        EndOfStatement = 1, // : end of statement
        String = 2,         // $ String literal
        Integer = 3,            // % 32 bit integer 
        Single = 4,         // ! 32-bit float 
        Double = 5,         // # 64-bit float 
        Text = 6,           // raw text (probably a REMark)
        Variable = 7,       // variable reference - 32-bit ID of variable
        Token = 8,          // 24-bit comamnd token (making this a 32 bit number in the data stream)
        Delimiter = 9,      // commas and other delimiters in lists
    }


}
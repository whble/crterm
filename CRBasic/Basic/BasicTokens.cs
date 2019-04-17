using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRBasic.Basic
{
    /// <summary>
    /// Tokens represent all valid commands and symbols in a BASIC program. Each token is a 16-bit number that
    /// represents an ASCII character or a single BASIC keyword or expression. 
    /// </summary>
    public class BasicTokens
    {
        public static SortedList<string, BasicToken> Commands = new SortedList<string, BasicToken>();
        public static SortedList<UInt16, BasicToken> Tokens = new SortedList<ushort, BasicToken>();
        public static SortedSet<char> OperatorChars = new SortedSet<char>();
        public static SortedList<string, BasicOperator> Operators = new SortedList<string, BasicOperator>();

        public static SortedSet<string> Sigils = new SortedSet<string>
        {
            "$", // string
            "%", // integer
            "!", // float-single
            "#"  // float-double
        };

        public static SortedSet<string> ListSeperators = new SortedSet<string>
        {
            ","
        };
        public static SortedSet<string> PrintSeperators = new SortedSet<string>
        {
            ",",";"
        };

        public void Add(string Name, UInt16 Code, BasicToken.CommandDelegate Command)
        {
            BasicToken t = new BasicToken(Name, Code, Command);
            Add(t);
        }

        public void AddOperator(string Name, int Precedence, UInt16 Code, BasicToken.CommandDelegate Translate)
        {
            BasicOperator op = new BasicOperator(Name, Precedence, Code, Translate);
            Operators.Add(Name, op);
        }

        /// <summary>
        /// Adds an alias for a command or function. 
        /// </summary>
        /// <param name="Alias">The new text (ie ?)</param>
        /// <param name="Original">The original command (ie: PRINT)</param>
        private void AddAlias(string Alias, string Original)
        {
            if (Commands.ContainsKey(Original))
                Commands.Add(Alias, Commands[Original]);

            if (Operators.ContainsKey(Original))
                Operators.Add(Alias, Operators[Original]);
        }

        public void Add(BasicToken Token)
        {
            Commands.Add(Token.Name, Token);
            // allow two tokens to share a definition
            if (Token.Code != 0)
                Tokens.Add(Token.Code, Token);
        }

        public BasicTokens()
        {
            // only initialize static data once
            if (Tokens.Count == 0)
            {
                AddTokens();
                AddOperators();
                AddOther();
            }
        }

        private void AddOther()
        {
            AddAlias("?", "PRINT");
            AddAlias("DATE$", "DAT$");
            AddAlias("==", "=");
            AddAlias("=<", "<=");
            AddAlias("=>", ">=");
        }

        private void AddOperators()
        {
            AddOperator("(", 13, 0x1b5, Translate_NoChange);
            AddOperator(")", 13, 0x1b6, Translate_NoChange);
            AddOperator("^", 12, 0x1b7, Translate_NoChange);
            AddOperator("*", 11, 0x1b8, Translate_NoChange);
            AddOperator("/", 11, 0x1b9, Translate_NoChange);
            AddOperator("\\", 10, 0x1ba, Translate_DivideInt);
            AddOperator("MOD", 9, 0x15b, Translate_Mod);
            AddOperator("+", 8, 0x1bb, Translate_NoChange);
            AddOperator("-", 8, 0x1bc, Translate_NoChange);
            AddOperator("=", 7, 0x1bd, Translate_Equal);
            AddOperator("<", 7, 0x1be, Translate_NoChange);
            AddOperator(">", 7, 0x1bf, Translate_NoChange);
            AddOperator("<>", 7, 0x1c0, Translate_NotEqual);
            AddOperator("<=", 7, 0x1c1, Translate_NoChange);
            AddOperator("=>", 7, 0x1c2, Translate_NoChange);
            AddOperator("NOT", 6, 0x14b, Translate_Not);
            AddOperator("AND", 5, 0x156, Translate_And);
            AddOperator("OR", 4, 0x157, Translate_Or);
            AddOperator("XOR", 3, 0x158, Translate_Xor);
            AddOperator("EQV", 2, 0x159, Translate_Eqv);
            AddOperator("IMP", 1, 0x15a, Translate_Imp);
        }

        private void Translate_Imp(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Eqv(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Xor(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Or(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_And(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Not(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_NotEqual(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Equal(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Mod(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_DivideInt(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Translate a BASIC token to the target language with no changes. 
        /// </summary>
        /// <param name="Program"></param>
        /// <param name="SourceLine"></param>
        private void Translate_NoChange(BasicProgram Program, ProgramLine SourceLine)
        {
        }

        private void AddTokens()
        {
            Add("END", 0x100, Translate_End);
            Add("FOR", 0x101, Translate_For);
            Add("NEXT", 0x102, Translate_Next);
            Add("DATA", 0x103, Translate_Data);
            Add("INPUT", 0x104, Translate_Input);
            Add("DIM", 0x105, Translate_Dim);
            Add("READ", 0x106, Translate_Read);
            Add("LET", 0x107, Translate_Let);
            Add("GOTO", 0x108, Translate_Goto);
            Add("RUN", 0x109, Translate_Run);
            Add("IF", 0x10A, Translate_If);
            Add("RESTORE", 0x10B, Translate_Restore);
            Add("GOSUB", 0x10C, Translate_Gosub);
            Add("RETURN", 0x10D, Translate_Return);
            Add("REM", 0x10E, Translate_Rem);
            Add("STOP", 0x10F, Translate_Stop);
            Add("PRINT", 0x110, Translate_Print);
            Add("CLEAR", 0x111, Translate_Clear);
            Add("LIST", 0x112, Translate_List);
            Add("NEW", 0x113, Translate_New);
            Add("ON", 0x114, Translate_On);
            Add("WAIT", 0x115, Translate_Wait);
            Add("DEF", 0x116, Translate_Def);
            Add("POKE", 0x117, Translate_Poke);
            Add("CONT", 0x118, Translate_Cont);
            Add("OUT", 0x119, Translate_Out);
            Add("LPRINT", 0x11A, Translate_Lprint);
            Add("LLIST", 0x11B, Translate_Llist);
            Add("WIDTH", 0x11C, Translate_Width);
            Add("ELSE", 0x11D, Translate_Else);
            Add("TRON", 0x11E, Translate_Tron);
            Add("TROFF", 0x11F, Translate_Troff);
            Add("SWAP", 0x120, Translate_Swap);
            Add("ERASE", 0x121, Translate_Erase);
            Add("EDIT", 0x122, Translate_Edit);
            Add("ERROR", 0x123, Translate_Error);
            Add("RESUME", 0x124, Translate_Resume);
            Add("DELETE", 0x125, Translate_Delete);
            Add("AUTO", 0x126, Translate_Auto);
            Add("RENUM", 0x127, Translate_Renum);
            Add("DEFSTR", 0x128, Translate_Defstr);
            Add("DEFINT", 0x129, Translate_Defint);
            Add("DEFSNG", 0x12A, Translate_Defsng);
            Add("DEFDBL", 0x12B, Translate_Defdbl);
            Add("LINE", 0x12C, Translate_Line);
            Add("WHILE", 0x12D, Translate_While);
            Add("WEND", 0x12E, Translate_Wend);
            Add("CALL", 0x12F, Translate_Call);
            Add("WRITE", 0x130, Translate_Write);
            Add("OPTION", 0x131, Translate_Option);
            Add("RANDOMIZE", 0x132, Translate_Randomize);
            Add("OPEN", 0x133, Translate_Open);
            Add("CLOSE", 0x134, Translate_Close);
            Add("LOAD", 0x135, Translate_Load);
            Add("MERGE", 0x136, Translate_Merge);
            Add("SAVE", 0x137, Translate_Save);
            Add("COLOR", 0x138, Translate_Color);
            Add("CLS", 0x139, Translate_Cls);
            Add("MOTOR", 0x13A, Translate_Motor);
            Add("BSAVE", 0x13B, Translate_Bsave);
            Add("BLOAD", 0x13C, Translate_Bload);
            Add("SOUND", 0x13D, Translate_Sound);
            Add("BEEP", 0x13E, Translate_Beep);
            Add("PSET", 0x13F, Translate_Pset);
            Add("PRESET", 0x140, Translate_Preset);
            Add("SCREEN", 0x141, Translate_Screen);
            Add("KEY", 0x142, Translate_Key);
            Add("LOCATE", 0x143, Translate_Locate);
            Add("TO", 0x144, Translate_To);
            Add("THEN", 0x145, Translate_Then);
            Add("TAB", 0x146, Translate_Tab);
            Add("STEP", 0x147, Translate_Step);
            Add("USR", 0x148, Translate_Usr);
            Add("FN", 0x149, Translate_Fn);
            Add("SPC", 0x14A, Translate_Spc);
            Add("ERL", 0x14C, Translate_Erl);
            Add("ERR", 0x14D, Translate_Err);
            Add("USING", 0x14F, Translate_Using);
            Add("INSTR", 0x150, Translate_Instr);
            Add("VARPTR", 0x151, Translate_Varptr);
            Add("CSRLIN", 0x152, Translate_Csrlin);
            Add("POINT", 0x153, Translate_Point);
            Add("OFF", 0x154, Translate_Off);
            Add("INKEY$", 0x155, Translate_Inkey);
            Add("CVI", 0x15C, Translate_Cvi);
            Add("CVS", 0x15D, Translate_Cvs);
            Add("CVD", 0x15E, Translate_Cvd);
            Add("MKI$", 0x15F, Translate_Mki);
            Add("MKS$", 0x160, Translate_Mks);
            Add("MKD$", 0x161, Translate_Mkd);
            Add("EXTERR", 0x162, Translate_Exterr);
            Add("FILES", 0x163, Translate_Files);
            Add("FIELD", 0x164, Translate_Field);
            Add("SYSTEM", 0x165, Translate_System);
            Add("NAME", 0x166, Translate_Name);
            Add("LSET", 0x167, Translate_Lset);
            Add("RSET", 0x168, Translate_Rset);
            Add("KILL", 0x169, Translate_Kill);
            Add("PUT", 0x16A, Translate_Put);
            Add("GET", 0x16B, Translate_Get);
            Add("RESET", 0x16C, Translate_Reset);
            Add("COMMON", 0x16D, Translate_Common);
            Add("CHAIN", 0x16E, Translate_Chain);
            Add("DAT$", 0x16F, Translate_Date);
            Add("TIME$", 0x170, Translate_Time);
            Add("PAINT", 0x171, Translate_Paint);
            Add("COM", 0x172, Translate_Com);
            Add("CIRCLE", 0x173, Translate_Circle);
            Add("DRAW", 0x174, Translate_Draw);
            Add("PLAY", 0x175, Translate_Play);
            Add("TIMER", 0x176, Translate_Timer);
            Add("ERDEV", 0x177, Translate_Erdev);
            Add("IOCTL", 0x178, Translate_Ioctl);
            Add("CHDIR", 0x179, Translate_Chdir);
            Add("MKDIR", 0x17A, Translate_Mkdir);
            Add("RMDIR", 0x17B, Translate_Rmdir);
            Add("SHELL", 0x17C, Translate_Shell);
            Add("ENVIRON", 0x17D, Translate_Environ);
            Add("VIEW", 0x17E, Translate_View);
            Add("WINDOW", 0x17F, Translate_Window);
            Add("PMAP", 0x180, Translate_Pmap);
            Add("PALETTE", 0x181, Translate_Palette);
            Add("LCOPY", 0x182, Translate_Lcopy);
            Add("CALLS", 0x183, Translate_Calls);
            Add("PCOPY", 0x184, Translate_Pcopy);
            Add("LOCK", 0x185, Translate_Lock);
            Add("UNLOCK", 0x186, Translate_Unlock);
            Add("LEFT$", 0x187, Translate_Left);
            Add("RIGHT$", 0x188, Translate_Right);
            Add("MID$", 0x189, Translate_Mid);
            Add("SGN", 0x18A, Translate_Sgn);
            Add("INT", 0x18B, Translate_Int);
            Add("ABS", 0x18C, Translate_Abs);
            Add("SQR", 0x18D, Translate_Sqr);
            Add("RND", 0x18E, Translate_Rnd);
            Add("SIN", 0x18F, Translate_Sin);
            Add("LOG", 0x190, Translate_Log);
            Add("EXP", 0x191, Translate_Exp);
            Add("COS", 0x192, Translate_Cos);
            Add("TAN", 0x193, Translate_Tan);
            Add("ATN", 0x194, Translate_Atn);
            Add("FRE", 0x195, Translate_Fre);
            Add("INP", 0x196, Translate_Inp);
            Add("POS", 0x197, Translate_Pos);
            Add("LEN", 0x198, Translate_Len);
            Add("STR$", 0x199, Translate_Str);
            Add("VAL", 0x19A, Translate_Val);
            Add("ASC", 0x19B, Translate_Asc);
            Add("CHR$", 0x19C, Translate_Chr);
            Add("PEEK", 0x19D, Translate_Peek);
            Add("SPACE$", 0x19E, Translate_Space);
            Add("OCT$", 0x19F, Translate_Oct);
            Add("HEX$", 0x1A0, Translate_Hex);
            Add("LPOS", 0x1A1, Translate_Lpos);
            Add("CINT", 0x1A2, Translate_Cint);
            Add("CSNG", 0x1A3, Translate_Csng);
            Add("CDBL", 0x1A4, Translate_Cdbl);
            Add("FIX", 0x1A5, Translate_Fix);
            Add("PEN", 0x1A6, Translate_Pen);
            Add("STICK", 0x1A7, Translate_Stick);
            Add("STRIG", 0x1A8, Translate_Strig);
            Add("EOF", 0x1A9, Translate_Eof);
            Add("LOC", 0x1AA, Translate_Loc);
            Add("LOF", 0x1AB, Translate_Lof);
            Add("NOISE", 0x1AC, Translate_Noise);
            Add("TERM", 0x1AD, Translate_Term);
            Add("SUB", 0x1AE, Translate_Sub);
            Add("FUNCTION", 0x1AF, Translate_Function);
            Add("TYPE", 0x1B2, Translate_Type);
            Add("AS", 0x1B3, Translate_As);
        }

        private void Translate_As(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Type(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Function(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Sub(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Term(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Noise(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Lof(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Loc(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Eof(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Strig(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Stick(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Pen(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Fix(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Cdbl(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Csng(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Cint(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Lpos(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Hex(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Oct(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Space(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Peek(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Chr(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Asc(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Val(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Str(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Len(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Pos(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Inp(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Fre(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Atn(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Tan(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Cos(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Exp(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Log(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Sin(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Rnd(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Sqr(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Abs(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Int(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Sgn(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Mid(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Right(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Left(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Unlock(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Lock(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Pcopy(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Calls(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Lcopy(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Palette(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Pmap(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Window(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_View(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Environ(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Shell(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Rmdir(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Mkdir(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Chdir(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Ioctl(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Erdev(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Timer(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Play(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Draw(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Circle(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Com(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Paint(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Time(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Date(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Chain(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Common(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Reset(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Get(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Put(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Kill(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Rset(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Lset(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Name(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_System(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Field(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Files(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Exterr(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Mkd(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Mks(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Mki(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Cvd(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Cvs(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Cvi(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Inkey(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Off(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Point(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Csrlin(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Varptr(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Instr(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Using(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Err(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Erl(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Spc(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Fn(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Usr(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Step(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Tab(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Then(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_To(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Locate(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Key(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Screen(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Preset(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Pset(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Beep(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Sound(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Bload(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Bsave(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Motor(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Cls(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Color(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Save(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Merge(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Load(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Close(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Open(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Randomize(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Option(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Write(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Call(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Wend(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_While(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Line(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Defdbl(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Defsng(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Defint(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Defstr(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Renum(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Auto(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Delete(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Resume(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Error(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Edit(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Erase(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Swap(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Troff(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Tron(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Else(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Llist(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Lprint(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Width(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Out(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Cont(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Poke(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Def(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Wait(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_On(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_New(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_List(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Clear(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Print(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Stop(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Rem(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Return(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Gosub(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Restore(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_If(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Run(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Goto(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Let(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Read(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Dim(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Input(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Data(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_Next(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_For(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }

        private void Translate_End(BasicProgram Program, ProgramLine SourceLine)
        {
            throw new NotImplementedException();
        }
    }
}

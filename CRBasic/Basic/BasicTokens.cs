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

        //public void Add(string Name, UInt16 Code)
        //{
        //    BasicToken t = new BasicToken(Name, Code);
        //    Add(t);
        //}
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
            AddTokens();
            AddOperators();
            AddOther();
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
            AddOperator("(", 13, 0x1b5, Eval_OpenParen);
            AddOperator(")", 13, 0x1b6, Eval_CloseParen);
            AddOperator("^", 12, 0x1b7, Eval_Exponent);
            AddOperator("*", 11, 0x1b8, Eval_Multiply);
            AddOperator("/", 11, 0x1b9, Eval_Divide);
            AddOperator("\\", 10, 0x1ba, Eval_DivideInt);
            AddOperator("MOD", 9, 0x15b, Eval_Mod);
            AddOperator("+", 8, 0x1bb, Eval_Add);
            AddOperator("-", 8, 0x1bc, Eval_Subtract);
            AddOperator("=", 7, 0x1bd, Eval_Equal);
            AddOperator("<", 7, 0x1be, Eval_LessThan);
            AddOperator(">", 7, 0x1bf, Eval_GreaterThan);
            AddOperator("<>", 7, 0x1c0, Eval_NotEqual);
            AddOperator("<=", 7, 0x1c1, Eval_LessEqual);
            AddOperator("=>", 7, 0x1c2, Eval_GreaterEqual);
            AddOperator("NOT", 6, 0x14b, Eval_Not);
            AddOperator("AND", 5, 0x156, Eval_And);
            AddOperator("OR", 4, 0x157, Eval_Or);
            AddOperator("XOR", 3, 0x158, Eval_Xor);
            AddOperator("EQV", 2, 0x159, Eval_Eqv);
            AddOperator("IMP", 1, 0x15a, Eval_Imp);
        }

        private void Eval_OpenParen(BasicProgram Program, ProgramStep Step)
        {
            throw new NotImplementedException();
        }

        private void Eval_Imp(BasicProgram Program, ProgramStep Step)
        {
            throw new NotImplementedException();
        }

        private void Eval_Eqv(BasicProgram Program, ProgramStep Step)
        {
            throw new NotImplementedException();
        }

        private void Eval_Xor(BasicProgram Program, ProgramStep Step)
        {
            throw new NotImplementedException();
        }

        private void Eval_Or(BasicProgram Program, ProgramStep Step)
        {
            throw new NotImplementedException();
        }

        private void Eval_And(BasicProgram Program, ProgramStep Step)
        {
            throw new NotImplementedException();
        }

        private void Eval_Not(BasicProgram Program, ProgramStep Step)
        {
            throw new NotImplementedException();
        }

        private void Eval_GreaterEqual(BasicProgram Program, ProgramStep Step)
        {
            throw new NotImplementedException();
        }

        private void Eval_LessEqual(BasicProgram Program, ProgramStep Step)
        {
            throw new NotImplementedException();
        }

        private void Eval_NotEqual(BasicProgram Program, ProgramStep Step)
        {
            throw new NotImplementedException();
        }

        private void Eval_GreaterThan(BasicProgram Program, ProgramStep Step)
        {
            throw new NotImplementedException();
        }

        private void Eval_LessThan(BasicProgram Program, ProgramStep Step)
        {
            throw new NotImplementedException();
        }

        private void Eval_Equal(BasicProgram Program, ProgramStep Step)
        {
            throw new NotImplementedException();
        }

        private void Eval_Subtract(BasicProgram Program, ProgramStep Step)
        {
            throw new NotImplementedException();
        }

        private void Eval_Add(BasicProgram Program, ProgramStep Step)
        {
            BasicValue l = Step.LValue;
            if (l.DataType == DataTypes.Variable)
                l = Program.Variables[l.ToString()];

            BasicSymbol result = new BasicSymbol();
            result.DataType = l.DataType;

            switch (l.DataType)
            {
                case DataTypes.String:
                    result.Value = l.ToString() + Step.RValue.ToString();
                    result.DataType = DataTypes.String;
                    break;
                case DataTypes.Integer:
                    result.Value = (int)l.Value + (int)Step.RValue.Value;
                    break;
                case DataTypes.Single:
                    result.Value = (Single)l.Value + (Single)Step.RValue.Value;
                    break;
                case DataTypes.Double:
                    result.Value = (Double)l.Value + (Double)Step.RValue.Value;
                    break;
                default:
                    throw new BasicException("Syntax Error", Step.LineNumber, Step.Pos, "Could not execute " + l.ToString() + " " + Step.Operation.ToString() + " " + Step.RValue.ToString());
            }
            Step.Result = result;
        }

        private void Eval_Mod(BasicProgram Program, ProgramStep Step)
        {
            throw new NotImplementedException();
        }

        private void Eval_DivideInt(BasicProgram Program, ProgramStep Step)
        {
            throw new NotImplementedException();
        }

        private void Eval_Divide(BasicProgram Program, ProgramStep Step)
        {
            throw new NotImplementedException();
        }

        private void Eval_Multiply(BasicProgram Program, ProgramStep Step)
        {
            BasicValue l = Step.LValue;
            BasicValue r = Step.RValue;
            if (l.DataType == DataTypes.Variable)
                l = Program.Variables[l.ToString()];

            BasicSymbol result = new BasicSymbol();
            result.DataType = l.DataType;

            switch (l.DataType)
            {
                case DataTypes.String:
                    StringBuilder s = new StringBuilder();
                    for (int i = 0; i < r.IntVal; i++)
                        s.Append(l.ToString());
                    result.Value = s.ToString();
                    result.DataType = DataTypes.String;
                    break;
                case DataTypes.Integer:
                    result.Value = (int)l.Value * (int)r.Value;
                    break;
                case DataTypes.Single:
                    result.Value = (Single)l.Value * (Single)r.Value;
                    break;
                case DataTypes.Double:
                    result.Value = (Double)l.Value * (Double)r.Value;
                    break;
                default:
                    throw new BasicException("Invalid Type", Step.LineNumber, Step.Pos, "Could not execute " + l.ToString() + " " + Step.Operation.ToString() + " " + Step.RValue.ToString());
            }
            Step.Result = result;
        }

        private void Eval_Exponent(BasicProgram Program, ProgramStep Step)
        {
            throw new NotImplementedException();
        }

        private void Eval_CloseParen(BasicProgram Program, ProgramStep Step)
        {
            throw new NotImplementedException();
        }

        void AddOperator(string Name, int Precedence, ushort Code, BasicToken.CommandDelegate EvalMethod)
        {
            char c = Name.ToUpper()[0];
            if (c < 'A' || c > 'Z')
                OperatorChars.Add(c);
            BasicOperator op = new BasicOperator(Name, Precedence, Code, EvalMethod);
            Operators.Add(Name, op);
            Add(Name, Code, EvalMethod);
        }

        private void AddTokens()
        {
            Add("END", 0x100, Exec_End);
            Add("FOR", 0x101, Exec_For);
            Add("NEXT", 0x102, Exec_Next);
            Add("DATA", 0x103, Exec_Data);
            Add("INPUT", 0x104, Exec_Input);
            Add("DIM", 0x105, Exec_Dim);
            Add("READ", 0x106, Exec_Read);
            Add("LET", 0x107, Exec_Let);
            Add("GOTO", 0x108, Exec_Goto);
            Add("RUN", 0x109, Exec_Run);
            Add("IF", 0x10A, Exec_If);
            Add("RESTORE", 0x10B, Exec_Restore);
            Add("GOSUB", 0x10C, Exec_Gosub);
            Add("RETURN", 0x10D, Exec_Return);
            Add("REM", 0x10E, Exec_Rem);
            Add("STOP", 0x10F, Exec_Stop);
            Add("PRINT", 0x110, Exec_Print);
            Add("CLEAR", 0x111, Exec_Clear);
            Add("LIST", 0x112, Exec_List);
            Add("NEW", 0x113, Exec_New);
            Add("ON", 0x114, Exec_On);
            Add("WAIT", 0x115, Exec_Wait);
            Add("DEF", 0x116, Exec_Def);
            Add("POKE", 0x117, Exec_Poke);
            Add("CONT", 0x118, Exec_Cont);
            Add("OUT", 0x119, Exec_Out);
            Add("LPRINT", 0x11A, Exec_Lprint);
            Add("LLIST", 0x11B, Exec_Llist);
            Add("WIDTH", 0x11C, Exec_Width);
            Add("ELSE", 0x11D, Exec_Else);
            Add("TRON", 0x11E, Exec_Tron);
            Add("TROFF", 0x11F, Exec_Troff);
            Add("SWAP", 0x120, Exec_Swap);
            Add("ERASE", 0x121, Exec_Erase);
            Add("EDIT", 0x122, Exec_Edit);
            Add("ERROR", 0x123, Exec_Error);
            Add("RESUME", 0x124, Exec_Resume);
            Add("DELETE", 0x125, Exec_Delete);
            Add("AUTO", 0x126, Exec_Auto);
            Add("RENUM", 0x127, Exec_Renum);
            Add("DEFSTR", 0x128, Exec_Defstr);
            Add("DEFINT", 0x129, Exec_Defint);
            Add("DEFSNG", 0x12A, Exec_Defsng);
            Add("DEFDBL", 0x12B, Exec_Defdbl);
            Add("LINE", 0x12C, Exec_Line);
            Add("WHILE", 0x12D, Exec_While);
            Add("WEND", 0x12E, Exec_Wend);
            Add("CALL", 0x12F, Exec_Call);
            Add("WRITE", 0x130, Exec_Write);
            Add("OPTION", 0x131, Exec_Option);
            Add("RANDOMIZE", 0x132, Exec_Randomize);
            Add("OPEN", 0x133, Exec_Open);
            Add("CLOSE", 0x134, Exec_Close);
            Add("LOAD", 0x135, Exec_Load);
            Add("MERGE", 0x136, Exec_Merge);
            Add("SAVE", 0x137, Exec_Save);
            Add("COLOR", 0x138, Exec_Color);
            Add("CLS", 0x139, Exec_Cls);
            Add("MOTOR", 0x13A, Exec_Motor);
            Add("BSAVE", 0x13B, Exec_Bsave);
            Add("BLOAD", 0x13C, Exec_Bload);
            Add("SOUND", 0x13D, Exec_Sound);
            Add("BEEP", 0x13E, Exec_Beep);
            Add("PSET", 0x13F, Exec_Pset);
            Add("PRESET", 0x140, Exec_Preset);
            Add("SCREEN", 0x141, Exec_Screen);
            Add("KEY", 0x142, Exec_Key);
            Add("LOCATE", 0x143, Exec_Locate);
            Add("TO", 0x144, Exec_To);
            Add("THEN", 0x145, Exec_Then);
            Add("TAB(", 0x146, Exec_Tab);
            Add("STEP", 0x147, Exec_Step);
            Add("USR", 0x148, Exec_Usr);
            Add("FN", 0x149, Exec_Fn);
            Add("SPC(", 0x14A, Exec_Spc);
            Add("ERL", 0x14C, Exec_Erl);
            Add("ERR", 0x14D, Exec_Err);
            Add("USING", 0x14F, Exec_Using);
            Add("INSTR", 0x150, Exec_Instr);
            Add("VARPTR", 0x151, Exec_Varptr);
            Add("CSRLIN", 0x152, Exec_Csrlin);
            Add("POINT", 0x153, Exec_Point);
            Add("OFF", 0x154, Exec_Off);
            Add("INKEY$", 0x155, Exec_Inkey);
            Add("CVI", 0x15C, Exec_Cvi);
            Add("CVS", 0x15D, Exec_Cvs);
            Add("CVD", 0x15E, Exec_Cvd);
            Add("MKI$", 0x15F, Exec_Mki);
            Add("MKS$", 0x160, Exec_Mks);
            Add("MKD$", 0x161, Exec_Mkd);
            Add("EXTERR", 0x162, Exec_Exterr);
            Add("FILES", 0x163, Exec_Files);
            Add("FIELD", 0x164, Exec_Field);
            Add("SYSTEM", 0x165, Exec_System);
            Add("NAME", 0x166, Exec_Name);
            Add("LSET", 0x167, Exec_Lset);
            Add("RSET", 0x168, Exec_Rset);
            Add("KILL", 0x169, Exec_Kill);
            Add("PUT", 0x16A, Exec_Put);
            Add("GET", 0x16B, Exec_Get);
            Add("RESET", 0x16C, Exec_Reset);
            Add("COMMON", 0x16D, Exec_Common);
            Add("CHAIN", 0x16E, Exec_Chain);
            Add("DAT$", 0x16F, Exec_Date);
            Add("TIME$", 0x170, Exec_Time);
            Add("PAINT", 0x171, Exec_Paint);
            Add("COM", 0x172, Exec_Com);
            Add("CIRCLE", 0x173, Exec_Circle);
            Add("DRAW", 0x174, Exec_Draw);
            Add("PLAY", 0x175, Exec_Play);
            Add("TIMER", 0x176, Exec_Timer);
            Add("ERDEV", 0x177, Exec_Erdev);
            Add("IOCTL", 0x178, Exec_Ioctl);
            Add("CHDIR", 0x179, Exec_Chdir);
            Add("MKDIR", 0x17A, Exec_Mkdir);
            Add("RMDIR", 0x17B, Exec_Rmdir);
            Add("SHELL", 0x17C, Exec_Shell);
            Add("ENVIRON", 0x17D, Exec_Environ);
            Add("VIEW", 0x17E, Exec_View);
            Add("WINDOW", 0x17F, Exec_Window);
            Add("PMAP", 0x180, Exec_Pmap);
            Add("PALETTE", 0x181, Exec_Palette);
            Add("LCOPY", 0x182, Exec_Lcopy);
            Add("CALLS", 0x183, Exec_Calls);
            Add("PCOPY", 0x184, Exec_Pcopy);
            Add("LOCK", 0x185, Exec_Lock);
            Add("UNLOCK", 0x186, Exec_Unlock);
            Add("LEFT$", 0x187, Exec_Left);
            Add("RIGHT$", 0x188, Exec_Right);
            Add("MID$", 0x189, Exec_Mid);
            Add("SGN", 0x18A, Exec_Sgn);
            Add("INT", 0x18B, Exec_Int);
            Add("ABS", 0x18C, Exec_Abs);
            Add("SQR", 0x18D, Exec_Sqr);
            Add("RND", 0x18E, Exec_Rnd);
            Add("SIN", 0x18F, Exec_Sin);
            Add("LOG", 0x190, Exec_Log);
            Add("EXP", 0x191, Exec_Exp);
            Add("COS", 0x192, Exec_Cos);
            Add("TAN", 0x193, Exec_Tan);
            Add("ATN", 0x194, Exec_Atn);
            Add("FRE", 0x195, Exec_Fre);
            Add("INP", 0x196, Exec_Inp);
            Add("POS", 0x197, Exec_Pos);
            Add("LEN", 0x198, Exec_Len);
            Add("STR$", 0x199, Exec_Str);
            Add("VAL", 0x19A, Exec_Val);
            Add("ASC", 0x19B, Exec_Asc);
            Add("CHR$", 0x19C, Exec_Chr);
            Add("PEEK", 0x19D, Exec_Peek);
            Add("SPACE$", 0x19E, Exec_Space);
            Add("OCT$", 0x19F, Exec_Oct);
            Add("HEX$", 0x1A0, Exec_Hex);
            Add("LPOS", 0x1A1, Exec_Lpos);
            Add("CINT", 0x1A2, Exec_Cint);
            Add("CSNG", 0x1A3, Exec_Csng);
            Add("CDBL", 0x1A4, Exec_Cdbl);
            Add("FIX", 0x1A5, Exec_Fix);
            Add("PEN", 0x1A6, Exec_Pen);
            Add("STICK", 0x1A7, Exec_Stick);
            Add("STRIG", 0x1A8, Exec_Strig);
            Add("EOF", 0x1A9, Exec_Eof);
            Add("LOC", 0x1AA, Exec_Loc);
            Add("LOF", 0x1AB, Exec_Lof);
            Add("NOISE", 0x1AC, Exec_Noise);
            Add("TERM", 0x1AD, Exec_Term);
            Add("SUB", 0x1AE, Exec_Sub);
            Add("FUNCTION", 0x1AF, Exec_Function);
            Add("TYPE", 0x1B2, Exec_Type);
            Add("AS", 0x1B3, Exec_As);
        }

        // Command implementations
        public void Exec_End(BasicProgram Program, ProgramStep Step) { }
        public void Exec_For(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Next(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Data(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Input(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Dim(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Read(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Let(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Goto(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Run(BasicProgram Program, ProgramStep Step)
        {
            Program.Run();
        }
        public void Exec_If(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Restore(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Gosub(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Return(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Rem(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Stop(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Print(BasicProgram Program, ProgramStep Step)
        {
            Program.Print(Step);
        }
        public void Exec_Clear(BasicProgram Program, ProgramStep Step) { }
        public void Exec_List(BasicProgram Program, ProgramStep Step)
        {
            Program.List(Step);
        }
        public void Exec_New(BasicProgram Program, ProgramStep Step) { }
        public void Exec_On(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Wait(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Def(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Poke(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Cont(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Out(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Lprint(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Llist(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Width(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Else(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Tron(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Troff(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Swap(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Erase(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Edit(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Error(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Resume(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Delete(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Auto(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Renum(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Defstr(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Defint(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Defsng(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Defdbl(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Line(BasicProgram Program, ProgramStep Step) { }
        public void Exec_While(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Wend(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Call(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Write(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Option(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Randomize(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Open(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Close(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Load(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Merge(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Save(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Color(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Cls(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Motor(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Bsave(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Bload(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Sound(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Beep(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Pset(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Preset(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Screen(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Key(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Locate(BasicProgram Program, ProgramStep Step) { }
        public void Exec_To(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Then(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Tab(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Step(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Usr(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Fn(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Spc(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Not(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Erl(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Err(BasicProgram Program, ProgramStep Step) { }
        public void Exec_String(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Using(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Instr(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Apostrophe(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Varptr(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Csrlin(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Point(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Off(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Inkey(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Greater(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Equal(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Less(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Plus(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Minus(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Multiply(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Divide(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Power(BasicProgram Program, ProgramStep Step) { }
        public void Exec_And(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Or(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Xor(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Eqv(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Imp(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Mod(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Backslash(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Cvi(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Cvs(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Cvd(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Mki(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Mks(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Mkd(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Exterr(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Files(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Field(BasicProgram Program, ProgramStep Step) { }
        public void Exec_System(BasicProgram Program, ProgramStep Step)
        {
            Program.Display.ParentForm.Close();
        }
        public void Exec_Name(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Lset(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Rset(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Kill(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Put(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Get(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Reset(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Common(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Chain(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Date(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Time(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Paint(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Com(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Circle(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Draw(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Play(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Timer(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Erdev(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Ioctl(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Chdir(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Mkdir(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Rmdir(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Shell(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Environ(BasicProgram Program, ProgramStep Step) { }
        public void Exec_View(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Window(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Pmap(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Palette(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Lcopy(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Calls(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Pcopy(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Lock(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Unlock(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Left(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Right(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Mid(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Sgn(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Int(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Abs(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Sqr(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Rnd(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Sin(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Log(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Exp(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Cos(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Tan(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Atn(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Fre(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Inp(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Pos(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Len(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Str(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Val(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Asc(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Chr(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Peek(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Space(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Oct(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Hex(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Lpos(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Cint(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Csng(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Cdbl(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Fix(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Pen(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Stick(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Strig(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Eof(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Loc(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Lof(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Sub(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Function(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Label(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Noise(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Term(BasicProgram Program, ProgramStep Step) { }
        public void Exec_Type(BasicProgram Program, ProgramStep Step) { }
        public void Exec_As(BasicProgram Program, ProgramStep Step) { }
    }
}

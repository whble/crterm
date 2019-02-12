using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRBasic.PyBasic
{
    /// <summary>
    /// Tokens represent all valid commands and symbols in a BASIC program. Each token is a 16-bit number that
    /// represents an ASCII character or a single BASIC keyword or expression. 
    /// </summary>
    public class BasicTokens
    {
        public static SortedList<string, BasicToken> Commands = new SortedList<string, BasicToken>();
        public static SortedList<UInt16, BasicToken> Tokens = new SortedList<ushort, BasicToken>();

        /// <summary>
        /// Mathematical and comparison opeators (ie: +,-,<,= etc)
        /// </summary>
        public static SortedSet<string> Operators = new SortedSet<string>();
        /// <summary>
        /// Variable type glyphs: String$, Int%, Single!, Double#
        /// </summary>
        public static SortedSet<string> Glyphs = new SortedSet<string>();
        /// <summary>
        /// Comma 
        /// </summary>
        public static SortedSet<string> ListSeperators = new SortedSet<string>();
        /// <summary>
        /// Comma and semicolon
        /// </summary>
        public static SortedSet<string> PrintSeperators = new SortedSet<string>();

        private int Pos = 0;

        public void Add(string Name, UInt16 Code)
        {
            BasicToken t = new BasicToken(Name, Code);
            Commands.Add(Name, t);
            Tokens.Add(Code, t);
        }

        public void Add(string Name, UInt16 Code, BasicToken.TranslateDelegate Command)
        {
            BasicToken t = new BasicToken(Name, Code, Command);
            Commands.Add(Name, t);
            Tokens.Add(Code, t);
        }

        public BasicTokens()
        {
            AddTokens();
            AddOther();
        }

        private void AddOther()
        {
            Commands.Add("?", Commands["PRINT"]);

            Operators.Add(">");
            Operators.Add("=");
            Operators.Add("<");
            Operators.Add("+");
            Operators.Add("-");
            Operators.Add("*");
            Operators.Add("/");
            Operators.Add("\\");
            Operators.Add("^");
            Operators.Add("MOD");
            Operators.Add("NOT");
            Operators.Add("AND");
            Operators.Add("OR");
            Operators.Add("XOR");
            Operators.Add("EQV");
            Operators.Add("IMP");

            PrintSeperators.Add(",");
            PrintSeperators.Add(";");

            ListSeperators.Add(",");
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
            Add("PLIST", 0x8112, Exec_PList);
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
            Add("TAB", 0x146, Exec_Tab);
            Add("STEP", 0x147, Exec_Step);
            Add("USR", 0x148, Exec_Usr);
            Add("FN", 0x149, Exec_Fn);
            Add("SPC", 0x14A, Exec_Spc);
            Add("NOT", 0x14B, Exec_Not);
            Add("ERL", 0x14C, Exec_Erl);
            Add("ERR", 0x14D, Exec_Err);
            Add("STRING", 0x14E, Exec_String);
            Add("USING", 0x14F, Exec_Using);
            Add("INSTR", 0x150, Exec_Instr);
            Add("Apostrophe", 0x151, Exec_Apostrophe);
            Add("VARPTR", 0x152, Exec_Varptr);
            Add("CSRLIN", 0x153, Exec_Csrlin);
            Add("POINT", 0x154, Exec_Point);
            Add("OFF", 0x155, Exec_Off);
            Add("INKEY", 0x156, Exec_Inkey);
            Add("Greater", 0x157, Exec_Greater);
            Add("Equal", 0x158, Exec_Equal);
            Add("Less", 0x159, Exec_Less);
            Add("Plus", 0x15A, Exec_Plus);
            Add("Minus", 0x15B, Exec_Minus);
            Add("Multiply", 0x15C, Exec_Multiply);
            Add("Divide", 0x15D, Exec_Divide);
            Add("Power", 0x15E, Exec_Power);
            Add("AND", 0x15F, Exec_And);
            Add("OR", 0x160, Exec_Or);
            Add("XOR", 0x161, Exec_Xor);
            Add("EQV", 0x162, Exec_Eqv);
            Add("IMP", 0x163, Exec_Imp);
            Add("MOD", 0x164, Exec_Mod);
            Add("Backslash", 0x165, Exec_Backslash);
            Add("CVI", 0x166, Exec_Cvi);
            Add("CVS", 0x167, Exec_Cvs);
            Add("CVD", 0x168, Exec_Cvd);
            Add("MKI", 0x169, Exec_Mki);
            Add("MKS", 0x16A, Exec_Mks);
            Add("MKD", 0x16B, Exec_Mkd);
            Add("EXTERR", 0x16C, Exec_Exterr);
            Add("FILES", 0x16D, Exec_Files);
            Add("FIELD", 0x16E, Exec_Field);
            Add("SYSTEM", 0x16F, Exec_System);
            Add("NAME", 0x170, Exec_Name);
            Add("LSET", 0x171, Exec_Lset);
            Add("RSET", 0x172, Exec_Rset);
            Add("KILL", 0x173, Exec_Kill);
            Add("PUT", 0x174, Exec_Put);
            Add("GET", 0x175, Exec_Get);
            Add("RESET", 0x176, Exec_Reset);
            Add("COMMON", 0x177, Exec_Common);
            Add("CHAIN", 0x178, Exec_Chain);
            Add("DATE", 0x179, Exec_Date);
            Add("TIME", 0x17A, Exec_Time);
            Add("PAINT", 0x17B, Exec_Paint);
            Add("COM", 0x17C, Exec_Com);
            Add("CIRCLE", 0x17D, Exec_Circle);
            Add("DRAW", 0x17E, Exec_Draw);
            Add("PLAY", 0x17F, Exec_Play);
            Add("TIMER", 0x180, Exec_Timer);
            Add("ERDEV", 0x181, Exec_Erdev);
            Add("IOCTL", 0x182, Exec_Ioctl);
            Add("CHDIR", 0x183, Exec_Chdir);
            Add("MKDIR", 0x184, Exec_Mkdir);
            Add("RMDIR", 0x185, Exec_Rmdir);
            Add("SHELL", 0x186, Exec_Shell);
            Add("ENVIRON", 0x187, Exec_Environ);
            Add("VIEW", 0x188, Exec_View);
            Add("WINDOW", 0x189, Exec_Window);
            Add("PMAP", 0x18A, Exec_Pmap);
            Add("PALETTE", 0x18B, Exec_Palette);
            Add("LCOPY", 0x18C, Exec_Lcopy);
            Add("CALLS", 0x18D, Exec_Calls);
            Add("PCOPY", 0x18E, Exec_Pcopy);
            Add("LOCK", 0x18F, Exec_Lock);
            Add("UNLOCK", 0x190, Exec_Unlock);
            Add("LEFT", 0x191, Exec_Left);
            Add("RIGHT", 0x192, Exec_Right);
            Add("MID", 0x193, Exec_Mid);
            Add("SGN", 0x194, Exec_Sgn);
            Add("INT", 0x195, Exec_Int);
            Add("ABS", 0x196, Exec_Abs);
            Add("SQR", 0x197, Exec_Sqr);
            Add("RND", 0x198, Exec_Rnd);
            Add("SIN", 0x199, Exec_Sin);
            Add("LOG", 0x19A, Exec_Log);
            Add("EXP", 0x19B, Exec_Exp);
            Add("COS", 0x19C, Exec_Cos);
            Add("TAN", 0x19D, Exec_Tan);
            Add("ATN", 0x19E, Exec_Atn);
            Add("FRE", 0x19F, Exec_Fre);
            Add("INP", 0x1A0, Exec_Inp);
            Add("POS", 0x1A1, Exec_Pos);
            Add("LEN", 0x1A2, Exec_Len);
            Add("STR", 0x1A3, Exec_Str);
            Add("VAL", 0x1A4, Exec_Val);
            Add("ASC", 0x1A5, Exec_Asc);
            Add("CHR", 0x1A6, Exec_Chr);
            Add("PEEK", 0x1A7, Exec_Peek);
            Add("SPACE", 0x1A8, Exec_Space);
            Add("OCT", 0x1A9, Exec_Oct);
            Add("HEX", 0x1AA, Exec_Hex);
            Add("LPOS", 0x1AB, Exec_Lpos);
            Add("CINT", 0x1AC, Exec_Cint);
            Add("CSNG", 0x1AD, Exec_Csng);
            Add("CDBL", 0x1AE, Exec_Cdbl);
            Add("FIX", 0x1AF, Exec_Fix);
            Add("PEN", 0x1B0, Exec_Pen);
            Add("STICK", 0x1B1, Exec_Stick);
            Add("STRIG", 0x1B2, Exec_Strig);
            Add("EOF", 0x1B3, Exec_Eof);
            Add("LOC", 0x1B4, Exec_Loc);
            Add("LOF", 0x1B5, Exec_Lof);
            Add("SUB", 0x1B6, Exec_Sub);
            Add("FUNCTION", 0x1B7, Exec_Function);
            Add("LABEL", 0x1B8, Exec_Label);
        }

        // Command implementations
        public void Exec_End(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_For(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Next(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Data(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Input(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Dim(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Read(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Let(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Goto(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Run(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex)
        {
            PythonString.Append("$RUN");
        }
        public void Exec_If(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Restore(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Gosub(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Return(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Rem(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Stop(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Print(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex)
        {
            PythonString.Append("print ");
            StartIndex += 1;
            Arguments.AppendPrintParams(PythonString, Token, Arguments, ref StartIndex);
        }
        public void Exec_Clear(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_List(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex)
        {
            PythonString.Append("$LIST");
        }
        public void Exec_PList(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex)
        {
            PythonString.Append("$PLIST");
        }
        public void Exec_New(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_On(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Wait(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Def(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Poke(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Cont(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Out(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Lprint(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Llist(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Width(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Else(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Tron(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Troff(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Swap(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Erase(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Edit(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Error(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Resume(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Delete(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Auto(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Renum(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Defstr(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Defint(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Defsng(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Defdbl(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Line(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_While(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Wend(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Call(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Write(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Option(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Randomize(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Open(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Close(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Load(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Merge(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Save(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Color(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Cls(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Motor(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Bsave(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Bload(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Sound(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Beep(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Pset(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Preset(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Screen(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Key(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Locate(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_To(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Then(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Tab(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Step(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Usr(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Fn(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Spc(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Not(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Erl(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Err(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_String(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Using(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Instr(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Apostrophe(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Varptr(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Csrlin(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Point(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Off(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Inkey(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Greater(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Equal(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Less(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Plus(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Minus(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Multiply(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Divide(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Power(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_And(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Or(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Xor(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Eqv(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Imp(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Mod(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Backslash(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Cvi(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Cvs(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Cvd(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Mki(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Mks(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Mkd(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Exterr(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Files(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Field(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_System(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Name(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Lset(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Rset(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Kill(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Put(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Get(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Reset(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Common(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Chain(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Date(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Time(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Paint(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Com(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Circle(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Draw(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Play(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Timer(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Erdev(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Ioctl(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Chdir(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Mkdir(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Rmdir(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Shell(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Environ(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_View(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Window(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Pmap(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Palette(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Lcopy(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Calls(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Pcopy(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Lock(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Unlock(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Left(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Right(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Mid(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Sgn(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Int(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Abs(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Sqr(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Rnd(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Sin(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Log(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Exp(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Cos(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Tan(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Atn(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Fre(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Inp(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Pos(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Len(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Str(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Val(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Asc(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Chr(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Peek(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Space(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Oct(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Hex(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Lpos(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Cint(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Csng(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Cdbl(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Fix(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Pen(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Stick(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Strig(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Eof(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Loc(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Lof(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Sub(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Function(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
        public void Exec_Label(StringBuilder PythonString, BasicToken Token, BasicLine Arguments, ref int StartIndex) { }
    }
}

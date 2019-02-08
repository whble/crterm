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

        public static SortedSet<string> Operators = new SortedSet<string>();
        public static SortedSet<string> ListSeperators = new SortedSet<string>();
        public static SortedSet<string> PrintSeperators = new SortedSet<string>();

        private int Pos = 0;

        public void Add(string Name, UInt16 Code)
        {
            BasicToken t = new BasicToken(Name, Code);
            Commands.Add(Name, t);
            Tokens.Add(Code, t);
        }

        public void Add(string Name, UInt16 Code, BasicToken.CommandDelegate Command)
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
        public void Exec_End(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_For(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Next(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Data(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Input(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Dim(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Read(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Let(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Goto(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Run(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_If(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Restore(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Gosub(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Return(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Rem(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Stop(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Print(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Clear(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_List(BasicProgram Program, BasicVariable Result, ArgumentList Arguments)
        {
            Program.List();
        }
        public void Exec_New(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_On(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Wait(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Def(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Poke(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Cont(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Out(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Lprint(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Llist(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Width(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Else(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Tron(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Troff(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Swap(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Erase(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Edit(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Error(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Resume(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Delete(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Auto(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Renum(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Defstr(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Defint(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Defsng(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Defdbl(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Line(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_While(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Wend(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Call(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Write(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Option(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Randomize(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Open(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Close(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Load(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Merge(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Save(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Color(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Cls(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Motor(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Bsave(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Bload(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Sound(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Beep(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Pset(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Preset(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Screen(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Key(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Locate(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_To(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Then(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Tab(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Step(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Usr(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Fn(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Spc(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Not(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Erl(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Err(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_String(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Using(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Instr(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Apostrophe(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Varptr(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Csrlin(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Point(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Off(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Inkey(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Greater(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Equal(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Less(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Plus(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Minus(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Multiply(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Divide(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Power(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_And(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Or(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Xor(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Eqv(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Imp(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Mod(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Backslash(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Cvi(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Cvs(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Cvd(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Mki(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Mks(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Mkd(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Exterr(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Files(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Field(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_System(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Name(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Lset(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Rset(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Kill(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Put(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Get(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Reset(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Common(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Chain(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Date(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Time(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Paint(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Com(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Circle(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Draw(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Play(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Timer(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Erdev(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Ioctl(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Chdir(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Mkdir(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Rmdir(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Shell(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Environ(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_View(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Window(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Pmap(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Palette(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Lcopy(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Calls(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Pcopy(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Lock(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Unlock(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Left(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Right(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Mid(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Sgn(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Int(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Abs(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Sqr(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Rnd(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Sin(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Log(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Exp(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Cos(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Tan(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Atn(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Fre(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Inp(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Pos(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Len(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Str(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Val(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Asc(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Chr(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Peek(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Space(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Oct(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Hex(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Lpos(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Cint(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Csng(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Cdbl(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Fix(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Pen(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Stick(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Strig(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Eof(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Loc(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Lof(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Sub(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Function(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
        public void Exec_Label(BasicProgram Program, BasicVariable Result, ArgumentList Arguments) { }
    }
}

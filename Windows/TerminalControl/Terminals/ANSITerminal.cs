using System;
using System.Collections.Generic;

namespace TerminalUI.Terminals
{
    public class ANSITerminal : BasicTerminal
    {
        private const char CONTROL_E = '\x05';
        private const char ESCAPE = '\x1B';
        private int inOperand = 0;
        private List<int> operands = new List<int>();
        private bool inCmd = false;
        private string cmdPrefix = "";
        private int savedPos;

        private SortedList<System.Windows.Forms.Keys, string> KeyCodes = new SortedList<System.Windows.Forms.Keys, string>
        {
            { System.Windows.Forms.Keys.Up, ESCAPE+"[A"},
            { System.Windows.Forms.Keys.Down, ESCAPE+"[B"},
            { System.Windows.Forms.Keys.Right, ESCAPE+"[C"},
            { System.Windows.Forms.Keys.Left, ESCAPE+"[D"},
            { System.Windows.Forms.Keys.Home, ESCAPE+"[1~"},
            { System.Windows.Forms.Keys.Insert, ESCAPE+"[2~"},
            { System.Windows.Forms.Keys.Delete, ESCAPE+"[3~"},
            { System.Windows.Forms.Keys.End, ESCAPE+"[4~"},
            { System.Windows.Forms.Keys.PageUp, ESCAPE+"[5~"},
            { System.Windows.Forms.Keys.PageDown, ESCAPE+"[6~"},
            { System.Windows.Forms.Keys.F1, ESCAPE+"[11~"},
            { System.Windows.Forms.Keys.F2, ESCAPE+"[12~"},
            { System.Windows.Forms.Keys.F3, ESCAPE+"[13~"},
            { System.Windows.Forms.Keys.F4, ESCAPE+"[14~"},
            { System.Windows.Forms.Keys.F5, ESCAPE+"[15~"},
            { System.Windows.Forms.Keys.F6, ESCAPE+"[16~"},
            { System.Windows.Forms.Keys.F7, ESCAPE+"[17~"},
            { System.Windows.Forms.Keys.F8, ESCAPE+"[18~"},
            { System.Windows.Forms.Keys.F9, ESCAPE+"[19~"},
            { System.Windows.Forms.Keys.F10, ESCAPE+"[20~"},
            { System.Windows.Forms.Keys.F11, ESCAPE+"[21~"},
            { System.Windows.Forms.Keys.F12, ESCAPE+"[22~"},
        };

        private SortedList<System.Windows.Forms.Keys, string> ShiftKeyCodes = new SortedList<System.Windows.Forms.Keys, string>
        {
            //{ System.Windows.Forms.Keys.F1, ESCAPE+"[23~"},
            //{ System.Windows.Forms.Keys.F2, ESCAPE+"[24~"},
            { System.Windows.Forms.Keys.F3, ESCAPE+"[25~"},
            { System.Windows.Forms.Keys.F4, ESCAPE+"[26~"},
            { System.Windows.Forms.Keys.F5, ESCAPE+"[28~"},
            { System.Windows.Forms.Keys.F6, ESCAPE+"[29~"},
            { System.Windows.Forms.Keys.F7, ESCAPE+"[31~"},
            { System.Windows.Forms.Keys.F8, ESCAPE+"[32~"},
            { System.Windows.Forms.Keys.F9, ESCAPE+"[33~"},
            { System.Windows.Forms.Keys.F10, ESCAPE+"[34~"},
            //{ System.Windows.Forms.Keys.F11, ESCAPE+"[21~"},
            //{ System.Windows.Forms.Keys.F12, ESCAPE+"[22~"},
        };

        public override string Name
        {
            get
            {
                return "ANSI";
            }
        }

        public override void SendKey(TerminalKeyEventArgs terminalKey)
        {
            base.SendKey(terminalKey);

            if (!terminalKey.Handled)
            {
                if (terminalKey.Modifier.HasFlag(System.Windows.Forms.Keys.Shift)
                   && ShiftKeyCodes.ContainsKey(terminalKey.KeyCode))
                    SendString(ShiftKeyCodes[terminalKey.KeyCode]);
                else
                if (KeyCodes.ContainsKey(terminalKey.KeyCode))
                    SendString(KeyCodes[terminalKey.KeyCode]);
            }
        }

        public override void ProcessReceivedCharacter(char c)
        {
            if (!inCmd)
            {
                switch (c)
                {
                    case CONTROL_E:
                        SendString("CRTerm");
                        break;
                    case '\x09':
                        int x = Display.CurrentColumn % 8;
                        x += 8 - x;
                        Display.CurrentColumn = x;
                        break;
                    case '\x0C':
                        Display.Clear();
                        Display.Locate(0, 0);
                        break;
                    case ESCAPE:
                        inCmd = true;
                        inOperand = 0;
                        operands.Clear();
                        operands.Add(0);
                        break;
                    default:
                        base.ProcessReceivedCharacter(c);
                        break;
                }
            }
            else
            {
                // append numerical digits to the command operand
                if (inCmd && c >= '0' && c <= '9')
                {
                    operands[inOperand] *= 10;
                    operands[inOperand] += (int)(c - '0');
                }
                else
                {
                    switch (c)
                    {
                        // start of ESC-[ command sequence (basically all ANSI commands)
                        case '[':
                            if (cmdPrefix == "")
                            {
                                inOperand = 0;
                                cmdPrefix = "[";
                            }
                            else
                                inCmd = false;
                            break;
                        // additional character for some obscure commands
                        case '?':
                            cmdPrefix = "[?";
                            break;

                        // separate operands
                        case ';':
                            inOperand += 1;
                            operands.Add(0);
                            break;

                        // cursor up
                        case 'A':
                            inCmd = false;
                            for (int i = 0; i < Math.Max(operands[0], 1); i++)
                            {
                                Display.CurrentRow -= 1;
                            }
                            break;

                        // cursor down
                        case 'B':
                            inCmd = false;
                            for (int i = 0; i < Math.Max(operands[0], 1); i++)
                            {
                                Display.CurrentRow += 1;
                            }
                            break;

                        // cursor right
                        case 'C':
                            inCmd = false;
                            for (int i = 0; i < Math.Max(operands[0], 1); i++)
                            {
                                Display.CurrentColumn += 1;
                            }
                            break;

                        // cursor left
                        case 'D':
                            inCmd = false;
                            for (int i = 0; i < Math.Max(operands[0], 1); i++)
                            {
                                Display.CurrentColumn -= 1;
                            }
                            break;

                        // what are you?
                        case 'c':
                            inCmd = false;
                            //Display.Clear();
                            //Display.Locate(0, 0);
                            SendString("ANSI");
                            break;

                        // set terminal attribute
                        // 25-show cursor
                        case 'h':
                            inCmd = false;
                            if (operands.Count > 0)
                            {
                                switch (operands[0])
                                {
                                    case 25:
                                        Display.TextCursor = TextCursorStyles.Underline;
                                        break;
                                }
                            }
                            break;

                        // reset terminal attribute
                        // 25-hide cursor
                        case 'l':
                            inCmd = false;
                            if (operands.Count > 0)
                            {
                                switch (operands[0])
                                {
                                    case 25:
                                        Display.TextCursor = TextCursorStyles.None;
                                        break;
                                }
                            }
                            break;

                        // set cursor location
                        // ^[[row;colH
                        case 'H':
                            inCmd = false;
                            if (operands.Count > 0)
                                Display.CurrentRow = Math.Max(operands[0] - 1, 0);
                            else
                                Display.CurrentRow = 0;

                            if (operands.Count > 1)
                                Display.CurrentColumn = Math.Max(operands[1] - 1, 0);
                            else
                                Display.CurrentColumn = 0;
                            break;

                        // Clear screen
                        // ^[[J from cursor to end of screen
                        // ^[[1J from start of screen to cursor
                        // ^[[2J clear screen
                        case 'J':
                            inCmd = false;
                            if (operands[0] == 1)
                                Display.ClearScreen(true, false);
                            else if (operands[0] == 2)
                                Display.Clear();
                            else
                                Display.ClearScreen(false, true);
                            break;

                        // Clear current line
                        // ^[[K from cursor to end of line
                        // ^[[1K from start of line to cursor
                        // ^[[2K clear screen
                        case 'K':
                            inCmd = false;
                            if (operands[0] == 1)
                                Display.ClearCurrentLine(true, false);
                            else if (operands[0] == 2)
                                Display.ClearCurrentLine(true, true);
                            else
                                Display.ClearCurrentLine(false, true);
                            break;

                        // attributes
                        // no attributes                   ESC[m
                        // no attributes                   ESC[0m
                        // select attribute bold           ESC[1m
                        // select attribute underline      ESC[4m
                        // select attribute blink          ESC[5m
                        // select attribute, reverse video ESC[7m
                        case 'm':
                            inCmd = false;
                            if (operands.Count == 0)
                                Display.CurrentAttribute = CharacterCell.AttributeCodes.Normal;
                            else
                            {
                                switch (operands[0])
                                {
                                    case 0:
                                        Display.CurrentAttribute = CharacterCell.AttributeCodes.Normal;
                                        break;
                                    case 1:
                                        Display.CurrentAttribute = CharacterCell.AttributeCodes.Bold;
                                        break;
                                    case 4:
                                        Display.CurrentAttribute = CharacterCell.AttributeCodes.Underline;
                                        break;
                                    case 5:
                                        Display.CurrentAttribute = CharacterCell.AttributeCodes.Blink;
                                        break;
                                    case 7:
                                        Display.CurrentAttribute = CharacterCell.AttributeCodes.Reverse;
                                        break;
                                    default:
                                        break;
                                }
                            }
                            break;

                        // device status report
                        // 6: cursor position
                        case 'n':
                            inCmd = false;
                            if (operands[0] == 6)
                                SendString(ESCAPE + "[" + (Display.CurrentRow + 1).ToString() + ";" + (Display.CurrentColumn + 1).ToString() + "R");
                            break;

                        // save cursor position
                        case 's':
                            inCmd = false;
                            savedPos = Display.CursorPos;
                            break;

                        // restore saved cursor position
                        case 'u':
                            inCmd = false;
                            Display.CursorPos = savedPos;
                            break;

                        // invalid or unimplimented escape sequence
                        default:
                            inCmd = false;
                            System.Diagnostics.Debug.Write("Escape character ignored: " + c + " (" + ((int)c).ToString() + ") Operands:");
                            for (int i = 0; i < operands.Count; i++)
                            {
                                if (i > 0)
                                    System.Diagnostics.Debug.Write(";");
                                System.Diagnostics.Debug.Write(operands[i].ToString());
                            }
                            System.Diagnostics.Debug.WriteLine("");
                            break;
                    }

                    if (!inCmd)
                        cmdPrefix = "";

                }
            }
        }
    }
}

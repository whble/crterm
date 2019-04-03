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
            if (terminalKey.Modifier.HasFlag(System.Windows.Forms.Keys.Shift)
                && ShiftKeyCodes.ContainsKey(terminalKey.KeyCode))
                SendString(ShiftKeyCodes[terminalKey.KeyCode]);
            else if (KeyCodes.ContainsKey(terminalKey.KeyCode))
                SendString(KeyCodes[terminalKey.KeyCode]);

            //switch (terminalKey.KeyCode)
            //{
            //    case System.Windows.Forms.Keys.Up:
            //        SendString(ESCAPE+"[A");
            //        break;
            //    case System.Windows.Forms.Keys.Down:
            //        SendString(ESCAPE+"[B");
            //        break;
            //    case System.Windows.Forms.Keys.Right:
            //        SendString(ESCAPE+"[C");
            //        break;
            //    case System.Windows.Forms.Keys.Left:
            //        SendString(ESCAPE+"[D");
            //        break;
            //}

            base.SendKey(terminalKey);
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
                if (inCmd && c >= '0' && c <= '9')
                {
                    operands[inOperand] *= 10;
                    operands[inOperand] += (int)(c - '0');
                }
                else
                {
                    switch (c)
                    {
                        case '[':
                            inOperand = 0;
                            break;
                        case ';':
                            inOperand += 1;
                            operands.Add(0);
                            break;
                        case 'A':
                            inCmd = false;
                            for (int i = 0; i < Math.Max(operands[0], 1); i++)
                            {
                                Display.CurrentRow -= 1;
                            }
                            break;
                        case 'B':
                            inCmd = false;
                            for (int i = 0; i < Math.Max(operands[0], 1); i++)
                            {
                                Display.CurrentRow += 1;
                            }
                            break;
                        case 'C':
                            inCmd = false;
                            for (int i = 0; i < Math.Max(operands[0], 1); i++)
                            {
                                Display.CurrentColumn += 1;
                            }
                            break;
                        case 'D':
                            inCmd = false;
                            for (int i = 0; i < Math.Max(operands[0], 1); i++)
                            {
                                Display.CurrentColumn -= 1;
                            }
                            break;
                        case 'c':
                            inCmd = false;
                            Display.Clear();
                            Display.Locate(0, 0);
                            break;
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
                        case 'J':
                            inCmd = false;
                            if (operands[0] == 1)
                                Display.ClearScreen(true, false);
                            else if (operands[0] == 2)
                                Display.Clear();
                            else
                                Display.ClearScreen(false, true);
                            break;
                        case 'K':
                            inCmd = false;
                            if (operands[0] == 1)
                                Display.ClearCurrentLine(true, false);
                            else if (operands[0] == 2)
                                Display.ClearCurrentLine(true, true);
                            else
                                Display.ClearCurrentLine(false, true);
                            break;
                        case 'n':
                            inCmd = false;
                            if (operands[0] == 6)
                                SendString(ESCAPE + "[" + (Display.CurrentRow + 1).ToString() + ";" + (Display.CurrentColumn + 1).ToString() + "R");
                            break;
                        case 's':
                            inCmd = false;
                            savedPos = Display.CursorPos;
                            break;
                        case 'u':
                            inCmd = false;
                            Display.CursorPos = savedPos;
                            break;
                        case 'm':
                            inCmd = false;
                            if (operands.Count > 0)
                            {
                                switch (operands[0])
                                {
                                    case 0:
                                        Display.CurrentAttribute = CharacterCell.Attributes.Normal;
                                        break;
                                    case 7:
                                        Display.CurrentAttribute = CharacterCell.Attributes.Reverse;
                                        break;
                                    default:
                                        break;
                                }
                            }
                            break;
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
                }
            }
        }
    }
}

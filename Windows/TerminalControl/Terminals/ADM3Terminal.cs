using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TerminalUI.Terminals
{
    public class ADM3Terminal : BasicTerminal
    {
        private const char CONTROL_E = '\x05';
        private const char ESCAPE = '\x1B';
        private int inOperand = 0;
        private char operation = (char)0;
        private List<int> operands = new List<int>();
        private bool inCmd = false;
        private int savedPos;

        private SortedList<System.Windows.Forms.Keys, string> KeyCodes = new SortedList<System.Windows.Forms.Keys, string>
        {
            { System.Windows.Forms.Keys.Up, "\x05"}, // ^E
            { System.Windows.Forms.Keys.Down, "\x18"}, // ^X
            { System.Windows.Forms.Keys.Left, "\x13"}, // ^S
            { System.Windows.Forms.Keys.Right, "\x4"}, // ^D
            { System.Windows.Forms.Keys.Home, "\x01"}, // ^A
            { System.Windows.Forms.Keys.End, "\x06"},  // ^F
            { System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Home, "\x1A"},  // ^Z Clear Screen
            { System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.End, "\x06"},  // ^T Delete to EOL
            { System.Windows.Forms.Keys.Back, "\x7F"},  // 127, Backspace & Delete
            { System.Windows.Forms.Keys.Delete, "\x07"},  // ^G Delete Character
            { System.Windows.Forms.Keys.Insert, "\x0V"},  // ^V Insert Character
        };

        private SortedList<System.Windows.Forms.Keys, string> ShiftKeyCodes = new SortedList<System.Windows.Forms.Keys, string>
        {
            //{ System.Windows.Forms.Keys.F1, ESCAPE+"[23~"},
            //{ System.Windows.Forms.Keys.F2, ESCAPE+"[24~"},
            //{ System.Windows.Forms.Keys.F3, ESCAPE+"[25~"},
            //{ System.Windows.Forms.Keys.F4, ESCAPE+"[26~"},
            //{ System.Windows.Forms.Keys.F5, ESCAPE+"[28~"},
            //{ System.Windows.Forms.Keys.F6, ESCAPE+"[29~"},
            //{ System.Windows.Forms.Keys.F7, ESCAPE+"[31~"},
            //{ System.Windows.Forms.Keys.F8, ESCAPE+"[32~"},
            //{ System.Windows.Forms.Keys.F9, ESCAPE+"[33~"},
            //{ System.Windows.Forms.Keys.F10, ESCAPE+"[34~"},
            //{ System.Windows.Forms.Keys.F11, ESCAPE+"[21~"},
            //{ System.Windows.Forms.Keys.F12, ESCAPE+"[22~"},
        };

        private SortedList<System.Windows.Forms.Keys, string> ControlKeyCodes = new SortedList<System.Windows.Forms.Keys, string>
        {
            //{ System.Windows.Forms.Keys.Up, "\x05"}, // ^E
            //{ System.Windows.Forms.Keys.Down, "\x18"}, // ^X
            //{ System.Windows.Forms.Keys.Left, "\x13"}, // ^S
            //{ System.Windows.Forms.Keys.Right, "\x4"}, // ^D
            //{ System.Windows.Forms.Keys.Home, "\x01"}, // ^A
            //{ System.Windows.Forms.Keys.End, "\x06"},  // ^F
            {System.Windows.Forms.Keys.Home, "\x1A"},  // ^Z Clear Screen
            {System.Windows.Forms.Keys.End, "\x06"},  // ^T Delete to EOL
            //{ System.Windows.Forms.Keys.Back, "\x7F"},  // 127, Backspace & Delete
            //{ System.Windows.Forms.Keys.Delete, "\x07"},  // ^G Delete Character
            //{ System.Windows.Forms.Keys.Insert, "\x0V"},  // ^V Insert Character
        };

        public override string Name
        {
            get
            {
                return "ADM3a (BBCBASIC)";
            }
        }

        public override void SendKey(TerminalKeyEventArgs terminalKey)
        {
            System.Diagnostics.Debug.WriteLine(terminalKey.KeyCode.ToString() + ", " + terminalKey.Modifier.ToString());

            base.SendKey(terminalKey);

            if (!terminalKey.Handled)
            {
                if (terminalKey.Modifier.HasFlag(System.Windows.Forms.Keys.Control)
                   && ControlKeyCodes.ContainsKey(terminalKey.KeyCode))
                    SendString(ShiftKeyCodes[terminalKey.KeyCode]);
                else if (terminalKey.Modifier.HasFlag(System.Windows.Forms.Keys.Shift)
                   && ShiftKeyCodes.ContainsKey(terminalKey.KeyCode))
                    SendString(ShiftKeyCodes[terminalKey.KeyCode]);
                else if (KeyCodes.ContainsKey(terminalKey.KeyCode))
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
                    case '\x1A':
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
                if (inCmd && c >= ' ' && c <= '~')
                {
                    operands[inOperand] = (int)c - 32;
                    inOperand++;
                    if (inOperand == 2)
                    {
                        Display.CurrentRow = operands[0];
                        Display.CurrentColumn = operands[1];
                    }
                }
                else
                {
                    switch (c)
                    {
                        case '=':
                            operation = c;
                            inOperand = 0;
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

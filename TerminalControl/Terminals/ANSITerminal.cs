using System;
using System.Collections.Generic;

namespace TerminalUI.Terminals
{
    public class ANSITerminal : BasicTerminal
    {
        private const char CONTROL_E = '\x05';
        private const char ESCAPE = '\x1B';
        int inOperand = 0;
        List<int> operands = new List<int>();
        bool inCmd = false;
        private int savedPos;

        public override string Name
        {
            get
            {
                return "ANSI";
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
                                Display.ClearCurrentLine(true,false);
                            else if (operands[0] == 2)
                                Display.ClearCurrentLine(true,true);
                            else
                                Display.ClearCurrentLine(false,true);
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
                        default:
                            inCmd = false;
                            break;

                    }
                }
            }
        }
    }
}

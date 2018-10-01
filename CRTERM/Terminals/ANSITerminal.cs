using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CRTerm.IO;

namespace CRTerm.Terminals
{
    class ANSITerminal : BasicTerminal
    {
        private const char CONTROL_E = '\x05';
        private const char ESCAPE = '\x1B';
        int inOperand = 0;
        List<int> operands = new List<int>();
        bool inCmd = false;

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
                        int x = FrameBuffer.X % 8;
                        x += 8 - x;
                        FrameBuffer.X = x;
                        break;
                    case '\x0C':
                        FrameBuffer.Clear();
                        FrameBuffer.Locate(0, 0);
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
                            for (int i = 0; i < Math.Max(operands[0],1); i++)
                            {
                                FrameBuffer.Y -= 1;
                            }
                            break;
                        case 'B':
                            inCmd = false;
                            for (int i = 0; i < Math.Max(operands[0], 1); i++)
                            {
                                FrameBuffer.Y += 1;
                            }
                            break;
                        case 'C':
                            for (int i = 0; i < Math.Max(operands[0], 1); i++)
                            {
                                FrameBuffer.X += 1;
                            }
                            inCmd = false;
                            break;
                        case 'D':
                            inCmd = false;
                            for (int i = 0; i < Math.Max(operands[0], 1); i++)
                            {
                                FrameBuffer.X -= 1;
                            }
                            break;
                        case 'c':
                            FrameBuffer.Clear();
                            FrameBuffer.Locate(0,0);
                            inCmd = false;
                            break;
                        case 'H':
                            inCmd = false;
                            if (operands.Count > 0)
                                FrameBuffer.Y = Math.Max(operands[0] - 1, 0);
                            else
                                FrameBuffer.Y = 0;

                            if (operands.Count > 1)
                                FrameBuffer.X = Math.Max(operands[1] - 1, 0);
                            else
                                FrameBuffer.X = 0;
                            break;
                        case 'J':
                            inCmd = false;
                            if (operands[0] == 2)
                                FrameBuffer.Clear();
                            break;
                        case 'n':
                            if (operands[0] == 6)
                                SendString(ESCAPE + "[" + (FrameBuffer.Y+1).ToString() + ";" + (FrameBuffer.X+1).ToString() + "R");
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

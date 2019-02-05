using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerminalUI
{
    public class CharacterCell
    {
        public enum ColorCodes
        {
            Black = 0x00,
            Blue = 0x01,
            Green = 0x02,
            Cyan = 0x03,
            Red = 0x04,
            Magenta = 0x05,
            Brown = 0x06,
            Gray = 0x07,
            DarkGray = 0x07,
            LightBlue = 0x09,
            Lightgreen = 0x0A,
            LightCyan = 0x0B,
            LightRed = 0x0C,
            LightMagenta = 0x0D,
            Yellow = 0x0E,
            White = 0x0F,
        }

        public enum Attributes
        {
            Normal=0,
            Underline = 0x01,
            Bold = 0x02,
            Italic = 0x04,
            Blink = 0x08
        }

        public ColorCodes TextColor = ColorCodes.Green;
        public ColorCodes BackColor = ColorCodes.Black;
        public Attributes Attribute = Attributes.Normal;
        public string Value = " ";
    }
}

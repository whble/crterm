using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalUI
{
    public class ScreenBuffer
    {
        public char[] CharacterData = null;
        public CharacterCell.ColorCodes[] TextColorData;
        public CharacterCell.ColorCodes[] BackColorData;
        public CharacterCell.AttributeCodes[] AttributeData;

        public int Length
        {
            get
            {
                if (CharacterData == null)
                    return 0;
                return CharacterData.Length;
            }
        }

        public ScreenBuffer()
        {

        }
        public ScreenBuffer(int Length)
        {
            CreateBuffer(Length);
        }

        public void CreateBuffer(int Length)
        {
            CharacterData = new char[Length];
            TextColorData = new CharacterCell.ColorCodes[Length];
            BackColorData = new CharacterCell.ColorCodes[Length];
            AttributeData = new CharacterCell.AttributeCodes[Length];
        }

        public void Save(
            char[] chars,
            CharacterCell.ColorCodes[] textColors,
            CharacterCell.ColorCodes[] backColors,
            CharacterCell.AttributeCodes[] Attributes)
        {
            if (CharacterData == null || chars.Length != CharacterData.Length)
                CreateBuffer(chars.Length);

            chars.CopyTo(CharacterData, 0);
            textColors.CopyTo(TextColorData, 0);
            backColors.CopyTo(BackColorData, 0);
            Attributes.CopyTo(AttributeData, 0);
        }

        public void Load(
            char[] chars,
            CharacterCell.ColorCodes[] textColors,
            CharacterCell.ColorCodes[] backColors,
            CharacterCell.AttributeCodes[] Attributes)
        {
            if (CharacterData == null)
                return;

            CharacterData.CopyTo(chars, 0);
            TextColorData.CopyTo(textColors, 0);
            BackColorData.CopyTo(backColors, 0);
            AttributeData.CopyTo(Attributes, 0);
        }
    }
}

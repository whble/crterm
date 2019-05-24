using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalUI
{
    public class TextDialog
    {
        ScreenBuffer buffer = new ScreenBuffer();

        public bool Visible = true;
        public DisplayControl Display = null;

        int _selectedIndex = 0;
        
        /// <summary>
        /// Screen position in character spaces
        /// </summary>
        public int Left, Top;
        /// <summary>
        /// Size in character spaces
        /// </summary>
        public int Width, Height;

        public string Title = "Menu";
        public List<string> MenuItems = new List<string>();
        public List<string> Shortcuts = new List<string>();

        public int SelectedIndex
        {
            get
            {
                return this._selectedIndex;
            }

            set
            {
                this._selectedIndex = value;
            }
        }

        public void Add(string v1, string v2)
        {
            MenuItems.Add(v1);
            Shortcuts.Add(v2);
        }

        public void Draw()
        {
            Display.ClearRectangle(Left, Top, Width, Height);
            Display.DrawRectangle(Left, Top, Width, Height);

            Display.Locate(Top, Left + Width / 2 - Title.Length / 2 - 2);
            Display.Print("[ ");
            Display.Print(Title);
            Display.Print(" ]");

            for (int i = 0; i < MenuItems.Count; i++)
            {
                Display.Locate(Top + 2 + i, Left + 2);
                if (i == SelectedIndex)
                    Display.CurrentAttribute = CharacterCell.AttributeCodes.Reverse;
                else
                    Display.CurrentAttribute = CharacterCell.AttributeCodes.Normal;
                Display.Print(MenuItems[i].PadRight(Width - 4));

                Display.CurrentColumn = Left + Width - 2 - Shortcuts[i].Length;
                Display.Print(Shortcuts[i]);
            }
        }

        public void Show()
        {
            Display.SaveScreen(buffer);
            Display.TextCursor = TextCursorStyles.None;

            this.Visible = true;
            Draw();
        }

        public void Hide()
        {
            Display.RestoreScreen(buffer);
            this.Visible = false;
            Display.TextCursor = TextCursorStyles.Underline;
            Display.Refresh();
        }
    }
}

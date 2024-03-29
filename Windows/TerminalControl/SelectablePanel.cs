﻿using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TerminalUI
{
    public class SelectablePanel : Panel
    {
        public SelectablePanel()
        {
            this.SetStyle(ControlStyles.Selectable, true);
            this.TabStop = true;
        }
        protected override bool IsInputKey(Keys keyData)
        {
            if (keyData == Keys.Up || keyData == Keys.Down) return true;
            if (keyData == Keys.Left || keyData == Keys.Right) return true;
            return base.IsInputKey(keyData);
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            KeyDown?.Invoke(this, e);
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            KeyPress?.Invoke(this, e);
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            KeyUp?.Invoke(this, e);
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            this.Focus();
            base.OnMouseEnter(e);
        }
        protected override void OnEnter(EventArgs e)
        {
            this.Invalidate();
            base.OnEnter(e);
        }
        protected override void OnLeave(EventArgs e)
        {
            this.Invalidate();
            base.OnLeave(e);
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            if (this.Focused)
            {
                var rc = this.ClientRectangle;
                rc.Inflate(-2, -2);
                ControlPaint.DrawFocusRectangle(pe.Graphics, rc);
            }
        }

        [Browsable(true)]
        public new event EventHandler<KeyEventArgs> KeyDown;
        [Browsable(true)]
        public new event EventHandler<KeyEventArgs> KeyUp;
        [Browsable(true)]
        public new event EventHandler<KeyPressEventArgs> KeyPress;
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CRTerm.Config
{
    public partial class ConfigItemControl : UserControl
    {
        public ConfigItemControl()
        {
            InitializeComponent();
        }

        private string _key;
        public string Key
        {
            get
            {
                return this._key;
            }

            set
            {
                this._key = value;
                this.KeyLabel.Text = value;
            }
        }

        private string _value;
        public string Value
        {
            get
            {
                return this._value;
            }

            set
            {
                this._value = value;
            }
        }

        public event EventHandler ValueChanged;
        /// <summary>
        /// Call to set new Value property and invoke appropriate callbacks
        /// </summary>
        /// <param name="Value"></param>
        protected virtual void OnValueChanged(string Value)
        {
            this.Value = Value;
            if (ValueChanged != null)
                ValueChanged(this, new EventArgs());
        }
    }
}

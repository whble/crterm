using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRTerm
{
    public class ParameterItem
    {
        /// <summary>
        /// Name of the parameter. Serialized with ToString and loaded with Load().
        /// Note that Name is immutable once set. 
        /// </summary>
        private string _name = "";
        public string Name
        {
            get { return _name; }
            protected set
            {
                if (_name == "")
                    _name = value;
            }
        }
        /// <summary>
        /// Value of parameter. Serialized with ToString() and loaded with Load()
        /// </summary>
        private string _value = "";
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        /// <summary>
        /// Valid values for this parameter. These values will be populated in the dropdown
        /// in the configuration panel. 
        /// </summary>
        public string[] AllowedValues = null;
        /// <summary>
        /// If true, only allow values from the AllowedValues list. 
        /// </summary>
        public bool Strict = false;
        /// <summary>
        /// How to display this parameter in the confuration panel.
        /// </summary>
        public enum ParameterType
        {
            Text,
            Combo,
            RadioButton,
            CheckYN
        }

        /// <summary>
        /// Create a new ParameterItem. This constructor is hidden to protect the immutability of the name,
        /// since changing the name would mess up the key in the ParameterList.
        /// </summary>
        protected ParameterItem()
        {
        }

        /// <summary>
        /// Create a new parameter. 
        /// </summary>
        /// <param name="Name">Name of this parameter. This cannot be changed later.</param>
        /// <param name="Value">Value of this parameter. This can be changed later.</param>
        public ParameterItem(string Name, string Value)
        {
            this._name = Name;
            this._value = Value;
        }

        /// <summary>
        /// Creates a new parameter from a name:value pair. 
        /// </summary>
        /// <param name="Data">Name and Value for this parameter in name:value form.</param>
        public ParameterItem(string Data)
        {
            string[] parts = Data.Split(':');
            if (parts.Length >= 1)
                this._name = parts[0];
            if (parts.Length >= 2)
                this._value = parts[1];
        }

        /// <summary>
        /// Serializes Name and Value to allow saving to disk. Only Name and Value are encoded. Parameter type,
        /// AllowedValues, and Strict are expected to never change and should be initialized by the program, not from 
        /// a data file.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ParameterList.Encode(_name) + ":" + ParameterList.Encode(_value);
        }

    }
}

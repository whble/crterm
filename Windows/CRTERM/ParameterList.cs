using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRTerm
{
    public class ParameterList : Dictionary<string, ParameterItem>
    {
        public string Name = "Parameters";

        private ParameterList()
            :this(null)
        {
        }

        public ParameterList(object Parent)
        {
            AddParameter(new ParameterItem("Type", Parent.GetType().ToString()));
            System.Diagnostics.Debug.WriteLine("Parameter Type: " + this.GetValue("Type"));
        }

        public ParameterList(string s)
        {
            Load(s);
        }

        private void Load(string s)
        {
            string[] parts = s.Split(';');
            foreach (string part in parts)
            {
                
            }
        }

        /// <summary>
        /// Add new parameter if it does not already exist
        /// If the item does exist, NewParameter is discarded.
        /// </summary>
        /// <param name="NewParameter">New parameter to add</param>
        public void AddParameter(ParameterItem NewParameter)
        {
            if (!this.ContainsKey(NewParameter.Name))
                this.Add(NewParameter.Name, NewParameter);
        }

        /// <summary>
        /// Add new parameter if it does not already exist
        /// If the item does exist, NewParameter is discarded.
        /// </summary>
        /// <param name="Name">Name of new parameter</param>
        /// <param name="Value">Value for new parameter</param>
        public void AddParameter(string Name, string Value)
        {
            ParameterItem param = GetParameter(Name);
            AddParameter(param);
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            foreach (string Name in this.Keys)
            {
                s.Append(this[Name].ToString());
            }
            return s.ToString();
        }

        public ParameterItem GetParameter(string Name)
        {
            if (this.ContainsKey(Name))
                return this[Name];

            ParameterItem newItem = new ParameterItem(Name);
            AddParameter(newItem);
            return newItem;
        }

        public string GetValue(string Name)
        {
            if (this.ContainsKey(Name))
                return this[Name].Value;
            return "";
        }

        public void SetValue(string Name, string Value)
        {
            ParameterItem item = GetParameter(Name);
            item.Value = Value;
        }

        public static string Encode(string s)
        {
            return s;
        }

        public static string Decode(string s)
        {
            return s;
        }
    }
}

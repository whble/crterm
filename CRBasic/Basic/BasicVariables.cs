using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRBasic.Basic
{
    /// <summary>
    /// Contains all of the variables in use in the BASIC program. Since the variables must be available in
    /// immediate mode, this list will persist the variables between runs. 
    /// <para>Use CreateGlobal to add global variables to your program.</para>
    /// </summary>
    public class BasicVariables : SortedList<string, BasicVariable>
    {
        public List<string> Watches = new List<string>();

        /// <summary>
        /// Create a global variable and, optionally, place a watch on that value.
        /// </summary>
        /// <param name="Name">Variable name</param>
        /// <param name="NewVariable">Value</param>
        /// <param name="Watch">If true, add this to the watch list</param>
        public void CreateGlobal(string Name, string Type)
        {
            BasicVariable v = new BasicVariable();
            v.Name = Name;
            v.Type = Type;
            this.Add(Name, v);
        }

        /// <summary>
        /// Get a list of all the variable names currently allocated
        /// </summary>
        /// <param name="Separator">Optional separator. Default is newline.</param>
        /// <returns></returns>
        public string GetNames(string Separator = "\r\n")
        {
            StringBuilder s = new StringBuilder();
            foreach (string key in this.Keys)
            {
                s.Append(key);
                s.Append(Separator);
            }
            return s.ToString();
        }

        /// <summary>
        /// Gets the names and values of all of the allocated variables.
        /// </summary>
        /// <param name="Separator"></param>
        /// <returns></returns>
        public string GetValues(string Separator = "\r\n")
        {
            StringBuilder s = new StringBuilder();
            foreach (string key in this.Keys)
            {
                s.Append(key);
                s.Append("=");
                s.Append(CSTR(this[key]));
                s.Append(Separator);
            }
            return s.ToString();
        }

        public void AddWatch(string Key)
        {
            if (Watches.Contains(Key))
                return;
            Watches.Add(Key);
        }

        /// <summary>
        /// Returns a string representation of the variable.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string CSTR(object strValue)
        {
            if (strValue is string)
                return "\"" + strValue + "\"";
            return strValue.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using CRTERM.Transport;
using CRTERM.Modem;
using CRTERM.Terminal;

namespace CRTERM.Common
{
	public class ProviderInstanceControl
	{
		private static ProviderInstanceControl _thisInstance = new ProviderInstanceControl();
		public static ProviderInstanceControl GetInstance()
		{
			return _thisInstance;
		}

		static SortedList<string, Type> Providers;
		private ProviderInstanceControl()
		{
			Providers = new SortedList<string, Type>();

			Assembly a = Assembly.GetExecutingAssembly();
			Type[] types = a.GetTypes();
			foreach (Type t in types)
			{
				if (t.IsClass && typeof(ICommProvider).IsAssignableFrom(t))
				{
					Providers.Add(t.Name, t);
				}
			}
		}

		/// <summary>
		/// Get a list of all the available providers for a specific provider type. Use ICommProvider to get
		/// all of the providers of all types, or specify a specific type to get a specific provider
		/// <example>List&lt;string&gt; </string>TransportList = GetProviderNames&lt;ITransport&gt;()</example>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static List<string> GetProviderNames<T>() where T : class, ICommProvider
		{
			List<string> keys = new List<string>();
			foreach (string key in Providers.Keys)
			{
				Type t = Providers[key];
				if (typeof(T).IsAssignableFrom(t))
					keys.Add(key);
			}
			return keys;
		}

		public static ICommProvider GetProviderInstance<T>(string ProviderName) where T : class,ICommProvider
		{
			if (!Providers.ContainsKey(ProviderName))
				return null;

			Type t = Providers[ProviderName];
			return Activator.CreateInstance(t) as ICommProvider;
		}

	}
}

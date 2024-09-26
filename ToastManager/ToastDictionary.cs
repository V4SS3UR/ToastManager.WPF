using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using ToastManager.Core;

namespace ToastManager
{
    internal class ToastDictionary
    {
        // Use a tuple of (parent, toastName) as the unique key
        private static Dictionary<(DependencyObject parent, string toastName), Toast> dictionary = new Dictionary<(DependencyObject, string), Toast>();

        public static void RegisterToast(DependencyObject parent, string toastName, Toast toast)
        {
            // Use parent (view instance) and toastName as the unique key
            var key = (parent, toastName);
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, toast);
            }
        }

        public static void UnregisterToast(DependencyObject parent, string toastName)
        {
            // Use parent (view instance) and toastName as the unique key
            var key = (parent, toastName);
            if (dictionary.ContainsKey(key))
            {
                dictionary.Remove(key);
            }
        }

        public static Toast GetToast(DependencyObject parent, string toastName)
        {
            // Use parent (view instance) and toastName as the unique key
            var key = (parent, toastName);  
            if (dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }
            return null;
        }

        public static IEnumerable<Toast> GetToasts(string toastName)
        {
            // Get the entry with the toastName
            var entry = dictionary.Where(x => x.Key.toastName == toastName);

            if(entry != null)
            {
                return entry.Select(o => o.Value);
            }

            return null;
        }


    }
}

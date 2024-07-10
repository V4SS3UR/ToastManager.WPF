using System.Collections.Generic;

namespace ToastManager
{
    internal class ToastDictionary
    {
        private static Dictionary<string, object> dictionary = new Dictionary<string, object>();     

        public static void RegisterToast(string toastName, Toast toast)
        {
            if (!dictionary.ContainsKey(toastName))
            {
                dictionary.Add(toastName, toast);
            }
        }
        public static void UnregisterToast(string toastName)
        {
            if (dictionary.ContainsKey(toastName))
            {
                dictionary.Remove(toastName);
            }
        }
        public static Toast GetToast(string toastName)
        {
            if (dictionary.ContainsKey(toastName))
            {
                return (Toast)dictionary[toastName];
            }
            return null;
        }
    }
}

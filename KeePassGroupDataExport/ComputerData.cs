using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KeePassGroupDataExport
{
    public class ComputerData
    {
        public string DeviceName { get; set; }
        public string Username { get; set; }
        public Dictionary<string, string> AdvancedInformation { get; set; }
        public static HashSet<string> AllKeys { get; } = new HashSet<string>();
        
        public ComputerData(string deviceName)
        {
            DeviceName = deviceName;
            AdvancedInformation = new Dictionary<string, string>();
        }
        
        public ComputerData(string deviceName, string username)
        {
            DeviceName = deviceName;
            Username = username;
            AdvancedInformation = new Dictionary<string, string>();
        }
        
        public void AddAdvancedInformation(string key, string value)
        {
            if (IsKeyUsername(key))
                Username = value;
            else if (!AdvancedInformation.ContainsKey(key) && !IsKeyTitle(key))
                AdvancedInformation.Add(key, value);

            AllKeys.Add(key);
        }
        
        private bool IsKeyUsername(string key)
        {
            return key.Equals("username", StringComparison.OrdinalIgnoreCase);
        }

        private bool IsKeyTitle(string key)
        {
            return key.Equals("title", StringComparison.OrdinalIgnoreCase);
        }
        
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Komputer: {DeviceName}");
            sb.AppendLine($"Użytkownik: {Username}");
            sb.AppendLine("Informacje Dodatkowe:");

            foreach (var info in AdvancedInformation.OrderBy(k => k.Key))
            {
                sb.AppendLine($"  {info.Key}: {info.Value}");
            }

            return sb.ToString();
        }
    }
}
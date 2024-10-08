﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KeePassGroupDataExport
{
    /// <summary>
    /// Reprezentuje dane komputera.
    /// </summary>
    public class ComputerData
    {
        public string DeviceName { get; set; }
        public string Username { get; set; }
        public Dictionary<string, string> AdvancedInformation { get; set; }
        public static HashSet<string> AllKeys { get; set; } = new HashSet<string>();
        public static HashSet<string> ExportKeys { get; set; } = new HashSet<string>();
        public Dictionary<string, string> ExportOrderData { get; set; } = new Dictionary<string, string>();

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

        /// <summary>
        /// Dodaje zaawansowane informacje do danych komputera.
        /// </summary>
        public void AddAdvancedInformation(string key, string value)
        {
            if (IsKeyUsername(key))
            {
                Username = value;
                AllKeys.Add("Użytkownik");
            }
            else if (IsKeyTitle(key))
            {
                AllKeys.Add("Komputer");
            }
            else if (!AdvancedInformation.ContainsKey(key) && !IsKeyTitle(key))
            {
                if (key.Equals("CPU"))
                {
                    key = "Procesor";
                }

                AdvancedInformation.Add(key, value);
                AllKeys.Add(key);
            }
        }

        private bool IsKeyUsername(string key) => key.Equals("UserName", StringComparison.OrdinalIgnoreCase);

        private bool IsKeyTitle(string key) => key.Equals("Title", StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Przygotowuje dane do eksportu.
        /// </summary>
        public void PrepareDataForExport(string emptyFieldValue)
        {
            if (ExportOrderData.Any())
                ExportOrderData.Clear();

            Username = string.IsNullOrWhiteSpace(Username) ? emptyFieldValue : Username;
            DeviceName = string.IsNullOrWhiteSpace(DeviceName) ? emptyFieldValue : DeviceName;

            foreach (var key in ExportKeys)
            {
                if (!AdvancedInformation.ContainsKey(key) && !IsKey(key))
                {
                    AdvancedInformation[key] = emptyFieldValue;
                }

                switch (key)
                {
                    case "Użytkownik":
                        ExportOrderData[key] = Username;
                        break;
                    case "Komputer":
                        ExportOrderData[key] = DeviceName;
                        break;
                    default:
                        if (AdvancedInformation.TryGetValue(key, out var value))
                        {
                            ExportOrderData[key] = value;
                        }
                        break;
                }
            }
        }

        private bool IsKey(string key) => key.Equals("Użytkownik") || key.Equals("Komputer");

        /// <summary>
        /// Zwraca stringową reprezentację danych komputera.
        /// </summary>
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

        /// <summary>
        /// Sprawdza dane do eksportu.
        /// </summary>
        public string ExportDataCheck()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Eksportowane dane:");
            foreach (var key in ExportOrderData.Keys)
            {
                sb.AppendLine($"  {key}: {ExportOrderData[key]}");
            }

            return sb.ToString();
        }
    }
}

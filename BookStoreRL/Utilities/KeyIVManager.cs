using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookStoreRL.Utilities
{
    public static class KeyIvManager
    {
        private static readonly string FilePath = "keys_and_iv.json";

        // Class to represent the key and IV
        private class UserKeyIv
        {
            public string UserName { get; set; }
            public string Key { get; set; }
            public string Iv { get; set; }
        }

        // Save the key and IV to a JSON file
        public static void SaveKeyAndIv(string userName, byte[] key, byte[] iv)
        {
            var userKeyIv = new UserKeyIv
            {
                UserName = userName,
                Key = Convert.ToBase64String(key),
                Iv = Convert.ToBase64String(iv)
            };

            var existingData = ReadAllKeysAndIvs();
            existingData[userName] = userKeyIv;

            string jsonString = JsonSerializer.Serialize(existingData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, jsonString);
        }

        // Retrieve the key and IV from the JSON file
        public static (byte[] key, byte[] iv) GetKeyAndIv(string userName)
        {
            var existingData = ReadAllKeysAndIvs();

            if (existingData.TryGetValue(userName, out var userKeyIv))
            {
                byte[] key = Convert.FromBase64String(userKeyIv.Key);
                byte[] iv = Convert.FromBase64String(userKeyIv.Iv);
                return (key, iv);
            }

            throw new ArgumentException("Key and IV not found for the given username.");
        }

        // Read all keys and IVs from the JSON file
        private static Dictionary<string, UserKeyIv> ReadAllKeysAndIvs()
        {
            if (!File.Exists(FilePath))
            {
                return new Dictionary<string, UserKeyIv>();
            }

            string jsonString = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<Dictionary<string, UserKeyIv>>(jsonString) ?? new Dictionary<string, UserKeyIv>();
        }
    }
}

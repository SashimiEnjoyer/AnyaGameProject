using UnityEngine;
using System;
using System.IO;
using System.Security.Cryptography;

public static class SaveSystem 
{
const string FILE_EXTENSION = ".dat";
    private static readonly string _savePath = Application.persistentDataPath;

    private static string _encryptionKey;
    private static string _encryptionIV;

    public static void Save<T>(string key, T objectToSave)
    {
        SaveData(key, objectToSave);
    }

    public static void CheckAESSetup()
    {
        if (!PlayerPrefs.HasKey("aes_key"))
        {
            using (Aes aes = Aes.Create())
            {
                PlayerPrefs.SetString("aes_key", Convert.ToBase64String(aes.Key));
                PlayerPrefs.SetString("aes_iv", Convert.ToBase64String(aes.IV));
            }
        }

        _encryptionKey = PlayerPrefs.GetString("aes_key");
        _encryptionIV = PlayerPrefs.GetString("aes_iv");
    }

    public static void SaveData<T>(string fileName, T objectToSave)
    {
        try
        {
            Directory.CreateDirectory(_savePath);
            string fullPath = Path.Combine(_savePath, $"{Application.productName}_{fileName}{FILE_EXTENSION}");

            // Convert object to JSON
            string json = JsonUtility.ToJson(objectToSave, true);

            // Encrypt JSON
            string encrypted = Encrypt(json);

            // Write encrypted data
            File.WriteAllText(fullPath, encrypted);

#if UNITY_EDITOR
            Debug.Log($" Encrypted save written to: {fullPath}");
#endif
        }
        catch (Exception e)
        {
            Debug.LogError($" Save failed: {e.Message}");
        }
    }

    public static T LoadData<T>(string key, Action<T> onComplete = null, Action<string> onFailed = null)
    {
        string fullPath = Path.Combine(_savePath, $"{Application.productName}_{key}{FILE_EXTENSION}");

        if (File.Exists(fullPath))
        {
            try
            {
                string encrypted = File.ReadAllText(fullPath);
                string decrypted = Decrypt(encrypted);
                T data = JsonUtility.FromJson<T>(decrypted);

                onComplete?.Invoke(data);
                return data;
            }
            catch (Exception e)
            {
                Debug.LogError($" Load failed: {e.Message}");
                onFailed?.Invoke(e.Message);
                return default;
            }
        }
        else
        {
            string msg = $"File not found: {fullPath}";
            //Debug.LogWarning(msg);
            onFailed?.Invoke(msg);
            return default;
        }
    }

    public static bool IsExists(string filename)
    {
        string fullPath = Path.Combine(_savePath, $"{Application.productName}_{filename}{FILE_EXTENSION}");
        return File.Exists(fullPath);
    }

    // --- AES Encryption/Decryption Helpers ---
    private static string Encrypt(string plainText)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Convert.FromBase64String(_encryptionKey);
            aes.IV  = Convert.FromBase64String(_encryptionIV);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using (MemoryStream ms = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (StreamWriter sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
                sw.Close();
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    private static string Decrypt(string cipherText)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Convert.FromBase64String(_encryptionKey);
            aes.IV  = Convert.FromBase64String(_encryptionIV);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (MemoryStream ms = new MemoryStream(buffer))
            using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (StreamReader sr = new StreamReader(cs))
            {
                return sr.ReadToEnd();
            }
        }
    }
}


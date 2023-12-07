
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;


public static class SaveSystem 
{
    private static string _savePath = $"{Application.persistentDataPath}";

    public static void Save<T>(string key, T objectToSave)
    {
        SaveData(key, objectToSave);
    }

    public static void SaveData<T>( string fileName, T objectToSave)
    {
        // Create the directory IF it doesn't already exist
        Directory.CreateDirectory(_savePath);
        // Grab an instance of the BinaryFormatter that will handle serializing our data
        BinaryFormatter formatter = new BinaryFormatter();
        // Open up a filestream, combining the path and object key
        FileStream fileStream = new FileStream($"{_savePath}{fileName}.any", FileMode.Create, FileAccess.Write);

        // Try/Catch/Finally block that will attempt to serialize/write-to-stream, closing stream when complete
        try
        {
            formatter.Serialize(fileStream, objectToSave);
        }
        catch (SerializationException exception)
        {
            Debug.LogError("Save failed. Error: " + exception.Message);
        }
        finally
        {
            fileStream.Close();
            fileStream.Dispose();
        }
    }

    public static T LoadData<T>(string key)
    {
        if (IsExists( key))
        {
            bool isValid = true;
            string error = "";
            // Initialize a variable with the default value of whatever type we're using
            T returnValue = default(T);

            // Grab an instance of the BinaryFormatter that will handle serializing our data
            BinaryFormatter formatter = new BinaryFormatter();

            // Open up a filestream, combining the path and object key
            FileStream fileStream = new FileStream($"{_savePath}{key}.any", FileMode.Open, FileAccess.Read);

            /* 
            * Try/Catch/Finally block that will attempt to deserialize the data
            * If we fail to successfully deserialize the data, we'll just return the default value for Type
            */
            try
            {
                returnValue = (T)formatter.Deserialize(fileStream);
            }
            catch (SerializationException exception)
            {
                error = exception.Message;
                isValid = false;
            }
            finally
            {
                fileStream.Close();
                fileStream.Dispose();
            }

            if (isValid)
            {
                //onComplete?.Invoke(returnValue);
                return returnValue;
            }
            else
            {
                //onFailed?.Invoke(error);
                return default;
            }
        }
        else
        {
            //onFailed?.Invoke("Can't read data or file not exists");
            return default;
        }
    }

    public static bool IsExists(string filename)
    {
        return File.Exists($"{_savePath}{filename}.any");
    }
}


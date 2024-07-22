using System;
using System.IO;
using UnityEngine;

namespace Dungeon
{
    public static class JsonDataController
    {
        public static void SaveData<T>(T data, string filePath)
        {
            try
            {
                string json = JsonUtility.ToJson(data, true);
                File.WriteAllText(filePath, json);
            }
            catch (Exception exception)
            {
                Debug.LogError("データの保存に失敗しました: " + exception.Message);
            }
        }

        public static T LoadData<T>(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    T data = JsonUtility.FromJson<T>(json);
                    Debug.Log("LoadData " + data + ": OK ");
                    return data;
                }
                else
                {
                    T newData = Activator.CreateInstance<T>();
                    SaveData(newData, filePath);
                    Debug.LogWarning("ファイルが存在しなかったため、新規データを作成しました");

                    return newData;
                }
            }
            catch (Exception exception)
            {
                Debug.LogError("データの読み込みに失敗しました: " + exception.Message);
                return default(T);
            }
        }
    }
}

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
                Debug.LogError("�f�[�^�̕ۑ��Ɏ��s���܂���: " + exception.Message);
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
                    Debug.LogWarning("�t�@�C�������݂��Ȃ��������߁A�V�K�f�[�^���쐬���܂���");

                    return newData;
                }
            }
            catch (Exception exception)
            {
                Debug.LogError("�f�[�^�̓ǂݍ��݂Ɏ��s���܂���: " + exception.Message);
                return default(T);
            }
        }
    }
}

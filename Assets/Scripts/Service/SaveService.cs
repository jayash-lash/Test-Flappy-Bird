using System;
using System.Collections.Generic;
using System.IO;
using IoC;
using Unity.VisualScripting;
using UnityEngine;

namespace Service
{
    public class SaveService : MonoBehaviour, IService
    {
        public Type ServiceType => typeof(SaveService);
    
        private readonly Dictionary<Type, object> _savesMap = new Dictionary<Type, object>();

        public void Save(object obj)
        {
            var savesDirectory = $"{Application.persistentDataPath}/Saves/";
            
            var savePath = $"{savesDirectory}Save{obj.GetType().Name}.json";
            var jsonString = obj.Serialize().json;

            if (!Directory.Exists(savesDirectory))
                Directory.CreateDirectory(savesDirectory);
            
            File.WriteAllText(savePath,jsonString);
        }

        public T Load<T>() where T : class, new() 
        {
            var savesDirectory = $"{Application.persistentDataPath}/Saves/";

            var savePath = $"{savesDirectory}Save{typeof(T).Name}.json";

            if (!File.Exists(savePath))
            {
                var obj = new T();
                _savesMap.Add(typeof(T),obj);
                return obj;
            }
        
            var fileContents = File.ReadAllText(savePath);
            var saveInst = JsonUtility.FromJson<T>(fileContents);
            _savesMap.Add(typeof(T),saveInst);
        
            return saveInst;
        }

        public T LoadFromCache<T>() where T : class, new()
        {
            return !_savesMap.TryGetValue(typeof(T), out var saveData) ? Load<T>() : (T)saveData;
        }

        public void SaveCached()
        {
            foreach (var keyValuePair in _savesMap)
                Save(keyValuePair.Value);
        }
    }

    [Serializable]
    public class GameSaves
    {
        public int[] HighScores = new int[3]; 
        public float Volume = 1f;
    }
}
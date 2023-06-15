using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IoC
{
    public class ServiceLocator
    {
        private static ServiceLocator _instance;
        private static ServiceLocator Instance => _instance ??= new ServiceLocator();

        private readonly Dictionary<Type, IService> _servicesMap = new Dictionary<Type, IService>();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void Initialize()
        {
            SceneManager.sceneUnloaded += _ => Clear();
        }

        public static void Register(IService service)
        {
            if(Instance._servicesMap.ContainsKey(service.ServiceType)) return;
      
            Instance._servicesMap.Add(service.ServiceType, service);
        }

        public static void Remove(IService service)
        {
            if (!Instance._servicesMap.ContainsKey(service.ServiceType)) return;
            Instance._servicesMap.Remove(service.ServiceType);
        }

        public static T Resolve<T>() where T : class, IService
        {
            var type = typeof(T);

            if (!Instance._servicesMap.TryGetValue(type, out var service))
            {
                Debug.LogError($"[{nameof(ServiceLocator)}] Instance of type {type} not contains in the map");
                return null;
            }

            return (T) service;
        }

        public static void Clear()
        {
            Instance._servicesMap.Clear();
        }
    }
}
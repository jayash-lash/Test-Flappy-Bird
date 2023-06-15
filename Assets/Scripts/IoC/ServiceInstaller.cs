using UnityEngine;

namespace IoC
{
    [DefaultExecutionOrder(-1000)]
    public class ServiceInstaller : MonoBehaviour
    {
        private void Awake()
        {
            var services = GetComponentsInChildren<IService>(true);
            foreach (var service in services)
            {
                ServiceLocator.Register(service);
            }
        }
    }
}
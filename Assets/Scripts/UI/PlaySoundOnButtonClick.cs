using IoC;
using Service;
using UnityEngine;

namespace UI
{
    public class PlaySoundOnButtonClick : MonoBehaviour
    {
        [SerializeField] private AudioClip _audioClip;

        private void Awake()
        {
            var soundManager = ServiceLocator.Resolve<SoundManager>();
        }

        public void OnPointerClick()
        {
            
        }
    }
}

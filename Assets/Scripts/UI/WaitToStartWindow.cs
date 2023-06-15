using IoC;
using Player;
using UnityEngine;

namespace UI
{
    public class WaitToStartWindow : MonoBehaviour
    {
        private PlayerModel _playerModel;

        private void Awake()
        {
            _playerModel = ServiceLocator.Resolve<PlayerModel>();
            _playerModel.OnModelChanged += OnHideText;
        }

        private void OnHideText()
        {
            if (_playerModel.IsMoving && gameObject.activeSelf) 
                gameObject.SetActive(false);
        }
    
        private void OnDisable()
        {
            _playerModel.OnModelChanged -= OnHideText;
        }
    }
}
using IoC;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        private PlayerModel _playerModel;

        private void Awake()
        {
            _playerModel = ServiceLocator.Resolve<PlayerModel>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Trigger"))
            {
                _playerModel.IncreaseScoreAmount();
                return;
            }

            _playerModel.SetIsAlive(false);
        }
    }
}
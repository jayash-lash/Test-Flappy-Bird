using IoC;
using Player;
using UnityEngine;

namespace Cummon
{
    public class SpriteAnimator : MonoBehaviour
    {
        [SerializeField] private Sprite[] _frameArray;

        private int _currentFrame;
        private float _timer;
        private float _framerate = 0.07f;
        private SpriteRenderer _spriteRenderer;
        private PlayerModel _playerModel;

        private void Awake()
        {
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            _playerModel = ServiceLocator.Resolve<PlayerModel>();
        }

        private void Update()
        {
            if (!_playerModel.IsAlive) return;
            _timer += Time.deltaTime;

            if (_timer >= _framerate)
            {
                _timer -= _framerate;
                _currentFrame = (_currentFrame + 1) % _frameArray.Length;
                _spriteRenderer.sprite = _frameArray[_currentFrame];
            }
        }
    }
}

using IoC;
using Service;
using UnityEngine;

namespace Player
{
   public class PlayerMovement : MonoBehaviour
   {
      [SerializeField] private float _jumpForce = 90f;
      [SerializeField] private float _maxSpeed = 100;
      [SerializeField] private AudioClip _clip;
      
      private Rigidbody2D _rigidbody;
      private float _gravityScale;
      private GameInput _gameInput;
      private PlayerModel _playerModel;
      private SoundManager _soundManager;
      
      private void OnEnable()
      {
         _gameInput.OnJumpPerformed += OnJump;
         _playerModel.OnModelChanged += OnHitSomething;
      }

      private void OnHitSomething()
      {
         if (!_playerModel.IsAlive)
            _jumpForce = 0f;
      }

      private void Awake()
      {
         _rigidbody = gameObject.GetComponent<Rigidbody2D>();
         _gravityScale = _rigidbody.gravityScale;
         _rigidbody.gravityScale = 0f;
         _gameInput = ServiceLocator.Resolve<GameInput>();
         _playerModel = ServiceLocator.Resolve<PlayerModel>();
         _soundManager = ServiceLocator.Resolve<SoundManager>();
      }
   
      private void OnDisable()
      {
         _gameInput.OnJumpPerformed -= OnJump;
         _playerModel.OnModelChanged -= OnHitSomething;
      }
   
      private void OnJump()
      {
         _rigidbody.gravityScale = _gravityScale;
         _playerModel.SetIsMoving(true);
         _rigidbody.velocity = Vector2.up * _jumpForce;
         transform.eulerAngles = new Vector3(0, 0, _rigidbody.velocity.y * .15f);
         _soundManager.PlaySoundJump(_clip);
      }

      private void FixedUpdate()
      {
         var velocity = _rigidbody.velocity;
         _rigidbody.velocity = Vector2.ClampMagnitude(velocity, _maxSpeed);
      }
   }
}
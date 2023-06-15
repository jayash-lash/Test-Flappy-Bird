using IoC;
using Player;
using Service;
using UnityEngine;

namespace Cummon
{
  public class Obstacle : MonoBehaviour, IPooledObject
  {
    [SerializeField] private float _speed;

    private PlayerModel _playerModel;

    private void Awake()
    {
      _playerModel = ServiceLocator.Resolve<PlayerModel>();
    }

    public void OnObjectSpawned()
    {

    }

    public void Update()
    {
      if (_playerModel.IsAlive)
        transform.position += Vector3.left * (_speed * Time.deltaTime);
    }
  }
} 
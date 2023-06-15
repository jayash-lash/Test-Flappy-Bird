using IoC;
using Player;
using Service;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Factory
{
    public class ObjectsSpawner : MonoBehaviour
    {
        [Header("SpawnPoints")]
        [SerializeField] private Transform _spawnPointObstacles;
        [SerializeField] private Transform _spawnPointGround;
        [SerializeField] private Transform _spawnPointClouds;
    
        [Header("TimeSettings")]
        [SerializeField] private float _queueTime = 3f;
        [SerializeField] private float _time = 0;

        [Header("ObstaclesSetting")]
        [SerializeField] private float _obstaclesHeight;
    
        [Header("CloudsSettings")]
        [SerializeField] private float _cloudsHeight;
    
        private ObjectPools _pools = null;
        private PlayerModel _playerModel;

        private void Awake()
        {
            _pools = ServiceLocator.Resolve<ObjectPools>();
            _playerModel = ServiceLocator.Resolve<PlayerModel>();
            _playerModel.OnModelChanged += OnPlayerModelChanged;
        }

        private void OnPlayerModelChanged()
        {
            if (!_playerModel.IsAlive)
                enabled = false;
        }

        public void Update()
        {
            if (!_playerModel.IsMoving) return;
            
            if (_time > _queueTime)
            {
                _pools.SpawnFromPool("Pipe", _spawnPointObstacles.position + new Vector3(0, Random.Range(-_obstaclesHeight, _obstaclesHeight), 0f), Quaternion.identity);
                _pools.SpawnFromPool("Ground", _spawnPointGround.position, Quaternion.identity);
                _pools.SpawnFromPool("Clouds", _spawnPointClouds.position + new Vector3(0, Random.Range(-_cloudsHeight, _cloudsHeight), 0f), Quaternion.identity);
            
                _time = 0;
            }

            _time += Time.deltaTime;
        }

        private void OnDestroy()
        {
            _playerModel.OnModelChanged -= OnPlayerModelChanged;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace _FlappyBird._Scripts
{
    public class PipeSpawner : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject pipePrefab;

        [Space(10)]
        
        [Header("Spawner Settings")]
        [Tooltip("Time (in seconds) after which a new pipe will spawn---Default = 2f")]
        [Range(1, 10)]
        [SerializeField] private float spawnInterval = 2f;
        [Tooltip("The height offset of the pipe (Defines the lower and upper bounds limit)---Default = 10f")]
        [SerializeField] private float heightOffset = 10f;
        
        private float _highestSpawnPointForPipe;
        private float _lowestSpawnPointForPipe;
        private readonly Queue<GameObject> _pipePool = new Queue<GameObject>();
        public static PipeSpawner Instance;

        #region Core Unity Methods
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
            InstantiatePipes();
        }
        private void Start()
        {
            StartCoroutine(SpawnPipeRoutine());
        }

        private IEnumerator SpawnPipeRoutine()
        {
            yield return new WaitUntil(() => GameStateManager.Instance.currentState == GameState.Play);
            while (true)
            {
                SpawnPipe();
                yield return new WaitForSeconds(spawnInterval);
            }
        }
        
        #endregion
    
        #region Helper Methods
        private void InstantiatePipes()
        {
            const int pipeSpawnAmount = 10;
        
            _lowestSpawnPointForPipe = transform.position.y - heightOffset;
            _highestSpawnPointForPipe = transform.position.y + heightOffset;
        
            for (var i = 0; i < pipeSpawnAmount; i++)
            {
                var spawnPosition = new Vector3(transform.position.x, Random.Range(_lowestSpawnPointForPipe, _highestSpawnPointForPipe),
                    transform.position.z);
            
                var pipe = Instantiate(pipePrefab, spawnPosition  , Quaternion.identity );
                ReturnToPool(pipe);
            }
        }
        private void SpawnPipe()
        {
            var pipe = GetFromPool();
            pipe.transform.position = new Vector3(transform.position.x, Random.Range(_lowestSpawnPointForPipe, _highestSpawnPointForPipe),
                transform.position.z);
        }
        public void ReturnToPool(GameObject pipe)
        {
            pipe.SetActive(false);
            _pipePool.Enqueue(pipe);
        }
        private GameObject GetFromPool()
        {
            var pipe = _pipePool.Dequeue();
            pipe.SetActive(true);
            return pipe;
        }
        #endregion
    }
}

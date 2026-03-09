using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PipeSpawnerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject pipePrefab;
    [SerializeField]
    private float spawnRate = 2f;
    [SerializeField]
    private float heightOffset = 10f;
    
    private float _timer = 0f;
    
    private readonly Queue<GameObject> _pipePool = new Queue<GameObject>();
    
    private float _highestSpawnPointForPipe;
    private float _lowestSpawnPointForPipe;

    public static PipeSpawnerScript Instance;

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
        SpawnPipe();
    }

    private void Update()
    {
        if (_timer < spawnRate)
        {
            _timer = _timer + Time.deltaTime;
        }
        else
        {
            SpawnPipe();
            _timer = 0;
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

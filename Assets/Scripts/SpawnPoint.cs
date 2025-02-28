using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private int _enemyCount;
    [SerializeField] private float _timeBetweenSpawns;
    [SerializeField] private Transform _enemySpawnLocation;

    private int _currentEnemyCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("StartSpawnSequence");
    }

    IEnumerator StartSpawnSequence()
    {
        while(_currentEnemyCount < _enemyCount)
        {
            Instantiate(_enemy, _enemySpawnLocation.transform.position, _enemySpawnLocation.transform.rotation);
            yield return new WaitForSeconds(_timeBetweenSpawns);
            _currentEnemyCount++;
            Debug.Log("Our enemy count is " + _enemyCount);
        }        
    }
}

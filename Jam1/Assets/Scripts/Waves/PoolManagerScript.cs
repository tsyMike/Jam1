using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManagerScript : MonoBehaviour
{
    [SerializeField]
    private int _defaultPoolSize = 10;
    [SerializeField]
    private GameObject _enemyPrefab;
    private List<GameObject> _enemyPool = new List<GameObject>();
    void Start()
    {
        GenerateEnemies(_defaultPoolSize);
    }

    List<GameObject> GenerateEnemies(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject enemy = Instantiate(_enemyPrefab);
            enemy.SetActive(false);

            _enemyPool.Add(enemy);
        }

        return _enemyPool;
    }
}

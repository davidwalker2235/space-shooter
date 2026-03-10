using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemy;
    [SerializeField]
    private float _spawnInterval = 5.0f;
    private IEnumerator coroutine;
    [SerializeField]
    GameObject _enemyContainer;
    private bool _stopSpawning = false;


    void Start()
    {
        coroutine = SpawnRoutine();
        StartCoroutine(coroutine);
    }

    private IEnumerator SpawnRoutine()
    {
        while (!_stopSpawning)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9, 9), 6, 0);
            GameObject newEnemy = Instantiate(enemy, posToSpawn, Quaternion.identity);
            newEnemy.transform.SetParent(_enemyContainer.transform);
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}

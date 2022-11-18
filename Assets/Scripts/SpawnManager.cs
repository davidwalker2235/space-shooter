using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject[] _powerupPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _powerupContainer;
    [SerializeField]

    private bool isPlayerAlive = true;
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine ()
    {
        while (isPlayerAlive)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9.4f, 9.4f), 6.4f, 0);
            GameObject newEnemy = Instantiate<GameObject>(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPowerupRoutine ()
    {
        while (isPlayerAlive)
        {
            int prefabIndex = Random.Range(0, _powerupPrefab.Length);
            Vector3 posToSpawnPowerup = new Vector3(Random.Range(-9.4f, 9.4f), 6.4f, 0);
            GameObject newPowerup = Instantiate<GameObject>(_powerupPrefab[prefabIndex], posToSpawnPowerup, Quaternion.identity);
            newPowerup.transform.parent = _powerupContainer.transform;
            yield return new WaitForSeconds(Random.Range(32f, 7f));
        }
    }

    public void OnPlayerDeath ()
    {
        isPlayerAlive = false;
    }
}

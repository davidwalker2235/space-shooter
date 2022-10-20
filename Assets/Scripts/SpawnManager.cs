using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private bool isPlayerAlive = true;
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine ()
    {
        while (isPlayerAlive)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9.4f, 9.4f), 6.4f, 0);
            GameObject newEnemy = Instantiate<GameObject>(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    public void OnPlayerDeath ()
    {
        isPlayerAlive = false;
    }
}

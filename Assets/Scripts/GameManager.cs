using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject player;
    private int maxEnemies = 1;
    private static GameManager instance;
    private List<GameObject> enemies;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(instance == null, "GameManager.instance == null");
        instance = this;
        enemies = new List<GameObject>();
        SpawnEnemies();
    }

    public static GameManager getInstance()
    {
        return instance;
    }
    private bool CanSpawnEnemyAt(Vector3 position)
    {
        foreach (GameObject other in enemies)
        {
            if (Vector3.Distance(other.transform.position, position) < 1.5)
            {
                return false;
            }
        }
        if (Vector3.Distance(player.transform.position, position) < 1.5)
        {
            return false;
        }
        return true;
    }

    public void SpawnEnemy()
    {
        Vector3 position = new Vector3(Random.Range(-10.0f, 10.0f), 0.5f, Random.Range(-10.0f, 10.0f));
        if (CanSpawnEnemyAt(position))
        {
            GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
            enemies.Add(enemy);
        }
    }

    public void SpawnEnemies()
    {
        while (enemies.Count < maxEnemies)
        {
            SpawnEnemy();
        }
    }

    public void HitEnemy(GameObject enemy)
    {
        Debug.Assert(enemies.Remove(enemy));
        Destroy(enemy);
        SpawnEnemies();
    }
}

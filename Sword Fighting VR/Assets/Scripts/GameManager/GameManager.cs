using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct EnemyCombination
{
    public string groupName;
    public List<GameObject> possibleEnemies;

    public int minScoreCost;
    public int maxScoreCost;
    public enum EnemyOrder
    {
        ORDER,
        RANDOM
    }

    public EnemyOrder orderState;
}
public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    public static GameManager Instance;
    public int score;
    [HideInInspector] public int livingEnemyAmount;
    [SerializeField] private int maxRooms;
    [SerializeField] private List<GameObject> rooms;

    [SerializeField] private GameObject outDoor;

    [SerializeField] private RenderTexture renderTexture;

    [SerializeField] private Transform spawnPos1, spawnPos2;

    private Vector3 _finalSpawnPos;
    public List<GameObject> enemies;

    [SerializeField] private GameObject loseCanvas;
    [SerializeField] private GameObject winCanvas;
    [SerializeField] private float canvasDistance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        _finalSpawnPos = spawnPos1.position;
    }

    public void EndBattle()
    {
        if (score >= maxRooms)
        {
            HandleWinning();
        }

        else
        {
            score++;
            GenerateNextRoom();
        }
        
    }

    private void GenerateNextRoom()
    {
        
        int randomRoom = UnityEngine.Random.Range(0, rooms.Count);

        GameObject newRoom = Instantiate(rooms[randomRoom], _finalSpawnPos, Quaternion.identity);

        _finalSpawnPos = _finalSpawnPos == spawnPos1.position ? spawnPos2.position : spawnPos1.position;

        newRoom.GetComponent<HandleBattling>().HandleSpawningDoor();
    }

    public void StartBattle()
    {
        OnTick.Instance.onTickEvent += CheckIfEnemiesAlive;
    }

    public void SetEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    public void InitializeBattle()
    {
        FindObjectOfType<HandleBattling>().enabled = true;
    }

    private void CheckIfEnemiesAlive()
    {
        List<GameObject> deadEnemies = new List<GameObject>();
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null)
            {
                deadEnemies.Add(enemies[i]);
            }
        }

        for (int i = 0; i < deadEnemies.Count; i++)
        {
            enemies.Remove(deadEnemies[i]);
        }

        if (enemies.Count == 0)
        {

            FindObjectOfType<HandleBattling>().HandleWinning();
            OnTick.Instance.onTickEvent -= CheckIfEnemiesAlive;
        }
    }

    public void HandleGameOver()
    {
        OnTick.Instance.onTickEvent -= CheckIfEnemiesAlive;

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        Instantiate(loseCanvas);
        
    }

    public void HandleWinning()
    {
        OnTick.Instance.onTickEvent -= CheckIfEnemiesAlive;

        Instantiate(winCanvas);
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;
public class GameManager : MonoBehaviour
{
    [SerializeField]   
    public GameState State;

    private bool isPaused;

    public SpawnLocations[] spawnLocations;
    [SerializeField] public List<GameObject> currentEnemies = new List<GameObject>();
    public Transform enemyParent;
    public int enemiesKilled;
    Stopwatch stopwatch = new Stopwatch();
    
    // Start is called before the first frame update
    void Start()
    {
        ChangeState(GameState.Starting);
    }

    public void ChangeState(GameState newState){
        State = newState;
        switch (newState)
        {
            case GameState.Starting:
                HandleStarting();
                break;
            case GameState.Play:
                break;
            case GameState.Paused:
                HandlePausing();
                break;
        }


    }
    void HandleStarting(){
        isPaused = false;
        SpawnEnemy();
        stopwatch.Start();

        ChangeState(GameState.Play);
    }

    void SpawnEnemy(){
        for(int i = 0; i < spawnLocations.Length;i++){
            for(int j = 0; j < spawnLocations[i].enemies.Length;j++){
                float offsetX = UnityEngine.Random.Range(-spawnLocations[i].width/2, spawnLocations[i].width/2/2);
                float offsetY = UnityEngine.Random.Range(-spawnLocations[i].height/2/2, spawnLocations[i].height/2/2);
                 GameObject go = Instantiate(spawnLocations[i].enemies[j], spawnLocations[i].location + new Vector2(offsetX, offsetY), Quaternion.identity);
                 go.transform.parent = enemyParent.transform;
                 currentEnemies.Add(go);
            }
           
        }
    }
    public void RemoveEnemy(GameObject enemy){
        enemiesKilled++;
        currentEnemies.Remove(enemy);
    }
    public void SetSFXVolume(float volume){
        for(int i = 0; i < currentEnemies.Count;i++){
            currentEnemies[i].GetComponent<EnemyUnitBase>().SetAudioVolume(volume);
        }
    }
    public void HandlePausing() => isPaused = !isPaused;
    public bool IsPaused() => isPaused;
    public int GetEnemiesKilled() => enemiesKilled;
    public string GetSurvivedTime(){
        TimeSpan ts = stopwatch.Elapsed;
        return String.Format("{0:0}M {1:0}S", ts.Minutes, ts.Seconds);
    }

}
[Serializable]
public enum GameState{
        Starting,
        Play,
        Paused,
}
[Serializable]
public struct SpawnLocations{
    public GameObject[] enemies;
    public Vector2 location;
    public int width, height;
}
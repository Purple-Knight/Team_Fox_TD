using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{
    #region Structs / Enums
    [Serializable]
    public struct WaveContent
    {
        public float startTime;
        public GameObject prefab;
        public float duration;
        public int count;
    }

    [Serializable]
    public struct Wave
    {
        public List<WaveContent> content;
    }

    private enum WaveState { Start, Spawning, Rest, Ended }
    #endregion

    #region Singleton
    private static WaveManager instance;
    public static WaveManager Instance { get { return instance; } }
    #endregion

    public UnityEvent OnEnemyKilled = new UnityEvent();

    [SerializeField] private PathController path;
    [SerializeField] private List<Wave> enemyWaves;

    private int waveIndex = 0;

    private WaveState waveState = WaveState.Start;

    private int aliveEnemyCount = 0;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(instance);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        OnEnemyKilled.AddListener(() => aliveEnemyCount--);
    }

    private void Update()
    {
        if (waveState == WaveState.Spawning) return;

        if (waveState == WaveState.Rest && aliveEnemyCount == 0)
            waveState = WaveState.Start;

        if (waveIndex >= enemyWaves.Count)
        {
            waveState = WaveState.Ended;
            return;
        }

        if(waveState == WaveState.Start)
        {
            StartCoroutine(StartSpawn());
            waveState = WaveState.Spawning;
        }
    }

    private IEnumerator StartSpawn()
    {
        for (int i = 0; i < enemyWaves[waveIndex].content.Count; ++i)
        {
            yield return new WaitForSeconds(enemyWaves[waveIndex].content[i].startTime);
            StartCoroutine(SpawnContent(i));
            yield return new WaitForSeconds(enemyWaves[waveIndex].content[i].duration);
        }

        waveIndex++;
    }

    private IEnumerator SpawnContent(int contentIndex)
    {
        float enemyCount = enemyWaves[waveIndex].content[contentIndex].count;
        float timeBetweenSpawn = enemyWaves[waveIndex].content[contentIndex].duration / (enemyCount + 1);

        for (int i = 0; i < enemyCount; ++i)
        {
            yield return new WaitForSeconds(timeBetweenSpawn);
            EnemyController enemy = Instantiate(enemyWaves[waveIndex].content[contentIndex].prefab, path.StartPosition, Quaternion.identity).GetComponent<EnemyController>();
            enemy.path = path;

            aliveEnemyCount++;
        }

        if (contentIndex + 1 == enemyWaves[waveIndex].content.Count)
            waveState = WaveState.Rest;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public Tree treePrefab;
    public GameObject spawnPosition;
    public GameObject chopTrigger;

    float spawnMargin;

    BeatData data;

    private void Start()
    {
        var beatsJson = (TextAsset)Resources.Load("beats");
        data = JsonUtility.FromJson<BeatData>(beatsJson.text);

        spawnMargin = Mathf.Abs((spawnPosition.transform.position.x - chopTrigger.transform.position.x) / treePrefab.speed.x);
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        var previousSpawnTime = 0f;

        foreach(var beat in data.beats)
        {
            var spawnTime = beat - spawnMargin;

            yield return new WaitForSeconds(spawnTime - previousSpawnTime);
            SpawnTree();
            previousSpawnTime = spawnTime;
        }
    }

    private void SpawnTree()
    {
        Instantiate(treePrefab, spawnPosition.transform.position, spawnPosition.transform.rotation);
    }

    internal void StopSpawning()
    {
        StopAllCoroutines();
    }
}

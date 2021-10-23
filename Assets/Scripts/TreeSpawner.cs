using System.Collections;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public Tree[] treePrefabs;
    public GameObject spawnPosition;
    public GameObject chopTrigger;

    public float feelGoodAdjustment;

    float spawnMargin;

    BeatData data;

    private void Start()
    {
        var beatsJson = (TextAsset)Resources.Load("beats");
        data = JsonUtility.FromJson<BeatData>(beatsJson.text);

        spawnMargin = Mathf.Abs((spawnPosition.transform.position.x - chopTrigger.transform.position.x) / treePrefabs[0].speed.x);
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
            var spawnTime = beat - spawnMargin + feelGoodAdjustment;

            yield return new WaitForSeconds(spawnTime - previousSpawnTime);
            SpawnTree();
            previousSpawnTime = spawnTime;
        }
    }

    private void SpawnTree()
    {
        var treePrefab = treePrefabs[UnityEngine.Random.Range(0, treePrefabs.Length)];
        Instantiate(treePrefab, spawnPosition.transform.position, spawnPosition.transform.rotation);
    }

    internal void StopSpawning()
    {
        StopAllCoroutines();
    }
}

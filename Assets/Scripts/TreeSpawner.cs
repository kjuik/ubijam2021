using System.Collections;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public Tree[] treePrefabs;
    public GameObject spawnPosition;
    public GameObject chopTrigger;

    float spawnMargin;

    public float treeHeightRandomAmplitude = 1f;
    public float treeSpawnAjustment = 0.3f;
    public float beatPlayAdjustment = -0.1f;

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
        StartCoroutine(PlayBeatsRoutine());
    }

    private IEnumerator PlayBeatsRoutine()
    {
        var previousBeatTime = 0f;

        foreach (var beat in data.beats)
        {
            var spawnTime = beat + beatPlayAdjustment;
            yield return new WaitForSeconds(spawnTime - previousBeatTime);

            if (GameManager.Instance.CurrentState != GameManager.State.Playing)
            {
                yield break;
            }

            SoundEffects.Instance.PlayChop();
            
            previousBeatTime = spawnTime;
        }
    }

    private IEnumerator SpawnRoutine()
    {
        var previousSpawnTime = 0f;

        foreach(var beat in data.beats)
        {
            var spawnTime = beat - spawnMargin + treeSpawnAjustment;

            yield return new WaitForSeconds(spawnTime - previousSpawnTime);
            SpawnTree();
            previousSpawnTime = spawnTime;
        }
    }

    private void SpawnTree()
    {
        var treePrefab = treePrefabs[Random.Range(0, treePrefabs.Length)];
        Instantiate(treePrefab, 
            spawnPosition.transform.position + (Random.value * treeHeightRandomAmplitude * Vector3.up), 
            spawnPosition.transform.rotation);
    }

    internal void StopSpawning()
    {
        StopAllCoroutines();
    }
}

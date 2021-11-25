using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkPlacer : MonoBehaviour
{
    public Transform player;
    public Chunk[] ChunkPrefabs;
    public Chunk firstChunk;
    public Transform parent;

    private List<Chunk> spawnedChunks = new List<Chunk>();
    // Start is called before the first frame update
    void Start()
    {
        spawnedChunks.Add(firstChunk);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.x > spawnedChunks[spawnedChunks.Count - 1].end.position.x - 15)
        {
            SpawnChunk();
        }
        if (player.position.x < spawnedChunks[0].begin.position.x + 15)
        {
            RespawnChunk();
        }
    }

    private void SpawnChunk()
    {
        Chunk newChunk = Instantiate(ChunkPrefabs[Random.Range(0, ChunkPrefabs.Length)]);
        newChunk.transform.SetParent(parent);
        newChunk.transform.position = spawnedChunks[spawnedChunks.Count - 1].end.position - newChunk.begin.localPosition;
        spawnedChunks.Add(newChunk);
        if(spawnedChunks.Count >= 5)
        {
            Destroy(spawnedChunks[0].gameObject);
            spawnedChunks.RemoveAt(0);
        }
    }

    private void RespawnChunk()
    {
        Chunk newChunk = Instantiate(ChunkPrefabs[Random.Range(0, ChunkPrefabs.Length)]);
        newChunk.transform.SetParent(parent);
        newChunk.transform.position = spawnedChunks[0].begin.position - newChunk.end.localPosition;
        spawnedChunks.Insert(0, newChunk);
        if (spawnedChunks.Count >= 5)
        {
            Destroy(spawnedChunks[spawnedChunks.Count - 1].gameObject);
            spawnedChunks.RemoveAt(spawnedChunks.Count - 1);
        }
    }
}

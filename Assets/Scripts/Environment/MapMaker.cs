using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MapMaker : MonoBehaviour {
    public List<GameObject> startChunks;
    public List<GameObject> eastChunks;
    public List<GameObject> westChunks;
    public List<GameObject> bothChunks;
    public List<GameObject> endChunks;

    public GameObject[] mapChunks;

    public List<GameObject> generatedMap;

    public NavMeshSurface surface;

    public Transform[] chunkLocations;

    public int mapLength = 5;

    void CategorizeChunks()
    {
        char connectionToken;
        for(int i = 0; i < mapChunks.Length; i++)
        {
            if(mapChunks[i].name[0] == 'X')
            {
                startChunks.Add(mapChunks[i]);
                continue;
            }
            if(mapChunks[i].name[2] == 'X')
            {
                endChunks.Add(mapChunks[i]);
                continue;
            }
            connectionToken = mapChunks[i].name[0];
            switch (connectionToken)
            {
                case 'B':
                    bothChunks.Add(mapChunks[i]);
                    break;
                case 'W':
                    westChunks.Add(mapChunks[i]);
                    break;
                case 'E':
                    eastChunks.Add(mapChunks[i]);
                    break;
                default:
                    Debug.Log("Invalid connection token for mapChunk: " + mapChunks[i].name);
                    break;
            }
        }
    }

    private void InitLists()
    {
        startChunks = new List<GameObject>();
        eastChunks = new List<GameObject>();
        westChunks = new List<GameObject>();
        bothChunks = new List<GameObject>();
        endChunks = new List<GameObject>();
    }

    private void LinkChunks(GameObject prev, GameObject next)
    {
        GameObject[] prevDoors = 

        DoorManager prevDM = prev.GetComponent<DoorManager>();
        DoorManager nextDM = next.GetComponent<DoorManager>();


    }

    private void AppendChunk(GameObject nextChunk)
    {
        if (generatedMap.Count > 1)
        {

        }
        generatedMap.Add(nextChunk);
    }

    public void GenerateMap(){
        InitLists();
        CategorizeChunks();

        int index;
        char connectionToken;
        GameObject nextChunk;
        //start
        index = Random.Range(0,startChunks.Count);
        nextChunk = startChunks[index];
        generatedMap.Add(nextChunk);
        connectionToken = nextChunk.name[2];

        //middle
        for(int i = 1; i < mapLength - 1; i++){
            switch(connectionToken){
                case 'B':
                    index = Random.Range(0,bothChunks.Count);
                    nextChunk = bothChunks[index];
                    generatedMap.Add(nextChunk);                    
                    connectionToken = nextChunk.name[2];
                    break;
                case 'E':
                    index = Random.Range(0,eastChunks.Count);
                    nextChunk = eastChunks[index];
                    generatedMap.Add(nextChunk);
                    connectionToken = nextChunk.name[2];
                    break;
                case 'W':
                    index = Random.Range(0,westChunks.Count);
                    nextChunk = westChunks[index];
                    generatedMap.Add(nextChunk);
                    connectionToken = nextChunk.name[2];
                    break;
            }
        }

        //end
        for(int i = 0; i < endChunks.Count; i++){
            if(endChunks[i].name[0] == connectionToken){
                generatedMap.Add(endChunks[i]);
                break;
            }
        }

        //Instantiate chunks
        for(int i = 0; i < mapLength; i++){
            Instantiate(generatedMap[i], chunkLocations[i]);
        }

        //Build NavMesh for AI
        surface.BuildNavMesh();
    }	
}

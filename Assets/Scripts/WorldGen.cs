using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGen : MonoBehaviour
{

    public GameObject blocktospawn;
    public GameObject player;
    private int worldsizeX = 15;    // x area around player
    private int worldsizeZ = 18;    // z area around player
    public float NoiseHeight;      // Max height
    private float gridoffset = 1f; // dist between base blocks

    private float blocksize;

    private Vector3 startPosition; //To see how much player has moved

    private Hashtable BlockStorage = new Hashtable();



    public List<GameObject> planets = new List<GameObject>();        // to store all Planet GameObjects
    private List<Vector3> blockPositions = new List<Vector3>();     // to store base Block Positions

    void Start()
    {
        blocksize = (int)blocktospawn.transform.localScale.x; //size of block
        
        //SpawnPlanet();
        
    }

    void Update()
    {
        if(Mathf.Abs(xPlayerMove)>=blocksize || Mathf.Abs(zPlayerMove) >= blocksize)
        {
            for (int x = -worldsizeX; x < worldsizeX; x++)
            {
                for (int z = -worldsizeZ; z < worldsizeZ; z++)
                {
                    float y = generateNoise(x + xPlayerLocation  , z+ zPlayerLocation, 8f);

                    Vector3 pos = new Vector3(
                        x * blocksize + xPlayerLocation,
                        y * NoiseHeight, 
                        z * blocksize + zPlayerLocation); //positions where the blocks are to be spawned around the player
                    
                    if (!BlockStorage.ContainsKey(pos)) //checking if blockobject already exists in hashmap
                    {
                        GameObject block = Instantiate(blocktospawn, pos, Quaternion.identity) as GameObject; //spawning blocks and placing them on sais positions

                        BlockStorage.Add(pos, block); //storing pos and block to hashmap

                        blockPositions.Add(block.transform.position); //adding positions to list

                        block.transform.SetParent(this.transform);
                    }
                }
                
            }
            //SpawnPlanet();
        }
        
    }

    public int xPlayerMove
    {
        get
        {
            return (int)(player.transform.position.x - startPosition.x);
        }
    }

    public int zPlayerMove
    {
        get
        {
            return (int)(player.transform.position.z - startPosition.z);
        }
    }

    public void SpawnPlanet()
    {
        int rmax = Random.Range(20, 50);

        for(int c = 0; c < rmax; c++)
        {
            int rplan = Random.Range(0, planets.Count);
            GameObject planet = Instantiate(planets[rplan], PlanetSpawnLocation(), Quaternion.identity);

        }
    } 

    private int xPlayerLocation
    {
        get
        {
            return (int)Mathf.Floor(player.transform.position.x);
        }
    }

    private int zPlayerLocation
    {
        get
        {
            return (int)Mathf.Floor(player.transform.position.z);
        }
    }

    private Vector3 PlanetSpawnLocation()
    {
        int randomind = Random.Range(0, blockPositions.Count);

            Vector3 newPos = new Vector3(
                blockPositions[randomind].x,
                blockPositions[randomind].y + 0.5f,
                blockPositions[randomind].z);
        blockPositions.RemoveAt(randomind);
        return newPos;
    }

    private float generateNoise(int x, int z, float detailScale)
    {
        float xNoise = (x + this.transform.position.x) / detailScale;
        float zNoise = (z + this.transform.position.y) / detailScale;

        return Mathf.PerlinNoise(xNoise, zNoise);
    }
} 

    

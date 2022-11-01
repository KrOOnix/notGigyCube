using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Transform[] tilePrefabs;
    public Transform obstaclePrefab;

    public Vector2 mapSize;
    List<Coord> allTileCoords;
    Queue<Coord> shuffledlTileCoords;
    public int seed = 10;
    public float obstaclePercent = 0.5f;
    Coord mapCenter;
    public List<Vector3> canSpawnTiles;
    public GameObject player;
    public GameObject movePoint;
    public GameManagement gm;
    

    private void Start()
    {
        GenerateMap();
        SpawnPlayer();
    }

    public void GenerateMap()
    {

        allTileCoords = new List<Coord>();


        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                allTileCoords.Add(new Coord(x, y));
            }

        }
        shuffledlTileCoords = new Queue<Coord>(Utility.ShuffleArray(allTileCoords.ToArray(),seed));
        mapCenter = new Coord((int)mapSize.x / 2, (int)mapSize.y / 2);

        for (int x = 0; x < mapSize.x; x++)
        {
            for(int y = 0; y < mapSize.y; y++)
            {
                int i = Random.Range(0, tilePrefabs.Length);
           
                Vector3 tilePosition = CoordToPosition(x, y);
                Transform newTile = Instantiate(tilePrefabs[i],tilePosition,Quaternion.identity) as Transform;
                canSpawnTiles.Add(new Vector3(newTile.position.x, 0.5f, newTile.position.z));

            }
        }

        bool[,] obstacleMap = new bool[(int)mapSize.x, (int)mapSize.y];

        int obstacleCount = (int)(mapSize.x*mapSize.y*obstaclePercent);
        int currentObstacleCount = 0;
        for (int i = 0; i < obstacleCount; i++)
        {
            Coord randomCoord = GetRandomCoord();
            obstacleMap[randomCoord.x,randomCoord.y] = true;
            currentObstacleCount++;
            if (randomCoord != mapCenter && MapIsFullyAccessible(obstacleMap, currentObstacleCount))
            {

                Vector3 obstaclePosition = CoordToPosition(randomCoord.x, randomCoord.y);
                Transform newObstacle = Instantiate(obstaclePrefab, obstaclePosition, Quaternion.identity) as Transform;
                


            }
            else
            {
                obstacleMap[randomCoord.x, randomCoord.y] = false;
                currentObstacleCount--;
               
            }

            
        }

       

    }

    public Vector3 CoordToPosition(int x , int y)
    {
        return new Vector3 (-mapSize.x / 2 + 0.5f + x, 0, -mapSize.y / 2 + 0.5f + y);
    }

    bool MapIsFullyAccessible(bool[,] obstacleMap,int currentObstacleCount)
    {
        bool[,] mapFlags = new bool [obstacleMap.GetLength(0),obstacleMap.GetLength(1)];
        Queue<Coord> queue = new Queue<Coord>();
        queue.Enqueue(mapCenter);
        mapFlags[mapCenter.x, mapCenter.y] = true;

        int accessableTileCount = 1;

        while(queue.Count > 0)
        {
            Coord tile = queue.Dequeue();

            
            for(int x = -1; x<=1;x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    int neighbourX = tile.x + x;
                    int neighbourY = tile.y + y;
                    if(x ==0 || y==0)
                    {
                        if (neighbourX >= 0 && neighbourX < obstacleMap.GetLength(0) && neighbourY >= 0 && neighbourY < obstacleMap.GetLength(1))
                        {
                            if(!mapFlags[neighbourX,neighbourY] && !obstacleMap[neighbourX,neighbourY])
                            {
                                mapFlags[neighbourX,neighbourY] = true;
                                queue.Enqueue(new Coord(neighbourX,neighbourY));
                                accessableTileCount++;
                            }
                        }
                    }
                }
            }
        }


        int targetAccessibleTileCount = (int)(mapSize.x * mapSize.y - currentObstacleCount);
        return targetAccessibleTileCount == accessableTileCount; 
    }


    public Coord GetRandomCoord()
    {
        Coord randomCoord = shuffledlTileCoords.Dequeue();
        shuffledlTileCoords.Enqueue(randomCoord);
        return randomCoord;
    }

    public struct Coord
    {
        public int x;
        public int y;

        public Coord(int _x,int _y)
        {
            x= _x;
            y= _y;
        }

        public static bool operator == (Coord c1,Coord c2)
        {
            return c1.x == c2.x && c1.y == c2.y;
        }

        public static bool operator !=(Coord c1, Coord c2)
        {
            return c1.x != c2.x && c1.y != c2.y;
        }
    }

    void SpawnPlayer()
    {
        int i = Random.Range(0, canSpawnTiles.Count);

        Instantiate(movePoint, canSpawnTiles[i], Quaternion.identity);
        Instantiate(player, canSpawnTiles[i], Quaternion.identity);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomGenerator : MonoBehaviour
{
    [SerializeField] private Tilemap groundMap, wallMap;
    [SerializeField] private TileBase ground, wall;
    [SerializeField] private GameObject cameraSwitchPrefab;
    [SerializeField] private GameObject startGrid;

    private bool[,] roomGrid;
    private Vector3 lastPosition;
    private Vector2Int lastCoord;

    private int builtRooms = 0;

    const int EMPTY = 0;
    const int GROUND = 1;
    const int WALL = 2;

    const int ROOMSIZE = 16;

    void Start()
    {
        builtRooms = 0;
        lastPosition = Vector3.zero;

        roomGrid = new bool[100, 100];
        lastCoord = new Vector2Int(50, 50);
    }

    public void BuildRoom(RoomCard card)
    {


        // BUILD BASIC ROOM
        int[,] map = new int[ROOMSIZE, ROOMSIZE];
        //Generate tiles
        for (int y = 0; y < map.GetLength(1); y++)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                //TEMP BASIC ROOM
                if (x != 0 && y != 0
                    && (x != map.GetLength(0) - 1) && (y != map.GetLength(0) - 1)) map[x, y] = GROUND;
                else map[x, y] = WALL;
            }
        }

        //FIGURE OUT WHERE THE ROOM SHOULD BE PLACED
        Vector3 position = Vector3.zero;

        //IF FIRST ROOM
        if (builtRooms == 0)
        {
            startGrid.SetActive(false);
            position = new Vector3((int)(-ROOMSIZE * 0.5f), (int)-(ROOMSIZE * 0.5f), 0);
            lastPosition = position;
            builtRooms++;

            roomGrid[lastCoord.x, lastCoord.y] = true;

        } else
        {
            int direction = Random.Range(0, 3);

            // Try to find spot to place
            while (true)
            {
                if (direction == 0)
                {
                    //Continue if occupied
                    if (roomGrid[lastCoord.x,lastCoord.y+1] == true)
                    {
                        direction += 1;
                        continue;
                    }

                    //BUILD UP
                    position = new Vector3(lastPosition.x, lastPosition.y + ROOMSIZE - 1, 0);
                    // MAKE DOOR 
                    map[7, 0] = GROUND;
                    map[8, 0] = GROUND;

                    lastCoord.y += 1;
                    break;
                }
                else if (direction == 1)
                {
                    //Continue if occupied
                    if (roomGrid[lastCoord.x, lastCoord.y-1] == true)
                    {
                        direction += 1;
                        continue;
                    }

                    //BUILD DOWN
                    position = new Vector3(lastPosition.x, lastPosition.y - ROOMSIZE + 1, 0);
                    // MAKE DOOR 
                    map[7, ROOMSIZE - 1] = GROUND;
                    map[8, ROOMSIZE - 1] = GROUND;

                    lastCoord.y -= 1;
                    break;
                }
                else if (direction == 2)
                {
                    //Continue if occupied
                    if (roomGrid[lastCoord.x-1, lastCoord.y] == true)
                    {
                        direction += 1;
                        continue;
                    }

                    //BUILD LEFT
                    position = new Vector3(lastPosition.x - ROOMSIZE + 1, lastPosition.y, 0);
                    // MAKE DOOR 
                    map[ROOMSIZE - 1, 7] = GROUND;
                    map[ROOMSIZE - 1, 8] = GROUND;

                    lastCoord.x -= 1;
                    break;
                }
                else if (direction == 3)
                {
                    //Continue if occupied
                    if (roomGrid[lastCoord.x+1, lastCoord.y] == true)
                    {
                        direction = 0;
                        continue;
                    }

                    //BUILD RIGHT
                    position = new Vector3(lastPosition.x + ROOMSIZE - 1, lastPosition.y, 0);
                    // MAKE DOOR 
                    map[0, 7] = GROUND;
                    map[0, 8] = GROUND;

                    lastCoord.x += 1;
                    break;
                }
            }

            roomGrid[lastCoord.x, lastCoord.y] = true;
            builtRooms++;
            lastPosition = position;
        }



        //PLACE TILES
        for (int y = 0; y < map.GetLength(1); y++)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                if (map[x, y] == GROUND)
                {
                    wallMap.SetTile(new Vector3Int((int)position.x + x, (int)position.y + y, 0), null);
                    groundMap.SetTile(new Vector3Int((int)position.x + x, (int)position.y + y, 0), ground);
                }
                else if (map[x, y] == WALL)
                {
                    wallMap.SetTile(new Vector3Int((int)position.x + x, (int)position.y + y, 0), wall);
                    groundMap.SetTile(new Vector3Int((int)position.x + x, (int)position.y + y, 0), null);
                }
            }
        }

        //Place Camera trigger
        var room = Instantiate(cameraSwitchPrefab, new Vector3(position.x + ROOMSIZE * 0.5f, position.y + ROOMSIZE * 0.5f, 0), transform.rotation);
        room.GetComponent<CameraSwitch>().SetData(card);
    }










    //========================================= ROOM GENERATION ======================================

    //Check if any neigbors are ground
    private bool ShouldBeWall(int x, int y, int[,] map)
    {
        if (map[x, y] != EMPTY) return false; //Not wall if not empty

        if (IsTileGround(x+1, y, map)) return true;
        if (IsTileGround(x, y+1, map)) return true;
        if (IsTileGround(x+1, y+1, map)) return true;
        if (IsTileGround(x, y-1, map)) return true;
        if (IsTileGround(x-1, y, map)) return true;
        if (IsTileGround(x-1, y-1, map)) return true;
        if (IsTileGround(x+1, y-1, map)) return true;
        if (IsTileGround(x-1, y+1, map)) return true;

        return false;
    }

    private bool IsTileGround(int x, int y, int[,] map)
    {
        // Return false if outside bounds
        if (x < 0 || x >= map.GetLength(0) || y < 0 || y >= map.GetLength(1)) return false;
        else if (map[x, y] == GROUND) return true;
        else return false;

    }

}

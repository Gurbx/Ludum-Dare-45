using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomGenerator : MonoBehaviour
{
    [SerializeField] private Tilemap groundMap, wallMap;
    [SerializeField] private TileBase ground, wall;

    [SerializeField] private GameObject camTarget1, camTarget2;

    private bool[,] roomGrid;
    private Vector3 lastPosition;

    private int builtRooms = 0;

    const int EMPTY = 0;
    const int GROUND = 1;
    const int WALL = 2;

    const int ROOMSIZE = 16;

    void Start()
    {
        builtRooms = 0;
        lastPosition = Vector3.zero;
    }

    public void BuildRoom(RoomCard card)
    {
        //FIGURE OUT WHERE THE ROOM SHOULD BE BUILT
        Vector3 position = Vector3.zero;

        if (builtRooms == 0)
        {
            position = new Vector3((int)(-ROOMSIZE * 0.5f), (int)-(ROOMSIZE * 0.5f), 0);
            lastPosition = position;
            builtRooms++;
        } else
        {
            position = new Vector3(lastPosition.x, lastPosition.y + ROOMSIZE-1, 0);
            lastPosition = position;
            camTarget2.transform.position = new Vector3(position.x + ROOMSIZE*0.5f, position.y + ROOMSIZE*0.5f, 0);
        }

        int[,] map = new int[ROOMSIZE, ROOMSIZE];
        //Generate tiles
        for (int y = 0; y < map.GetLength(1); y++)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                //TEMP BASIC ROOM
                if (x != 0 && y != 0
                    && (x != map.GetLength(0)-1) && (y != map.GetLength(0)-1)) map[x, y] = GROUND;
                else map[x, y] = WALL;
            }
        }


        //PLACE TILES
        for (int y = 0; y < map.GetLength(1); y++)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                if (map[x, y] == GROUND) groundMap.SetTile(new Vector3Int((int)position.x + x, (int)position.y + y, 0), ground);
                else if (map[x, y] == WALL) wallMap.SetTile(new Vector3Int((int)position.x + x, (int)position.y + y, 0), wall);
            }
        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Room Card", menuName = "Custom Assets/Room Card")]
public class RoomCard : ScriptableObject
{
 //   public Vector2Int minSize, maxSize;
    public int minLevel, maxLevel;
    public RoomType type;
    public TileBase groundTile, wallTile;


    [System.Serializable]
    public enum RoomType
    {
        COMBAT,
        LOOT,
        SHOP,
        BOSS
    };
}

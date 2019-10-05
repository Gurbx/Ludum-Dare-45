using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private RoomCardPickup roomCardPickupPrefab;
    [SerializeField] private ItemPickup lootPrefab;

    private RoomCard roomCard;

    private bool isActiveCamera;

    private bool isCombatRoom, enemiesSpawned;
    private int combatRoomLevel;
    private GameObject combatEvent;

    private void Awake()
    {
        isCombatRoom = false;
        enemiesSpawned = false;
        combatRoomLevel = 0;
        isActiveCamera = false;
    }

    public void SetData(RoomCard roomCard)
    {
        this.roomCard = roomCard;
        if (roomCard.type == RoomCard.RoomType.LOOT)
        {
            SpawnLoot();
        }
        else if (roomCard.type == RoomCard.RoomType.COMBAT)
        {
            Debug.Log("IS COMABAT");
            isCombatRoom = true;
            combatEvent = roomCard.enemies;
            //combatRoomLevel = roomCard.
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isCombatRoom && !enemiesSpawned)
            {
                Invoke("SpawnEnemies", 2f);
            }

            if (!isActiveCamera)
            {
                //Make transition and swithc active cam

                //If cam1 is active
                if (GameHandler.GetGameHandler().activeCam == GameHandler.GetGameHandler().cam1)
                {
                    GameHandler.GetGameHandler().cam2.Follow = transform;
                    GameHandler.GetGameHandler().activeCam = GameHandler.GetGameHandler().cam2;
                    GameHandler.GetGameHandler().cam1.Priority = 0;
                    GameHandler.GetGameHandler().cam2.Priority = 15;
                // Cam 2 active
                } else if (GameHandler.GetGameHandler().activeCam == GameHandler.GetGameHandler().cam2)
                {
                    GameHandler.GetGameHandler().cam1.Follow = transform;
                    GameHandler.GetGameHandler().activeCam = GameHandler.GetGameHandler().cam1;
                    GameHandler.GetGameHandler().cam2.Priority = 0;
                    GameHandler.GetGameHandler().cam1.Priority = 15;
                }
            }
        }
    }



    //ROOM CARD PICKUP FUNCTONS
    private void SpawnRoomCardPickup()
    {
        var pickup = Instantiate(roomCardPickupPrefab, transform.position, transform.rotation);
    }


    // LOOT FUNCTIONS
    private GameObject spawnedLoot;
    private void SpawnLoot()
    {
        var loot = Instantiate(lootPrefab, transform.position, transform.rotation);
        spawnedLoot = loot.gameObject;
        InvokeRepeating("CheckIfLootIsPickedUp", 1f, 1f);
    }
    private void CheckIfLootIsPickedUp()
    {
        if (spawnedLoot == null)
        {
            CancelInvoke();
            Invoke("SpawnRoomCardPickup", 4f);
        }
    }

    //COMBAT ROOM
    private void SpawnEnemies()
    {
        enemiesSpawned = true;
        Debug.Log("ENEMIES SPAWNED");
        var en = Instantiate(combatEvent, transform.position, transform.rotation);
    }
}

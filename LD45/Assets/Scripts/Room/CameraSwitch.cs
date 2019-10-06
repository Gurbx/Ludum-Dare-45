using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private GameObject roomCardPickupPrefab;
    [SerializeField] private Light2D light;
    [SerializeField] private AudioSource spawnSound;
   // [SerializeField] private ItemPickup lootPrefab;

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
            SpawnRoomCardPickup();
        }
        else if (roomCard.type == RoomCard.RoomType.COMBAT)
        {
            Debug.Log("IS COMABAT");
            isCombatRoom = true;
            combatEvent = roomCard.enemies;
            //combatRoomLevel = roomCard.
        }

        light.color = roomCard.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isCombatRoom && !enemiesSpawned)
            {
                Invoke("SpawnEnemies", 2f);
                Invoke("SpawnSound", 5f);
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
 //   private GameObject spawnedLoot;
    private void SpawnLoot()
    {
        var loot = Instantiate(roomCard.loot, transform.position, transform.rotation);
   //     spawnedLoot = loot.gameObject;
        //InvokeRepeating("CheckIfLootIsPickedUp", 1f, 1f);
    }
    /*
    private void CheckIfLootIsPickedUp()
    {
        if (spawnedLoot == null)
        {
            CancelInvoke();
            Invoke("SpawnRoomCardPickup", 4f);
        }
    }
    */

    //COMBAT ROOM
    private GameObject spawnedCombatEvent;
    private void SpawnEnemies()
    {
        enemiesSpawned = true;
        Debug.Log("ENEMIES SPAWNED");
        var en = Instantiate(combatEvent, transform.position, transform.rotation);
        spawnedCombatEvent = en.gameObject;
        InvokeRepeating("CheckCombaEventStatus", 1f, 1f);
    }

    private void SpawnSound()
    {
        spawnSound.Play();
    }

    private void CheckCombaEventStatus()
    {
        if (spawnedCombatEvent.GetComponent<CombatEvent>().IsEventComplete())
        {
            CancelInvoke();
            Invoke("SpawnRoomCardPickup", 1f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCardPickup : MonoBehaviour
{
    //[SerializeField] private RoomCard roomCard;

    private CardSelectHandler cardSelectHandler;

    // Start is called before the first frame update
    void Start()
    {
        cardSelectHandler = GameObject.Find("UI/Card Select Menu").GetComponent<CardSelectHandler>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            cardSelectHandler.CardCollected();

            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private PickupType type;
    [SerializeField] private GameObject pickupEffect;

    public int value;

    [System.Serializable]
    public enum PickupType
    {
        DAMAGE,
        HEALTH,
        CURRENCY,
        CARDS
    };

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == false) return;

        switch(type)
        {
            case PickupType.DAMAGE:
                collision.gameObject.GetComponent<PlayerShootingHandler>().IncreasePowerLevel(10);
                break;

            case PickupType.HEALTH:
                break;

            case PickupType.CARDS:
                GameObject.Find("UI/Card Select Menu").GetComponent<CardSelectHandler>().CardCollected();
                break;

            case PickupType.CURRENCY:
                GameHandler.GetGameHandler().AddCurrency(value);
                break;

        }


        var effect = Instantiate(pickupEffect, transform.position, transform.rotation);
        Destroy(effect, 10f);
        Destroy(gameObject);
    }


}

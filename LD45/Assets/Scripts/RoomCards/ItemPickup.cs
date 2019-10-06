using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private PickupType type;

    [System.Serializable]
    public enum PickupType
    {
        DAMAGE,
        HEALTH
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

        }

        Destroy(gameObject);
    }


}

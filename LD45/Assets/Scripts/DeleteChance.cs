using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteChance : MonoBehaviour
{
    [SerializeField] private float chance;

    void Awake()
    {
        if (Random.Range(0, 100) <= chance*100)
        {
            Destroy(gameObject);
        }
        
    }

}

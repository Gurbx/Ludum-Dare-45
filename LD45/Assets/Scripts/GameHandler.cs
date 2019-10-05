using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private List<RoomCard> roomCards;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        
    }
}

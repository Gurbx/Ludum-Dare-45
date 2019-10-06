using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameHandler : MonoBehaviour
{
    private static GameHandler instance;

    public int roomCurrency;

    public CinemachineVirtualCamera cam1, cam2;
    [HideInInspector] public CinemachineVirtualCamera activeCam;

    void Awake()
    {
        if (instance == null)
        {
            roomCurrency = 0;
            activeCam = cam1;
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }

    }


    public static GameHandler GetGameHandler()
    {
        return instance;
    }
}

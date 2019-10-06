using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class GameHandler : MonoBehaviour
{
    private static GameHandler instance;

    private int roomCurrency;
    [SerializeField] private Text currencyText;

    public CinemachineVirtualCamera cam1, cam2;
    [HideInInspector] public CinemachineVirtualCamera activeCam;

    void Awake()
    {
        if (instance != null) Destroy(instance.gameObject);

        roomCurrency = 0;
        activeCam = cam1;
        instance = this;


    }


    public static GameHandler GetGameHandler()
    {
        return instance;
    }

    public void AddCurrency(int amount)
    {
        roomCurrency += amount;
        currencyText.text = roomCurrency.ToString();
    }

    public void RemoveCurrency(int amount)
    {
        roomCurrency -= amount;
        currencyText.text = roomCurrency.ToString();
    }

    public int GetRoomCurrency() { return roomCurrency; }
}

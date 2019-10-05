using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RoomTransitionHandler : MonoBehaviour
{
    [SerializeField] RoomGenerator roomGenerator;
    [SerializeField] CinemachineVirtualCamera cam1, cam2;
    [SerializeField] Animator flasher;
    [SerializeField] GameObject flashImage;

    private CinemachineVirtualCamera activeCam;

    private RoomCard lastRoomCard;

    private void Start()
    {
        flashImage.SetActive(true);
        activeCam = cam1;
    }

    public void TransitionToRoom(RoomCard roomCard)
    {
        lastRoomCard = roomCard;

        flasher.SetTrigger("Flash");
        Invoke("BuildRoom", 0.1f);
    }

    private void BuildRoom()
    {
        roomGenerator.BuildRoom(lastRoomCard);
    }
}

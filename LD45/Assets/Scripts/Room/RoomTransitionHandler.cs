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


    public void TransitionToRoom(RoomCard roomCard)
    {
        roomGenerator.BuildRoom(roomCard);
    }

    private void Start()
    {
        flashImage.SetActive(true);
        activeCam = cam1;
    }

    private void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            flasher.SetTrigger("Flash");
        }
    }

    public void TransitionRooms()
    {
        if (activeCam == cam1)
        {
            cam1.Priority = 0;
            cam2.Priority = 15;
            activeCam = cam2;
        } else
        {
            cam2.Priority = 15;
            cam1.Priority = 0;
            activeCam = cam1;
        }

    }
}

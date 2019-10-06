﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject player;

    [SerializeField] private CinemachineVirtualCamera menuCam;
    [SerializeField] private CinemachineBrain cBrain;
    [SerializeField] private Animator anmator;

    public void MainMenuButtonPressed()
    {
        cBrain.m_DefaultBlend.m_Time = 5f;
        menuCam.Priority = -50;
        anmator.SetTrigger("playPressed");

        Invoke("ActivateUI", 5f);
        player.SetActive(true);
    }

    private void ActivateUI()
    {
        cBrain.m_DefaultBlend.m_Time = 1f;
        UI.SetActive(true);


    }
}
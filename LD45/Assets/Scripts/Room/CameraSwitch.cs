using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    private bool isActiveCamera;

    private void Start()
    {
        isActiveCamera = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isActiveCamera)
            {
                //Make transition and swithc active cam

                //If cam1 is active
                if (GameHandler.GetGameHandler().activeCam == GameHandler.GetGameHandler().cam1)
                {
                    GameHandler.GetGameHandler().cam2.Follow = transform;
                    GameHandler.GetGameHandler().activeCam = GameHandler.GetGameHandler().cam2;
                    GameHandler.GetGameHandler().cam1.Priority = 0;
                    GameHandler.GetGameHandler().cam2.Priority = 15;
                // Cam 2 active
                } else if (GameHandler.GetGameHandler().activeCam == GameHandler.GetGameHandler().cam2)
                {
                    GameHandler.GetGameHandler().cam1.Follow = transform;
                    GameHandler.GetGameHandler().activeCam = GameHandler.GetGameHandler().cam1;
                    GameHandler.GetGameHandler().cam2.Priority = 0;
                    GameHandler.GetGameHandler().cam1.Priority = 15;
                }
            }
        }
    }
}

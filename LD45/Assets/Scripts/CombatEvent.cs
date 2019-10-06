using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEvent : MonoBehaviour
{
    [SerializeField] private GameObject spawnEffectPrefab, spawnExplosionPrefab;

    private bool eventHasStarted = false;


    private void Start()
    {
        foreach (Transform child in transform)
        {
            var spawnEffect = Instantiate(spawnEffectPrefab, child.position, child.rotation);
            Destroy(spawnEffect, 8f);
            child.gameObject.SetActive(false);
        }

        Invoke("ActivateEnemeies", 3f);
    }

    private void ActivateEnemeies()
    {
        eventHasStarted = true;

        GameHandler.AddSceenShake(7, 7, 0.4f);

        foreach (Transform child in transform)
        {
            var spawnEffect = Instantiate(spawnExplosionPrefab, child.position, child.rotation);
            Destroy(spawnEffect, 5f);
            child.gameObject.SetActive(true);
        }
    }

    public bool IsEventComplete()
    {
        if (transform.childCount <= 0 && eventHasStarted) return true;
        else return false;
    }


}

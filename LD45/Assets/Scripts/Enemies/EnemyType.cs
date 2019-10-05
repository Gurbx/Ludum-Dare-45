using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Enemy", menuName = "Custom Assets/Enemy")]
public class EnemyType : ScriptableObject
{
    public Color color;
    public int health;
    public float movementSpeed;

    public GameObject bullet;
    public Behaviour behaviour;


    [System.Serializable]
    public enum Behaviour
    {
        CHASE,
        KEEP_DISTANCE
    };
}

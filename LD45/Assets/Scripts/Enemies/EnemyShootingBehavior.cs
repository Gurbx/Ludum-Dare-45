using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingBehavior : MonoBehaviour
{ 
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float cooldown;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private AudioSource shootAudio;

    [SerializeField] private bool volley;

    private float timer;


    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            if (!volley) Shoot();
            else ShootVolley();
            timer = cooldown + Random.Range(-0.2f, 0.2f);
            shootAudio.pitch = Random.Range(0.8f, 1.2f);
            shootAudio.Play();
        }
    }


    private void Shoot()
    {
        Vector3 direction = player.transform.position - transform.position;

        direction.Normalize();

        var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        
        bullet.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed);
    }

    private void ShootVolley()
    {
        for (int i = 0; i < 18; i++)
        {
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0f, 0f, i * 20));
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * bulletSpeed);
        }
    }
}

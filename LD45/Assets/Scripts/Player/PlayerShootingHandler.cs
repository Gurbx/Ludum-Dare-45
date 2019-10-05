using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingHandler : MonoBehaviour
{
    [SerializeField] private Color color;
    [SerializeField] private float bulletSpeed;

    [SerializeField] private PlayerBullet bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("left")) Shoot(Vector3.left);
        if (Input.GetKeyDown("right")) Shoot(Vector3.right);
        if (Input.GetKeyDown("up")) Shoot(Vector3.up);
        if (Input.GetKeyDown("down")) Shoot(Vector3.down);
    }

    private void Shoot(Vector3 direction)
    {
        var bullet = Instantiate(bulletPrefab, transform.position + direction*0.5f, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed);
        bullet.Initialize(color);

    }
}

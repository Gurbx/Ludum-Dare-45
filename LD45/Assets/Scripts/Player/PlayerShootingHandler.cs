using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class PlayerShootingHandler : MonoBehaviour
{
    [SerializeField] private Color color;
    [SerializeField] private float bulletSpeed;

    [SerializeField] private PlayerBullet bulletPrefab;

    [SerializeField] private GameObject playerSprite;
    [SerializeField] private Light2D light;
    

    private int powerLevel;

    // Start is called before the first frame update
    void Start()
    {
        powerLevel = 0;
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
        if (powerLevel <= 0) return;
        var bullet = Instantiate(bulletPrefab, transform.position + direction*0.5f, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed);
        bullet.Initialize(color, powerLevel);

        powerLevel--;
        PowerLevelChanged();

    }

    public void IncreasePowerLevel(int amount)
    {
        powerLevel += amount;
        PowerLevelChanged();
    }

    public void DecreasePowerLevel(int amount)
    {
        powerLevel--;
        PowerLevelChanged();
    }

    private void PowerLevelChanged()
    {
        playerSprite.transform.localScale = new Vector3(0.05f*powerLevel, 0.05f*powerLevel, 1);
        light.gameObject.transform.localScale = new Vector3(0.05f * powerLevel, 0.05f * powerLevel, 1);
        light.intensity = powerLevel/75f; 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.SceneManagement;

public class PlayerShootingHandler : MonoBehaviour
{
    [SerializeField] private Color color;
    [SerializeField] private float bulletSpeed;

    [SerializeField] private PlayerBullet bulletPrefab;

    [SerializeField] private GameObject playerSprite;

    [SerializeField] private GameObject deathEffect, hitEffectPrefab;

    [SerializeField] private Light2D light;

    [SerializeField] private Slider energyBar;

    private float powerPercentage;
    private int powerLevel;
    private const int MAX_POWER_LEVEL = 50;

    // Start is called before the first frame update
    void Start()
    {
        powerLevel = 0;
        PowerLevelChanged();
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
        bullet.GetComponent<Rigidbody2D>().AddForce(direction * (bulletSpeed * (1-powerPercentage)+25));
        bullet.Initialize(color, (int)(10f*powerPercentage)+1);

        //powerLevel--;
        DecreasePowerLevel(1, false);
        //PowerLevelChanged();

    }

    public void IncreasePowerLevel(int amount)
    {
        powerLevel += amount;
        if (powerLevel > MAX_POWER_LEVEL) powerLevel = MAX_POWER_LEVEL;
        PowerLevelChanged();
    }

    public void DecreasePowerLevel(int amount, bool hitEffect)
    {
        powerLevel--;
        if (powerLevel <= 0)
        {
            powerLevel = 0;
            playerSprite.SetActive(false);
            Instantiate(deathEffect, transform.position, transform.rotation);
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            gameObject.layer = 2;
            Invoke("GameOverScreen", 2f);
        }
        else if (hitEffect)
        {
            var effect = Instantiate(hitEffectPrefab, transform.position, transform.rotation);
            Destroy(effect, 4f);
        }
        PowerLevelChanged();
    }

    private void PowerLevelChanged()
    {
        powerPercentage = (float)powerLevel / MAX_POWER_LEVEL;

        playerSprite.transform.localScale = new Vector3(powerPercentage * 0.7f, powerPercentage * 0.7f, 1);
        light.gameObject.transform.localScale = new Vector3(powerPercentage * 5f, powerPercentage * 5f, 1);
        light.intensity = powerPercentage + 0.65f;

        energyBar.value = powerPercentage;
    }

    public float GetPowerPercentage() { return powerPercentage; }

    private void GameOverScreen()
    {
        SceneManager.LoadScene(0);
    }
}

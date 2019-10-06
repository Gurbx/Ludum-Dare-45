using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.SceneManagement;

public class PlayerShootingHandler : MonoBehaviour
{
    private int damage = 5;
    private bool isDead;

    [SerializeField] private Color color;
    [SerializeField] private float bulletSpeed;

    [SerializeField] private PlayerBullet bulletPrefab;

    [SerializeField] private GameObject playerSprite;

    [SerializeField] private GameObject deathEffect, hitEffectPrefab;

    [SerializeField] private Light2D light;

    [SerializeField] private Slider energyBar;

    private float powerPercentage;
    private int powerLevel;
    private int MAX_POWER_LEVEL = 35;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        powerLevel = 0;
        PowerLevelChanged();
        GameHandler.GetGameHandler().UpdateDamageText(damage);
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
        bullet.GetComponent<Rigidbody2D>().AddForce(direction * (bulletSpeed * (1-powerPercentage)+100));
        bullet.Initialize(color, damage);

        //powerLevel--;
        if (powerLevel > 1) DecreasePowerLevel(1, false);
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
        if (isDead) return;

        powerLevel--;
        if (powerLevel <= 0)
        {
            powerLevel = 0;
            playerSprite.SetActive(false);
            Instantiate(deathEffect, transform.position, transform.rotation);
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            Destroy(gameObject.GetComponent<CircleCollider2D>());
            gameObject.layer = 2;
            Invoke("GameOverScreen", 2f);
            isDead = true;
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
        light.gameObject.transform.localScale = new Vector3(powerPercentage * 1.5f, powerPercentage * 1.5f, 1);
        light.intensity = 0.8f * powerPercentage;

        GameHandler.GetGameHandler().UpdateEnergyText(powerLevel, MAX_POWER_LEVEL);
        energyBar.value = powerPercentage;
    }

    public float GetPowerPercentage() { return powerPercentage; }

    private void GameOverScreen()
    {
        SceneManager.LoadScene(0);
    }

    public void IncreaseDamage(int amount)
    {
        damage += amount;
        GameHandler.GetGameHandler().UpdateDamageText(damage);
    }

    public void IncreaseEnergyCap(int amount)
    {
        MAX_POWER_LEVEL += amount;
        PowerLevelChanged();
    }
}

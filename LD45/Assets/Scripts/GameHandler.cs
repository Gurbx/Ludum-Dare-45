using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class GameHandler : MonoBehaviour
{
    private static GameHandler instance;

    private int roomCurrency;
    [SerializeField] private Text currencyText;
    [SerializeField] private Text dmgText;
    [SerializeField] private Text energyText;

    public CinemachineVirtualCamera cam1, cam2;
    [HideInInspector] public CinemachineVirtualCamera activeCam;

    private static CinemachineBasicMultiChannelPerlin noise1, noise2;
    private static float timer;

    void Awake()
    {
        if (instance != null) Destroy(instance.gameObject);

        roomCurrency = 0;
        activeCam = cam1;
        instance = this;

        noise1 = cam1.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noise2 = cam2.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 0;
            noise1.m_AmplitudeGain = 0;
            noise1.m_AmplitudeGain = 0;

            noise2.m_AmplitudeGain = 0;
            noise2.m_AmplitudeGain = 0;
        }
    }

    public static void AddSceenShake(float amp, float freq, float dur)
    {

        noise1.m_AmplitudeGain = amp;
        noise1.m_FrequencyGain = freq;

        noise2.m_AmplitudeGain = amp;
        noise2.m_FrequencyGain = freq;

        timer = dur;
    }


    public static GameHandler GetGameHandler()
    {
        return instance;
    }

    public void AddCurrency(int amount)
    {
        roomCurrency += amount;
        currencyText.text = roomCurrency.ToString();
    }

    public void RemoveCurrency(int amount)
    {
        roomCurrency -= amount;
        currencyText.text = roomCurrency.ToString();
    }

    public int GetRoomCurrency() { return roomCurrency; }

    public void UpdateDamageText(int damage)
    {
        dmgText.text = damage.ToString();
    }

    public void UpdateEnergyText(int energy, int maxEnerey)
    {
        energyText.text = energy.ToString() + " / " + maxEnerey.ToString();
    }
}

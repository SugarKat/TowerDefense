using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int startingMoney = 200;
    public int startingHealth = 100;

    public int currentMoney;
    public int currentHealth;

    public bool HasEnoughMoney(int cost) { return currentMoney >= cost; }
    public static PlayerStats Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        Instance = this;
    }
    void Start()
    {
        currentHealth = startingHealth;
        currentMoney = startingMoney;
    }
    public void TakeDamage(int amount)
    {
        if (currentHealth > amount)
        {
            currentHealth -= amount;
        }
        else
        {
            Debug.Log("TODO: Death condition. ");
        }
    }

    public void AddMoney(int amount)
    {
        currentMoney = currentMoney + amount;
    }
}

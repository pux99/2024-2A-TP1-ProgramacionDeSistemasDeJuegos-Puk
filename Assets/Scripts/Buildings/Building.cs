using System;
using System.Collections;
using System.Collections.Generic;
using HealthSystem;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private UHealth health;
    [SerializeField] private int respawnTimer;

    private void Start()
    {
        health.OnDead += Destroyed;
    }

    void Destroyed()
    {
        //gameObject.SetActive(false);
        StartCoroutine(Respawn(respawnTimer));
    }
    
    IEnumerator Respawn(int timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(true);
        health.FullHeal();
        
    }
}

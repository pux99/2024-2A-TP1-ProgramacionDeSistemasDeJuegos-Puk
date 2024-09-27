using System;
using System.Collections;
using System.Collections.Generic;
using HealthSystem;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private UHealth health;
    [SerializeField] private int respawnTimer;
    private ITargetGiverService _targetGiverService;
    public Action OnDestoyed;
    private MeshRenderer _meshRenderer;
    private IEnumerator _respawn;

    private void Awake()
    {
        
    }

    private void Start()
    {
        health.OnDead += Destroyed;
        _meshRenderer = GetComponent<MeshRenderer>();
        _targetGiverService = ServiceLocator.Instance.GetService<ITargetGiverService>();
        _targetGiverService.AddTarget(this.gameObject);
        _targetGiverService.NoMoreBuildings += StopRespawn;
        

    }

    void Destroyed()
    {
        //gameObject.SetActive(false);
        OnDestoyed?.Invoke();
        _meshRenderer.enabled = false;
        _targetGiverService.RemoveTarget(this.gameObject);
        _respawn = Respawn(respawnTimer);
        StartCoroutine(_respawn);
    }
    
    IEnumerator Respawn(int timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(true);
        health.FullHeal();
        _meshRenderer.enabled = true;
        _targetGiverService.AddTarget(this.gameObject);
    }

    void StopRespawn()
    {
        StopCoroutine(_respawn);
    }
}

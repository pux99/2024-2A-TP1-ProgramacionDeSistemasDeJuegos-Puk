using System;
using System.Collections;
using Pool;
using UnityEngine;
using UnityEngine.Serialization;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int spawnsPerPeriod = 10;
    [SerializeField] private float frequency = 30;
    [SerializeField] private float period = 0;
    private IGoblinService _goblinService;

    private void OnEnable()
    {
        if (frequency > 0) period = 1 / frequency;
    }
    
    private IEnumerator Start()
    {
        _goblinService = ServiceLocator.Instance.GetService<IGoblinService>();
        while (!destroyCancellationToken.IsCancellationRequested)
        {
            for (int i = 0; i < spawnsPerPeriod; i++)
            {
                GameObject newGoblin = _goblinService.CreateGoblin(transform.position, transform.rotation);
                //Instantiate(characterPrefab, transform.position, transform.rotation);
            }

            yield return new WaitForSeconds(period);
        }
    }
}

using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int spawnsPerPeriod = 10;
    [SerializeField] private float frequency = 30;
    [SerializeField] private float period = 0;
    private IGoblinService _goblinService;
    private ITargetGiverService _targetService;
    private bool _raidIsActive = true;

    private void OnEnable()
    {
        if (frequency > 0) period = 1 / frequency;
    }
    private IEnumerator Start()
    {
        _goblinService = ServiceLocator.Instance.GetService<IGoblinService>();
        _targetService = ServiceLocator.Instance.GetService<ITargetGiverService>();
        _targetService.NoMoreBuildings += EndRaid;
        while (!destroyCancellationToken.IsCancellationRequested&&_raidIsActive)
        {
            yield return new WaitForSeconds(period);
            for (int i = 0; i < spawnsPerPeriod; i++)
            {
                GameObject newGoblin = _goblinService.CreateGoblin(transform.position, transform.rotation);
            }
        }
    }
    void EndRaid()
    {
        _raidIsActive = false;
    }
}

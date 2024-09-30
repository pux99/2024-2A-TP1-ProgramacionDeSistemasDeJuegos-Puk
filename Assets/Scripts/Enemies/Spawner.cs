using System.Collections;
using UnityEngine;

namespace Enemies
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private int spawnsPerPeriod = 10;
        [SerializeField] private float frequency = 30;
        [SerializeField] private float period = 0;
        private IGoblinService _goblinService;
        private ITargetService _targetService;
        private bool _raidIsActive = true;

        private void OnEnable()
        {
            if (frequency > 0) period = 1 / frequency;
        }
        private IEnumerator Start()
        {
            _goblinService = ServiceLocator.Instance.GetService<IGoblinService>();
            _targetService = ServiceLocator.Instance.GetService<ITargetService>();
            _targetService.NoMoreBuildings += EndRaid;
            while (!destroyCancellationToken.IsCancellationRequested)
            {
                yield return new WaitForSeconds(period);
                for (int i = 0; i < spawnsPerPeriod; i++)
                {
                    if (_raidIsActive)
                    {
                        GameObject newGoblin = _goblinService.CreateGoblin(transform.position, transform.rotation);
                    }
                    else
                        break;
                }
            }
        }
        void EndRaid()
        {
            _raidIsActive = false;
        }
    }
}

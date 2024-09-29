using UnityEngine;
using Pool;

public class GoblinService :MonoBehaviour, IGoblinService
{
    private void Awake()
    {
        ServiceLocator serviceLocator = ServiceLocator.Instance;
        serviceLocator.RegisterService<IGoblinService>(this);
    }

    public GameObject CreateGoblin(Vector3 position, Quaternion rotation)
    {
        return GoblinPool.Instance.GetElement(position, rotation);
    }

}

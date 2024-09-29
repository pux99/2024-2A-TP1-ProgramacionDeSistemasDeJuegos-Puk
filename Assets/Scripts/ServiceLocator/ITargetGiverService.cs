using System;
using UnityEngine;

public interface ITargetGiverService
{

    GameObject GetTarget();
    bool TryToGet(out GameObject target);
    void RemoveTarget(GameObject target);
    void AddTarget(GameObject target);
    event Action NoMoreBuildings;
}

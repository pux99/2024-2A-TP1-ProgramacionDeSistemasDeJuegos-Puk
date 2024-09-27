using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetGiverService
{

    GameObject GetTarget();
    bool tryToGet(out GameObject target);
    void RemoveTarget(GameObject target);
    void AddTarget(GameObject target);
    event Action NoMoreBuildings;
}

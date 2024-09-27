using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGoblinService
{
    GameObject CreateGoblin(Vector3 position, Quaternion rotation);
}

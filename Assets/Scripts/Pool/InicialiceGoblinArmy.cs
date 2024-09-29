using System.Collections;
using System.Collections.Generic;
using Pool;
using UnityEngine;

public class InicialiceGoblinArmy : MonoBehaviour
{
    [SerializeField] private Transform Spawner;
    private IEnumerator Start()
    {
        yield return 2;
        List<GameObject> goblins=new();
        for (int i = 0; i < 150; i++)
        {
            goblins.Add(GoblinPool.Instance.GetElement(Spawner.position,Spawner.rotation));
        }

        foreach (var goblin in goblins)
        {
            goblin.SetActive(false);
            GoblinPool.Instance.ReceiveElement(goblin);
        }
    }
}

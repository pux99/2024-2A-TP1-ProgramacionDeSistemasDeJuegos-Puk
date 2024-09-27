using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfGameManager : MonoBehaviour
{
    private ITargetGiverService _targetService;
    [SerializeField]private UIManager uiManager;
    private void Start()
    {
        _targetService = ServiceLocator.Instance.GetService<ITargetGiverService>();
        _targetService.NoMoreBuildings += GoblisWin;
    }

    private void GoblisWin()
    {
        uiManager.GoblisWin();
    }
}

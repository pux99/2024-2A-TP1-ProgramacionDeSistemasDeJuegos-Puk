using Manager;
using UnityEngine;

public class EndOfGameManager : MonoBehaviour
{
    private ITargetService _targetService;
    [SerializeField]private UIManager uiManager;
    private void Start()
    {
        _targetService = ServiceLocator.Instance.GetService<ITargetService>();
        _targetService.NoMoreBuildings += GoblisWin;
    }

    private void GoblisWin()
    {
        uiManager.GoblinsWin();
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class UIManager : MonoBehaviour
    {
    
        [SerializeField] private Color goblinsColor;
        [SerializeField] private string goblinsMesage;
        [SerializeField] private Color humansColor;
        [SerializeField] private string humansMesage;
        [SerializeField] private Image endOfRaidImage;
        [SerializeField] private TextMeshProUGUI endOfRaidText;

        public void GoblinsWin()
        {
            endOfRaidImage.gameObject.SetActive(true);
            endOfRaidImage.color = goblinsColor;
            endOfRaidText.text = goblinsMesage;
        }
    }
}

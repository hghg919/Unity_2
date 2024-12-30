using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class T_Loading : MonoBehaviour
{
    [SerializeField] private GameObject BG;
    [SerializeField] private Image loadingImage;
    [SerializeField] private TextMeshProUGUI loadingTextComponent;

    public void Show()
    {
        BG.SetActive(true);
    }

    public void Hide()
    {
        BG.SetActive(false);
    }

    public void SetLoadingPercent(float percent)
    {
        loadingImage.fillAmount = percent;
        loadingTextComponent.text = $"{percent * 100}%";
    }
}

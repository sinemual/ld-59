using Client.Infrastructure.UI.BaseUI;
using TMPro;
using UnityEngine.UI;

public class LoadingScreen : BaseScreen
{
    public TextMeshProUGUI loadingProcessText;
    public Image loadinProgressBar;

    public void UpdateProgressText(float progress)
    {
        //loadingProcessText.DOText($"{(int)progress}", 0.1f);
    }
}
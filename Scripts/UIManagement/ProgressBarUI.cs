using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    [SerializeField] private GameObject hasProgressGameObject;
    private IHasProgress hasProgress;
    private void Start()
    {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        if (hasProgress == null)
        {
            Debug.LogError("Has progress = null");
        }
        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
        progressBar.fillAmount = 0f;
        hide();
    }
    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        progressBar.fillAmount = e.progressNormalized;
        if(progressBar.fillAmount==0 || progressBar.fillAmount==1)
        {
            hide();
        }
        else
        {
            show();
        }
    }
    private void show()
    {
        gameObject.SetActive(true);
    }
    private void hide()
    {
        gameObject.SetActive(false);
    }
}

using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == baseCounter)
        {
            Show();
        }
        else if(e.selectedCounter != baseCounter)
        {
            Hide();
        }
    }
    private void Show()
    {
        foreach(GameObject go in visualGameObject)
        {
            go.SetActive(true);
        }
        
    }
    private void Hide()
    {
        foreach (GameObject go in visualGameObject)
        {
            go.SetActive(false);
        }
    }


}

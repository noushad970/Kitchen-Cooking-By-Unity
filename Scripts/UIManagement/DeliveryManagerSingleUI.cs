using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeName;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplete;
    private void Awake()
    {
        iconTemplete.gameObject.SetActive(false);   
    }
    

    // Update is called once per frame
    
    public void SetRecipeSO(KitchenRecipeSO recipeSO)
    {
        recipeName.text = recipeSO.recipeName;
        foreach(Transform child in iconContainer)
        {
            if (child == iconTemplete) continue;
            Destroy(child.gameObject);
        }
        foreach(KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList)
        {
            Transform iconTransform = Instantiate(iconTemplete, iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }
    }
}

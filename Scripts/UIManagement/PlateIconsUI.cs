using System;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform IconTemplete;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        IconTemplete.gameObject.SetActive(false);
    }
    private void Start()
    {
        plateKitchenObject=GetComponentInParent<PlateKitchenObject>();
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        updateVisual();
    }
    private void updateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child == IconTemplete) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObjectSO in plateKitchenObject.getKitchenObjectSOList())
        {
           
            Transform iconTransform = Instantiate(IconTemplete, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateIconSingleUI>().setKitchenObjectSO(kitchenObjectSO);
        }
    }
}

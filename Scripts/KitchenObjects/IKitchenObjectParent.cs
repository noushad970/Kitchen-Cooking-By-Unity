using UnityEngine;

public interface IKitchenObjectParent 
{
    public Transform GetKitchenObjectFollowTransform();


    public void setKitchenObject(KitchenObject kitchenObject);


    public KitchenObject GetKitchenObject();
    public void clearKitchenObject();
    public bool hasKitchenObject();
}

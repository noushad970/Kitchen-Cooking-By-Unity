using UnityEngine;

public class BaseCounter : MonoBehaviour,IKitchenObjectParent
{
    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;
    
    public virtual void interect(Player player)
    {
        Debug.LogError("BaseCounter.Interect()");
    }
    public virtual void interctAlternate(Player player)
    {
        Debug.Log("BaseCounter.interctAlternate()");
    }
    //interfaces
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }
    public void setKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }
    public KitchenObject GetKitchenObject() { return kitchenObject; }
    public void clearKitchenObject() { kitchenObject = null; }
    public bool hasKitchenObject() { return kitchenObject != null; }
}

using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField]private KitchenObjectSO kitchenObjectSO;
    private IKitchenObjectParent kitchenObjectParent;
    public KitchenObjectSO getKitchenObj()
    {
        return kitchenObjectSO;
    }
    //public void setClearCounter(ClearCounter kitchenObjectParent)
    //{
    //    this.kitchenObjectParent = kitchenObjectParent;
    //}
    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.clearKitchenObject();
        }

        this.kitchenObjectParent = kitchenObjectParent;
        if (kitchenObjectParent.hasKitchenObject())
        {
            Debug.Log("Counter already has a kitchen object");
        }
        kitchenObjectParent.setKitchenObject(this);
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }
    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;  
    }
}

using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField]private KitchenObjectSO kitchenObjectSO;
    private IKitchenObjectParent kitchenObjectParent;
    public KitchenObjectSO getKitchenObjSO()
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
    public void destroySelf()
    {
        kitchenObjectParent.clearKitchenObject();
        Destroy(gameObject);
    }
    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject= null;
            return false;
        }
    }
    public static KitchenObject spawnKitchenObject( KitchenObjectSO kitchenObjectSO,IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
        return kitchenObject;
    }
}

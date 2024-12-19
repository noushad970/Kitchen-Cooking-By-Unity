using UnityEngine;

public class ContainerCounter : BaseCounter, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;
    public override void interect(Player player)
    {
        if (kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);

        }
        else
        {
            kitchenObject.SetKitchenObjectParent(player);

        }
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

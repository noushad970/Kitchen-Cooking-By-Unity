using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField]private KitchenObjectSO kitchenObjectSO;
    private ClearCounter clearCounter;
    public KitchenObjectSO getKitchenObj()
    {
        return kitchenObjectSO;
    }
    public void setClearCounter(ClearCounter cc)
    {
        this.clearCounter = cc;
    }
    public ClearCounter GetClearCounter()
    {
        return clearCounter;
    }
}

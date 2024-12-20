using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] CuttingRecipeSO[] cuttingRecipeSOArray;
    public override void interect(Player player)
    {
        if (!hasKitchenObject())
        {
            if (player.hasKitchenObject())
            {
                if(HasRecipeWithInput(player.GetKitchenObject().getKitchenObjSO()))
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                Debug.Log("Player has nothing");
            }
        }
        else
        {
            if (player.hasKitchenObject())
            {
                Debug.Log("Container has nothing");
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
    public override void interctAlternate(Player player)
    {
        if(hasKitchenObject() && HasRecipeWithInput(GetKitchenObject().getKitchenObjSO()))
        {
            KitchenObjectSO outputKitchenObjectSO = getOutputForInput(GetKitchenObject().getKitchenObjSO());
            GetKitchenObject().destroySelf(); 
            KitchenObject.spawnKitchenObject(outputKitchenObjectSO, this);
        }
    }
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO crs in cuttingRecipeSOArray)
        {
            if (crs.input == inputKitchenObjectSO)
            {
                return true;
            }
        }
        return false;
    }
    private KitchenObjectSO getOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach(CuttingRecipeSO crs in cuttingRecipeSOArray)
        {
            if(crs.input== inputKitchenObjectSO)
            {
                return crs.output;
            }
        }
        return null;
    }
}

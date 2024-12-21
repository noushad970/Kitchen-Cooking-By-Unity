using System;
using UnityEngine;

public class CuttingCounter : BaseCounter,IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    
    public event EventHandler OnCut;
    [SerializeField] CuttingRecipeSO[] cuttingRecipeSOArray;
    private int cuttingProgress;
    public override void interect(Player player)
    {
        if (!hasKitchenObject())
        {
            if (player.hasKitchenObject())
            {
                if(HasRecipeWithInput(player.GetKitchenObject().getKitchenObjSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().getKitchenObjSO());
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingRecipeMax
                    });
                    cuttingProgress = 0;
                }
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
                //Container has something and player carrying something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().getKitchenObjSO()))
                    {
                        GetKitchenObject().destroySelf();
                    }

                }
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
            cuttingProgress++;
            OnCut?.Invoke(this, EventArgs.Empty);
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().getKitchenObjSO());
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingRecipeMax
            });
            if (cuttingProgress >= cuttingRecipeSO.cuttingRecipeMax)
            {
                KitchenObjectSO outputKitchenObjectSO = getOutputForInput(GetKitchenObject().getKitchenObjSO());
                GetKitchenObject().destroySelf();
                KitchenObject.spawnKitchenObject(outputKitchenObjectSO, this);

            }
        }
    }
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }
    private KitchenObjectSO getOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if(cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }
    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO crs in cuttingRecipeSOArray)
        {
            if (crs.input == inputKitchenObjectSO)
            {
                return crs;
            }
        }
        return null;
    }
}

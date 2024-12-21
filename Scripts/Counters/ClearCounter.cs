
using System;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    public override void interect(Player player)
    {
        if (!hasKitchenObject())
        {
            if (player.hasKitchenObject())
            {
                //Container has nothing and player carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //"Container has nothing and player carrying nothing
            }
        }
        else
        {
            if (player.hasKitchenObject())
            {
                //Container has something and player carrying something
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().getKitchenObjSO()))
                    {
                        GetKitchenObject().destroySelf();
                    }

                }
                else
                {
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().getKitchenObjSO()))
                        {
                            player.GetKitchenObject().destroySelf();
                        }
                    }
                }
            }
            else
            {
               //Container has something and player dont carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

}

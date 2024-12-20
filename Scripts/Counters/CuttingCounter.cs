using UnityEngine;

public class CuttingCounter : BaseCounter
{
    public override void interect(Player player)
    {
        if (!hasKitchenObject())
        {
            if (player.hasKitchenObject())
            {
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
        if(hasKitchenObject())
        {

        }
    }
}

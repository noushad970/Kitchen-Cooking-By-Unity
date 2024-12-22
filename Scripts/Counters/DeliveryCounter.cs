using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void interect(Player player)
    {
        if (player.hasKitchenObject())
        {
            if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                //only accepts plates
                DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
                player.GetKitchenObject().destroySelf();
            }
        }
    }
}

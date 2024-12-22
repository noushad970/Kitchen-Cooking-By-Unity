using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void interect(Player player)
    {
        if (player.hasKitchenObject())
        {
            if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                player.GetKitchenObject().destroySelf();
            }
        }
    }
}

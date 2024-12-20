using UnityEngine;

public class TrashCounter : BaseCounter
{
    public override void interect(Player player)
    {
        if (player.hasKitchenObject())
        {
            player.GetKitchenObject().destroySelf();
        }
    }
}

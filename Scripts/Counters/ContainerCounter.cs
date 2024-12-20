using System;
using Unity.VisualScripting;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public event EventHandler OnPlayerGrabbedObject;
    public override void interect(Player player)
    {

        if (!player.hasKitchenObject())
        {
            //carrying an object
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            KitchenObject.spawnKitchenObject(kitchenObjectSO, player);

        }
    }
    //interfaces
    
}

using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawn;
    public event EventHandler OnPlateRemove;
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;
    private int platesSpawnedAmount;
    private int platesSpawnedAmountMax=4;
    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnPlateTimerMax)
        {
            spawnPlateTimer = 0;
            if(platesSpawnedAmount<spawnPlateTimerMax)
            {
                platesSpawnedAmount++;
                OnPlateSpawn?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    public override void interect(Player player)
    {
        if (!player.hasKitchenObject())
        {
            if (platesSpawnedAmount > 0)
            {
                platesSpawnedAmount--;
                KitchenObject.spawnKitchenObject(plateKitchenObjectSO, player); 
                OnPlateRemove?.Invoke(this, EventArgs.Empty);

            }
        }
    }
}

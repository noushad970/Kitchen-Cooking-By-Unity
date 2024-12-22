using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted; 
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO recipeListSO;
    private List<KitchenRecipeSO> WaitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 5f;
    private int waitingRecipesMax = 4;
    private void Awake()
    {
        Instance = this;
        WaitingRecipeSOList = new List<KitchenRecipeSO>();
    }
    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;
            if (WaitingRecipeSOList.Count < waitingRecipesMax)
            {
                KitchenRecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                WaitingRecipeSOList.Add(waitingRecipeSO);
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for(int i = 0;i < WaitingRecipeSOList.Count;i++)
        {
            KitchenRecipeSO waitingRecipeSO= WaitingRecipeSOList[i];
            if(waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.getKitchenObjectSOList().Count)
            {
                //has the same number of ingredient
                bool plateContentsMatchesRecipes = true;
                foreach(KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    //Cycling All Ingredients in the recipe
                    bool ingredientFounded = false;
                    foreach(KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.getKitchenObjectSOList()) {
                        if (plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            //ingredient matches
                            ingredientFounded= true;
                            break;
                        }
                    }
                    if (!ingredientFounded)
                    {
                        //this recipe ingredient was not found on the plate
                        plateContentsMatchesRecipes = false;
                    }
                }
                if (plateContentsMatchesRecipes)
                {
                    //player deliver the correct recipe
                    WaitingRecipeSOList.RemoveAt(i);
                    OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        //no matching found
        // player did not deliver a correct recipe;
    }
    public List<KitchenRecipeSO> GetWaitingRecipeSOList()
    {
        return WaitingRecipeSOList;
    }
}
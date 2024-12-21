using System;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter,IHasProgress
{

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }
    private State state;
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] BurningRecipeSOArray;
    private float fryingTimer,burningTimer;
    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;
    private void Start()
    {
        state = State.Idle;
    }
    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.Frying:
                MeatFrying();
                

                break; 
            case State.Fried:
                MeatBurning();
                break; 
            case State.Burned:
                break;
        }
    }
    private void MeatFrying()
    {
        if (hasKitchenObject())
        {
            fryingTimer += Time.deltaTime;
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
            });
            if (fryingTimer > fryingRecipeSO.fryingTimerMax)
            {
                Debug.Log("Fried!");
                GetKitchenObject().destroySelf();
                KitchenObject.spawnKitchenObject(fryingRecipeSO.output, this);
                state = State.Fried;
                burningTimer = 0f;
                burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().getKitchenObjSO());
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = state,
                });
            }
        }
    }
    private void MeatBurning()
    {
       
            burningTimer += Time.deltaTime;
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
        {
            progressNormalized = burningTimer / burningRecipeSO.BurningTimerMax
        });
        if (burningTimer > burningRecipeSO.BurningTimerMax)
            {
                Debug.Log("Burned!");
                GetKitchenObject().destroySelf();
                KitchenObject.spawnKitchenObject(burningRecipeSO.output, this);
                state = State.Burned;
            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
            {
                state = state,
            });
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized =  0f
            });

        }
        
    }
    public override void interect(Player player)
    {
        if (!hasKitchenObject())
        {
            if (player.hasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().getKitchenObjSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().getKitchenObjSO());
                    state = State.Frying;
                    fryingTimer = 0f;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state,
                    });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                    });
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
                Debug.Log("Container has nothing");
                //Container has something and player carrying something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().getKitchenObjSO()))
                    {
                        GetKitchenObject().destroySelf();
                        state = State.Idle;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state,
                        });
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0,
                        });
                    }

                }
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = state,
                });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0,
                });
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }
    private KitchenObjectSO getOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }
    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO frs in fryingRecipeSOArray)
        {
            if (frs.input == inputKitchenObjectSO)
            {
                return frs;
            }
        }
        return null;
    }
    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO brs in BurningRecipeSOArray)
        {
            if (brs.input == inputKitchenObjectSO)
            {
                return brs;
            }
        }
        return null;
    }
}

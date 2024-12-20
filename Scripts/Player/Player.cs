using System;
using UnityEngine;

public class Player : MonoBehaviour,IKitchenObjectParent
{
    public static Player Instance { get; private set; }
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter; 
    }
    [SerializeField]private float moveSpeed=7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] private Transform counterTopPoint;
    private KitchenObject kitchenObjectHoldPoint;
    private bool isWalking;
    private Vector3 lastInterectDir;
    private BaseCounter selectedCounter;
    // Update is called once per frame
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There is more than one player instance");
        }
        Instance= this; 
    }
    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractActionAlternate;
    }
    private void Update()
    {
        HandleMovement();
        handleInteraction();
    }
    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if(selectedCounter != null)
        {
            selectedCounter.interect(this);
        }
    }
    private void GameInput_OnInteractActionAlternate(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.interctAlternate(this);
        }
    }
    private void handleInteraction()
    {

        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        if (moveDir != Vector3.zero)
        {
            lastInterectDir = moveDir;
        }
        float interactionDistance = 2f;
        if (Physics.Raycast(transform.position, lastInterectDir, out RaycastHit rayCastHit, interactionDistance, counterLayerMask))
        {
            Debug.Log("Object collide with: "+ rayCastHit.transform.name);
            if (rayCastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                //kitchenObjectParent.interect();
                if (baseCounter != selectedCounter)
                {
                    selectingCounter(baseCounter);
                }
                
            }
            else
            {
                selectingCounter(null);
            }
        }
        else
        {
            selectingCounter(null);
        }
    }
    //this method will move a player and detect collision and collide with physics object with RayCapsule
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
        if (!canMove)
        {
            //move towards moveDir

            //attempt only x movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                //attempt only z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = moveDir.z!=0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {
                    ///
                }
            }
        }
        if (canMove)
        {
            transform.position += moveDir * moveDistance;

        }
        float rotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
        isWalking = moveDir != Vector3.zero;
    }
    public bool IsWalking()
    {
        return isWalking;
    }
    private void selectingCounter(BaseCounter baseCounter)
    {
        this.selectedCounter = baseCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });

    }

    //intarfaces
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }
    public void setKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObjectHoldPoint = kitchenObject;
    }
    public KitchenObject GetKitchenObject() { return kitchenObjectHoldPoint; }
    public void clearKitchenObject() { kitchenObjectHoldPoint = null; }
    public bool hasKitchenObject() { return kitchenObjectHoldPoint != null; }
}
/*
 using System;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] private bool testing;
    private KitchenObject kitchenObjectHoldPoint;
    private void Update()
    {
        if(testing && Input.GetKeyDown(KeyCode.T))
        {
            if(kitchenObjectHoldPoint != null)
            {
                kitchenObjectHoldPoint.setClearCounter(secondClearCounter);
            }
        }
    }
    public void interect()
    {
        if (kitchenObjectHoldPoint == null)
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().setClearCounter(this);
            //kitchenObjectTransform.localPosition = Vector3.zero;
            //kitchenObjectHoldPoint = kitchenObjectTransform.GetComponent<KitchenObject>();
            //kitchenObjectHoldPoint.setClearCounter(this);
        }
        else
        {
            Debug.Log(kitchenObjectHoldPoint.getKitchenObjSO());
        }
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }
    public void setKitchenObject(KitchenObject kitchenObjectHoldPoint)
    {
        this.kitchenObjectHoldPoint = kitchenObjectHoldPoint; 
    }
    public KitchenObject GetKitchenObject() { return kitchenObjectHoldPoint; }
    public void clearKitchenObject() { kitchenObjectHoldPoint=null; }
    public bool hasKitchenObject(){ return kitchenObjectHoldPoint != null;}
}

 */
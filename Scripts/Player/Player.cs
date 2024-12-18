using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter; 
    }
    [SerializeField]private float moveSpeed=7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
    private bool isWalking;
    private Vector3 lastInterectDir;
    private ClearCounter selectedCounter;
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
            selectedCounter.interect();
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
            if (rayCastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                //clearCounter.interect();
                if (clearCounter != selectedCounter)
                {
                    selectingCounter(clearCounter);
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
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
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
    private void selectingCounter(ClearCounter cc)
    {
        this.selectedCounter = cc;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });

    }
}

using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    private PlayerInputAction playerInputAction;
    private void Awake()
    {
        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Enable();

        playerInputAction.Player.Interect.performed += Interact_performed;
        playerInputAction.Player.InterectAlternate.performed += InteractAlternate_Performed;

    }

    private void InteractAlternate_Performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
       
    }
    //private void Interact_Alternate_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    //{
    //    OnInteractAction?.Invoke(this, EventArgs.Empty);

    //}

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputAction.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;
        return inputVector;
    }
}

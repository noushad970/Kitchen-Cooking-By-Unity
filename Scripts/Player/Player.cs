using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]private float moveSpeed=7f;
    [SerializeField] private GameInput gameInput;
    private bool isWalking;
    // Update is called once per frame
    void Update()
    {   Vector2 inputVector= gameInput.GetMovementVectorNormalized();
        Vector3 moveDire = new Vector3(inputVector.x, 0, inputVector.y);
        transform.position += moveDire* moveSpeed * Time.deltaTime;
        transform.forward =Vector3.Slerp(transform.forward, moveDire,Time.deltaTime*10);
        isWalking = moveDire != Vector3.zero;
    }
    public bool IsWalking()
    {
        return isWalking;
    }
}

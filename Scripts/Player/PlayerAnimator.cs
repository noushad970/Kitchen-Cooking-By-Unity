using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator anim;
    private const string IS_MOVING = "IsWalking";
    [SerializeField] private Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        anim.SetBool(IS_MOVING,player.IsWalking());
    }
}

using UnityEngine;
//animation
public class CuttingCountarVisual : MonoBehaviour
{
    private const string CUT = "Cut";
    [SerializeField] private CuttingCounter cuttingCounter;
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        cuttingCounter.OnCut += ContainerCounter_OnCut;
    }
    private void ContainerCounter_OnCut(object sender, System.EventArgs e)
    {
        anim.SetTrigger(CUT);
    }
}

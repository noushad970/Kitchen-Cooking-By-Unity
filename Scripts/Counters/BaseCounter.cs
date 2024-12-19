using UnityEngine;

public class BaseCounter : MonoBehaviour
{
    public virtual void interect(Player player)
    {
        Debug.LogError("BaseCounter.Interect()");
    }

}

using System;
using UnityEngine;

public class Interraction : MonoBehaviour
{
    public bool IsFocus = false;
    public Player player;
    public bool interraction = false;
    public void Focus(Player playerTarget)
    {
        IsFocus = true;
        player = playerTarget;
    }

    public void PlusFocus()
    {
        IsFocus = false;
        player = null;
        interraction = false;
    }

    public virtual void Interract(Player player)
    {
        Debug.Log("On interragie avec "+ transform.name);
    }

    private void Update()
    {
        if (IsFocus && !interraction)
        {
            Interract(player);
            interraction = true;
            
        }
    }
}

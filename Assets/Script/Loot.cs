using UnityEngine;

public class Loot : Interraction
{
    public Vector3 tourne;
    public Itemscript item;
    private RaycastHit hit;
    public override void Interract(Player player)
    {
        base.Interract(player);
        PickUp(player);
    }

    void PickUp(Player player)
    {
        Debug.Log("ça loot sale");
        bool detruit = Inventory.instance.Add(item,player);
        if (detruit)
        {
            Destroy(gameObject);
        }
    }

    void Update()    
    { 
        transform.Rotate(tourne * Time.deltaTime);;
    }
    
}

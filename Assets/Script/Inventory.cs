using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region L'instance
    
    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Wsh pokito c'est pas normal , c'est peut-être du à un multi mais c'est grave chelou");
        }
        instance = this;
    }
    
    #endregion

    public delegate void OnItemChange();

    public OnItemChange onItemChangeCallback;
    public  List<Itemscript> items = new List<Itemscript>();

    public bool Add(Itemscript item, Player player)
    {
        if (items.Count >= player.GetMaxInv())
        {
            Debug.Log("MON SAC EST FAIT (Plein)");
            return false;
        }
        items.Add(item);
        AddSats(item,player);  //Ceci est temporaire, le but sera d'utiliser Equiped par la suite
        if (onItemChangeCallback != null)
        {
            onItemChangeCallback.Invoke();
        }
        return true;
    }

    public void Equiped(Itemscript item, Player player)
    {
        AddSats(item,player);
    }

    public void Remove(Itemscript item, Player player)
    {
        DelStats(item,player);
        items.Remove(item);

    }

    public void AddSats(Itemscript item, Player player)
    {
        player.Damage += item.Damage;
        player.Defence += item.Defence;
        player.HealthMax += item.Health;
        player.Manamax += item.Mana;
        player.Tenacity += item.Tenacity;
        player.Sagacity += item.Sagacity;
    }

    public void DelStats(Itemscript item, Player player)
    {
        player.Damage -= item.Damage;
        player.Defence -= item.Defence;
        player.HealthMax -= item.Health;
        player.Manamax -= item.Mana;
        player.Tenacity -= item.Tenacity;
        player.Sagacity -= item.Sagacity;
    }
    
}

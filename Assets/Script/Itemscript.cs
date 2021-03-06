using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item",menuName = "Inventory/Item")]
public class Itemscript : ScriptableObject
{
    public string obj_name; //Nom de l'objet
    public string Itis; //Une arme gauche, un casque ,...
    public Sprite icon; //L'icon qui représentera l'objet
    public Color rarity; //La rareté de l'objet ==> La couleur du nom de l'objet
    public int Health;
    public int Tenacity; // De Health
    public int Defence; //Les stats de l'objets à écrire si elles sont != 0
    public int Damage; // à Sagacity
    public int Mana; 
    public int Sagacity; 
    public string Description; //La description de l'objet (pour le lore ou le fun =D)   Uitile pour l'affichage 
    
    
}

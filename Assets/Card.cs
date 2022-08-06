using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class Card
{
    public int rarity;
    public string id, name, description;

    public Card(int rarityIn, string idIn, string nameIn, string descIn) {
        rarity = rarityIn;
        id = idIn;
        name = nameIn;
        description = descIn;
    }

	public override string ToString()
	{
        return "Rarity: " + rarity + ", Name: " + name;
	}
}

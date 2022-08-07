using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
	public TextAsset cardSheet;
	public List<List<Card>> allCards;
	public BigCard cardDisplay;

	string[] cardList;
	PlayerCollection playerDeck;

	private void Start()
	{
		playerDeck = GameObject.FindObjectOfType<PlayerCollection>();
		GatherCards();
	}

	void GatherCards()
	{
		cardList = cardSheet.text.Split('\n');
		allCards = new List<List<Card>>();
		for (int i = 0; i < 5; i++)
		{
			allCards.Add(new List<Card>());
		}


		string[] parsedCard;
		int parsedRarity = 1;
		for (int i = 1; i < cardList.Length; i++)
		{
			parsedCard = cardList[i].Split(',');
			if (!int.TryParse(parsedCard[0], out parsedRarity))
			{
				parsedRarity = 1;
			}

			Card newCard = new Card(parsedRarity, parsedCard[1], parsedCard[2], parsedCard[3]);
			allCards[parsedRarity - 1].Add(newCard);
		}
	}

	public void PickCard()
	{
		int rarity = Random.Range(3, 4);//(0, 5);
		int cardIndex = Random.Range(0, allCards[rarity].Count);

		int superRareOdds = 1;
		int rareGrab = Random.Range(0, 100);
		if (rareGrab < superRareOdds)
		{
			rarity = 4;
			cardIndex = 0;
			Debug.Log("Lucky draw");
		}

		Card pickedCard = allCards[rarity][cardIndex];


		if (playerDeck.AddCard(pickedCard)) {
			cardDisplay.QueueCard(pickedCard);
		}
	}
}

[System.Serializable]
public class Card
{
	public int rarity;
	public string id, name, description;

	public Card(int rarityIn, string idIn, string nameIn, string descIn)
	{
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardManager : MonoBehaviour
{
	public TextAsset cardSheet;
	[SerializeField]
	public List<List<Card>> allCards;
	public BigCard cardDisplay;

	public int maxOdds = 100000;
	//Chance out of 10,000 for a card of x rarity to be picked
	public int[] rarityOdds = new int[5];

	string[] cardList;
	PlayerCollection playerDeck;

	public bool debugDisableBigCard;

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
			parsedCard = cardList[i].Split('|');
			if (!int.TryParse(parsedCard[0], out parsedRarity))
			{
				parsedRarity = 1;
			}

			//Lol gotta handle commas in the description
			string parsedDescription = parsedCard[4];
			if (parsedCard.Length > 5)
			{
				for (int j = 5; j < parsedCard.Length; j++)
				{
					parsedDescription += "," + parsedCard[j];
				}
			}

			Card newCard = new Card(parsedRarity, parsedCard[1], parsedCard[2], parsedCard[3], parsedDescription);
			allCards[parsedRarity - 1].Add(newCard);
		}
	}

	public void PickCard()
	{
		int rareGrab = Random.Range(0, maxOdds);
		int tier, cardIndex = Random.Range(0, allCards[0].Count); ;
		for (tier = 4; tier > 0; tier--)
		{
			if (rareGrab <= rarityOdds[tier])
			{
				if (tier == 4)
					Debug.Log("UBER RARE ACQUIRED");
				cardIndex = Random.Range(0, allCards[tier].Count);
				break;
			}
		}

		Card pickedCard = allCards[tier][cardIndex];

		//For now just skips a card if the prerequisite hasnt been obtained
		if (!pickedCard.prereq.Equals("") && !playerDeck.CheckPrereq(pickedCard.prereq))
		{
			Debug.Log("Prereq not found: " + pickedCard.prereq);
			PickCard();
			return;
		}

		if (playerDeck.AddCard(pickedCard) && !debugDisableBigCard)
//		&& (pickedCard.prereq.Contains("CARD") || pickedCard.id.Equals("where")))
		{
			cardDisplay.QueueCard(pickedCard);
		}
	}
}

[System.Serializable]
public class Card
{
	public int rarity;
	public string id, name, prereq, description;

	public Card(int rarityIn, string idIn, string nameIn, string prereqIn, string descIn)
	{
		rarity = rarityIn;
		id = idIn;
		name = nameIn;
		prereq = prereqIn;
		description = descIn;
	}

	public override string ToString()
	{
		return "Rarity: " + rarity + ", Name: " + name;
	}
}

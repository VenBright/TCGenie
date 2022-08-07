using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardManager : MonoBehaviour
{
	public TextAsset cardSheet;
	public List<List<Card>> allCards;
	public TextMeshPro cardName, cardDesc;
	public SpriteRenderer cardRarity;
	public Sprite[] raritySprites = new Sprite[5];

	Animator cardAnim;
	string[] cardList;
	PlayerCollection playerDeck;

	private void Start()
	{
		cardAnim = GetComponent<Animator>();
		playerDeck = GameObject.FindObjectOfType<PlayerCollection>();
		GatherCards();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			cardAnim.Play("FullFlip", 0, 0);
		}
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
		int rarity = Random.Range(0, 5);
		int cardIndex = Random.Range(0, allCards[rarity].Count);

		Card pickedCard = allCards[rarity][cardIndex];
		cardName.text = pickedCard.name;
		cardDesc.text = pickedCard.description;
		cardRarity.sprite = raritySprites[rarity];

		if (playerDeck.AddCard(pickedCard)) {
			Debug.Log("NEW CARD: " + pickedCard.id);
			cardAnim.Play("PopIn");
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

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardManager : MonoBehaviour
{
	public TextAsset cardSheet;
    public List<List<Card>> allCards;
	public TextMeshPro cardName, cardDesc;
	public Animator cardAnim;

	string[] cardList;

	private void Start()
	{
		GatherCards();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			PickCard();
			cardAnim.Play("FlipForward", 0, 0);
		}
	}

	void GatherCards() {
		cardList = cardSheet.text.Split('\n');
		allCards = new List<List<Card>>();
		for (int i = 0; i < 5; i++) {
			allCards.Add(new List<Card>());
		}


		string[] parsedCard;
		int parsedRarity = 1;
		for (int i = 1; i < cardList.Length; i++) {
			parsedCard = cardList[i].Split(',');
			if (!int.TryParse(parsedCard[0], out parsedRarity))
			{
				parsedRarity = 1;
			}

			Card newCard = new Card(parsedRarity, parsedCard[1], parsedCard[2], parsedCard[3]);
			allCards[parsedRarity - 1].Add(newCard);
		}
	}

	public void PickCard() {
		int rarity = Random.Range(0, 5);
		int card = Random.Range(0, allCards[rarity].Count);

		Card pickedCard = allCards[rarity][card];
		cardName.text = pickedCard.name;
		cardDesc.text = pickedCard.description;
	}
}

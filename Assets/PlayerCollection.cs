using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollection : MonoBehaviour
{

	//Keeps track of how many of a card a player has
	[System.Serializable]
	public class CollectedCard : Card
	{
		public int count = 0;

		public CollectedCard(Card cardIn) : base (cardIn.rarity, cardIn.id, cardIn.name, cardIn.description)
		{
			count = 1;
		}
	}

	public List<CollectedCard> cardCollection;
	public List<string> collectedIds;

	private void Start()
	{
		cardCollection = new List<CollectedCard>();
		collectedIds = new List<string>();
	}

	//Returns true if the added card was not yet in the deck
	public bool AddCard(Card cardIn) {
		if (!collectedIds.Contains(cardIn.id))
		{
			CollectedCard newCard = new CollectedCard(cardIn);
			cardCollection.Add(newCard);
			collectedIds.Add(newCard.id);

			return true;
		}
		else {
			int cardIndex = collectedIds.IndexOf(cardIn.id);
			cardCollection[cardIndex].count += 1;
		}
		return false;
	}
}

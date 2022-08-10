using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCollection : MonoBehaviour
{

	//Keeps track of how many of a card a player has
	[System.Serializable]
	public class CollectedCard : Card
	{
		public int count = 0;

		public CollectedCard(Card cardIn) : base (cardIn.rarity, cardIn.id, cardIn.name, cardIn.prereq, cardIn.description)
		{
			count = 1;
		}
	}

	public int totalCards = 0;
	public TextMeshProUGUI currencyText;

	public List<CollectedCard> cardCollection;
	public List<string> collectedIds;

	private void Start()
	{
		cardCollection = new List<CollectedCard>();
		collectedIds = new List<string>();
	}

	//Returns true if the added card was not yet in the deck
	public bool AddCard(Card cardIn) {
		totalCards++;
		currencyText.text = string.Format("{0:#,###0}", totalCards); ;
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

	public bool CheckPrereq(string prereq) {
		
		string[] allReqs = prereq.Split('/');

		string[] req;
		string type, id;
		for (int i = 0; i < allReqs.Length; i++) {
			req = allReqs[i].Split(':');
			type = req[0];
			id = req[1];

			switch (type) {
				case "CARD":
					if (!collectedIds.Contains(id))
						return false;
					break;
				case "PACK":
					//Return false if the current pack is too low
					//(Would compare the string to an enum to check)
					break;
				case "ITEM":
					Debug.Log("Item required: " + id);
					//Return false if the player doesnt have an item with this id
					break;
				default:
					//idk I guess it doesn't matter
					break;
			}
		}

		return true;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCollection : MonoBehaviour
{
    public CollectionCard cardPrefab;
	public int xBase = 160, xOffset = 330, yBase = -250, yOffset = 400;
	public CollectionCard[] allCards;
	public int cardsPerRow = 5;
	CardManager cardManager;
	public bool debugEnableBlackout = true;

	private void Awake()
	{
		cardManager = GameObject.FindObjectOfType<CardManager>();
		Populate(0);
	}

	public void Populate(int rarity) {
		for (int i = 0; i < allCards.Length; i++) {
			if (allCards[i] != null)
				Destroy(allCards[i].gameObject);
		}

		allCards = new CollectionCard[cardManager.allCards[rarity].Count];

		int row = 0;
		for (int i = 0; i < allCards.Length; i++)
		{
			if (i  % cardsPerRow == 0)
				row += 1;
			allCards[i] = Instantiate(cardPrefab, transform.position, transform.rotation, transform);
			allCards[i].GetComponent<RectTransform>().anchoredPosition =
				new Vector3(
					xBase + xOffset * (i % cardsPerRow),
					yBase - yOffset * (row - 1),
				0);

			Card myCard = cardManager.allCards[rarity][i];
			allCards[i].UpdateCard(myCard);
			if (!PlayerCollection.collectedIds.Contains(myCard.id) && debugEnableBlackout) {
				allCards[i].BlackOut();
			}
		}
		RectTransform rect = GetComponent<RectTransform>();
		rect.sizeDelta =
			new Vector2(rect.sizeDelta.x, 600 * allCards.Length / cardsPerRow);
	}
}

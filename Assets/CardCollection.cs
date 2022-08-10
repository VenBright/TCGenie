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

	private void Start()
	{
		cardManager = GameObject.FindObjectOfType<CardManager>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.K))
			Populate(0);
		if (Input.GetKeyDown(KeyCode.J))
			Populate(1);
		if (Input.GetKeyDown(KeyCode.H))
			Populate(2);
	}

	public void Populate(int rarity) {
		for (int i = 0; i < allCards.Length; i++) {
			if (allCards[i] != null)
				Destroy(allCards[i].gameObject);
		}
		Debug.Log(cardManager.allCards.Count);
		Debug.Log(cardManager.allCards[0][0].rarity);
		Debug.Log(cardManager.allCards[1][0].rarity);
		Debug.Log(cardManager.allCards[2][0].rarity);
		Debug.Log(cardManager.allCards[3][0].rarity);
		Debug.Log(cardManager.allCards[4][0].rarity);

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
			if (!PlayerCollection.collectedIds.Contains(myCard.id)) {
				allCards[i].BlackOut();
			}
		}
		RectTransform rect = GetComponent<RectTransform>();
		rect.sizeDelta =
			new Vector2(rect.sizeDelta.x, 600 * allCards.Length / cardsPerRow);
	}
}

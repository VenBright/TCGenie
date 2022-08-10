using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectionCard : MonoBehaviour
{
    public Image img;
    public TextMeshProUGUI cardName, cardDesc;
    public Image rarityDecal;

    BigCard bigCard;

    void Start()
    {
        bigCard = GameObject.FindObjectOfType<BigCard>();
    }

    public void UpdateCard(Card myCard) {
        string path = "CardPics/" + myCard.id;
        Sprite loadedImage = Resources.Load<Sprite>(path);
        img.sprite = loadedImage;

        cardName.text = myCard.name;
        cardDesc.text = myCard.description;
        if (bigCard == null)
            bigCard = GameObject.FindObjectOfType<BigCard>();
        rarityDecal.sprite = bigCard.raritySprites[myCard.rarity-1];
    }

    public void BlackOut() {
        cardName.gameObject.SetActive(false);
        cardDesc.gameObject.SetActive(false);
        img.gameObject.SetActive(false);
        GetComponent<Image>().color = Color.black;

    }
}

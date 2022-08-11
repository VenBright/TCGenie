using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class BigCard : MonoBehaviour
{
    public TextMeshPro cardName, cardDesc;
    public MeshRenderer cardPic;
    public Texture2D defaultCardPic;
    public SpriteRenderer cardRarity;
    public Sprite[] raritySprites = new Sprite[5];
    Animator cardAnim;

    public GameObject ui;

    public List<Card> queuedCards;

    [System.Serializable]
    public enum State {OFF, BACK, FRONT };
    public State currentState = State.OFF;

    public bool isWinner = false;
    public GameObject winThing;
    void Start()
    {
        cardAnim = GetComponent<Animator>();
        queuedCards = new List<Card>();
    }

    void Update()
    {
        //Clicking on the card
        if (Input.GetMouseButtonDown(0))
        {
            if (cardAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform == this.transform)
                        MoveStage();
                }
            }
        }
    }

    void MoveStage() {
        switch (currentState) {
            case State.OFF:
                if (queuedCards.Count > 0)
                {
                    DisplayCard(queuedCards[0]);
                    queuedCards.RemoveAt(0);
                    cardAnim.Play("PopIn", 0, 0);
                    currentState = State.BACK;
                }
                break;
            case State.BACK:
                cardAnim.Play("FlipForward", 0, 0);
                currentState = State.FRONT;
                break;
            case State.FRONT:
                cardAnim.Play("SlideOut", 0, 0);
                currentState = State.OFF;
                if (isWinner)
                {
                    Debug.Log("YOU DID IT. YOU WON. IDIOT");
                    winThing.SetActive(true);
                    GameObject.Find("PackManager").SetActive(false);
                    gameObject.SetActive(false);
                    break;
                }
                if (queuedCards.Count <= 0)
                    ui.SetActive(true);
                break;
        }
    }

    public void QueueCard(Card card)
    {
        if (currentState == State.OFF)
        {
            DisplayCard(card);
            cardAnim.Play("PopIn", 0, 0);
            currentState = State.BACK;
        }
        else
        {
            queuedCards.Add(card);
        }
    }

    public void DisplayCard(Card card)
    {
        ui.SetActive(false);

        cardName.text = card.name;
        cardDesc.text = card.description;
        cardRarity.sprite = raritySprites[card.rarity - 1];

        string path = "CardPics/" + card.id;

        if (Resources.Load<Texture2D>(path) != null)
        {
            Texture2D loadedImage = Resources.Load<Texture2D>(path);
            cardPic.material.SetTexture("_MainTex", loadedImage);
        }
        else
        {
            cardPic.material.SetTexture("_MainTex", defaultCardPic);
        }

        if (card.id.Equals("getout"))
            isWinner = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPack : MonoBehaviour
{
    public int packAmt = 8;

    CardManager cardManager;
    CardSpewing spew;
    Animator anim;

    private void Start()
    {
        cardManager = GameObject.FindObjectOfType<CardManager>();
        spew = GameObject.FindObjectOfType<CardSpewing>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //Clicking on the button
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == this.transform)
                    OpenPack();
            }
        }
    }

    public void OpenPack()
    {
        for (int i = 0; i < packAmt; i++) {
            cardManager.PickCard();
        }
        spew.QueueCardParticles(packAmt);
        anim.Play("PackBounce", 0, 0);
    }
}

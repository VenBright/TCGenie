using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPack : MonoBehaviour
{
    public int packAmt = 8;

    CardSpewing spew;

    private void Start()
    {
        spew = GameObject.FindObjectOfType<CardSpewing>();
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
        spew.QueueCardParticles(packAmt);
    }
}

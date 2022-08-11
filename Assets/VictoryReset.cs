using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryReset : MonoBehaviour
{
    public GameObject packs, canvas;
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
                    ScreenReset();
            }
        }
    }

	public void ScreenReset()
	{
        packs.SetActive(true);
        canvas.SetActive(true);
        gameObject.SetActive(false);
	}
}

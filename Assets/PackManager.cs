using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackManager : MonoBehaviour
{
    public GameObject[] packs;

	private void Start()
	{
        EnablePack(0);
	}

	public void EnablePack(int index)
    {
        for (int i = 0; i < packs.Length; i++)
        {
            if (i == index)
                packs[i].SetActive(true);
            else
                packs[i].SetActive(false);
        }
    }
}

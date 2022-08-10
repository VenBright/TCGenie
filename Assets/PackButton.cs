using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PackButton : MonoBehaviour
{
    public string packName;
    public Text text;
    Button button;
	Image sprite;
	string startText;

	private void Awake()
	{
		button = GetComponent<Button>();
		sprite = GetComponent<Image>();
		text = GetComponentInChildren<Text>();
		startText = text.text;
	}

	public void ToggleForSale(string amount)
	{
		button.enabled = false;
		if (text == null)
			text = GetComponentInChildren<Text>();

		text.text = packName + "\nCost: " + amount + " Cards";
		sprite.color = Color.black;
	}

	public void ToggleEnabled() {
		button.enabled = true;
		text.text = startText;
		sprite.color = Color.white;
	}
}

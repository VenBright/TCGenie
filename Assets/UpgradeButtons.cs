using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButtons : MonoBehaviour
{
    public int upgradeLevel = 0;
    public PackButton[] upgradeButtons = new PackButton[7];

    public PlayerCollection playerWallet;
    public int[] upgradeCosts = new int[6];


	private void Start()
	{
        UpdateButtons();
	}

	public void UpdateButtons() {
        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            if (i <= upgradeLevel)
            {
                upgradeButtons[i].gameObject.SetActive(true);
                upgradeButtons[i].ToggleEnabled();
            }
            else if (i == upgradeLevel+1)
            {
                upgradeButtons[i].gameObject.SetActive(true);
                upgradeButtons[i].ToggleForSale(string.Format("{0:#,###0}", upgradeCosts[i]));
            }
            else
                upgradeButtons[i].gameObject.SetActive(false);
        }
    }

    public void PurchaseUpgrade() {
        if (playerWallet.totalCards >= upgradeCosts[upgradeLevel])
        {
            playerWallet.SpendCards(upgradeCosts[upgradeLevel]);
            upgradeLevel++;
            UpdateButtons();
        }
        else
        {
            Debug.Log("Too poor. Required: " + upgradeCosts[upgradeLevel] + " Cards");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardChangeBtnGroup : MonoBehaviour
{
	public MainManager main;
	public void CardChange()
	{
		C_CardChange change = new C_CardChange();

		for (int i = 0; i < main.roomPanel.myPanel.selected.Length; i++)
		{
			if (main.roomPanel.myPanel.selected[i])
			{
				C_CardChange.Cards temp = new C_CardChange.Cards();

				temp.cardName = main.roomPanel.myPanel.cards[i].cName;
				temp.cardNum = main.roomPanel.myPanel.cards[i].number;
				temp.cardOriginNum = main.roomPanel.myPanel.cards[i].originNum;

				change.Cardss.Add(temp);
			}
		}
		main.networkManager.Send(change.Write());

		gameObject.SetActive(false);
	}

	public void CardPass()
	{
		C_CardChange change = new C_CardChange();

		main.networkManager.Send(change.Write());
		gameObject.SetActive(false);
	}
}

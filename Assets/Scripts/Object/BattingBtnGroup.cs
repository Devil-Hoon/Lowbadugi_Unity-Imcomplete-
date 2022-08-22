using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattingBtnGroup : MonoBehaviour
{
    public MainManager main;

    public Button bbingBtn;
    public Button harfBtn;
    public Button fullBtn;
    public Button checkBtn;
    public Button callBtn;
    public Button dieBtn;

    public void BBingOn()
	{
        bbingBtn.interactable = true;
	}
    public void BBingOff()
    {
        bbingBtn.interactable = false;
    }
    public void HarfOn()
    {
        harfBtn.interactable = true;
    }
    public void HarfOff()
    {
        harfBtn.interactable = false;
    }
    public void FullOn()
    {
        fullBtn.interactable = true;
    }
    public void FullOff()
    {
        fullBtn.interactable = false;
    }
    public void CheckOn()
    {
        checkBtn.interactable = true;
    }
    public void CheckOff()
    {
        checkBtn.interactable = false;
    }
    public void CallOn()
    {
        callBtn.interactable = true;
    }
    public void CallOff()
    {
        callBtn.interactable = false;
    }
    public void DieOn()
    {
        dieBtn.interactable = true;
    }
    public void DieOff()
    {
        dieBtn.interactable = false;
    }
    public void AllOff()
    {
        callBtn.interactable = false;
        dieBtn.interactable = false;
        checkBtn.interactable = false;
        bbingBtn.interactable = false;
        harfBtn.interactable = false;
        fullBtn.interactable = false;
    }

    public void AllIn()
	{
        C_Batting batting = new C_Batting();
        int money = DBManager.GP;
        batting.isAllIn = true;
        batting.batting = money;
        main.roomPanel.myPanel.player.GP -= money;
        DBManager.GP -= money;
        AllOff();
	}
}

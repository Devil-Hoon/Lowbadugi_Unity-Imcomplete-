using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum Phase
{
    First,
    Moning,
    Afternoon,
    Night,
    End
}
public class RoomPanel : MonoBehaviour
{
    public NetworkManager networkManager;
    public MainManager main;

    public UserPanel myPanel;
    public List<UserPanel> panels;

    public Phase phase;

    public GameObject prefabCard;
    public List<GameObject> deck;
    public Transform cardParent;

    public Button gameStart;
    public GameObject battingBtnGroup;
    public GameObject cardChangeBtnGroup;

    public int bbing;
    public int call;
    public int harf;
    public int full;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void DeckClear()
	{
		foreach (GameObject item in deck)
		{
            Destroy(item);
		}

        deck.Clear();
	}
    public void RoomLeave()
	{
        C_LeaveRoom room = new C_LeaveRoom();
        
        networkManager.Send(room.Write());
    }
    public void DeckCreate(List<S_BroadcastDeckList.Deck> deck)
	{
        for (int i = deck.Count - 1; i >= 0; i--)
        {
            GameObject obj = Instantiate(prefabCard, cardParent);
            obj.transform.localScale = Vector3.one;
            Card card = obj.GetComponent<Card>();
            card.originNum = deck[i].originNum;
            card.isMine = false;
            card.front = false;
            card.cName = deck[i].cName;
            card.shape = (CardShape)deck[i].shape;
            card.number = deck[i].number;
            card.SetFrontImage(main.frontImgs[deck[i].originNum]);
            card.SetBackImage(main.backImg);
            card.ShowCard();
            this.deck.Insert(0, obj);
        }
    }

    public void GameStartBtnOnOff(bool isStart)
	{
        gameStart.gameObject.SetActive(isStart);
	}

    public void GameStart()
	{
        gameStart.gameObject.SetActive(false);
        C_GameStart start = new C_GameStart();
        start.start = true;
        networkManager.Send(start.Write());
	}

    public void CardGive(string playerId)
	{
        bool isMine = false;
        UserPanel temp = null;
        if (myPanel.player.PlayerId == playerId)
        {
            temp = myPanel;
            isMine = true;
        }
        else
        {
            temp = panels.Find(x => (x.player.PlayerId == playerId));
        }
		if (temp == null)
		{
            return;
		}
        Card ca = deck[0].GetComponent<Card>();

        temp.CardGive(ca, isMine);

        deck.RemoveAt(0);
	}

    public void BattingBtnGroupOn(S_BroadcastPlayerBatting packet)
	{
		if (myPanel.player.PlayerId != packet.playerId)
		{
            battingBtnGroup.SetActive(false);
            return;
		}
        battingBtnGroup.SetActive(true);        
	}

    public void CardUsing(S_BroadcastPlayerChangingCards packet)
	{
        bool isMine = false;
        UserPanel temp = null;
        if (myPanel.player.PlayerId == packet.playerId)
        {
            temp = myPanel;
            isMine = true;
        }
        else
        {
            temp = panels.Find(x => (x.player.PlayerId == packet.playerId));
        }
        if (temp == null)
        {
            return;
        }

		for (int i = 0; i < packet.Cardss.Count; i++)
		{
            temp.CardUsing(packet.Cardss[i].originNum);
		}
    }

    public void CardChangeTurn(string playerId)
	{
		if (myPanel.player.PlayerId == playerId)
		{
            cardChangeBtnGroup.SetActive(true);
		}
		else
        {
            cardChangeBtnGroup.SetActive(false);
        }
	}
}

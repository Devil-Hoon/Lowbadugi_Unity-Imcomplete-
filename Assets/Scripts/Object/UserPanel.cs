using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserPanel : MonoBehaviour
{
    public RoomPanel roomPanel;
    public bool isMine;
    public Player player;
    public Text nickname;
    public Text moneyText;
    public Transform[] cardParent;
    public bool[] selected;
    public Card[] cards;
    [SerializeField]
    private Level level;
    [SerializeField]
    private int levelNum;
    // Start is called before the first frame update
    void Start()
    {
        selected = new bool[4];
        cards = new Card[4];
		for (int i = 0; i < cards.Length; i++)
		{
            cards[i] = null;
		}
		for (int i = 0; i < selected.Length; i++)
		{
            selected[i] = false;
		}
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
		{
            moneyText.text = player.GP.ToString();
		}
    }

    public void CardShow()
	{
		for (int i = 0; i < cards.Length; i++)
		{
			if (cards[i] != null)
			{
                cards[i].ShowCard();
			}
		}
		if (isMine)
		{
            Toggle[] toggles = gameObject.GetComponentsInChildren<Toggle>();
			for (int i = 0; i < toggles.Length; i++)
			{
                toggles[i].interactable = true;
			}
		}
	}

    public void SetPlayer(Player pl)
	{
        player = pl;
        nickname.text = player.PlayerName;
	}

    public void ToggleValueChanged(Toggle toggle)
	{
		if (roomPanel.phase == Phase.End)
		{
            return;
		}

        Card card = toggle.gameObject.GetComponentInChildren<Card>();
		if (toggle.isOn)
		{
            int num = toggle.GetComponent<CardPosition>().num;
            card.CardSelect();
            selected[num] = true;
            //player.CardSelecte();
            return;
		}
		else
		{
            int num = toggle.GetComponent<CardPosition>().num;
            card.CardDeselect();
            selected[num] = false;
            //player.CardDeselected();
            return;
		}
	}

    public void LevelTrans(int num)
	{
		switch (num)
		{
            case 0:
                level = Level.Made;
                break;
            case 1:
                level = Level.Base;
                break;
            case 2:
                level = Level.TwoBase;
                break;
			default:
                level = Level.None;
				break;
		}
	}
    public void DataClear()
	{
        player = null;
        nickname.text = "";
        moneyText.text = "";
        level = Level.None;
		for (int i = 0; i < cards.Length; i++)
		{
            if (cards[i] != null)
            {
                Destroy(cards[i].gameObject);
            }
            cards[i] = null;
            selected[i] = false;
		}
	}
    public void CardUsing(int num)
	{
        Toggle[] toggles = null;
		if (isMine)
		{
            toggles = gameObject.GetComponentsInChildren<Toggle>();
		}
        int count = int.MinValue;
        Card card = null;
		for (int i = 0; i < cards.Length; i++)
		{
            if (cards[i] != null && cards[i].originNum == num)
			{
                card = cards[i];
                cards[i] = null;
                count = i;
                break;
			}
		}
        if (card != null)
		{
            Destroy(card.gameObject);
			if (isMine)
			{
                toggles[count].interactable = false;
                toggles[count].isOn = false;
			}
		}
	}

    public void CardGive(Card card, bool isMine)
    {
        Toggle[] toggles = null;
		if (isMine)
		{
            toggles = gameObject.GetComponentsInChildren<Toggle>();
        }
        for (int i = 0; i < cardParent.Length; i++)
		{
			if (cardParent[i].Find("Card(Clone)") == null)
			{
				if (isMine)
				{
                    card.SetParent(cardParent[i], 100.0f);
                    toggles[i].interactable = true;
				}
				else
                {
                    card.SetParent(cardParent[i], 3.0f);
                }
                cards[i] = card;
                selected[i] = false;

                return;
			}
		}
	}

    public bool CheckCardCount()
	{
        bool temp = true;

		for (int i = 0; i < cards.Length; i++)
		{
			if (cards[i] == null)
			{
                temp = false;
                break;
			}
		}

        return temp;
	}
}

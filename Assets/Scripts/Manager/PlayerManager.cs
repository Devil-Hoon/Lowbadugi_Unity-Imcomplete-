using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    //MyPlayer _myPlayer;
    Dictionary<int, Player> _players = new Dictionary<int, Player>();
    public static PlayerManager Instance { get; } = new PlayerManager();
	MainManager main;
    // Start is called before the first frame update
    public void Add(S_RoomPlayerList packet)
	{
		int count = 0;
		foreach (S_RoomPlayerList.Player p in packet.Players)
		{
			Player player = new Player();
			player.SessionId = p.sessionId;
			player.PlayerId = p.playerId;
			player.PlayerName = p.playerName;
			player.GP = p.gp;

			if (!_players.ContainsKey(p.sessionId))
			{
				_players.Add(p.sessionId, player);
				if (player.PlayerId != DBManager.UserID)
				{
					main.roomPanel.panels[count].SetPlayer(player);
					main.roomPanel.panels[count].gameObject.SetActive(true);
					count++;
				}
				else
				{
					main.roomPanel.myPanel.SetPlayer(player);
					main.roomPanel.myPanel.isMine = true;
					main.roomPanel.myPanel.gameObject.SetActive(true);
				}
			}
		}
	}
	public void DuplicateCheck(S_DuplicateLogin packet)
	{
		main = GameObject.Find("MainManager").GetComponent<MainManager>();
		
		if (main.loginPanel.gameObject.activeSelf)
		{
			if (packet.loginedSessionId == -1)
			{
				main.loginPanel.Login();
			}
			else
			{
				main.loginPanel.DuplicateFind(packet);
			}
		}
	}

	public void LobbyEnter(S_EnterLobby packet)
	{
		if (main.loginPanel.gameObject.activeSelf)
		{
			main.loginPanel.LoginComplete();
			main.lobbyPanel.gameObject.SetActive(true);

			Debug.Log("Lobby Enter Success");
		}
	}

	public void LobbyLeave(S_LeaveLobby packet)
	{
		DBManager.LogOut();
		main.networkManager.Disconnect();
		main.lobbyPanel.gameObject.SetActive(false);
		main.loginPanel.gameObject.SetActive(true);
		Debug.Log("Lobby Leave Success");
	}

	public void RoomEnter(S_BroadcastEnterRoom packet)
	{
		if (packet.playerId == DBManager.UserID)
		{
			main.lobbyPanel.gameObject.SetActive(false);
			main.roomPanel.gameObject.SetActive(true);
			Debug.Log("Room Enter Success");
			return;
		}
		Player player = new Player();
		player.SessionId = packet.sessionId;
		player.PlayerId = packet.playerId;
		player.PlayerName = packet.playerName;
		player.GP = packet.gp;

		if (!_players.ContainsKey(packet.sessionId))
		{
			_players.Add(packet.sessionId, player);
			foreach (UserPanel panel in main.roomPanel.panels)
			{
				if (panel.player != null)
				{
					continue;
				}
				panel.SetPlayer(player);
				panel.gameObject.SetActive(true);
				break;
			}
			Debug.Log($"{player.PlayerName} has Enter Room");
		}
	}

	public void GameDeckSetting(S_BroadcastDeckList s_BroadcastDeckList)
	{
		main.roomPanel.DeckCreate(s_BroadcastDeckList.Decks);
	}
	public void RoomLeave(S_BroadcastLeaveRoom packet)
	{
		Player player = null;
		if (_players.TryGetValue(packet.sessionId, out player))
		{
			if (player.PlayerId == DBManager.UserID)
			{
				main.lobbyPanel.gameObject.SetActive(true);
				main.roomPanel.DeckClear();
				main.roomPanel.myPanel.DataClear();
				main.roomPanel.gameObject.SetActive(false);
				Debug.Log("Room Leave Success");
				foreach (UserPanel panel in main.roomPanel.panels)
				{
					panel.DataClear();
					panel.gameObject.SetActive(false);
				}
				_players.Clear();
			}
			else
			{
				foreach (UserPanel panel in main.roomPanel.panels)
				{
					if (panel.player == player)
					{
						panel.DataClear();
						panel.gameObject.SetActive(false);
						break;
					}
				}
				_players.Remove(packet.sessionId);
			}
			//GameObject.Destroy(player.gameObject);
			Debug.Log($"{player.PlayerName} has Leave Room");
		}
		
	}

	public void GameWait(S_GameStartWait packet)
	{
		main.roomPanel.GameStartBtnOnOff(packet.start);
	}

	public void GameStart(S_BroadcastGameStart packet)
	{
		main.roomPanel.GameStart();
	}


	public void CardGive(S_BroadcastCardGive packet)
	{
		main.roomPanel.CardGive(packet.playerId);
	}
	
	public void CardUsing(S_BroadcastPlayerChangingCards packet)
	{
		main.roomPanel.CardUsing(packet);
	}

	public void CardChangeTurn(S_BroadcastPlayerChangeTurn packet)
	{
		main.roomPanel.CardChangeTurn(packet.playerId);
	}
}

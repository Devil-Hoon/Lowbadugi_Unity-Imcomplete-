using DummyClient;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

class PacketHandler
{
	public static void S_BroadcastPlayerBattingHandler(PacketSession session, IPacket packet)
	{
		S_BroadcastPlayerBatting pkt = packet as S_BroadcastPlayerBatting;
		ServerSession serverSession = session as ServerSession;
	}
	public static void S_BroadcastPlayerChangingCardsHandler(PacketSession session, IPacket packet)
	{
		S_BroadcastPlayerChangingCards pkt = packet as S_BroadcastPlayerChangingCards;
		ServerSession serverSession = session as ServerSession;

		PlayerManager.Instance.CardUsing(pkt);

	}
	public static void S_PlayerDieHandler(PacketSession session, IPacket packet)
	{
		S_PlayerDie pkt = packet as S_PlayerDie;
		ServerSession serverSession = session as ServerSession;
	}
	public static void S_BroadcastPlayerChangeTurnHandler(PacketSession session, IPacket packet)
	{
		S_BroadcastPlayerChangeTurn pkt = packet as S_BroadcastPlayerChangeTurn;
		ServerSession serverSession = session as ServerSession;

		Debug.Log("Server Broadcast Turn Change Packet");
		PlayerManager.Instance.CardChangeTurn(pkt);
	}
	public static void S_GameStartWaitHandler(PacketSession session, IPacket packet)
	{
		S_GameStartWait pkt = packet as S_GameStartWait;
		ServerSession serverSession = session as ServerSession;

		PlayerManager.Instance.GameWait(pkt);
	}
	public static void S_BroadcastGameStartHandler(PacketSession session, IPacket packet)
	{
		S_BroadcastGameStart pkt = packet as S_BroadcastGameStart;
		ServerSession serverSession = session as ServerSession;

		Debug.Log("Server Broadcast GameStart Packet");
		//PlayerManager.Instance.GameStart(pkt);
	}
	public static void S_BroadcastDeckListHandler(PacketSession session, IPacket packet)
	{
		Debug.Log("Server Broadcast Deck List Packet");
		S_BroadcastDeckList pkt = packet as S_BroadcastDeckList;
		ServerSession serverSession = session as ServerSession;

		PlayerManager.Instance.GameDeckSetting(pkt);
	}
	public static void S_BroadcastCardGiveHandler(PacketSession session, IPacket packet)
	{
		S_BroadcastCardGive pkt = packet as S_BroadcastCardGive;
		ServerSession serverSession = session as ServerSession;
		Debug.Log("Server Broadcast Card Give");
		PlayerManager.Instance.CardGive(pkt);//PlayerManager.Instance.DuplicateCheck(pkt);
	}
	public static void S_DuplicateLoginHandler(PacketSession session, IPacket packet)
	{
		S_DuplicateLogin pkt = packet as S_DuplicateLogin;
		ServerSession serverSession = session as ServerSession;

		PlayerManager.Instance.DuplicateCheck(pkt);
	}

	public static void S_EnterLobbyHandler(PacketSession session, IPacket packet)
	{
		S_EnterLobby pkt = packet as S_EnterLobby;
		ServerSession serverSession = session as ServerSession;

		PlayerManager.Instance.LobbyEnter(pkt);
	}
	public static void S_LeaveLobbyHandler(PacketSession session, IPacket packet)
	{
		S_LeaveLobby pkt = packet as S_LeaveLobby;
		ServerSession serverSession = session as ServerSession;

		PlayerManager.Instance.LobbyLeave(pkt);
	}
	public static void S_BroadcastEnterRoomHandler(PacketSession session, IPacket packet)
	{
		S_BroadcastEnterRoom pkt = packet as S_BroadcastEnterRoom;
		ServerSession serverSession = session as ServerSession;

		PlayerManager.Instance.RoomEnter(pkt);
	}

	public static void S_BroadcastLeaveRoomHandler(PacketSession session, IPacket packet)
	{
		S_BroadcastLeaveRoom pkt = packet as S_BroadcastLeaveRoom;
		ServerSession serverSession = session as ServerSession;
		PlayerManager.Instance.RoomLeave(pkt);
	}

	public static void S_LobbyPlayerListHandler(PacketSession session, IPacket packet)
	{
		S_LobbyPlayerList pkt = packet as S_LobbyPlayerList;
		ServerSession serverSession = session as ServerSession;
	}

	public static void S_RoomPlayerListHandler(PacketSession session, IPacket packet)
	{
		S_RoomPlayerList pkt = packet as S_RoomPlayerList;
		ServerSession serverSession = session as ServerSession;

		PlayerManager.Instance.Add(pkt);
	}
}

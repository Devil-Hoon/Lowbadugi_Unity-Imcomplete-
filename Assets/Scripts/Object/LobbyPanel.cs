using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPanel : MonoBehaviour
{

    public NetworkManager networkManager;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LobbyLeave()
    {
        C_LeaveLobby lobby = new C_LeaveLobby();

        networkManager.Send(lobby.Write());
    }

    public void RoomEnter()
	{
        C_EnterRoom room = new C_EnterRoom();

        room.playerId = DBManager.UserID;
        room.playerName = DBManager.UserName;
        room.gp = DBManager.GP;

        networkManager.Send(room.Write());
        Debug.Log("Room Enter Send");
	}
}

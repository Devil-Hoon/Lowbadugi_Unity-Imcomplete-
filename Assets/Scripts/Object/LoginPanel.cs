using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour
{
    public NetworkManager networkManager;
    
    public InputField id_input;
    public InputField pw_input;
    public Button loginBtn;

    public GameObject alarmPanel;
    public Text alarmText;

    bool loginTry = false;
    bool duplicateCheck = false;
    // Start is called before the first frame update
    void Start()
    {
        loginTry = false;
        alarmPanel.SetActive(false);
        id_input.text = "";
        pw_input.text = "";
    }

	private void FixedUpdate()
	{
        if (loginTry)
        {
            if (NetworkManager.isConnected && !duplicateCheck)
            {
                DuplicateCheck();
            }
        }
    }
	public void PasswordCheck()
	{
        string alarmMessage;

        if (!DBManager.PasswordCheck(id_input.text, pw_input.text, out alarmMessage))
		{
            loginTry = false;
            AlarmOn(alarmMessage);
		}
		else
		{
            networkManager.Accept();
            duplicateCheck = false;
            loginTry = true;
            Debug.Log("Password Check Success");
        }
	}

    public void DuplicateCheck()
	{
        duplicateCheck = true;
        C_DuplicateCheck dCheck = new C_DuplicateCheck();
        dCheck.playerId = id_input.text;

        networkManager.Send(dCheck.Write());
        Debug.Log("DuplicateCheck Send");
    }

    public void DuplicateFind(S_DuplicateLogin pkt)
    {
        loginTry = false;
        AlarmOn("이미 아이디가 로그인 되어있습니다.");
        networkManager.Disconnect();
	}

    public void LobbyEnter()
	{
        C_EnterLobby lobby = new C_EnterLobby();
        lobby.playerId = DBManager.UserID;
        lobby.playerName = DBManager.UserName;
        lobby.gp = DBManager.GP;

        networkManager.Send(lobby.Write());
        Debug.Log("Lobby Enter Send");
    }
    public void Login()
	{
        string alarmMessage = "";
		if (DBManager.Login(id_input.text, pw_input.text, out alarmMessage))
		{
            LobbyEnter();
		}
		else
		{
            AlarmOn(alarmMessage);
		}
	}
    public void LoginComplete()
	{
        id_input.text = "";
        pw_input.text = "";

        gameObject.SetActive(false);
        Debug.Log("Login Complete");
    }

    public void AlarmOn(string alarmMessage)
	{
        alarmText.text = alarmMessage;
        alarmPanel.SetActive(true);
	}
    public void AlarmOff()
    {
        loginTry = false;
        alarmPanel.SetActive(false);
	}

    public void VerifyInput()
	{
        loginBtn.interactable = (id_input.text.Length >= 1 && pw_input.text.Length >= 6);
	}
}

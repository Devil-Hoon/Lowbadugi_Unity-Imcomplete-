using DummyClient;
using ServerCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    ServerSession _session;
    Connector connector = new Connector();

    public static bool isConnected;
    public void Send(ArraySegment<byte> sendBuff)
    {
        _session.Send(sendBuff);
    }
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(960, 600, false);
        //Login();
    }


    public void Accept()
    {
        _session = new ServerSession();
        // DNS (Domain Name System)
        string host = Dns.GetHostName();
        IPHostEntry ipHost = Dns.GetHostEntry(host);
        IPAddress ipAddr = ipHost.AddressList[0];
        //string dns = "172.30.1.19"; //임시로 네이버로 잡았고 필요에 따라 dns 쓰면 됨.
        //IPAddress bnetServerIP = Dns.GetHostAddresses(dns)[0];
        IPEndPoint endPoint = new IPEndPoint(ipAddr, 8000);
        
        connector.Connect(endPoint,
            () => { return _session; },
            1);
    }

    public void Disconnect()
	{
        connector.Disconnect();
	}
	private void OnApplicationQuit()
	{
        connector.Disconnect();
        //C_LeaveRoom room = new C_LeaveRoom();
        //_session.Send(room.Write());
        //C_LeaveLobby lobby = new C_LeaveLobby();
        //_session.Send(lobby.Write());
        _session = null;
	}
	// Update is called once per frame
	void Update()
    {
        isConnected = connector.isConnected;
        List<IPacket> list = PacketQueue.Instance.PopAll();
		foreach (IPacket packet in list)
		{
            PacketManager.Instance.HandlePacket(_session, packet);
		}
    }
}

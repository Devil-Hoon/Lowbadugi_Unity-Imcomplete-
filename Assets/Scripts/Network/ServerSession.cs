using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using ServerCore;

namespace DummyClient
{
	class ServerSession : PacketSession
	{
		//static unsafe void ToBytes(byte[] array, int offset, ulong value)
		//{
		//	fixed (byte* ptr = &array[offset])
		//		*(ulong*)ptr = value;
		//}
		public override void OnConnected(EndPoint endPoint)
		{
			Console.WriteLine($"OnCconected : {endPoint}");
		}

		public override void OnDisconnected(EndPoint endPoint)
		{
			Console.WriteLine($"OnDisconected : {endPoint}");
		}

		public override void OnRecvPacket(ArraySegment<byte> buffer)
		{
			PacketManager.Instance.OnRecvPacket(this, buffer, (s, p) => PacketQueue.Instance.Push(p));
		}

		public override void OnSend(int numOfBytes)
		{
			//Console.WriteLine($"Transferred bytes: {numOfBytes}");
		}
	}
}

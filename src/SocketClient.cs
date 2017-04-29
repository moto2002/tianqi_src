using LuaFramework;
using System;
using System.IO;
using System.Net.Sockets;
using UnityEngine;

public class SocketClient
{
	private const int MAX_READ = 8192;

	private TcpClient client;

	private NetworkStream outStream;

	private MemoryStream memStream;

	private BinaryReader reader;

	private byte[] byteBuffer = new byte[8192];

	public static bool loggedIn;

	public void OnRegister()
	{
		this.memStream = new MemoryStream();
		this.reader = new BinaryReader(this.memStream);
	}

	public void OnRemove()
	{
		this.Close();
		this.reader.Close();
		this.memStream.Close();
	}

	private void ConnectServer(string host, int port)
	{
		this.client = null;
		this.client = new TcpClient();
		this.client.set_SendTimeout(1000);
		this.client.set_ReceiveTimeout(1000);
		this.client.set_NoDelay(true);
		try
		{
			this.client.BeginConnect(host, port, new AsyncCallback(this.OnConnect), null);
		}
		catch (Exception ex)
		{
			this.Close();
			Debug.LogError(ex.get_Message());
		}
	}

	private void OnConnect(IAsyncResult asr)
	{
		this.outStream = this.client.GetStream();
		this.client.GetStream().BeginRead(this.byteBuffer, 0, 8192, new AsyncCallback(this.OnRead), null);
		NetworkManager.AddEvent(101, new ByteBuffer());
	}

	private void WriteMessage(byte[] message)
	{
		MemoryStream memoryStream2;
		MemoryStream memoryStream = memoryStream2 = new MemoryStream();
		try
		{
			memoryStream.set_Position(0L);
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			ushort num = (ushort)message.Length;
			binaryWriter.Write(num);
			binaryWriter.Write(message);
			binaryWriter.Flush();
			if (this.client != null && this.client.get_Connected())
			{
				byte[] array = memoryStream.ToArray();
				this.outStream.BeginWrite(array, 0, array.Length, new AsyncCallback(this.OnWrite), null);
			}
			else
			{
				Debug.LogError("client.connected----->>false");
			}
		}
		finally
		{
			if (memoryStream2 != null)
			{
				memoryStream2.Dispose();
			}
		}
	}

	private void OnRead(IAsyncResult asr)
	{
		int num = 0;
		try
		{
			NetworkStream stream = this.client.GetStream();
			lock (stream)
			{
				num = this.client.GetStream().EndRead(asr);
			}
			if (num < 1)
			{
				this.OnDisconnected(DisType.Disconnect, "bytesRead < 1");
			}
			else
			{
				this.OnReceive(this.byteBuffer, num);
				NetworkStream stream2 = this.client.GetStream();
				lock (stream2)
				{
					Array.Clear(this.byteBuffer, 0, this.byteBuffer.Length);
					this.client.GetStream().BeginRead(this.byteBuffer, 0, 8192, new AsyncCallback(this.OnRead), null);
				}
			}
		}
		catch (Exception ex)
		{
			this.OnDisconnected(DisType.Exception, ex.get_Message());
		}
	}

	private void OnDisconnected(DisType dis, string msg)
	{
		this.Close();
		int num = (dis != DisType.Exception) ? 103 : 102;
		ByteBuffer byteBuffer = new ByteBuffer();
		byteBuffer.WriteShort((ushort)num);
		NetworkManager.AddEvent(num, byteBuffer);
		Debug.LogError(string.Concat(new object[]
		{
			"Connection was closed by the server:>",
			msg,
			'Distype:>",
			dis
		}));
	}

	private void PrintBytes()
	{
		string text = string.Empty;
		for (int i = 0; i < this.byteBuffer.Length; i++)
		{
			text += this.byteBuffer[i].ToString("X2");
		}
		Debug.LogError(text);
	}

	private void OnWrite(IAsyncResult r)
	{
		try
		{
			this.outStream.EndWrite(r);
		}
		catch (Exception ex)
		{
			Debug.LogError("OnWrite--->>>" + ex.get_Message());
		}
	}

	private void OnReceive(byte[] bytes, int length)
	{
		this.memStream.Seek(0L, 2);
		this.memStream.Write(bytes, 0, length);
		this.memStream.Seek(0L, 0);
		while (this.RemainingBytes() > 2L)
		{
			ushort num = this.reader.ReadUInt16();
			if (this.RemainingBytes() < (long)num)
			{
				this.memStream.set_Position(this.memStream.get_Position() - 2L);
				break;
			}
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			binaryWriter.Write(this.reader.ReadBytes((int)num));
			memoryStream.Seek(0L, 0);
			this.OnReceivedMessage(memoryStream);
		}
		byte[] array = this.reader.ReadBytes((int)this.RemainingBytes());
		this.memStream.SetLength(0L);
		this.memStream.Write(array, 0, array.Length);
	}

	private long RemainingBytes()
	{
		return this.memStream.get_Length() - this.memStream.get_Position();
	}

	private void OnReceivedMessage(MemoryStream ms)
	{
		BinaryReader binaryReader = new BinaryReader(ms);
		byte[] data = binaryReader.ReadBytes((int)(ms.get_Length() - ms.get_Position()));
		ByteBuffer byteBuffer = new ByteBuffer(data);
		int @event = (int)byteBuffer.ReadShort();
		NetworkManager.AddEvent(@event, byteBuffer);
	}

	private void SessionSend(byte[] bytes)
	{
		this.WriteMessage(bytes);
	}

	public void Close()
	{
		if (this.client != null)
		{
			if (this.client.get_Connected())
			{
				this.client.Close();
			}
			this.client = null;
		}
		SocketClient.loggedIn = false;
	}

	public void SendConnect()
	{
		this.ConnectServer(AppConst.SocketAddress, AppConst.SocketPort);
	}

	public void SendMessage(ByteBuffer buffer)
	{
		this.SessionSend(buffer.ToBytes());
		buffer.Close();
	}
}

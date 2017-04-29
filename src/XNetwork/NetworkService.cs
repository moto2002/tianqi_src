using Package;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading;
using UnityEngine;

namespace XNetwork
{
	public class NetworkService
	{
		public delegate void OnError(ServerType serverType, string message);

		public delegate void OnDisconnect(ServerType serverType);

		public delegate void OnReconnect(ServerType serverType, bool success);

		protected static NetworkService instance;

		protected XDict<ServerType, NetworkConnection> connections = new XDict<ServerType, NetworkConnection>();

		protected NetBuffer sendBuffer;

		protected static Dictionary<short, List<NetHandler>> receivePacketHandler = new Dictionary<short, List<NetHandler>>();

		public NetworkService.OnError OnErrorHandler;

		public NetworkService.OnDisconnect OnDisconnectHandler;

		public NetworkService.OnReconnect OnReconnectHandler;

		protected int pingThreadCount;

		protected Thread pingThread;

		protected long pingBeginTime;

		protected long pingEndTime;

		protected int pingValue;

		protected ManualResetEvent pingAutoEvent = new ManualResetEvent(false);

		public static NetworkService Instance
		{
			get
			{
				if (NetworkService.instance == null)
				{
					NetworkService.instance = new NetworkService();
				}
				return NetworkService.instance;
			}
		}

		public bool HasNetwork
		{
			get
			{
				return Application.get_internetReachability() != 0;
			}
		}

		protected NetBuffer SendBuffer
		{
			get
			{
				return this.sendBuffer;
			}
			set
			{
				if (this.sendBuffer != null && this.sendBuffer.IsWriting)
				{
					Debug.Log(NetworkService.CreateStackTrace(string.Concat(new object[]
					{
						"GetBug: ",
						this.sendBuffer.ReadShortAt(4),
						" ",
						this.sendBuffer.PacketType,
						" ",
						this.sendBuffer.CurrentPos
					})));
				}
				this.sendBuffer = value;
			}
		}

		public bool IsDataServerConnected
		{
			get
			{
				NetworkConnection connection = this.GetConnection(ServerType.Data);
				return connection != null && connection.IsConnected;
			}
		}

		protected long PingBeginTime
		{
			set
			{
				this.pingBeginTime = value;
			}
		}

		public long PingEndTime
		{
			set
			{
				this.pingEndTime = value;
				this.PingValue = (int)((float)(this.pingEndTime - this.pingBeginTime) * 0.5f);
			}
		}

		public int PingValue
		{
			get
			{
				return (this.GetConnection(ServerType.Data) != null) ? this.pingValue : -1;
			}
			protected set
			{
				this.pingValue = value;
			}
		}

		protected NetworkService()
		{
		}

		protected static string CreateStackTrace(string str)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(str);
			stringBuilder.Append("\n");
			StackTrace stackTrace = new StackTrace(true);
			StackFrame[] frames = stackTrace.GetFrames();
			for (int i = 0; i < frames.Length; i++)
			{
				stringBuilder.Append(string.Concat(new object[]
				{
					frames[i].GetFileName(),
					"__",
					frames[i].GetFileLineNumber(),
					"__",
					frames[i].GetMethod().get_Name(),
					"\n"
				}));
			}
			stringBuilder.Append("=========================================");
			return stringBuilder.ToString();
		}

		public void Init()
		{
			NetworkUtility.Init(GamePackets.SendPackets, GamePackets.RecvPackets, GamePackets.SendPackets2, GamePackets.RecvPackets2);
			NetBufferPool.Instance.Init();
		}

		public void Release()
		{
			for (int i = 0; i < this.connections.Count; i++)
			{
				if (this.connections.ElementValueAt(i) != null)
				{
					this.connections.ElementValueAt(i).Release();
				}
			}
			this.connections.Clear();
			if (this.pingThread != null)
			{
				this.pingThread.Abort();
				this.pingThread = null;
			}
		}

		public void ConnectByDomain(string dnsName, int port, ServerType serverType, Action onConnectSuccessCallBack = null, Action onConnectFailedCallBack = null)
		{
			string text = string.Empty;
			IPAddress[] array = null;
			try
			{
				array = Dns.GetHostAddresses(dnsName);
			}
			catch (Exception)
			{
				Debug.LogError("无法解析当前域名：" + dnsName);
				if (onConnectFailedCallBack != null)
				{
					onConnectFailedCallBack.Invoke();
				}
				return;
			}
			if (array == null)
			{
				if (onConnectFailedCallBack != null)
				{
					onConnectFailedCallBack.Invoke();
				}
				return;
			}
			if (array.Length == 0)
			{
				if (onConnectFailedCallBack != null)
				{
					onConnectFailedCallBack.Invoke();
				}
				return;
			}
			text = array[0].ToString();
			Debug.Log(text);
			Action<object[]> action = new Action<object[]>(this.ConnectByIPEx);
			XDict<Ping, object[]> xDict = new XDict<Ping, object[]>();
			for (int i = 0; i < array.Length; i++)
			{
				xDict.Add(new Ping(array[i].ToString()), new object[]
				{
					array[i],
					port,
					serverType,
					onConnectSuccessCallBack,
					onConnectFailedCallBack
				});
			}
		}

		protected void ConnectByIPEx(object[] temp)
		{
			if (temp == null)
			{
				return;
			}
			if (temp.Length < 5)
			{
				return;
			}
			Action onConnectSuccessCallBack = temp[3] as Action;
			Action action = temp[4] as Action;
			string text = temp[0].ToString();
			if (text == null)
			{
				if (action != null)
				{
					action.Invoke();
				}
				return;
			}
			int port = -1;
			if (!int.TryParse(temp[1].ToString(), ref port))
			{
				if (action != null)
				{
					action.Invoke();
				}
				return;
			}
			object obj = Enum.Parse(typeof(ServerType), temp[2].ToString());
			if (obj == null)
			{
				if (action != null)
				{
					action.Invoke();
				}
				return;
			}
			this.ConnectByIP(text, port, (ServerType)((int)obj), onConnectSuccessCallBack, action);
		}

		public void ConnectByIP(string ipString, int port, ServerType serverType, Action onConnectSuccessCallBack = null, Action onConnectFailedCallBack = null)
		{
			IPEndPoint ipEndPoint = null;
			try
			{
				ipEndPoint = new IPEndPoint(IPAddress.Parse(ipString), port);
			}
			catch (Exception ex)
			{
				Debug.LogError("IP is wrong:" + ex.get_Message());
				if (onConnectFailedCallBack != null)
				{
					onConnectFailedCallBack.Invoke();
				}
				return;
			}
			this.ConnectByIPEndPoint(ipEndPoint, serverType, onConnectSuccessCallBack, onConnectFailedCallBack);
		}

		public void ConnectByIPEndPoint(IPEndPoint ipEndPoint, ServerType serverType, Action onConnectSuccessCallBack, Action onConnectFailedCallBack)
		{
			NetworkConnection networkConnection;
			if (this.connections.ContainsKey(serverType))
			{
				Debug.Log(serverType + "的Connection已经存在了");
				networkConnection = this.connections[serverType];
			}
			else
			{
				networkConnection = new NetworkConnection(serverType);
				this.connections.Add(serverType, networkConnection);
			}
			networkConnection.Connect(ipEndPoint, delegate
			{
				Loom.Current.QueueOnMainThread(delegate
				{
					if (onConnectSuccessCallBack != null)
					{
						onConnectSuccessCallBack.Invoke();
					}
				});
			}, delegate
			{
				Loom.Current.QueueOnMainThread(delegate
				{
					if (onConnectFailedCallBack != null)
					{
						onConnectFailedCallBack.Invoke();
					}
				});
			});
		}

		public void Disconnect(ServerType serverType)
		{
			if (serverType == ServerType.Data && this.pingThread != null)
			{
				this.pingThread.Abort();
				this.pingThread = null;
			}
			NetworkConnection connection = this.GetConnection(serverType);
			if (connection != null)
			{
				connection.DisconnectAndClearData();
			}
		}

		public void DisconnectAll()
		{
			ReconnectManager.Instance.EnableReconnect = false;
			if (this.pingThread != null)
			{
				this.pingThread.Abort();
				this.pingThread = null;
			}
			for (int i = 0; i < this.connections.Values.get_Count(); i++)
			{
				if (this.connections.Values.get_Item(i) != null)
				{
					this.connections.Values.get_Item(i).DisconnectAndClearData();
				}
			}
			this.connections.Clear();
		}

		public void DisconnectAndReconnectAll()
		{
			if (this.pingThread != null)
			{
				this.pingThread.Abort();
				this.pingThread = null;
			}
			for (int i = 0; i < this.connections.Values.get_Count(); i++)
			{
				if (this.connections.Values.get_Item(i) != null)
				{
					this.connections.Values.get_Item(i).DisconnectAndReconnect();
				}
			}
		}

		public void BeginSend(Type dataType, object data, ServerType serverType, PacketType packetType = PacketType.Data, bool isVitalData = false)
		{
			if (serverType == ServerType.Chat && EntityWorld.Instance.EntSelf != null && ClientGMManager.NetSwitch01)
			{
				Debug.Log("BeginSend");
			}
			if (NetworkUtility.GetSendPacketsType(dataType) == 0)
			{
				Debug.LogError("发送类型对应OpCode不存在");
				return;
			}
			NetworkConnection connection = this.GetConnection(serverType);
			if (connection == null)
			{
				Debug.Log("连接还没准备好");
				return;
			}
			NetBuffer netBuffer = NetBuffer.Create();
			if (packetType == PacketType.Verify)
			{
				netBuffer.PacketType = PacketType.Verify;
			}
			if (isVitalData)
			{
				netBuffer.WriteDataPacket(dataType, data, 0);
				InstantlyAckReq newWrapData = NetworkUtility.GetNewWrapData();
				newWrapData.argBuf = new byte[netBuffer.MemorySize];
				Array.Copy(netBuffer.Buffer, 0, newWrapData.argBuf, 0, netBuffer.MemorySize);
				NetBuffer netBuffer2 = NetBuffer.Create();
				netBuffer2.PacketType = netBuffer.PacketType;
				netBuffer2.WriteDataPacket(NetworkUtility.WrapType, newWrapData, connection.TryGetAckNumber(NetworkUtility.WrapType));
				connection.SendPacket(netBuffer2);
			}
			else
			{
				netBuffer.WriteDataPacket(dataType, data, connection.TryGetAckNumber(dataType));
				connection.SendPacket(netBuffer);
			}
		}

		public void ProcessPackets(float deltaTime)
		{
			for (int i = 0; i < this.connections.Values.get_Count(); i++)
			{
				if (this.connections.Values.get_Item(i) != null)
				{
					this.connections.Values.get_Item(i).ProcessInstantlyAckCheck(deltaTime);
				}
				if (this.connections.Values.get_Item(i) != null)
				{
					this.connections.Values.get_Item(i).ProcessSendQueue();
				}
				if (this.connections.Values.get_Item(i) != null)
				{
					this.connections.Values.get_Item(i).ProcessReceiveQueue();
				}
			}
		}

		public void HandlePacketData(short opCode, object[] parameters)
		{
			if (NetworkService.receivePacketHandler.ContainsKey(opCode))
			{
				List<NetHandler> list = NetworkService.receivePacketHandler.get_Item(opCode);
				if (list.get_Count() > 0)
				{
					for (int i = 0; i < list.get_Count(); i++)
					{
						try
						{
							list.get_Item(i).Method.Invoke(list.get_Item(i).Target, parameters);
						}
						catch (Exception ex)
						{
							Debuger.Error(ex.get_Message(), new object[0]);
							throw;
						}
					}
				}
				else
				{
					Debuger.Error(string.Concat(new object[]
					{
						"未找到opCode: ",
						opCode,
						" (",
						(!GamePackets.RecvPackets.ContainsKey(opCode)) ? "null" : GamePackets.RecvPackets.get_Item(opCode).get_Name(),
						") 对应的Handler(s), 所有Handler都被删除"
					}), new object[0]);
				}
			}
			else
			{
				Debuger.Error(string.Concat(new object[]
				{
					"未找到opCode: ",
					opCode,
					" (",
					(!GamePackets.RecvPackets.ContainsKey(opCode)) ? "null" : GamePackets.RecvPackets.get_Item(opCode).get_Name(),
					") 对应的Handler(s)"
				}), new object[0]);
			}
		}

		public void HandlePacketError(ServerType serverType, string msg)
		{
			if (this.OnErrorHandler != null)
			{
				this.OnErrorHandler(serverType, msg);
			}
		}

		public void HandlePacketDisconnect(ServerType serverType)
		{
			if (this.OnDisconnectHandler != null)
			{
				this.OnDisconnectHandler(serverType);
			}
		}

		public void HandlePacketReconnect(ServerType serverType, bool success)
		{
			if (this.OnReconnectHandler != null)
			{
				this.OnReconnectHandler(serverType, success);
			}
		}

		public void AddListenEvent<T>(NetCallBackMethod<T> callBack) where T : class
		{
			Type typeFromHandle = typeof(T);
			short recvPacketsType = NetworkUtility.GetRecvPacketsType(typeFromHandle);
			if (!NetworkService.receivePacketHandler.ContainsKey(recvPacketsType))
			{
				NetworkService.receivePacketHandler.Add(recvPacketsType, new List<NetHandler>());
			}
			NetworkService.receivePacketHandler.get_Item(recvPacketsType).Add(new NetHandler(callBack.get_Method(), callBack.get_Target()));
		}

		public void RemoveListenEvent<T>(NetCallBackMethod<T> callBack) where T : class
		{
			Type typeFromHandle = typeof(T);
			short recvPacketsType = NetworkUtility.GetRecvPacketsType(typeFromHandle);
			if (!NetworkService.receivePacketHandler.ContainsKey(recvPacketsType))
			{
				return;
			}
			List<NetHandler> list = NetworkService.receivePacketHandler.get_Item(recvPacketsType);
			for (int i = 0; i < list.get_Count(); i++)
			{
				if (list.get_Item(i).Method == callBack.get_Method() && list.get_Item(i).Target == callBack.get_Target())
				{
					list.Remove(list.get_Item(i));
					return;
				}
			}
		}

		public void InitPing()
		{
			if (this.pingAutoEvent != null)
			{
				this.pingAutoEvent.Close();
				this.pingAutoEvent = null;
			}
			if (this.pingThread != null)
			{
				this.pingThread.Abort();
				this.pingThread = null;
			}
			this.pingAutoEvent = new ManualResetEvent(false);
			this.pingThread = new Thread(new ParameterizedThreadStart(this.SendPing));
			this.pingThreadCount++;
			this.pingThread.Start(this.pingThreadCount);
		}

		protected void SendPing(object state)
		{
			while (true)
			{
				NetworkConnection connection = this.GetConnection(ServerType.Data);
				if (connection != null && connection.IsConnected && this.pingAutoEvent != null)
				{
					this.pingAutoEvent.Reset();
					this.PingBeginTime = DateTime.get_Now().get_Ticks() / 10000L;
					NetBuffer netBuffer = NetBuffer.Create();
					netBuffer.PacketType = PacketType.Ping;
					netBuffer.WritePingPacket(NetworkUtility.PingReqOpCode);
					bool flag = connection.SendPacket(netBuffer);
					if (flag)
					{
						if (this.pingAutoEvent.WaitOne(10000, false))
						{
							Thread.Sleep(5000);
						}
						else
						{
							Debug.LogError("Ping Time out: " + DateTime.get_Now());
							if (connection != null)
							{
								connection.TryStateDisconnect();
							}
						}
					}
					else
					{
						Thread.Sleep(5000);
					}
				}
				else
				{
					Thread.Sleep(5000);
				}
			}
		}

		public void OnPingRes()
		{
			if (this.pingAutoEvent != null)
			{
				this.pingAutoEvent.Set();
			}
			this.PingEndTime = DateTime.get_Now().get_Ticks() / 10000L;
		}

		public void SendCacheData(ServerType serverType, object ackData = null)
		{
			NetBuffer netBuffer = null;
			if (ackData != null)
			{
				netBuffer = NetBuffer.Create();
				netBuffer.PacketType = PacketType.Data;
				netBuffer.WriteDataPacket(ackData.GetType(), ackData, 0);
			}
			NetworkConnection connection = this.GetConnection(serverType);
			if (connection != null)
			{
				connection.SendCacheData(netBuffer);
			}
		}

		public NetworkConnection GetConnection(ServerType serverType)
		{
			if (this.connections.ContainsKey(serverType))
			{
				return this.connections[serverType];
			}
			return null;
		}

		public string ShowAllConnectionState()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.connections.Values.get_Count(); i++)
			{
				if (this.connections.Values.get_Item(i) == null)
				{
					stringBuilder.Append(this.connections.Keys.get_Item(i).ToString() + " == null\n");
				}
				else
				{
					stringBuilder.Append(this.connections.Values.get_Item(i).GetStateString());
				}
				stringBuilder.Append("\n");
			}
			return stringBuilder.ToString();
		}

		public string ShowConnectionState(ServerType serverType)
		{
			NetworkConnection connection = this.GetConnection(serverType);
			if (connection == null)
			{
				return serverType + " == null\n";
			}
			return connection.GetStateString();
		}

		public string ShowConnectionAckCache(ServerType serverType)
		{
			NetworkConnection connection = this.GetConnection(serverType);
			if (connection == null)
			{
				return serverType + " == null\n";
			}
			return connection.GetAckCacheString();
		}
	}
}

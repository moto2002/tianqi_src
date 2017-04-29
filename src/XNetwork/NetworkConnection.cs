using Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

namespace XNetwork
{
	public class NetworkConnection : NetworkBase
	{
		protected const int MaxReceiveAckNumber = 2100000000;

		protected int connectingID;

		protected int connectedID;

		protected Socket connection;

		protected DateTime lastConnectingTime = default(DateTime);

		protected DateTime lastConnectedTime = default(DateTime);

		protected IPEndPoint ipEndPoint;

		protected NetworkReachability markReachabilityState;

		protected NetworkConnectState connectState;

		protected Action onConnectSuccessCallBack;

		protected Action onConnectFailedCallBack;

		protected bool isManualDisconnect;

		private static object connectLockObject = new object();

		protected Thread sendThread;

		protected Queue<NetBuffer> sendQueue = new Queue<NetBuffer>();

		protected Queue<NetBuffer> sendFailedCacheQueue = new Queue<NetBuffer>();

		protected bool isSending;

		protected static int sendUID = 0;

		protected SortedDictionary<int, NetBuffer> sendNeedConfirmCache = new SortedDictionary<int, NetBuffer>();

		protected XDict<int, float> needInstantlyAckNumbers = new XDict<int, float>();

		protected int lastSendAckNumber;

		protected byte[] receiveCache = new byte[8192];

		protected NetBuffer receiveDataBuffer;

		protected int readBeginOffset;

		protected int readExpectedSize;

		protected int lastReceiveAckNumber = -1;

		protected float lastApplyReceiveAckTime;

		protected int betweenCount1;

		protected int betweenCount2;

		protected Queue<NetBuffer> receiveQueue = new Queue<NetBuffer>();

		protected static bool isOpenTest = true;

		protected static XDict<string, List<NetBuffer>> allSendNeedConfirmCache = new XDict<string, List<NetBuffer>>();

		public IPEndPoint EndPoint
		{
			get
			{
				return this.ipEndPoint;
			}
		}

		public NetworkReachability MarkReachabilityState
		{
			get
			{
				return this.markReachabilityState;
			}
			set
			{
				this.markReachabilityState = value;
			}
		}

		protected bool IsNetworkAvailableAndConstant
		{
			get
			{
				return this.MarkReachabilityState != null && this.MarkReachabilityState == Application.get_internetReachability();
			}
		}

		public NetworkConnectState ConnectState
		{
			get
			{
				return this.connectState;
			}
			set
			{
				NetworkConnectState networkConnectState = this.connectState;
				this.connectState = value;
				if (networkConnectState == NetworkConnectState.Connected && this.connectState == NetworkConnectState.NotConnected)
				{
					this.ClearInstantlyAck();
				}
			}
		}

		public bool IsConnected
		{
			get
			{
				return this.ConnectState == NetworkConnectState.Connected;
			}
		}

		public bool IsManualDisconnect
		{
			get
			{
				return this.isManualDisconnect;
			}
			protected set
			{
				this.isManualDisconnect = value;
			}
		}

		protected bool IsSending
		{
			get
			{
				return this.isSending;
			}
			set
			{
				this.isSending = value;
			}
		}

		protected static int SendUID
		{
			get
			{
				NetworkConnection.sendUID++;
				return NetworkConnection.sendUID;
			}
		}

		public NetworkConnection(ServerType type) : base(type)
		{
		}

		public void Release()
		{
			this.ClearAllData();
		}

		protected void ClearAllData()
		{
			this.isManualDisconnect = true;
			if (this.sendThread != null)
			{
				this.sendThread.Abort();
				this.sendThread = null;
			}
			this.isSending = false;
			Queue<NetBuffer> queue = this.sendQueue;
			lock (queue)
			{
				this.sendQueue.Clear();
			}
			Queue<NetBuffer> queue2 = this.sendFailedCacheQueue;
			lock (queue2)
			{
				this.sendFailedCacheQueue.Clear();
			}
			NetworkConnection.sendUID = 0;
			SortedDictionary<int, NetBuffer> sortedDictionary = this.sendNeedConfirmCache;
			lock (sortedDictionary)
			{
				this.sendNeedConfirmCache.Clear();
			}
			if (this.receiveDataBuffer != null)
			{
				this.receiveDataBuffer.ClearData();
				this.receiveDataBuffer = null;
			}
			this.receiveQueue.Clear();
		}

		public void Connect(IPEndPoint theIPEndPoint, Action theOnConnectSuccessCallBack, Action theOnConnectFailedCallBack)
		{
			Debug.Log(string.Concat(new object[]
			{
				"开始连接:: ",
				theIPEndPoint.get_Address(),
				" ",
				DateTime.get_Now()
			}));
			if (theIPEndPoint == null || (theIPEndPoint == this.ipEndPoint && this.IsConnected))
			{
				if (theOnConnectSuccessCallBack != null)
				{
					theOnConnectSuccessCallBack.Invoke();
				}
			}
			else
			{
				this.CloseSocket("Connect Try Close Old Socket", false, null);
				this.ipEndPoint = theIPEndPoint;
				this.onConnectSuccessCallBack = theOnConnectSuccessCallBack;
				this.onConnectFailedCallBack = theOnConnectFailedCallBack;
				this.BeginConnect();
			}
		}

		protected void BeginConnect()
		{
			Debug.Log("BeginConnect");
			if (this.ipEndPoint == null)
			{
				Debug.LogError("ipEndPoint == null");
				return;
			}
			this.connectedID = 0;
			this.MarkReachabilityState = Application.get_internetReachability();
			try
			{
				Debug.Log(string.Concat(new object[]
				{
					"OSSupportsIPv6: ",
					Socket.get_OSSupportsIPv6(),
					" Port: ",
					this.ipEndPoint.get_Port(),
					" ",
					this.RemotePortInUseCount(this.ipEndPoint.get_Port())
				}));
				Socket socket = new Socket(this.ipEndPoint.get_AddressFamily(), 1, 6);
				NetworkConnectionData temp = new NetworkConnectionData(socket);
				this.connectingID = temp.ID;
				this.ConnectState = NetworkConnectState.Connecting;
				this.lastConnectingTime = DateTime.get_Now();
				IAsyncResult asyncResult = socket.BeginConnect(this.ipEndPoint, new AsyncCallback(this.OnGetConnectResult), temp);
				TimerHeap.AddTimer(3000u, 0, delegate
				{
					Debug.Log("Timeout Check");
					try
					{
						object obj = NetworkConnection.connectLockObject;
						lock (obj)
						{
							if (this.connectingID != temp.ID)
							{
								Debug.Log(string.Concat(new object[]
								{
									this.ServerType,
									" Timeout Check Obsolete connectingID: ",
									this.connectingID,
									" ",
									temp.ID
								}));
							}
							else if (this.ConnectState != NetworkConnectState.Connecting)
							{
								Debug.Log(this.ServerType + " Timeout Check Obsolete connectState: " + this.ConnectState);
							}
							else
							{
								Debug.Log("-------------- Timeout!!!");
								this.CommonConnectFailed();
							}
						}
					}
					catch (Exception ex2)
					{
						Debug.LogError("Exception Free Lock On Timeout Check: " + ex2.get_Message());
					}
				});
			}
			catch (Exception ex)
			{
				this.CloseSocket("BeginConnect Failed", false, ex.get_Message());
				this.CommonConnectFailed();
			}
			finally
			{
				Debug.Log(string.Concat(new object[]
				{
					"OSSupportsIPv6 1: ",
					Socket.get_OSSupportsIPv6(),
					" Port: ",
					this.ipEndPoint.get_Port(),
					" ",
					this.RemotePortInUseCount(this.ipEndPoint.get_Port())
				}));
			}
		}

		protected int LocalPortInUseCount(int port)
		{
			if (Application.get_platform() == 8 || Application.get_platform() == null || Application.get_platform() == 1)
			{
				return -1;
			}
			int num = 0;
			IPGlobalProperties iPGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
			TcpConnectionInformation[] activeTcpConnections = iPGlobalProperties.GetActiveTcpConnections();
			TcpConnectionInformation[] array = activeTcpConnections;
			for (int i = 0; i < array.Length; i++)
			{
				TcpConnectionInformation tcpConnectionInformation = array[i];
				if (tcpConnectionInformation.get_LocalEndPoint().get_Port() == port)
				{
					num++;
				}
			}
			return num;
		}

		protected int RemotePortInUseCount(int port)
		{
			if (Application.get_platform() == 8 || Application.get_platform() == null || Application.get_platform() == 1)
			{
				return -1;
			}
			int num = 0;
			IPGlobalProperties iPGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
			TcpConnectionInformation[] activeTcpConnections = iPGlobalProperties.GetActiveTcpConnections();
			TcpConnectionInformation[] array = activeTcpConnections;
			for (int i = 0; i < array.Length; i++)
			{
				TcpConnectionInformation tcpConnectionInformation = array[i];
				if (tcpConnectionInformation.get_RemoteEndPoint().get_Port() == port)
				{
					num++;
				}
			}
			return num;
		}

		protected void OnGetConnectResult(IAsyncResult connectResult)
		{
			object obj = NetworkConnection.connectLockObject;
			lock (obj)
			{
				try
				{
					if (connectResult == null)
					{
						Debug.LogError(base.ServerType + " OnGetConnectResult connectResult == null");
						this.CommonConnectFailed();
					}
					else
					{
						Debug.Log(string.Concat(new object[]
						{
							"OnGetConnectResult: ",
							connectResult.get_CompletedSynchronously(),
							" ",
							connectResult.get_IsCompleted(),
							" ",
							DateTime.get_Now()
						}));
						NetworkConnectionData networkConnectionData = (NetworkConnectionData)connectResult.get_AsyncState();
						if (networkConnectionData == null || networkConnectionData.Connection == null || !networkConnectionData.Connection.get_Connected())
						{
							if (this.connectingID == networkConnectionData.ID)
							{
								if (networkConnectionData == null)
								{
									Debug.LogError(base.ServerType + " OnGetConnectResult resultSocket == null");
								}
								else if (networkConnectionData.Connection == null)
								{
									Debug.LogError(base.ServerType + " OnGetConnectResult resultData.Connection == null");
								}
								else if (!networkConnectionData.Connection.get_Connected())
								{
									Debug.LogError(base.ServerType + " OnGetConnectResult !resultSocket.Connected");
								}
								this.CommonConnectFailed();
							}
							else
							{
								Debug.LogError(string.Concat(new object[]
								{
									base.ServerType,
									" OnGetConnectResult waitingConnectID != resultData.ID: ",
									this.connectingID,
									" ",
									networkConnectionData.ID
								}));
								if (networkConnectionData.Connection != null)
								{
									networkConnectionData.Connection.Close();
								}
							}
						}
						else if (this.connectingID != networkConnectionData.ID)
						{
							Debug.LogError(string.Concat(new object[]
							{
								base.ServerType,
								" OnGetConnectResult waitingConnectID != resultData.ID: ",
								this.connectingID,
								" ",
								networkConnectionData.ID
							}));
							networkConnectionData.Connection.Shutdown(2);
							networkConnectionData.Connection.Close();
						}
						else
						{
							try
							{
								this.connection = networkConnectionData.Connection;
								this.connection.EndConnect(connectResult);
							}
							catch (Exception ex)
							{
								this.CloseSocket("EndConnect Failed", false, ex.get_Message());
								this.CommonConnectFailed();
								return;
							}
							Debug.Log(string.Concat(new object[]
							{
								"-----------------------------------连接成功: Time: ",
								DateTime.get_Now(),
								" LocalEndPoint: ",
								this.connection.get_LocalEndPoint()
							}));
							if (this.connection.get_LocalEndPoint() != null)
							{
								string[] array = this.connection.get_LocalEndPoint().ToString().Split(new char[]
								{
									':'
								});
								if (array.Length > 1)
								{
									int port = int.Parse(array[1]);
									Debug.Log(string.Concat(new object[]
									{
										"CheckLocalPort: ",
										array[1],
										" ",
										this.LocalPortInUseCount(port)
									}));
								}
							}
							this.connectingID = 0;
							this.connectedID = networkConnectionData.ID;
							this.ConnectState = NetworkConnectState.Connected;
							this.lastConnectedTime = DateTime.get_Now();
							this.StartReceiveData();
							if (this.sendThread == null)
							{
								this.sendThread = new Thread(new ThreadStart(this.TrySendPacket));
								this.sendThread.Start();
							}
							if (this.onConnectSuccessCallBack != null)
							{
								this.onConnectSuccessCallBack.Invoke();
							}
						}
					}
				}
				catch (Exception ex2)
				{
					Debug.LogError("Exception Free Lock On GetConnectResult: " + ex2.get_Message());
				}
			}
		}

		protected void CommonConnectFailed()
		{
			this.connectingID = 0;
			this.ConnectState = NetworkConnectState.NotConnected;
			if (this.onConnectFailedCallBack != null)
			{
				this.onConnectFailedCallBack.Invoke();
			}
			this.onConnectFailedCallBack = null;
		}

		public void DisconnectAndClearData()
		{
			this.ClearAllData();
			this.CloseSocket(base.ServerType + " Manual Disconnect And Clear Data", false, null);
		}

		public void DisconnectAndReconnect()
		{
			if (this.sendThread != null)
			{
				this.sendThread.Abort();
				this.sendThread = null;
			}
			this.CloseSocket(base.ServerType + " Manual Disconnect And Reconnect", false, null);
		}

		protected void CloseSocket(string closeMessage, bool isNotifyDisconnectToLogic, string errorMessage = null)
		{
			this.ConnectState = NetworkConnectState.NotConnected;
			if (this.receiveDataBuffer != null)
			{
				this.receiveDataBuffer = null;
			}
			if (this.connection != null)
			{
				try
				{
					Debug.Log(string.Format("{1} ClientShutDown 与服务器 {2} 断开连接，操作:{0}", closeMessage, base.ServerType, this.ipEndPoint));
					if (this.connection.get_Connected())
					{
						this.connection.Shutdown(2);
					}
					this.connection.Close();
				}
				catch (Exception ex)
				{
					Debug.LogError(string.Concat(new string[]
					{
						"ClientShutDown Failed: ",
						closeMessage,
						"\n",
						ex.get_Message(),
						"\n",
						ex.get_StackTrace()
					}));
				}
				this.connection = null;
				if (errorMessage != null)
				{
					Debug.LogError(errorMessage);
				}
				if (isNotifyDisconnectToLogic && !this.IsManualDisconnect)
				{
					this.SimulateDisconnectPacket(closeMessage + "_" + errorMessage);
				}
			}
		}

		public void TryStateDisconnect()
		{
			Debug.LogError("TryStateDisconnect");
			if (this.ConnectState == NetworkConnectState.Connected)
			{
				this.ConnectState = NetworkConnectState.NotConnected;
			}
		}

		public bool SendPacket(NetBuffer buffer)
		{
			if (base.ServerType == ServerType.Chat && EntityWorld.Instance.EntSelf != null && ClientGMManager.NetSwitch01)
			{
				Debug.Log("SendPacket");
			}
			Queue<NetBuffer> queue = this.sendQueue;
			bool result;
			lock (queue)
			{
				Queue<NetBuffer> queue2 = this.sendFailedCacheQueue;
				lock (queue2)
				{
					bool flag = buffer.PacketType == PacketType.Ping || this.IsNetworkAvailableAndConstant;
					if (this.IsConnected && flag)
					{
						if (this.sendFailedCacheQueue.get_Count() > 0 && buffer.PacketType != PacketType.Verify)
						{
							this.AddDataToCache(buffer);
							result = false;
						}
						else
						{
							if (base.ServerType == ServerType.Chat && EntityWorld.Instance.EntSelf != null && ClientGMManager.NetSwitch01)
							{
								Debug.Log("Enqueue");
							}
							this.sendQueue.Enqueue(buffer);
							this.IsSending = true;
							result = true;
						}
					}
					else
					{
						if (this.ConnectState == NetworkConnectState.Connected)
						{
							this.ConnectState = NetworkConnectState.NotConnected;
						}
						this.IsSending = false;
						this.CheckSendPacketReconnect(buffer);
						result = false;
					}
				}
			}
			return result;
		}

		protected void TrySendPacket()
		{
			while (true)
			{
				if (!this.IsSending)
				{
					Thread.Sleep(20);
				}
				this.ProcessSendQueue();
			}
		}

		public void ProcessSendQueue()
		{
			string text = string.Empty;
			try
			{
				Queue<NetBuffer> queue = this.sendQueue;
				lock (queue)
				{
					try
					{
						if (this.sendQueue.get_Count() == 0)
						{
							this.IsSending = false;
						}
						else
						{
							this.IsSending = true;
							NetBuffer netBuffer = this.sendQueue.Peek();
							short num = netBuffer.ReadShortAt(4);
							if (base.ServerType == ServerType.Chat && EntityWorld.Instance.EntSelf != null && ClientGMManager.NetSwitch01)
							{
								RemoteLogSender.Instance.SendToRemote("Chat Send", string.Empty, 3);
							}
							if (num == SettleDungeonReq.OP)
							{
								ClientGMManager.Instance.NetSendSettleReq.Add(DateTime.get_Now());
							}
							SocketError socketError;
							this.connection.Send(netBuffer.Buffer, 0, netBuffer.MemorySize, 0, ref socketError);
							if (num == SettleDungeonReq.OP)
							{
								ClientGMManager.Instance.NetSendSettleReqCode.Add(new KeyValuePair<DateTime, SocketError>(DateTime.get_Now(), socketError));
							}
							if (base.ServerType == ServerType.Chat && EntityWorld.Instance.EntSelf != null && ClientGMManager.NetSwitch01)
							{
								RemoteLogSender.Instance.SendToRemote("Chat Send: " + socketError, string.Empty, 3);
							}
							if (socketError == null)
							{
								NetBuffer netBuffer2 = this.sendQueue.Dequeue();
								try
								{
									int num2 = netBuffer2.ReadIntAt(8);
									if (num2 < 0)
									{
										SortedDictionary<int, NetBuffer> sortedDictionary = this.sendNeedConfirmCache;
										lock (sortedDictionary)
										{
											if (this.sendNeedConfirmCache.ContainsKey(-num2))
											{
												Debug.LogError(string.Concat(new object[]
												{
													"sendNeedConfirmCache Add SameKey: ",
													-num2,
													" ",
													num
												}));
											}
											else
											{
												this.sendNeedConfirmCache.Add(-num2, netBuffer2);
											}
										}
										if (num == NetworkUtility.InstantlyAckReqOpCode)
										{
											this.InstantlyAckSend(num2);
										}
										if (NetworkConnection.isOpenTest)
										{
											string key = this.connectedID + "_" + ((EntityWorld.Instance.EntSelf != null) ? EntityWorld.Instance.EntSelf.ID.ToString() : string.Empty);
											if (!NetworkConnection.allSendNeedConfirmCache.ContainsKey(key))
											{
												NetworkConnection.allSendNeedConfirmCache.Add(key, new List<NetBuffer>());
											}
											NetworkConnection.allSendNeedConfirmCache[key].Add(netBuffer2);
										}
									}
								}
								catch (Exception ex)
								{
									string text2 = text;
									text = string.Concat(new object[]
									{
										text2,
										ex.get_Message(),
										"_",
										ex.get_StackTrace(),
										"_",
										this.sendQueue == null,
										"_",
										netBuffer2 == null,
										"_",
										this.sendNeedConfirmCache == null,
										"_",
										NetworkConnection.allSendNeedConfirmCache == null
									});
								}
							}
							else if (socketError == 10053 || socketError == 10101 || socketError == 10058 || socketError == 10064 || socketError == 10050)
							{
								Debug.LogError("Final Send Failed: " + socketError);
								if (this.ConnectState == NetworkConnectState.Connected)
								{
									this.ConnectState = NetworkConnectState.NotConnected;
								}
								this.IsSending = false;
								this.CheckProcessSendQueueReconnect(netBuffer);
							}
							else
							{
								Debug.LogError("Final Send Failed: " + socketError);
							}
						}
					}
					catch (Exception ex2)
					{
						string text2 = text;
						text = string.Concat(new object[]
						{
							text2,
							ex2.get_Message(),
							"_",
							ex2.get_StackTrace(),
							"_",
							this.sendQueue == null,
							"_",
							this.sendNeedConfirmCache == null,
							"_",
							NetworkConnection.allSendNeedConfirmCache == null
						});
					}
				}
			}
			catch (Exception ex3)
			{
				this.CloseSocket("ProcessSendQueue Failed", true, string.Concat(new object[]
				{
					ex3.get_Message(),
					"_",
					ex3.get_StackTrace(),
					"_",
					this.sendQueue == null,
					"_",
					text
				}));
			}
		}

		protected void CheckSendPacketReconnect(NetBuffer buffer)
		{
			Debug.Log(string.Concat(new object[]
			{
				"发包检查重连: ",
				base.ServerType,
				" ",
				buffer.PacketType
			}));
			if (this.IsManualDisconnect)
			{
				return;
			}
			PacketType packetType = buffer.PacketType;
			if (packetType != PacketType.Verify)
			{
				if (packetType != PacketType.Heartbeat)
				{
					this.AddDataToCache(buffer);
					this.OnCheckBeginReconnect();
				}
				else
				{
					this.OnCheckBeginReconnect();
				}
			}
			else
			{
				this.OnVerifyReqNotConnected();
			}
		}

		protected void AddDataToCache(NetBuffer buffer)
		{
			if (base.ServerType == ServerType.Chat && EntityWorld.Instance.EntSelf != null && ClientGMManager.NetSwitch01)
			{
				Debug.Log("AddDataToCache");
			}
			if (base.ServerType != ServerType.Data)
			{
				return;
			}
			Queue<NetBuffer> queue = this.sendFailedCacheQueue;
			lock (queue)
			{
				if (buffer.PacketType == PacketType.Data)
				{
					this.sendFailedCacheQueue.Enqueue(buffer);
				}
			}
		}

		protected void CheckProcessSendQueueReconnect(NetBuffer buffer)
		{
			if (this.IsManualDisconnect)
			{
				return;
			}
			try
			{
				PacketType packetType = buffer.PacketType;
				if (packetType != PacketType.Verify)
				{
					if (packetType != PacketType.Heartbeat)
					{
						this.SetSendQueueDataToCache();
						this.OnCheckBeginReconnect();
					}
					else
					{
						this.sendQueue.Dequeue();
						this.SetSendQueueDataToCache();
						this.OnCheckBeginReconnect();
					}
				}
				else
				{
					this.sendQueue.Dequeue();
					this.SetSendQueueDataToCache();
					this.OnVerifyReqNotConnected();
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(string.Concat(new object[]
				{
					ex.get_Message(),
					"_",
					ex.get_StackTrace(),
					"_",
					buffer == null,
					"_",
					this.sendQueue == null
				}));
			}
		}

		protected void SetSendQueueDataToCache()
		{
			if (base.ServerType == ServerType.Chat && EntityWorld.Instance.EntSelf != null && ClientGMManager.NetSwitch01)
			{
				Debug.Log("SetSendQueueDataToCache");
			}
			if (base.ServerType != ServerType.Data)
			{
				return;
			}
			Queue<NetBuffer> queue = this.sendFailedCacheQueue;
			lock (queue)
			{
				Queue<NetBuffer> queue2 = this.sendQueue;
				lock (queue2)
				{
					Debug.LogError("SetDataToCache:" + this.sendQueue.get_Count());
					NetBuffer[] array = this.sendFailedCacheQueue.ToArray();
					this.sendFailedCacheQueue.Clear();
					if (this.sendQueue.get_Count() > 0)
					{
						NetBuffer[] array2 = this.sendQueue.ToArray();
						this.sendQueue.Clear();
						if (array2.Length > 0)
						{
							for (int i = 0; i < array2.Length; i++)
							{
								if (array2[i].PacketType == PacketType.Data)
								{
									this.sendFailedCacheQueue.Enqueue(array2[i]);
								}
							}
						}
					}
					if (array.Length > 0)
					{
						for (int j = 0; j < array.Length; j++)
						{
							if (array[j].PacketType == PacketType.Data)
							{
								this.sendFailedCacheQueue.Enqueue(array[j]);
							}
						}
					}
				}
			}
		}

		protected void OnVerifyReqNotConnected()
		{
			Debug.Log("验证发送断开: " + base.ServerType);
			ReconnectManager.Instance.VerifyReqNotConnected();
		}

		protected void OnCheckBeginReconnect()
		{
			if (ReconnectManager.Instance.IsSendingDataServerVerify)
			{
				Debug.Log("验证返回等待中断开: " + base.ServerType);
				ReconnectManager.Instance.VerifyResNotConnected();
			}
			else if (!ReconnectManager.Instance.IsReconnectingDataServer)
			{
				Debug.Log("开始重连: " + base.ServerType);
				ReconnectManager.Instance.BeginReconnect(this.ipEndPoint, base.ServerType, null, null);
			}
		}

		public void SendCacheData(NetBuffer ackPacket = null)
		{
			NetBuffer[] array = null;
			NetBuffer[] array2 = null;
			SortedDictionary<int, NetBuffer> sortedDictionary = this.sendNeedConfirmCache;
			lock (sortedDictionary)
			{
				array = Enumerable.ToArray<NetBuffer>(this.sendNeedConfirmCache.get_Values());
			}
			Queue<NetBuffer> queue = this.sendFailedCacheQueue;
			lock (queue)
			{
				array2 = this.sendFailedCacheQueue.ToArray();
				this.sendFailedCacheQueue.Clear();
			}
			if (ackPacket != null)
			{
				Debug.Log("SendAckPacket->code: " + ackPacket.ReadShortAt(4));
				this.SendPacket(ackPacket);
			}
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					Debug.Log(string.Concat(new object[]
					{
						"SendNeedConfirmCache->code: ",
						array[i].ReadShortAt(4),
						" ",
						array[i].MemorySize
					}));
					this.SendPacket(array[i]);
				}
			}
			if (array2 != null)
			{
				for (int j = 0; j < array2.Length; j++)
				{
					Debug.Log(string.Concat(new object[]
					{
						"SendCacheData->code: ",
						array2[j].ReadShortAt(4),
						" ",
						array2[j].MemorySize
					}));
					this.SendPacket(array2[j]);
				}
			}
		}

		public int TryGetAckNumber(Type dataType)
		{
			if (NetworkUtility.NeedConfirmAckNumber.ContainsKey(dataType))
			{
				return -NetworkConnection.SendUID;
			}
			return 0;
		}

		protected void RemoveCacheByServerConfirmKey(int ackNumber)
		{
			SortedDictionary<int, NetBuffer> sortedDictionary = this.sendNeedConfirmCache;
			lock (sortedDictionary)
			{
				this.lastSendAckNumber = ackNumber;
				List<int> list = new List<int>();
				using (SortedDictionary<int, NetBuffer>.Enumerator enumerator = this.sendNeedConfirmCache.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<int, NetBuffer> current = enumerator.get_Current();
						if (current.get_Key() <= ackNumber)
						{
							list.Add(current.get_Key());
						}
					}
				}
				for (int i = 0; i < list.get_Count(); i++)
				{
					this.sendNeedConfirmCache.Remove(list.get_Item(i));
				}
			}
		}

		protected void InstantlyAckSend(int ackNumber)
		{
			XDict<int, float> xDict = this.needInstantlyAckNumbers;
			lock (xDict)
			{
				if (this.IsConnected && !this.needInstantlyAckNumbers.ContainsKey(ackNumber))
				{
					this.needInstantlyAckNumbers.Add(ackNumber, 5f);
				}
			}
		}

		protected void InstantlyAckReceived(int ackNumber)
		{
			XDict<int, float> xDict = this.needInstantlyAckNumbers;
			lock (xDict)
			{
				List<int> list = new List<int>();
				for (int i = 0; i < this.needInstantlyAckNumbers.Keys.get_Count(); i++)
				{
					if (this.needInstantlyAckNumbers.Keys.get_Item(i) >= ackNumber)
					{
						list.Add(this.needInstantlyAckNumbers.Keys.get_Item(i));
					}
				}
				for (int j = 0; j < list.get_Count(); j++)
				{
					this.needInstantlyAckNumbers.Remove(list.get_Item(j));
				}
			}
		}

		protected void ClearInstantlyAck()
		{
			XDict<int, float> xDict = this.needInstantlyAckNumbers;
			lock (xDict)
			{
				this.needInstantlyAckNumbers.Clear();
			}
		}

		public void ProcessInstantlyAckCheck(float deltaTime)
		{
			bool flag = false;
			int num = 0;
			XDict<int, float> xDict = this.needInstantlyAckNumbers;
			lock (xDict)
			{
				if (this.needInstantlyAckNumbers.Count == 0)
				{
					return;
				}
				for (int i = 0; i < this.needInstantlyAckNumbers.Keys.get_Count(); i++)
				{
					if (this.needInstantlyAckNumbers[this.needInstantlyAckNumbers.Keys.get_Item(i)] <= deltaTime)
					{
						flag = true;
						num = this.needInstantlyAckNumbers.Keys.get_Item(i);
						break;
					}
					XDict<int, float> xDict2;
					XDict<int, float> expr_55 = xDict2 = this.needInstantlyAckNumbers;
					int key;
					int expr_69 = key = this.needInstantlyAckNumbers.Keys.get_Item(i);
					float num2 = xDict2[key];
					expr_55[expr_69] = num2 - deltaTime;
				}
			}
			if (flag)
			{
				Debug.LogError("ProcessInstantlyAckCheck Timeout: " + num);
				this.TryStateDisconnect();
			}
		}

		public void StartReceiveData()
		{
			if (this.connection == null)
			{
				return;
			}
			if (!this.connection.get_Connected())
			{
				return;
			}
			this.ipEndPoint = (IPEndPoint)this.connection.get_RemoteEndPoint();
			try
			{
				this.connection.BeginReceive(this.receiveCache, 0, this.receiveCache.Length, 0, new AsyncCallback(this.OnReceiveData), this.connection);
			}
			catch (Exception ex)
			{
				this.CloseSocket("StartReceiveData Failed", true, ex.get_Message());
			}
		}

		protected void OnReceiveData(IAsyncResult result)
		{
			Socket socket = result.get_AsyncState() as Socket;
			if (socket == null)
			{
				return;
			}
			if (socket != this.connection)
			{
				return;
			}
			int num = 0;
			try
			{
				SocketError socketError;
				num = this.connection.EndReceive(result, ref socketError);
			}
			catch (SocketException ex)
			{
				this.CloseSocket("EndReceive Failed", false, ex.get_Message());
				Debug.LogError("EndReceive Failed ErrorCode: " + ex.get_SocketErrorCode());
				if (!this.IsManualDisconnect)
				{
					if (ex.get_SocketErrorCode() == 10054)
					{
						Debug.LogError("服务器断开连接，原因是重置连接");
					}
					else
					{
						ReconnectManager.Instance.BeginReconnect(this.ipEndPoint, base.ServerType, null, null);
					}
				}
				return;
			}
			catch (Exception ex2)
			{
				this.CloseSocket("EndReceive Failed", false, ex2.get_Message());
				if (!this.IsManualDisconnect)
				{
					ReconnectManager.Instance.BeginReconnect(this.ipEndPoint, base.ServerType, null, null);
				}
				return;
			}
			if (num == 0)
			{
				if (!this.IsConnected)
				{
					return;
				}
				try
				{
					this.connection.BeginReceive(this.receiveCache, 0, this.receiveCache.Length, 0, new AsyncCallback(this.OnReceiveData), this.connection);
				}
				catch (Exception ex3)
				{
					this.CloseSocket("Re BeginReceive Failed", true, ex3.get_Message());
				}
			}
			else
			{
				this.WriteToReceiveCache(num);
				if (this.ProcessReceiveCache())
				{
					if (!this.IsConnected)
					{
						return;
					}
					try
					{
						this.connection.BeginReceive(this.receiveCache, 0, this.receiveCache.Length, 0, new AsyncCallback(this.OnReceiveData), this.connection);
					}
					catch (Exception ex4)
					{
						this.CloseSocket("Re BeginReceive Failed", true, ex4.get_Message());
					}
				}
				else
				{
					this.CloseSocket("ProcessReceiveCache Failed", true, null);
				}
			}
		}

		protected void WriteToReceiveCache(int byteCount)
		{
			if (this.receiveDataBuffer == null)
			{
				this.receiveDataBuffer = NetBuffer.Create();
				Debug.Log(string.Concat(new object[]
				{
					"receiveDataBuffer: ",
					this.receiveDataBuffer.thisIndex,
					" ",
					base.ServerType
				}));
				this.receiveDataBuffer.GetWriter(NetBuffer.WriterMode.Begin).Write(this.receiveCache, 0, byteCount);
			}
			else
			{
				this.receiveDataBuffer.GetWriter(NetBuffer.WriterMode.Continue).Write(this.receiveCache, 0, byteCount);
			}
		}

		protected bool ProcessReceiveCache()
		{
			int i = this.receiveDataBuffer.CurrentPos - this.readBeginOffset;
			while (i >= 4)
			{
				if (this.readExpectedSize == 0)
				{
					this.readExpectedSize = this.receiveDataBuffer.ReadIntAt(this.readBeginOffset);
					if (this.readExpectedSize < 0 || this.readExpectedSize > 16777216)
					{
						this.CloseSocket("ProcessReceiveCache Out Of Expected", true, null);
						return false;
					}
				}
				if (i == this.readExpectedSize)
				{
					this.CreateDataPacketFromReceiveBuffer();
					this.receiveDataBuffer.ClearData();
					this.readBeginOffset = 0;
					this.readExpectedSize = 0;
					break;
				}
				if (i <= this.readExpectedSize)
				{
					if (!this.IsConnected)
					{
						this.receiveDataBuffer.ClearData();
						this.readBeginOffset = 0;
						this.readExpectedSize = 0;
					}
					break;
				}
				this.CreateDataPacketFromReceiveBuffer();
				i -= this.readExpectedSize;
				this.readBeginOffset += this.readExpectedSize;
				this.readExpectedSize = 0;
			}
			return true;
		}

		protected void CreateDataPacketFromReceiveBuffer()
		{
			short num = this.receiveDataBuffer.ReadShortAt(this.readBeginOffset + 4);
			if (num == NetworkUtility.PingResOpCode)
			{
				NetworkService.Instance.OnPingRes();
				return;
			}
			if (num == NetworkUtility.AckNtyOpCode)
			{
				Debug.Log(string.Concat(new object[]
				{
					"AckNty: ",
					this.receiveDataBuffer.ReadShortAt(8),
					" ",
					DateTime.get_Now()
				}));
			}
			NetBuffer netBuffer = NetBuffer.Create();
			netBuffer.GetWriter(NetBuffer.WriterMode.Begin).Write(this.receiveDataBuffer.Buffer, this.readBeginOffset, this.readExpectedSize);
			netBuffer.EndWrite();
			this.AddToReceiveQueue(netBuffer);
		}

		protected void AddToReceiveQueue(NetBuffer buffer)
		{
			Queue<NetBuffer> queue = this.receiveQueue;
			lock (queue)
			{
				this.receiveQueue.Enqueue(buffer);
			}
		}

		public void ProcessReceiveQueue()
		{
			Queue<NetBuffer> queue = this.receiveQueue;
			lock (queue)
			{
				NetBuffer buffer = null;
				bool flag = true;
				int num = 0;
				while (flag && this.GetPacketFromReceiveQueue(out buffer))
				{
					this.ProcessPacket(buffer);
					num++;
					flag = (num < 2147483647);
					buffer = null;
				}
			}
		}

		protected bool GetPacketFromReceiveQueue(out NetBuffer buffer)
		{
			if (this.receiveQueue.get_Count() != 0)
			{
				Queue<NetBuffer> queue = this.receiveQueue;
				lock (queue)
				{
					buffer = this.receiveQueue.Dequeue();
					return true;
				}
			}
			buffer = null;
			return false;
		}

		protected bool ProcessPacket(NetBuffer buffer)
		{
			if (buffer.MemorySize == 0 && buffer.PacketType == PacketType.Data)
			{
				Debug.LogError("ProcessPacket Failed");
				return false;
			}
			switch (buffer.PacketType)
			{
			case PacketType.Data:
				this.HandleData(buffer);
				return true;
			case PacketType.Error:
				this.HandleDataError(buffer.GetReader(0).ReadString());
				return false;
			case PacketType.Disconnect:
				this.HandleDataDisconnect(buffer.FuckStr);
				return false;
			case PacketType.ReconnectResult:
				this.HandleDataReconnect(buffer.GetReader(0).ReadBoolean());
				return false;
			}
			Debug.LogError("无法解析该包：" + buffer.PacketType);
			return false;
		}

		protected void HandleData(NetBuffer buffer)
		{
			int num = buffer.ReadIntAt(8);
			if (!this.CheckIsActNumberLegal(num))
			{
				return;
			}
			float realtimeSinceStartup = Time.get_realtimeSinceStartup();
			if (num != 0 && (this.lastReceiveAckNumber % 100 == 0 || realtimeSinceStartup - this.lastApplyReceiveAckTime >= 20f))
			{
				NetBuffer netBuffer = NetBuffer.Create();
				netBuffer.WriteAckPacket(buffer.Buffer);
				this.SendPacket(netBuffer);
				this.lastApplyReceiveAckTime = realtimeSinceStartup;
			}
			short opCode = 0;
			object[] parameters = buffer.TryReadAsDataPacket(out opCode);
			NetworkService.Instance.HandlePacketData(opCode, parameters);
		}

		protected bool CheckIsActNumberLegal(int thisAckNumber)
		{
			if (this.lastReceiveAckNumber == -1 && thisAckNumber > 0)
			{
				this.lastReceiveAckNumber = thisAckNumber;
				return true;
			}
			if (thisAckNumber > 0)
			{
				if (this.lastReceiveAckNumber < thisAckNumber)
				{
					if (this.lastReceiveAckNumber != thisAckNumber - 1)
					{
						Debug.LogError(string.Format("在序号{0}和{1}之间丢包，请迅速通知程序: {2},{3}", new object[]
						{
							this.lastReceiveAckNumber,
							thisAckNumber,
							this.betweenCount1,
							this.betweenCount2
						}));
					}
					this.lastReceiveAckNumber = thisAckNumber;
				}
				else
				{
					if (this.lastReceiveAckNumber != 2100000000)
					{
						Debug.Log(string.Format("重连接收到已处理的旧包{0}，抛弃", thisAckNumber));
						this.betweenCount2++;
						return false;
					}
					if (thisAckNumber != 1)
					{
						Debug.LogError(string.Format("在序号{0}和{1}之间丢包，请迅速通知程序: {2},{3}", new object[]
						{
							this.lastReceiveAckNumber,
							thisAckNumber,
							this.betweenCount1,
							this.betweenCount2
						}));
					}
					this.lastReceiveAckNumber = thisAckNumber;
				}
				this.betweenCount1 = 0;
				this.betweenCount2 = 0;
			}
			else
			{
				if (thisAckNumber < 0)
				{
					this.InstantlyAckReceived(thisAckNumber);
					this.RemoveCacheByServerConfirmKey(-thisAckNumber);
					return false;
				}
				this.betweenCount1++;
			}
			return true;
		}

		protected void HandleDataError(string msg)
		{
			NetworkService.Instance.HandlePacketError(base.ServerType, msg);
		}

		protected void HandleDataDisconnect(string msg)
		{
			Debug.LogError(string.Concat(new object[]
			{
				"DataDisconnect:",
				base.ServerType,
				"_",
				msg
			}));
			NetworkService.Instance.HandlePacketDisconnect(base.ServerType);
		}

		protected void HandleDataReconnect(bool success)
		{
			NetworkService.Instance.HandlePacketReconnect(base.ServerType, success);
		}

		protected void SimulateErrorPacket(string msg)
		{
			Debug.LogError(base.ServerType + " SimulateErrorPacket: " + msg);
			NetBuffer netBuffer = NetBuffer.Create();
			netBuffer.PacketType = PacketType.Error;
			netBuffer.WriteErrorPacket(msg);
			this.AddToReceiveQueue(netBuffer);
		}

		protected void SimulateDisconnectPacket(string fuck)
		{
			Debug.LogError(base.ServerType + " SimulateDisconnectPacket");
			NetBuffer netBuffer = NetBuffer.Create();
			netBuffer.PacketType = PacketType.Disconnect;
			netBuffer.WriteDisconnectPacket();
			netBuffer.FuckStr = fuck;
			this.AddToReceiveQueue(netBuffer);
		}

		protected void SimulateReconnectResultPacket(bool isSuccess)
		{
			Debug.LogError(base.ServerType + " SimulateReconnectResultPacket: " + isSuccess);
			NetBuffer netBuffer = NetBuffer.Create();
			netBuffer.PacketType = PacketType.ReconnectResult;
			netBuffer.WriteReconnectPacket(isSuccess);
			this.AddToReceiveQueue(netBuffer);
		}

		protected void HandleSendSocketError(SocketError socketErrorCode, NetBuffer sendData, Action successCallBack = null)
		{
			if (this.IsManualDisconnect)
			{
				return;
			}
			switch (socketErrorCode)
			{
			case 10050:
			case 10053:
				goto IL_7A;
			case 10051:
			case 10052:
				IL_2E:
				if (socketErrorCode == null)
				{
					if (successCallBack != null)
					{
						successCallBack.Invoke();
					}
					return;
				}
				if (socketErrorCode != 10058 && socketErrorCode != 10064 && socketErrorCode != 10101)
				{
					return;
				}
				goto IL_7A;
			case 10054:
				Debug.LogError("服务器断开连接");
				return;
			}
			goto IL_2E;
			IL_7A:
			if (this.ConnectState == NetworkConnectState.Connected)
			{
				this.ConnectState = NetworkConnectState.NotConnected;
			}
			this.IsSending = false;
			this.CheckProcessSendQueueReconnect(sendData);
		}

		protected void HandleDefaultSocketError(SocketError socketErrorCode, Action successCallBack = null)
		{
			if (this.IsManualDisconnect)
			{
				return;
			}
			if (socketErrorCode != null)
			{
				if (socketErrorCode != 10054)
				{
					ReconnectManager.Instance.BeginReconnect(this.ipEndPoint, base.ServerType, null, null);
				}
				else
				{
					Debug.LogError("服务器断开连接");
				}
			}
			else if (successCallBack != null)
			{
				successCallBack.Invoke();
			}
		}

		public string GetStateString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ServerType: ");
			stringBuilder.Append(base.ServerType);
			stringBuilder.Append("  ");
			if (this.connection == null)
			{
				stringBuilder.Append("socket == null  ");
			}
			else
			{
				stringBuilder.Append("socket.Available: ");
				stringBuilder.Append(this.connection.get_Available());
				stringBuilder.Append("  ");
				stringBuilder.Append("socket.Connected: ");
				stringBuilder.Append(this.connection.get_Connected());
				stringBuilder.Append("  ");
				stringBuilder.Append("socket.Blocking: ");
				stringBuilder.Append(this.connection.get_Blocking());
				stringBuilder.Append("  ");
			}
			stringBuilder.Append("Application.internetReachability: ");
			stringBuilder.Append(Application.get_internetReachability());
			stringBuilder.Append("  ");
			stringBuilder.Append("NetworkState: ");
			stringBuilder.Append(this.ConnectState);
			stringBuilder.Append("  ");
			stringBuilder.Append("IsManualDisconnect: ");
			stringBuilder.Append(this.IsManualDisconnect);
			stringBuilder.Append("  ");
			stringBuilder.Append("sendQueue: ");
			stringBuilder.Append(this.sendQueue.get_Count());
			stringBuilder.Append("  ");
			stringBuilder.Append("sendCacheQueue: ");
			stringBuilder.Append(this.sendFailedCacheQueue.get_Count());
			stringBuilder.Append("  ");
			stringBuilder.Append("IsSending: ");
			stringBuilder.Append(this.IsSending);
			stringBuilder.Append("  ");
			stringBuilder.Append("receiveQueue: ");
			stringBuilder.Append(this.receiveQueue.get_Count());
			stringBuilder.Append("  ");
			stringBuilder.Append("connectingID: ");
			stringBuilder.Append(this.connectingID);
			stringBuilder.Append("  ");
			stringBuilder.Append("connectedID: ");
			stringBuilder.Append(this.connectedID);
			stringBuilder.Append("  ");
			stringBuilder.Append("lastConnectingTime: ");
			stringBuilder.Append(this.lastConnectingTime);
			stringBuilder.Append("  ");
			stringBuilder.Append("lastConnectedTime: ");
			stringBuilder.Append(this.lastConnectedTime);
			stringBuilder.Append("  ");
			return stringBuilder.ToString();
		}

		public string GetAckCacheString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("GetAckCacheString: ");
			int num = 0;
			using (SortedDictionary<int, NetBuffer>.Enumerator enumerator = this.sendNeedConfirmCache.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, NetBuffer> current = enumerator.get_Current();
					stringBuilder.Append("{");
					stringBuilder.Append(current.get_Key());
					stringBuilder.Append("=>");
					stringBuilder.Append(current.get_Value().ReadShortAt(8));
					stringBuilder.Append(",");
					short num2 = current.get_Value().ReadShortAt(4);
					stringBuilder.Append(num2);
					if (num2 == NetworkUtility.InstantlyAckReqOpCode)
					{
						stringBuilder.Append("=>");
						stringBuilder.Append(current.get_Value().ReadShortAt(24));
					}
					stringBuilder.Append("}");
					if (num % 5 == 4)
					{
						num = 0;
						stringBuilder.Append("\n");
					}
					else
					{
						num++;
					}
				}
			}
			stringBuilder.Append("\n");
			stringBuilder.Append("sendLastAckNumber: ");
			stringBuilder.Append(this.lastSendAckNumber);
			stringBuilder.Append(" sendUID: ");
			stringBuilder.Append(NetworkConnection.sendUID);
			stringBuilder.Append("\n");
			stringBuilder.Append("=========================================");
			stringBuilder.Append("\n");
			return stringBuilder.ToString();
		}

		public static string GetAllAckCacheString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("GetAllAckCacheString: ");
			for (int i = 0; i < NetworkConnection.allSendNeedConfirmCache.Count; i++)
			{
				stringBuilder.Append("ID: ");
				stringBuilder.Append(NetworkConnection.allSendNeedConfirmCache.ElementKeyAt(i));
				stringBuilder.Append("\n");
				List<NetBuffer> list = NetworkConnection.allSendNeedConfirmCache.ElementValueAt(i);
				for (int j = 0; j < list.get_Count(); j++)
				{
					stringBuilder.Append("{");
					stringBuilder.Append(list.get_Item(j).ReadShortAt(8));
					stringBuilder.Append(",");
					short num = list.get_Item(j).ReadShortAt(4);
					stringBuilder.Append(num);
					if (num == NetworkUtility.InstantlyAckReqOpCode)
					{
						stringBuilder.Append("=>");
						stringBuilder.Append(list.get_Item(j).ReadShortAt(24));
					}
					stringBuilder.Append("}");
					if (j % 5 == 4)
					{
						stringBuilder.Append("\n");
					}
				}
				stringBuilder.Append("\n");
				stringBuilder.Append("=========================================");
				stringBuilder.Append("\n");
			}
			return stringBuilder.ToString();
		}
	}
}

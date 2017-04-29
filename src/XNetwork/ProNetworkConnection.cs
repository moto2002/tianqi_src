using Package;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using UnityEngine;

namespace XNetwork
{
	public class ProNetworkConnection : NetworkBase
	{
		protected const int MaxAckNumber = 2100000000;

		public TcpClient socket;

		protected NetworkStream socketStream;

		protected IPEndPoint ipEndPoint;

		protected NetworkReachability markReachabilityState;

		public NetworkConnectState state;

		protected Action onConnectSuccessCallBack;

		protected Action onConnectFailedCallBack;

		protected bool isManualDisconnect;

		protected Queue<NetBuffer> sendQueue = new Queue<NetBuffer>();

		protected Queue<NetBuffer> sendCacheQueue = new Queue<NetBuffer>();

		protected bool isSending;

		protected byte[] receiveCache = new byte[8192];

		protected NetBuffer receiveDataBuffer;

		protected int readBeginOffset;

		protected int readExpectedSize;

		protected int lastAckNumber = -1;

		protected float lastSendAckTime;

		protected int betweenCount1;

		protected int betweenCount2;

		protected Queue<NetBuffer> receiveQueue = new Queue<NetBuffer>();

		public IPEndPoint EndPoint
		{
			get
			{
				return this.ipEndPoint;
			}
		}

		protected NetworkReachability MarkReachabilityState
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

		public bool IsConnected
		{
			get
			{
				return this.state == NetworkConnectState.Connected;
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

		public ProNetworkConnection(ServerType type) : base(type)
		{
		}

		public void Release()
		{
			this.isManualDisconnect = true;
			this.sendQueue.Clear();
			this.sendCacheQueue.Clear();
			if (this.receiveDataBuffer != null)
			{
				this.receiveDataBuffer.ClearData();
				this.receiveDataBuffer = null;
			}
			this.receiveQueue.Clear();
		}

		public void Connect(IPEndPoint theIPEndPoint, Action theOnConnectSuccessCallBack, Action theOnConnectFailedCallBack)
		{
			Debug.LogError(string.Concat(new object[]
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
			Debug.LogError("BeginConnect");
			if (this.ipEndPoint == null)
			{
				Debug.LogError("ipEndPoint == null");
				return;
			}
			this.state = NetworkConnectState.Connecting;
			this.MarkReachabilityState = Application.get_internetReachability();
			try
			{
				Debug.LogError(string.Concat(new object[]
				{
					"OSSupportsIPv6: ",
					Socket.get_OSSupportsIPv6(),
					" Port: ",
					this.ipEndPoint.get_Port(),
					" ",
					this.RemotePortInUseCount(this.ipEndPoint.get_Port())
				}));
				this.socket = new TcpClient(2);
				IAsyncResult asyncResult = this.socket.BeginConnect(this.ipEndPoint.get_Address(), this.ipEndPoint.get_Port(), new AsyncCallback(this.OnGetConnectResult), this.socket);
			}
			catch (Exception ex)
			{
				this.CloseSocket("BeginConnect Failed", false, ex.get_Message());
				if (this.onConnectFailedCallBack != null)
				{
					this.onConnectFailedCallBack.Invoke();
				}
			}
			finally
			{
				Debug.LogError(string.Concat(new object[]
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
			Debug.Log(string.Concat(new object[]
			{
				"OnGetConnectResult: ",
				connectResult.get_CompletedSynchronously(),
				" ",
				connectResult.get_IsCompleted(),
				" ",
				DateTime.get_Now()
			}));
			if (connectResult == null)
			{
				this.state = NetworkConnectState.NotConnected;
				if (this.onConnectFailedCallBack != null)
				{
					this.onConnectFailedCallBack.Invoke();
				}
				return;
			}
			TcpClient tcpClient = (TcpClient)connectResult.get_AsyncState();
			if (tcpClient == null || this.socket == null || tcpClient != this.socket || !tcpClient.get_Connected())
			{
				this.state = NetworkConnectState.NotConnected;
				if (this.onConnectFailedCallBack != null)
				{
					this.onConnectFailedCallBack.Invoke();
				}
				return;
			}
			try
			{
				tcpClient.EndConnect(connectResult);
				this.socketStream = tcpClient.GetStream();
			}
			catch (Exception ex)
			{
				this.CloseSocket("EndConnect Failed", false, ex.get_Message());
				if (this.onConnectFailedCallBack != null)
				{
					this.onConnectFailedCallBack.Invoke();
				}
				return;
			}
			Debug.Log(string.Concat(new object[]
			{
				"-----------------------------------连接成功: Time: ",
				DateTime.get_Now(),
				" LocalEndPoint: ",
				this.socket.get_Client().get_LocalEndPoint()
			}));
			if (this.socket.get_Client().get_LocalEndPoint() != null)
			{
				string[] array = this.socket.get_Client().get_LocalEndPoint().ToString().Split(new char[]
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
			this.state = NetworkConnectState.Connected;
			this.StartReceiveData();
			if (this.onConnectSuccessCallBack != null)
			{
				this.onConnectSuccessCallBack.Invoke();
			}
		}

		public void DisconnectAndClearData()
		{
			this.IsManualDisconnect = true;
			this.sendQueue.Clear();
			this.sendCacheQueue.Clear();
			if (this.receiveDataBuffer != null)
			{
				this.receiveDataBuffer.ClearData();
				this.receiveDataBuffer = null;
			}
			this.receiveQueue.Clear();
			this.CloseSocket(base.ServerType + " Manual Disconnect And Clear Data", false, null);
		}

		public void DisconnectAndReconnect()
		{
			this.CloseSocket(base.ServerType + " Manual Disconnect And Reconnect", false, null);
		}

		protected void CloseSocket(string closeMessage, bool isNotifyDisconnectToLogic, string errorMessage = null)
		{
			this.state = NetworkConnectState.NotConnected;
			if (this.receiveDataBuffer != null)
			{
				this.receiveDataBuffer = null;
			}
			if (this.socket != null)
			{
				try
				{
					Debug.LogError(string.Format("{1} ClientShutDown 与服务器 {2} 断开连接，操作:{0}", closeMessage, base.ServerType, this.ipEndPoint));
					this.socketStream.Dispose();
					this.socketStream.Close();
					this.socket.Close();
				}
				catch (Exception ex)
				{
					Debug.LogError("ClientShutDown Failed: " + closeMessage + "\n" + ex.get_Message());
				}
				this.socket = null;
				if (errorMessage != null)
				{
					Debug.LogError(errorMessage);
				}
				if (isNotifyDisconnectToLogic && !this.IsManualDisconnect)
				{
					this.SimulateDisconnectPacket();
				}
			}
		}

		public bool SendPacket(NetBuffer buffer)
		{
			Queue<NetBuffer> queue = this.sendQueue;
			bool result;
			lock (queue)
			{
				Queue<NetBuffer> queue2 = this.sendCacheQueue;
				lock (queue2)
				{
					bool flag = buffer.PacketType == PacketType.Ping || this.IsNetworkAvailableAndConstant;
					if (this.IsConnected && flag)
					{
						if (this.sendCacheQueue.get_Count() > 0 && buffer.PacketType != PacketType.Verify)
						{
							this.AddDataToCache(buffer);
							result = false;
						}
						else
						{
							this.sendQueue.Enqueue(buffer);
							result = true;
						}
					}
					else
					{
						this.state = NetworkConnectState.NotConnected;
						this.CheckSendPacketReconnect(buffer);
						result = false;
					}
				}
			}
			return result;
		}

		public void ProcessSendQueue()
		{
			try
			{
				if (!this.IsSending)
				{
					Queue<NetBuffer> queue = this.sendQueue;
					lock (queue)
					{
						if (this.sendQueue.get_Count() != 0)
						{
							this.IsSending = true;
							NetBuffer netBuffer = this.sendQueue.Peek();
							SocketError socketError = 0;
							this.socketStream.BeginWrite(netBuffer.Buffer, 0, netBuffer.MemorySize, new AsyncCallback(this.OnSendData), this.socket);
							if (socketError != null)
							{
								if (socketError == 10053 || socketError == 10101 || socketError == 10058 || socketError == 10064 || socketError == 10050)
								{
									Debug.LogError("Final Send Failed: " + socketError);
									this.state = NetworkConnectState.NotConnected;
									this.CheckProcessSendQueueReconnect(netBuffer);
								}
								else
								{
									Debug.LogError("Final Send Failed: " + socketError);
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.CloseSocket("ProcessSendQueue Failed", true, ex.get_Message());
			}
		}

		protected void OnSendData(IAsyncResult result)
		{
			Queue<NetBuffer> queue = this.sendQueue;
			lock (queue)
			{
				TcpClient tcpClient = result.get_AsyncState() as TcpClient;
				if (tcpClient != null)
				{
					if (tcpClient == this.socket)
					{
						try
						{
							this.socketStream.EndWrite(result);
							NetBuffer netBuffer = this.sendQueue.Dequeue();
						}
						catch (Exception ex)
						{
							Debug.LogError("OnSendData Failed: " + ex.get_Message());
						}
						finally
						{
							this.IsSending = false;
						}
					}
				}
			}
		}

		protected void CheckSendPacketReconnect(NetBuffer buffer)
		{
			Debug.LogError("发包检查重连: " + base.ServerType);
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
			if (base.ServerType != ServerType.Data)
			{
				return;
			}
			Queue<NetBuffer> queue = this.sendCacheQueue;
			lock (queue)
			{
				if (buffer.PacketType == PacketType.Data)
				{
					this.sendCacheQueue.Enqueue(buffer);
				}
			}
		}

		protected void CheckProcessSendQueueReconnect(NetBuffer buffer)
		{
			if (this.IsManualDisconnect)
			{
				return;
			}
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

		protected void SetSendQueueDataToCache()
		{
			if (base.ServerType != ServerType.Data)
			{
				return;
			}
			Queue<NetBuffer> queue = this.sendCacheQueue;
			lock (queue)
			{
				Queue<NetBuffer> queue2 = this.sendQueue;
				lock (queue2)
				{
					Debug.LogError("SetDataToCache:" + this.sendQueue.get_Count());
					NetBuffer[] array = this.sendCacheQueue.ToArray();
					this.sendCacheQueue.Clear();
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
									this.sendCacheQueue.Enqueue(array2[i]);
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
								this.sendCacheQueue.Enqueue(array[j]);
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
			Queue<NetBuffer> queue = this.sendCacheQueue;
			lock (queue)
			{
				NetBuffer[] array = this.sendCacheQueue.ToArray();
				this.sendCacheQueue.Clear();
				if (ackPacket != null)
				{
					Debug.LogError("SendAckPacket->code: " + ackPacket.ReadShortAt(4));
					this.SendPacket(ackPacket);
				}
				for (int i = 0; i < array.Length; i++)
				{
					Debug.LogError(string.Concat(new object[]
					{
						"SendCacheData->code: ",
						array[i].ReadShortAt(4),
						" ",
						array[i].MemorySize
					}));
					this.SendPacket(array[i]);
				}
			}
		}

		public void StartReceiveData()
		{
			if (this.socket == null)
			{
				return;
			}
			if (!this.socket.get_Connected())
			{
				return;
			}
			this.ipEndPoint = (IPEndPoint)this.socket.get_Client().get_RemoteEndPoint();
			try
			{
				this.socketStream.BeginRead(this.receiveCache, 0, this.receiveCache.Length, new AsyncCallback(this.OnReceiveData), this.socket);
			}
			catch (Exception ex)
			{
				this.CloseSocket("StartReceiveData Failed", true, ex.get_Message());
			}
		}

		protected void OnReceiveData(IAsyncResult result)
		{
			TcpClient tcpClient = result.get_AsyncState() as TcpClient;
			if (tcpClient == null)
			{
				return;
			}
			if (tcpClient != this.socket)
			{
				return;
			}
			int num = 0;
			try
			{
				num = this.socketStream.EndRead(result);
			}
			catch (SocketException ex)
			{
				this.CloseSocket("EndReceive Failed", false, ex.get_Message());
				Debug.LogError("EndReceive Failed ErrorCode: " + ex.get_SocketErrorCode());
				if (!this.IsManualDisconnect)
				{
					if (ex.get_SocketErrorCode() == 10054)
					{
						Debug.LogError("服务器断开连接");
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
				Debug.LogError("服务器断开连接");
				this.CloseSocket(base.ServerType + " Server Shutdown: byteCount == 0", true, null);
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
						this.socketStream.BeginRead(this.receiveCache, 0, this.receiveCache.Length, new AsyncCallback(this.OnReceiveData), this.socket);
					}
					catch (Exception ex3)
					{
						this.CloseSocket("Re BeginReceive Failed", true, ex3.get_Message());
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
			if (num == PingRes.OP)
			{
				NetworkService.Instance.PingEndTime = DateTime.get_Now().get_Ticks() / 10000L;
				return;
			}
			if (num == 517)
			{
				Debug.Log("SwitchMapRes");
			}
			else if (num == 705)
			{
				Debug.LogError("GetServerListRes: " + DateTime.get_Now());
			}
			else if (num == 1115)
			{
				Debug.LogError("Get PVP Result Packet");
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
				while (flag && this.GetPacketFromReceiveQueue(out buffer))
				{
					this.ProcessPacket(buffer);
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
				this.HandleDataDisconnect();
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
			if (num != 0 && (this.lastAckNumber % 100 == 0 || realtimeSinceStartup - this.lastSendAckTime >= 20f))
			{
				NetBuffer netBuffer = NetBuffer.Create();
				netBuffer.WriteAckPacket(buffer.Buffer);
				this.SendPacket(netBuffer);
				this.lastSendAckTime = realtimeSinceStartup;
			}
			short opCode = 0;
			object[] parameters = buffer.TryReadAsDataPacket(out opCode);
			NetworkService.Instance.HandlePacketData(opCode, parameters);
		}

		protected bool CheckIsActNumberLegal(int thisAckNumber)
		{
			if (this.lastAckNumber == -1 && thisAckNumber > 0)
			{
				this.lastAckNumber = thisAckNumber;
				return true;
			}
			if (thisAckNumber > 0)
			{
				if (this.lastAckNumber < thisAckNumber)
				{
					if (this.lastAckNumber != thisAckNumber - 1)
					{
					}
					this.lastAckNumber = thisAckNumber;
				}
				else
				{
					if (this.lastAckNumber != 2100000000)
					{
						Debug.Log(string.Format("重连接收到已处理的旧包{0}，抛弃", thisAckNumber));
						this.betweenCount2++;
						return false;
					}
					if (thisAckNumber != 1)
					{
					}
					this.lastAckNumber = thisAckNumber;
				}
				this.betweenCount1 = 0;
				this.betweenCount2 = 0;
			}
			else
			{
				if (thisAckNumber < 0)
				{
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

		protected void HandleDataDisconnect()
		{
			Debug.LogError("DataDisconnect:" + base.ServerType);
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

		protected void SimulateDisconnectPacket()
		{
			Debug.LogError(base.ServerType + " SimulateDisconnectPacket");
			NetBuffer netBuffer = NetBuffer.Create();
			netBuffer.PacketType = PacketType.Disconnect;
			netBuffer.WriteDisconnectPacket();
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
			this.state = NetworkConnectState.NotConnected;
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
	}
}

using ProtoBuf;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEngine;

namespace XNetwork
{
	public class NetBuffer : IDisposable
	{
		public enum WriterMode
		{
			Begin,
			Continue
		}

		public const int HeadSize = 18;

		public const int HeadSeekPos = 0;

		public const int PacketSizeSeekPos = 0;

		public const int OpCodeSeekPos = 4;

		public const int StateSeekPos = 6;

		public const int AckNumberSeekPos = 8;

		public const int FlagSeekPos = 12;

		public const int DataSizeSeekPos = 14;

		public const int HeadInfoSeekPos = 4;

		public const int HeadInfoSize = 10;

		public const int DataSeekPos = 18;

		public const int SendPacketMaxSize = 1048576;

		public const int ReceivePacketMaxSize = 16777216;

		protected MemoryStream memoryStream;

		protected BinaryWriter writer;

		protected BinaryReader reader;

		protected bool isWriting = true;

		protected int memorySize;

		protected PacketType packetType;

		public string FuckStr;

		protected bool hasTrueDisposed;

		public static int i;

		public int thisIndex;

		public bool IsWriting
		{
			get
			{
				return this.isWriting;
			}
			protected set
			{
				this.isWriting = value;
			}
		}

		public int MemorySize
		{
			get
			{
				return this.memorySize;
			}
			protected set
			{
				this.memorySize = value;
				this.SeekPos = 0L;
			}
		}

		public int CurrentPos
		{
			get
			{
				return (!this.IsWriting) ? this.MemorySize : ((int)this.memoryStream.get_Position());
			}
		}

		public long SeekPos
		{
			set
			{
				this.memoryStream.Seek(value, 0);
			}
		}

		public byte[] Buffer
		{
			get
			{
				return this.memoryStream.GetBuffer();
			}
		}

		public PacketType PacketType
		{
			get
			{
				return this.packetType;
			}
			set
			{
				this.packetType = value;
			}
		}

		public NetBuffer()
		{
			this.thisIndex = NetBuffer.i;
			NetBuffer.i++;
			this.memoryStream = new MemoryStream();
			this.writer = new BinaryWriter(this.memoryStream);
			this.reader = new BinaryReader(this.memoryStream);
		}

		~NetBuffer()
		{
			if (NetBufferPool.Instance.EnableRecycling)
			{
				this.FakeDispose();
			}
			else
			{
				this.TrueDispose();
			}
		}

		public void Dispose()
		{
			this.TrueDispose();
			GC.SuppressFinalize(this);
		}

		public static NetBuffer Create()
		{
			return NetBufferPool.Instance.GetNetBuffer();
		}

		public static NetBuffer TrueGetNetBuffer()
		{
			return new NetBuffer();
		}

		protected void FakeDispose()
		{
			this.ClearData();
			GC.ReRegisterForFinalize(this);
			NetBufferPool.Instance.Recycle(this);
		}

		public void ClearData()
		{
			this.MemorySize = 0;
			this.PacketType = PacketType.Data;
		}

		protected void TrueDispose()
		{
			if (!this.hasTrueDisposed)
			{
				this.hasTrueDisposed = true;
				try
				{
					if (this.writer != null)
					{
						this.writer.Close();
					}
					if (this.reader != null)
					{
						this.reader.Close();
					}
					this.memoryStream.Dispose();
				}
				catch (Exception)
				{
				}
			}
		}

		public BinaryWriter GetWriter(NetBuffer.WriterMode mode = NetBuffer.WriterMode.Begin)
		{
			if (mode == NetBuffer.WriterMode.Begin || !this.IsWriting)
			{
				this.MemorySize = 0;
			}
			else if (mode != NetBuffer.WriterMode.Continue)
			{
				Debug.Log(string.Concat(new object[]
				{
					"IsWriting: ",
					this.IsWriting,
					" MemorySize: ",
					this.MemorySize
				}));
			}
			this.IsWriting = true;
			return this.writer;
		}

		public void EndWrite()
		{
			if (this.IsWriting)
			{
				this.IsWriting = false;
				this.MemorySize = (int)this.memoryStream.get_Position();
			}
		}

		public void WriteDataPacket(Type dataType, object data, int ackNumber = 0)
		{
			this.GetWriter(NetBuffer.WriterMode.Begin);
			this.WriteDataAndEndWriteData(dataType, data, ackNumber);
		}

		protected void WriteDataAndEndWriteData(Type dataType, object data, int ackNumber)
		{
			this.SeekPos = 18L;
			if (data != null)
			{
				try
				{
					Serializer.NonGeneric.Serialize(this.memoryStream, data);
				}
				catch (Exception ex)
				{
					Debug.LogError(NetBuffer.CreateStackTrace(string.Format("protobuf序列化失败，opCode为{0}", NetworkUtility.GetSendPacketsType(dataType))));
					throw ex;
				}
			}
			else
			{
				Debug.LogError(NetBuffer.CreateStackTrace(string.Format("数据包为空，opCode为{0}", NetworkUtility.GetSendPacketsType(dataType))));
			}
			if (this.memoryStream.get_Position() > 1048576L)
			{
				Debug.LogError(NetBuffer.CreateStackTrace(string.Format("数据包过大，请迅速通知程序: {0}-{1}", dataType.get_Name(), this.memoryStream.get_Position())));
			}
			else
			{
				this.WriteDataHeadAndEndWriteData(NetworkUtility.GetSendPacketsType(dataType), (int)this.memoryStream.get_Position(), ackNumber);
			}
		}

		protected void WriteDataHeadAndEndWriteData(short opCode, int packetSize, int ackNumber)
		{
			this.SeekPos = 0L;
			this.writer.Write(packetSize);
			this.writer.Write(opCode);
			short num = 0;
			this.writer.Write(num);
			this.writer.Write(ackNumber);
			short num2 = 0;
			this.writer.Write(num2);
			int num3 = packetSize - 18;
			this.writer.Write(num3);
			this.EndWriteData(packetSize);
		}

		protected void EndWriteData(int size)
		{
			if (this.IsWriting)
			{
				this.IsWriting = false;
				this.MemorySize = size;
			}
		}

		public void WriteAckPacket(byte[] data)
		{
			this.GetWriter(NetBuffer.WriterMode.Begin);
			this.writer.Write(18);
			this.writer.Write(data, 4, 10);
			this.writer.Write(0);
			this.EndWrite();
		}

		public void WritePingPacket(short code)
		{
			this.GetWriter(NetBuffer.WriterMode.Begin);
			this.WriteDataHeadAndEndWriteData(code, 18, 0);
		}

		public void WriteErrorPacket(string msg)
		{
			this.GetWriter(NetBuffer.WriterMode.Begin);
			this.writer.Write(msg);
			this.EndWrite();
		}

		public void WriteDisconnectPacket()
		{
			this.GetWriter(NetBuffer.WriterMode.Begin);
			this.EndWrite();
		}

		public void WriteReconnectPacket(bool isSuccess)
		{
			this.GetWriter(NetBuffer.WriterMode.Begin);
			this.writer.Write(isSuccess);
			this.EndWrite();
		}

		public BinaryReader GetReader(int beginSeekPos = 0)
		{
			if (this.IsWriting)
			{
				this.IsWriting = false;
			}
			this.SeekPos = (long)beginSeekPos;
			return this.reader;
		}

		public int ReadIntAt(int beginSeekPos)
		{
			long position = this.memoryStream.get_Position();
			this.SeekPos = (long)beginSeekPos;
			int result = this.reader.ReadInt32();
			this.SeekPos = position;
			return result;
		}

		public short ReadShortAt(int beginSeekPos)
		{
			long position = this.memoryStream.get_Position();
			this.SeekPos = (long)beginSeekPos;
			short result = this.reader.ReadInt16();
			this.SeekPos = position;
			return result;
		}

		public object[] TryReadAsDataPacket(out short opCode)
		{
			opCode = this.ReadShortAt(4);
			short num = this.ReadShortAt(6);
			Type recvType = NetworkUtility.GetRecvType(opCode);
			if (recvType == null)
			{
				Debug.LogError(NetBuffer.CreateStackTrace("通过opCode没找到对应的protobuf类型:" + opCode));
				return null;
			}
			int num2 = this.ReadIntAt(0);
			if (num2 >= 18)
			{
				this.SeekPos = 18L;
				try
				{
					this.memoryStream.SetLength((long)num2);
					object obj = Serializer.NonGeneric.Deserialize(recvType, this.memoryStream);
					return new object[]
					{
						num,
						obj
					};
				}
				catch (Exception ex)
				{
					Debug.LogError(NetBuffer.CreateStackTrace(string.Format("protobuf反序列化失败，opCode为{0}", opCode)));
					throw ex;
				}
			}
			Debug.LogError(NetBuffer.CreateStackTrace(string.Format("包长不对，包长为{0}，opCode:{1}", num2, opCode)));
			return null;
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
	}
}

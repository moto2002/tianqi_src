using LuaInterface;
using System;
using System.IO;
using System.Text;

namespace LuaFramework
{
	public class ByteBuffer
	{
		private MemoryStream stream;

		private BinaryWriter writer;

		private BinaryReader reader;

		public ByteBuffer()
		{
			this.stream = new MemoryStream();
			this.writer = new BinaryWriter(this.stream);
		}

		public ByteBuffer(byte[] data)
		{
			if (data != null)
			{
				this.stream = new MemoryStream(data);
				this.reader = new BinaryReader(this.stream);
			}
			else
			{
				this.stream = new MemoryStream();
				this.writer = new BinaryWriter(this.stream);
			}
		}

		public void Close()
		{
			if (this.writer != null)
			{
				this.writer.Close();
			}
			if (this.reader != null)
			{
				this.reader.Close();
			}
			this.stream.Close();
			this.writer = null;
			this.reader = null;
			this.stream = null;
		}

		public void WriteByte(byte v)
		{
			this.writer.Write(v);
		}

		public void WriteInt(int v)
		{
			this.writer.Write(v);
		}

		public void WriteShort(ushort v)
		{
			this.writer.Write(v);
		}

		public void WriteLong(long v)
		{
			this.writer.Write(v);
		}

		public void WriteFloat(float v)
		{
			byte[] bytes = BitConverter.GetBytes(v);
			Array.Reverse(bytes);
			this.writer.Write(BitConverter.ToSingle(bytes, 0));
		}

		public void WriteDouble(double v)
		{
			byte[] bytes = BitConverter.GetBytes(v);
			Array.Reverse(bytes);
			this.writer.Write(BitConverter.ToDouble(bytes, 0));
		}

		public void WriteString(string v)
		{
			byte[] bytes = Encoding.get_UTF8().GetBytes(v);
			this.writer.Write((ushort)bytes.Length);
			this.writer.Write(bytes);
		}

		public void WriteBytes(byte[] v)
		{
			this.writer.Write(v.Length);
			this.writer.Write(v);
		}

		public void WriteBuffer(LuaByteBuffer strBuffer)
		{
			this.WriteBytes(strBuffer.buffer);
		}

		public byte ReadByte()
		{
			return this.reader.ReadByte();
		}

		public int ReadInt()
		{
			return this.reader.ReadInt32();
		}

		public ushort ReadShort()
		{
			return (ushort)this.reader.ReadInt16();
		}

		public long ReadLong()
		{
			return this.reader.ReadInt64();
		}

		public float ReadFloat()
		{
			byte[] bytes = BitConverter.GetBytes(this.reader.ReadSingle());
			Array.Reverse(bytes);
			return BitConverter.ToSingle(bytes, 0);
		}

		public double ReadDouble()
		{
			byte[] bytes = BitConverter.GetBytes(this.reader.ReadDouble());
			Array.Reverse(bytes);
			return BitConverter.ToDouble(bytes, 0);
		}

		public string ReadString()
		{
			ushort num = this.ReadShort();
			byte[] array = new byte[(int)num];
			array = this.reader.ReadBytes((int)num);
			return Encoding.get_UTF8().GetString(array);
		}

		public byte[] ReadBytes()
		{
			int num = this.ReadInt();
			return this.reader.ReadBytes(num);
		}

		public LuaByteBuffer ReadBuffer()
		{
			byte[] buf = this.ReadBytes();
			return new LuaByteBuffer(buf);
		}

		public byte[] ToBytes()
		{
			this.writer.Flush();
			return this.stream.ToArray();
		}

		public void Flush()
		{
			this.writer.Flush();
		}
	}
}

using System;
using System.IO;
using UnityEngine;

namespace AltProg
{
	public struct LogPacket
	{
		public string logString;

		public string stackTrace;

		public LogType type;

		public byte[] ToByteArray()
		{
			MemoryStream memoryStream = new MemoryStream(1024);
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			binaryWriter.Write(this.logString);
			binaryWriter.Write(this.stackTrace);
			binaryWriter.Write(this.type);
			binaryWriter.Close();
			return memoryStream.ToArray();
		}

		public static LogPacket FromByteArray(byte[] array)
		{
			LogPacket result = default(LogPacket);
			MemoryStream memoryStream = new MemoryStream(array, false);
			BinaryReader binaryReader = new BinaryReader(memoryStream);
			try
			{
				result.logString = binaryReader.ReadString();
				result.stackTrace = binaryReader.ReadString();
				result.type = binaryReader.ReadInt32();
				binaryReader.Close();
				memoryStream.Close();
				memoryStream.Dispose();
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.get_Message());
			}
			return result;
		}
	}
}

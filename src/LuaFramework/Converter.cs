using System;

namespace LuaFramework
{
	public class Converter
	{
		public static int GetBigEndian(int value)
		{
			if (BitConverter.IsLittleEndian)
			{
				return Converter.swapByteOrder(value);
			}
			return value;
		}

		public static ushort GetBigEndian(ushort value)
		{
			if (BitConverter.IsLittleEndian)
			{
				return Converter.swapByteOrder(value);
			}
			return value;
		}

		public static uint GetBigEndian(uint value)
		{
			if (BitConverter.IsLittleEndian)
			{
				return Converter.swapByteOrder(value);
			}
			return value;
		}

		public static long GetBigEndian(long value)
		{
			if (BitConverter.IsLittleEndian)
			{
				return Converter.swapByteOrder(value);
			}
			return value;
		}

		public static double GetBigEndian(double value)
		{
			if (BitConverter.IsLittleEndian)
			{
				return Converter.swapByteOrder(value);
			}
			return value;
		}

		public static float GetBigEndian(float value)
		{
			if (BitConverter.IsLittleEndian)
			{
				return (float)Converter.swapByteOrder((int)value);
			}
			return value;
		}

		public static int GetLittleEndian(int value)
		{
			if (BitConverter.IsLittleEndian)
			{
				return value;
			}
			return Converter.swapByteOrder(value);
		}

		public static uint GetLittleEndian(uint value)
		{
			if (BitConverter.IsLittleEndian)
			{
				return value;
			}
			return Converter.swapByteOrder(value);
		}

		public static ushort GetLittleEndian(ushort value)
		{
			if (BitConverter.IsLittleEndian)
			{
				return value;
			}
			return Converter.swapByteOrder(value);
		}

		public static double GetLittleEndian(double value)
		{
			if (BitConverter.IsLittleEndian)
			{
				return value;
			}
			return Converter.swapByteOrder(value);
		}

		private static int swapByteOrder(int value)
		{
			return (int)((long)((255 & value >> 24) | (65280 & value >> 8) | (16711680 & value << 8)) | (long)((ulong)-16777216 & (ulong)((long)((long)value << 24))));
		}

		private static long swapByteOrder(long value)
		{
			return (long)((255uL & (ulong)value >> 56) | (65280uL & (ulong)value >> 40) | (16711680uL & (ulong)value >> 24) | ((ulong)-16777216 & (ulong)value >> 8) | (ulong)(1095216660480L & value << 8) | (ulong)(280375465082880L & value << 24) | (ulong)(71776119061217280L & value << 40) | (ulong)(-72057594037927936L & value << 56));
		}

		private static ushort swapByteOrder(ushort value)
		{
			return (ushort)((255 & value >> 8) | (65280 & (int)value << 8));
		}

		private static uint swapByteOrder(uint value)
		{
			return (255u & value >> 24) | (65280u & value >> 8) | (16711680u & value << 8) | (4278190080u & value << 24);
		}

		private static double swapByteOrder(double value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			Array.Reverse(bytes, 0, bytes.Length);
			return BitConverter.ToDouble(bytes, 0);
		}
	}
}

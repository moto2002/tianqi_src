using System;

namespace ProtoBuf
{
	internal enum ProtoTypeCode
	{
		Empty,
		Unknown,
		Boolean = 3,
		Char,
		SByte,
		Byte,
		Int16,
		UInt16,
		Int32,
		UInt32,
		Int64,
		UInt64,
		Single,
		Double,
		Decimal,
		DateTime,
		String = 18,
		TimeSpan = 100,
		ByteArray,
		Guid,
		Uri,
		Type
	}
}

using System;

namespace ProtoBuf
{
	public enum WireType
	{
		None = -1,
		Variant,
		Fixed64,
		String,
		StartGroup,
		EndGroup,
		Fixed32,
		SignedVariant = 8
	}
}

using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "Direction")]
	[Serializable]
	public class Direction : IExtensible
	{
		[ProtoContract(Name = "ENUM")]
		public enum ENUM
		{
			[ProtoEnum(Name = "EAST", Value = 0)]
			EAST,
			[ProtoEnum(Name = "EAST_NORTH", Value = 1)]
			EAST_NORTH,
			[ProtoEnum(Name = "NORTH", Value = 2)]
			NORTH,
			[ProtoEnum(Name = "WEST_NORTH", Value = 3)]
			WEST_NORTH,
			[ProtoEnum(Name = "WEST", Value = 4)]
			WEST,
			[ProtoEnum(Name = "WEST_SOUTH", Value = 5)]
			WEST_SOUTH,
			[ProtoEnum(Name = "SOUTH", Value = 6)]
			SOUTH,
			[ProtoEnum(Name = "EAST_SOUTH", Value = 7)]
			EAST_SOUTH
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}

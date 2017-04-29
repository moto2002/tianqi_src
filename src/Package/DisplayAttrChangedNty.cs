using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(915), ForSend(915), ProtoContract(Name = "DisplayAttrChangedNty")]
	[Serializable]
	public class DisplayAttrChangedNty : IExtensible
	{
		[ProtoContract(Name = "AttrPair")]
		[Serializable]
		public class AttrPair : IExtensible
		{
			private int _attrType;

			private long _attrValue;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "attrType", DataFormat = DataFormat.TwosComplement)]
			public int attrType
			{
				get
				{
					return this._attrType;
				}
				set
				{
					this._attrType = value;
				}
			}

			[ProtoMember(2, IsRequired = true, Name = "attrValue", DataFormat = DataFormat.TwosComplement)]
			public long attrValue
			{
				get
				{
					return this._attrValue;
				}
				set
				{
					this._attrValue = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		public static readonly short OP = 915;

		private GameObjectType.ENUM _type;

		private long _id;

		private readonly List<DisplayAttrChangedNty.AttrPair> _attrs = new List<DisplayAttrChangedNty.AttrPair>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(GameObjectType.ENUM.Role)]
		public GameObjectType.ENUM type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "id", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		[ProtoMember(3, Name = "attrs", DataFormat = DataFormat.Default)]
		public List<DisplayAttrChangedNty.AttrPair> attrs
		{
			get
			{
				return this._attrs;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}

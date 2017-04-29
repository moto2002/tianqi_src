using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ProtoContract(Name = "BattleAction_AttrChanged")]
	[Serializable]
	public class BattleAction_AttrChanged : IExtensible
	{
		[ProtoContract(Name = "Pair")]
		[Serializable]
		public class Pair : IExtensible
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

		private long _soldierId;

		private readonly List<BattleAction_AttrChanged.Pair> _attrs = new List<BattleAction_AttrChanged.Pair>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "soldierId", DataFormat = DataFormat.TwosComplement)]
		public long soldierId
		{
			get
			{
				return this._soldierId;
			}
			set
			{
				this._soldierId = value;
			}
		}

		[ProtoMember(2, Name = "attrs", DataFormat = DataFormat.Default)]
		public List<BattleAction_AttrChanged.Pair> attrs
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

using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"AIid"
	}), ProtoContract(Name = "AI")]
	[Serializable]
	public class AI : IExtensible
	{
		private int _AIid;

		private int _percentage;

		private int _conditonID;

		private int _behaviorID;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "AIid", DataFormat = DataFormat.TwosComplement)]
		public int AIid
		{
			get
			{
				return this._AIid;
			}
			set
			{
				this._AIid = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "percentage", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int percentage
		{
			get
			{
				return this._percentage;
			}
			set
			{
				this._percentage = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "conditonID", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int conditonID
		{
			get
			{
				return this._conditonID;
			}
			set
			{
				this._conditonID = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "behaviorID", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int behaviorID
		{
			get
			{
				return this._behaviorID;
			}
			set
			{
				this._behaviorID = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}

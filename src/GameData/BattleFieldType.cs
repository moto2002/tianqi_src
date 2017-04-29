using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"type"
	}), ProtoContract(Name = "BattleFieldType")]
	[Serializable]
	public class BattleFieldType : IExtensible
	{
		private int _type;

		private int _petNumber;

		private int _actionPoint;

		private int _pointId;

		private int _monsterBornDirection;

		private int _countDown;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "type", DataFormat = DataFormat.TwosComplement)]
		public int type
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

		[ProtoMember(3, IsRequired = false, Name = "petNumber", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int petNumber
		{
			get
			{
				return this._petNumber;
			}
			set
			{
				this._petNumber = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "actionPoint", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int actionPoint
		{
			get
			{
				return this._actionPoint;
			}
			set
			{
				this._actionPoint = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "pointId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int pointId
		{
			get
			{
				return this._pointId;
			}
			set
			{
				this._pointId = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "monsterBornDirection", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int monsterBornDirection
		{
			get
			{
				return this._monsterBornDirection;
			}
			set
			{
				this._monsterBornDirection = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "countDown", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int countDown
		{
			get
			{
				return this._countDown;
			}
			set
			{
				this._countDown = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}

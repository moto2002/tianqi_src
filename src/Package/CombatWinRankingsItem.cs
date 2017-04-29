using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "CombatWinRankingsItem")]
	[Serializable]
	public class CombatWinRankingsItem : IExtensible
	{
		private long _roleId;

		private string _name;

		private int _modelId;

		private int _lv;

		private int _winCount;

		private long _fighting;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "roleId", DataFormat = DataFormat.TwosComplement)]
		public long roleId
		{
			get
			{
				return this._roleId;
			}
			set
			{
				this._roleId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "name", DataFormat = DataFormat.Default)]
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "modelId", DataFormat = DataFormat.TwosComplement)]
		public int modelId
		{
			get
			{
				return this._modelId;
			}
			set
			{
				this._modelId = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "lv", DataFormat = DataFormat.TwosComplement)]
		public int lv
		{
			get
			{
				return this._lv;
			}
			set
			{
				this._lv = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "winCount", DataFormat = DataFormat.TwosComplement)]
		public int winCount
		{
			get
			{
				return this._winCount;
			}
			set
			{
				this._winCount = value;
			}
		}

		[ProtoMember(6, IsRequired = true, Name = "fighting", DataFormat = DataFormat.TwosComplement)]
		public long fighting
		{
			get
			{
				return this._fighting;
			}
			set
			{
				this._fighting = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}

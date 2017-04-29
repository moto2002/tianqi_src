using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "PassiveSkillInfo")]
	[Serializable]
	public class PassiveSkillInfo : IExtensible
	{
		private int _Id;

		private int _Lv;

		private bool _State;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "Id", DataFormat = DataFormat.TwosComplement)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "Lv", DataFormat = DataFormat.TwosComplement)]
		public int Lv
		{
			get
			{
				return this._Lv;
			}
			set
			{
				this._Lv = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "State", DataFormat = DataFormat.Default)]
		public bool State
		{
			get
			{
				return this._State;
			}
			set
			{
				this._State = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}

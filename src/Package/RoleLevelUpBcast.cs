using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(672), ForSend(672), ProtoContract(Name = "RoleLevelUpBcast")]
	[Serializable]
	public class RoleLevelUpBcast : IExtensible
	{
		public static readonly short OP = 672;

		private long _roleId;

		private string _name;

		private int _oldLv;

		private int _newLv;

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

		[ProtoMember(3, IsRequired = true, Name = "oldLv", DataFormat = DataFormat.TwosComplement)]
		public int oldLv
		{
			get
			{
				return this._oldLv;
			}
			set
			{
				this._oldLv = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "newLv", DataFormat = DataFormat.TwosComplement)]
		public int newLv
		{
			get
			{
				return this._newLv;
			}
			set
			{
				this._newLv = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}

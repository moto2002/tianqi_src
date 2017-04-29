using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "BattleAction_EndLoading")]
	[Serializable]
	public class BattleAction_EndLoading : IExtensible
	{
		private long _roleId;

		private IExtension extensionObject;

		[ProtoMember(3, IsRequired = true, Name = "roleId", DataFormat = DataFormat.TwosComplement)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}

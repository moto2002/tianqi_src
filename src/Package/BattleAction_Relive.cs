using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "BattleAction_Relive")]
	[Serializable]
	public class BattleAction_Relive : IExtensible
	{
		private MapObjInfo _soldierInfo;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "soldierInfo", DataFormat = DataFormat.Default)]
		public MapObjInfo soldierInfo
		{
			get
			{
				return this._soldierInfo;
			}
			set
			{
				this._soldierInfo = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}

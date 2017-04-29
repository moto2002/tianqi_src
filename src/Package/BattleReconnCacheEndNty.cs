using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(947), ForSend(947), ProtoContract(Name = "BattleReconnCacheEndNty")]
	[Serializable]
	public class BattleReconnCacheEndNty : IExtensible
	{
		public static readonly short OP = 947;

		private bool _isRoleManaging;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "isRoleManaging", DataFormat = DataFormat.Default)]
		public bool isRoleManaging
		{
			get
			{
				return this._isRoleManaging;
			}
			set
			{
				this._isRoleManaging = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}

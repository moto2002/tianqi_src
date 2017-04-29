using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(573), ForSend(573), ProtoContract(Name = "BattleLoadInfo")]
	[Serializable]
	public class BattleLoadInfo : IExtensible
	{
		public static readonly short OP = 573;

		private int _unloadRoleCount;

		private int _loadTimeout;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "unloadRoleCount", DataFormat = DataFormat.TwosComplement)]
		public int unloadRoleCount
		{
			get
			{
				return this._unloadRoleCount;
			}
			set
			{
				this._unloadRoleCount = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "loadTimeout", DataFormat = DataFormat.TwosComplement)]
		public int loadTimeout
		{
			get
			{
				return this._loadTimeout;
			}
			set
			{
				this._loadTimeout = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}

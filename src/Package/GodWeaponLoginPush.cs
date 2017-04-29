using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(779), ForSend(779), ProtoContract(Name = "GodWeaponLoginPush")]
	[Serializable]
	public class GodWeaponLoginPush : IExtensible
	{
		public static readonly short OP = 779;

		private readonly List<GodWeaponInfo> _weaponInfo = new List<GodWeaponInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "weaponInfo", DataFormat = DataFormat.Default)]
		public List<GodWeaponInfo> weaponInfo
		{
			get
			{
				return this._weaponInfo;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}

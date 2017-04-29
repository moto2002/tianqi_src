using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(635), ForSend(635), ProtoContract(Name = "HolyWeaponPush")]
	[Serializable]
	public class HolyWeaponPush : IExtensible
	{
		public static readonly short OP = 635;

		private readonly List<HolyWeaponInfo> _weapon = new List<HolyWeaponInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "weapon", DataFormat = DataFormat.Default)]
		public List<HolyWeaponInfo> weapon
		{
			get
			{
				return this._weapon;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}

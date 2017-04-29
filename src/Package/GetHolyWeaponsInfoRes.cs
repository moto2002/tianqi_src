using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(4999), ForSend(4999), ProtoContract(Name = "GetHolyWeaponsInfoRes")]
	[Serializable]
	public class GetHolyWeaponsInfoRes : IExtensible
	{
		public static readonly short OP = 4999;

		private readonly List<HolyWeaponInfo> _Info = new List<HolyWeaponInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "Info", DataFormat = DataFormat.Default)]
		public List<HolyWeaponInfo> Info
		{
			get
			{
				return this._Info;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}

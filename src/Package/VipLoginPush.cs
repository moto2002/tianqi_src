using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(531), ForSend(531), ProtoContract(Name = "VipLoginPush")]
	[Serializable]
	public class VipLoginPush : IExtensible
	{
		public static readonly short OP = 531;

		private readonly List<VipEffectInfo> _effects = new List<VipEffectInfo>();

		private readonly List<VipBoxItemInfo> _boxItems = new List<VipBoxItemInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "effects", DataFormat = DataFormat.Default)]
		public List<VipEffectInfo> effects
		{
			get
			{
				return this._effects;
			}
		}

		[ProtoMember(2, Name = "boxItems", DataFormat = DataFormat.Default)]
		public List<VipBoxItemInfo> boxItems
		{
			get
			{
				return this._boxItems;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}

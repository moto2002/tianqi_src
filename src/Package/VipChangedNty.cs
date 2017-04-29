using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(548), ForSend(548), ProtoContract(Name = "VipChangedNty")]
	[Serializable]
	public class VipChangedNty : IExtensible
	{
		public static readonly short OP = 548;

		private readonly List<VipEffectInfo> _effects = new List<VipEffectInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "effects", DataFormat = DataFormat.Default)]
		public List<VipEffectInfo> effects
		{
			get
			{
				return this._effects;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}

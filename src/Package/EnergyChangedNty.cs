using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(30004), ForSend(30004), ProtoContract(Name = "EnergyChangedNty")]
	[Serializable]
	public class EnergyChangedNty : IExtensible
	{
		public static readonly short OP = 30004;

		private EnergyBuyInfo _buyInfo;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "buyInfo", DataFormat = DataFormat.Default)]
		public EnergyBuyInfo buyInfo
		{
			get
			{
				return this._buyInfo;
			}
			set
			{
				this._buyInfo = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}

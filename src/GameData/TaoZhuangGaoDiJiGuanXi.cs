using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "TaoZhuangGaoDiJiGuanXi")]
	[Serializable]
	public class TaoZhuangGaoDiJiGuanXi : IExtensible
	{
		private readonly List<int> _id = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(2, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public List<int> id
		{
			get
			{
				return this._id;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}

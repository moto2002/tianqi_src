using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "PuTongFuBenDiaoLuoPeiZhi")]
	[Serializable]
	public class PuTongFuBenDiaoLuoPeiZhi : IExtensible
	{
		private int _copyId;

		private readonly List<int> _normalDrop = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "copyId", DataFormat = DataFormat.TwosComplement)]
		public int copyId
		{
			get
			{
				return this._copyId;
			}
			set
			{
				this._copyId = value;
			}
		}

		[ProtoMember(3, Name = "normalDrop", DataFormat = DataFormat.TwosComplement)]
		public List<int> normalDrop
		{
			get
			{
				return this._normalDrop;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}

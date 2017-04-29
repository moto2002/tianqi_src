using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "JiaZaiJieMianPeiTu")]
	[Serializable]
	public class JiaZaiJieMianPeiTu : IExtensible
	{
		private int _loadingPic;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = false, Name = "loadingPic", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int loadingPic
		{
			get
			{
				return this._loadingPic;
			}
			set
			{
				this._loadingPic = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}

using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "GuaJiDengJiJingYan")]
	[Serializable]
	public class GuaJiDengJiJingYan : IExtensible
	{
		private int _lv;

		private long _exp;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "lv", DataFormat = DataFormat.TwosComplement)]
		public int lv
		{
			get
			{
				return this._lv;
			}
			set
			{
				this._lv = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "exp", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long exp
		{
			get
			{
				return this._exp;
			}
			set
			{
				this._exp = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}

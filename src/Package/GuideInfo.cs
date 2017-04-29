using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "GuideInfo")]
	[Serializable]
	public class GuideInfo : IExtensible
	{
		private int _guideGroupId;

		private int _completeTimes;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "guideGroupId", DataFormat = DataFormat.TwosComplement)]
		public int guideGroupId
		{
			get
			{
				return this._guideGroupId;
			}
			set
			{
				this._guideGroupId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "completeTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int completeTimes
		{
			get
			{
				return this._completeTimes;
			}
			set
			{
				this._completeTimes = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}

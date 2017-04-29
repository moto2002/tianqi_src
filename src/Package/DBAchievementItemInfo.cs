using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "DBAchievementItemInfo")]
	[Serializable]
	public class DBAchievementItemInfo : IExtensible
	{
		private int _linkSystem;

		private AchievementItemInfo _info;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "linkSystem", DataFormat = DataFormat.TwosComplement)]
		public int linkSystem
		{
			get
			{
				return this._linkSystem;
			}
			set
			{
				this._linkSystem = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "info", DataFormat = DataFormat.Default)]
		public AchievementItemInfo info
		{
			get
			{
				return this._info;
			}
			set
			{
				this._info = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}

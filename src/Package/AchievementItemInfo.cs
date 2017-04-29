using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ProtoContract(Name = "AchievementItemInfo")]
	[Serializable]
	public class AchievementItemInfo : IExtensible
	{
		private int _achievementId;

		private int _isAccept;

		private readonly List<int> _completeProgress = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "achievementId", DataFormat = DataFormat.TwosComplement)]
		public int achievementId
		{
			get
			{
				return this._achievementId;
			}
			set
			{
				this._achievementId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "isAccept", DataFormat = DataFormat.TwosComplement)]
		public int isAccept
		{
			get
			{
				return this._isAccept;
			}
			set
			{
				this._isAccept = value;
			}
		}

		[ProtoMember(3, Name = "completeProgress", DataFormat = DataFormat.TwosComplement)]
		public List<int> completeProgress
		{
			get
			{
				return this._completeProgress;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}

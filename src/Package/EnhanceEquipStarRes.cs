using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(813), ForSend(813), ProtoContract(Name = "EnhanceEquipStarRes")]
	[Serializable]
	public class EnhanceEquipStarRes : IExtensible
	{
		public static readonly short OP = 813;

		private long _equipId;

		private int _currentStar;

		private readonly List<ExcellentAttr> _excellentAttrs = new List<ExcellentAttr>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "equipId", DataFormat = DataFormat.TwosComplement)]
		public long equipId
		{
			get
			{
				return this._equipId;
			}
			set
			{
				this._equipId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "currentStar", DataFormat = DataFormat.TwosComplement)]
		public int currentStar
		{
			get
			{
				return this._currentStar;
			}
			set
			{
				this._currentStar = value;
			}
		}

		[ProtoMember(3, Name = "excellentAttrs", DataFormat = DataFormat.Default)]
		public List<ExcellentAttr> excellentAttrs
		{
			get
			{
				return this._excellentAttrs;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}

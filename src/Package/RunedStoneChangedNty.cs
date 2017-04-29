using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(53), ForSend(53), ProtoContract(Name = "RunedStoneChangedNty")]
	[Serializable]
	public class RunedStoneChangedNty : IExtensible
	{
		[ProtoContract(Name = "ChangeType")]
		public enum ChangeType
		{
			[ProtoEnum(Name = "Add", Value = 1)]
			Add = 1,
			[ProtoEnum(Name = "Update", Value = 2)]
			Update,
			[ProtoEnum(Name = "Remove", Value = 3)]
			Remove
		}

		public static readonly short OP = 53;

		private RunedStoneChangedNty.ChangeType _changeType;

		private readonly List<RunedStoneInfo> _stoneInfos = new List<RunedStoneInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "changeType", DataFormat = DataFormat.TwosComplement)]
		public RunedStoneChangedNty.ChangeType changeType
		{
			get
			{
				return this._changeType;
			}
			set
			{
				this._changeType = value;
			}
		}

		[ProtoMember(2, Name = "stoneInfos", DataFormat = DataFormat.Default)]
		public List<RunedStoneInfo> stoneInfos
		{
			get
			{
				return this._stoneInfos;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}

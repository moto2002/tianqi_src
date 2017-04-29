using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(880), ForSend(880), ProtoContract(Name = "LightAllKeyPointsNty")]
	[Serializable]
	public class LightAllKeyPointsNty : IExtensible
	{
		public static readonly short OP = 880;

		private readonly List<int> _linePointIds = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "linePointIds", DataFormat = DataFormat.TwosComplement)]
		public List<int> linePointIds
		{
			get
			{
				return this._linePointIds;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}

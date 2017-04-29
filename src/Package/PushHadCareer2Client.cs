using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(5667), ForSend(5667), ProtoContract(Name = "PushHadCareer2Client")]
	[Serializable]
	public class PushHadCareer2Client : IExtensible
	{
		public static readonly short OP = 5667;

		private readonly List<int> _hadCareer = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "hadCareer", DataFormat = DataFormat.TwosComplement)]
		public List<int> hadCareer
		{
			get
			{
				return this._hadCareer;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}

using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(212), ForSend(212), ProtoContract(Name = "PickUpPetTaskReq")]
	[Serializable]
	public class PickUpPetTaskReq : IExtensible
	{
		public static readonly short OP = 212;

		private long _idx;

		private readonly List<int> _pets = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "idx", DataFormat = DataFormat.TwosComplement)]
		public long idx
		{
			get
			{
				return this._idx;
			}
			set
			{
				this._idx = value;
			}
		}

		[ProtoMember(2, Name = "pets", DataFormat = DataFormat.TwosComplement)]
		public List<int> pets
		{
			get
			{
				return this._pets;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}

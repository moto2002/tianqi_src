using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "DianLiangBuShu")]
	[Serializable]
	public class DianLiangBuShu : IExtensible
	{
		private int _stage;

		private int _stepNum;

		private readonly List<int> _library = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "stage", DataFormat = DataFormat.TwosComplement)]
		public int stage
		{
			get
			{
				return this._stage;
			}
			set
			{
				this._stage = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "stepNum", DataFormat = DataFormat.TwosComplement)]
		public int stepNum
		{
			get
			{
				return this._stepNum;
			}
			set
			{
				this._stepNum = value;
			}
		}

		[ProtoMember(3, Name = "library", DataFormat = DataFormat.TwosComplement)]
		public List<int> library
		{
			get
			{
				return this._library;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}

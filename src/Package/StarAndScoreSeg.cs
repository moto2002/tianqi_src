using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "StarAndScoreSeg")]
	[Serializable]
	public class StarAndScoreSeg : IExtensible
	{
		private int _starSeg;

		private int _scoreSeg;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "starSeg", DataFormat = DataFormat.TwosComplement)]
		public int starSeg
		{
			get
			{
				return this._starSeg;
			}
			set
			{
				this._starSeg = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "scoreSeg", DataFormat = DataFormat.TwosComplement)]
		public int scoreSeg
		{
			get
			{
				return this._scoreSeg;
			}
			set
			{
				this._scoreSeg = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}

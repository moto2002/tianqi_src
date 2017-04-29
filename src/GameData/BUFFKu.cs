using ProtoBuf;
using System;

namespace GameData
{
	[ProtoContract(Name = "BUFFKu")]
	[Serializable]
	public class BUFFKu : IExtensible
	{
		private int _buffId;

		private int _weight;

		private string _pic;

		private int _buffExplain;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "buffId", DataFormat = DataFormat.TwosComplement)]
		public int buffId
		{
			get
			{
				return this._buffId;
			}
			set
			{
				this._buffId = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "weight", DataFormat = DataFormat.TwosComplement)]
		public int weight
		{
			get
			{
				return this._weight;
			}
			set
			{
				this._weight = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "pic", DataFormat = DataFormat.Default)]
		public string pic
		{
			get
			{
				return this._pic;
			}
			set
			{
				this._pic = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "buffExplain", DataFormat = DataFormat.TwosComplement)]
		public int buffExplain
		{
			get
			{
				return this._buffExplain;
			}
			set
			{
				this._buffExplain = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}

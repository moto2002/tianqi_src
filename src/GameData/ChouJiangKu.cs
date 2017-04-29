using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ChouJiangKu")]
	[Serializable]
	public class ChouJiangKu : IExtensible
	{
		private int _id;

		private int _articleId;

		private int _amountA;

		private int _attributeId;

		private int _amountB;

		private int _chance;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "articleId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int articleId
		{
			get
			{
				return this._articleId;
			}
			set
			{
				this._articleId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "amountA", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int amountA
		{
			get
			{
				return this._amountA;
			}
			set
			{
				this._amountA = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "attributeId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int attributeId
		{
			get
			{
				return this._attributeId;
			}
			set
			{
				this._attributeId = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "amountB", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int amountB
		{
			get
			{
				return this._amountB;
			}
			set
			{
				this._amountB = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "chance", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int chance
		{
			get
			{
				return this._chance;
			}
			set
			{
				this._chance = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}

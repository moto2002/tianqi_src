using System;

namespace GameData
{
	public class BuffCtrlAttrs
	{
		private int elemType;

		private int addProbAddAmend;

		private int durTimeAddAmend;

		public int ElemType
		{
			get
			{
				return this.elemType;
			}
		}

		public int AddProbAddAmend
		{
			get
			{
				return this.addProbAddAmend;
			}
			set
			{
				this.addProbAddAmend = value;
			}
		}

		public int DurTimeAddAmend
		{
			get
			{
				return this.durTimeAddAmend;
			}
			set
			{
				this.durTimeAddAmend = value;
			}
		}

		public BuffCtrlAttrs(int elemType)
		{
			this.elemType = elemType;
		}
	}
}

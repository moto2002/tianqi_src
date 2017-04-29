using System;

namespace UnityEngine.UI
{
	[AddComponentMenu("UI/TextCustom", 11)]
	public class TextCustom : Text
	{
		[SerializeField]
		private int _TextId;

		public int TextId
		{
			get
			{
				return this._TextId;
			}
			set
			{
				this._TextId = value;
				this.set_text(GameDataUtils.GetChineseContent(this._TextId, false));
			}
		}

		protected override void Start()
		{
			base.Start();
			this.set_text(GameDataUtils.GetChineseContent(this._TextId, false));
		}
	}
}

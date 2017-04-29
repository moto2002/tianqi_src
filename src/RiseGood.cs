using System;
using UnityEngine;
using UnityEngine.UI;

public class RiseGood : MonoBehaviour
{
	private Image frame;

	private Image icon;

	private Text num;

	private ButtonCustom btn;

	private ButtonCustom reduce;

	private int count;

	private int use;

	private Goods dataTpl;

	private bool isGray;

	private void Awake()
	{
		this.btn = base.GetComponent<ButtonCustom>();
		Transform transform = base.get_transform().FindChild("bgItem");
		this.frame = transform.FindChild("frame").GetComponent<Image>();
		this.icon = transform.FindChild("icon").GetComponent<Image>();
		this.num = transform.FindChild("num").GetComponent<Text>();
		this.reduce = transform.FindChild("reduce").GetComponent<ButtonCustom>();
		EventDispatcher.AddListener<bool>(EventNames.SliderOverflow, new Callback<bool>(this.OnNoSelectClick));
		ButtonCustom expr_91 = this.btn;
		expr_91.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_91.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickIcon));
		ButtonCustom expr_B8 = this.reduce;
		expr_B8.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_B8.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickReduce));
	}

	private void OnDestroy()
	{
		EventDispatcher.RemoveListener<bool>(EventNames.SliderOverflow, new Callback<bool>(this.OnNoSelectClick));
	}

	private void OnNoSelectClick(bool gray)
	{
		if (gray)
		{
			this.isGray = gray;
			this.btn.set_enabled(!this.isGray);
			ImageColorMgr.SetImageColor(this.frame, true);
			ImageColorMgr.SetImageColor(this.icon, true);
		}
		else if (this.isGray)
		{
			ImageColorMgr.SetImageColor(this.frame, false);
			ImageColorMgr.SetImageColor(this.icon, false);
			this.isGray = gray;
			this.btn.set_enabled(!this.isGray);
		}
	}

	private void OnClickReduce(GameObject go)
	{
		if (this.use > 0)
		{
			this.use--;
			if (this.use == 0)
			{
				this.num.set_text(this.count.ToString());
				this.ShowReduce(false);
			}
			else
			{
				this.num.set_text(string.Format("{0}/{1}", this.use, this.count));
			}
			EventDispatcher.Broadcast<long, int, bool>(EventNames.UpdateSlider, this.dataTpl.GetLongId(), this.use, false);
		}
	}

	private void OnClickIcon(GameObject go)
	{
		if (this.use < this.count)
		{
			if (this.use == 0)
			{
				this.ShowReduce(true);
			}
			this.use++;
			this.num.set_text(string.Format("{0}/{1}", this.use, this.count));
			EventDispatcher.Broadcast<long, int, bool>(EventNames.UpdateSlider, this.dataTpl.GetLongId(), this.use, true);
		}
	}

	private void ShowReduce(bool isShow)
	{
		this.reduce.get_gameObject().SetActive(isShow);
	}

	public void UpdateUI(Goods data)
	{
		if (data != null)
		{
			this.use = 0;
			this.dataTpl = data;
			this.ShowReduce(false);
			this.count = BackpackManager.Instance.OnGetGoodCount(this.dataTpl.GetLongId());
			GameDataUtils.SetItem(this.dataTpl.LocalItem, this.frame, this.icon, null, true);
			this.num.set_text(this.count.ToString());
		}
	}
}

using System;
using UnityEngine;
using UnityEngine.UI;

public class ComboControl : MonoBehaviour
{
	private enum ComboSpeed
	{
		Speed1,
		Speed2,
		Speed3
	}

	private const string IdleName = "Silent";

	private const string ComboName = "Combo";

	private const string ComboClipName = "AnimCombo";

	private const float ComboSpeed1 = 1f;

	private const float ComboSpeed2 = 1.5f;

	private const float ComboSpeed3 = 2f;

	private const float TimeAddRate2 = 0.6666667f;

	protected Transform Combo;

	protected Animator m_animatorCombo;

	protected Image m_spComboNum0;

	protected Image m_spComboNum1;

	protected Image m_spComboNum2;

	protected Image m_spComboNumT0;

	protected Image m_spComboNumT1;

	protected Image m_spComboNumT2;

	protected string fontStr = "new_combo_";

	protected string fontZero = string.Empty;

	private bool m_isShowCombo;

	private float m_lengthComboTime;

	private float m_totalInterval;

	public void AwakeSelf()
	{
		this.Combo = base.get_transform().get_parent();
		this.m_animatorCombo = base.get_transform().GetComponent<Animator>();
		this.m_spComboNum0 = base.get_transform().Find("ComboNumL1/ComboNum0").GetComponent<Image>();
		this.m_spComboNum1 = base.get_transform().Find("ComboNumL1/ComboNum1").GetComponent<Image>();
		this.m_spComboNum2 = base.get_transform().Find("ComboNumL1/ComboNum2").GetComponent<Image>();
		this.m_spComboNumT0 = base.get_transform().Find("ComboNumL2/ComboNumT0").GetComponent<Image>();
		this.m_spComboNumT1 = base.get_transform().Find("ComboNumL2/ComboNumT1").GetComponent<Image>();
		this.m_spComboNumT2 = base.get_transform().Find("ComboNumL2/ComboNumT2").GetComponent<Image>();
		this.SetSpeed(ComboControl.ComboSpeed.Speed1);
		for (int i = 0; i < this.m_animatorCombo.get_runtimeAnimatorController().get_animationClips().Length; i++)
		{
			if (this.m_animatorCombo.get_runtimeAnimatorController().get_animationClips()[i].get_name() == "AnimCombo")
			{
				AnimationClip animationClip = this.m_animatorCombo.get_runtimeAnimatorController().get_animationClips()[i];
				this.m_lengthComboTime = animationClip.get_length();
				break;
			}
		}
		if (this.m_lengthComboTime <= 0f)
		{
			Debug.LogError("m_lengthComboTime <= 0.0f");
		}
	}

	private void OnDisable()
	{
		this.ResetAll();
	}

	private void Update()
	{
		if (this.m_isShowCombo)
		{
			this.m_totalInterval += Time.get_deltaTime();
		}
	}

	public void SetCombo(bool isShow, int combo = 0, bool isMovePosition = false)
	{
		this.m_isShowCombo = isShow;
		if (isShow)
		{
			if (!this.m_animatorCombo.GetCurrentAnimatorStateInfo(0).IsName("Combo"))
			{
				Utils.SetTransformZOn(this.Combo.get_transform(), isShow);
				this.DisableNum();
				if (combo == 0)
				{
					combo = 1;
				}
				if (combo > 999)
				{
					combo = 999;
				}
				int num = combo / 100;
				int num2 = combo % 100 / 10;
				int num3 = combo % 10;
				if (num > 0)
				{
					this.EnableNum(this.m_spComboNum0, this.m_spComboNumT0, num);
					this.EnableNum(this.m_spComboNum1, this.m_spComboNumT1, num2);
					this.EnableNum(this.m_spComboNum2, this.m_spComboNumT2, num3);
					this.SetComboPosition(3, isMovePosition);
				}
				else if (num2 > 0)
				{
					this.EnableNum(this.m_spComboNum0, this.m_spComboNumT0, num2);
					this.EnableNum(this.m_spComboNum1, this.m_spComboNumT1, num3);
					this.SetComboPosition(2, isMovePosition);
				}
				else if (num3 > 0)
				{
					this.EnableNum(this.m_spComboNum0, this.m_spComboNumT0, num3);
					this.SetComboPosition(1, isMovePosition);
				}
				if (this.m_totalInterval > this.m_lengthComboTime * 1f)
				{
					this.SetSpeed(ComboControl.ComboSpeed.Speed1);
				}
				else if (this.m_totalInterval > this.m_lengthComboTime * 0.6666667f)
				{
					this.SetSpeed(ComboControl.ComboSpeed.Speed2);
				}
				else
				{
					this.SetSpeed(ComboControl.ComboSpeed.Speed3);
				}
				this.m_totalInterval = 0f;
				this.m_animatorCombo.get_transform().set_localScale(new Vector3(1f, 1f, 1f));
				this.m_animatorCombo.Play("Combo");
			}
		}
		else
		{
			Utils.SetTransformZOn(this.Combo.get_transform(), isShow);
			this.ResetAll();
		}
	}

	public void SetComboPosition(int count, bool isMovePosition)
	{
		if (!isMovePosition)
		{
			return;
		}
		if (count == 1)
		{
			base.get_transform().Find("ComboNumL1/ComboNum0").set_localPosition(new Vector2(-8.2f, 2.5f));
			base.get_transform().Find("ComboNumL2/ComboNumT0").set_localPosition(new Vector2(-42.2f, 11.6f));
		}
		else if (count == 2)
		{
			base.get_transform().Find("ComboNumL1/ComboNum0").set_localPosition(new Vector2(-25f, -2f));
			base.get_transform().Find("ComboNumL1/ComboNum1").set_localPosition(new Vector2(12f, 11f));
			base.get_transform().Find("ComboNumL2/ComboNumT0").set_localPosition(new Vector2(-26.8f, 15.8f));
			base.get_transform().Find("ComboNumL2/ComboNumT1").set_localPosition(new Vector2(11.5f, 26.6f));
		}
		else if (count == 3)
		{
			base.get_transform().Find("ComboNumL1/ComboNum0").set_localPosition(new Vector2(-43.7f, -11.6f));
			base.get_transform().Find("ComboNumL1/ComboNum1").set_localPosition(new Vector2(-8.2f, 2.5f));
			base.get_transform().Find("ComboNumL1/ComboNum2").set_localPosition(new Vector2(28.9f, 14.3f));
			base.get_transform().Find("ComboNumL2/ComboNumT0").set_localPosition(new Vector2(-42.2f, 11.6f));
			base.get_transform().Find("ComboNumL2/ComboNumT1").set_localPosition(new Vector2(-3.9f, 22.4f));
			base.get_transform().Find("ComboNumL2/ComboNumT2").set_localPosition(new Vector2(32.2f, 35.9f));
		}
	}

	public void ResetAll()
	{
		this.DisableNum();
		if (this.m_animatorCombo != null)
		{
			this.m_animatorCombo.get_transform().set_localScale(new Vector3(1f, 1f, 1f));
			this.m_animatorCombo.Play("Silent");
		}
		this.m_totalInterval = 0f;
	}

	public void SetFontStr(string fontStr, string fontZero = "")
	{
		this.fontStr = fontStr;
		this.fontZero = fontZero;
	}

	private void EnableNum(Image targetNum1, Image targetNum2, int num)
	{
		string spriteName;
		if (num == 0 && !this.fontZero.Equals(string.Empty))
		{
			spriteName = this.fontZero;
		}
		else
		{
			spriteName = this.fontStr + num;
		}
		SpriteRenderer iconSprite = ResourceManager.GetIconSprite(spriteName);
		targetNum1.set_enabled(true);
		ResourceManager.SetSprite(targetNum1, iconSprite);
		targetNum2.set_enabled(true);
		ResourceManager.SetSprite(targetNum2, iconSprite);
	}

	private void DisableNum()
	{
		if (this.m_spComboNum0 != null)
		{
			this.m_spComboNum0.set_enabled(false);
		}
		if (this.m_spComboNum1 != null)
		{
			this.m_spComboNum1.set_enabled(false);
		}
		if (this.m_spComboNum2 != null)
		{
			this.m_spComboNum2.set_enabled(false);
		}
		if (this.m_spComboNumT0 != null)
		{
			this.m_spComboNumT0.set_enabled(false);
		}
		if (this.m_spComboNumT1 != null)
		{
			this.m_spComboNumT1.set_enabled(false);
		}
		if (this.m_spComboNumT2 != null)
		{
			this.m_spComboNumT2.set_enabled(false);
		}
	}

	private void SetSpeed(ComboControl.ComboSpeed type)
	{
		switch (type)
		{
		case ComboControl.ComboSpeed.Speed1:
			this.SetSpeed(1f);
			break;
		case ComboControl.ComboSpeed.Speed2:
			this.SetSpeed(1.5f);
			break;
		case ComboControl.ComboSpeed.Speed3:
			this.SetSpeed(2f);
			break;
		}
	}

	private void SetSpeed(float speed)
	{
		this.m_animatorCombo.set_speed(speed);
	}
}

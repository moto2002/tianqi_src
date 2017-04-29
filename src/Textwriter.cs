using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class Textwriter : MonoBehaviour
{
	public int charsPerSecond = 20;

	[HideInInspector]
	public bool IsWritering;

	private Text mLabel;

	private string mText;

	private int mOffset;

	private float mNextChar;

	private bool isStart;

	public Text Label
	{
		get
		{
			if (this.mLabel == null)
			{
				this.mLabel = base.GetComponent<Text>();
			}
			return this.mLabel;
		}
	}

	public void SetText(string text)
	{
		this.Clear();
		this.mText = text;
		this.isStart = true;
		if (base.get_gameObject().get_activeInHierarchy())
		{
			base.StartCoroutine(this.UpdateText());
		}
	}

	public void StopText()
	{
		this.Clear();
		this.Label.set_text(this.mText);
		this.IsWritering = false;
	}

	private void Clear()
	{
		this.isStart = false;
		this.mOffset = 0;
		this.mNextChar = 0f;
		this.Label.set_text(string.Empty);
	}

	[DebuggerHidden]
	private IEnumerator UpdateText()
	{
		Textwriter.<UpdateText>c__Iterator67 <UpdateText>c__Iterator = new Textwriter.<UpdateText>c__Iterator67();
		<UpdateText>c__Iterator.<>f__this = this;
		return <UpdateText>c__Iterator;
	}
}

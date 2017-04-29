using System;
using UnityEngine;
using UnityEngine.UI;

public class SignInTab : MonoBehaviour
{
	public SignInUI.SignInUIState uiType;

	private Action callback;

	private void Awake()
	{
		ButtonCustom component = base.GetComponent<ButtonCustom>();
		component.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnButtonClick);
	}

	private void OnButtonClick(GameObject sender)
	{
		if (this.callback != null)
		{
			this.callback.Invoke();
		}
	}

	public void SetIcon(string icon1, string icon2)
	{
		ResourceManager.SetSprite(base.get_transform().FindChild("ImageBG1/Icon1").GetComponent<Image>(), ResourceManager.GetCodeSprite(icon1));
		ResourceManager.SetSprite(base.get_transform().FindChild("ImageBG2/Icon2").GetComponent<Image>(), ResourceManager.GetCodeSprite(icon2));
	}

	public void SetName(string name)
	{
		base.get_transform().FindChild("Text1").GetComponent<Text>().set_text(name);
		base.get_transform().FindChild("Text2").GetComponent<Text>().set_text(name);
	}

	public void SetCallback(Action action)
	{
		this.callback = action;
	}

	public void SetIsSelected(bool isSelected)
	{
		base.get_transform().FindChild("ImageBG1").get_gameObject().SetActive(!isSelected);
		base.get_transform().FindChild("Text1").get_gameObject().SetActive(!isSelected);
		base.get_transform().FindChild("ImageBG2").get_gameObject().SetActive(isSelected);
		base.get_transform().FindChild("Text2").get_gameObject().SetActive(isSelected);
	}

	public void SetImageBadage(bool tip)
	{
		base.get_transform().FindChild("ImageBadage").get_gameObject().SetActive(tip);
	}
}

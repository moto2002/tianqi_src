using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class RoleMouse : MonoBehaviour
{
	public bool isMen;

	private bool isZoom;

	private Vector3 mousePos;

	public Transform obj1;

	public Transform obj2;

	public Transform obj3;

	public Transform obj4;

	private Color color1;

	private Color color2;

	private Color color3;

	private Color color4;

	private Color dim = new Color(0.2f, 0.2f, 0.2f, 1f);

	private bool Islight = true;

	private void Start()
	{
		this.color1 = this.obj1.GetComponent<Renderer>().get_material().get_color();
		this.color2 = this.obj2.GetComponent<Renderer>().get_material().get_color();
		this.color3 = this.obj3.GetComponent<Renderer>().get_material().get_color();
		this.color4 = this.obj4.GetComponent<Renderer>().get_material().get_color();
		EventDispatcher.AddListener<PlayerCareerType>("UpdateSwitchLightDim", new Callback<PlayerCareerType>(this.UpdateSwitchLightDim));
	}

	private void OnDrawn()
	{
	}

	private void OnMouseDown()
	{
		this.mousePos = Input.get_mousePosition();
	}

	private void OnMouseUp()
	{
		if (Vector3.Distance(this.mousePos, Input.get_mousePosition()) < 12f && !LoginManager.Instance.IsCreatAnimationing)
		{
			EventDispatcher.Broadcast<bool>("ClickCameraMove", this.isMen);
		}
	}

	private void UpdateSwitchLightDim(PlayerCareerType arg1)
	{
		switch (arg1)
		{
		case PlayerCareerType.None:
			base.StartCoroutine(this.ChangeColor(true));
			break;
		case PlayerCareerType.Saber:
			if (this.isMen)
			{
				base.StartCoroutine(this.ChangeColor(true));
			}
			else
			{
				base.StartCoroutine(this.ChangeColor(false));
			}
			break;
		case PlayerCareerType.Basaker:
			if (!this.isMen)
			{
				base.StartCoroutine(this.ChangeColor(true));
			}
			else
			{
				base.StartCoroutine(this.ChangeColor(false));
			}
			break;
		}
	}

	[DebuggerHidden]
	private IEnumerator ChangeColor(bool isLight)
	{
		RoleMouse.<ChangeColor>c__Iterator62 <ChangeColor>c__Iterator = new RoleMouse.<ChangeColor>c__Iterator62();
		<ChangeColor>c__Iterator.isLight = isLight;
		<ChangeColor>c__Iterator.<$>isLight = isLight;
		<ChangeColor>c__Iterator.<>f__this = this;
		return <ChangeColor>c__Iterator;
	}
}

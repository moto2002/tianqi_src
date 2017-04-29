using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class NcDrawFpsRect : MonoBehaviour
{
	public bool centerTop = true;

	public Rect startRect = new Rect(0f, 0f, 75f, 50f);

	public bool updateColor = true;

	public bool allowDrag = true;

	public float frequency = 0.5f;

	public int nbDecimal = 1;

	private float accum;

	private int frames;

	private Color color = Color.get_white();

	private string sFPS = string.Empty;

	private GUIStyle style;

	private void Start()
	{
		base.StartCoroutine(this.FPS());
	}

	private void Update()
	{
		this.accum += Time.get_timeScale() / Time.get_deltaTime();
		this.frames++;
	}

	[DebuggerHidden]
	private IEnumerator FPS()
	{
		NcDrawFpsRect.<FPS>c__Iterator5 <FPS>c__Iterator = new NcDrawFpsRect.<FPS>c__Iterator5();
		<FPS>c__Iterator.<>f__this = this;
		return <FPS>c__Iterator;
	}

	private void OnGUI()
	{
		if (this.style == null)
		{
			this.style = new GUIStyle(GUI.get_skin().get_label());
			this.style.get_normal().set_textColor(Color.get_white());
			this.style.set_alignment(4);
		}
		GUI.set_color((!this.updateColor) ? Color.get_white() : this.color);
		Rect rect = this.startRect;
		if (this.centerTop)
		{
			rect.set_x(rect.get_x() + ((float)(Screen.get_width() / 2) - rect.get_width() / 2f));
		}
		this.startRect = GUI.Window(0, rect, new GUI.WindowFunction(this.DoMyWindow), string.Empty);
		if (this.centerTop)
		{
			this.startRect.set_x(this.startRect.get_x() - ((float)(Screen.get_width() / 2) - rect.get_width() / 2f));
		}
	}

	private void DoMyWindow(int windowID)
	{
		GUI.Label(new Rect(0f, 0f, this.startRect.get_width(), this.startRect.get_height()), this.sFPS + " FPS", this.style);
		if (this.allowDrag)
		{
			GUI.DragWindow(new Rect(0f, 0f, (float)Screen.get_width(), (float)Screen.get_height()));
		}
	}
}

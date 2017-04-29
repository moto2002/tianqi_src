using System;
using UnityEngine;

public class NcDrawFpsText : MonoBehaviour
{
	public float updateInterval = 0.5f;

	private float accum;

	private int frames;

	private float timeleft;

	private void Start()
	{
		if (!base.GetComponent<GUIText>())
		{
			Debuger.Info("UtilityFramesPerSecond needs a GUIText component!", new object[0]);
			base.set_enabled(false);
			return;
		}
		this.timeleft = this.updateInterval;
	}

	private void Update()
	{
		this.timeleft -= Time.get_deltaTime();
		this.accum += Time.get_timeScale() / Time.get_deltaTime();
		this.frames++;
		if ((double)this.timeleft <= 0.0)
		{
			float num = this.accum / (float)this.frames;
			string text = string.Format("{0:F2} FPS", num);
			base.GetComponent<GUIText>().set_text(text);
			if (num < 30f)
			{
				base.GetComponent<GUIText>().get_material().set_color(Color.get_yellow());
			}
			else if (num < 10f)
			{
				base.GetComponent<GUIText>().get_material().set_color(Color.get_red());
			}
			else
			{
				base.GetComponent<GUIText>().get_material().set_color(Color.get_green());
			}
			this.timeleft = this.updateInterval;
			this.accum = 0f;
			this.frames = 0;
		}
	}
}

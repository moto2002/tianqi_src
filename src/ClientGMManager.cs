using GameData;
using Package;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using XEngine.AssetLoader;
using XEngineActor;
using XNetwork;

public class ClientGMManager : MonoBehaviour
{
	private static ClientGMManager instance;

	public static string PermissionCode = "permissionxxx";

	public static bool IsPermission;

	protected bool gmOpen;

	protected static GUIStyle style;

	public string cmd = string.Empty;

	public string result = string.Empty;

	protected bool netSwitch00;

	protected static bool netSwitch01;

	protected bool timeSwitch00;

	protected bool battleSwitch00 = true;

	protected bool battleLog;

	public List<KeyValuePair<int, DateTime>> SendSettleReq = new List<KeyValuePair<int, DateTime>>();

	public List<DateTime> NetSendSettleReq = new List<DateTime>();

	public List<KeyValuePair<DateTime, SocketError>> NetSendSettleReqCode = new List<KeyValuePair<DateTime, SocketError>>();

	public static int fxout_id;

	protected bool isAlwaysOpen;

	private List<ActorModel> testModels = new List<ActorModel>();

	private int spine_uuid;

	public static string spineout_ids = string.Empty;

	public static ClientGMManager Instance
	{
		get
		{
			if (ClientGMManager.instance == null)
			{
				GameObject gameObject = new GameObject();
				gameObject.set_name("GM");
				ClientGMManager.instance = gameObject.AddUniqueComponent<ClientGMManager>();
			}
			return ClientGMManager.instance;
		}
	}

	public bool GMOpen
	{
		get
		{
			return this.gmOpen;
		}
		set
		{
			this.gmOpen = value;
		}
	}

	public bool NetSwitch00
	{
		get
		{
			return this.netSwitch00;
		}
	}

	public static bool NetSwitch01
	{
		get
		{
			return ClientGMManager.netSwitch01;
		}
	}

	public bool TimeSwitch00
	{
		get
		{
			return this.timeSwitch00;
		}
	}

	public bool BattleSwitch00
	{
		get
		{
			return this.battleSwitch00;
		}
	}

	public bool BattleLog
	{
		get
		{
			return this.battleLog;
		}
	}

	public bool IsAlwaysOpen
	{
		get
		{
			return this.isAlwaysOpen;
		}
		set
		{
			this.isAlwaysOpen = value;
		}
	}

	public bool IsPermissionOn(int channel)
	{
		return !SDKManager.Instance.HasSDK() || (ClientGMManager.IsPermission && channel == 4);
	}

	private void OnGUI()
	{
		if (!this.gmOpen)
		{
			return;
		}
		this.SetStyle();
		this.cmd = GUI.TextArea(new Rect(0f, 0f, (float)(Screen.get_width() - 50), 30f), this.cmd);
		if (GUI.Button(new Rect((float)(Screen.get_width() - 50), 0f, 50f, 30f), "GM") && this.cmd != null)
		{
			if (this.cmd.StartsWith(ChatManager.GM_PREFIX_SERVER_AND_CLIENT))
			{
				ChatManager.Instance.SendGMCommand(0, this.cmd);
				string cmd_normal = this.cmd.ReplaceFirst(ChatManager.GM_PREFIX_SERVER_AND_CLIENT, string.Empty, 0);
				this.result = this.GetGMResult(cmd_normal);
				this.cmd = string.Empty;
				if (DebugUIView.Instance != null)
				{
					DebugUIView.Instance.SetOutput(this.result);
				}
			}
			else if (this.cmd.StartsWith(ChatManager.GM_PREFIX_SERVER))
			{
				ChatManager.Instance.SendGMCommand(0, this.cmd);
				this.cmd = string.Empty;
			}
			else
			{
				this.result = this.GetGMResult(this.cmd);
				this.cmd = string.Empty;
				if (DebugUIView.Instance != null)
				{
					DebugUIView.Instance.SetOutput(this.result);
				}
			}
		}
		GUI.TextField(new Rect(0f, 30f, (float)Screen.get_width(), (float)(Screen.get_height() - 30)), this.result, ClientGMManager.style);
	}

	private void SetStyle()
	{
		ClientGMManager.style = GUI.get_skin().get_textField();
		ClientGMManager.style.set_wordWrap(true);
	}

	public string GetGMResult(string cmd_normal)
	{
		StringBuilder stringBuilder = new StringBuilder();
		string resolution = cmd_normal.ToLower();
		if (!this.close(resolution))
		{
			if (!this.netshut(resolution))
			{
				if (!this.netstate(resolution, ref stringBuilder))
				{
					if (!this.pingopen(resolution))
					{
						if (!this.pingclose(resolution))
						{
							if (!this.packetInterval(resolution))
							{
								if (!this.netcheck(resolution))
								{
									if (!this.settle(resolution, ref stringBuilder))
									{
										if (!this.fxclose(resolution))
										{
											if (!this.timecheck(resolution))
											{
												if (!this.battlecheck(resolution))
												{
													if (!this.peopleshow(resolution))
													{
														if (!this.showallinfo(resolution, ref stringBuilder))
														{
															if (!this.showallattr(resolution, ref stringBuilder))
															{
																if (!this.showallactor(resolution, ref stringBuilder))
																{
																	if (!this.showall(resolution, ref stringBuilder))
																	{
																		if (!this.showinfo(resolution, ref stringBuilder))
																		{
																			if (!this.showattr(resolution, ref stringBuilder))
																			{
																				if (!this.showactor(resolution, ref stringBuilder))
																				{
																					if (!this.show(resolution, ref stringBuilder))
																					{
																						if (!this.showselfisclose(resolution))
																						{
																							if (!this.startallai(resolution))
																							{
																								if (!this.startallmonsterai(resolution))
																								{
																									if (!this.startallpetai(resolution))
																									{
																										if (!this.stopai(resolution))
																										{
																											if (!this.stopallai(resolution))
																											{
																												if (!this.stopallplayerai(resolution))
																												{
																													if (!this.stopallmonsterai(resolution))
																													{
																														if (!this.stopallpetai(resolution))
																														{
																															if (!this.checkai(resolution, ref stringBuilder))
																															{
																																if (!this.skillindex(resolution))
																																{
																																	if (!this.skill(resolution))
																																	{
																																		if (!this.stoptime(resolution))
																																		{
																																			if (!this.kill(resolution))
																																			{
																																				if (!this.buff(resolution))
																																				{
																																					if (!this.fusetime(resolution))
																																					{
																																						if (!this.hp(resolution))
																																						{
																																							if (!this.actionforce(resolution))
																																							{
																																								if (!this.action(resolution))
																																								{
																																									if (!this.monster(resolution))
																																									{
																																										if (!this.copynow(resolution))
																																										{
																																											if (!this.copy(resolution))
																																											{
																																												if (!this.wudi(resolution))
																																												{
																																													if (!this.youdi(resolution))
																																													{
																																														if (!this.shizimao(resolution))
																																														{
																																															if (!this.lineon(resolution))
																																															{
																																																if (!this.lineoff(resolution))
																																																{
																																																	if (!this.clearcd(resolution))
																																																	{
																																																		if (!this.battlelog(resolution))
																																																		{
																																																			if (!this.gotocity(resolution))
																																																			{
																																																				if (!this.gotoo(resolution))
																																																				{
																																																					if (!this.allpos(resolution, ref stringBuilder))
																																																					{
																																																						if (!this.fx(resolution))
																																																						{
																																																							if (!this.pp(resolution))
																																																							{
																																																								if (!this.lod(resolution))
																																																								{
																																																									if (!this.fog(resolution))
																																																									{
																																																										if (!this.log(resolution))
																																																										{
																																																											if (!this.headinfo(resolution))
																																																											{
																																																												if (!this.fps(resolution))
																																																												{
																																																													if (!this.adfps(resolution))
																																																													{
																																																														if (!this.debug(resolution))
																																																														{
																																																															if (!this.ping(resolution))
																																																															{
																																																																if (!this.release(resolution))
																																																																{
																																																																	if (!this.xgpush(resolution))
																																																																	{
																																																																		if (!this.playfx(resolution))
																																																																		{
																																																																			if (!this.guidelog(resolution, ref stringBuilder))
																																																																			{
																																																																				if (!this.guide(resolution))
																																																																				{
																																																																					if (!this.uilocklog(resolution, ref stringBuilder))
																																																																					{
																																																																						if (!this.ref_control(resolution))
																																																																						{
																																																																							if (!this.resdebug(resolution))
																																																																							{
																																																																								if (!this.modelcreate(resolution))
																																																																								{
																																																																									if (!this.modeladd(resolution))
																																																																									{
																																																																										if (!this.modelremove(resolution))
																																																																										{
																																																																											if (!this.scenenormal(resolution))
																																																																											{
																																																																												if (!this.scenetest(resolution))
																																																																												{
																																																																													if (!this.loaddata(resolution))
																																																																													{
																																																																														if (!this.playspine(resolution))
																																																																														{
																																																																															if (!this.updateClient(resolution))
																																																																															{
																																																																																if (!this.waitui1(resolution))
																																																																																{
																																																																																	if (!this.waitui2(resolution))
																																																																																	{
																																																																																		if (!this.unloadui(resolution))
																																																																																		{
																																																																																			if (!this.wifi(resolution))
																																																																																			{
																																																																																				if (!this.chatui(resolution))
																																																																																				{
																																																																																					if (!this.voice_play(resolution))
																																																																																					{
																																																																																						if (!this.autotask(resolution))
																																																																																						{
																																																																																							if (!this.check_common_rendertexture(resolution))
																																																																																							{
																																																																																								if (!this.set_resolution(resolution))
																																																																																								{
																																																																																									if (!this.load_asset(cmd_normal))
																																																																																									{
																																																																																										if (!this.release_asset(resolution))
																																																																																										{
																																																																																											if (!this.refresh_materials(resolution))
																																																																																											{
																																																																																												if (!this.unlock_all_system(resolution))
																																																																																												{
																																																																																													if (!this.relock_system(resolution))
																																																																																													{
																																																																																														if (!this.isTriggerException(resolution))
																																																																																														{
																																																																																															if (!this.isTriggerCrash(resolution))
																																																																																															{
																																																																																																if (!this.GC(resolution))
																																																																																																{
																																																																																																	if (!this.ApkMd5(resolution))
																																																																																																	{
																																																																																																		if (!this.PkgName(resolution))
																																																																																																		{
																																																																																																			if (!this.UseLan(resolution))
																																																																																																			{
																																																																																																				if (!this.guide_switch(resolution))
																																																																																																				{
																																																																																																					if (!this.GuildBossModelTest(resolution))
																																																																																																					{
																																																																																																						if (!this.PreloadEnd(resolution))
																																																																																																						{
																																																																																																							if (!this.bgm(resolution))
																																																																																																							{
																																																																																																								if (!this.UpdateCanvas(resolution))
																																																																																																								{
																																																																																																									if (!this.LoadSpeed(resolution))
																																																																																																									{
																																																																																																										if (!this.VersionCode(resolution))
																																																																																																										{
																																																																																																											if (!this.OpenKF(resolution))
																																																																																																											{
																																																																																																												if (!this.CloseKF(resolution))
																																																																																																												{
																																																																																																													if (this.Restart(resolution))
																																																																																																													{
																																																																																																													}
																																																																																																												}
																																																																																																											}
																																																																																																										}
																																																																																																									}
																																																																																																								}
																																																																																																							}
																																																																																																						}
																																																																																																					}
																																																																																																				}
																																																																																																			}
																																																																																																		}
																																																																																																	}
																																																																																																}
																																																																																															}
																																																																																														}
																																																																																													}
																																																																																												}
																																																																																											}
																																																																																										}
																																																																																									}
																																																																																								}
																																																																																							}
																																																																																						}
																																																																																					}
																																																																																				}
																																																																																			}
																																																																																		}
																																																																																	}
																																																																																}
																																																																															}
																																																																														}
																																																																													}
																																																																												}
																																																																											}
																																																																										}
																																																																									}
																																																																								}
																																																																							}
																																																																						}
																																																																					}
																																																																				}
																																																																			}
																																																																		}
																																																																	}
																																																																}
																																																															}
																																																														}
																																																													}
																																																												}
																																																											}
																																																										}
																																																									}
																																																								}
																																																							}
																																																						}
																																																					}
																																																				}
																																																			}
																																																		}
																																																	}
																																																}
																																															}
																																														}
																																													}
																																												}
																																											}
																																										}
																																									}
																																								}
																																							}
																																						}
																																					}
																																				}
																																			}
																																		}
																																	}
																																}
																															}
																														}
																													}
																												}
																											}
																										}
																									}
																								}
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
		return stringBuilder.Append("\n").ToString();
	}

	protected string ShowAllEntity()
	{
		StringBuilder result = new StringBuilder();
		this.AllEntityDoAction(delegate(EntityParent e)
		{
			result.Append(e.GetString());
			result.Append("\n");
		});
		return result.ToString();
	}

	protected string ShowEntity(long id)
	{
		StringBuilder result = new StringBuilder();
		if (!this.EntityDoAction(id, delegate(EntityParent e)
		{
			result.Append(e.GetString());
			result.Append("\n");
		}))
		{
			result.Append("ID ");
			result.Append(id);
			result.Append("Not Exist!\n");
		}
		return result.ToString();
	}

	protected string ShowAllEntityInfo()
	{
		StringBuilder result = new StringBuilder();
		this.AllEntityDoAction(delegate(EntityParent e)
		{
			result.Append(e.GetInfoString());
			result.Append("\n");
		});
		return result.ToString();
	}

	protected string ShowAllEntityAttr()
	{
		StringBuilder result = new StringBuilder();
		this.AllEntityDoAction(delegate(EntityParent e)
		{
			result.Append(e.GetAttrString());
			result.Append("\n");
		});
		return result.ToString();
	}

	protected string ShowAllEntityActor()
	{
		StringBuilder result = new StringBuilder();
		this.AllEntityDoAction(delegate(EntityParent e)
		{
			result.Append(e.GetActorString());
			result.Append("\n");
		});
		return result.ToString();
	}

	protected string ShowEntityInfo(long id)
	{
		StringBuilder result = new StringBuilder();
		if (!this.EntityDoAction(id, delegate(EntityParent e)
		{
			result.Append(e.GetInfoString());
			result.Append("\n");
		}))
		{
			result.Append("ID ");
			result.Append(id);
			result.Append("Not Exist!\n");
		}
		return result.ToString();
	}

	protected string ShowEntityAttr(long id)
	{
		StringBuilder result = new StringBuilder();
		if (!this.EntityDoAction(id, delegate(EntityParent e)
		{
			result.Append(e.GetAttrString());
			result.Append("\n");
		}))
		{
			result.Append("ID ");
			result.Append(id);
			result.Append("Not Exist!\n");
		}
		return result.ToString();
	}

	protected string ShowEntityActor(long id)
	{
		StringBuilder result = new StringBuilder();
		if (!this.EntityDoAction(id, delegate(EntityParent e)
		{
			result.Append(e.GetActorString());
			result.Append("\n");
		}))
		{
			result.Append("ID ");
			result.Append(id);
			result.Append("Not Exist!\n");
		}
		return result.ToString();
	}

	private bool startallai(string cmd)
	{
		if (cmd.StartsWith("startallai"))
		{
			EntityWorld.Instance.GetEntities<EntityMonster>().Values.ForEach(delegate(EntityParent e)
			{
				e.GetAIManager().Active();
			});
			EntityWorld.Instance.GetEntities<EntityPet>().Values.ForEach(delegate(EntityParent e)
			{
				e.GetAIManager().Active();
			});
			return true;
		}
		return false;
	}

	private bool startallmonsterai(string cmd)
	{
		if (cmd.StartsWith("startallmonsterai"))
		{
			EntityWorld.Instance.GetEntities<EntityMonster>().Values.ForEach(delegate(EntityParent e)
			{
				e.GetAIManager().Active();
			});
			return true;
		}
		return false;
	}

	private bool startallpetai(string cmd)
	{
		if (cmd.StartsWith("startallpetai"))
		{
			EntityWorld.Instance.GetEntities<EntityPet>().Values.ForEach(delegate(EntityParent e)
			{
				e.GetAIManager().Active();
			});
			return true;
		}
		return false;
	}

	private bool updateClient(string cmd)
	{
		return cmd.StartsWith("updateclient");
	}

	private bool stopai(string cmd)
	{
		if (cmd.StartsWith("stopai"))
		{
			long id = long.Parse(cmd.Replace("stopai", string.Empty).Trim(new char[]
			{
				' '
			}));
			EntityWorld.Instance.AllEntities.Values.ForEach(delegate(EntityParent e)
			{
				if (id == e.ID)
				{
					e.GetAIManager().Deactive();
				}
			});
			return true;
		}
		return false;
	}

	private bool stopallai(string cmd)
	{
		if (cmd.StartsWith("stopallai"))
		{
			ClientGMManager.Stopallai();
			return true;
		}
		return false;
	}

	private bool stopallplayerai(string cmd)
	{
		if (cmd.StartsWith("stopallplayerai"))
		{
			EntityWorld.Instance.GetEntities<EntityPlayer>().Values.ForEach(delegate(EntityParent e)
			{
				e.GetAIManager().Deactive();
			});
			return true;
		}
		return false;
	}

	private bool stopallmonsterai(string cmd)
	{
		if (cmd.StartsWith("stopallmonsterai"))
		{
			EntityWorld.Instance.GetEntities<EntityMonster>().Values.ForEach(delegate(EntityParent e)
			{
				e.GetAIManager().Deactive();
			});
			return true;
		}
		return false;
	}

	private bool stopallpetai(string cmd)
	{
		if (cmd.StartsWith("stopallpetai"))
		{
			EntityWorld.Instance.GetEntities<EntityPet>().Values.ForEach(delegate(EntityParent e)
			{
				e.GetAIManager().Deactive();
			});
			return true;
		}
		return false;
	}

	private bool checkai(string cmd, ref StringBuilder sb)
	{
		if (cmd.StartsWith("checkai"))
		{
			sb.Append(string.Concat(new object[]
			{
				"beginDragTimes: ",
				SelfAIControlManager.Instance.beginDragTimes,
				" finishDragTimes: ",
				SelfAIControlManager.Instance.finishDragTimes,
				" buttonDownTimes: ",
				SelfAIControlManager.Instance.buttonDownTimes,
				" buttonUpTimes: ",
				SelfAIControlManager.Instance.buttonUpTimes
			}));
			return true;
		}
		return false;
	}

	private bool skillindex(string cmd)
	{
		if (cmd.StartsWith("skillindex"))
		{
			string[] array = cmd.Replace("skillindex", string.Empty).Split(new char[]
			{
				' '
			});
			long casterID = long.Parse(array[1]);
			int skillIndex = int.Parse(array[2]);
			long targetID = 0L;
			if (array.Length > 3)
			{
				targetID = long.Parse(array[3]);
			}
			EntityWorld.Instance.AllEntities.Values.ForEach(delegate(EntityParent e)
			{
				if (casterID != e.ID)
				{
					return;
				}
				int skillID = 0;
				e.GetSkillManager().GetSkillIDByIndex(skillIndex, out skillID);
				if (targetID != 0L && EntityWorld.Instance.AllEntities.ContainsKey(targetID))
				{
					e.AITarget = EntityWorld.Instance.AllEntities[targetID];
				}
				else
				{
					e.GetAIManager().SetTargetBySkillIndex(skillIndex, TargetRangeType.World, false, 0f);
				}
				e.GetSkillManager().ClientHandleSkillByID(skillID);
			});
			return true;
		}
		return false;
	}

	private bool skill(string cmd)
	{
		if (cmd.StartsWith("skill"))
		{
			string[] array = cmd.Replace("skill", string.Empty).Split(new char[]
			{
				' '
			});
			long casterID = long.Parse(array[1]);
			int skillID = int.Parse(array[2]);
			long targetID = 0L;
			if (array.Length > 3)
			{
				targetID = long.Parse(array[3]);
			}
			EntityWorld.Instance.AllEntities.Values.ForEach(delegate(EntityParent e)
			{
				if (casterID != e.ID)
				{
					return;
				}
				if (targetID != 0L && EntityWorld.Instance.AllEntities.ContainsKey(targetID))
				{
					e.AITarget = EntityWorld.Instance.AllEntities[targetID];
				}
				else
				{
					e.AITarget = EntityWorld.Instance.EntSelf;
				}
				e.GetSkillManager().ClientHandleSkillByID(skillID);
			});
			return true;
		}
		return false;
	}

	private bool showallinfo(string cmd, ref StringBuilder result)
	{
		if (cmd.StartsWith("showallinfo"))
		{
			result.Append(this.ShowAllEntityInfo());
			return true;
		}
		return false;
	}

	private bool showallattr(string cmd, ref StringBuilder result)
	{
		if (cmd.StartsWith("showallattr"))
		{
			result.Append(this.ShowAllEntityAttr());
			return true;
		}
		return false;
	}

	private bool showallactor(string cmd, ref StringBuilder result)
	{
		if (cmd.StartsWith("showallactor"))
		{
			result.Append(this.ShowAllEntityActor());
			return true;
		}
		return false;
	}

	private bool showall(string cmd, ref StringBuilder result)
	{
		if (cmd.StartsWith("showall"))
		{
			result.Append(this.ShowAllEntity());
			return true;
		}
		return false;
	}

	private bool showinfo(string cmd, ref StringBuilder result)
	{
		if (cmd.StartsWith("showinfo"))
		{
			long id = long.Parse(cmd.Replace("showinfo", string.Empty).Trim(new char[]
			{
				' '
			}));
			result.Append(this.ShowEntityInfo(id));
			return true;
		}
		return false;
	}

	private bool showattr(string cmd, ref StringBuilder result)
	{
		if (cmd.StartsWith("showattr"))
		{
			long id = long.Parse(cmd.Replace("showattr", string.Empty).Trim(new char[]
			{
				' '
			}));
			result.Append(this.ShowEntityAttr(id));
			return true;
		}
		return false;
	}

	private bool showactor(string cmd, ref StringBuilder result)
	{
		if (cmd.StartsWith("showactor"))
		{
			long id = long.Parse(cmd.Replace("showactor", string.Empty).Trim(new char[]
			{
				' '
			}));
			result.Append(this.ShowEntityActor(id));
			return true;
		}
		return false;
	}

	private bool show(string cmd, ref StringBuilder result)
	{
		if (cmd.StartsWith("show"))
		{
			long id = long.Parse(cmd.Replace("show", string.Empty).Trim(new char[]
			{
				' '
			}));
			result.Append(this.ShowEntity(id));
			return true;
		}
		return false;
	}

	private bool showselfisclose(string cmd)
	{
		if (cmd.StartsWith("showself"))
		{
			Debug.LogError("==>IsCloseRenderer = " + EntityWorld.Instance.EntSelf.IsCloseRenderer);
			return true;
		}
		return false;
	}

	private bool gotocity(string cmd)
	{
		if (cmd.StartsWith("gotocity"))
		{
			string[] array = cmd.Replace("gotocity", string.Empty).Split(new char[]
			{
				' '
			});
			int num = 0;
			if (array.Length > 1)
			{
				num = int.Parse(array[1]);
			}
			AOIService.MaxCounter = AOIService.DefaultMaxCounter;
			InstanceManager.ChangeInstanceManager(num, false);
			MySceneManager.Instance.SwitchMapResp(0, new SwitchMapRes
			{
				oldMapId = MySceneManager.Instance.CurSceneID,
				newMapId = num,
				mapLayer = 0
			});
			return true;
		}
		return false;
	}

	private bool gotoo(string cmd)
	{
		if (cmd.StartsWith("goto"))
		{
			string[] array = cmd.Replace("goto", string.Empty).Split(new char[]
			{
				' '
			});
			int instanceDataID = int.Parse(array[1]);
			AOIService.MaxCounter = 0;
			InstanceManager.ChangeInstanceManager(instanceDataID, false);
			InstanceManager.SimulateEnterField(InstanceManager.CurrentInstanceData.type, null);
			InstanceManager.SimulateSwicthMap(InstanceManager.CurrentInstanceData.scene, LocalInstanceHandler.Instance.CreateSelfInfo(InstanceManager.CurrentInstanceData.type, InstanceManager.CurrentInstanceDataID, InstanceManager.CurrentInstanceData.scene, 0, 0, null, null, null), null, 0);
			return true;
		}
		return false;
	}

	private bool stoptime(string cmd)
	{
		if (cmd.StartsWith("stoptime"))
		{
			LocalInstanceHandler.Instance.FinishTimeLimit = 2147483647;
			return true;
		}
		return false;
	}

	private bool kill(string cmd)
	{
		if (cmd.StartsWith("kill"))
		{
			string[] array = cmd.Replace("kill", string.Empty).Split(new char[]
			{
				' '
			});
			for (int j = 1; j < array.Length; j++)
			{
				if (array[j].Contains("-"))
				{
					string[] array2 = array[j].Split(new char[]
					{
						'-'
					});
					long num = long.Parse(array2[0]);
					long num2 = long.Parse(array2[1]);
					if (num > num2 || num2 - num > 100L)
					{
						return true;
					}
					long i;
					for (i = num; i <= num2; i += 1L)
					{
						EntityWorld.Instance.AllEntities.Values.ForEach(delegate(EntityParent e)
						{
							if (i != e.ID)
							{
								return;
							}
							LocalBattleHitHandler.AppDead(e.ID);
							e.Hp = 0L;
						});
					}
				}
				else
				{
					long id = long.Parse(array[j]);
					EntityWorld.Instance.AllEntities.Values.ForEach(delegate(EntityParent e)
					{
						if (id != e.ID)
						{
							return;
						}
						LocalBattleHitHandler.AppDead(e.ID);
						e.Hp = 0L;
					});
				}
			}
			return true;
		}
		return false;
	}

	private bool buff(string cmd)
	{
		if (cmd.StartsWith("buff"))
		{
			string[] array = cmd.Replace("buff", string.Empty).Split(new char[]
			{
				' '
			});
			long id = long.Parse(array[1]);
			long num = long.Parse(array[2]);
			int buffID = int.Parse(array[3]);
			EntityParent entityByID = EntityWorld.Instance.GetEntityByID(id);
			EntityParent entityByID2 = EntityWorld.Instance.GetEntityByID(num);
			LocalBattleHandler.Instance.AppAddBuff(buffID, entityByID, num, 0, 0, false);
			return true;
		}
		return false;
	}

	private bool fusetime(string cmd)
	{
		if (cmd.StartsWith("fusetime"))
		{
			string[] array = cmd.Replace("fusetime", string.Empty).Split(new char[]
			{
				' '
			});
			long num = long.Parse(array[1]);
			EntityWorld.Instance.EntSelf.TotalFuseTimePlus = (float)(num * 1000L);
			return true;
		}
		return false;
	}

	private bool hp(string cmd)
	{
		if (cmd.StartsWith("hp"))
		{
			string[] array = cmd.Replace("hp", string.Empty).Split(new char[]
			{
				' '
			});
			int hp = int.Parse(array[2]);
			if (array[1].Contains("-"))
			{
				string[] array2 = array[1].Split(new char[]
				{
					'-'
				});
				long num = long.Parse(array2[0]);
				long num2 = long.Parse(array2[1]);
				if (num > num2 || num2 - num > 100L)
				{
					return true;
				}
				long i;
				for (i = num; i <= num2; i += 1L)
				{
					EntityWorld.Instance.AllEntities.Values.ForEach(delegate(EntityParent e)
					{
						if (i == e.ID)
						{
							LocalInstanceHandler.Instance.SetSpiritCurHp(e, (long)hp);
							e.Hp = (long)hp;
						}
					});
				}
			}
			else
			{
				long targetID = long.Parse(array[1]);
				EntityWorld.Instance.AllEntities.Values.ForEach(delegate(EntityParent e)
				{
					if (targetID == e.ID)
					{
						LocalInstanceHandler.Instance.SetSpiritCurHp(e, (long)hp);
						e.Hp = (long)hp;
					}
				});
			}
			return true;
		}
		return false;
	}

	private bool actionforce(string cmd)
	{
		if (cmd.StartsWith("actionforce"))
		{
			string[] array = cmd.Replace("actionforce", string.Empty).Split(new char[]
			{
				' '
			});
			long id = long.Parse(array[1]);
			string actionStatusName = array[2];
			this.EntityDoAction(id, delegate(EntityParent e)
			{
				if (e.Actor)
				{
					e.Actor.ChangeAction(actionStatusName, true, true, 1f, 0, 0, string.Empty);
				}
			});
			return true;
		}
		return false;
	}

	private bool action(string cmd)
	{
		if (cmd.StartsWith("action"))
		{
			string[] array = cmd.Replace("action", string.Empty).Split(new char[]
			{
				' '
			});
			long id = long.Parse(array[1]);
			string actionStatusName = array[2];
			this.EntityDoAction(id, delegate(EntityParent e)
			{
				if (e.Actor)
				{
					e.Actor.ChangeAction(actionStatusName, false, true, 1f, 0, 0, string.Empty);
				}
			});
			return true;
		}
		return false;
	}

	private bool monster(string cmd)
	{
		if (cmd.StartsWith("monster"))
		{
			string[] array = cmd.Replace("monster", string.Empty).Split(new char[]
			{
				' '
			});
			int monsterTypeID = int.Parse(array[1]);
			int monsterLevel = 1;
			if (array.Length > 2)
			{
				monsterLevel = int.Parse(array[2]);
			}
			int camp = 3;
			if (array.Length > 3)
			{
				camp = int.Parse(array[3]);
			}
			int pointGroupID = 0;
			if (array.Length > 4)
			{
				pointGroupID = int.Parse(array[4]);
			}
			bool isBoss = false;
			if (array.Length > 5)
			{
				isBoss = (int.Parse(array[5]) != 0);
			}
			LocalInstanceHandler.Instance.CreateGMMonster(monsterTypeID, monsterLevel, camp, pointGroupID, isBoss);
			return true;
		}
		return false;
	}

	private bool copynow(string cmd)
	{
		if (cmd.StartsWith("copynow"))
		{
			string[] array = cmd.Replace("copynow", string.Empty).Split(new char[]
			{
				' '
			});
			int pointGroupID = 0;
			if (array.Length > 1)
			{
				pointGroupID = int.Parse(array[1]);
			}
			bool isSameCamp = false;
			if (array.Length > 2)
			{
				isSameCamp = (int.Parse(array[2]) == 0);
			}
			LocalInstanceHandler.Instance.CopySelf(pointGroupID, isSameCamp, true);
			return true;
		}
		return false;
	}

	private bool copy(string cmd)
	{
		if (cmd.StartsWith("copy"))
		{
			string[] array = cmd.Replace("copy", string.Empty).Split(new char[]
			{
				' '
			});
			int pointGroupID = 0;
			if (array.Length > 1)
			{
				pointGroupID = int.Parse(array[1]);
			}
			bool isSameCamp = false;
			if (array.Length > 2)
			{
				isSameCamp = (int.Parse(array[2]) == 0);
			}
			LocalInstanceHandler.Instance.CopySelf(pointGroupID, isSameCamp, false);
			return true;
		}
		return false;
	}

	private bool wudi(string cmd)
	{
		if (cmd.StartsWith("wudi"))
		{
			string[] array = cmd.Replace("wudi", string.Empty).Split(new char[]
			{
				' '
			});
			long targetID = long.Parse(array[1]);
			EntityWorld.Instance.AllEntities.Values.ForEach(delegate(EntityParent e)
			{
				if (targetID == e.ID)
				{
					e.IsIgnoreFormula = true;
				}
			});
			return true;
		}
		return false;
	}

	private bool youdi(string cmd)
	{
		if (cmd.StartsWith("youdi"))
		{
			string[] array = cmd.Replace("youdi", string.Empty).Split(new char[]
			{
				' '
			});
			long targetID = long.Parse(array[1]);
			EntityWorld.Instance.AllEntities.Values.ForEach(delegate(EntityParent e)
			{
				if (targetID == e.ID)
				{
					e.IsIgnoreFormula = false;
				}
			});
			return true;
		}
		return false;
	}

	private bool shizimao(string cmd)
	{
		if (cmd.StartsWith("shizimao"))
		{
			string[] array = cmd.Replace("shizimao", string.Empty).Split(new char[]
			{
				' '
			});
			long targetID = long.Parse(array[1]);
			EntityWorld.Instance.AllEntities.Values.ForEach(delegate(EntityParent e)
			{
				if (targetID == e.ID)
				{
					e.IsUnconspicuous = true;
				}
			});
			return true;
		}
		return false;
	}

	private bool lineon(string cmd)
	{
		if (cmd.StartsWith("lineon"))
		{
			SystemConfig.IsOpenEffectDrawLine = true;
			return true;
		}
		return false;
	}

	private bool lineoff(string cmd)
	{
		if (cmd.StartsWith("lineoff"))
		{
			SystemConfig.IsOpenEffectDrawLine = false;
			return true;
		}
		return false;
	}

	private bool clearcd(string cmd)
	{
		if (cmd.StartsWith("clearcd"))
		{
			SystemConfig.IsClearCD = true;
			return true;
		}
		return false;
	}

	private bool battlelog(string cmd)
	{
		if (cmd.StartsWith("battlelog"))
		{
			string text = cmd.Replace("battlelog", string.Empty).Trim(new char[]
			{
				' '
			});
			if (text.Equals("on"))
			{
				ClientApp.Instance.isShowFightLog = true;
			}
			else if (text.Equals("off"))
			{
				ClientApp.Instance.isShowFightLog = false;
			}
			return true;
		}
		return false;
	}

	private bool modelcreate(string cmd)
	{
		if (cmd.StartsWith("modelcreate"))
		{
			string[] array = cmd.Replace("modelcreate", string.Empty).Split(new char[]
			{
				' '
			});
			int model = int.Parse(array[1]);
			int num = int.Parse(array[2]);
			this.CreateModel(model, num);
			return true;
		}
		return false;
	}

	private bool modeladd(string cmd)
	{
		if (cmd.StartsWith("modeladd"))
		{
			string[] array = cmd.Replace("modeladd", string.Empty).Split(new char[]
			{
				' '
			});
			int model = int.Parse(array[1]);
			this.AddModel(model);
			return true;
		}
		return false;
	}

	private bool modelremove(string cmd)
	{
		if (cmd.StartsWith("modelremove"))
		{
			this.RemoveModel();
			return true;
		}
		return false;
	}

	private bool scenetest(string cmd)
	{
		if (cmd.StartsWith("scenetest"))
		{
			string[] array = cmd.Replace("scenetest", string.Empty).Split(new char[]
			{
				' '
			});
			int num = int.Parse(array[1]);
			this.SceneSwitchTestMode(num);
			return true;
		}
		return false;
	}

	private bool scenenormal(string cmd)
	{
		if (cmd.StartsWith("scenenormal"))
		{
			this.SceneSwitchNormalMode();
			return true;
		}
		return false;
	}

	private bool GuildBossModelTest(string cmd)
	{
		if (cmd.StartsWith("guildboss"))
		{
			string[] array = cmd.Replace("guildboss", string.Empty).Split(new char[]
			{
				' '
			});
			int bossID = int.Parse(array[1]);
			this.CreateGuildBossModel(bossID);
			return true;
		}
		return false;
	}

	private bool fog(string cmd)
	{
		if (cmd.StartsWith("fog"))
		{
			string[] array = cmd.Replace("fog", string.Empty).Trim().Split(new char[]
			{
				' '
			});
			if (array.Length >= 1)
			{
				string text = array[0].Trim();
				if (text.Equals("on"))
				{
					RenderSettings.set_fog(true);
				}
				else if (text.Equals("off"))
				{
					RenderSettings.set_fog(false);
				}
				else if (text.Equals("near"))
				{
					if (array.Length >= 2)
					{
						RenderSettings.set_fogStartDistance(float.Parse(array[1]));
					}
				}
				else if (text.Equals("far"))
				{
					if (array.Length >= 2)
					{
						RenderSettings.set_fogEndDistance(float.Parse(array[1]));
					}
				}
				else if (text.Equals("den"))
				{
					if (array.Length >= 2)
					{
						RenderSettings.set_fogDensity(float.Parse(array[1]));
					}
				}
				else if (text.Equals("test1"))
				{
					RenderSettings.set_fog(true);
					RenderSettings.set_fogMode(1);
					RenderSettings.set_fogStartDistance(1f);
					RenderSettings.set_fogEndDistance(10f);
					RenderSettings.set_fogColor(new Color32(255, 0, 0, 255));
				}
				else if (text.Equals("test2"))
				{
					RenderSettings.set_fog(true);
					RenderSettings.set_fogMode(2);
					RenderSettings.set_fogDensity(0.01f);
					RenderSettings.set_fogColor(new Color32(0, 255, 0, 255));
				}
			}
			return true;
		}
		return false;
	}

	private bool log(string cmd)
	{
		if (cmd.StartsWith("log"))
		{
			string text = cmd.Replace("log", string.Empty).Trim(new char[]
			{
				' '
			});
			if (text.Equals("on"))
			{
				this.ShowLog(true);
			}
			else if (text.Equals("off"))
			{
				this.ShowLog(false);
			}
			else if (text.Equals("console"))
			{
				RemoteLogSender.Instance.SendLogCachesToConsole();
			}
			else if (text.Equals("file"))
			{
				SystemConfig.IsLogToFile = true;
			}
			else if (text.Equals("demo"))
			{
				ClientApp.Instance.get_gameObject().AddMissingComponent<BuglyDemo>();
			}
			else
			{
				this.ShowLog(true);
				RemoteLogSender remoteLogSender = RemoteLogSender.Instance;
				if (remoteLogSender != null)
				{
					remoteLogSender.IP = text;
				}
			}
			return true;
		}
		return false;
	}

	private bool headinfo(string cmd)
	{
		if (cmd.StartsWith("headinfo"))
		{
			string text = cmd.Replace("headinfo", string.Empty).Trim(new char[]
			{
				' '
			});
			if (text.Equals("on"))
			{
				SystemConfig.IsHeadInfoOn = true;
			}
			else if (text.Equals("off"))
			{
				SystemConfig.IsHeadInfoOn = false;
			}
			return true;
		}
		return false;
	}

	private bool fps(string cmd)
	{
		if (cmd.StartsWith("fps"))
		{
			string text = cmd.Replace("fps", string.Empty).Trim(new char[]
			{
				' '
			});
			if (text.Equals("on"))
			{
				SystemConfig.IsTargetFrameRateOn = true;
				FPSManager.Instance.ResetFPS();
			}
			else if (text.Equals("off"))
			{
				SystemConfig.IsTargetFrameRateOn = false;
				FPSManager.Instance.ResetFPS();
			}
			else
			{
				int fPS = int.Parse(text);
				FPSManager.Instance.SetFPS(fPS);
			}
			return true;
		}
		return false;
	}

	private bool adfps(string cmd)
	{
		if (cmd.StartsWith("fps"))
		{
			string text = cmd.Replace("adfps", string.Empty).Trim(new char[]
			{
				' '
			});
			if (text.Equals("on"))
			{
				UIUtils.AdvancedFPS.get_gameObject().SetActive(true);
			}
			else if (text.Equals("off"))
			{
				UIUtils.AdvancedFPS.get_gameObject().SetActive(false);
			}
			return true;
		}
		return false;
	}

	private bool lod(string cmd)
	{
		if (cmd.StartsWith("lod"))
		{
			switch (int.Parse(cmd.Replace("lod", string.Empty).Trim(new char[]
			{
				' '
			})))
			{
			case 1:
				GameLevelManager.SetGameQuality(200, false);
				break;
			case 2:
				GameLevelManager.SetGameQuality(250, false);
				break;
			case 3:
				GameLevelManager.SetGameQuality(300, false);
				break;
			}
			return true;
		}
		return false;
	}

	private bool pp(string cmd)
	{
		if (cmd.StartsWith("pp"))
		{
			string text = cmd.Replace("pp", string.Empty).Trim(new char[]
			{
				' '
			});
			if (text.Equals("on"))
			{
				SystemConfig.IsPostProcessOn = true;
			}
			else if (text.Equals("off"))
			{
				SystemConfig.IsPostProcessOn = false;
			}
			return true;
		}
		return false;
	}

	private bool fx(string cmd)
	{
		if (cmd.StartsWith("fx"))
		{
			string text = cmd.Replace("fx", string.Empty).Trim(new char[]
			{
				' '
			});
			if (text.Equals("on"))
			{
				SystemConfig.IsFXOn = true;
			}
			else if (text.Equals("off"))
			{
				SystemConfig.IsFXOn = false;
			}
			return true;
		}
		return false;
	}

	private bool ref_control(string cmd)
	{
		if (cmd.StartsWith("ref"))
		{
			string text = cmd.Replace("ref", string.Empty).Trim(new char[]
			{
				' '
			});
			if (text.Equals("on"))
			{
				SystemConfig.IsRefenenceControlOn = true;
			}
			else if (text.Equals("off"))
			{
				SystemConfig.IsRefenenceControlOn = false;
			}
			return true;
		}
		return false;
	}

	private bool resdebug(string cmd)
	{
		if (cmd.StartsWith("resdebug"))
		{
			string text = cmd.Replace("resdebug", string.Empty).Trim(new char[]
			{
				' '
			});
			if (!text.Equals("on"))
			{
				if (text.Equals("off"))
				{
				}
			}
			return true;
		}
		return false;
	}

	private bool close(string cmd)
	{
		if (cmd.StartsWith("close"))
		{
			this.gmOpen = false;
			return true;
		}
		return false;
	}

	private bool fxclose(string cmd)
	{
		if (cmd.StartsWith("fxclose"))
		{
			this.CloseMonstersFX();
			return true;
		}
		return false;
	}

	private bool peopleshow(string cmd)
	{
		if (cmd.StartsWith("peopleshow"))
		{
			ActorVisibleManager.Instance.Print();
			return true;
		}
		return false;
	}

	private bool debug(string cmd)
	{
		if (cmd.StartsWith("debug"))
		{
			string text = cmd.Replace("debug", string.Empty).Trim(new char[]
			{
				' '
			});
			if (text.Equals("on"))
			{
				ClientGMManager.ShowDebugInfo(true);
			}
			else if (text.Equals("off"))
			{
				ClientGMManager.ShowDebugInfo(false);
			}
			return true;
		}
		return false;
	}

	private bool ping(string cmd)
	{
		if (cmd.StartsWith("ping"))
		{
			string text = cmd.Replace("ping", string.Empty).Trim(new char[]
			{
				' '
			});
			if (text.Equals("on"))
			{
				ClientGMManager.ShowPing(true);
			}
			else if (text.Equals("off"))
			{
				ClientGMManager.ShowPing(false);
			}
			return true;
		}
		return false;
	}

	private bool release(string cmd)
	{
		if (cmd.StartsWith("release"))
		{
			this.ReleaseSetting();
			return true;
		}
		return false;
	}

	private bool playfx(string cmd)
	{
		if (cmd.StartsWith("playfxout"))
		{
			string text = cmd.Replace("playfxout", string.Empty).Trim(new char[]
			{
				' '
			});
			ClientGMManager.fxout_id = int.Parse(text);
			return true;
		}
		if (cmd.StartsWith("playfx"))
		{
			Shader.set_globalMaximumLOD(300);
			string text2 = cmd.Replace("playfx", string.Empty).Trim(new char[]
			{
				' '
			});
			FXManager.Instance.PlayFXOfDisplay(int.Parse(text2), EntityWorld.Instance.ActSelf.FixTransform, Vector3.get_zero(), Quaternion.get_identity(), 1f, 1f, 0, false, null, null);
			return true;
		}
		return false;
	}

	private bool guide(string cmd)
	{
		if (cmd.StartsWith("guidetest"))
		{
			ClientApp.Instance.get_gameObject().AddMissingComponent<GuideTest>();
			return true;
		}
		return false;
	}

	private bool guidelog(string cmd, ref StringBuilder result)
	{
		if (cmd.StartsWith("guidelog"))
		{
			result.Append(GuideManager.Instance.PrintMessage());
			return true;
		}
		return false;
	}

	private bool uilocklog(string cmd, ref StringBuilder result)
	{
		if (cmd.StartsWith("uilocklog"))
		{
			result.Append(UIStateSystem.PrintMessage());
			return true;
		}
		return false;
	}

	private bool autotask(string cmd)
	{
		if (cmd.StartsWith("guaji"))
		{
			MainTaskManager.Instance.IsAllAuto = !MainTaskManager.Instance.IsAllAuto;
			string text = cmd.Replace("guaji", string.Empty);
			if (!string.IsNullOrEmpty(text))
			{
				if (text == "fast")
				{
					MainTaskManager.Instance.IsAutoFast = !MainTaskManager.Instance.IsAutoFast;
				}
				else
				{
					Time.set_timeScale((float)int.Parse(text));
					AppConst.GlobalTimeScale = Time.get_timeScale();
					ChatManager.Instance.SendGMCommand(0, "#passCheck");
				}
			}
			return true;
		}
		return false;
	}

	private bool unlock_all_system(string cmd)
	{
		if (cmd.StartsWith("unlock"))
		{
			this.IsAlwaysOpen = true;
		}
		return false;
	}

	private bool relock_system(string cmd)
	{
		if (cmd.StartsWith("lock"))
		{
			this.IsAlwaysOpen = false;
		}
		return false;
	}

	private bool netshut(string cmd)
	{
		if (cmd.StartsWith("netshut"))
		{
			string[] array = cmd.Replace("netshut", string.Empty).Trim().Split(new char[]
			{
				' '
			});
			if (array.Length >= 1)
			{
				string text = array[0].Trim();
				if (text.StartsWith("a"))
				{
					NetworkManager.Instance.ShutDownAndReconnectAllServer();
				}
				else if (text.StartsWith("c"))
				{
					NetworkService.Instance.GetConnection(ServerType.Chat).ConnectState = NetworkConnectState.NotConnected;
				}
				else if (text.StartsWith("d"))
				{
					NetworkService.Instance.GetConnection(ServerType.Data).ConnectState = NetworkConnectState.NotConnected;
				}
			}
			return true;
		}
		return false;
	}

	private bool netstate(string cmd, ref StringBuilder result)
	{
		if (cmd.StartsWith("netstate"))
		{
			string[] array = cmd.Replace("netstate", string.Empty).Trim().Split(new char[]
			{
				' '
			});
			if (array.Length >= 1)
			{
				string text = array[0].Trim();
				if (text.StartsWith("a"))
				{
					result.Append(NetworkService.Instance.ShowAllConnectionState());
					result.Append(ReconnectManager.Instance.ToString());
				}
				else if (text.StartsWith("b"))
				{
					result.Append(NetworkService.Instance.ShowConnectionAckCache(ServerType.Data));
					result.Append(NetworkConnection.GetAllAckCacheString());
				}
				else if (text.StartsWith("c"))
				{
					result.Append(NetworkService.Instance.ShowConnectionState(ServerType.Chat));
					result.Append("\n");
					result.Append(LoginManager.Instance.lastLoginChatTime.ToString());
				}
				else if (text.StartsWith("d"))
				{
					result.Append(NetworkService.Instance.ShowConnectionState(ServerType.Data));
				}
				else if (text.StartsWith("r"))
				{
					result.Append(ReconnectManager.Instance.ToString());
				}
				else if (text.StartsWith("s"))
				{
					result.Append(NetworkService.Instance.ShowAllConnectionState());
				}
			}
			return true;
		}
		return false;
	}

	private bool pingopen(string cmd)
	{
		if (cmd.StartsWith("pingopen"))
		{
			SystemConfig.IsDebugPing = true;
			return true;
		}
		return false;
	}

	private bool pingclose(string cmd)
	{
		if (cmd.StartsWith("pingclose"))
		{
			SystemConfig.IsDebugPing = false;
			return true;
		}
		return false;
	}

	private bool packetInterval(string cmd)
	{
		if (cmd.StartsWith("packetinterval"))
		{
			string[] array = cmd.Replace("packetinterval", string.Empty).Split(new char[]
			{
				' '
			});
			float handlePacketIntervel = float.Parse(array[1]);
			NetworkManager.Instance.HandlePacketIntervel = handlePacketIntervel;
			return true;
		}
		return false;
	}

	private bool netcheck(string cmd)
	{
		if (cmd.StartsWith("netcheck"))
		{
			string[] array = cmd.Replace("netcheck", string.Empty).Trim().Split(new char[]
			{
				' '
			});
			if (array.Length >= 1)
			{
				string text = array[0].Trim();
				if (text.StartsWith("00on"))
				{
					this.netSwitch00 = true;
				}
				else if (text.StartsWith("00off"))
				{
					this.netSwitch00 = false;
				}
				else if (text.StartsWith("01on"))
				{
					ClientGMManager.netSwitch01 = true;
				}
				else if (text.StartsWith("01off"))
				{
					ClientGMManager.netSwitch01 = false;
				}
			}
			return true;
		}
		return false;
	}

	private bool settle(string cmd, ref StringBuilder result)
	{
		if (cmd.StartsWith("settle"))
		{
			result.AppendLine("**************** SendSettleReq Time ****************");
			for (int i = 0; i < this.SendSettleReq.get_Count(); i++)
			{
				result.AppendLine(this.SendSettleReq.get_Item(i).get_Key() + " " + this.SendSettleReq.get_Item(i).get_Value().ToString());
			}
			result.AppendLine("**************** NetSendSettleReq Time ****************");
			for (int j = 0; j < this.NetSendSettleReq.get_Count(); j++)
			{
				result.AppendLine(this.NetSendSettleReq.get_Item(j).ToString());
			}
			result.AppendLine("**************** NetSendSettleReq Result ****************");
			for (int k = 0; k < this.NetSendSettleReqCode.get_Count(); k++)
			{
				result.AppendLine(this.NetSendSettleReqCode.get_Item(k).get_Key().ToString() + " " + this.NetSendSettleReqCode.get_Item(k).get_Value().ToString());
			}
		}
		return false;
	}

	private bool allpos(string cmd, ref StringBuilder result)
	{
		if (cmd.StartsWith("allpos"))
		{
			result.Append("CurrentPos: ");
			result.Append(PosDirUtility.ToDetailString(EntityWorld.Instance.EntSelf.Actor.FixTransform.get_position()));
			result.Append("\n");
			result.Append(EntityWorld.Instance.ShowAllPosState());
			return true;
		}
		return false;
	}

	private bool timecheck(string cmd)
	{
		if (cmd.StartsWith("timecheck"))
		{
			string[] array = cmd.Replace("timecheck", string.Empty).Trim().Split(new char[]
			{
				' '
			});
			if (array.Length >= 1)
			{
				string text = array[0].Trim();
				if (text.StartsWith("00on"))
				{
					this.timeSwitch00 = true;
				}
				else if (text.StartsWith("00off"))
				{
					this.timeSwitch00 = false;
				}
			}
			return true;
		}
		return false;
	}

	private bool battlecheck(string cmd)
	{
		if (cmd.StartsWith("battlecheck"))
		{
			string[] array = cmd.Replace("battlecheck", string.Empty).Trim().Split(new char[]
			{
				' '
			});
			if (array.Length >= 1)
			{
				string text = array[0].Trim();
				if (text.StartsWith("00on"))
				{
					this.battleSwitch00 = true;
				}
				else if (text.StartsWith("00off"))
				{
					this.battleSwitch00 = false;
				}
				else if (text.StartsWith("01on"))
				{
					GlobalBattleNetwork.Instance.CurCowardCheckType = GlobalBattleNetwork.CowardCheckType.Count;
				}
				else if (text.StartsWith("01off"))
				{
					GlobalBattleNetwork.Instance.CurCowardCheckType = GlobalBattleNetwork.CowardCheckType.Time;
				}
				else if (text.StartsWith("logon"))
				{
					this.battleLog = true;
				}
				else if (text.StartsWith("logoff"))
				{
					this.battleLog = false;
				}
			}
			return true;
		}
		return false;
	}

	private bool changesdktype(string cmd)
	{
		if (cmd.StartsWith("sdk"))
		{
			string text = cmd.Replace("sdk", string.Empty);
			if (!string.IsNullOrEmpty(text) && int.TryParse(text, ref SDKManager.TestSDKType))
			{
				SDKManager.IsCanChangeSDKType = true;
				LoginManager.Instance.GetServerListFile(LoginManager.AddressType.DOMAIN);
			}
			else
			{
				SDKManager.IsCanChangeSDKType = false;
			}
			return true;
		}
		return false;
	}

	private bool guide_switch(string cmd)
	{
		if (cmd.StartsWith("guide"))
		{
			string text = cmd.Replace("guide", string.Empty).Trim(new char[]
			{
				' '
			});
			if (text.Equals("on"))
			{
				SystemConfig.IsGuideSystemOn = true;
				PlayerPrefsExt.SetBool("IsGuideOnName", SystemConfig.IsGuideSystemOn);
				return true;
			}
			if (text.Equals("off"))
			{
				SystemConfig.IsGuideSystemOn = false;
				PlayerPrefsExt.SetBool("IsGuideOnName", SystemConfig.IsGuideSystemOn);
				return true;
			}
		}
		return false;
	}

	private void ShowLog(bool isOn)
	{
		Debuger.EnableLog = isOn;
		Debug.get_logger().set_logEnabled(isOn);
	}

	public static void ShowDebugInfo(bool isOn)
	{
		SystemConfig.IsDebugInfoOn = isOn;
		if (isOn)
		{
			ClientApp.Instance.get_gameObject().AddMissingComponent<DebugInfoUIViewManager>();
		}
		else
		{
			DebugInfoUIViewManager component = ClientApp.Instance.get_gameObject().GetComponent<DebugInfoUIViewManager>();
			if (component != null)
			{
				Object.Destroy(component);
			}
		}
	}

	public static void ShowPing(bool isOn)
	{
		SystemConfig.IsDebugPing = isOn;
		if (isOn)
		{
			ClientApp.Instance.get_gameObject().AddMissingComponent<PingDebug>();
		}
		else
		{
			PingDebug component = ClientApp.Instance.get_gameObject().GetComponent<PingDebug>();
			if (component != null)
			{
				Object.Destroy(component);
			}
		}
	}

	private void AllEntityDoAction(Action<EntityParent> action)
	{
		EntityWorld.Instance.GetEntities<EntitySelf>().Values.ForEach(action);
		EntityWorld.Instance.GetEntities<EntityPet>().Values.ForEach(action);
		EntityWorld.Instance.GetEntities<EntityMonster>().Values.ForEach(action);
		EntityWorld.Instance.GetEntities<EntityPlayer>().Values.ForEach(action);
		EntityWorld.Instance.GetEntities<EntityCityPlayer>().Values.ForEach(action);
	}

	private bool EntityDoAction(long id, Action<EntityParent> action)
	{
		return this.EntityDoAction<EntitySelf>(id, action) || this.EntityDoAction<EntityPet>(id, action) || this.EntityDoAction<EntityPlayer>(id, action) || this.EntityDoAction<EntityMonster>(id, action) || this.EntityDoAction<EntityCityPlayer>(id, action);
	}

	private bool EntityDoAction<U>(long id, Action<EntityParent, U> action, U arg1)
	{
		return this.EntityDoAction<EntitySelf, U>(id, action, arg1) || this.EntityDoAction<EntityPet, U>(id, action, arg1) || this.EntityDoAction<EntityPlayer, U>(id, action, arg1) || this.EntityDoAction<EntityMonster, U>(id, action, arg1) || this.EntityDoAction<EntityCityPlayer, U>(id, action, arg1);
	}

	private bool EntityDoAction<T>(long id, Action<EntityParent> action) where T : EntityParent
	{
		if (EntityWorld.Instance.GetEntities<T>().ContainsKey(id))
		{
			action.Invoke(EntityWorld.Instance.GetEntities<T>()[id]);
			return true;
		}
		return false;
	}

	private bool EntityDoAction<T, U>(long id, Action<EntityParent, U> action, U arg1) where T : EntityParent
	{
		if (EntityWorld.Instance.GetEntities<T>().ContainsKey(id))
		{
			action.Invoke(EntityWorld.Instance.GetEntities<T>()[id], arg1);
			return true;
		}
		return false;
	}

	public static void Stopallai()
	{
		EntityWorld.Instance.GetEntities<EntityPlayer>().Values.ForEach(delegate(EntityParent e)
		{
			e.GetAIManager().Deactive();
		});
		EntityWorld.Instance.GetEntities<EntityMonster>().Values.ForEach(delegate(EntityParent e)
		{
			e.GetAIManager().Deactive();
		});
		EntityWorld.Instance.GetEntities<EntityPet>().Values.ForEach(delegate(EntityParent e)
		{
			e.GetAIManager().Deactive();
		});
	}

	private void CreateModel(int model, int num)
	{
		for (int i = 0; i < this.testModels.get_Count(); i++)
		{
			this.testModels.get_Item(i).Destroy();
		}
		this.testModels.Clear();
		for (int j = 0; j < num; j++)
		{
			this.AddModel(model);
		}
	}

	private void AddModel(int model)
	{
		Vector3 vector = new Vector3((float)Random.Range(-5, 5), 0f, (float)Random.Range(-5, 5));
		ActorModel actorModel = ModelPool.Instance.Get(model);
		actorModel.get_transform().set_position(EntityWorld.Instance.EntSelf.Actor.FixTransform.get_position() + vector);
		actorModel.InitLocalPosition = new Vector3(actorModel.get_transform().get_localPosition().x, actorModel.get_transform().get_localPosition().y, actorModel.get_transform().get_localPosition().z);
		actorModel.get_gameObject().SetActive(true);
		this.testModels.Add(actorModel);
	}

	private void RemoveModel()
	{
		if (this.testModels.get_Count() > 0)
		{
			ActorModel script = this.testModels.get_Item(0);
			this.testModels.RemoveAt(0);
			script.Destroy();
		}
	}

	private void SceneSwitchTestMode(int num)
	{
	}

	private void SceneSwitchNormalMode()
	{
	}

	private void SummonMonster()
	{
	}

	private void CloseMonstersFX()
	{
		List<EntityParent> values = EntityWorld.Instance.GetEntities<EntityMonster>().Values;
		for (int i = 0; i < values.get_Count(); i++)
		{
			if (values.get_Item(i).Actor && values.get_Item(i).Actor.FixTransform != null)
			{
				ActorFX[] componentsInChildren = values.get_Item(i).Actor.FixTransform.GetComponentsInChildren<ActorFX>();
				for (int j = 0; j < componentsInChildren.Length; j++)
				{
					componentsInChildren[j].get_gameObject().SetActive(false);
				}
			}
		}
	}

	private void ReleaseSetting()
	{
		ClientGMManager.ShowDebugInfo(false);
		ClientGMManager.ShowPing(false);
		this.ShowLog(false);
	}

	private bool xgpush(string cmd)
	{
		if (cmd.StartsWith("pushadd1"))
		{
			PushNotificationManager.SetPushTag("darkTrain");
			return true;
		}
		if (cmd.StartsWith("pushadd2"))
		{
			PushNotificationManager.SetPushTag("gangFight");
			return true;
		}
		if (cmd.StartsWith("pushdel1"))
		{
			PushNotificationManager.DeletePushTag("darkTrain");
			return true;
		}
		if (cmd.StartsWith("pushdel2"))
		{
			PushNotificationManager.DeletePushTag("gangFight");
			return true;
		}
		if (cmd.StartsWith("pushl"))
		{
			this.PushNotification(100, 10, NotificationRepeatInterval.None);
			this.PushNotification(200, 20, NotificationRepeatInterval.None);
			this.PushNotification(300, 30, NotificationRepeatInterval.Day);
			this.PushNotification(400, 40, NotificationRepeatInterval.Week);
			return true;
		}
		if (cmd.StartsWith("cleanlocal1"))
		{
			Debug.LogError("[推送]清理");
			LocalForAndroidManager.CleanNotification();
			return true;
		}
		if (cmd.StartsWith("cleanlocal2"))
		{
			Debug.LogError("[推送]清理");
			LocalForAndroidManager.CancelNotification(100);
			LocalForAndroidManager.CancelNotification(200);
			LocalForAndroidManager.CancelNotification(300);
			LocalForAndroidManager.CancelNotification(400);
			return true;
		}
		return false;
	}

	private void PushNotification(int push_id, int delay_second, NotificationRepeatInterval npi)
	{
		TuiSongTongZhi tuiSongTongZhi = DataReader<TuiSongTongZhi>.Get(8);
		if (tuiSongTongZhi != null)
		{
			Debug.LogError("[离线召回] time = " + delay_second);
			NativeCallManager.NotificationMessage(push_id, GameDataUtils.GetChineseContent(tuiSongTongZhi.detail, false), DateTime.get_Now().AddSeconds((double)delay_second), npi);
		}
	}

	private void CreateGuildBossModel(int bossID)
	{
		GuildBossManager.Instance.GuildBossID = bossID;
		if (bossID <= 0)
		{
			GuildBossManager.Instance.ShowGuildBossTest = false;
		}
		else
		{
			GuildBossManager.Instance.ShowGuildBossTest = true;
		}
	}

	private bool loaddata(string cmd)
	{
		if (cmd.StartsWith("loaddata"))
		{
			SystemConfig.LogSetting(true);
			Debug.Log("start GameData.DiaoLuoZu, time = " + Time.get_realtimeSinceStartup());
			DataReader<DiaoLuoZu>.Init();
			Debug.Log("start GameData.DiaoLuoZu, count = " + DataReader<DiaoLuoZu>.DataList.get_Count());
			Debug.Log("start GameData.MonsterAttr, time = " + Time.get_realtimeSinceStartup());
			DataReader<MonsterAttr>.Init();
			Debug.Log("start GameData.MonsterAttr, count = " + DataReader<MonsterAttr>.DataList.get_Count());
			Debug.Log("start GameData.DiaoLuoGuiZe, time = " + Time.get_realtimeSinceStartup());
			DataReader<DiaoLuoGuiZe>.Init();
			Debug.Log("start GameData.DiaoLuoGuiZe, count = " + DataReader<DiaoLuoGuiZe>.DataList.get_Count());
			Debug.Log("end, time = " + Time.get_realtimeSinceStartup());
			return true;
		}
		return false;
	}

	private bool playspine(string cmd)
	{
		if (cmd.StartsWith("spineout"))
		{
			string text = cmd.Replace("spineout", string.Empty).Trim(new char[]
			{
				' '
			});
			ClientGMManager.spineout_ids = text;
			return true;
		}
		if (cmd.StartsWith("spine"))
		{
			string text2 = cmd.Replace("spine", string.Empty).Trim(new char[]
			{
				' '
			});
			int templateId = int.Parse(text2);
			FXSpineManager.Instance.DeleteSpine(this.spine_uuid, true);
			this.spine_uuid = FXSpineManager.Instance.PlaySpine(templateId, UINodesManager.TopUIRoot, string.Empty, 14000, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			return true;
		}
		if (cmd.StartsWith("01spine"))
		{
			FXSpineManager.Instance.DeleteSpine(this.spine_uuid, true);
			this.spine_uuid = FXSpineManager.Instance.ReplaySpine(this.spine_uuid, 3903, UINodesManager.NormalUIRoot, "TownUI", 1998, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			return true;
		}
		if (cmd.StartsWith("02spine"))
		{
			FXSpineManager.Instance.DeleteSpine(this.spine_uuid, true);
			this.spine_uuid = FXSpineManager.Instance.ReplaySpine(this.spine_uuid, 3903, UINodesManager.NormalUIRoot, "TownUI", 1996, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			return true;
		}
		if (cmd.StartsWith("03spine"))
		{
			FXSpineManager.Instance.DeleteSpine(this.spine_uuid, true);
			this.spine_uuid = FXSpineManager.Instance.ReplaySpine(this.spine_uuid, 3903, UINodesManager.MiddleUIRoot, "TownUI", 2900, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			return true;
		}
		return false;
	}

	private bool chatui(string cmd)
	{
		if (cmd.StartsWith("chatui"))
		{
			ChatManager.Instance.OpenChatUI(1);
			return true;
		}
		return false;
	}

	private bool voice_play(string cmd)
	{
		if (cmd.StartsWith("voiceplay"))
		{
			string[] array = cmd.Replace("voiceplay", string.Empty).Split(new char[]
			{
				' '
			});
			string name = array[1];
			VoiceSDKManager.Instance.SpeechPlay(name);
			return true;
		}
		return false;
	}

	private bool waitui1(string cmd)
	{
		if (cmd.StartsWith("waitui1"))
		{
			WaitUI.TestOpenUI01();
			return true;
		}
		return false;
	}

	private bool waitui2(string cmd)
	{
		if (cmd.StartsWith("waitui2"))
		{
			WaitUI.TestOpenUI02();
			return true;
		}
		return false;
	}

	private bool unloadui(string cmd)
	{
		if (cmd.StartsWith("unloadui"))
		{
			UIManagerControl.Instance.UnLoadUIPrefab("TownUI");
			return true;
		}
		return false;
	}

	private bool wifi(string cmd)
	{
		if (cmd.StartsWith("wifi1"))
		{
			Debug.Log("IsWifiOn: " + NativeCallManager.IsWifiOn());
			return true;
		}
		if (cmd.StartsWith("wifi2"))
		{
			Debug.Log("Rssi: " + NativeCallManager.GetNetworkRssi());
			return true;
		}
		return false;
	}

	private bool isTriggerException(string cmd)
	{
		if (cmd.Equals("nullex"))
		{
			Object.Instantiate(null);
			return true;
		}
		return false;
	}

	private bool isTriggerCrash(string cmd)
	{
		if (cmd.Equals("crash"))
		{
			XUtility.GetConfigTxt("null", ".txt");
			return true;
		}
		return false;
	}

	private bool check_common_rendertexture(string cmd)
	{
		if (cmd.StartsWith("checkrt"))
		{
			if (CamerasMgr.Camera2RTCommon.get_targetTexture() == null)
			{
				Debug.LogError("CamerasMgr.Camera2RTCommon.targetTexture is null");
			}
			else
			{
				Debug.LogError("CamerasMgr.Camera2RTCommon.targetTexture is normal");
			}
			if (RTManager.Instance.RTCommon == null)
			{
				Debug.LogError("RTManager.Instance.RTCommon is null");
			}
			else
			{
				Debug.LogError("RTManager.Instance.RTCommon is normal");
			}
			return true;
		}
		return false;
	}

	private bool set_resolution(string cmd)
	{
		if (cmd.StartsWith("resolution01"))
		{
			string text = cmd.Replace("resolution", string.Empty).Trim();
			if (!string.IsNullOrEmpty(text))
			{
				UIUtils.SetHardwareResolution(int.Parse(text));
			}
			return true;
		}
		if (cmd.StartsWith("resolution02"))
		{
			Debug.LogError("Screen.currentResolution.height = " + Screen.get_currentResolution().get_height());
			Debug.LogError("Screen.currentResolution.width = " + Screen.get_currentResolution().get_width());
			Debug.LogError("Screen.currentResolution.refreshRate = " + Screen.get_currentResolution().get_refreshRate());
			return true;
		}
		if (cmd.StartsWith("resolution03"))
		{
			EventDispatcher.Broadcast("ControlStick.HardwareResolutionChange");
			return true;
		}
		return false;
	}

	private bool load_asset(string cmd)
	{
		if (cmd.StartsWith("load01"))
		{
			string resName = cmd.Replace("load01", string.Empty).Trim();
			AssetLoader.LoadAsset(resName, typeof(Object), delegate(Object obj)
			{
				if (obj != null)
				{
					Debug.LogError("load success, obj = " + obj.get_name());
				}
				Object.Instantiate(obj);
				Debug.LogError("Instantiate success, obj= " + obj.get_name());
			});
			return true;
		}
		if (cmd.StartsWith("load02"))
		{
			string resName2 = cmd.Replace("load02", string.Empty).Trim();
			AssetLoader.LoadAsset(resName2, typeof(Object), delegate(Object obj)
			{
				if (obj != null)
				{
					Debug.LogError("load success, obj = " + obj.get_name());
				}
			});
			return true;
		}
		return false;
	}

	private bool release_asset(string cmd)
	{
		if (cmd.StartsWith("noref"))
		{
			AssetManager.ReleaseNoRef(true);
			return true;
		}
		return false;
	}

	private bool GC(string cmd)
	{
		if (cmd.StartsWith("gc"))
		{
			Resources.UnloadUnusedAssets();
			System.GC.Collect();
			return true;
		}
		return false;
	}

	private bool ApkMd5(string cmd)
	{
		if (cmd.StartsWith("apkmd5"))
		{
			Debug.LogFormat("apkmd5", new object[0]);
			if (Application.get_platform() == 11)
			{
				Debug.LogFormat("apk md5: {0}", new object[]
				{
					MD5Util.EncryptFile(Application.get_dataPath())
				});
				byte[] androidSignatures = NativeCallManager.GetAndroidSignatures();
				Debug.LogFormat("signatures md5: {0}", new object[]
				{
					MD5Util.Encrypt(androidSignatures)
				});
			}
			return true;
		}
		return false;
	}

	private bool PkgName(string cmd)
	{
		if (cmd.StartsWith("pkgname"))
		{
			Debug.LogFormat("pkgname", new object[0]);
			if (Application.get_platform() == 11)
			{
				Debug.LogFormat("apk package name: {0}", new object[]
				{
					NativeCallManager.GetAndroidPackageName()
				});
			}
			return true;
		}
		return false;
	}

	private bool UseLan(string cmd)
	{
		if (cmd.StartsWith("uselan"))
		{
			Debug.Log("uselan");
			string useLanFilePath = AppConst.UseLanFilePath;
			if (!File.Exists(useLanFilePath))
			{
				File.Create(useLanFilePath);
			}
			return true;
		}
		return false;
	}

	private bool PreloadEnd(string cmd)
	{
		if (cmd.StartsWith("preloadend"))
		{
			if (LoginLoadingRes.Instance != null)
			{
				Debug.LogFormat("preloadend: {0}    allEndFlag: {1}", new object[]
				{
					LoginLoadingRes.Instance.LoadEndFlag,
					LoginLoadingRes.Instance.AllEndFlag
				});
			}
			else
			{
				Debug.LogError("LoginLoadingRes.Instance == null");
			}
			return true;
		}
		return false;
	}

	private bool bgm(string cmd)
	{
		if (cmd.StartsWith("bgm"))
		{
			string text = cmd.Replace("bgm", string.Empty).Trim();
			if (!string.IsNullOrEmpty(text))
			{
				SoundManager.Instance.PlayBGMByID(int.Parse(text));
			}
			return true;
		}
		return false;
	}

	private bool UpdateCanvas(string cmd)
	{
		if (cmd.StartsWith("canvasup"))
		{
			Canvas.ForceUpdateCanvases();
			return true;
		}
		return false;
	}

	private bool LoadSpeed(string cmd)
	{
		if (cmd.StartsWith("loadspeed"))
		{
			int maxConcurrencyNum = int.Parse(cmd.Split(new char[]
			{
				' '
			})[1]);
			AssetBundleLoader.Instance.maxConcurrencyNum = maxConcurrencyNum;
			return true;
		}
		return false;
	}

	private bool VersionCode(string cmd)
	{
		if (cmd.StartsWith("versioncode"))
		{
			Debug.LogFormat("versionCode :{0}", new object[]
			{
				NativeCallManager.GetVersionCode()
			});
			return true;
		}
		return false;
	}

	private bool Restart(string cmd)
	{
		if (cmd.StartsWith("restart"))
		{
			Debug.Log("restart");
			NativeCallManager.Restart();
			return true;
		}
		return false;
	}

	private bool UpdatePatch()
	{
		if (this.cmd.StartsWith("updatepatch"))
		{
			Debug.Log("updatepatch");
			GameManager.Instance.CurrentUpdateManager.Resume(true, null);
			return true;
		}
		return false;
	}

	private bool refresh_materials(string cmd)
	{
		if (cmd.StartsWith("refreshmat"))
		{
			this.RefreshMaterial();
			return true;
		}
		return false;
	}

	private bool OpenKF(string cmd)
	{
		if (cmd.StartsWith("openkf"))
		{
			SDKManager.Instance.OpenKF();
			return true;
		}
		return false;
	}

	private bool CloseKF(string cmd)
	{
		if (cmd.StartsWith("closekf"))
		{
			SDKManager.Instance.CloseKF();
			return true;
		}
		return false;
	}

	private void RefreshMaterial()
	{
		Material[] array = Object.FindObjectsOfType<Material>();
		Debug.LogError("RefreshMaterial, count = " + array.Length);
		for (int i = 0; i < array.Length; i++)
		{
			Material material = array[i];
			material.set_shader(ShaderManager.Instance.FindShader(material.get_shader().get_name()));
		}
	}
}

using System;
using Assets.src.g;

namespace Assets.src.f
{
	internal class Controller2
	{
		public static void readMessage(Message msg)
		{
			try
			{
				switch (msg.command)
				{
				case sbyte.MinValue:
					readInfoEffChar(msg);
					break;
				case sbyte.MaxValue:
					readInfoRada(msg);
					break;
				case 114:
					try
					{
						msg.reader().readUTF();
						mSystem.curINAPP = msg.reader().readByte();
						mSystem.maxINAPP = msg.reader().readByte();
						break;
					}
					catch (Exception)
					{
						break;
					}
				case 113:
				{
					int loop = 0;
					int layer = 0;
					int id = 0;
					short x = 0;
					short y = 0;
					short loopCount = -1;
					try
					{
						loop = msg.reader().readByte();
						layer = msg.reader().readByte();
						id = msg.reader().readShort();
						x = msg.reader().readShort();
						y = msg.reader().readShort();
						loopCount = msg.reader().readShort();
					}
					catch (Exception)
					{
					}
					EffecMn.addEff(new Effect(id, x, y, layer, loop, loopCount));
					break;
				}
				case 48:
					ServerListScreen.SetIpSelect(msg.reader().readByte(), false);
					GameCanvas.instance.doResetToLoginScr(GameCanvas.serverScreen);
					Session_ME.gI().close();
					GameCanvas.endDlg();
					ServerListScreen.waitToLogin = true;
					break;
				case 31:
				{
					int num33 = msg.reader().readInt();
					if (msg.reader().readByte() == 1)
					{
						short smallID = msg.reader().readShort();
						sbyte b19 = -1;
						int[] array9 = null;
						short wimg = 0;
						short himg = 0;
						try
						{
							b19 = msg.reader().readByte();
							if (b19 > 0)
							{
								sbyte b20 = msg.reader().readByte();
								array9 = new int[b20];
								for (int num34 = 0; num34 < b20; num34++)
								{
									array9[num34] = msg.reader().readByte();
								}
								wimg = msg.reader().readShort();
								himg = msg.reader().readShort();
							}
						}
						catch (Exception)
						{
						}
						if (num33 == Char.myCharz().charID)
						{
							Char.myCharz().petFollow = new PetFollow();
							Char.myCharz().petFollow.smallID = smallID;
							if (b19 > 0)
							{
								Char.myCharz().petFollow.SetImg(b19, array9, wimg, himg);
							}
							break;
						}
						Char obj3 = GameScr.findCharInMap(num33);
						obj3.petFollow = new PetFollow();
						obj3.petFollow.smallID = smallID;
						if (b19 > 0)
						{
							obj3.petFollow.SetImg(b19, array9, wimg, himg);
						}
					}
					else if (num33 == Char.myCharz().charID)
					{
						Char.myCharz().petFollow.remove();
						Char.myCharz().petFollow = null;
					}
					else
					{
						Char obj4 = GameScr.findCharInMap(num33);
						obj4.petFollow.remove();
						obj4.petFollow = null;
					}
					break;
				}
				case -89:
					GameCanvas.open3Hour = msg.reader().readByte() == 1;
					break;
				case 42:
				{
					GameCanvas.endDlg();
					LoginScr.isContinueToLogin = false;
					Char.isLoadingMap = false;
					sbyte haveName = msg.reader().readByte();
					if (GameCanvas.registerScr == null)
					{
						GameCanvas.registerScr = new RegisterScreen(haveName);
					}
					GameCanvas.registerScr.switchToMe();
					break;
				}
				case 52:
				{
					sbyte num37 = msg.reader().readByte();
					if (num37 == 1)
					{
						int num38 = msg.reader().readInt();
						if (num38 == Char.myCharz().charID)
						{
							Char.myCharz().setMabuHold(true);
							Char.myCharz().cx = msg.reader().readShort();
							Char.myCharz().cy = msg.reader().readShort();
						}
						else
						{
							Char obj5 = GameScr.findCharInMap(num38);
							if (obj5 != null)
							{
								obj5.setMabuHold(true);
								obj5.cx = msg.reader().readShort();
								obj5.cy = msg.reader().readShort();
							}
						}
					}
					if (num37 == 0)
					{
						int num39 = msg.reader().readInt();
						if (num39 == Char.myCharz().charID)
						{
							Char.myCharz().setMabuHold(false);
						}
						else
						{
							Char obj6 = GameScr.findCharInMap(num39);
							if (obj6 != null)
							{
								obj6.setMabuHold(false);
							}
						}
					}
					if (num37 == 2)
					{
						int charId2 = msg.reader().readInt();
						int id4 = msg.reader().readInt();
						((Mabu)GameScr.findCharInMap(charId2)).eat(id4);
					}
					if (num37 == 3)
					{
						GameScr.mabuPercent = msg.reader().readByte();
					}
					break;
				}
				case 51:
				{
					Mabu mabu = (Mabu)GameScr.findCharInMap(msg.reader().readInt());
					sbyte id3 = msg.reader().readByte();
					short x2 = msg.reader().readShort();
					short y2 = msg.reader().readShort();
					sbyte b14 = msg.reader().readByte();
					Char[] array7 = new Char[b14];
					long[] array8 = new long[b14];
					for (int num20 = 0; num20 < b14; num20++)
					{
						int num21 = msg.reader().readInt();
						Res.outz("char ID=" + num21);
						array7[num20] = null;
						if (num21 != Char.myCharz().charID)
						{
							array7[num20] = GameScr.findCharInMap(num21);
						}
						else
						{
							array7[num20] = Char.myCharz();
						}
						array8[num20] = msg.reader().readLong();
					}
					mabu.setSkill(id3, x2, y2, array7, array8);
					break;
				}
				case -127:
					readLuckyRound(msg);
					break;
				case -126:
				{
					sbyte b15 = msg.reader().readByte();
					Res.outz("type quay= " + b15);
					if (b15 == 1)
					{
						msg.reader().readByte();
						string num22 = msg.reader().readUTF();
						string finish = msg.reader().readUTF();
						GameScr.gI().showWinNumber(num22, finish);
					}
					if (b15 == 0)
					{
						GameScr.gI().showYourNumber(msg.reader().readUTF());
					}
					break;
				}
				case -122:
				{
					Npc npc = GameScr.findNPCInMap(msg.reader().readShort());
					sbyte b3 = msg.reader().readByte();
					npc.duahau = new int[b3];
					Res.outz("N DUA HAU= " + b3);
					for (int k = 0; k < b3; k++)
					{
						npc.duahau[k] = msg.reader().readShort();
					}
					npc.setStatus(msg.reader().readByte(), msg.reader().readInt());
					break;
				}
				case 102:
				{
					sbyte b11 = msg.reader().readByte();
					if (b11 == 0 || b11 == 1 || b11 == 2 || b11 == 6)
					{
						BigBoss2 bigBoss = Mob.getBigBoss2();
						if (bigBoss == null)
						{
							break;
						}
						if (b11 == 6)
						{
							bigBoss.x = (bigBoss.y = (bigBoss.xTo = (bigBoss.yTo = (bigBoss.xFirst = (bigBoss.yFirst = -1000)))));
							break;
						}
						sbyte b12 = msg.reader().readByte();
						Char[] array2 = new Char[b12];
						long[] array3 = new long[b12];
						for (int num13 = 0; num13 < b12; num13++)
						{
							int num14 = msg.reader().readInt();
							array2[num13] = null;
							if (num14 != Char.myCharz().charID)
							{
								array2[num13] = GameScr.findCharInMap(num14);
							}
							else
							{
								array2[num13] = Char.myCharz();
							}
							array3[num13] = msg.reader().readLong();
						}
						bigBoss.setAttack(array2, array3, b11);
					}
					if (b11 == 3 || b11 == 4 || b11 == 5 || b11 == 7)
					{
						BachTuoc bachTuoc = Mob.getBachTuoc();
						if (bachTuoc == null)
						{
							break;
						}
						switch (b11)
						{
						case 7:
							bachTuoc.x = (bachTuoc.y = (bachTuoc.xTo = (bachTuoc.yTo = (bachTuoc.xFirst = (bachTuoc.yFirst = -1000)))));
							return;
						case 3:
						case 4:
						{
							sbyte b13 = msg.reader().readByte();
							Char[] array4 = new Char[b13];
							long[] array5 = new long[b13];
							for (int num15 = 0; num15 < b13; num15++)
							{
								int num16 = msg.reader().readInt();
								array4[num15] = null;
								if (num16 != Char.myCharz().charID)
								{
									array4[num15] = GameScr.findCharInMap(num16);
								}
								else
								{
									array4[num15] = Char.myCharz();
								}
								array5[num15] = msg.reader().readLong();
							}
							bachTuoc.setAttack(array4, array5, b11);
							break;
						}
						}
						if (b11 == 5)
						{
							short xMoveTo = msg.reader().readShort();
							bachTuoc.move(xMoveTo);
						}
					}
					if (b11 > 9 && b11 < 30)
					{
						readActionBoss(msg, b11);
					}
					break;
				}
				case 101:
				{
					Res.outz("big boss--------------------------------------------------");
					BigBoss bigBoss2 = Mob.getBigBoss();
					if (bigBoss2 == null)
					{
						break;
					}
					sbyte b21 = msg.reader().readByte();
					if (b21 == 0 || b21 == 1 || b21 == 2 || b21 == 4 || b21 == 3)
					{
						if (b21 == 3)
						{
							bigBoss2.xTo = (bigBoss2.xFirst = msg.reader().readShort());
							bigBoss2.yTo = (bigBoss2.yFirst = msg.reader().readShort());
							bigBoss2.setFly();
						}
						else
						{
							sbyte b22 = msg.reader().readByte();
							Res.outz("CHUONG nChar= " + b22);
							Char[] array10 = new Char[b22];
							long[] array11 = new long[b22];
							for (int num41 = 0; num41 < b22; num41++)
							{
								int num42 = msg.reader().readInt();
								Res.outz("char ID=" + num42);
								array10[num41] = null;
								if (num42 != Char.myCharz().charID)
								{
									array10[num41] = GameScr.findCharInMap(num42);
								}
								else
								{
									array10[num41] = Char.myCharz();
								}
								array11[num41] = msg.reader().readLong();
							}
							bigBoss2.setAttack(array10, array11, b21);
						}
					}
					if (b21 == 5)
					{
						bigBoss2.haftBody = true;
						bigBoss2.status = 2;
					}
					if (b21 == 6)
					{
						bigBoss2.getDataB2();
						bigBoss2.x = msg.reader().readShort();
						bigBoss2.y = msg.reader().readShort();
					}
					if (b21 == 7)
					{
						bigBoss2.setAttack(null, null, b21);
					}
					if (b21 == 8)
					{
						bigBoss2.xTo = (bigBoss2.xFirst = msg.reader().readShort());
						bigBoss2.yTo = (bigBoss2.yFirst = msg.reader().readShort());
						bigBoss2.status = 2;
					}
					if (b21 == 9)
					{
						bigBoss2.x = (bigBoss2.y = (bigBoss2.xTo = (bigBoss2.yTo = (bigBoss2.xFirst = (bigBoss2.yFirst = -1000)))));
					}
					break;
				}
				case -120:
					Service.logController = mSystem.currentTimeMillis() - Service.curCheckController;
					Service.gI().sendCheckController();
					break;
				case -121:
					Service.logMap = mSystem.currentTimeMillis() - Service.curCheckMap;
					Service.gI().sendCheckMap();
					break;
				case 100:
				{
					sbyte num3 = msg.reader().readByte();
					sbyte b5 = msg.reader().readByte();
					Item item = null;
					if (num3 == 0)
					{
						item = Char.myCharz().arrItemBody[b5];
					}
					if (num3 == 1)
					{
						item = Char.myCharz().arrItemBag[b5];
					}
					short num4 = msg.reader().readShort();
					if (num4 == -1)
					{
						break;
					}
					item.template = ItemTemplates.get(num4);
					item.quantity = msg.reader().readInt();
					item.info = msg.reader().readUTF();
					item.content = msg.reader().readUTF();
					sbyte b6 = msg.reader().readByte();
					if (b6 != 0)
					{
						item.itemOption = new ItemOption[b6];
						for (int m = 0; m < item.itemOption.Length; m++)
						{
							ItemOption itemOption2 = Controller.gI().readItemOption(msg);
							if (itemOption2 != null)
							{
								item.itemOption[m] = itemOption2;
							}
						}
					}
					if (item.quantity <= 0)
					{
						item = null;
					}
					break;
				}
				case -123:
				{
					int charId = msg.reader().readInt();
					if (GameScr.findCharInMap(charId) != null)
					{
						GameScr.findCharInMap(charId).perCentMp = msg.reader().readByte();
					}
					break;
				}
				case -119:
					Char.myCharz().rank = msg.reader().readInt();
					break;
				case -117:
					GameScr.gI().tMabuEff = 0;
					GameScr.gI().percentMabu = msg.reader().readByte();
					if (GameScr.gI().percentMabu == 100)
					{
						GameScr.gI().mabuEff = true;
					}
					if (GameScr.gI().percentMabu == 101)
					{
						Npc.mabuEff = true;
					}
					break;
				case -116:
					GameScr.canAutoPlay = msg.reader().readByte() == 1;
					break;
				case -115:
					Char.myCharz().setPowerInfo(msg.reader().readUTF(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readShort());
					break;
				case -113:
				{
					sbyte[] array = new sbyte[10];
					for (int l = 0; l < 10; l++)
					{
						array[l] = msg.reader().readByte();
						Res.outz("vlue i= " + array[l]);
					}
					GameScr.gI().onKSkill(array);
					GameScr.gI().onOSkill(array);
					GameScr.gI().onCSkill(array);
					break;
				}
				case -111:
				{
					short num35 = msg.reader().readShort();
					ImageSource.vSource = new MyVector();
					for (int num36 = 0; num36 < num35; num36++)
					{
						string iD = msg.reader().readUTF();
						sbyte version = msg.reader().readByte();
						ImageSource.vSource.addElement(new ImageSource(iD, version));
					}
					ImageSource.checkRMS();
					ImageSource.saveRMS();
					break;
				}
				case 125:
				{
					sbyte fusion = msg.reader().readByte();
					int num40 = msg.reader().readInt();
					if (num40 == Char.myCharz().charID)
					{
						Char.myCharz().setFusion(fusion);
					}
					else if (GameScr.findCharInMap(num40) != null)
					{
						GameScr.findCharInMap(num40).setFusion(fusion);
					}
					break;
				}
				case 124:
				{
					short id5 = msg.reader().readShort();
					string text3 = msg.reader().readUTF();
					Res.outz("noi chuyen = " + text3 + "npc ID= " + id5);
					Npc npc2 = GameScr.findNPCInMap(id5);
					if (npc2 != null)
					{
						npc2.addInfo(text3);
					}
					break;
				}
				case 123:
				{
					Res.outz("SET POSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSss");
					int num25 = msg.reader().readInt();
					short xPos = msg.reader().readShort();
					short yPos = msg.reader().readShort();
					sbyte b17 = msg.reader().readByte();
					Char obj2 = null;
					if (num25 == Char.myCharz().charID)
					{
						obj2 = Char.myCharz();
					}
					else if (GameScr.findCharInMap(num25) != null)
					{
						obj2 = GameScr.findCharInMap(num25);
					}
					if (obj2 != null)
					{
						ServerEffect.addServerEffect((b17 != 0) ? 173 : 60, obj2, 1);
						obj2.setPos(xPos, yPos, b17);
					}
					break;
				}
				case 122:
				{
					short timeLogin = msg.reader().readShort();
					Res.outz("second login = " + timeLogin);
					LoginScr.timeLogin = timeLogin;
					LoginScr.currTimeLogin = (LoginScr.lastTimeLogin = mSystem.currentTimeMillis());
					GameCanvas.endDlg();
					break;
				}
				case 121:
					mSystem.publicID = msg.reader().readUTF();
					mSystem.strAdmob = msg.reader().readUTF();
					Res.outz("SHOW AD public ID= " + mSystem.publicID);
					mSystem.createAdmob();
					break;
				case -124:
				{
					sbyte b18 = msg.reader().readByte();
					sbyte num26 = msg.reader().readByte();
					if (num26 == 0)
					{
						if (b18 == 2)
						{
							int num27 = msg.reader().readInt();
							if (num27 == Char.myCharz().charID)
							{
								Char.myCharz().removeEffect();
							}
							else if (GameScr.findCharInMap(num27) != null)
							{
								GameScr.findCharInMap(num27).removeEffect();
							}
						}
						int num28 = msg.reader().readUnsignedByte();
						int num29 = msg.reader().readInt();
						if (num28 == 32)
						{
							if (b18 == 1)
							{
								int num30 = msg.reader().readInt();
								if (num29 == Char.myCharz().charID)
								{
									Char.myCharz().holdEffID = num28;
									GameScr.findCharInMap(num30).setHoldChar(Char.myCharz());
								}
								else if (GameScr.findCharInMap(num29) != null && num30 != Char.myCharz().charID)
								{
									GameScr.findCharInMap(num29).holdEffID = num28;
									GameScr.findCharInMap(num30).setHoldChar(GameScr.findCharInMap(num29));
								}
								else if (GameScr.findCharInMap(num29) != null && num30 == Char.myCharz().charID)
								{
									GameScr.findCharInMap(num29).holdEffID = num28;
									Char.myCharz().setHoldChar(GameScr.findCharInMap(num29));
								}
							}
							else if (num29 == Char.myCharz().charID)
							{
								Char.myCharz().removeHoleEff();
							}
							else if (GameScr.findCharInMap(num29) != null)
							{
								GameScr.findCharInMap(num29).removeHoleEff();
							}
						}
						if (num28 == 33)
						{
							if (b18 == 1)
							{
								if (num29 == Char.myCharz().charID)
								{
									Char.myCharz().protectEff = true;
								}
								else if (GameScr.findCharInMap(num29) != null)
								{
									GameScr.findCharInMap(num29).protectEff = true;
								}
							}
							else if (num29 == Char.myCharz().charID)
							{
								Char.myCharz().removeProtectEff();
							}
							else if (GameScr.findCharInMap(num29) != null)
							{
								GameScr.findCharInMap(num29).removeProtectEff();
							}
						}
						if (num28 == 39)
						{
							if (b18 == 1)
							{
								if (num29 == Char.myCharz().charID)
								{
									Char.myCharz().huytSao = true;
								}
								else if (GameScr.findCharInMap(num29) != null)
								{
									GameScr.findCharInMap(num29).huytSao = true;
								}
							}
							else if (num29 == Char.myCharz().charID)
							{
								Char.myCharz().removeHuytSao();
							}
							else if (GameScr.findCharInMap(num29) != null)
							{
								GameScr.findCharInMap(num29).removeHuytSao();
							}
						}
						if (num28 == 40)
						{
							if (b18 == 1)
							{
								if (num29 == Char.myCharz().charID)
								{
									Char.myCharz().blindEff = true;
								}
								else if (GameScr.findCharInMap(num29) != null)
								{
									GameScr.findCharInMap(num29).blindEff = true;
								}
							}
							else if (num29 == Char.myCharz().charID)
							{
								Char.myCharz().removeBlindEff();
							}
							else if (GameScr.findCharInMap(num29) != null)
							{
								GameScr.findCharInMap(num29).removeBlindEff();
							}
						}
						if (num28 == 41)
						{
							if (b18 == 1)
							{
								if (num29 == Char.myCharz().charID)
								{
									Char.myCharz().sleepEff = true;
								}
								else if (GameScr.findCharInMap(num29) != null)
								{
									GameScr.findCharInMap(num29).sleepEff = true;
								}
							}
							else if (num29 == Char.myCharz().charID)
							{
								Char.myCharz().removeSleepEff();
							}
							else if (GameScr.findCharInMap(num29) != null)
							{
								GameScr.findCharInMap(num29).removeSleepEff();
							}
						}
						if (num28 == 42)
						{
							if (b18 == 1)
							{
								if (num29 == Char.myCharz().charID)
								{
									Char.myCharz().stone = true;
								}
							}
							else if (num29 == Char.myCharz().charID)
							{
								Char.myCharz().stone = false;
							}
						}
					}
					if (num26 != 1)
					{
						break;
					}
					int num31 = msg.reader().readUnsignedByte();
					sbyte mobIndex = msg.reader().readByte();
					Res.outz("modbHoldID= " + mobIndex + " skillID= " + num31 + "eff ID= " + b18);
					if (num31 == 32)
					{
						if (b18 == 1)
						{
							int num32 = msg.reader().readInt();
							if (num32 == Char.myCharz().charID)
							{
								GameScr.findMobInMap(mobIndex).holdEffID = num31;
								Char.myCharz().setHoldMob(GameScr.findMobInMap(mobIndex));
							}
							else if (GameScr.findCharInMap(num32) != null)
							{
								GameScr.findMobInMap(mobIndex).holdEffID = num31;
								GameScr.findCharInMap(num32).setHoldMob(GameScr.findMobInMap(mobIndex));
							}
						}
						else
						{
							GameScr.findMobInMap(mobIndex).removeHoldEff();
						}
					}
					if (num31 == 40)
					{
						if (b18 == 1)
						{
							GameScr.findMobInMap(mobIndex).blindEff = true;
						}
						else
						{
							GameScr.findMobInMap(mobIndex).removeBlindEff();
						}
					}
					if (num31 == 41)
					{
						if (b18 == 1)
						{
							GameScr.findMobInMap(mobIndex).sleepEff = true;
						}
						else
						{
							GameScr.findMobInMap(mobIndex).removeSleepEff();
						}
					}
					break;
				}
				case -125:
				{
					ChatTextField.gI().isShow = false;
					string text2 = msg.reader().readUTF();
					Res.outz("titile= " + text2);
					sbyte b16 = msg.reader().readByte();
					ClientInput.gI().setInput(b16, text2);
					for (int num23 = 0; num23 < b16; num23++)
					{
						ClientInput.gI().tf[num23].name = msg.reader().readUTF();
						sbyte num24 = msg.reader().readByte();
						if (num24 == 0)
						{
							ClientInput.gI().tf[num23].setIputType(TField.INPUT_TYPE_NUMERIC);
						}
						if (num24 == 1)
						{
							ClientInput.gI().tf[num23].setIputType(TField.INPUT_TYPE_ANY);
						}
						if (num24 == 2)
						{
							ClientInput.gI().tf[num23].setIputType(TField.INPUT_TYPE_PASSWORD);
						}
					}
					break;
				}
				case -110:
				{
					sbyte num17 = msg.reader().readByte();
					if (num17 == 1)
					{
						int id2 = msg.reader().readInt();
						sbyte[] array6 = Rms.loadRMS(id2 + string.Empty);
						if (array6 == null)
						{
							Service.gI().sendServerData(1, -1, null);
						}
						else
						{
							Service.gI().sendServerData(1, id2, array6);
						}
					}
					if (num17 == 0)
					{
						int num18 = msg.reader().readInt();
						short num19 = msg.reader().readShort();
						sbyte[] data = new sbyte[num19];
						msg.reader().read(ref data, 0, num19);
						Rms.saveRMS(num18 + string.Empty, data);
					}
					break;
				}
				case 93:
				{
					string str = msg.reader().readUTF();
					str = Res.changeString(str);
					GameScr.gI().chatVip(str);
					break;
				}
				case -106:
				{
					short num11 = msg.reader().readShort();
					int num12 = msg.reader().readShort();
					if (ItemTime.isExistItem(num11))
					{
						ItemTime.getItemById(num11).initTime(num12);
						break;
					}
					ItemTime o = new ItemTime(num11, num12);
					Char.vItemTime.addElement(o);
					break;
				}
				case -105:
					TransportScr.gI().time = 0;
					TransportScr.gI().maxTime = msg.reader().readShort();
					TransportScr.gI().last = (TransportScr.gI().curr = mSystem.currentTimeMillis());
					TransportScr.gI().type = msg.reader().readByte();
					TransportScr.gI().switchToMe();
					break;
				case -103:
					switch (msg.reader().readByte())
					{
					case 0:
					{
						GameCanvas.panel.vFlag.removeAllElements();
						sbyte b8 = msg.reader().readByte();
						for (int num7 = 0; num7 < b8; num7++)
						{
							Item item2 = new Item();
							short num8 = msg.reader().readShort();
							if (num8 != -1)
							{
								item2.template = ItemTemplates.get(num8);
								sbyte b9 = msg.reader().readByte();
								if (b9 != -1)
								{
									item2.itemOption = new ItemOption[b9];
									for (int num9 = 0; num9 < item2.itemOption.Length; num9++)
									{
										ItemOption itemOption3 = Controller.gI().readItemOption(msg);
										if (itemOption3 != null)
										{
											item2.itemOption[num9] = itemOption3;
										}
									}
								}
							}
							GameCanvas.panel.vFlag.addElement(item2);
						}
						GameCanvas.panel.setTypeFlag();
						GameCanvas.panel.show();
						break;
					}
					case 1:
					{
						int num10 = msg.reader().readInt();
						sbyte b10 = msg.reader().readByte();
						Res.outz("---------------actionFlag1:  " + num10 + " : " + b10);
						if (num10 == Char.myCharz().charID)
						{
							Char.myCharz().cFlag = b10;
						}
						else if (GameScr.findCharInMap(num10) != null)
						{
							GameScr.findCharInMap(num10).cFlag = b10;
						}
						GameScr.gI().getFlagImage(num10, b10);
						break;
					}
					case 2:
					{
						sbyte b7 = msg.reader().readByte();
						int num5 = msg.reader().readShort();
						PKFlag pKFlag = new PKFlag();
						pKFlag.cflag = b7;
						pKFlag.IDimageFlag = num5;
						GameScr.vFlag.addElement(pKFlag);
						for (int n = 0; n < GameScr.vFlag.size(); n++)
						{
							PKFlag pKFlag2 = (PKFlag)GameScr.vFlag.elementAt(n);
							Res.outz("i: " + n + "  cflag: " + pKFlag2.cflag + "   IDimageFlag: " + pKFlag2.IDimageFlag);
						}
						for (int num6 = 0; num6 < GameScr.vCharInMap.size(); num6++)
						{
							Char obj = (Char)GameScr.vCharInMap.elementAt(num6);
							if (obj != null && obj.cFlag == b7)
							{
								obj.flagImage = num5;
							}
						}
						if (Char.myCharz().cFlag == b7)
						{
							Char.myCharz().flagImage = num5;
						}
						break;
					}
					}
					break;
				case -102:
				{
					sbyte b4 = msg.reader().readByte();
					if (b4 != 0 && b4 == 1)
					{
						GameCanvas.loginScr.isLogin2 = false;
						Service.gI().login(Rms.loadRMSString(Rms.RMS_acc), Rms.loadRMSString(Rms.RMS_pass), GameMidlet.VERSION, 0);
						LoginScr.isLoggingIn = true;
					}
					break;
				}
				case -101:
				{
					GameCanvas.loginScr.isLogin2 = true;
					GameCanvas.connect();
					string text = msg.reader().readUTF();
					Rms.saveRMSString(Rms.RMS_userAo + ServerListScreen.ipSelect, text);
					Service.gI().setClientType();
					Service.gI().login(text, string.Empty, GameMidlet.VERSION, 1);
					break;
				}
				case -100:
				{
					InfoDlg.hide();
					bool flag = false;
					if (GameCanvas.w > 2 * Panel.WIDTH_PANEL)
					{
						flag = true;
					}
					sbyte b = msg.reader().readByte();
					if (b < 0)
					{
						break;
					}
					Res.outz("t Indxe= " + b);
					GameCanvas.panel.maxPageShop[b] = msg.reader().readByte();
					GameCanvas.panel.currPageShop[b] = msg.reader().readByte();
					Res.outz("max page= " + GameCanvas.panel.maxPageShop[b] + " curr page= " + GameCanvas.panel.currPageShop[b]);
					int num = msg.reader().readUnsignedByte();
					Char.myCharz().arrItemShop[b] = new Item[num];
					for (int i = 0; i < num; i++)
					{
						short num2 = msg.reader().readShort();
						if (num2 == -1)
						{
							continue;
						}
						Res.outz("template id= " + num2);
						Char.myCharz().arrItemShop[b][i] = new Item();
						Char.myCharz().arrItemShop[b][i].template = ItemTemplates.get(num2);
						Char.myCharz().arrItemShop[b][i].itemId = msg.reader().readShort();
						Char.myCharz().arrItemShop[b][i].buyCoin = msg.reader().readInt();
						Char.myCharz().arrItemShop[b][i].buyGold = msg.reader().readInt();
						Char.myCharz().arrItemShop[b][i].buyType = msg.reader().readByte();
						Char.myCharz().arrItemShop[b][i].quantity = msg.reader().readInt();
						Char.myCharz().arrItemShop[b][i].isMe = msg.reader().readByte();
						Panel.strWantToBuy = mResources.say_wat_do_u_want_to_buy;
						sbyte b2 = msg.reader().readByte();
						if (b2 != -1)
						{
							Char.myCharz().arrItemShop[b][i].itemOption = new ItemOption[b2];
							for (int j = 0; j < Char.myCharz().arrItemShop[b][i].itemOption.Length; j++)
							{
								ItemOption itemOption = Controller.gI().readItemOption(msg);
								if (itemOption != null)
								{
									Char.myCharz().arrItemShop[b][i].itemOption[j] = itemOption;
									Char.myCharz().arrItemShop[b][i].compare = GameCanvas.panel.getCompare(Char.myCharz().arrItemShop[b][i]);
								}
							}
						}
						if (msg.reader().readByte() == 1)
						{
							int headTemp = msg.reader().readShort();
							int bodyTemp = msg.reader().readShort();
							int legTemp = msg.reader().readShort();
							int bagTemp = msg.reader().readShort();
							Char.myCharz().arrItemShop[b][i].setPartTemp(headTemp, bodyTemp, legTemp, bagTemp);
						}
						if (GameMidlet.intVERSION >= 237)
						{
							Char.myCharz().arrItemShop[b][i].nameNguoiKyGui = msg.reader().readUTF();
							Res.err("nguoi ki gui  " + Char.myCharz().arrItemShop[b][i].nameNguoiKyGui);
						}
					}
					if (flag)
					{
						GameCanvas.panel2.setTabKiGui();
					}
					GameCanvas.panel.setTabShop();
					GameCanvas.panel.cmy = (GameCanvas.panel.cmtoY = 0);
					break;
				}
				}
			}
			catch (Exception ex4)
			{
				Res.outz("=====> Controller2 " + ex4.StackTrace);
			}
		}

		private static void readLuckyRound(Message msg)
		{
			try
			{
				switch (msg.reader().readByte())
				{
				case 0:
				{
					sbyte b2 = msg.reader().readByte();
					short[] array2 = new short[b2];
					for (int j = 0; j < b2; j++)
					{
						array2[j] = msg.reader().readShort();
					}
					sbyte b3 = msg.reader().readByte();
					int price = msg.reader().readInt();
					short idTicket = msg.reader().readShort();
					CrackBallScr.gI().SetCrackBallScr(array2, (byte)b3, price, idTicket);
					break;
				}
				case 1:
				{
					sbyte b = msg.reader().readByte();
					short[] array = new short[b];
					for (int i = 0; i < b; i++)
					{
						array[i] = msg.reader().readShort();
					}
					CrackBallScr.gI().DoneCrackBallScr(array);
					break;
				}
				}
			}
			catch (Exception)
			{
			}
		}

		private static void readInfoRada(Message msg)
		{
			try
			{
				switch (msg.reader().readByte())
				{
				case 0:
				{
					RadarScr.gI();
					MyVector myVector = new MyVector(string.Empty);
					short num2 = msg.reader().readShort();
					int num3 = 0;
					for (int i = 0; i < num2; i++)
					{
						Info_RadaScr info_RadaScr = new Info_RadaScr();
						int id = msg.reader().readShort();
						int no = i + 1;
						int idIcon = msg.reader().readShort();
						sbyte rank = msg.reader().readByte();
						sbyte amount = msg.reader().readByte();
						sbyte max_amount = msg.reader().readByte();
						short templateId = -1;
						Char charInfo = null;
						sbyte b = msg.reader().readByte();
						if (b == 0)
						{
							templateId = msg.reader().readShort();
						}
						else
						{
							short head = msg.reader().readShort();
							int body = msg.reader().readShort();
							int leg = msg.reader().readShort();
							int bag = msg.reader().readShort();
							charInfo = Info_RadaScr.SetCharInfo(head, body, leg, bag);
						}
						string name = msg.reader().readUTF();
						string info = msg.reader().readUTF();
						sbyte b2 = msg.reader().readByte();
						sbyte use = msg.reader().readByte();
						sbyte b3 = msg.reader().readByte();
						ItemOption[] array = null;
						if (b3 != 0)
						{
							array = new ItemOption[b3];
							for (int j = 0; j < array.Length; j++)
							{
								ItemOption itemOption = Controller.gI().readItemOption(msg);
								sbyte activeCard = msg.reader().readByte();
								if (itemOption != null)
								{
									array[j] = itemOption;
									array[j].activeCard = activeCard;
								}
							}
						}
						info_RadaScr.SetInfo(id, no, idIcon, rank, b, templateId, name, info, charInfo, array);
						info_RadaScr.SetLevel(b2);
						info_RadaScr.SetUse(use);
						info_RadaScr.SetAmount(amount, max_amount);
						myVector.addElement(info_RadaScr);
						if (b2 > 0)
						{
							num3++;
						}
					}
					RadarScr.gI().SetRadarScr(myVector, num3, num2);
					RadarScr.gI().switchToMe();
					break;
				}
				case 1:
				{
					int id3 = msg.reader().readShort();
					sbyte use2 = msg.reader().readByte();
					if (Info_RadaScr.GetInfo(RadarScr.list, id3) != null)
					{
						Info_RadaScr.GetInfo(RadarScr.list, id3).SetUse(use2);
					}
					RadarScr.SetListUse();
					break;
				}
				case 2:
				{
					int num4 = msg.reader().readShort();
					sbyte level = msg.reader().readByte();
					int num5 = 0;
					for (int k = 0; k < RadarScr.list.size(); k++)
					{
						Info_RadaScr info_RadaScr2 = (Info_RadaScr)RadarScr.list.elementAt(k);
						if (info_RadaScr2 != null)
						{
							if (info_RadaScr2.id == num4)
							{
								info_RadaScr2.SetLevel(level);
							}
							if (info_RadaScr2.level > 0)
							{
								num5++;
							}
						}
					}
					RadarScr.SetNum(num5, RadarScr.list.size());
					if (Info_RadaScr.GetInfo(RadarScr.listUse, num4) != null)
					{
						Info_RadaScr.GetInfo(RadarScr.listUse, num4).SetLevel(level);
					}
					break;
				}
				case 3:
				{
					int id2 = msg.reader().readShort();
					sbyte amount2 = msg.reader().readByte();
					sbyte max_amount2 = msg.reader().readByte();
					if (Info_RadaScr.GetInfo(RadarScr.list, id2) != null)
					{
						Info_RadaScr.GetInfo(RadarScr.list, id2).SetAmount(amount2, max_amount2);
					}
					if (Info_RadaScr.GetInfo(RadarScr.listUse, id2) != null)
					{
						Info_RadaScr.GetInfo(RadarScr.listUse, id2).SetAmount(amount2, max_amount2);
					}
					break;
				}
				case 4:
				{
					int num = msg.reader().readInt();
					short idAuraEff = msg.reader().readShort();
					Char obj = null;
					obj = ((num != Char.myCharz().charID) ? GameScr.findCharInMap(num) : Char.myCharz());
					if (obj != null)
					{
						obj.idAuraEff = idAuraEff;
						obj.idEff_Set_Item = msg.reader().readByte();
					}
					break;
				}
				}
			}
			catch (Exception)
			{
			}
		}

		private static void readInfoEffChar(Message msg)
		{
			try
			{
				sbyte b = msg.reader().readByte();
				int num = msg.reader().readInt();
				Char obj = null;
				obj = ((num != Char.myCharz().charID) ? GameScr.findCharInMap(num) : Char.myCharz());
				switch (b)
				{
				case 0:
				{
					int id = msg.reader().readShort();
					int layer = msg.reader().readByte();
					int loop = msg.reader().readByte();
					short loopCount = msg.reader().readShort();
					sbyte isStand = msg.reader().readByte();
					if (obj != null)
					{
						obj.addEffChar(new Effect(id, obj, layer, loop, loopCount, isStand));
					}
					break;
				}
				case 1:
				{
					int id2 = msg.reader().readShort();
					if (obj != null)
					{
						obj.removeEffChar(0, id2);
					}
					break;
				}
				case 2:
					if (obj != null)
					{
						obj.removeEffChar(-1, 0);
					}
					break;
				}
			}
			catch (Exception)
			{
			}
		}

		private static void readActionBoss(Message msg, int actionBoss)
		{
			try
			{
				NewBoss newBoss = Mob.getNewBoss(msg.reader().readByte());
				if (newBoss == null)
				{
					return;
				}
				if (actionBoss == 10)
				{
					short xMoveTo = msg.reader().readShort();
					short yMoveTo = msg.reader().readShort();
					newBoss.move(xMoveTo, yMoveTo);
				}
				if (actionBoss >= 11 && actionBoss <= 20)
				{
					sbyte b = msg.reader().readByte();
					Char[] array = new Char[b];
					long[] array2 = new long[b];
					for (int i = 0; i < b; i++)
					{
						int num = msg.reader().readInt();
						array[i] = null;
						if (num != Char.myCharz().charID)
						{
							array[i] = GameScr.findCharInMap(num);
						}
						else
						{
							array[i] = Char.myCharz();
						}
						array2[i] = msg.reader().readLong();
					}
					sbyte dir = msg.reader().readByte();
					newBoss.setAttack(array, array2, (sbyte)(actionBoss - 10), dir);
				}
				if (actionBoss == 21)
				{
					newBoss.xTo = msg.reader().readShort();
					newBoss.yTo = msg.reader().readShort();
					newBoss.setFly();
				}
				int num2 = 22;
				if (actionBoss == 23)
				{
					newBoss.setDie();
				}
			}
			catch (Exception)
			{
			}
		}
	}
}

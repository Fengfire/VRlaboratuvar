using UnityEngine;
namespace ChemistryLab {
public class LabHUD:MonoBehaviour {
 LabSession session; int page; GUIStyle title,body,small,button; Texture2D panel;
 void Awake(){session=GetComponent<LabSession>();}
 void OnDestroy(){if(panel)Destroy(panel);}
 void Styles(){if(panel)return;panel=new Texture2D(1,1);panel.SetPixel(0,0,new Color(.035f,.065f,.08f,.94f));panel.Apply();body=new GUIStyle(GUI.skin.label){fontSize=18,wordWrap=true};body.normal.textColor=new Color(.91f,.94f,.93f);title=new GUIStyle(body){fontSize=26,fontStyle=FontStyle.Bold};small=new GUIStyle(body){fontSize=15};button=new GUIStyle(GUI.skin.button){fontSize=17,padding=new RectOffset(12,12,8,8)};}
 void Card(Rect r){GUI.DrawTexture(r,panel);}
 void OnGUI(){Styles();float scale=Mathf.Min(Screen.width/1280f,Screen.height/720f);GUI.matrix=Matrix4x4.TRS(new Vector3((Screen.width-1280*scale)/2,0),Quaternion.identity,Vector3.one*scale);
 Card(new Rect(20,18,1240,64));GUI.Label(new Rect(38,30,310,40),"ELEMENT  /  KİMYA",title);GUI.Label(new Rect(350,40,150,25),session.successes+" / 6 keşif",small);
 if(GUI.Button(new Rect(540,30,160,40),"Deney defteri  F1",button))session.guide=!session.guide;
 if(GUI.Button(new Rect(710,30,160,40),"Temizle  F6",button)){session.desktopRig.GetComponent<DesktopLabController>().Drop();session.ResetLab();}
 if(GUI.Button(new Rect(880,30,160,40),"Loş / Aydınlık  F7",button))session.ToggleLight();
 if(GUI.Button(new Rect(1050,30,190,40),session.xrMode?"Masaüstü  F8":"XR eller  F8",button))session.ToggleMode();
 Card(new Rect(20,586,1240,114));GUI.Label(new Rect(38,598,1200,60),session.message,body);
 GUI.Label(new Rect(38,666,1200,26),session.xrMode?"K kavra • O bırak • N işaret • Tekerlek mesafe • Q basılı: dök • Home elleri ortala • 8/2 yükseklik":"WASD yürü • Sağ fare bak • E tut/bırak • Tekerlek mesafe • Q basılı: dök • P işaret • 8/2 yükseklik",small);
 if(!session.xrMode)GUI.Label(new Rect(633,351,25,25),"·",title);
 if(session.guide&&session.recipes.Length>0){var r=session.recipes[page];Card(new Rect(20,100,460,460));GUI.Label(new Rect(42,120,410,36),"DENEY DEFTERİ",title);GUI.Label(new Rect(42,170,410,32),(page+1)+" / "+session.recipes.Length+"   "+r.title,body);GUI.Label(new Rect(42,215,410,50),"MADDELER\n"+r.reagentA+"  +  "+r.reagentB,body);GUI.Label(new Rect(42,280,410,60),r.equation,body);GUI.Label(new Rect(42,350,410,110),r.explanation,body);
 if(GUI.Button(new Rect(42,496,120,40),"← Önceki",button))page=(page+session.recipes.Length-1)%session.recipes.Length;
 if(GUI.Button(new Rect(176,496,120,40),"Sonraki →",button))page=(page+1)%session.recipes.Length;
 if(GUI.Button(new Rect(310,496,140,40),"Kapat",button))session.guide=false;}
 }
}
}


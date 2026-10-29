using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
namespace ChemistryLab {
public class LabSession : MonoBehaviour {
 public ReactionDefinition[] recipes;
 public Renderer liquid, solid; public ParticleSystem bubbles; public Light flash;
 public GameObject xrRig, simulator, desktopRig;
 public string message="Rehberi açın, iki reaktifi merkezdeki deney kabına aktarın.";
 public string first=""; public bool completed, lightOn, xrMode;
 public int successes; public bool guide=true;
 HashSet<string> discovered=new HashSet<string>();
 public void Add(LabItem item){
  if(completed){message="Yeni deney için F6 ile masayı temizleyin.";return;}
  if(item.consumed){message="Bu reaktif zaten eklendi.";return;}
  if(first==item.reagent){message="Farklı bir ikinci madde seçin.";return;}
  item.consumed=true;
  if(first==""){first=item.reagent;message=item.displayName+" eklendi. İkinci maddeyi ekleyin.";return;}
  Evaluate(first,item.reagent);
 }
 public void Evaluate(string a,string b){
  foreach(var r in recipes)if(r.Matches(a,b)){
   if(r.lightRequired&&!lightOn){message="Gaz hücresi hazır. Fotokimyasal başlatma için F7 (ışık).";pending=r;return;}
   Complete(r);return;
  }
  completed=true;message="Bu ikili için modelde gözlenebilir tepkime tanımlı değil. Bu, gerçek hayatta güvenli oldukları anlamına gelmez. F6: temizle.";
 }
 ReactionDefinition pending; AudioClip tone; AudioSource sound; void PlayTone(){if(!sound)sound=gameObject.AddComponent<AudioSource>();if(!tone){tone=AudioClip.Create("Discovery",11025,1,44100,false);var samples=new float[11025];for(int i=0;i<samples.Length;i++)samples[i]=Mathf.Sin(i*2*Mathf.PI*660/44100f)*.08f*(1f-i/11025f);tone.SetData(samples,0);}sound.PlayOneShot(tone);} void OnDestroy(){if(tone)Destroy(tone);}
 void Complete(ReactionDefinition r){var atmosphere=GetComponent<LabAtmosphere>();if(atmosphere)atmosphere.Celebrate();PlayTone();completed=true;pending=null;solid.gameObject.SetActive(r.precipitate);solid.material.color=r.title.Contains("Demir")?new Color(.7f,.3f,.12f):r.color;
  if(r.gas)bubbles.Play();if(r.lightRequired)flash.intensity=3;
  var effects=GetComponent<LabReactionEffects>();if(effects)effects.Show(r);discovered.Add(r.name);successes=discovered.Count;
  message=r.title+"\n"+r.equation+"\n"+r.explanation;var safety=GetComponent<LabSafety>();if(safety)safety.OnReaction();
 }
 public void ResetLab(){var safety=GetComponent<LabSafety>();if(safety)safety.ClearIrritation();var atmosphere=GetComponent<LabAtmosphere>();if(atmosphere)atmosphere.ResetLighting();var effects=GetComponent<LabReactionEffects>();if(effects)effects.Clear();foreach(var item in FindObjectsByType<LabItem>(FindObjectsSortMode.None))item.Restore();first="";completed=false;pending=null;lightOn=false;liquid.material.color=new Color(.25f,.7f,.8f);solid.gameObject.SetActive(false);bubbles.Stop(true,ParticleSystemStopBehavior.StopEmittingAndClear);flash.intensity=0;message="Deney alanı hazır. İki farklı reaktif seçin.";}
 void Update(){var k=Keyboard.current;if(k==null)return;
  if(k.f1Key.wasPressedThisFrame)guide=!guide;
  if(k.f6Key.wasPressedThisFrame){var p=desktopRig.GetComponent<DesktopLabController>();p.Drop();ResetLab();}
  if(k.f7Key.wasPressedThisFrame)ToggleLight();
  if(k.f8Key.wasPressedThisFrame)ToggleMode();
  flash.intensity=Mathf.MoveTowards(flash.intensity,0,Time.deltaTime*2);
 }
 public void ToggleLight(){var atmosphere=GetComponent<LabAtmosphere>();if(atmosphere)atmosphere.ToggleDim();lightOn=!lightOn;if(lightOn&&pending)Complete(pending);}
 public void ToggleMode(){desktopRig.GetComponent<DesktopLabController>().Drop();ResetLab();xrMode=!xrMode;desktopRig.SetActive(!xrMode);xrRig.SetActive(xrMode);simulator.SetActive(xrMode);}
}
}




using UnityEngine;
using UnityEngine.InputSystem;
namespace ChemistryLab {
public class LabAtmosphere:MonoBehaviour {
 Light[] lights; float[] intensities; Color ambient; UnityEngine.Rendering.AmbientMode mode; float reflection; float endTime; bool dim; Light glow; Material liquid; LabSession session;
 void Awake(){session=GetComponent<LabSession>();lights=FindObjectsByType<Light>(FindObjectsSortMode.None);intensities=new float[lights.Length];for(int i=0;i<lights.Length;i++)intensities[i]=lights[i].intensity;ambient=RenderSettings.ambientLight;mode=RenderSettings.ambientMode;reflection=RenderSettings.reflectionIntensity;var g=new GameObject("Reaction vessel glow");g.transform.SetParent(transform);glow=g.AddComponent<Light>();glow.range=3;glow.color=new Color(.3f,.9f,1);glow.intensity=0;liquid=session.liquid.material;}
 public void ToggleDim(){dim=!dim;}
 public void Celebrate(){endTime=Time.time+3.5f;}
 public void ResetLighting(){dim=false;endTime=0;}
 void LateUpdate(){bool celebrate=Time.time<endTime;float target=celebrate?.015f:dim?.12f:1;for(int i=0;i<lights.Length;i++)if(lights[i]&&lights[i]!=session.flash)lights[i].intensity=Mathf.Lerp(lights[i].intensity,intensities[i]*target,Time.deltaTime*6);
 RenderSettings.ambientMode=target<1?UnityEngine.Rendering.AmbientMode.Flat:mode;RenderSettings.ambientLight=Color.Lerp(RenderSettings.ambientLight,target<1?new Color(.015f,.022f,.035f):ambient,Time.deltaTime*6);RenderSettings.reflectionIntensity=Mathf.Lerp(RenderSettings.reflectionIntensity,reflection*target,Time.deltaTime*6);
 glow.transform.position=session.liquid.transform.position+Vector3.up*.17f;glow.intensity=celebrate?2.5f+Mathf.Sin(Time.time*5)*.4f:0;liquid.EnableKeyword("_EMISSION");liquid.SetColor("_EmissionColor",celebrate?new Color(.1f,.75f,1)*2:Color.black);
 }
 void OnDestroy(){for(int i=0;i<lights.Length;i++)if(lights[i])lights[i].intensity=intensities[i];RenderSettings.ambientLight=ambient;RenderSettings.ambientMode=mode;RenderSettings.reflectionIntensity=reflection;}
}
}

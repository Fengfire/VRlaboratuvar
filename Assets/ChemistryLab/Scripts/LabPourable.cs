using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
namespace ChemistryLab {
public class LabPourable:MonoBehaviour {
 public float containerMass=.1f, contentsMass=.15f; public bool desktopHeld; public float remaining=1; public float delivered;
 LabItem item; XRGrabInteractable grab; LineRenderer stream; Transform visual; Quaternion rest; Vector3 mouth; Material streamMat; ParticleSystem grains; bool granular;
 public void UpdateMass(){var body=GetComponent<Rigidbody>();if(body)body.mass=Mathf.Max(.001f,containerMass+contentsMass*remaining);}
 void Start(){UpdateMass();item=GetComponent<LabItem>();grab=GetComponent<XRGrabInteractable>();var bounds=GetComponent<Collider>().bounds;mouth=transform.InverseTransformPoint(new Vector3(bounds.center.x,bounds.max.y,bounds.center.z));
 visual=new GameObject("Pouring visual pivot").transform;visual.SetParent(transform,false);visual.localPosition=transform.InverseTransformPoint(bounds.center);var rs=GetComponentsInChildren<Renderer>();foreach(var r in rs)if(r.transform!=transform)r.transform.SetParent(visual,true);rest=visual.localRotation;
 var g=new GameObject("Pour stream");g.transform.SetParent(transform,false);stream=g.AddComponent<LineRenderer>();stream.positionCount=2;stream.startWidth=.012f;stream.endWidth=.018f;streamMat=new Material(Shader.Find("Universal Render Pipeline/Unlit"));streamMat.color=item.reagent=="CuSO4"?new Color(.1f,.5f,.9f):new Color(.85f,.9f,.95f);stream.sharedMaterial=streamMat;stream.enabled=false;granular=item.reagent=="NaHCO3"||item.reagent=="Nişasta"||item.reagent=="NaCl"||item.reagent=="Fe";if(granular){var particles=new GameObject("Falling solid grains");particles.transform.SetParent(transform,false);grains=particles.AddComponent<ParticleSystem>();var main=grains.main;main.playOnAwake=false;main.startLifetime=.45f;main.startSpeed=.1f;main.startSize=.012f;main.gravityModifier=1;main.simulationSpace=ParticleSystemSimulationSpace.World;var emission=grains.emission;emission.rateOverTime=50;var shape=grains.shape;shape.shapeType=ParticleSystemShapeType.Cone;shape.radius=.018f;shape.angle=10;var pr=grains.GetComponent<ParticleSystemRenderer>();var sphere=GameObject.CreatePrimitive(PrimitiveType.Sphere);pr.renderMode=ParticleSystemRenderMode.Mesh;pr.mesh=sphere.GetComponent<MeshFilter>().sharedMesh;pr.sharedMaterial=streamMat;Destroy(sphere);grains.Stop(true,ParticleSystemStopBehavior.StopEmittingAndClear);}}
 void Update(){if(!item||string.IsNullOrEmpty(item.reagent))return;bool held=desktopHeld||(grab&&grab.isSelected);bool request=held&&Keyboard.current!=null&&Keyboard.current.qKey.isPressed;
 visual.localRotation=Quaternion.Slerp(visual.localRotation,rest*Quaternion.Euler(0,0,request?110:0),Time.deltaTime*5);
 bool tilted=Vector3.Dot(transform.up,Vector3.up)<.25f||request&&Quaternion.Angle(rest,visual.localRotation)>75;
 var vessel=FindFirstObjectByType<ReactionVessel>();Vector3 origin=transform.TransformPoint(mouth);if(request)origin=visual.position+visual.up*.08f;
 bool gas=item.reagent.Contains("(g)");bool active=request&&remaining>0&&!item.consumed&&(gas||tilted);stream.enabled=active&&!granular&&!gas;if(grains){grains.transform.position=origin;grains.transform.rotation=Quaternion.Euler(90,0,0);if(active&&!grains.isPlaying)grains.Play();else if(!active&&grains.isPlaying)grains.Stop();}
 if(!active)return;Vector3 end=origin+Vector3.down*.8f;stream.SetPosition(0,origin);stream.SetPosition(1,end);
 var safety=FindFirstObjectByType<LabSafety>();if(safety)safety.CheckStream(item.reagent,origin,end);remaining=Mathf.Max(0,remaining-Time.deltaTime*.22f);UpdateMass();
 if(vessel){var b=vessel.GetComponent<Collider>().bounds;bool inside=origin.x>b.min.x&&origin.x<b.max.x&&origin.z>b.min.z&&origin.z<b.max.z&&origin.y>b.min.y&&origin.y<b.max.y+1;
 if(inside){end.y=b.min.y;stream.SetPosition(1,end);delivered+=Time.deltaTime;if(delivered>.65f)vessel.session.Add(item);}}
 }
 public void ResetContents(){desktopHeld=false;remaining=1;UpdateMass();delivered=0;if(grains)grains.Stop(true,ParticleSystemStopBehavior.StopEmittingAndClear);if(stream)stream.enabled=false;if(visual)visual.localRotation=rest;}
 void OnDestroy(){if(streamMat)Destroy(streamMat);}
}
}






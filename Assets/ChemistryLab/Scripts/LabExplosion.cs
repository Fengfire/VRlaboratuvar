using UnityEngine;
using System.Collections;
namespace ChemistryLab {
public class LabExplosion:MonoBehaviour {
 ParticleSystem sparks; LineRenderer ring; Material material; Coroutine routine;
 void Build(){if(sparks)return;material=new Material(Shader.Find("Universal Render Pipeline/Unlit"));material.color=new Color(1,.65f,.2f);var g=new GameObject("Contained reaction burst");g.transform.SetParent(transform,false);sparks=g.AddComponent<ParticleSystem>();sparks.Stop(true,ParticleSystemStopBehavior.StopEmittingAndClear);var main=sparks.main;main.playOnAwake=false;main.loop=false;main.duration=.7f;main.startLifetime=.55f;main.startSpeed=new ParticleSystem.MinMaxCurve(.3f,1.2f);main.startSize=new ParticleSystem.MinMaxCurve(.01f,.035f);main.gravityModifier=.2f;main.simulationSpace=ParticleSystemSimulationSpace.World;var emission=sparks.emission;emission.rateOverTime=0;emission.SetBursts(new[]{new ParticleSystem.Burst(0,75)});var shape=sparks.shape;shape.shapeType=ParticleSystemShapeType.Sphere;shape.radius=.08f;var source=GameObject.CreatePrimitive(PrimitiveType.Sphere);var renderer=sparks.GetComponent<ParticleSystemRenderer>();renderer.renderMode=ParticleSystemRenderMode.Mesh;renderer.mesh=source.GetComponent<MeshFilter>().sharedMesh;renderer.sharedMaterial=material;Destroy(source);sparks.Stop(true,ParticleSystemStopBehavior.StopEmittingAndClear);
 var r=new GameObject("Shockwave ring");r.transform.SetParent(transform,false);ring=r.AddComponent<LineRenderer>();ring.sharedMaterial=material;ring.positionCount=65;ring.useWorldSpace=true;ring.startWidth=ring.endWidth=.012f;ring.enabled=false;}
 public void Play(Vector3 center){Build();Clear();sparks.transform.position=center;sparks.Play();routine=StartCoroutine(Wave(center));}
 IEnumerator Wave(Vector3 center){ring.enabled=true;float t=0;while(t<.6f){t+=Time.deltaTime;float radius=.08f+t*.85f;ring.startWidth=ring.endWidth=.02f*(1-t/.6f);for(int i=0;i<65;i++){float a=i*Mathf.PI/32;ring.SetPosition(i,center+new Vector3(Mathf.Sin(a)*radius,0,Mathf.Cos(a)*radius));}yield return null;}ring.enabled=false;routine=null;}
 public void Clear(){if(routine!=null)StopCoroutine(routine);routine=null;if(sparks)sparks.Stop(true,ParticleSystemStopBehavior.StopEmittingAndClear);if(ring)ring.enabled=false;}
 void OnDestroy(){if(material)Destroy(material);}
}
}


using UnityEngine;
using UnityEngine.InputSystem;
using System.Reflection;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
namespace ChemistryLab {
// XRI 3.5.1 simulator state adapter: updates the tracked hand itself, so grabs follow it.
[DefaultExecutionOrder(1000)]
public class LabHandReach:MonoBehaviour {
 XRInteractionSimulator sim; FieldInfo left,right; bool placed; float startTime;
 void OnEnable(){sim=GetComponent<XRInteractionSimulator>();left=typeof(XRInteractionSimulator).GetField("m_LeftHandState",BindingFlags.Instance|BindingFlags.NonPublic);right=typeof(XRInteractionSimulator).GetField("m_RightHandState",BindingFlags.Instance|BindingFlags.NonPublic);placed=false;startTime=Time.time;}
 public void MoveHand(bool isLeft,Vector3 delta){var f=isLeft?left:right;if(f==null||sim==null)return;object state=f.GetValue(sim);var position=state.GetType().GetProperty("position");if(position==null)return;position.SetValue(state,(Vector3)position.GetValue(state)+delta);f.SetValue(sim,state);}
 void LateUpdate(){if(!sim||!Camera.main)return;var camera=Camera.main;var parent=camera.transform.parent;var k=Keyboard.current;
 if((!placed&&Time.time-startTime>.5f)||(k!=null&&k.homeKey.wasPressedThisFrame)){Place(left,-.22f);Place(right,.22f);placed=true;}
 float scroll=Mouse.current==null?0:Mouse.current.scroll.ReadValue().y;ApplyScroll(scroll);}
 public void ApplyScroll(float scroll){
 if(!Camera.main||Mathf.Abs(scroll)<.0001f)return;
 // Input System platforms report either normalized wheel steps or legacy 120-unit ticks.
 float ticks=Mathf.Abs(scroll)<=2?scroll:scroll/120f;
 float step=Mathf.Clamp(ticks,-3,3)*.16f;
 foreach(var grab in FindObjectsByType<XRGrabInteractable>(FindObjectsSortMode.None)){
 if(!grab.isSelected)continue;
 var near=grab.firstInteractorSelecting as UnityEngine.XR.Interaction.Toolkit.Interactors.NearFarInteractor;
 if(near==null)continue;
 var controller=near.interactionAttachController;
 if(controller==null)continue;
 var anchor=controller.GetOrCreateAnchorTransform();
 Vector3 head=Camera.main.transform.position;Vector3 ray=anchor.position-head;
 float distance=ray.magnitude;Vector3 direction=distance>.001f?ray/distance:Camera.main.transform.forward;
 controller.MoveTo(head+direction*Mathf.Clamp(distance+step,.35f,3.2f));
 break;
 }
 } void Place(FieldInfo f,float side){if(f==null)return;var cam=Camera.main.transform;Vector3 world=cam.position+cam.forward*.6f+cam.right*side-Vector3.up*.32f;Vector3 local=cam.parent?cam.parent.InverseTransformPoint(world):world;object state=f.GetValue(sim);state.GetType().GetProperty("position").SetValue(state,local);f.SetValue(sim,state);}
}
}





using UnityEngine;
using UnityEngine.InputSystem;
namespace ChemistryLab {
public class DesktopLabController : MonoBehaviour {
 public Camera view; public LabSession session; public Transform vessel; public Transform hand;
 Rigidbody held; float distance=1.3f,pitch=12; CharacterController body;
 void Awake(){body=GetComponent<CharacterController>();if(hand&&!hand.GetComponent<VirtualLabHand>())hand.gameObject.AddComponent<VirtualLabHand>();}
 void Update(){var k=Keyboard.current;var m=Mouse.current;if(k==null||m==null)return;
  if(m.rightButton.isPressed){var d=m.delta.ReadValue();transform.Rotate(0,d.x*.13f,0);pitch=Mathf.Clamp(pitch-d.y*.13f,-75,75);view.transform.localEulerAngles=new Vector3(pitch,0,0);}
  Vector3 move=transform.right*((k.dKey.isPressed?1:0)-(k.aKey.isPressed?1:0))+transform.forward*((k.wKey.isPressed?1:0)-(k.sKey.isPressed?1:0));body.Move((move.normalized*2+Vector3.down*3)*Time.deltaTime);
  if(k.eKey.wasPressedThisFrame){if(held)Drop();else if(Physics.Raycast(view.ViewportPointToRay(new Vector3(.5f,.5f)),out var hit,3)){var wearable=hit.collider.GetComponentInParent<SafetyWearable>();if(wearable)wearable.Wear();if(hit.collider.name=="Guide")session.guide=!session.guide;var item=hit.collider.GetComponentInParent<LabItem>();if(item){held=item.GetComponent<Rigidbody>();held.useGravity=false;held.linearDamping=10;var pour=held.GetComponent<LabPourable>();if(pour)pour.desktopHeld=true;distance=Mathf.Clamp(Vector3.Distance(view.transform.position,held.position),.45f,3f);}}}
  distance=Mathf.Clamp(distance+m.scroll.ReadValue().y*.002f,.45f,3f);
  if(held&&held.position.y<-.5f)Drop();
 }
 void FixedUpdate(){if(hand.GetComponent<VirtualLabHand>())hand.GetComponent<VirtualLabHand>().gripping=held!=null;hand.rotation=view.transform.rotation;if(held){Vector3 target=view.transform.position+view.transform.forward*distance;held.linearVelocity=Vector3.ClampMagnitude((target-held.position)*12,7);held.angularVelocity=Vector3.zero;hand.position=held.position;}else hand.position=view.transform.position+view.transform.forward*.55f+view.transform.right*.25f-Vector3.up*.22f;}
 public void Drop(){if(!held)return;var pour=held.GetComponent<LabPourable>();if(pour)pour.desktopHeld=false;held.useGravity=true;held.linearDamping=.1f;held=null;}
}
}





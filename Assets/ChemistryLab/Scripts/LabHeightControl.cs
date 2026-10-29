using UnityEngine;
using UnityEngine.InputSystem;
namespace ChemistryLab {
public class LabHeightControl:MonoBehaviour {
 LabSession session; float desktopOffset,xrOffset;
 void Awake(){session=GetComponent<LabSession>();}
 public void Adjust(float delta){if(session.xrMode){var cam=session.xrRig.GetComponentInChildren<Camera>();if(!cam)return;float next=Mathf.Clamp(xrOffset+delta,-.65f,.8f);cam.transform.parent.localPosition+=Vector3.up*(next-xrOffset);xrOffset=next;}else{var cam=session.desktopRig.GetComponentInChildren<Camera>();float next=Mathf.Clamp(desktopOffset+delta,-.65f,.8f);cam.transform.localPosition+=Vector3.up*(next-desktopOffset);desktopOffset=next;}}
 void Update(){var k=Keyboard.current;if(k==null)return;float move=(k.digit8Key.isPressed||k.numpad8Key.isPressed?1:0)-(k.digit2Key.isPressed||k.numpad2Key.isPressed?1:0);if(move!=0)Adjust(move*Time.deltaTime*.5f);}
}
}


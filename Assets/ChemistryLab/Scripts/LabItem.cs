using UnityEngine;
namespace ChemistryLab {
public class LabItem : MonoBehaviour {
 public string reagent, displayName;
 public Vector3 home; public Quaternion rotation;
 public bool consumed;
 void Start(){home=transform.position;rotation=transform.rotation;}
 public void Restore(){ var grab=GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();if(grab && grab.isSelected){var selectors=new System.Collections.Generic.List<UnityEngine.XR.Interaction.Toolkit.Interactors.IXRSelectInteractor>(grab.interactorsSelecting);foreach(var selector in selectors)grab.interactionManager.SelectExit(selector,grab);}consumed=false;var pour=GetComponent<LabPourable>();if(pour)pour.ResetContents(); var rb=GetComponent<Rigidbody>(); rb.linearVelocity=Vector3.zero;rb.angularVelocity=Vector3.zero; transform.SetPositionAndRotation(home,rotation); }
}
}



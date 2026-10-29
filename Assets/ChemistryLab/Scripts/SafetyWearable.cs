using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
namespace ChemistryLab {
public class SafetyWearable:MonoBehaviour {
 public string kind;
 XRGrabInteractable grab; bool worn;
 void Awake(){grab=GetComponent<XRGrabInteractable>();if(grab)grab.selectEntered.AddListener(OnSelected);}
 void OnSelected(UnityEngine.XR.Interaction.Toolkit.SelectEnterEventArgs args){Wear();}
 public void Wear(){if(worn)return;var safety=FindFirstObjectByType<LabSafety>();if(!safety)return;worn=true;safety.Equip(kind);foreach(var r in GetComponentsInChildren<Renderer>())r.enabled=false;StartCoroutine(RemoveFromStation());}
 System.Collections.IEnumerator RemoveFromStation(){yield return null;gameObject.SetActive(false);}
 void OnDestroy(){if(grab)grab.selectEntered.RemoveListener(OnSelected);}
}
}


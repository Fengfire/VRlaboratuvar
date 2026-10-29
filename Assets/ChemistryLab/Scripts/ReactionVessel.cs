using UnityEngine;
namespace ChemistryLab {
public class ReactionVessel : MonoBehaviour {
 public LabSession session;
 void OnTriggerEnter(Collider other){var item=other.GetComponentInParent<LabItem>(); if(item && !item.GetComponent<LabPourable>() && !string.IsNullOrEmpty(item.reagent))session.Add(item);}
}
}


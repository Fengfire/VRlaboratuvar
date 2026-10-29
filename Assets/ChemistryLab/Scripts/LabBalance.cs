using UnityEngine;
using System.Collections.Generic;
namespace ChemistryLab {
public class LabBalance : MonoBehaviour {
 public Vector3 center;
 void Update(){var bodies=new HashSet<Rigidbody>();float mass=0;foreach(var c in Physics.OverlapBox(center,new Vector3(.18f,.16f,.18f))){var b=c.attachedRigidbody;if(b&&bodies.Add(b))mass+=b.mass;}GetComponent<TextMesh>().text=(mass*1000).ToString("F1")+" g\nSANAL KÜTLE";}
}
}

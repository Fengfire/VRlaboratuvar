using UnityEngine;
using UnityEngine.InputSystem;
namespace ChemistryLab {
public class WorldLabBook:MonoBehaviour {
 public LabSession session; int index;
 void Update(){if(!session||session.recipes.Length==0)return;var k=Keyboard.current;if(k!=null){if(k.rightArrowKey.wasPressedThisFrame)index=(index+1)%session.recipes.Length;if(k.leftArrowKey.wasPressedThisFrame)index=(index+session.recipes.Length-1)%session.recipes.Length;}var r=session.recipes[index];GetComponent<TextMesh>().characterSize=.006f;GetComponent<TextMesh>().text="DENEY DEFTERİ    "+(index+1)+" / "+session.recipes.Length+"\n\n"+r.title+"\n\n"+r.reagentA+" + "+r.reagentB+"\n\n"+r.equation+"\n\n← / → sayfa değiştir";}
}
}



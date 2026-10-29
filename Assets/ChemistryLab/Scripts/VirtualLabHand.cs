using UnityEngine;
using UnityEngine.InputSystem;
namespace ChemistryLab {
public class VirtualLabHand:MonoBehaviour {
 public bool gripping; Transform[] fingers=new Transform[5];
 void Start(){var old=GetComponent<Renderer>();if(old)old.enabled=false;transform.localScale=Vector3.one;var mat=new Material(Shader.Find("Universal Render Pipeline/Lit"));mat.color=new Color(.22f,.55f,.69f);Part(transform,new Vector3(0,0,0),new Vector3(.085f,.035f,.09f),mat);for(int i=0;i<5;i++){var pivot=new GameObject("Finger "+i).transform;pivot.SetParent(transform,false);pivot.localPosition=i==4?new Vector3(-.052f,0,0):new Vector3((i-1.5f)*.022f,0,.035f);fingers[i]=pivot;Part(pivot,new Vector3(0,0,.032f),new Vector3(.017f,.018f,.065f),mat);}}
 void Part(Transform parent,Vector3 p,Vector3 size,Material m){var g=GameObject.CreatePrimitive(PrimitiveType.Capsule);Destroy(g.GetComponent<Collider>());g.transform.SetParent(parent,false);g.transform.localPosition=p;g.transform.localScale=size;g.GetComponent<Renderer>().sharedMaterial=m;}
 void LateUpdate(){bool point=Keyboard.current!=null&&Keyboard.current.pKey.isPressed;for(int i=0;i<5;i++)if(fingers[i])fingers[i].localRotation=Quaternion.Slerp(fingers[i].localRotation,Quaternion.Euler(gripping?75:point&&i!=0?75:0,i==4?-45:0,0),Time.deltaTime*15);}
}
}

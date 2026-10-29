using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
namespace ChemistryLab {
public static class LabVisualUpgrade {
 static Material oak,stone,steel,paper; static Transform root;
 static Material Material(string name,Color color,string texture=null){string path="Assets/ChemistryLab/"+name+".mat";var m=AssetDatabase.LoadAssetAtPath<Material>(path);if(!m){m=new Material(Shader.Find("Universal Render Pipeline/Lit"));AssetDatabase.CreateAsset(m,path);}m.color=color;if(texture!=null)m.mainTexture=AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/ChemistryLab/Textures/"+texture+".jpg");m.SetFloat("_Smoothness",.35f);return m;}
 static GameObject Box(string name,Vector3 p,Vector3 scale,Material m){var g=GameObject.CreatePrimitive(PrimitiveType.Cube);g.name=name;g.transform.SetParent(root);g.transform.position=p;g.transform.localScale=scale;g.GetComponent<Renderer>().sharedMaterial=m;return g;}
 [MenuItem("Chemistry Lab/Apply Reference Style")]
 public static void Apply(){
 var previous=GameObject.Find("Reference style additions");if(previous)Object.DestroyImmediate(previous);root=new GameObject("Reference style additions").transform;
 oak=Material("Reference oak",Color.white,"wood_table_001");stone=Material("Reference stone",new Color(.16f,.17f,.19f),"marble_01");steel=Material("Brushed steel",new Color(.4f,.45f,.48f));paper=Material("Warm plaster",new Color(.89f,.88f,.83f));
 foreach(var r in Object.FindObjectsByType<Renderer>(FindObjectsSortMode.None)){
 if(r.name=="Stone counter"||r.name=="Side worktop"||r.name=="Reagent bench")r.sharedMaterial=stone;
 if(r.name=="Experiment island")r.sharedMaterial=oak;
 if(r.name=="Floor")r.sharedMaterial=Material("Pale stone floor",new Color(.85f,.86f,.87f),"marble_01");
 if(r.name.Contains("wall"))r.sharedMaterial=paper;
 if(r.name=="Floor seam")r.gameObject.SetActive(false);
 }
 for(int side=-1;side<=1;side+=2)for(int i=0;i<4;i++){
 float x=side*4.8f,z=.2f+i*1.35f;
 Box("Oak base cabinet",new Vector3(x,.49f,z),new Vector3(1.65f,.94f,1.28f),oak);
 Box("Steel drawer pull",new Vector3(x-side*.84f,.78f,z),new Vector3(.045f,.045f,.32f),steel);
 Box("Wall cupboard",new Vector3(side*6.15f,2.6f,z),new Vector3(.55f,1,1.2f),oak);
 Box("Cupboard handle",new Vector3(side*5.85f,2.55f,z),new Vector3(.035f,.24f,.035f),steel);
 }
 for(int i=-2;i<=2;i++){
 Box("Back cabinet",new Vector3(i*1.18f,.5f,5.75f),new Vector3(1.14f,1,.9f),oak);
 Box("Back cabinet handle",new Vector3(i*1.18f,.75f,5.27f),new Vector3(.35f,.035f,.04f),steel);
 }
 Box("Back stone counter",new Vector3(0,1.04f,5.75f),new Vector3(6,.12f,1),stone);
 for(int x=-2;x<=2;x++)for(int z=-2;z<=3;z++)Box("Ceiling tile",new Vector3(x*2.55f,4.08f,z*2),new Vector3(2.53f,.08f,1.98f),paper);
 for(int side=-1;side<=1;side+=2)for(int shelf=0;shelf<2;shelf++)Box("Oak display shelf",new Vector3(side*4.2f,1.7f+shelf*.55f,6.13f),new Vector3(2.1f,.07f,.48f),oak);
 var s=Object.FindFirstObjectByType<LabSession>();
 var stand=Box("Reading stand",new Vector3(1.2f,1.35f,1.6f),new Vector3(.06f,.5f,.06f),steel);
 Box("Book backing",new Vector3(1.2f,1.65f,1.6f),new Vector3(.8f,.52f,.05f),oak);
 var page=new GameObject("Experiment book pages");page.transform.SetParent(root);page.transform.position=new Vector3(.84f,1.86f,1.56f);var text=page.AddComponent<TextMesh>();text.fontSize=64;text.characterSize=.018f;text.color=new Color(.1f,.12f,.15f);text.anchor=TextAnchor.UpperLeft;
 Box("Paper pages",new Vector3(1.2f,1.65f,1.565f),new Vector3(.76f,.48f,.015f),paper);
 var book=page.AddComponent<WorldLabBook>();book.session=s;
 foreach(var item in Object.FindObjectsByType<LabItem>(FindObjectsSortMode.None)){
 if(string.IsNullOrEmpty(item.reagent))continue;
 var existing=item.transform.Find("Physical sample");if(existing)Object.DestroyImmediate(existing.gameObject);
 var sample=new GameObject("Physical sample");sample.transform.SetParent(item.transform,false);
 var renderers=item.GetComponentsInChildren<Renderer>();var bounds=renderers[0].bounds;foreach(var r in renderers)bounds.Encapsulate(r.bounds);
 bool gas=item.reagent.Contains("(g)");bool solid=item.reagent=="Fe"||item.reagent=="NaHCO3"||item.reagent=="Nişasta"||item.reagent=="NaCl";
 var g=GameObject.CreatePrimitive(gas?PrimitiveType.Capsule:solid?PrimitiveType.Cube:PrimitiveType.Cylinder);g.name=gas?"Sealed gas cartridge":solid?"Solid sample":"Aqueous solution";Object.DestroyImmediate(g.GetComponent<Collider>());g.transform.position=bounds.center;g.transform.localScale=gas?new Vector3(.16f,.2f,.16f):new Vector3(.12f,.045f,.12f);g.transform.SetParent(sample.transform,true);
 Color c=gas?new Color(.35f,.45f,.5f):item.reagent=="Fe"?Color.gray:item.reagent=="CuSO4"?new Color(.05f,.4f,.85f):item.reagent=="I2/KI"?new Color(.4f,.16f,.04f):new Color(.85f,.9f,.9f);
 g.GetComponent<Renderer>().sharedMaterial=Material("Sample_"+item.reagent.Replace("/","_"),c);
 if(gas)foreach(var r in renderers)r.enabled=false;
 item.displayName=item.reagent+" / "+(gas?"GAZ · kapalı kartuş":solid?"KATI":"SIVI · çözelti");
 }
 EditorSceneManager.MarkSceneDirty(UnityEngine.SceneManagement.SceneManager.GetActiveScene());EditorSceneManager.SaveOpenScenes();AssetDatabase.SaveAssets();
 }
}
}

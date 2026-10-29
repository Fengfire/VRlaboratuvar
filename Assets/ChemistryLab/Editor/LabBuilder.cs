using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
namespace ChemistryLab {
public static class LabBuilder {
 static Material navy,white,teal,metal,wood; static Transform root;
 static Material Mat(string name,Color c){var path="Assets/ChemistryLab/"+name+".mat";var m=AssetDatabase.LoadAssetAtPath<Material>(path);if(!m){m=new Material(Shader.Find("Universal Render Pipeline/Lit"));AssetDatabase.CreateAsset(m,path);}m.color=c;return m;}
 static GameObject Shape(string n,Vector3 p,Vector3 s,Material m,PrimitiveType type=PrimitiveType.Cube){var g=GameObject.CreatePrimitive(type);g.name=n;g.transform.SetParent(root);g.transform.position=p;g.transform.localScale=s;g.GetComponent<Renderer>().sharedMaterial=m;return g;}
 static void Label(string text,Vector3 p,float size=0.14f){var g=new GameObject(text);g.transform.SetParent(root);g.transform.position=p;g.transform.rotation=Quaternion.identity;var t=g.AddComponent<TextMesh>();t.text=text;t.fontSize=64;t.characterSize=size*.3f;t.anchor=TextAnchor.MiddleCenter;t.color=new Color(.8f,.95f,1);}
 static GameObject Model(string path,Vector3 p,float height){var prefab=AssetDatabase.LoadAssetAtPath<GameObject>(path);if(!prefab)return null;var g=(GameObject)PrefabUtility.InstantiatePrefab(prefab);g.transform.SetParent(root);g.transform.position=Vector3.zero;var rs=g.GetComponentsInChildren<Renderer>();foreach(var renderer in rs){var mats=renderer.sharedMaterials;for(int j=0;j<mats.Length;j++){var source=mats[j];if(source && !source.shader.name.StartsWith("Universal Render Pipeline")){Color tint=source.HasProperty("_Color")?source.color:new Color(.55f,.7f,.73f);mats[j]=Mat("Imported_"+source.name.Replace("/","_"),tint);}}renderer.sharedMaterials=mats;}var b=new Bounds();bool init=false;foreach(var r in rs){if(!init){b=r.bounds;init=true;}else b.Encapsulate(r.bounds);}g.transform.localScale*=height/Mathf.Max(b.size.y,.01f);b=new Bounds();init=false;foreach(var r in rs){if(!init){b=r.bounds;init=true;}else b.Encapsulate(r.bounds);}g.transform.position=p-new Vector3(b.center.x,b.min.y,b.center.z);return g;}
 static void Grab(GameObject g,string id,string name){var b=new Bounds(g.transform.position,Vector3.zero);foreach(var r in g.GetComponentsInChildren<Renderer>())b.Encapsulate(r.bounds);foreach(var c in g.GetComponentsInChildren<Collider>())Object.DestroyImmediate(c);var col=g.AddComponent<BoxCollider>();col.center=g.transform.InverseTransformPoint(b.center);col.size=new Vector3(b.size.x/g.transform.lossyScale.x,b.size.y/g.transform.lossyScale.y,b.size.z/g.transform.lossyScale.z);var rb=g.AddComponent<Rigidbody>();rb.mass=.25f;rb.collisionDetectionMode=CollisionDetectionMode.ContinuousDynamic;var item=g.AddComponent<LabItem>();item.reagent=id;item.displayName=name;g.AddComponent<XRGrabInteractable>();}
 [MenuItem("Chemistry Lab/Build Prototype")]
 public static void Build(){
 var old=UnityEngine.SceneManagement.SceneManager.GetActiveScene();if(old.isDirty)EditorSceneManager.SaveScene(old);
 var scene=EditorSceneManager.NewScene(NewSceneSetup.EmptyScene,NewSceneMode.Single);
 root=new GameObject("ELEMENT • Chemistry laboratory").transform;
 navy=Mat("Graphite",new Color(.035f,.075f,.12f));white=Mat("Porcelain",new Color(.75f,.82f,.84f));teal=Mat("Teal",new Color(.04f,.55f,.58f));metal=Mat("Steel",new Color(.24f,.32f,.38f));wood=Mat("Oak",new Color(.43f,.29f,.16f));
 Shape("Floor",new Vector3(0,-.12f,1),new Vector3(13,.2f,12),white);
 for(int x=-6;x<=6;x++)Shape("Floor seam",new Vector3(x,0,1),new Vector3(.012f,.005f,12),metal);
 for(int z=-5;z<=7;z++)Shape("Floor seam",new Vector3(0,0,z),new Vector3(13,.005f,.012f),metal);
 Shape("Back wall",new Vector3(0,2,6.5f),new Vector3(13,4,.2f),white);
 Shape("Left wall",new Vector3(-6.5f,2,1),new Vector3(.2f,4,11),white);
 Shape("Right wall",new Vector3(6.5f,2,1),new Vector3(.2f,4,11),white);
 Shape("Back accent",new Vector3(0,2.6f,6.36f),new Vector3(10,1.2f,.08f),navy);
 Label("E L E M E N T",new Vector3(0,2.83f,6.28f),.32f);Label("SANAL KİMYA / KEŞFET • GÖZLEMLE • ÖĞREN",new Vector3(0,2.35f,6.27f),.075f);
 for(int side=-1;side<=1;side+=2){Shape("Side worktop",new Vector3(side*4.7f,1,2.2f),new Vector3(2,.12f,6),navy);for(int i=0;i<3;i++){Model("Assets/Package04_Free_Kitchen/FreeKitchen/Prefabs/Version01/FreeCabinet01.prefab",new Vector3(side*4.7f,.02f,i*2),.92f);Model("Assets/Package04_Free_Kitchen/FreeKitchen/Prefabs/Version01/FreeChair.prefab",new Vector3(side*3.2f,.02f,i*2),.85f);}}
 Shape("Experiment island",new Vector3(0,.5f,1.8f),new Vector3(3.8f,1,1.7f),wood);Shape("Stone counter",new Vector3(0,1.04f,1.8f),new Vector3(4,.14f,1.9f),navy);Shape("Teal band",new Vector3(0,.8f,.94f),new Vector3(3.8f,.08f,.03f),teal);
 Label("01 / REAKSİYON İSTASYONU",new Vector3(0,.62f,.91f),.075f);
 var session=new GameObject("Lab session").AddComponent<LabSession>();
 var vessel=Shape("Reaction chamber",new Vector3(0,1.26f,1.7f),new Vector3(.55f,.15f,.55f),metal,PrimitiveType.Cylinder);
 // Open chamber: rim segments leave its contents visible from above.
 for(int i=0;i<16;i++){float a=i*Mathf.PI/8;Shape("Chamber rim",new Vector3(Mathf.Sin(a)*.27f,1.47f,1.7f+Mathf.Cos(a)*.27f),new Vector3(.07f,.35f,.07f),teal);}
 var trigger=new GameObject("Reagent receiving volume");trigger.transform.position=new Vector3(0,1.65f,1.7f);var tc=trigger.AddComponent<BoxCollider>();tc.isTrigger=true;tc.size=new Vector3(.65f,.5f,.65f);trigger.AddComponent<ReactionVessel>().session=session;
 session.liquid=Shape("Liquid",new Vector3(0,1.44f,1.7f),new Vector3(.46f,.025f,.46f),Mat("Liquid",Color.cyan),PrimitiveType.Cylinder).GetComponent<Renderer>();
 session.solid=Shape("Precipitate",new Vector3(0,1.48f,1.7f),new Vector3(.28f,.025f,.28f),Mat("Product",Color.white),PrimitiveType.Cylinder).GetComponent<Renderer>();session.solid.gameObject.SetActive(false);
 var fx=new GameObject("Gas bubbles");fx.transform.position=new Vector3(0,1.5f,1.7f);session.bubbles=fx.AddComponent<ParticleSystem>();var main=session.bubbles.main;main.playOnAwake=false;main.duration=4;main.loop=false;main.startLifetime=1.3f;main.startSpeed=.3f;main.startSize=.035f;main.startColor=new Color(.7f,.9f,1,.5f);var shape=session.bubbles.shape;shape.shapeType=ParticleSystemShapeType.Cone;shape.angle=15;shape.radius=.17f;fx.transform.rotation=Quaternion.Euler(-90,0,0);fx.GetComponent<ParticleSystemRenderer>().sharedMaterial=Mat("Bubble",new Color(.65f,.85f,1));session.bubbles.Stop(true,ParticleSystemStopBehavior.StopEmittingAndClear);
 session.flash=new GameObject("Reaction flash").AddComponent<Light>();session.flash.transform.position=new Vector3(0,2,1.7f);session.flash.range=4;session.flash.intensity=0;
 string[] ids={"CH3COOH","NaHCO3","HCl(aq)","NaOH","Fe","CuSO4","CaCl2","Na2CO3","I2/KI","Nişasta","H2(g)","Cl2(g)","H2O","NaCl"};
 for(int i=0;i<ids.Length;i++){float x=-1.65f+(i%7)*.55f;float z=3.6f+(i/7)*.6f;var p=new Vector3(x,1.12f,z);var g=Model("Assets/FreeLabAssets/Prefabs/Flask_0"+(i%6+1)+".prefab",p,.3f);if(g){Grab(g,ids[i],ids[i]);g.name="Reagent / "+ids[i];}Label(ids[i],new Vector3(x,1.18f,z-.22f),.035f);}
 Shape("Reagent bench",new Vector3(0,1.02f,3.9f),new Vector3(4,.16f,1.5f),white);for(int i=-1;i<=1;i+=2)Shape("Bench leg",new Vector3(i*1.7f,.5f,3.9f),new Vector3(.12f,1,.8f),metal);
 for(int i=0;i<4;i++){var g=Model("Assets/FreeLabAssets/Prefabs/Flask_0"+(i+1)+".prefab",new Vector3(-4.8f,1.08f,i),.35f);if(g)Grab(g,"","Cam kap");}
 var pip=Model("Assets/FreeLabAssets/Prefabs/Pipette.prefab",new Vector3(1.4f,1.12f,1.8f),.25f);if(pip)Grab(pip,"","Pipet");
 var spat=Shape("Spatula",new Vector3(1,1.15f,1.7f),new Vector3(.04f,.025f,.4f),metal);Grab(spat,"","Spatül");
 Shape("Balance",new Vector3(-1.3f,1.18f,1.7f),new Vector3(.55f,.12f,.5f),navy);Shape("Balance plate",new Vector3(-1.3f,1.27f,1.7f),new Vector3(.3f,.025f,.3f),metal,PrimitiveType.Cylinder);var scaleText=new GameObject("Balance display");scaleText.transform.position=new Vector3(-1.3f,1.25f,1.42f);var readout=scaleText.AddComponent<TextMesh>();readout.fontSize=48;readout.characterSize=.018f;readout.anchor=TextAnchor.MiddleCenter;readout.color=Color.cyan;var balance=scaleText.AddComponent<LabBalance>();balance.center=new Vector3(-1.3f,1.4f,1.7f);
 for(int i=0;i<5;i++){var tube=Shape("Test tube",new Vector3(4.5f+i*.12f,1.3f,2),new Vector3(.07f,.22f,.07f),teal,PrimitiveType.Cylinder);Grab(tube,"","Deney tüpü");}
 Label("GÜVENLİK\nGözlük • Önlük • Kapalı ayakkabı\nGaz deneyleri yalnızca sanal hücrede",new Vector3(-4,2.5f,6.25f),.07f);
 Shape("Guide",new Vector3(.8f,1.15f,1.15f),new Vector3(.38f,.06f,.28f),teal);Label("DEFTER / F1",new Vector3(.8f,1.2f,.99f),.025f);var sun=new GameObject("Sun").AddComponent<Light>();sun.type=LightType.Directional;sun.intensity=1.2f;sun.transform.rotation=Quaternion.Euler(45,-25,0);RenderSettings.ambientLight=new Color(.55f,.65f,.75f);
 for(int i=-1;i<=1;i++){Shape("Ceiling light",new Vector3(i*3,3.8f,1.8f),new Vector3(1.8f,.05f,.4f),white);var l=new GameObject("Area fill").AddComponent<Light>();l.type=LightType.Point;l.transform.position=new Vector3(i*3,3.5f,1.8f);l.intensity=2;l.range=7;}
 var rig=PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Samples/XR Interaction Toolkit/3.5.1/Starter Assets/Prefabs/XR Origin (XR Rig).prefab")) as GameObject;rig.transform.position=new Vector3(0,0,-1);session.xrRig=rig;rig.SetActive(false);
 session.simulator=PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Samples/XR Interaction Toolkit/3.5.1/XR Interaction Simulator/XR Interaction Simulator.prefab")) as GameObject;session.simulator.SetActive(false);
 new GameObject("XR Interaction Manager").AddComponent<UnityEngine.XR.Interaction.Toolkit.XRInteractionManager>();
 var player=new GameObject("Desktop explorer");player.transform.position=new Vector3(0,0,-2);var cc=player.AddComponent<CharacterController>();cc.height=1.75f;cc.center=new Vector3(0,.88f,0);cc.radius=.22f;var cam=new GameObject("Desktop camera").AddComponent<Camera>();cam.tag="MainCamera";cam.transform.SetParent(player.transform);cam.transform.localPosition=new Vector3(0,1.65f,0);cam.nearClipPlane=.03f;cam.AddAudioListener();var ctrl=player.AddComponent<DesktopLabController>();cam.transform.localRotation=Quaternion.Euler(12,0,0);ctrl.view=cam;ctrl.session=session;ctrl.vessel=trigger.transform;ctrl.hand=Shape("Virtual hand",Vector3.zero,new Vector3(.09f,.08f,.14f),teal,PrimitiveType.Sphere).transform;Object.DestroyImmediate(ctrl.hand.GetComponent<Collider>());ctrl.hand.SetParent(player.transform);session.desktopRig=player;
 session.recipes=new ReactionDefinition[]{
 Recipe("01",ids[0],ids[1],"Gaz çıkışı","CH3COOH + NaHCO3 → CH3COONa + H2O + CO2", "Karbondioksit kabarcıkları oluşur. Sodyum asetat çözeltide kalır.",Color.cyan,true,false,false),
 Recipe("02",ids[2],ids[3],"Nötrleşme","HCl + NaOH → NaCl + H2O","Tuz ve su oluşur; ısı açığa çıkar. Yeşil renk sanal pH göstergesidir, ürünün doğal rengi değildir.",Color.green,false,false,false),
 Recipe("03",ids[4],ids[5],"Demir ve bakır","Fe + CuSO4 → FeSO4 + Cu","Demir yükseltgenir; bakır metali çöker. Çözelti soluk yeşile döner; süreç hızlandırılmıştır.",new Color(.5f,.75f,.5f),false,true,false),
 Recipe("04",ids[6],ids[7],"Beyaz çökelek","CaCl2 + Na2CO3 → CaCO3↓ + 2NaCl","Suda az çözünen kalsiyum karbonat beyaz katı olarak ayrılır.",Color.white,false,true,false),
 Recipe("05",ids[8],ids[9],"Nişasta göstergesi","Nişasta + poliiodür ⇌ mavi-siyah kompleks","İyot/iyodür çözeltisi nişastayla koyu kompleks oluşturur. Bu şematik kompleksleşmedir; basit stokiyometrik denklem değildir.",new Color(.045f,.04f,.18f),false,false,false),
 Recipe("06",ids[10],ids[11],"Fotokimyasal birleşme","H2(g) + Cl2(g) → 2HCl(g) [ışık]","Hidrojen klorür renksiz, aşındırıcı bir gazdır. Işık zincir tepkimesini başlatır. Yalnızca kapalı sanal hücre gösterimi; gerçek uygulama yapmayın.",new Color(.75f,.85f,.9f),false,false,true)};
 EditorSceneManager.SaveScene(scene,"Assets/ChemistryLab/ChemistryLab.unity");AssetDatabase.SaveAssets();
 }
 static ReactionDefinition Recipe(string id,string a,string b,string title,string eq,string info,Color c,bool gas,bool solid,bool light){string p="Assets/ChemistryLab/Reaction"+id+".asset";var r=AssetDatabase.LoadAssetAtPath<ReactionDefinition>(p);if(!r){r=ScriptableObject.CreateInstance<ReactionDefinition>();AssetDatabase.CreateAsset(r,p);}r.reagentA=a;r.reagentB=b;r.title=title;r.equation=eq;r.explanation=info;r.color=c;r.gas=gas;r.precipitate=solid;r.lightRequired=light;EditorUtility.SetDirty(r);return r;}
 static void AddAudioListener(this Camera c){c.gameObject.AddComponent<AudioListener>();}
}
}




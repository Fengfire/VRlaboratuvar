using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
namespace ChemistryLab {
public static class PeriodicWall {
 [MenuItem("Chemistry Lab/Build Periodic Wall")]
 public static void Build(){var old=GameObject.Find("Periodic table display");if(old)Object.DestroyImmediate(old);var root=new GameObject("Periodic table display");root.transform.position=new Vector3(0,2.7f,6.18f);
 string[] rows={"H . . . . . . . . . . . . . . . . He","Li Be . . . . . . . . . . B C N O F Ne","Na Mg . . . . . . . . . . Al Si P S Cl Ar","K Ca Sc Ti V Cr Mn Fe Co Ni Cu Zn Ga Ge As Se Br Kr","Rb Sr Y Zr Nb Mo Tc Ru Rh Pd Ag Cd In Sn Sb Te I Xe","Cs Ba La Hf Ta W Re Os Ir Pt Au Hg Tl Pb Bi Po At Rn","Fr Ra Ac Rf Db Sg Bh Hs Mt Ds Rg Cn Nh Fl Mc Lv Ts Og",". . . Ce Pr Nd Pm Sm Eu Gd Tb Dy Ho Er Tm Yb Lu .",". . . Th Pa U Np Pu Am Cm Bk Cf Es Fm Md No Lr ."};
 string[] atomic="H He Li Be B C N O F Ne Na Mg Al Si P S Cl Ar K Ca Sc Ti V Cr Mn Fe Co Ni Cu Zn Ga Ge As Se Br Kr Rb Sr Y Zr Nb Mo Tc Ru Rh Pd Ag Cd In Sn Sb Te I Xe Cs Ba La Ce Pr Nd Pm Sm Eu Gd Tb Dy Ho Er Tm Yb Lu Hf Ta W Re Os Ir Pt Au Hg Tl Pb Bi Po At Rn Fr Ra Ac Th Pa U Np Pu Am Cm Bk Cf Es Fm Md No Lr Rf Db Sg Bh Hs Mt Ds Rg Cn Nh Fl Mc Lv Ts Og".Split(' ');
 var mats=new Material[4];Color[] colors={new Color(.1f,.39f,.42f),new Color(.25f,.37f,.58f),new Color(.57f,.24f,.28f),new Color(.58f,.4f,.17f)};for(int c=0;c<4;c++){string path="Assets/ChemistryLab/PeriodicGroup"+c+".mat";mats[c]=AssetDatabase.LoadAssetAtPath<Material>(path);if(!mats[c]){mats[c]=new Material(Shader.Find("Universal Render Pipeline/Unlit"));AssetDatabase.CreateAsset(mats[c],path);}mats[c].color=colors[c];}
 var backing=GameObject.CreatePrimitive(PrimitiveType.Cube);backing.name="Oak frame";backing.transform.SetParent(root.transform,false);backing.transform.localPosition=new Vector3(0,-.17f,.05f);backing.transform.localScale=new Vector3(4.1f,2.25f,.09f);backing.GetComponent<Renderer>().sharedMaterial=AssetDatabase.LoadAssetAtPath<Material>("Assets/ChemistryLab/Reference oak.mat");
 for(int r=0;r<rows.Length;r++){var cells=rows[r].Split(' ');for(int c=0;c<cells.Length;c++){string symbol=cells[c];if(symbol==".")continue;var cell=GameObject.CreatePrimitive(PrimitiveType.Cube);cell.name=symbol;cell.transform.SetParent(root.transform,false);cell.transform.localPosition=new Vector3((c-8.5f)*.21f,.7f-r*.21f,0);cell.transform.localScale=new Vector3(.197f,.194f,.025f);cell.GetComponent<Renderer>().sharedMaterial=mats[c<2?1:c<12?2:c<17?3:0];var label=new GameObject("Element label");label.transform.SetParent(root.transform,false);label.transform.localPosition=cell.transform.localPosition+Vector3.back*.02f;var t=label.AddComponent<TextMesh>();t.text=(System.Array.IndexOf(atomic,symbol)+1)+"\n"+symbol;t.fontSize=48;t.characterSize=.012f;t.anchor=TextAnchor.MiddleCenter;t.alignment=TextAlignment.Center;t.color=Color.white;}}
 foreach(var t in Object.FindObjectsByType<TextMesh>(FindObjectsSortMode.None))if(t.text=="E L E M E N T"||t.text.StartsWith("SANAL KİMYA /"))t.gameObject.SetActive(false);
 EditorSceneManager.MarkSceneDirty(root.scene);EditorSceneManager.SaveOpenScenes();AssetDatabase.SaveAssets();}
}
}


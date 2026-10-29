using UnityEngine;
using UnityEngine.UI;
using System.Collections;
namespace ChemistryLab {
public class ReactionResultBoard:MonoBehaviour {
 GameObject board; Transform panel; Text heading,equation,description; Coroutine rise;
 void Create(){if(board)return;board=new GameObject("Reaction result • rising panel",typeof(Canvas));var canvas=board.GetComponent<Canvas>();canvas.renderMode=RenderMode.WorldSpace;panel=board.transform;var rt=board.GetComponent<RectTransform>();rt.sizeDelta=new Vector2(640,440);panel.localScale=Vector3.one*.0015f;
 var background=new GameObject("Background",typeof(RectTransform),typeof(Image));background.transform.SetParent(panel,false);var rect=background.GetComponent<RectTransform>();rect.anchorMin=Vector2.zero;rect.anchorMax=Vector2.one;rect.offsetMin=rect.offsetMax=Vector2.zero;background.GetComponent<Image>().color=new Color(.025f,.065f,.085f,.97f);
 heading=Label("Title",new Vector2(28,-25),new Vector2(584,84),28,new Color(.4f,.93f,.8f));equation=Label("Equation",new Vector2(28,-110),new Vector2(584,75),27,Color.white);description=Label("Properties",new Vector2(28,-210),new Vector2(584,195),25,new Color(.85f,.9f,.94f));
 }
 Text Label(string n,Vector2 pos,Vector2 size,int font,Color color){var g=new GameObject(n,typeof(RectTransform),typeof(Text));g.transform.SetParent(panel,false);var r=g.GetComponent<RectTransform>();r.anchorMin=r.anchorMax=new Vector2(0,1);r.pivot=new Vector2(0,1);r.anchoredPosition=pos;r.sizeDelta=size;var t=g.GetComponent<Text>();t.font=Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");t.fontSize=font;t.color=color;t.horizontalOverflow=HorizontalWrapMode.Wrap;t.verticalOverflow=VerticalWrapMode.Truncate;return t;}
 public void Show(ReactionDefinition reaction){Create();if(rise!=null)StopCoroutine(rise);heading.text="KEŞİF TAMAMLANDI\n"+reaction.title;equation.text=reaction.equation;description.text=reaction.explanation;board.SetActive(true);var camera=Camera.main.transform;Vector3 target=camera.position+camera.forward*1.55f+camera.right*.85f-Vector3.up*.08f;panel.rotation=camera.rotation;rise=StartCoroutine(Rise(target));}
 IEnumerator Rise(Vector3 target){Vector3 start=target-Vector3.up*1.15f;float t=0;while(t<.85f){t+=Time.deltaTime;float u=Mathf.SmoothStep(0,1,t/.85f);panel.position=Vector3.Lerp(start,target,u);yield return null;}panel.position=target;rise=null;}
 public void Hide(){if(rise!=null)StopCoroutine(rise);rise=null;if(board)board.SetActive(false);}
 void OnDestroy(){if(board)Destroy(board);}
}
}


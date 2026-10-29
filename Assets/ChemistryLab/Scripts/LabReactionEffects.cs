using UnityEngine;
using System.Collections;
namespace ChemistryLab {
public class LabReactionEffects:MonoBehaviour {
 LabSession session; Coroutine animation; Vector3 solidScale; Material liquidMaterial;
 void Awake(){session=GetComponent<LabSession>();solidScale=session.solid.transform.localScale;liquidMaterial=session.liquid.material;}
 public void Show(ReactionDefinition r){var board=GetComponent<ReactionResultBoard>();if(board)board.Show(r);if(r.lightRequired){var burst=GetComponent<LabExplosion>();if(burst)burst.Play(session.liquid.transform.position+Vector3.up*.12f);}if(animation!=null)StopCoroutine(animation);animation=StartCoroutine(Animate(r));}
 IEnumerator Animate(ReactionDefinition r){Color before=liquidMaterial.color;session.solid.transform.localScale=solidScale*.05f;session.solid.gameObject.SetActive(r.precipitate);float t=0;while(t<3){t+=Time.deltaTime;float u=Mathf.SmoothStep(0,1,t/3);liquidMaterial.color=Color.Lerp(before,r.color,u);if(r.precipitate)session.solid.transform.localScale=solidScale*u;if(r.lightRequired)session.flash.intensity=Mathf.Exp(-t*3)*5;yield return null;}animation=null;}
 public void Clear(){var board=GetComponent<ReactionResultBoard>();if(board)board.Hide();var burst=GetComponent<LabExplosion>();if(burst)burst.Clear();if(animation!=null)StopCoroutine(animation);animation=null;session.solid.transform.localScale=solidScale;}
}
}


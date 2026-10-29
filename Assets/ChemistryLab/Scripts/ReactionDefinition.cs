using UnityEngine;
namespace ChemistryLab {
[CreateAssetMenu(menuName="Chemistry/Reaction")]
public class ReactionDefinition : ScriptableObject {
 public string reagentA, reagentB, title, equation;
 [TextArea] public string explanation;
 public Color color=Color.white;
 public bool gas, precipitate, lightRequired;
 public bool Matches(string a,string b) => (a==reagentA && b==reagentB)||(a==reagentB && b==reagentA);
}
}

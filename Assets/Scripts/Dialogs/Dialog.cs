using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Line
{
    [TextArea(5, 7)]
    public string text;

    public bool questionTuto;

}

[CreateAssetMenu(fileName = "New Monologue", menuName = "Monologue")]
public class Dialog : ScriptableObject
{
    public Line[] lines;
}
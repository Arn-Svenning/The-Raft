using UnityEngine;

[CreateAssetMenu(fileName = "New Character Body", menuName = "Character Body")]
public class SOCharacterBody : ScriptableObject
{
    // ~~ 1. Holds details about the full character body

    public BodyPart[] characterBodyParts;
}
[System.Serializable]
public class BodyPart
{
    public string bodyPartName;
    public SOBodyPart bodyPart;
}

using System.Collections.Generic;

[System.Serializable]
public class Reply
{
    public string reply;
    public Replic nextReplic;
    public List<ConditionForDialogue> conditions = new();
}

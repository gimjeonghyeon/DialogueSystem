
public class Dialogue
{
    public enum Type
    {
        NORMAL,
        DELAY,
    }
    
    public string speaker;
    public string text;
    public string[] delayText;
    public Type type;

    public Dialogue(string speaker, string text)
    {
        this.speaker = speaker;
        this.text = text;
        
        type = Type.NORMAL;
    }

    public Dialogue(string speaker,string[] delayText)
    {
        this.speaker = speaker;
        this.delayText = delayText;
        
        type = Type.DELAY;
    }
}

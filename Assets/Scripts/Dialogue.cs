
public class Dialogue
{
    public string speaker;
    public string text;
    public string[] delayTexts;
    public int type;

    public Dialogue(string speaker, string text)
    {
        this.speaker = speaker;
        this.text = text;
        
        type = 1;
    }

    private Dialogue(string speaker,string[] delayTexts)
    {
        this.speaker = speaker;
        this.delayTexts = delayTexts;
        
        type = 2;
    }

}

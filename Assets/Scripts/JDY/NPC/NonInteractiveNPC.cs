public class NonInteractiveNPC : Interaction
{
    //대화창만 띄울 NPC들
    protected override void Start()
    {
        base.Start();
        isNonInteractive = true;
    }
}

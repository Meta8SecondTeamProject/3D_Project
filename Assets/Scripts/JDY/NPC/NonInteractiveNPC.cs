public class NonInteractiveNPC : Interaction
{
    //��ȭâ�� ��� NPC��
    protected override void Start()
    {
        base.Start();
        isNonInteractive = true;
    }
}

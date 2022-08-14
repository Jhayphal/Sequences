namespace Sequences
{
  public class SequenceContext
  {
    public readonly SequenceNode Head;
    public readonly string Value;

    public int Position { get; set; } = -1;

    public SequenceContext(SequenceNode head, string value)
    {
      Head = head;
      Value = value;
    }
  }
}
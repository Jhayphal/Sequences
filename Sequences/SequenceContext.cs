namespace Sequences
{
  public class SequenceContext
  {
    public readonly ISequenceNode Head;
    public readonly string Value;

    public int Position { get; set; } = -1;

    public SequenceContext(ISequenceNode head, string value)
    {
      Head = head;
      Value = value;
    }
  }
}
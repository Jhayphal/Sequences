using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Sequences
{
  [DebuggerDisplay("{Value}")]
  public class SequenceNode : ISequenceNode
  {
    private readonly List<ISequenceNode> children = new();

    public readonly string Value;
    public virtual bool Success => Position == Value.Length - 1;
    public virtual bool Verified { get; protected set; }
    public ReadOnlyCollection<ISequenceNode> Next => new(children);

    public int Position { get; set; }

    public event EventHandler<SequenceContext>? OnVerified;

    public SequenceNode(string value)
    {
      Value = value;

      Reset();
    }

    public virtual void AddNext(ISequenceNode next)
    {
      if (children.Contains(next))
        throw new ArgumentException($"Already exists after '{Value}'", nameof(next));

      children.Add(next);
    }

    public void MakeVerified(SequenceContext context)
    {
      if (Verified)
        throw new InvalidOperationException(nameof(Verified));

      OnVerified?.Invoke(this, context);
    }

    public void Reset()
    {
      Position = -1;
    }
  }
}
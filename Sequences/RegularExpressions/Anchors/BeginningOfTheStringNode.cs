using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequences.RegularExpressions.Anchors
{
  public abstract class AnchorNode : ISequenceNode
  {
    private readonly List<ISequenceNode> children = new();

    public readonly string Value;
    public virtual bool Success => Position == Value.Length - 1;
    public virtual bool Verified { get; protected set; }
    public ReadOnlyCollection<ISequenceNode> Next => new(children);

    public int Position { get; set; }

    public event EventHandler<SequenceContext>? OnVerified;

    public AnchorNode()
    {
      Value = string.Empty;

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

  /// <summary>
  /// "^"
  /// </summary>
  public class BeginningOfTheLineNode : AnchorNode { }

  /// <summary>
  /// "$"
  /// </summary>
  public class EndOfTheLineNode : AnchorNode { }

  /// <summary>
  /// "\A"
  /// </summary>
  public class BeginningOfTheStringNode : AnchorNode { }

  /// <summary>
  /// "\Z"
  /// </summary>
  public class SoftEndOfTheStringNode : AnchorNode { }

  /// <summary>
  /// "\z"
  /// </summary>
  public class HardEndOfTheStringNode : AnchorNode { }

  /// <summary>
  /// "\G"
  /// </summary>
  public class ContinuityNode : AnchorNode { }

  /// <summary>
  /// "\b"
  /// </summary>
  public class BoundaryNode : AnchorNode { }

  /// <summary>
  /// "\B"
  /// </summary>
  public class NonBoundaryNode : AnchorNode { }
}

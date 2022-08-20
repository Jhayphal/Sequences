using System.Collections.ObjectModel;

namespace Sequences
{
  public interface ISequenceNode
  {
    ReadOnlyCollection<ISequenceNode> Next { get; }
    int Position { get; set; }
    bool Success { get; }
    bool Verified { get; }

    event EventHandler<SequenceContext>? OnVerified;

    void AddNext(ISequenceNode next);
    void MakeVerified(SequenceContext context);
    void Reset();
  }
}
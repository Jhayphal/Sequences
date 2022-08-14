namespace Sequences
{
  public class SequenceVerifier
  {
    public enum VerifierState
    {
      None,
      Partial,
      Complete
    }

    public readonly SequenceContext Context;

    public SequenceVerifier(SequenceNode head, string value)
    {
      Context = new SequenceContext(head, value);
    }

    public VerifierState Verify()
    {
      List<SequenceNode> activeNodes = new();
      List<SequenceNode> nextGeneration = new();
      
      activeNodes.Add(Context.Head);

      bool wasSpace = true;

      while (++Context.Position < Context.Value.Length)
      {
        var contextItem = Context.Value[Context.Position];

        if (char.IsWhiteSpace(contextItem))
        {
          if (!wasSpace)
          {
            foreach (var child in activeNodes)
            {
              if (child.Success)
              {
                child.MakeVerified(Context);

                nextGeneration.AddRange(child.Next);
              }
              else
              {
                child.Reset();
              }
            }

            activeNodes.Clear();
            activeNodes.AddRange(nextGeneration);
            nextGeneration.Clear();

            if (activeNodes.Count == 0)
              return VerifierState.Complete;
          }

          wasSpace = true;
        }
        else
        {
          wasSpace = false;

          foreach (var node in activeNodes)
          {
            if (!node.Success && Verify(node, contextItem))
            {
              nextGeneration.Add(node);
            }
            else
            {
              node.Reset();
            }
          }

          activeNodes.Clear();
          activeNodes.AddRange(nextGeneration);
          nextGeneration.Clear();
        }

        if (Context.Position < Context.Value.Length - 1 && activeNodes.Count == 0)
          return VerifierState.None;
      }

      if (!wasSpace)
      {
        foreach (var child in activeNodes)
        {
          if (child.Success)
          {
            child.MakeVerified(Context);

            nextGeneration.AddRange(child.Next);
          }
        }

        activeNodes.Clear();
        activeNodes.AddRange(nextGeneration);
        nextGeneration.Clear();
      }

      var result = activeNodes.Count == 0
        ? VerifierState.Complete
        : VerifierState.Partial;

      activeNodes.Clear();

      return result;
    }

    private bool Verify(SequenceNode current, char contextItem)
      => contextItem == current.Value[++current.Position];
  }
}
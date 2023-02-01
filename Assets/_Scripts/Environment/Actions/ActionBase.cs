using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionBase
{
    public abstract void Undo();

    public abstract bool Validate(Board board);

    public abstract void Execute();
}

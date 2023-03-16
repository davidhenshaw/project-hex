using DG.Tweening;
using metakazz.Hex;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BoardFollower : EntityController
{
    [FormerlySerializedAs("toFollow")]
    public GridEntity Leader;
    EntityController _leaderController;

    public override event Action<ActionBase> NextActionCalculated;

    protected override void Awake()
    {
        base.Awake();
        if(Leader != null)
        {
            _leaderController = Leader.GetComponent<EntityController>();
            _leaderController.NextActionCalculated += OnLeaderCalculateAction;
        }
    }

    private void OnLeaderCalculateAction(ActionBase action)
    {
        NextAction = action.GetMimickAction(this);
    }
    
    public void SetLeader(GridEntity leader)
    {
        Leader = leader;
        _leaderController = leader.GetComponent<EntityController>();
    }

    public void ClearLeader()
    {
        Leader = null;
        _leaderController = null;
    }

    public override ActionBase CalculateNextAction()
    {
        if (_leaderController == null)
            return null;

        if (_leaderController.NextAction == null)
            return null;

        NextAction = _leaderController.NextAction.GetMimickAction(this);
        NextActionCalculated?.Invoke(NextAction);
        return NextAction;
    }
}

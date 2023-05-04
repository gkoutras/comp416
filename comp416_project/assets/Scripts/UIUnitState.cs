using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUnitState : MonoBehaviour
{
    public Image stateBG;
    public Image stateImage;

    public Sprite idleState;
    public Sprite gatherResourceState;
    public Sprite constructState;
    public Sprite attackState;

    public void StateChange(UnitState state)
    {
        stateBG.enabled = true;
        stateImage.enabled = true;

        switch (state)
        {
            case UnitState.Idle:
                {
                    stateImage.sprite = idleState;
                    break;
                }
            case UnitState.GatherResource:
                {
                    stateImage.sprite = gatherResourceState;
                    break;
                }
            case UnitState.Construct:
                {
                    stateImage.sprite = constructState;
                    break;
                }
            case UnitState.Attack:
                {
                    stateImage.sprite = attackState;
                    break;
                }
            default:
                {
                    stateBG.enabled = false;
                    stateImage.enabled = false;
                    break;
                }
        }
    }
}

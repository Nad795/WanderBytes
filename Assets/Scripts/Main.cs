using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { FreeRoam, Dialog, Battle }
public class Main : MonoBehaviour
{
    [SerializeField] Player player;

    State state;

    private void Start()
    {
        DialogManager.Instance.OnShowDialog += () =>
        {
            state = State.Dialog;
        };

        DialogManager.Instance.OnHideDialog += () =>
        {
            if (state == State.Dialog)
            {
                state = State.FreeRoam;
            }
        };
    }

    private void Update()
    {
        if (state == State.FreeRoam)
        {
            player.HandleUpdate();

        }
        else if (state == State.Dialog)
        {
            DialogManager.Instance.HandleUpdate();
        }
        else if (state == State.Battle)
        {

        }
    }
}
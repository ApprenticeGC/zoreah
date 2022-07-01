namespace GiantCroissant.MoYraq.Game
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Cysharp.Threading.Tasks;
    using UnityEngine;

    public class Unit : MonoBehaviour, ISelectable
    {
        public int belongToGroup;

        public GameObject noPickableGO;
        
        private ManagerBattle _managerBattle;

        private bool _acted;
        private int _didAction;
        
        private void Start()
        {
            // _managerBattle = FindObjectOfType<ManagerBattle>();
        }

        public void SetManagerBattle(ManagerBattle managerBattle)
        {
            _managerBattle = managerBattle;
        }

        public async UniTask<int> DoActionAsync()
        {
            // Debug.Log($"Unit - DoActionAsync - starts");
            var asyncTasks = new List<UniTask>
            {
                // DoActionASync_DoNothing(),
                DoActionAsync_Attack(),
                DoActionAsync_EndTurn()
            };

            // var result = await _managerBattle.RequestShowingSelectionHud(this);
            // _didAction = result;
            
            var result = await UniTask.WhenAny(asyncTasks);
            Debug.Log($"Unit - DoActionAsync - end");

            return result;
        }

        // private async UniTask DoActionASync_DoNothing()
        // {
        //     await UniTask.WaitUntil(() => _didAction == 0);
        // }

        private async UniTask<int> DoActionAsync_Attack()
        {
            await UniTask.WaitUntil(() => _didAction == 1);
            Debug.Log($"Unit - DoActionAsync_Attack");

            return 1;
        }

        private async UniTask<int> DoActionAsync_EndTurn()
        {
            await UniTask.WaitUntil(() => _didAction == 2);
            Debug.Log($"Unit - DoActionAsync_EndTurn");

            return 2;
        }

        public void Reset()
        {
            _acted = false;
            _didAction = -1;
        }

        public async UniTask Select()
        {
            Debug.Log("Show selection ui");

            var result = await _managerBattle.RequestShowingSelectionHud(this);
            _didAction = result;
        }

        public void SetPickableMode(bool inValue)
        {
            if (noPickableGO is null) return;

            noPickableGO.SetActive(!inValue);
        }
    }
}
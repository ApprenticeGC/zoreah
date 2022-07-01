namespace GiantCroissant.MoYraq.Game
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using Cysharp.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.UI;

    public class ManagerHud : MonoBehaviour
    {
        public GameObject selectionHudGO;
        
        public Button actionDoNothingButton;
        public Button actionAttackButton;
        public Button actionEndTurnButton;

        private void Start()
        {
            selectionHudGO.SetActive(false);
        }

        public async UniTask SelectDoNothingAsync(CancellationToken token)
        {
            await actionDoNothingButton.OnClickAsync(token);
        }

        public async UniTask SelectAttackAsync(CancellationToken token)
        {
            await actionAttackButton.OnClickAsync(token);
        }        
        
        public async UniTask SelectEndTurnAsync(CancellationToken token)
        {
            await actionEndTurnButton.OnClickAsync(token);
        }

        public async UniTask<int> ShowSelectionHud(Vector3 worldPosition)
        {
            if (selectionHudGO is null)
            {
                return -1;
            }

            var screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
            selectionHudGO.transform.position = new Vector3(screenPosition.x, screenPosition.y - 150.0f, 0);
            
            selectionHudGO.SetActive(true);

            var token = new CancellationToken(default);
            var asyncTasks = new List<UniTask>
            {
                SelectDoNothingAsync(token),
                SelectAttackAsync(token),
                SelectEndTurnAsync(token)
            };

            var result = await UniTask.WhenAny(asyncTasks);
            Debug.Log($"ManagerHud - ShowSelectionHud - result: {result}");
            selectionHudGO.SetActive(false);

            return result;
        }
    }
}

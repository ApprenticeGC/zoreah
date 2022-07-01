namespace GiantCroissant.MoYraq.Game
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cysharp.Threading.Tasks;
    using UnityEngine;

    public class ManagerBattle : MonoBehaviour
    {
        public ManagerHud mangerHud;

        public List<Unit> units;
        public int groupCount;

        private Unit _focusUnit;

        private bool _finish;
        private int _actedGroupCount;

        private async UniTask Start()
        {
            foreach (var unit in units)
            {
                unit.SetManagerBattle(this);
            }

            while (!_finish)
            {
                Debug.Log($"ManagerBattle - Start - 1st while");

                foreach (var unit in units)
                {
                    unit.Reset();
                }

                //
                var asyncTasks = new List<UniTask>();
                foreach (var unit in units)
                {
                    asyncTasks.Add(unit.DoActionAsync());
                }

                Debug.Log($"ManagerBattle - Start - asyncTasks count: {asyncTasks.Count}");

                _actedGroupCount = 0;
                while (_actedGroupCount < groupCount)
                {
                    foreach (var unit in units)
                    {
                        unit.SetPickableMode(_actedGroupCount == unit.belongToGroup);
                    }

                    Debug.Log($"ManagerBattle - Start - 2nd while, actedGroupCount: {_actedGroupCount}");
                    var r = await UniTask.WhenAny(asyncTasks);
                    Debug.Log($"ManagerBattle - Start - r: {r}");
                    asyncTasks.Clear();
                    for (var i = 0; i < units.Count; ++i)
                    {
                        if (units[i].belongToGroup == _actedGroupCount) continue;
                        
                        asyncTasks.Add(units[i].DoActionAsync());
                    }
                    
                    Debug.Log($"ManagerBattle - Start - asyncTasks count: {asyncTasks.Count}");
                    
                    ++_actedGroupCount;
                }
                
            }
        }

        public async UniTask<int> RequestShowingSelectionHud(Unit inValue)
        {
            if (inValue.belongToGroup != _actedGroupCount) return -1;
            
            _focusUnit = inValue;
            
            if (mangerHud is not null)
            {
                var result = await mangerHud.ShowSelectionHud(inValue.transform.position);
                Debug.Log($"ManagerBattle - RequestShowingSelectionHud");

                return result;
            }

            return -1;
        }
    }
}
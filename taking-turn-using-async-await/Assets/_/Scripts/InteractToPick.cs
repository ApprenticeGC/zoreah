namespace GiantCroissant.MoYraq.Game
{
    using System;
    using UnityEngine;
    using UnityEngine.Assertions;

    public class InteractToPick : MonoBehaviour
    {
        public GameObject ownerGO;

        private void Awake()
        {
            // Assert.IsNotNull(ownerGO);
        }

        public void Interact()
        {
            if (ownerGO is null) return;

            var selectable = ownerGO.GetComponent<ISelectable>();
            if (selectable is not null)
            {
                selectable.Select();
            }
        }
    }
}
using UnityEngine;

namespace GP_Utilities.Component
{
    public class GP_Component : MonoBehaviour
    {
        private static GP_Component Component;

        private void Awake()
        {
            if (GP_Component.Component == null)
                GP_Component.Component = this;
            else
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }
    }
}

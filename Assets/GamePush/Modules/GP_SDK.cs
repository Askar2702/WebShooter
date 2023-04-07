using UnityEngine;
using UnityEngine.Events;

namespace GamePush
{
    public class GP_SDK : MonoBehaviour
    {
        [SerializeField] private bool _initOnEditorStart = true;

        public static event UnityAction OnReady;

#if !UNITY_EDITOR && UNITY_WEBGL
#else
        private void Start() { if (_initOnEditorStart) OnReady?.Invoke(); }
#endif

        private void CallSDKReady() => OnReady?.Invoke();
    }
}
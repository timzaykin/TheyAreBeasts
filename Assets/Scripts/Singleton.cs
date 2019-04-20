using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TAB { 
    public class Singleton<T> : MonoBehaviour where T:MonoBehaviour {
        public static bool isApplicationQitting;
        private static T _instance;
        public static System.Object _lock = new System.Object();
        public static T Instance {
            get {
                if (isApplicationQitting) {
                    return null;
                }

                lock (_lock) { 
                    if (_instance == null) {
                        _instance = FindObjectOfType<T>();

                        if (_instance == null) {
                            var singleton = new GameObject("[SINGLETON]" + typeof(T));
                            _instance = singleton.AddComponent<T>();
                            DontDestroyOnLoad(singleton);
                        }
                    }
                    return _instance;
                }
            }

        }
        public virtual void onDestroy() {
            isApplicationQitting = true;
        }
	}
}

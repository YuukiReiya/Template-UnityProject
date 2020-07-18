using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Util
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        //  static instance
        private static T instance = null;

        //  getter
        public static T Instance
        {
            get
            {
                //  null check!
                if (instance == null)
                {
                    //  find
                    instance = (T)FindObjectOfType(typeof(T));

                    //  not found!
                    if (instance == null)
                    {
                        Debug.LogError("<color=red>" + typeof(T) + "</color>" + " is nothing");
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// Awake
        /// </summary>
        protected virtual void Awake()
        {
            //  null check!
            if (instance == null)
            {
                instance = this as T;
            }

            //  static check!
            if (instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
        }
    }
}

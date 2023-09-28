using System.Threading.Tasks;
using LO.Event;
using UnityEngine;
using UnityEngine.AddressableAssets;

/*
    ref : https://docs.unity3d.com/Packages/com.unity.addressables@1.15/manual/LoadingAddressableAssets.html
*/

namespace LO.Utils {

    public static class LOAddressable {

        /// <summary>
        /// Load asset async by the key 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">addressable key</param>
        /// <returns></returns>
        public static async Task<T> LoadAsync<T>(string key) where T : Object {

            var asset = Addressables.LoadAssetAsync<T>(key);
            await asset.Task;

            var result = asset.Result as T;
            return result;
        }

        /// <summary>
        /// Load asset by the key with the complete callback
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">addressable key</param>
        /// <param name="complete">result callback</param>
        public static void Load<T>(string key, LOObjectEvent<T> complete = null) where T : Object {

            Addressables.LoadAssetAsync<T>(key).Completed += (task) => {

                complete?.Invoke(task.Result as T);
            };
        }

        /// <summary>
        /// Instantiate asset after loaded
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">addressable key</param>
        /// <param name="parent">object parent</param>
        /// <param name="complete">result callback</param>
        public static void Instantiate<T>(string key, Transform parent, LOObjectEvent<T> complete = null) where T : Object {

            Load<GameObject>(key, (asset) => {
                var obj = GameObject.Instantiate(asset, parent).GetComponent<T>();
                complete?.Invoke(obj);
            });
        }
    }
}
using UnityEditor;
using UnityEngine;

namespace LO.Editor {

    public class LORuntimeEditor {

        [MenuItem("City Game/Play mode/Add 1 level")]
        static void UpdateUserLevel1() {

            if (!Application.isPlaying) {
                return;
            }

            LOApplication.Instance.GameDataManager.UserDataManager.AddUserLevel(1);
        }

        [MenuItem("City Game/Play mode/Add 1000 coin")]
        static void UpdateUserCoin1000() {

            if (!Application.isPlaying) {
                return;
            }

            LOApplication.Instance.GameDataManager.UserDataManager.AddUserCoin(1000);
        }

        [MenuItem("City Game/Play mode/Add 100 tech")]
        static void UpdateUserTech100() {

            if (!Application.isPlaying) {
                return;
            }

            LOApplication.Instance.GameDataManager.UserDataManager.AddUserTechPoint(100);
        }
    }
}
using UnityEditor;
using LO.Meta;
using System.Collections.Generic;

namespace LO.Editor {

    public class LOCardMetaEditor {

        [MenuItem("City Game/Card Meta/Update All Meta")]
        static void UpdateAllCardMeta() {

            List<LOCardMetaBase> cardMetas = LOEditorUtils.FindAssets<LOCardMetaBase>(LOEditorPath.CARD_META_PATH.WithAssets());

            foreach (var cardMeta in cardMetas) {
                cardMeta.EditorUpdateMeta();
            }
        }
    }
}
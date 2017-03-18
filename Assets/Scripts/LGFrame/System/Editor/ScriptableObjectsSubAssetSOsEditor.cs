using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace LGFrame
{
    public class ScriptableObjectsSubAssetSOsEditor<T,Z> : EditorWindow 
        where T: ScriptableObject
        where Z:ScriptableObjectsSubAssetSOs<T>
    {
        Z itemDefnList1;

        //[MenuItem("Window/ScriptableObjectsSubAssetSOs Editor %#m")]
        static void Init()
        {
            EditorWindow.GetWindow<ScriptableObjectsSubAssetSOsEditor<T,Z>>();
        }

        void OnGUI()
        {
            string listname = string.Format("{0}_List", typeof(Z).Name);
            if (GUILayout.Button(listname))
            {
                // The bug: When adding a ScriptableObject as a child of a ScriptableObject,
                //   the inspector can get confused about which is the parent and which
                //   is the child, and display the containing object as one of the children
                //
                // Repro'ed in: Unity 5.4.0b12 (beta), but it sounds like this has been
                //   around for a while.
                //
                // The reason this sometimes happens: If the filename of the main asset is
                //   alphabetically after the name of a child asset, then the inspector
                //   misidentifies which is which and the child asset appears as the main
                //   asset (+vice versa).
                //
                // To workaround: Name your main/containing asset's file something
                //   alphabetically before all child assets.  e.g.: append with "aaa"
                //

                // This one will fail; the main asset appears as the last asset in the container
                itemDefnList1 = ScriptableObject.CreateInstance<Z>();
                itemDefnList1.List = new List<T>();
               
                createTestAsset(listname, itemDefnList1);                             

                AssetDatabase.SaveAssets();
            }

            if (GUI.changed)
            {
                EditorUtility.SetDirty(itemDefnList1);
            }
        }

        void createTestAsset<Q>(string assetName, ScriptableObjectsSubAssetSOs<Q> itemDefnList) where Q:ScriptableObject
        {
            // Save the container asset with the specified name
            string path = AssetDatabase.GenerateUniqueAssetPath("Assets/" + assetName + ".asset");
            AssetDatabase.CreateAsset(itemDefnList, path);

            // Create some test items and add them to the container asset.
            for (int i = 0; i < 5; i++)
            {
                Q newMyItemDefn = ScriptableObject.CreateInstance<Q>();
                newMyItemDefn.name = "jjj " + i;
                itemDefnList.List.Add(newMyItemDefn);
                AssetDatabase.AddObjectToAsset(newMyItemDefn, itemDefnList);
            }
        }
    }
}
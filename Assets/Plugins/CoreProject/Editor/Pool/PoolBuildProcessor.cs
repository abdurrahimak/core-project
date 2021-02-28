using CoreProject.Pool;
using UnityEditor;
using UnityEngine;

namespace CoreProject.Editor.Pool
{
    public class PoolEditor 
    {
        [InitializeOnLoadMethod]
        static void OnProjectLoadedInEditor()
        {
            AssemblyReloadEvents.afterAssemblyReload += CreatedNew;
        }

        static void CreatedNew(){

            Debug.Log("CreatedNew");
            PoolData poolData = Resources.Load<PoolData>("PoolData");
            if(poolData == null)
            { 
                poolData = ScriptableObject.CreateInstance<PoolData>();
                poolData.Pools = new System.Collections.Generic.List<PoolModel>();                    
                AssetDatabase.CreateAsset(poolData, "Assets/Resources/PoolData.asset");
            }
        }
    }
}

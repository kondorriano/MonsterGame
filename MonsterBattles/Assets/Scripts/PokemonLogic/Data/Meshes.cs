using UnityEngine;
using System.Collections.Generic;

public class Meshes
{
    public const string BattleMeshDefault = "default";
    public readonly static HashSet<string> BattleMeshes = new HashSet<string>
    {
        {"bulbasaur"},
        {"meowth"},
    };

    public static Dictionary<string, ResourceRequest> BattleMeshesLoading = new Dictionary<string, ResourceRequest>();
    public static Dictionary<string, GameObject> BattleMeshesLoaded = new Dictionary<string, GameObject>();

    private static ResourceRequest BattleMeshDefaultLoading = null;
    private static GameObject BattleMeshDefaultLoaded = null;

    public static string GetMeshPath(string meshName)
    {
        return "Meshes\\" + meshName;
    }

    public static GameObject GetMeshSync(string meshName)
    {
        GameObject battleMesh;
        if (BattleMeshesLoaded.TryGetValue(meshName, out battleMesh))
        {
            return battleMesh;
        }

        ResourceRequest meshLoading;
        if (BattleMeshesLoading.TryGetValue(meshName, out meshLoading))
        {
            // Wrong path tete
            while (!meshLoading.isDone) ;

            BattleMeshesLoaded.Add(meshName, (GameObject)meshLoading.asset);
            BattleMeshesLoading.Remove(meshName);
        }

        if (BattleMeshes.Contains(meshName))
        {
            battleMesh = Resources.Load<GameObject>(GetMeshPath(meshName));
            if (battleMesh != null)
            {
                BattleMeshesLoaded.Add(meshName, battleMesh);
                return battleMesh;
            }
        }

        if (BattleMeshDefaultLoading != null)
        {
            // Wrong path tete
            while (!BattleMeshDefaultLoading.isDone) ;

            BattleMeshDefaultLoaded = (GameObject)BattleMeshDefaultLoading.asset;
            BattleMeshDefaultLoading = null;
        }

        if (BattleMeshDefaultLoaded == null)
        {
            battleMesh = Resources.Load<GameObject>(GetMeshPath(BattleMeshDefault));
            if (battleMesh != null)
            {
                BattleMeshDefaultLoaded = (GameObject)battleMesh;
            }
            else
            {
                BattleMeshDefaultLoaded = new GameObject();
                BattleMeshDefaultLoaded.name = "BattleMeshDefaultVoid";
            }
        }

        return BattleMeshDefaultLoaded;
    }

    public static void LoadAsync(string meshName)
    {
        if (BattleMeshes.Contains(meshName) && !BattleMeshesLoading.ContainsKey(meshName) && !BattleMeshesLoaded.ContainsKey(meshName))
        {
            ResourceRequest request = Resources.LoadAsync<GameObject>(GetMeshPath(meshName));
            BattleMeshesLoading.Add(meshName, request);
        }
    }

    public static void Update()
    {
        if (BattleMeshDefaultLoading == null && BattleMeshDefaultLoaded == null)
        {
            ResourceRequest request = Resources.LoadAsync<GameObject>(GetMeshPath(BattleMeshDefault));
            BattleMeshDefaultLoading = request;
        }
        else if (BattleMeshDefaultLoading != null && BattleMeshDefaultLoaded == null)
        {
            if (BattleMeshDefaultLoading.isDone)
            {
                if (BattleMeshDefaultLoading.asset != null)
                {
                    BattleMeshDefaultLoaded = (GameObject)BattleMeshDefaultLoading.asset;
                }
                else
                {
                    BattleMeshDefaultLoaded = new GameObject();
                    BattleMeshDefaultLoaded.name = "BattleMeshDefaultVoid";
                }

                BattleMeshDefaultLoading = null;
            }
        }

        List<KeyValuePair<string, ResourceRequest>> toRemove = new List<KeyValuePair<string, ResourceRequest>>();
        foreach(KeyValuePair<string, ResourceRequest> pair in BattleMeshesLoading)
        {
            if (pair.Value.isDone)
            {
                if (pair.Value.asset != null)
                {
                    BattleMeshesLoaded.Add(pair.Key, (GameObject)pair.Value.asset);
                }
                
                toRemove.Add(pair);
            }
        }

        foreach (KeyValuePair<string, ResourceRequest> pair in toRemove)
        {
            BattleMeshesLoading.Remove(pair.Key);
        }
    }

    public static bool AllLoaded()
    {
        return BattleMeshesLoading.Count == 0 && BattleMeshDefaultLoading == null;
    }
}
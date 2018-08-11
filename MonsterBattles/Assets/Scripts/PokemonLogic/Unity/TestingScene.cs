using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PokemonSkin
{
    string speciesId;
    GameObject prefab;
}

public class TestingScene : MonoBehaviour {

    Battle b;

    PokemonSet[] sets1 = new PokemonSet[]
    {
        new PokemonSet(
            speciesId: "bulbasaur",
            name: "Frodo Bulbón",
            level: 50,
            gender: Globals.GenderName.F,
            abilityId: "Overgrow",
            movesId: new string[]
            {
                "tackle"
            }
            )
    };

    PokemonSet[] sets2 = new PokemonSet[]
    {
        new PokemonSet(
            speciesId: "meowth",
            name: "Smeowg",
            level: 50,
            gender: Globals.GenderName.M,
            abilityId: "Technician",
            movesId: new string[]
            {
                "tackle"
            }
            )
    };

    //            PokemonSet set1 = new PokemonSet(   movesId: new string[]{ "testingrunevent" });

    private bool WaitingForLoading = false;

    private void Start()
    {
        for(int i = 0; i < sets1.Length; i++)
            Meshes.LoadAsync(sets1[i].speciesId);

        for (int i = 0; i < sets2.Length; i++)
            Meshes.LoadAsync(sets2[i].speciesId);

        WaitingForLoading = true;
    }

    private void Update()
    {
        Meshes.Update();

        if (Meshes.AllLoaded() && WaitingForLoading)
        {
            WaitingForLoading = false;

            Battle.Team[] teams = new Battle.Team[]
            {
                new Battle.Team(sets1),
                new Battle.Team(sets2)
            };

            b = new Battle(null, teams);

            for(int i = 0; i < teams.Length; i++)
            {
                for (int j = 0; j < teams[i].pokemons.Length; j++)
                {
                    teams[i].pokemons[j].pokemonData.isActive = true;
                }
            }

            AddCameras(teams);
        }
    }

    public static void AddCameras(Battle.Team[] teams)
    {
        int playerCount = 0;
        Camera[] cameras = new Camera[2];
        CameraController[] cControllers = new CameraController[2];

        for (int i = 0; i < teams.Length; i++)
        {
            for (int j = 0; j < teams[i].pokemons.Length; j++)
            {
                GameObject playerCameraGO = new GameObject();
                playerCameraGO.name = string.Format("CameraPlayer{0}", playerCount);
                Camera playerCamera = playerCameraGO.AddComponent<Camera>();
                CameraController playerCameraController = playerCameraGO.AddComponent<CameraController>();
                cameras[playerCount] = playerCamera;
                cControllers[playerCount] = playerCameraController;
                playerCount++;
            }
        }

        if (playerCount >= 2)
        {
            var rect0 = cameras[0].rect;
            rect0.x = 0.0f;
            rect0.width = 0.5f;
            cameras[0].rect = rect0;

            var rect1 = cameras[1].rect;
            rect1.x = 0.5f;
            rect1.width = 0.5f;
            cameras[1].rect = rect1;

            teams[0].pokemons[0].transform.localPosition = new Vector3(0, 0, 5);
            teams[1].pokemons[0].transform.localPosition = new Vector3(0, 0, -5);

            cControllers[0].character = cControllers[1].target = teams[0].pokemons[0].transform;
            cControllers[1].character = cControllers[0].target = teams[1].pokemons[0].transform;

            teams[0].pokemons[0].camTrans = cameras[0].transform;
            teams[1].pokemons[0].camTrans = cameras[1].transform;

            teams[0].pokemons[0].camPokemon = cControllers[0];
            teams[1].pokemons[0].camPokemon = cControllers[1];

            teams[0].pokemons[0].ControllerId = 1;
            teams[1].pokemons[0].ControllerId = 2;
        }

        if (Camera.main)
        {
            Camera.main.enabled = false;
        }
    }
}

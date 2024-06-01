using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

[RequireComponent(typeof(CharacterController))]
public class InputReader : MonoBehaviour
{
    public static InputReader instance;
    void Awake() => instance = this;

    // The Rewired player id of this character
    public int playerId = 0;

    Player player; // The Rewired Player
    CharacterController cc;

    public float moveX;
    public float moveZ;
    public float lookX;
    public float lookY;
    public bool interact;

    void Start() {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);

        // Get the character controller
        cc = GetComponent<CharacterController>();
    }

    void Update () {
        GetInput();
    }

    void GetInput() {
        // Get the input from the Rewired Player. All controllers that the Player owns will contribute, so it doesn't matter
        // whether the input is coming from a joystick, the keyboard, mouse, or a custom controller.
        if (Time.timeScale != 0f) {
            moveX = player.GetAxis("MoveEastWest"); // get input by name or action id
            moveZ = player.GetAxis("MoveNorthSouth");
            lookX = player.GetAxis("LookHorizontal");
            lookY = player.GetAxis("LookVertical");
            interact = player.GetButtonDown("Interact");
        }

        if (player.GetButtonDown("Pause")) {
            GameStateManager.instance.PauseGame();
            Debug.Log("pause");
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
    private GridSelector gridSelector;

    public PlayMode playMode;
    public SceneLoadState loadState;

    private CameraFollow playCamera;
    private UICameraControl editCamera;
    private GameObject selectionUI;

    private Action OnExitEditModeCallbacks;

    private int deathCount;

    public void StartStage(int stageNum)
    {
        gridSelector = UnityEngine.Object.FindObjectOfType<GridSelector>();
        selectionUI = GameObject.Find("SelectionUI");
        gridSelector.InitSelectionUI(stageNum);

        playCamera = UnityEngine.Object.FindObjectOfType<CameraFollow>();
        editCamera = UnityEngine.Object.FindObjectOfType<UICameraControl>();
        

        deathCount = -1;
        if (stageNum != 1)
        {
            EditMode();
        }
        else
        {
            EditMode();
            ExitEditMode();
        }
    }

    public void EditMode()
    {
        playMode = PlayMode.EDIT;

        playCamera.enabled = false;
        editCamera.enabled = true;
        editCamera.Init();
        selectionUI.SetActive(true);

        deathCount += 1;

        if (deathCount > 0)
        {
            selectionUI.GetComponentInChildren<ChangeScore>().PlayScore(deathCount - 1, deathCount);
        }
    }
    
    public void ExitEditMode()
    {
        playMode = PlayMode.PLAY;

        playCamera.enabled = true;
        editCamera.Init();
        editCamera.enabled = false;
        selectionUI.SetActive(false);

        OnExitEditModeCallbacks?.Invoke();
    }

    public void Init()
    {
        playMode = PlayMode.LOBBY;
        loadState = SceneLoadState.ENDLOAD;
    }

    public void GoLobby()
    {
        playMode = PlayMode.LOBBY;
        SceneManager.LoadScene("Lobby");
    }

    public IEnumerator LoadScene(int stageNum)
    {
        loadState = SceneLoadState.LOAD;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Stage" + stageNum, LoadSceneMode.Single);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        Debug.Log("asdf");
        yield return new WaitForEndOfFrame();
        loadState = SceneLoadState.ENDLOAD;
        StartStage(stageNum);
    }

    public void RegisterExitEditorCallback(Action callback)
    {
        OnExitEditModeCallbacks += callback;
    }
}

public enum PlayMode
{
    LOBBY,
    PLAY,
    EDIT
}

public enum SceneLoadState
{
    LOAD,
    ENDLOAD
}
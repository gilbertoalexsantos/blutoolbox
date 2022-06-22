## Dependencies

### EditorAppInfo

You need to add a EditorAppInfo ScriptableObject on a Resources folder with path

```Game/General```

### Binds

You need to add Binders for:

* IScreenManagerInfo
* IScreenResolver

## How to add Custom Installers

### ScriptableObject

You need to create a ScriptableObject that inherits from

```AppStartCustomScriptableInstaller```

And put in a Resources folder, with the path

```Game/CustomInstallers```

### SceneInstaller

You need to create a MonoBehaviour on the MainScene that inherits from

```AppStartCustomSceneInstaller```

## How to change original implementations

Create a CustomInstaller and use ```container.Rebind<>```
using Kingdom;
using Kingdom.Input;
using Kingdom.Monarch;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class MainSceneInstaller : MonoInstaller
{
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] PouchPresenter pouchPresenter;
    [SerializeField] private ApproachableController approachableController;
    [SerializeField] MonarchInteractionController monarchInteractionController;
    [SerializeField] private Camera camera;
    [SerializeField] SharedSettingsSo sharedSettingsSo;
    public override void InstallBindings()
    {
        Container.Bind<InputHandler>().FromInstance(inputHandler).AsSingle();
        Container.Bind<PouchPresenter>().FromInstance(pouchPresenter).AsSingle();
        Container.Bind<ApproachableController>().FromInstance(approachableController).AsSingle();
        Container.Bind<MonarchInteractionController>().FromInstance(monarchInteractionController).AsSingle();
        Container.Bind<Camera>().FromInstance(camera).AsSingle();
        Container.Bind<SharedSettings>().FromInstance(sharedSettingsSo.Settings).AsSingle();
    }
}
using Kingdom;
using Kingdom.Entities;
using Kingdom.Input;
using Kingdom.Monarch;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class MainSceneInstaller : MonoInstaller
{
    [SerializeField] private InputHandler inputHandler;
    [FormerlySerializedAs("pouchPresenter")] [SerializeField] private MonarchPouchPresenter monarchPouchPresenter;
    [SerializeField] private ApproachableController approachableController;
    [SerializeField] private MonarchInteractionController monarchInteractionController;
    [SerializeField] private Camera camera;
   
    public override void InstallBindings()
    {
        Container.Bind<InputHandler>().FromInstance(inputHandler).AsSingle();
        Container.Bind<MonarchPouchPresenter>().FromInstance(monarchPouchPresenter).AsSingle();
        Container.Bind<ApproachableController>().FromInstance(approachableController).AsSingle();
        Container.Bind<MonarchInteractionController>().FromInstance(monarchInteractionController).AsSingle();
        Container.Bind<Camera>().FromInstance(camera).AsSingle();

        Container.Bind<CoinPool>().FromNew().AsSingle();
    }
}
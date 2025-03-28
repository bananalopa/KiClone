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
    [SerializeField] private MonarchPouchPresenter monarchPouchPresenter;
    [FormerlySerializedAs("approachableController")] [SerializeField] private ApproachablesLookup approachablesLookup;
    [SerializeField] private MonarchInteractionController monarchInteractionController;
    [SerializeField] private Camera camera;
    [SerializeField] MonarchMoveController monarchMoveController;
   
    public override void InstallBindings()
    {
        Container.Bind<InputHandler>().FromInstance(inputHandler).AsSingle();
        Container.Bind<MonarchPouchPresenter>().FromInstance(monarchPouchPresenter).AsSingle();
        Container.Bind<ApproachablesLookup>().FromInstance(approachablesLookup).AsSingle();
        Container.Bind<MonarchInteractionController>().FromInstance(monarchInteractionController).AsSingle();
        Container.Bind<Camera>().FromInstance(camera).AsSingle();

        Container.Bind<CoinPool>().FromNew().AsSingle();
        
        Container.Bind<MonarchMoveController>().FromInstance(monarchMoveController).AsSingle();
    }
}
using Kingdom.Input;
using UnityEngine;
using Zenject;

public class MainSceneInstaller : MonoInstaller
{
    [SerializeField] private PlayerInputHandler playerInputHandler;
    
    public override void InstallBindings()
    {
        Container.Bind<PlayerInputHandler>().FromInstance(playerInputHandler).AsSingle();
        
    }
}
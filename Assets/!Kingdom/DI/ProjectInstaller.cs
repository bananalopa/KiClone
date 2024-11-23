using Kingdom;
using Kingdom.Entities;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private SharedSettingsSo sharedSettingsSo;
    [SerializeField] private CoinSettingSo coinSettingSo;
    
    public override void InstallBindings()
    {
        Container.Bind<SharedSettings>().FromInstance(sharedSettingsSo.Settings).AsSingle();
        Container.Bind<CoinSetting>().FromInstance(coinSettingSo.Setting).AsSingle();
    }
}
using UnityEngine;
using Zenject;


[CreateAssetMenu (fileName = " ZenjectInstaller ", menuName = "ZenjectInstaller")]
public class ZenjectInstaller : ScriptableObjectInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IData>().To<Data>().AsSingle();
    }
}

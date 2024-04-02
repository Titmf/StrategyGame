using SceneManagement;

using Zenject;

public class SceneLoaderInstaller : MonoInstaller<SceneLoaderInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<SceneLoader>().AsSingle().NonLazy();
    }
}
namespace GiantCroissant.MoYraq.Game
{
    using Cysharp.Threading.Tasks;

    public interface ISelectable
    {
        UniTask Select();
    }
}

/// <summary>
/// Grid Map 상에서, 이미 개체가 존재하는 Grid에, 별도의 개체가 진입(Move)하려 할 때 발생 가능한 동작에 대한 묶음말
/// 피동자 = 이미 대상 Grid에 존재하는 개체 (IEncounterable) |
/// 행동자 = 대상 Grid로 진입하려는 개체 (사실상 GridPawn) |
/// </summary>
public interface IEncounterable
{
    /// <summary>
    /// 행동자(GridPawn)가 피동자(IEncounterable)가 존재하는 Grid로 진입하려 할 때 발생하는 동작
    /// </summary>
    /// <param name="actor">행동자(GridPawn)</param>
    /// <returns>피동자와 행동자 간의 충돌이 발생하는지 여부</returns>
    public bool EncounteredBy(GridPawn actor);
}
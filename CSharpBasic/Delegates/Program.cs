namespace Delegates
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PlayerUI playerUI = new PlayerUI();
            Player player = new Player();
            // 대리자에 함수 대입하기
            player.onHpChanged = playerUI.Refresh;
            // 대리자에 함수 구독하기
            player.onHpChanged += playerUI.Refresh;
            // 대리자에 함수 구독 취소하기
            player.onHpChanged -= playerUI.Refresh;

            while (true)
            {
                // todo -> 고블린과의 모의전투 중 hp가 깎일 예정
            }
        }
    }
}
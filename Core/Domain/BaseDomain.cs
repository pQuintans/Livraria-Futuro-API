namespace LivrariaFuturo.Authentication.Core.Domain
{
    public static class BaseDomain
    {
        #region [ Propriedades ]

        public static DateTime DataAgoraUTC { get { return DateTime.Now.ToUniversalTime(); } }

        #endregion [ FIM - Propriedades ]
    }
}

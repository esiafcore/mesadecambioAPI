namespace eSiafApiN4;

// ReSharper disable once InconsistentNaming
public static class AC
{
    //Conexiones
    
    public const string EsiafN4Cnx = "eSIAFN4Connection";
    public const string XanesN4Cnx = "XanesN4Connection";
    public const string XanesN8Cnx = "XanesN8Connection";

    public const string LocalUserName = "LocalUser";
    public const string LocHostMe = "LocalHostMe";
    public const string Ipv4Default = "127.0.0.1";
    public const string SecretUserId = "DbConnection:UserId";
    public const string SecretUserPwd = "DbConnection:Password";
    public const string SectionKeys = "Authentication:Schemes:Bearer:SigningKeys";
    public const string IssuedByKey = "Issuer";
    public const string IssuedByValue = "Value";
    public const string IssuedApp = "esiafapin4-app";

    //Claims
    public const string EmailClaim = "email";


    public const int CacheOutputExpire = 15;

    public const string EvictByTagBancos = "bancos-get";
    public const string EvictByTagAsientosContables = "asientoscontables-get";
    public const string EvictByTagCuentasBancarias = "cuentasbancarias-get";
    public const string EvictByTagTransaccionBancarias = "transaccionesbco-get";

}
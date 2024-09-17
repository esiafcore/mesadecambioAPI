namespace XanesN8.Api;

// ReSharper disable once InconsistentNaming
public static class AC
{
    public const string CategoryBcoByDefault = "BCO";
    public const string CategoryCntByDefault = "CNT";

    //Conexiones
    public const string EsiafN4Cnx = "eSIAFN4Connection";
    public const string XanesN4Cnx = "XanesN4Connection";
    public const string XanesN8Cnx = "XanesN8Connection";

    public const string LocalUserName = "LocalUser";
    public const string LocHostMe = "LocalHostMe";
    public const string LocalIpv4Default = "127.0.0.1";
    public const string SecretUserId = "DbConnection:UserId";
    public const string SecreteSiafN4CompanyUid = "eSiafN4CompanyUid";
    public const string SecretUserPwd = "DbConnection:Password";
    public const string SectionKeys = "Authentication:Schemes:Bearer:SigningKeys";
    public const string IssuedByKey = "Issuer";
    public const string IssuedByValue = "Value";
    public const string IssuedApp = "esiafapin4-app";

    //Claims
    public const string TypeClaimEmail = "email";

    public const int CacheOutputExpire = 15;

    public const string EvictByTagBancos = "bancos-get";
    public const string EvictByTagCuentasBancarias = "cuentasbancarias-get";
    public const string EvictByTagConsecutivosBco = "consecutivosbco-get";
    public const string EvictByTagConsecutivosBcoDetalle = "consecutivosbcodetalle-get";
    public const string EvictByTagConfigBco = "configbco-get";
    public const string EvictByTagConfigCnt = "configcnt-get";
    public const string EvictByTagConsecutivosCnt = "consecutivoscnt-get";
    public const string EvictByTagConsecutivosCntDetalle = "consecutivoscntdetalle-get";
    public const string EvictByTagAsientosContables = "asientoscontables-get";
    public const string EvictByTagAsientosContablesDetalle = "asientoscontablesdetalle-get";
    public const string EvictByTagTransaccionBancarias = "transaccionesbco-get";
    public const string EvictByTagTransaccionBancariasRelacion = "transaccionesbcorel-get";
    public const string EvictByTagTransaccionBancariasDetalle = "transaccionesbcodetalle-get";
    public const string EvictByTagCustomersLegacy = "customerslegacy-get";
    public const string EvictByTagQuotationsDetailLegacy = "quotationsdetaillegacy-get";
    public const string EvictByTagQuotationsHeaderLegacy = "quotationsheaderlegacy-get";
    public const string EvictByTagQuotationsHeader = "quotationsheader-get";


    //Messages Errores
    public const string LoginIncorrectMessage = "Login incorrecto";
    public const string UserNotFound = "Usuario no encontrado";

    //Politicas de Claims
    public const string IsAdminClaim = "isadmin";
    public const string IsPowerUserClaim = "ispoweruser";
    public const string IsOperatorClaim = "isoperator";
    public const string IsQueryClaim = "isquery";


    public const short TransactionTotalDigitsNumberDefault = 5;
    public const char CharDefaultEmpty = '0';

}
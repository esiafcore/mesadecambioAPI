namespace XanesN8.Api.Utilidades;
public static class Enumeradores
{

    [Flags]
    public enum BancoConsecutivoPor : int
    {
        Categoria = 1,
        CategoriaMensual = 2,
        CategoriaAnual = 4,
        Tipo = 8,
        TipoMensual = 16,
        TipoAnual = 32
    }

    [Flags]
    public enum ContabilidadConsecutivoPor : int
    {
        Categoria = 1,
        CategoriaMensual = 2,
        CategoriaAnual = 4,
        Tipo = 8,
        TipoMensual = 16,
        TipoAnual = 32
    }

    [Flags]
    public enum ConsecutivoTipo : int
    {
        Temporal = 1,
        Perpetuo = 2
    }


    [Flags]
    public enum TransaccionBcoTipo : int
    {
        Pago = 1,
        Deposito = 2,
        NotaDebito = 4,
        NotaCredito = 8,
        Transferencia = 16
    }

    [Flags]
    public enum TransaccionBcoPagoSubtipo : int
    {
        Cheque = 1,
        Transferencia = 2,
        MesaCambio = 4
    }

    [Flags]
    public enum TransaccionBcoDepositoSubtipo : int
    {
        Deposito = 1
    }

    [Flags]
    public enum TransaccionBcoNotaDebitoSubtipo : int
    {
        NotaDebito = 1,
        NotaDebitoDiferencia = 2
    }

    [Flags]
    public enum TransaccionBcoNotaCreditoSubtipo : int
    {
        NotaCredito = 1,
        NotaCreditoDiferencia = 2
    }

    [Flags]
    public enum TransaccionBcoTransferenciaSubtipo : int
    {
        Transferencia = 1,
        TransferenciaDebito = 2,
        TransferenciaCredito = 4
    }
}


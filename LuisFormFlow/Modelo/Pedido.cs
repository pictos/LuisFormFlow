using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Threading.Tasks;

namespace LuisFormFlow.Modelo
{
    [Serializable]
    public class Pedido
    {
        public Hamburguers Hamburguers { get; set; }

        public Bebidas Bebidas { get; set; }

        public TipoEntrega TipoEntrega { get; set; }

        public string Nome { get; set; }

        public string Telefone { get; set; }

        public string Endereco { get; set; }

        public static IForm<Pedido> BuildForm()
        {
            
            return new FormBuilder<Pedido>()
                .Message("Pode fazer o pedido")
                .OnCompletion(MetodoCallBack)
                .Build();
        }

        //Jesus esteve aqui => esse é o delegate que o angelo usou, mas extraí em forma de metodo
        private static async Task MetodoCallBack(IDialogContext context, Pedido state)
        {

            await context.PostAsync($"{state.Nome}.Seu pedido foi gerado com sucesso! e em breve será entregue no endereço : {state.Endereco}");

        }
    }

    public enum TipoEntrega
    {
        RetirarNoLocal = 1,
        Entrega
    }

    public enum Hamburguers
    {
        Classico = 1,
        Misto,
        Frango,
        Vegetariano,
        Vegano,
        MegaXTudo
    }

    public enum Bebidas
    {
        CocaCola = 1,
        Refrigerante,
        Suco,
        SemBebida
    }
}

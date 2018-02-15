using LuisFormFlow.Modelo;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Threading.Tasks;

namespace LuisFormFlow.Dialogs
{
    [Serializable]
    //Suas chaves 
    [LuisModel("", "")]
    public class LuisForm : LuisDialog<Pedido>
    {
        private readonly BuildFormDelegate<Pedido> CriarPedido;

        internal LuisForm(BuildFormDelegate<Pedido> criarPedido)
        {
            CriarPedido = criarPedido;
        }

        [LuisIntent("Comprar")]
        public async Task Comprar(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Um minuto...");
            var pedidoForm = new FormDialog<Pedido>(new Pedido(), this.CriarPedido, FormOptions.PromptInStart);
            context.Call<Pedido>(pedidoForm, PedidoCompleto);
        }

        async Task PedidoCompleto(IDialogContext context, IAwaitable<Pedido> result)
        {
            Pedido pedido = null;
            try
            {
                pedido = await result;
            }
            catch (OperationCanceledException)
            {

                await context.PostAsync("Você cancelou o pedido.");
                return;
            }

            if (pedido != null)
                await context.PostAsync("Obrigado por comprar conosco!");
            else
                await context.PostAsync("O formulário retornou vazio");

            context.Wait(MessageReceived);
        }
    }
}
using WTranslate.ferramentas;
using NVAccess;
using Translate.Google;

namespace WTranslate
{
    partial class Form1
    {
        #region Desnecessario
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "WTranslate";
            Ferramentas ferramentas = new Ferramentas();

            TextBox Campo = ferramentas.CampoTx();

            string[] idiomasDsp = { "pt", "en", "es" };
            ListBox opcoes = ferramentas.LtExcolha(idiomasDsp, "Selecione idioma de origem");
            ListBox opcoesDest = ferramentas.LtExcolha(idiomasDsp, "Selecione idioma de destino");

            this.Controls.Add(opcoes);
            this.Controls.Add(opcoesDest);
            this.Controls.Add(Campo);
            //fazer a tradução quando for pressionado o atalho f1
            Campo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Campo_KeyDownAsync);
        }

        private async void Campo_KeyDownAsync(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                //tradusir com Translate
                string translate = await Translate(sender as TextBox);

                NVDA.Speak(translate);
            }
        }

        private async Task<string> Translate(TextBox sender)
        {
            var google = new TranslateGoogle();

            //reculperar o texto digitado
            TextBox Campo = sender as TextBox;
            //reculperar os idiomas origem e destinos selecionados
            ListBox opcoes = this.Controls[0] as ListBox;
            ListBox opcoesDest = this.Controls[1] as ListBox;
            //reculperar o idioma de origem
            string idiomaOrigem = opcoes.SelectedItem.ToString();
            //reculperar o idioma de destino
            string idiomaDestino = opcoesDest.SelectedItem.ToString();
            //obter o texto no campo
            string texto = Campo.Text;

            //fazer a tradução
            var tradusido = await google.TranslateTextAsync(texto, idiomaOrigem, idiomaDestino);

            return (tradusido.Replace(@"\r", ""));
        }

        #endregion
    }
}
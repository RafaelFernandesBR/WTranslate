﻿using WTranslate.ferramentas;
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
        private string texto_atual;
        private Ferramentas ferramentas;

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
            this.ferramentas = new Ferramentas();

            TextBox Campo = ferramentas.CampoTx();

            string[] idiomasDsp = { "pt", "en", "es" };
            ListBox opcoes = ferramentas.LtExcolha(idiomasDsp, "Selecione idioma de origem");
            ListBox opcoesDest = ferramentas.LtExcolha(idiomasDsp, "Selecione idioma de destino");
            CheckBox AutoClip = ferramentas.CreateCheckBox("Iniciar tradução automática");

            this.Controls.Add(opcoes);
            this.Controls.Add(opcoesDest);
            this.Controls.Add(Campo);
            this.Controls.Add(AutoClip);
            //fazer a tradução quando for pressionado o atalho f1
            Campo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Campo_KeyDownAsync);
            //iniciar monitoração do clipboard quando marcado
            AutoClip.CheckedChanged += new System.EventHandler(this.AutoClip_CheckedChanged);
        }

        private void AutoClip_CheckedChanged(object sender, System.EventArgs e)
        {
            CheckBox AutoClip = sender as CheckBox;

            ClipboardMonitor.ClipboardUpdate += async (object sender, EventArgs e) =>
{
    if (Clipboard.ContainsText())
    {
        NVAccess.NVDA.CancelSpeech();
        var clipboardText = Clipboard.GetText();
        //reculperar os idiomas origem e destinos selecionados
        ListBox opcoes = this.Controls[0] as ListBox;
        ListBox opcoesDest = this.Controls[1] as ListBox;
        //reculperar o idioma de origem
        string idiomaOrigem = opcoes.SelectedItem.ToString();
        //reculperar o idioma de destino
        string idiomaDestino = opcoesDest.SelectedItem.ToString();

        await TradusClipboard(clipboardText, idiomaOrigem, idiomaDestino);
    }
};

            if (AutoClip.Checked)
            {
                //iniciar monitoração do clipboard
                texto_atual = null;
                ClipboardMonitor.Start();
            }
            else
            {
                //parar monitoração do clipboard
                ClipboardMonitor.Stop();
            }
        }

        private async void Campo_KeyDownAsync(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
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

                string translate = await Translate(texto, idiomaOrigem, idiomaDestino);

                NVAccess.NVDA.Speak(translate.Replace(@"\r", ""));
            }
        }

        private async Task<string> Translate(string texto, string idiomaOrigem, string idiomaDestino)
        {
            //tradusir em segundo plano
            return await Task.Run(() =>
{
    var google = new TranslateGoogle();
    //fazer a tradução
    var tradusido = google.TranslateTextAsync(texto, idiomaOrigem, idiomaDestino);

    return tradusido;
});
        }

        private async Task TradusClipboard(string texto_novo, string idiomaOrigem, string idiomaDestino)
        {
            //verificar se o texto novo é diferente do texto atual
            if (texto_novo != texto_atual)
            {
                //atualizar o texto atual
                texto_atual = texto_novo;
                //traduzir o texto novo
                string translate = await Translate(texto_novo, idiomaOrigem, idiomaDestino);

                NVAccess.NVDA.Speak(translate.Replace(@"\r", ""));
            }
        }

        #endregion
    }
}

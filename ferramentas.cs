namespace WTranslate.ferramentas;

public class Ferramentas
{

    public TextBox CampoTx(string? Texto = null)
    {
        //adicionar um campo de edição
        var textBox = new System.Windows.Forms.TextBox();
        textBox.Multiline = true;
        textBox.Location = new System.Drawing.Point(12, 12);
        textBox.Text = Texto;
        textBox.Name = "textBox1";
        textBox.Size = new System.Drawing.Size(100, 20);
        textBox.AccessibleName = "Digite o texto para tradusir";
        textBox.TabIndex = 0;

        return textBox;
    }

    public ListBox LtExcolha(string[] opcoes, string AccessibleName)
    {
        //adicionar uma lista de opções
        var listBox = new System.Windows.Forms.ListBox();
        listBox.FormattingEnabled = true;
        listBox.Location = new System.Drawing.Point(12, 12);
        listBox.Name = "listBox1";
        listBox.Size = new System.Drawing.Size(120, 95);
        listBox.TabIndex = 1;
        listBox.Items.AddRange(opcoes);
        listBox.AccessibleName = AccessibleName;
        //gerar um número aleatório de até o tamanho do array
        int numero = new System.Random().Next(opcoes.Length);
        //selecionar a opção aleatória
        listBox.SelectedIndex = numero;

        return listBox;
    }

    public Button CreateButton(string Texto)
    {
        //criar um botão
        var button = new System.Windows.Forms.Button();
        button.Text = Texto;
        button.Location = new System.Drawing.Point(100, 100);
        button.Size = new System.Drawing.Size(100, 100);

        return button;
    }

    public void PlaySon(string audio)
    {
        System.Media.SoundPlayer player = new System.Media.SoundPlayer();
        player.SoundLocation = audio;
        player.Play();
    }

}
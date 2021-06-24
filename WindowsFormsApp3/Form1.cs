using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        CancellationTokenSource source;
        CancellationToken cancelToken;
        int sayac;
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //await Task.Run(()=> denemeAsync()) ;
            source = new CancellationTokenSource();
            cancelToken = source.Token;
           var deneme= Task.Run(()=> denemeAsync(cancelToken),cancelToken) ;
            try
            {
                await deneme;
            }
            catch
            {
                MessageBox.Show("durduruldu");
            }
            
        }
        public async Task denemeAsync(CancellationToken cancellationToken)
        {
            
              while (true)
              {
                  if(label1.InvokeRequired)
                  label1.Text = sayac.ToString();
                  sayac++;
                  System.Threading.Thread.Sleep(1000);
                cancellationToken.ThrowIfCancellationRequested();
            }
            
            
        }
        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            label2.Text = textBox1.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            source.Cancel();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sayac = 0;
            label1.Text = "";
        }
    }
}

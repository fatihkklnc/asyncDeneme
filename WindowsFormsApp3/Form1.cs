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
        bool otomatik=false;
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (!otomatik)
            {
                //await Task.Run(()=> denemeAsync()) ;
                source = new CancellationTokenSource();
                cancelToken = source.Token;
                var deneme = Task.Run(() => denemeAsync(cancelToken));
                var deneme2 = Task.Run(() => denemeAsync2(cancelToken));
                
                await deneme;
                await deneme2;
                
                
            }
            
        }
        public void denemeAsync(CancellationToken cancellationToken)
        {
            try
            {
                MessageBox.Show("birinci fonk");
                otomatik = true;
                while (true)
                {
                    if (label1.InvokeRequired)
                        label1.Text = sayac.ToString();
                    sayac++;
                    System.Threading.Thread.Sleep(1000);
                    cancellationToken.ThrowIfCancellationRequested();
                }
                
            }
            catch
            {
                
            }
            
        }
        public void denemeAsync2(CancellationToken cancellationToken)
        {
            try
            {
                MessageBox.Show("ikinci fonk");

                otomatik = true;
                while (true)
                {
                    Console.WriteLine("deneme");
                    System.Threading.Thread.Sleep(1000);
                    cancellationToken.ThrowIfCancellationRequested();
                }
            }
            catch
            {

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
            otomatik = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sayac = 0;
            label1.Text = "";
        }
    }
}

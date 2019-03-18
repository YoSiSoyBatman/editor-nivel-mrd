using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace editornivelmrd
{
    
    public partial class Form1 : Form
    {

        int clicks = 0; //numero de click
        int clickx0, clicky0; // coordenadas primer click
        int mouseX, mouseY;
        public List<Rectangle> rects;
        public List<char> things;
        char key = 'n';
        int unitsize = 10; //talla de rectangle de enemigos y personaje
        public Form1()
        {
            InitializeComponent();
            rects = new List<Rectangle>();
            things = new List<char>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Height = 600;
            this.Width = 600;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            int i = 0;
            foreach (Rectangle r in rects)  //dibujar rectangles
            {
                Pen p1 = new Pen(Color.Blue);
                switch (things[i])
                {
                    case 'p':
                        p1.Color = Color.Blue;
                        break;
                    case 'e':
                        p1.Color = Color.Red;
                        break;
                    case 'y':
                        p1.Color = Color.Green;
                        break;
                    default:
                        break;
                }
                e.Graphics.DrawRectangle(p1, r.X, r.Y, r.Width, r.Height);
                i++;
            }
            if (clicks == 1 && key == 'p')
            {
                e.Graphics.DrawRectangle(new Pen(Color.Cyan), clickx0, clicky0, mouseX - clickx0, mouseY - clicky0);
            }

        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            key = e.KeyChar;
            switch (key)  //to be filled
            {
                case 's'://print, show
                    Imprimir_Cosas();
                    break;
                case 'z': //pop
                    if (rects.Count >= 1)
                    {
                        rects.RemoveAt(rects.Count - 1);
                        things.RemoveAt(things.Count - 1);
                    }
                    break;
                default:
                    break;
            }
            clicks = 0;
            Invalidate();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            clicks++; //logica clicks
            if (clicks == 1 && key=='p') //logica solo util para el piso
            {
                clickx0 = e.X;
                clicky0 = e.Y;
            }
            else {
                switch (key)  //to be filled
                {
                    case 'p':
                        int width = e.X - clickx0;
                        int height = e.Y - clicky0;
                        rects.Add(new Rectangle(clickx0, clicky0, width, height));
                        things.Add('p');
                        break;
                    case 'e':
                        rects.Add(new Rectangle(e.X, e.Y, unitsize, unitsize));
                        things.Add('e');
                        break;
                    case 'y':
                        rects.Add(new Rectangle(e.X, e.Y, unitsize, unitsize));
                        things.Add('y');
                        break;
                    default:
                        break;
                }
                clicks = 0;
            }
            Invalidate();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (clicks == 1 && key == 'p')
            {
                mouseX = e.X;
                mouseY = e.Y;
                Invalidate();
            }
        }

        private void Imprimir_Cosas()
        {
            string s = "";
            for (int i = 0; i < rects.Count; i++)
            {
                int x1 = rects[i].X;
                int y1 = rects[i].Y;
                int x2 = rects[i].X + rects[i].Height;
                int y2 = rects[i].Y + rects[i].Width;
                s += things[i].ToString() + " " + x1 + " " + y1 + " " + x2 + " " + y2 + "\n";
            }
            MessageBox.Show(s);
        }
    }
    
}

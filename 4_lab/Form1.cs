using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _4_lab
{
    public partial class Form1 : Form
    {
        MyList l = new MyList();

        public Form1()
        {
            InitializeComponent();


        }



        private void pBox_MouseClick(object sender, MouseEventArgs e)
        {


            int flag = 0;

            for (l.First(); l.Eol(); l.Next())
            {
                if (l.GetObj().mouseInKrug(e.X, e.Y) == true)
                {
                    if (l.GetObj().GetColor() == 1)
                        l.GetObj().SwitchColor(2);
                    else l.GetObj().SwitchColor(1);
                    flag = 1;
                    break;
                }
            }

            if (flag != 1)
            {
                l.add(new krug(e.X, e.Y, 1));
            }
            this.Refresh();
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pBox_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Text = e.X.ToString();
            label2.Text = e.Y.ToString();
        }

        private void pBox_Paint(object sender, PaintEventArgs e)
        {
            for (l.First(); l.Eol(); l.Next())
            {
                l.GetObj().DrawKrug(l.GetObj(), e.Graphics);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            for (l.First(); l.Eol(); l.Next())
            {
                if (l.GetObj().GetColor() == 2)
                {
                    l.delete(l.GetObj());
                }
                if (l.nalichie() == true && l.GetNextNode() == null)
                {

                    if (l.GetHeadObj().GetColor() == 2)
                    {
                        l.delete(l.GetHeadObj());
                    }
                }

            }
            this.Refresh();
        }
    }

    public class krug
    {
        public int diam = 0;
        public int x = 0;
        public int y = 0;
        public int color = 0;

        public krug(int dx, int dy, int color)
        {
            x = dx;
            y = dy;
            diam = 200;
            this.color = color;
        }

        public void SwitchColor(int c)
        {
            color = c;
        }

        public int GetColor()
        {
            return color;
        }

        public bool mouseInKrug(int dx, int dy)
        {
            return (((dx - x) * (dx - x) + (dy - y) * (dy - y)) <= ((diam / 2) * (diam / 2)));
        }

        public void DrawKrug(krug k, Graphics g)
        {
            Pen pen;
            if (k.color == 1) pen = new Pen(Brushes.Blue);
            else pen = new Pen(Brushes.Red);
            g.DrawEllipse(pen, k.x - diam / 2, k.y - diam / 2, diam, diam);
        }
    }

    public class Node
    {
        public krug data;
        public Node next;

        public Node(krug value)
        {
            data = value;
        }
    }

    public class MyList
    {
        public Node Head;
        public Node Tail;
        public int count;
        public Node current;
        public MyList()
        {
            Head = null;
            Tail = null;
            current = Head;
            count = 0;
        }

        public MyList(krug value)
        {
            var node = new Node(value);
            Head = node;
            current = Head;
            Tail = node;
            count++;
        }

        public MyList(Node node)
        {
            Head = node;
            Tail = node;
            current = Head;
            count++;
        }



        public krug GetObj()
        {
            return current.data;
        }

        public bool Eol()
        {
            if (current == null)
                return false;

            return true;
        }

        public void First()
        {
            current = Head;
        }

        public void Next()
        {
            if (current != null)
                current = current.next;

        }

        public Node GetNode()
        {
            return current;
        }

        public Node GetNextNode()
        {
            return current.next;
        }

        public krug GetHeadObj()
        {
            return Head.data;
        }

        public void add(krug value)
        {
            var node = new Node(value);
            if (Head == null)
            {
                Head = node;
            }
            else
            {
                Tail.next = node;
            }
            Tail = node;
            count++;
        }




        public void delete(krug value)
        {

            if (Head != null)
            {
                if (Head.data.Equals(value))
                {
                    if (current.next != Tail && current.next != null)
                    {
                        Head = Head.next;
                        count--;

                        current = Head;
                        return;
                    }
                    else
                    {
                        if (current.next == Tail)
                        {
                            Head = Tail;
                            current = Head;
                            count--;
                            return;
                        }
                        else
                        {
                            Head = null;
                            Tail = null;
                            current = null;
                            return;
                        }
                    }
                }

                current = Head.next;
                var pred = Head;

                while (current != null)
                {
                    if (current.data.Equals(value))
                    {
                        if (current == Tail && pred == Head)
                        {
                            Tail = Head;
                            current = Head;
                            current.next = null;
                            return;
                        }
                        if (current.next == null)
                        {
                            Tail = pred;
                            count--;
                            current = Tail;
                            current.next = null;
                            return;
                        }
                        else
                        {
                            pred.next = current.next;
                            count--;

                            current = pred;
                            return;
                        }
                    }
                    pred = current;
                    current = current.next;
                }
            }
            return;
        }

        public bool nalichie()
        {

            if (Head != null)
            {
                return true;
            }
            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;


namespace TaskManager
{
    public partial class Form1 : Form
    {
        List<Process> proc;
        List<Process> nProc;
        public Form1()
        {
            InitializeComponent();
            InitListView();
            timer1.Start();
        }
        private void InitListView()
		{
			proc = Process.GetProcesses().ToList();
			foreach (var item in proc)
			{
				ListViewItem procName = new ListViewItem();
				procName.Name = "ProcessName";
				procName.Text = item.ProcessName;
				ListViewItem.ListViewSubItem pri = new ListViewItem.ListViewSubItem();
				try
				{
					pri.Name = "Priority";
					pri.Text = item.PriorityClass.ToString();
					procName.SubItems.Add(pri);
				}
				catch(Exception a)
				{
					pri.Name = "Priority";
					pri.Text = "";
                }
				ListViewItem.ListViewSubItem id = new ListViewItem.ListViewSubItem();
				ListViewItem.ListViewSubItem thrds = new ListViewItem.ListViewSubItem();
				id.Name = "Id";
				id.Text = item.Id.ToString();
				thrds.Name = "Threads";
				thrds.Text = item.Threads.Count.ToString();
				procName.SubItems.Add(id);
				procName.SubItems.Add(thrds);
				listView1.Items.Add(procName);
			}
		}

        private void timer1_Tick(object sender, EventArgs e)
        {
			nProc = Process.GetProcesses().ToList();
			IEnumerable<Process> r = null;
			if (nProc.Count > proc.Count)
			{
				r = nProc.Except(proc, new idc()).ToList();
				foreach (var item in r)
				{
					ListViewItem procName = new ListViewItem();
					procName.Name = "ProcessName";
					procName.Text = item.ProcessName;
					ListViewItem.ListViewSubItem pri = new ListViewItem.ListViewSubItem();
					try
					{
						pri.Name = "Priority";
						pri.Text = item.PriorityClass.ToString();
						procName.SubItems.Add(pri);
					}
					catch (Exception ex)
					{
						pri.Name = "Priority";
						pri.Text = "";
                    }
					ListViewItem.ListViewSubItem lvsi2 = new ListViewItem.ListViewSubItem();
					ListViewItem.ListViewSubItem lvsi3 = new ListViewItem.ListViewSubItem();
					lvsi2.Name = "Id";
					lvsi2.Text = item.Id.ToString();
					lvsi3.Name = "Threads";
					lvsi3.Text = item.Threads.Count.ToString();
					procName.SubItems.Add(lvsi2);
					procName.SubItems.Add(lvsi3);
                    listView1.Items.Add(procName);
				}
			}
			else
			{
				r = proc.Except(nProc, new idc()).ToList();
				foreach (Process p in r)
				{
                    foreach (ListViewItem i in listView1.Items)
					{
						foreach (ListViewItem.ListViewSubItem si in i.SubItems)
						{
							if (si.Name == "Id" && si.Text == p.Id.ToString()) listView1.Items.Remove(i);
						}
					}
				}
			}
			proc = nProc;
		}
    }
}
    

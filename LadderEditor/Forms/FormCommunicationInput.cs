using Devinno.Communications.Modbus;
using Devinno.Extensions;
using Devinno.Forms;
using Devinno.Forms.Controls;
using Devinno.Forms.Dialogs;
using Devinno.Forms.Themes;
using Devinno.PLC.Ladder;
using Devinno.Tools;
using LadderEditor.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LadderEditor.Forms
{
    public partial class FormCommunicationInput : DvForm
    {
        #region Member Variable
        LcModbusRtuSlave MDRS = new LcModbusRtuSlave();
        LcModbusRtuMaster MDRM = new LcModbusRtuMaster();
        LcModbusTcpSlave MDTS = new LcModbusTcpSlave();
        LcModbusTcpMaster MDTM = new LcModbusTcpMaster();
        LcMqtt MQTT = new LcMqtt();
        #endregion

        #region Constructor
        public FormCommunicationInput()
        {
            InitializeComponent();

            #region ComboBox
            var bauds = new int[] { 4800, 9600, 19200, 38400, 57600, 115200 };
            MDRM_inBaudrate.Items.AddRange(bauds.Select(x => new ComboBoxItem(x.ToString()) { Tag = x }));
            MDRS_inBaudrate.Items.AddRange(bauds.Select(x => new ComboBoxItem(x.ToString()) { Tag = x }));

            MDRS_inBaudrate.SelectedIndex = bauds.Length - 1;
            MDRM_inBaudrate.SelectedIndex = bauds.Length - 1;
            #endregion

            #region DataGrid
            var fns = new ModbusFunction[] { ModbusFunction.BITREAD_F1, ModbusFunction.BITREAD_F2, ModbusFunction.WORDREAD_F3, ModbusFunction.WORDREAD_F4 }.Select(x => new DvDataGridComboBoxItem(x.ToString().Replace("_", " [").Replace("BITREAD","Bit Read").Replace("WORDREAD", "Word Read") + "]") { Source = x }).ToList();
            var mods = new BindMode[] { BindMode.BitRead, BindMode.BitWrite, BindMode.WordRead, BindMode.WordWrite }.Select(x => new DvDataGridComboBoxItem(x.ToString()) { Source = x }).ToList();
            MDRM_dgMonitor.UseThemeColor = MDRM_dgBind.UseThemeColor = MDTM_dgMonitor.UseThemeColor = MDTM_dgBind.UseThemeColor = MQTT_dgPub.UseThemeColor = MQTT_dgSub.UseThemeColor = false;
            MDRM_dgMonitor.ColumnColor = MDRM_dgBind.ColumnColor = MDTM_dgMonitor.ColumnColor = MDTM_dgBind.ColumnColor = MQTT_dgPub.ColumnColor = MQTT_dgSub.ColumnColor = Color.FromArgb(30, 30, 30);

            MDRM_dgMonitor.AutoSet = true;
            MDRM_dgMonitor.SelectionMode = DvDataGridSelectionMode.SELECTOR;
            MDRM_dgMonitor.Columns.Add(new DvDataGridColumn(MDRM_dgMonitor) { Name = "Slave", HeaderText = "국번", SizeMode = SizeMode.Percent, Width = 15, CellType = typeof(DvDataGridEditNumberCell) });
            MDRM_dgMonitor.Columns.Add(new DvDataGridComboBoxColumn(MDRM_dgMonitor) { Name = "Func", HeaderText = "코드", SizeMode = SizeMode.Percent, Width = 40, Items = fns });
            MDRM_dgMonitor.Columns.Add(new DvDataGridColumn(MDRM_dgMonitor) { Name = "Address", HeaderText = "시작 주소", SizeMode = SizeMode.Percent, Width = 30, CellType = typeof(AddressCell) });
            MDRM_dgMonitor.Columns.Add(new DvDataGridColumn(MDRM_dgMonitor) { Name = "Length", HeaderText = "길이", SizeMode = SizeMode.Percent, Width = 15, CellType = typeof(DvDataGridEditNumberCell) });

            MDRM_dgBind.AutoSet = true;
            MDRM_dgBind.SelectionMode = DvDataGridSelectionMode.SELECTOR;
            MDRM_dgBind.Columns.Add(new DvDataGridColumn(MDRM_dgBind) { Name = "Slave", HeaderText = "국번", SizeMode = SizeMode.Percent, Width = 15, CellType = typeof(DvDataGridEditNumberCell) });
            MDRM_dgBind.Columns.Add(new DvDataGridComboBoxColumn(MDRM_dgBind) { Name = "Mode", HeaderText = "모드", SizeMode = SizeMode.Percent, Width = 35, Items = mods });
            MDRM_dgBind.Columns.Add(new DvDataGridColumn(MDRM_dgBind) { Name = "Address", HeaderText = "시작 주소", SizeMode = SizeMode.Percent, Width = 30, CellType = typeof(AddressCell) });
            MDRM_dgBind.Columns.Add(new DvDataGridColumn(MDRM_dgBind) { Name = "Bind", HeaderText = "바인딩", SizeMode = SizeMode.Percent, Width = 20, CellType = typeof(DvDataGridEditTextCell) });

            MDTM_dgMonitor.AutoSet = true;
            MDTM_dgMonitor.SelectionMode = DvDataGridSelectionMode.SELECTOR;
            MDTM_dgMonitor.Columns.Add(new DvDataGridColumn(MDTM_dgMonitor) { Name = "Slave", HeaderText = "국번", SizeMode = SizeMode.Percent, Width = 15, CellType = typeof(DvDataGridEditNumberCell) });
            MDTM_dgMonitor.Columns.Add(new DvDataGridComboBoxColumn(MDTM_dgMonitor) { Name = "Func", HeaderText = "코드", SizeMode = SizeMode.Percent, Width = 40, Items = fns });
            MDTM_dgMonitor.Columns.Add(new DvDataGridColumn(MDTM_dgMonitor) { Name = "Address", HeaderText = "시작 주소", SizeMode = SizeMode.Percent, Width = 30, CellType = typeof(AddressCell) });
            MDTM_dgMonitor.Columns.Add(new DvDataGridColumn(MDTM_dgMonitor) { Name = "Length", HeaderText = "길이", SizeMode = SizeMode.Percent, Width = 15, CellType = typeof(DvDataGridEditNumberCell) });

            MDTM_dgBind.AutoSet = true;
            MDTM_dgBind.SelectionMode = DvDataGridSelectionMode.SELECTOR;
            MDTM_dgBind.Columns.Add(new DvDataGridColumn(MDTM_dgBind) { Name = "Slave", HeaderText = "국번", SizeMode = SizeMode.Percent, Width = 15, CellType = typeof(DvDataGridEditNumberCell) });
            MDTM_dgBind.Columns.Add(new DvDataGridComboBoxColumn(MDTM_dgBind) { Name = "Mode", HeaderText = "모드", SizeMode = SizeMode.Percent, Width = 35, Items = mods });
            MDTM_dgBind.Columns.Add(new DvDataGridColumn(MDTM_dgBind) { Name = "Address", HeaderText = "시작 주소", SizeMode = SizeMode.Percent, Width = 30, CellType = typeof(AddressCell) });
            MDTM_dgBind.Columns.Add(new DvDataGridColumn(MDTM_dgBind) { Name = "Bind", HeaderText = "바인딩", SizeMode = SizeMode.Percent, Width = 20, CellType = typeof(DvDataGridEditTextCell) });

            MQTT_dgPub.AutoSet = true;
            MQTT_dgPub.SelectionMode = DvDataGridSelectionMode.SELECTOR;
            MQTT_dgPub.Columns.Add(new DvDataGridColumn(MQTT_dgPub) { Name = "Topic", HeaderText = "토픽", SizeMode = SizeMode.Percent, Width = 70, CellType = typeof(DvDataGridEditTextCell) });
            MQTT_dgPub.Columns.Add(new DvDataGridColumn(MQTT_dgPub) { Name = "Address", HeaderText = "메모리 주소", SizeMode = SizeMode.Percent, Width = 30, CellType = typeof(DvDataGridEditTextCell) });

            MQTT_dgSub.AutoSet = true;
            MQTT_dgSub.SelectionMode = DvDataGridSelectionMode.SELECTOR;
            MQTT_dgSub.Columns.Add(new DvDataGridColumn(MQTT_dgSub) { Name = "Topic", HeaderText = "토픽", SizeMode = SizeMode.Percent, Width = 70, CellType = typeof(DvDataGridEditTextCell) });
            MQTT_dgSub.Columns.Add(new DvDataGridColumn(MQTT_dgSub) { Name = "Address", HeaderText = "메모리 주소", SizeMode = SizeMode.Percent, Width = 30, CellType = typeof(DvDataGridEditTextCell) });
            #endregion

            #region Event
            #region tgl[MDRS/MDRM/MDTS/MDTM/MQTT].ButtonClick
            tglMDRS.ButtonClick += (o, s) => { tab.SelectedTab = tpMDRS; SetToggle(); };
            tglMDRM.ButtonClick += (o, s) => { tab.SelectedTab = tpMDRM; SetToggle(); };
            tglMDTS.ButtonClick += (o, s) => { tab.SelectedTab = tpMDTS; SetToggle(); };
            tglMDTM.ButtonClick += (o, s) => { tab.SelectedTab = tpMDTM; SetToggle(); };
            tglMQTT.ButtonClick += (o, s) => { tab.SelectedTab = tpMQTT; SetToggle(); };
            #endregion
            #region btn[OK/Cancel].ButtonClick
            btnOK.ButtonClick += (o, s) =>
            {
                var ret = ValidCheck();
                if (ret.Count == 0) DialogResult = DialogResult.OK;
                else
                {
                    var sb = new StringBuilder();
                    foreach (var v in ret) sb.AppendLine(v);
                    Program.MessageBox.ShowMessageBoxOk("입력 오류", sb.ToString());
                }
            };
            btnCancel.ButtonClick += (o, s) => DialogResult = DialogResult.Cancel;
            #endregion
            #region MDRS_lblArea[P/M/T/C/D/WP/WM].ButtonClick
            MDRS_lblAreaP.ButtonClick += (o, s) =>
            {
                var c = ((DvValueLabelButton)o);
                var r = InputBaseAddress(c.Text.Split(' ').FirstOrDefault() + "영역", c.Text);
                if (r.HasValue) c.Value = ValueTool.GetHexString(r.Value);
            };

            MDRS_lblAreaM.ButtonClick += (o, s) =>
            {
                var c = ((DvValueLabelButton)o);
                var r = InputBaseAddress(c.Text.Split(' ').FirstOrDefault() + "영역", c.Text);
                if (r.HasValue) c.Value = ValueTool.GetHexString(r.Value);
            };

            MDRS_lblAreaT.ButtonClick += (o, s) =>
            {
                var c = ((DvValueLabelButton)o);
                var r = InputBaseAddress(c.Text.Split(' ').FirstOrDefault() + "영역", c.Text);
                if (r.HasValue) c.Value = ValueTool.GetHexString(r.Value);
            };

            MDRS_lblAreaC.ButtonClick += (o, s) =>
            {
                var c = ((DvValueLabelButton)o);
                var r = InputBaseAddress(c.Text.Split(' ').FirstOrDefault() + "영역", c.Text);
                if (r.HasValue) c.Value = ValueTool.GetHexString(r.Value);
            };

            MDRS_lblAreaD.ButtonClick += (o, s) =>
            {
                var c = ((DvValueLabelButton)o);
                var r = InputBaseAddress(c.Text.Split(' ').FirstOrDefault() + "영역", c.Text);
                if (r.HasValue) c.Value = ValueTool.GetHexString(r.Value);
            };

            MDRS_lblAreaWP.ButtonClick += (o, s) =>
            {
                var c = ((DvValueLabelButton)o);
                var r = InputBaseAddress(c.Text.Split(' ').FirstOrDefault() + "영역", c.Text);
                if (r.HasValue) c.Value = ValueTool.GetHexString(r.Value);
            };

            MDRS_lblAreaWM.ButtonClick += (o, s) =>
            {
                var c = ((DvValueLabelButton)o);
                var r = InputBaseAddress(c.Text.Split(' ').FirstOrDefault() + "영역", c.Text);
                if (r.HasValue) c.Value = ValueTool.GetHexString(r.Value);
            };
            #endregion
            #region MDTS_lblArea[P/M/T/C/D/WP/WM].ButtonClick
            MDTS_lblAreaP.ButtonClick += (o, s) =>
            {
                var c = ((DvValueInputButton)o);
                var r = InputBaseAddress(c.Text.Split(' ').FirstOrDefault() + "영역", c.Text);
                if (r.HasValue) c.Value = ValueTool.GetHexString(r.Value);
            };

            MDTS_lblAreaM.ButtonClick += (o, s) =>
            {
                var c = ((DvValueInputButton)o);
                var r = InputBaseAddress(c.Text.Split(' ').FirstOrDefault() + "영역", c.Text);
                if (r.HasValue) c.Value = ValueTool.GetHexString(r.Value);
            };

            MDTS_lblAreaT.ButtonClick += (o, s) =>
            {
                var c = ((DvValueInputButton)o);
                var r = InputBaseAddress(c.Text.Split(' ').FirstOrDefault() + "영역", c.Text);
                if (r.HasValue) c.Value = ValueTool.GetHexString(r.Value);
            };

            MDTS_lblAreaC.ButtonClick += (o, s) =>
            {
                var c = ((DvValueInputButton)o);
                var r = InputBaseAddress(c.Text.Split(' ').FirstOrDefault() + "영역", c.Text);
                if (r.HasValue) c.Value = ValueTool.GetHexString(r.Value);
            };

            MDTS_lblAreaD.ButtonClick += (o, s) =>
            {
                var c = ((DvValueInputButton)o);
                var r = InputBaseAddress(c.Text.Split(' ').FirstOrDefault() + "영역", c.Text);
                if (r.HasValue) c.Value = ValueTool.GetHexString(r.Value);
            };

            MDTS_lblAreaWP.ButtonClick += (o, s) =>
            {
                var c = ((DvValueInputButton)o);
                var r = InputBaseAddress(c.Text.Split(' ').FirstOrDefault() + "영역", c.Text);
                if (r.HasValue) c.Value = ValueTool.GetHexString(r.Value);
            };

            MDTS_lblAreaWM.ButtonClick += (o, s) =>
            {
                var c = ((DvValueInputButton)o);
                var r = InputBaseAddress(c.Text.Split(' ').FirstOrDefault() + "영역", c.Text);
                if (r.HasValue) c.Value = ValueTool.GetHexString(r.Value);
            };
            #endregion
            #region MDRM_btnMonitor[Add/Del].ButtonClick
            MDRM_btnMonitorAdd.ButtonClick += (o, s) =>
            {
                var v = MDRM;

                v.Monitors.Add(new ModbusMonitor());

                var vpos1 = MDRM_dgMonitor.VScrollPosition;
                MDRM_dgMonitor.SetDataSource<ModbusMonitor>(v.Monitors);
                MDRM_dgMonitor.VScrollPosition = vpos1;
                MDRM_dgMonitor.Invalidate();
            };
            
            MDRM_btnMonitorDel.ButtonClick += (o, s) =>
            {
                var v = MDRM;

                var sels = MDRM_dgMonitor.Rows.Where(x => x.Selected).Select(x => x.Source as ModbusMonitor);
                foreach (var itm in sels) v.Monitors.Remove(itm);

                var vpos1 = MDRM_dgMonitor.VScrollPosition;
                MDRM_dgMonitor.SetDataSource<ModbusMonitor>(v.Monitors);
                MDRM_dgMonitor.VScrollPosition = vpos1;
                MDRM_dgMonitor.Invalidate();
            };
            #endregion
            #region MDRM_btnBind[Add/Del].ButtonClick
            MDRM_btnBindAdd.ButtonClick += (o, s) =>
            {
                var v = MDRM;

                v.Binds.Add(new ModbusBind());

                var vpos1 = MDRM_dgBind.VScrollPosition;
                MDRM_dgBind.SetDataSource<ModbusBind>(v.Binds);
                MDRM_dgBind.VScrollPosition = vpos1;
                MDRM_dgBind.Invalidate();
            };

            MDRM_btnBindDel.ButtonClick += (o, s) =>
            {
                var v = MDRM;

                var sels = MDRM_dgBind.Rows.Where(x => x.Selected).Select(x => x.Source as ModbusBind);
                foreach (var itm in sels) v.Binds.Remove(itm);

                var vpos1 = MDRM_dgBind.VScrollPosition;
                MDRM_dgBind.SetDataSource<ModbusBind>(v.Binds);
                MDRM_dgBind.VScrollPosition = vpos1;
                MDRM_dgBind.Invalidate();
            };
            #endregion
            #region MDTM_btnMonitor[Add/Del].ButtonClick
            MDTM_btnMonitorAdd.ButtonClick += (o, s) =>
            {
                var v = MDTM;

                v.Monitors.Add(new ModbusMonitor());

                var vpos1 = MDTM_dgMonitor.VScrollPosition;
                MDTM_dgMonitor.SetDataSource<ModbusMonitor>(v.Monitors);
                MDTM_dgMonitor.VScrollPosition = vpos1;
                MDTM_dgMonitor.Invalidate();
            };

            MDTM_btnMonitorDel.ButtonClick += (o, s) =>
            {
                var v = MDTM;

                var sels = MDTM_dgMonitor.Rows.Where(x => x.Selected).Select(x => x.Source as ModbusMonitor);
                foreach (var itm in sels) v.Monitors.Remove(itm);

                var vpos1 = MDTM_dgMonitor.VScrollPosition;
                MDTM_dgMonitor.SetDataSource<ModbusMonitor>(v.Monitors);
                MDTM_dgMonitor.VScrollPosition = vpos1;
                MDTM_dgMonitor.Invalidate();
            };
            #endregion
            #region MDTM_btnBind[Add/Del].ButtonClick
            MDTM_btnBindAdd.ButtonClick += (o, s) =>
            {
                var v = MDTM;

                v.Binds.Add(new ModbusBind());

                var vpos1 = MDTM_dgBind.VScrollPosition;
                MDTM_dgBind.SetDataSource<ModbusBind>(v.Binds);
                MDTM_dgBind.VScrollPosition = vpos1;
                MDTM_dgBind.Invalidate();
            };

            MDTM_btnBindDel.ButtonClick += (o, s) =>
            {
                var v = MDTM;

                var sels = MDTM_dgBind.Rows.Where(x => x.Selected).Select(x => x.Source as ModbusBind);
                foreach (var itm in sels) v.Binds.Remove(itm);

                var vpos1 = MDTM_dgBind.VScrollPosition;
                MDTM_dgBind.SetDataSource<ModbusBind>(v.Binds);
                MDTM_dgBind.VScrollPosition = vpos1;
                MDTM_dgBind.Invalidate();
            };
            #endregion
            #region MQTT_btnSub[Add/Del].ButtonClick
            MQTT_btnSubAdd.ButtonClick += (o, s) =>
            {
                var v = MQTT;

                v.Subs.Add(new MqttPubSub());

                var vpos1 = MQTT_dgSub.VScrollPosition;
                MQTT_dgSub.SetDataSource(v.Subs);
                MQTT_dgSub.VScrollPosition = vpos1;
                MQTT_dgSub.Invalidate();
            };

            MQTT_btnSubDel.ButtonClick += (o, s) =>
            {
                var v = MQTT;

                var sels = MQTT_dgSub.Rows.Where(x => x.Selected).Select(x => x.Source as MqttPubSub);
                foreach (var itm in sels) v.Subs.Remove(itm);

                var vpos1 = MQTT_dgSub.VScrollPosition;
                MQTT_dgSub.SetDataSource(v.Subs);
                MQTT_dgSub.VScrollPosition = vpos1;
                MQTT_dgSub.Invalidate();
            };
            #endregion
            #region MQTT_btnPub[Add/Del].ButtonClick
            MQTT_btnPubAdd.ButtonClick += (o, s) =>
            {
                var v = MQTT;

                v.Pubs.Add(new MqttPubSub());

                var vpos1 = MQTT_dgPub.VScrollPosition;
                MQTT_dgPub.SetDataSource(v.Pubs);
                MQTT_dgPub.VScrollPosition = vpos1;
                MQTT_dgPub.Invalidate();
            };

            MQTT_btnPubDel.ButtonClick += (o, s) =>
            {
                var v = MQTT;

                var sels = MQTT_dgPub.Rows.Where(x => x.Selected).Select(x => x.Source as MqttPubSub);
                foreach (var itm in sels) v.Pubs.Remove(itm);

                var vpos1 = MQTT_dgPub.VScrollPosition;
                MQTT_dgPub.SetDataSource(v.Pubs);
                MQTT_dgPub.VScrollPosition = vpos1;
                MQTT_dgPub.Invalidate();
            };
            #endregion
            #endregion

            #region Form Props
            StartPosition = FormStartPosition.CenterParent;
            this.Icon = Tools.IconTool.GetIcon(new Devinno.Forms.Icons.DvIcon(TitleIconString, Convert.ToInt32(TitleIconSize)), Program.ICO_WH, Program.ICO_WH, Color.White);
            #endregion
        }
        #endregion

        #region Method
        #region InputBaseAddress
        int? InputBaseAddress(string Title, string Text)
        {
            int n2 = 0;
            var str2 = Text;
            bool b = false;
            if (str2 != null)
            {
                var r = ValueTool.GetHexValue(str2);
                b = r.HasValue;
                if (r.HasValue) n2 = r.Value;
            }

            int? ret = null;
            var str = Program.InputBox.ShowString(Title, "시작 주소", b ? "0x" + n2.ToString("X4") : null);
            if (str != null)
            {
                var r = ValueTool.GetHexValue(str);
                if (r.HasValue) ret = r.Value;
            }
            return ret;
        }
        #endregion
        #region SetToggle
        void SetToggle()
        {
            #region Toggle
            tglMDRS.Checked = tab.SelectedTab == tpMDRS;
            tglMDRM.Checked = tab.SelectedTab == tpMDRM;
            tglMDTS.Checked = tab.SelectedTab == tpMDTS;
            tglMDTM.Checked = tab.SelectedTab == tpMDTM;
            tglMQTT.Checked = tab.SelectedTab == tpMQTT;
            #endregion

            if (tab.SelectedTab == tpMDRS) pnlContent.Text = MDRS.Name;
            if (tab.SelectedTab == tpMDRM) pnlContent.Text = MDRM.Name;
            if (tab.SelectedTab == tpMDTS) pnlContent.Text = MDTS.Name;
            if (tab.SelectedTab == tpMDTM) pnlContent.Text = MDTM.Name;
            if (tab.SelectedTab == tpMQTT) pnlContent.Text = MQTT.Name;

        }
        #endregion
        #region SetUI
        void SetUI()
        {
            SetToggle();

            #region MDRS
            {
                var v = MDRS;

                MDRS_inPort.Value = v.Port;
                MDRS_inBaudrate.SelectedIndex = MDRS_inBaudrate.Items.Select(x => (int)x.Tag).ToList().IndexOf(v.Baudrate);
                MDRS_inSlave.Value = v.Slave.ToString();
                MDRS_lblAreaP.Value = ValueTool.GetHexString(v.P_BaseAddress);
                MDRS_lblAreaM.Value = ValueTool.GetHexString(v.M_BaseAddress);
                MDRS_lblAreaT.Value = ValueTool.GetHexString(v.T_BaseAddress);
                MDRS_lblAreaC.Value = ValueTool.GetHexString(v.C_BaseAddress);
                MDRS_lblAreaD.Value = ValueTool.GetHexString(v.D_BaseAddress);
                MDRS_lblAreaWP.Value = ValueTool.GetHexString(v.WP_BaseAddress);
                MDRS_lblAreaWM.Value = ValueTool.GetHexString(v.WM_BaseAddress);
            }
            #endregion
            #region MDRM
            {
                var v = MDRM;

                MDRM_inPort.Value = v.Port;
                MDRM_inBaudrate.SelectedIndex = MDRM_inBaudrate.Items.Select(x => (int)x.Tag).ToList().IndexOf(v.Baudrate);

                var vpos1 = MDRM_dgMonitor.VScrollPosition;
                MDRM_dgMonitor.SetDataSource<ModbusMonitor>(v.Monitors);
                MDRM_dgMonitor.VScrollPosition = vpos1;
                MDRM_dgMonitor.Invalidate();

                var vpos2 = MDRM_dgBind.VScrollPosition;
                MDRM_dgBind.SetDataSource<ModbusBind>(v.Binds);
                MDRM_dgBind.VScrollPosition = vpos2;
                MDRM_dgBind.Invalidate();
            }
            #endregion
            #region MDTS
            {
                var v = MDTS;

                MDTS_inSlave.Value = v.Slave.ToString();
                MDTS_lblAreaP.Value = ValueTool.GetHexString(v.P_BaseAddress);
                MDTS_lblAreaM.Value = ValueTool.GetHexString(v.M_BaseAddress);
                MDTS_lblAreaT.Value = ValueTool.GetHexString(v.T_BaseAddress);
                MDTS_lblAreaC.Value = ValueTool.GetHexString(v.C_BaseAddress);
                MDTS_lblAreaD.Value = ValueTool.GetHexString(v.D_BaseAddress);
                MDTS_lblAreaWP.Value = ValueTool.GetHexString(v.WP_BaseAddress);
                MDTS_lblAreaWM.Value = ValueTool.GetHexString(v.WM_BaseAddress);
            }
            #endregion
            #region MDTM
            {
                var v = MDTM;

                MDTM_inRemoteIP.Value = v.RemoteIP;

                var vpos1 = MDTM_dgMonitor.VScrollPosition;
                MDTM_dgMonitor.SetDataSource<ModbusMonitor>(v.Monitors);
                MDTM_dgMonitor.VScrollPosition = vpos1;
                MDTM_dgMonitor.Invalidate();

                var vpos2 = MDTM_dgBind.VScrollPosition;
                MDTM_dgBind.SetDataSource<ModbusBind>(v.Binds);
                MDTM_dgBind.VScrollPosition = vpos2;
                MDTM_dgBind.Invalidate();
            }
            #endregion
            #region MQTT
            {
                var v = MQTT;

                MQTT_inRemoteIP.Value = v.BrokerIP;

                var vpos1 = MQTT_dgPub.VScrollPosition;
                MQTT_dgPub.SetDataSource<MqttPubSub>(v.Pubs);
                MQTT_dgPub.VScrollPosition = vpos1;
                MQTT_dgPub.Invalidate();

                var vpos2 = MQTT_dgSub.VScrollPosition;
                MQTT_dgSub.SetDataSource<MqttPubSub>(v.Subs);
                MQTT_dgSub.VScrollPosition = vpos2;
                MQTT_dgSub.Invalidate();
            }
            #endregion
        }
        #endregion

        #region Commit
        void Commit()
        {
            #region MDRS
            if (tab.SelectedTab == tpMDRS)
            {
                MDRS.Port = MDRS_inPort.Value;
                MDRS.Baudrate = (int)MDRS_inBaudrate.Items[MDRS_inBaudrate.SelectedIndex].Tag;
                MDRS.Slave = Convert.ToInt32(MDRS_inSlave.Value);

                if (MDRS_lblAreaP.UseButton) MDRS.P_BaseAddress = ValueTool.GetHexValue(MDRS_lblAreaP.Value).Value;
                if (MDRS_lblAreaM.UseButton) MDRS.M_BaseAddress = ValueTool.GetHexValue(MDRS_lblAreaM.Value).Value;
                if (MDRS_lblAreaT.UseButton) MDRS.T_BaseAddress = ValueTool.GetHexValue(MDRS_lblAreaT.Value).Value;
                if (MDRS_lblAreaC.UseButton) MDRS.C_BaseAddress = ValueTool.GetHexValue(MDRS_lblAreaC.Value).Value;
                if (MDRS_lblAreaD.UseButton) MDRS.D_BaseAddress = ValueTool.GetHexValue(MDRS_lblAreaD.Value).Value;
                if (MDRS_lblAreaWP.UseButton) MDRS.WP_BaseAddress = ValueTool.GetHexValue(MDRS_lblAreaWP.Value).Value;
                if (MDRS_lblAreaWM.UseButton) MDRS.WM_BaseAddress = ValueTool.GetHexValue(MDRS_lblAreaWM.Value).Value;
            }
            #endregion
            #region MDRM
            else if (tab.SelectedTab == tpMDRM)
            {
                MDRM.Port = MDRM_inPort.Value;
                MDRM.Baudrate = (int)MDRM_inBaudrate.Items[MDRS_inBaudrate.SelectedIndex].Tag;
            }
            #endregion
            #region MDTS
            else if (tab.SelectedTab == tpMDTS)
            {
                MDTS.Slave = Convert.ToInt32(MDTS_inSlave.Value);

                if (MDTS_lblAreaP.UseButton) MDTS.P_BaseAddress = ValueTool.GetHexValue(MDTS_lblAreaP.Value).Value;
                if (MDTS_lblAreaM.UseButton) MDTS.M_BaseAddress = ValueTool.GetHexValue(MDTS_lblAreaM.Value).Value;
                if (MDTS_lblAreaT.UseButton) MDTS.T_BaseAddress = ValueTool.GetHexValue(MDTS_lblAreaT.Value).Value;
                if (MDTS_lblAreaC.UseButton) MDTS.C_BaseAddress = ValueTool.GetHexValue(MDTS_lblAreaC.Value).Value;
                if (MDTS_lblAreaD.UseButton) MDTS.D_BaseAddress = ValueTool.GetHexValue(MDTS_lblAreaD.Value).Value;
                if (MDTS_lblAreaWP.UseButton) MDTS.WP_BaseAddress = ValueTool.GetHexValue(MDTS_lblAreaWP.Value).Value;
                if (MDTS_lblAreaWM.UseButton) MDTS.WM_BaseAddress = ValueTool.GetHexValue(MDTS_lblAreaWM.Value).Value;
            }
            #endregion
            #region MDTM
            if (tab.SelectedTab == tpMDTM)
            {
                MDTM.RemoteIP = MDTM_inRemoteIP.Value;
            }
            #endregion
            #region MQTT
            if (tab.SelectedTab == tpMQTT)
            {
                MQTT.BrokerIP = MQTT_inRemoteIP.Value;
            }
            #endregion
        }
        #endregion
        #region GetResult
        ILadderComm GetResult()
        {
            ILadderComm ret = null;

            try
            {
                if (ValidCheck().Count == 0)
                {
                    Commit();
                
                    if (tab.SelectedTab == tpMDRS) ret = MDRS;
                    else if (tab.SelectedTab == tpMDRM) ret = MDRM;
                    else if (tab.SelectedTab == tpMDTS) ret = MDTS;
                    else if (tab.SelectedTab == tpMDTM) ret = MDTM;
                    else if (tab.SelectedTab == tpMQTT) ret = MQTT;
                }
            }
            catch { }

            return ret;
        }
        #endregion
        #region ValidCheck
        List<string> ValidCheck()
        {
            var ret = new List<string>();

            if(tab.SelectedTab == tpMDRS)
            {
                var n = 0;
                var c1 = !string.IsNullOrWhiteSpace(MDRS_inPort.Value);
                var c2 = MDRS_inBaudrate.SelectedIndex >= 0;
                var c3 = int.TryParse(MDRS_inSlave.Value, out n) && n >= 0 && n <= 255;
                var c4 = ValueTool.GetHexValue(MDRS_lblAreaP.Value).HasValue;
                var c5 = ValueTool.GetHexValue(MDRS_lblAreaM.Value).HasValue;
                var c6 = ValueTool.GetHexValue(MDRS_lblAreaT.Value).HasValue;
                var c7 = ValueTool.GetHexValue(MDRS_lblAreaC.Value).HasValue;
                var c8 = ValueTool.GetHexValue(MDRS_lblAreaD.Value).HasValue;
                var c9 = ValueTool.GetHexValue(MDRS_lblAreaWP.Value).HasValue;
                var c10 = ValueTool.GetHexValue(MDRS_lblAreaWM.Value).HasValue;

                if (!c1) ret.Add("· 통신 포트를 입력하세요.");
                if (!c2) ret.Add("· 통신 속도를 입력하세요.");
                if (!c3) ret.Add("· 국번은 0~255 사이 숫자로 입력하여야 합니다.");
                if (!c4) ret.Add("· P 영역 시작주소를 입력하세요.");
                if (!c5) ret.Add("· M 영역 시작주소를 입력하세요.");
                if (!c6) ret.Add("· T 영역 시작주소를 입력하세요.");
                if (!c7) ret.Add("· C 영역 시작주소를 입력하세요.");
                if (!c8) ret.Add("· D 영역 시작주소를 입력하세요.");
                if (!c9) ret.Add("· WP 영역 시작주소를 입력하세요.");
                if (!c10) ret.Add("· WM 영역 시작주소를 입력하세요.");
            }
            else if(tab.SelectedTab == tpMDRM)
            {
                var c1 = !string.IsNullOrWhiteSpace(MDRM_inPort.Value);
                var c2 = MDRM_inBaudrate.SelectedIndex >= 0;

                if (!c1) ret.Add("· 통신 포트를 입력하세요.");
                if (!c2) ret.Add("· 통신 속도를 입력하세요.");
            }
            else if (tab.SelectedTab == tpMDTS)
            {
                var n = 0;
                var c1 = int.TryParse(MDTS_inSlave.Value, out n) && n >= 0 && n <= 255;
                var c2 = ValueTool.GetHexValue(MDTS_lblAreaP.Value).HasValue;
                var c3 = ValueTool.GetHexValue(MDTS_lblAreaM.Value).HasValue;
                var c4 = ValueTool.GetHexValue(MDTS_lblAreaT.Value).HasValue;
                var c5 = ValueTool.GetHexValue(MDTS_lblAreaC.Value).HasValue;
                var c6 = ValueTool.GetHexValue(MDTS_lblAreaD.Value).HasValue;
                var c7 = ValueTool.GetHexValue(MDTS_lblAreaWP.Value).HasValue;
                var c8 = ValueTool.GetHexValue(MDTS_lblAreaWM.Value).HasValue;

                if (!c1) ret.Add("· 국번은 0~255 사이 숫자로 입력하여야 합니다.");
                if (!c2) ret.Add("· P 영역 시작주소를 입력하세요.");
                if (!c3) ret.Add("· M 영역 시작주소를 입력하세요.");
                if (!c4) ret.Add("· T 영역 시작주소를 입력하세요.");
                if (!c5) ret.Add("· C 영역 시작주소를 입력하세요.");
                if (!c6) ret.Add("· D 영역 시작주소를 입력하세요.");
                if (!c7) ret.Add("· WP 영역 시작주소를 입력하세요.");
                if (!c8) ret.Add("· WM 영역 시작주소를 입력하세요.");
            }
            else if (tab.SelectedTab == tpMDTM)
            {
                var c1 = NetworkTool.ValidIPv4(MDTM_inRemoteIP.Value);

                if (!c1) ret.Add("· 원격 주소를 IPv4 형식으로 입력하여야 합니다.");
            }
            else if (tab.SelectedTab == tpMQTT)
            {
                var c1 = NetworkTool.ValidIPv4(MQTT_inRemoteIP.Value);

                if (!c1) ret.Add("· 원격 주소를 IPv4 형식으로 입력하여야 합니다.");
            }

            return ret;
        }
        #endregion

        #region ShowCommInputAdd
        public ILadderComm ShowCommInputAdd()
        {
            #region Set
            this.Title = this.Text = "통신 추가";
            #endregion
            #region Var Set
            MDRS = new LcModbusRtuSlave();
            MDRM = new LcModbusRtuMaster();
            MDTS = new LcModbusTcpSlave();
            MDTM = new LcModbusTcpMaster();
            MQTT = new LcMqtt();

            SetUI();
            #endregion

            ILadderComm ret = null;
            if(this.ShowDialog() == DialogResult.OK)
            {
                ret = GetResult();
            }
            return ret;
        }
        #endregion
        #region ShowCommInputModify
        public ILadderComm ShowCommInputModify(ILadderComm v)
        {
            #region Set
            this.Title = this.Text = "통신 수정";
            #endregion
            #region Var Set
            MDRS = new LcModbusRtuSlave();
            MDRM = new LcModbusRtuMaster();
            MDTS = new LcModbusTcpSlave();
            MDTM = new LcModbusTcpMaster();
            MQTT = new LcMqtt();

            #region MDRS
            if (v is LcModbusRtuSlave)
            {
                var vm = v as LcModbusRtuSlave;
                var tm = MDRS;

                tm.Port = vm.Port;
                tm.Baudrate = vm.Baudrate;
                tm.Slave = vm.Slave;
                tm.P_BaseAddress = vm.P_BaseAddress;
                tm.M_BaseAddress = vm.M_BaseAddress;
                tm.T_BaseAddress = vm.T_BaseAddress;
                tm.C_BaseAddress = vm.C_BaseAddress;
                tm.D_BaseAddress = vm.D_BaseAddress;
                tm.WP_BaseAddress = vm.WP_BaseAddress;
                tm.WM_BaseAddress = vm.WM_BaseAddress;

                tab.SelectedTab = tpMDRS;
            }
            #endregion
            #region MDRM
            else if (v is LcModbusRtuMaster)
            {
                var vm = v as LcModbusRtuMaster;
                var tm = MDRM;

                tm.Port = vm.Port;
                tm.Baudrate = vm.Baudrate;
                tm.Monitors = vm.Monitors.Select(x => new ModbusMonitor()
                {
                    Slave = x.Slave,
                    Address = x.Address,
                    Func = x.Func,
                    Length = x.Length
                }).ToList();

                tm.Binds = vm.Binds.Select(x => new ModbusBind()
                {
                    Slave = x.Slave,
                    Address = x.Address,
                    Mode = x.Mode,
                    Bind = x.Bind
                }).ToList();

                tab.SelectedTab = tpMDRM;
            }
            #endregion
            #region MDTS
            else if (v is LcModbusTcpSlave)
            {
                var vm = v as LcModbusTcpSlave;
                var tm = MDTS;
                
                tm.Slave = vm.Slave;
                tm.P_BaseAddress = vm.P_BaseAddress;
                tm.M_BaseAddress = vm.M_BaseAddress;
                tm.T_BaseAddress = vm.T_BaseAddress;
                tm.C_BaseAddress = vm.C_BaseAddress;
                tm.D_BaseAddress = vm.D_BaseAddress;
                tm.WP_BaseAddress = vm.WP_BaseAddress;
                tm.WM_BaseAddress = vm.WM_BaseAddress;

                tab.SelectedTab = tpMDTS;
            }
            #endregion
            #region MDTM
            else if (v is LcModbusTcpMaster)
            {
                var vm = v as LcModbusTcpMaster;
                var tm = MDTM;

                tm.RemoteIP = vm.RemoteIP;
                tm.Monitors = vm.Monitors.Select(x => new ModbusMonitor()
                {
                    Slave = x.Slave,
                    Address = x.Address,
                    Func = x.Func,
                    Length = x.Length
                }).ToList();

                tm.Binds = vm.Binds.Select(x => new ModbusBind()
                {
                    Slave = x.Slave,
                    Address = x.Address,
                    Mode = x.Mode,
                    Bind = x.Bind
                }).ToList();

                tab.SelectedTab = tpMDTM;
            }
            #endregion
            #region MQTT
            else if (v is LcMqtt)
            {
                var vm = v as LcMqtt;
                var tm = MQTT;

                tm.BrokerIP = vm.BrokerIP;

                tm.Pubs = vm.Pubs.Select(x => new MqttPubSub() { Address = x.Address, Topic = x.Topic }).ToList();
                tm.Subs = vm.Subs.Select(x => new MqttPubSub() { Address = x.Address, Topic = x.Topic }).ToList();

                tab.SelectedTab = tpMQTT;
            }
            #endregion

            SetUI();
            #endregion

            ILadderComm ret = null;
            if (this.ShowDialog() == DialogResult.OK)
            {
                ret = GetResult();
            }
            return ret;
        }
        #endregion
        #endregion
    }

    #region Cell
    public class AddressCell : DvDataGridCell
    {
        #region Properties
        public bool ReadOnly { get; set; }
        #endregion
        #region Constructor
        public AddressCell(DvDataGrid Grid, DvDataGridRow Row, DvDataGridColumn Column) : base(Grid, Row, Column)
        {
        }
        #endregion
        #region Override
        #region CellPaint
        public override void CellPaint(DvTheme Theme, Graphics g, Rectangle CellBounds)
        {
            var f = Grid.DpiRatio;
            var cTextBack = CellBackColor.BrightnessTransmit(-0.2);
            var rt = new Rectangle(CellBounds.X, CellBounds.Y, CellBounds.Width, CellBounds.Height); rt.Inflate(-Convert.ToInt32(2 * f), -Convert.ToInt32(2 * f));
            Theme.DrawBox(g, cTextBack, cTextBack, rt, RoundType.NONE, BoxDrawOption.BORDER);
            if (Value != null)
            {
                var s = "";

                if (Value is int)
                {
                    var v = (int)Value;
                    s = $"0x{v.ToString("X4")}";
                }
                
                if (!string.IsNullOrWhiteSpace(s))
                {
                    var c = CellTextColor;
                    var bg = (Row.Selected ? SelectedCellBackColor : CellBackColor);
                    if (Grid.TextShadow) Theme.DrawTextShadow(g, null, s, Grid.Font, c, bg, rt);
                    else Theme.DrawText(g, null, s, Grid.Font, c, bg, rt);
                }
            }
            base.CellPaint(Theme, g, CellBounds);
        }
        #endregion
        #region CellMouseUp
        public override void CellMouseUp(Rectangle CellBounds, int x, int y)
        {
            if (CollisionTool.Check(CellBounds, x, y))
            {
                var frm = Grid.FindForm() as DvForm;
                if (frm != null) frm.Block = true;
                var ret = Program.InputBox.ShowString(Column.HeaderText, "입력", "0x" + ((int)Value).ToString("X4"));
                int n;
                if (ret != null)
                {
                    var r = ValueTool.GetHexValue(ret);
                    if(r.HasValue)
                    {
                        var v = r.Value;
                        if (v != (int)Value)
                        {
                            var old = Value;
                            Value = v;
                            Grid.InvokeValueChanged(this, old, v);
                        }
                    }
                }
                if (frm != null) frm.Block = false;
            }

            base.CellMouseUp(CellBounds, x, y);
        }
        #endregion
        #endregion
    }
    #endregion
}

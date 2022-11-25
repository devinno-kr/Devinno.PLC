using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devinno.PLC.Ladder
{
    #region class : LadderItem
    public class LadderItem : IComparable
    {
        #region Properties
        public int Row { get; set; } = 0;
        public int Col { get; set; } = 0;
        public LadderItemType ItemType { get; set; } = LadderItemType.NONE;
        public string Code { get; set; } = "";
        public bool VerticalLine { get; set; } = false;
        public string PositionText => $"X:{Col + 1},Y:{Row + 1}";

        public string Key => Row + "," + Col;

        [JsonIgnore]
        public bool Monitor { get; set; }

        [JsonIgnore]
        public bool MonitorV { get; set; }

        [JsonIgnore]
        public bool VerticalMonitorV { get; set; }

        [JsonIgnore]
        public bool PrevMonitorV { get; set; }

        [JsonIgnore]
        public int Watch { get; set; }

        [JsonIgnore]
        public long WatchL { get; set; }

        [JsonIgnore]
        public string WatchT { get; set; }

        [JsonIgnore]
        public float WatchF { get; set; }

        [JsonIgnore]
        public int Timer { get; set; }

        public bool Expand { get; set; } = true;
        #endregion

        #region Constructor
        public LadderItem()
        {
            

        }
        #endregion

        #region Method
        #region Clone
        public LadderItem Clone()
        {
            return new LadderItem()
            {
                Row = this.Row,
                Col = this.Col,
                ItemType = this.ItemType,
                VerticalLine = this.VerticalLine,
                Code = this.Code,
                Expand = this.Expand,
            };
        }
        public LadderItem CloneWithPositionChange(int row, int col)
        {
            return new LadderItem()
            {
                Row = row,
                Col = col,
                ItemType = this.ItemType,
                Code = this.Code,
                VerticalLine = this.VerticalLine,
                Expand = this.Expand,
            };
        }
        #endregion
        #region Copy
        public void Copy(LadderItem v)
        {
            this.Row = v.Row;
            this.Col = v.Col;
            this.ItemType = ItemType;
            this.VerticalLine = v.VerticalLine;
            this.Code = v.Code;
            this.Expand = v.Expand;
        }
        #endregion
        #region CompareTo
        public int CompareTo(object o)
        {
            LadderItem target = o as LadderItem;
            if (this.Row.CompareTo(target.Row) == 0) return this.Col.CompareTo(target.Col);
            else return this.Row.CompareTo(target.Row);
        }
        #endregion
        #endregion
    }
    #endregion

    #region enum : LadderItemType
    public enum LadderItemType
    {
        NONE, LINE_H, LINE_V, IN_A, IN_B, OUT_COIL, OUT_FUNC, RISING_EDGE, FALLING_EDGE, NOT
    }
    #endregion
}

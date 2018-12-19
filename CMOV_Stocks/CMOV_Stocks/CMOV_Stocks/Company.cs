using System;
using System.ComponentModel;
using SQLite;

namespace CMOV_Stocks
{
    public class Company : INotifyPropertyChanged, IEquatable<Company> {
		[PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private bool selected;
        public bool Selected {
            get { return selected; }
            set {
                selected = value;
                if (PropertyChanged != null) {
                    PropertyChanged(this, new PropertyChangedEventArgs("Selected"));
                }
            }
        }

        public Company() {
            ID = 0;
            Code = "";
            Name = "";
            Selected = false;
        }

        public Company(string _code, string _name) {
            Code = _code;
            Name = _name;
            Selected = false;
        }

        public Company(int _id, string _code, string _name) {
            ID = _id;
            Code = _code;
            Name = _name;
            Selected = false;
        }

        public bool Equals(Company other) {
            if (other == null)
                return false;
            if (Code == other.Code)
                return true;
            else
                return false;
        }

        
        public override string ToString() {
            return Code + " - " + Name;
        }
    }
}

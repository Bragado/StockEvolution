using System;
using System.ComponentModel;

namespace CMOV_Stocks
{
    public class Company : INotifyPropertyChanged {
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

        

        public Company(string _code, string _name) {
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

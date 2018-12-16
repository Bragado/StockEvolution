using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace CMOV_Stocks
{
    class Utils
    {
        public static float FindBoundValue(ArrayList arrayList, Boolean max) {
            float aux = float.MinValue;
            if (!max)
                aux = float.MaxValue;

            for (int i = 0; i < arrayList.Count; i++) {
                if (!max && (float) arrayList[i] < aux)
                    aux = (float) arrayList[i];
                else if (max && (float) arrayList[i] > aux)
                    aux = (float) arrayList[i];
            }
            return aux;
        }

        public static ArrayList ParseStockPoints(string message, int period) {
            JObject json = JObject.Parse(message);
            JArray jArray = (JArray) json["results"];
            ArrayList points = new ArrayList();
            for (int i = jArray.Count - 1; i > jArray.Count - period - 1; i--) {
                float value = (float) jArray[i]["close"];
                points.Add(value);
            }

            points.Reverse();
            return points;
        }



        public static ArrayList GetXLabels(ArrayList points, int x) {
            if (points == null) {
                return new ArrayList() { "1", "2", "3", "4" };
            }

            ArrayList ret = new ArrayList();
            float maxValue = FindBoundValue(points, true);
            float minValue = FindBoundValue(points, false);
            float diff = maxValue - minValue;
            float sec = minValue + diff / 3;
            float third = sec + diff / 3;

            return new ArrayList() { minValue.ToString("0.00"), sec.ToString("0.00"), third.ToString("0.00"), maxValue.ToString("0.00") };
        }
        public static ArrayList GetYLabels(string pointsDay, int y) {
            if (pointsDay.Equals(""))
                return new ArrayList() { "Seg", "Ter", "Qua", "Qui", "Sex", "Sab", "Dom" };
            JObject json = JObject.Parse(pointsDay);
            JArray jArray = (JArray) json["results"];

            string finalDay = (string) jArray[jArray.Count - 1]["tradingDay"];
            string initialDay = (string) jArray[jArray.Count - y]["tradingDay"];
            if (y == 7) {
                return new ArrayList() { initialDay, "", "", "", "", "", finalDay };
            } else if (y == 30) {
                return new ArrayList() { initialDay, "", "", "", "", finalDay };
            } else {
                return new ArrayList() { "Seg", "Ter", "Qua", "Qui", "Sex", "Sab", "Dom" };
            }
        }
    }
}

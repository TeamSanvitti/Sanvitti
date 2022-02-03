using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV.Framework.Common.LabelGenerator
{
    public class ShipMethodOperation
    {

        public static List<ShipMethod> GetShipMethods(bool IsInternational)
        {
            List<ShipMethod> shipMethods = new List<ShipMethod>();

            if(IsInternational)
            { }
            else
            {

            }


            return shipMethods;
        }

        public static List<ShipShapeInfo> GetShipPackageShapes(bool IsInternational)
        {
            List<ShipShapeInfo> shiShapes = new List<ShipShapeInfo>();
            ShipShapeInfo shipShapeInfo = null;

            if (IsInternational)
            {
                string[] itemNames = System.Enum.GetNames(typeof(SV.Framework.Common.LabelGenerator.InternationalShipPackageShape));

                for (int i = 0; i <= itemNames.Length - 1; i++)
                {
                    shipShapeInfo = new ShipShapeInfo();
                    shipShapeInfo.ShipShapeText = itemNames[i];
                    shipShapeInfo.ShipShapeValue = itemNames[i];

                    shiShapes.Add(shipShapeInfo);
                }

            }
            else
            {
                string[] itemNames = System.Enum.GetNames(typeof(SV.Framework.Common.LabelGenerator.ShipPackageShape));

                for (int i = 0; i <= itemNames.Length - 1; i++)
                {
                    shipShapeInfo = new ShipShapeInfo();
                    shipShapeInfo.ShipShapeText = itemNames[i];
                    shipShapeInfo.ShipShapeValue = itemNames[i];

                    shiShapes.Add(shipShapeInfo);
                }
            }


            return shiShapes;
        }



    }
}
